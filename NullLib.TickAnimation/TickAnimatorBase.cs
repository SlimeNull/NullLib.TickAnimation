using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace NullLib.TickAnimation
{
    public class TickAnimatorBase : ITickAnimator
    {
        protected Type objType;

        protected ITicker ticker;
        protected int delay = 10;
        protected object obj;
        protected PropertyInfo prop;
        protected object doingState;
        protected Action<Action> propSetter = null;

        protected CancellationTokenSource runningAnimationCancellationTokenSouorce;

        public ITicker Ticker => ticker;
        public object TargetInstance => obj;
        public PropertyInfo TargetProperty => prop;
        public int TickDelay { get => delay; }

        public void SetTicker(ITicker ticker)
        {
            if (ticker is null)
                throw new ArgumentNullException(nameof(ticker));
            this.ticker = ticker;
        }
        public void SetTickDelay(int delay)
        {
            this.delay = delay;
        }
        public void SetPropertySetter(Action<Action> setter)
        {
            propSetter = setter;
        }
        public void SetTargetInstance(object obj)
        {
            if (obj is null)
                throw new ArgumentNullException(nameof(obj));
            this.obj = obj;
            this.objType = obj.GetType();
        }
        public void SetTargetProperty(PropertyInfo prop)
        {
            if (prop == null)
                throw new ArgumentNullException(nameof(prop));
            if (!prop.DeclaringType.IsAssignableFrom(objType))
                throw new ArgumentOutOfRangeException(nameof(prop), $"{prop.Name} is not defined in class {objType.Name}");
            if (!prop.CanRead)
                throw new ArgumentOutOfRangeException(nameof(prop), "Property cannot be read!");
            if (!prop.CanWrite)
                throw new ArgumentOutOfRangeException(nameof(prop), "Property cannot be written!");
            this.prop = prop;
        }
        public void SetTargetProperty(string propName)
        {
            PropertyInfo prop = objType.GetProperty(propName);
            if (prop == null)
                throw new ArgumentOutOfRangeException(propName, "Property not found");
            SetTargetProperty(prop);
        }

        public Task Animate<VT>(Func<double, VT> tickPicker, TimeSpan timeSpan)
        {
            InitAnimation<VT>();

            CancellationTokenSource tokenSource = new CancellationTokenSource();
            CancellationToken token = tokenSource.Token;

            Func<VT, bool> callback;
            if (propSetter == null)
            {
                callback = (value) =>
                {
                    if (token.IsCancellationRequested)
                        return false;

                    prop.SetValue(obj, value);
                    Thread.Sleep(delay);
                    return true;
                };
            }
            else
            {
                callback = (value) =>
                {
                    if (token.IsCancellationRequested)
                        return false;

                    propSetter.Invoke(() => prop.SetValue(obj, value));
                    Thread.Sleep(delay);
                    return true;
                };
            }

            runningAnimationCancellationTokenSouorce = tokenSource;
            return Task.Run(() =>
            {
                TickAnimationProc.SyncAnimate(ticker, tickPicker, timeSpan, callback);
            });
        }
        public ITickAnimator SyncAnimate<VT>(Func<double, VT> tickPicker, TimeSpan timeSpan)
        {
            InitAnimation<VT>();

            Action<VT> callback;
            if (propSetter == null)
            {
                callback = (value) =>
                {
                    prop.SetValue(obj, value);
                    Thread.Sleep(delay);
                };
            }
            else
            {
                callback = (value) => propSetter.Invoke(() =>
                {
                    prop.SetValue(obj, value);
                    Thread.Sleep(delay);
                });
            }

            TickAnimationProc.SyncAnimate(ticker, tickPicker, timeSpan, callback);
            return this;
        }


        bool CheckPropType(Type type) => prop.PropertyType.IsAssignableFrom(type);

        private void InitAnimation<VT>()
        {
            if (!CheckPropType(typeof(VT)))
                throw new InvalidOperationException("Type not match specified property type");
            if (runningAnimationCancellationTokenSouorce != null)
                runningAnimationCancellationTokenSouorce.Cancel();
        }
    }
}