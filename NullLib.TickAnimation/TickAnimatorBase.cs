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

        public ITickAnimator SetTicker(ITicker ticker)
        {
            if (ticker is null)
                throw new ArgumentNullException(nameof(ticker));
            this.ticker = ticker;
            return this;
        }
        public ITickAnimator SetTickDelay(int delay)
        {
            this.delay = delay;
            return this;
        }
        public ITickAnimator SetPropertySetter(Action<Action> setter)
        {
            propSetter = setter;
            return this;
        }
        public ITickAnimator SetTargetInstance(object obj)
        {
            if (obj is null)
                throw new ArgumentNullException(nameof(obj));
            this.obj = obj;
            this.objType = obj.GetType();
            return this;
        }
        public ITickAnimator SetTargetProperty(PropertyInfo prop)
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
            return this;
        }
        public ITickAnimator SetTargetProperty(string propName)
        {
            PropertyInfo prop = objType.GetProperty(propName);
            if (prop == null)
                throw new ArgumentOutOfRangeException(propName, "Property not found");
            return SetTargetProperty(prop);
        }

        public Task Animate<VT>(Func<double, VT> tickPicker, TimeSpan timeSpan)
        {
            InitAnimation<VT>();

            CancellationTokenSource tokenSource = new CancellationTokenSource();
            CancellationToken token = tokenSource.Token;

            Func<VT, Task<bool>> callback;
            if (propSetter == null)
            {
                callback = async (value) =>
                {
                    if (token.IsCancellationRequested)
                        return false;

                    prop.SetValue(obj, value);
                    await Task.Delay(delay, token);
                    return true;
                };
            }
            else
            {
                callback = async (value) =>
                {
                    if (token.IsCancellationRequested)
                        return false;

                    propSetter.Invoke(() => prop.SetValue(obj, value));
                    await Task.Delay(delay, token);
                    return true;
                };
            }

            runningAnimationCancellationTokenSouorce = tokenSource;
            return TickAnimationProc.Animate(ticker, tickPicker, timeSpan, callback);
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

        private void InitAnimation<VT>()
        {
            if (!prop.PropertyType.IsAssignableFrom(typeof(VT)))
                throw new InvalidOperationException("Type not match specified property type");
            if (runningAnimationCancellationTokenSouorce != null)
                runningAnimationCancellationTokenSouorce.Cancel();
        }
    }
}