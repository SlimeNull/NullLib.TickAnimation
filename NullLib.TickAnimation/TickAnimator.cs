using System;
using System.Reflection;
using System.Threading.Tasks;

namespace NullLib.TickAnimation
{
    public class TickAnimator : ITickAnimator<double>, ITickAnimator<float>, ITickAnimator<int>
    {
        Type objType;

        ITicker ticker;
        object obj;
        PropertyInfo prop;
        object doingState;
        Action<Action> propSetter = null;

        public ITicker Ticker => ticker;
        public object TargetInstance => obj;
        public PropertyInfo TargetProperty => prop;

        public TickAnimator(ITicker ticker, object obj, PropertyInfo prop)
        {
            SetTicker(ticker);
            SetTargetInstance(obj);
            SetTargetProperty(prop);
        }
        public TickAnimator(object obj, PropertyInfo prop) : this(new QuadraticBezierTicker(), obj, prop) { }
        public TickAnimator(ITicker ticker, object obj, string propName)
        {
            SetTicker(ticker);
            SetTargetInstance(obj);
            SetTargetProperty(propName);
        }
        public TickAnimator(object obj, string propName) : this(new QuadraticBezierTicker(), obj, propName) { }
        bool CheckPropType(Type type) => prop.PropertyType.IsAssignableFrom(type);

        private void InitAnimation(out object newObj)
        {
            doingState = newObj = new object();
        }

        public void SetTicker(ITicker ticker)
        {
            if (ticker is null)
                throw new ArgumentNullException(nameof(ticker));
            this.ticker = ticker;
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
            if (!prop.CanWrite)
                throw new ArgumentOutOfRangeException(nameof(prop), "Readonly property!");
            this.prop = prop;
        }
        public void SetTargetProperty(string propName)
        {
            PropertyInfo prop = objType.GetProperty(propName);
            if (prop == null)
                throw new ArgumentOutOfRangeException(propName, "Property not found");
            SetTargetProperty(prop);
        }
        public void SetPropertySetter(Action<Action> setter)
        {
            propSetter = setter;
        }

        private Func<VT, bool> GetAnimationCallback<VT>(object state)
        {
            if (propSetter == null)
                return (v) =>
                {
                    prop.SetValue(obj, v);
                    return doingState == state;
                };
            else
                return (v) =>
                {
                    propSetter.Invoke(() =>　prop.SetValue(obj, v));
                    return doingState == state;
                };
        }

        public void SyncFrameAnimate<VT>(Func<double, VT> tickPicker, TimeSpan timeSpan)
        {
            if (!CheckPropType(typeof(VT)))
                throw new InvalidOperationException("Type not match specified property type");
            InitAnimation(out var newState);
            TickAnimationProc.SyncFrameAnimate(ticker, tickPicker, timeSpan, GetAnimationCallback<VT>(newState));
        }
        public Task FrameAnimate<VT>(Func<double, VT> tickPicker, TimeSpan timeSpan)
        {
            if (!CheckPropType(typeof(VT)))
                throw new InvalidOperationException("Type not match specified property type");
            InitAnimation(out var newState);
            return TickAnimationProc.FrameAnimate(ticker, tickPicker, timeSpan, GetAnimationCallback<VT>(newState));
        }
        public void SyncSmoothAnimate<VT>(Func<double, VT> tickPicker, TimeSpan timeSpan)
        {
            if (!CheckPropType(typeof(VT)))
                throw new InvalidOperationException("Type not match specified property type");
            InitAnimation(out var newState);
            TickAnimationProc.SyncSmoothAnimate(ticker, tickPicker, timeSpan, GetAnimationCallback<VT>(newState));
        }
        public Task SmoothAnimate<VT>(Func<double, VT> tickPicker, TimeSpan timeSpan)
        {
            if (!CheckPropType(typeof(VT)))
                throw new InvalidOperationException("Type not match specified property type");
            InitAnimation(out var newState);
            return TickAnimationProc.SmoothAnimate(ticker, tickPicker, timeSpan, GetAnimationCallback<VT>(newState));
        }

        public Task FrameAnimate(double start, double end, int dur)
        {
            double diff = end - start;
            return FrameAnimate((t) => (double)(start + diff * t), TimeSpan.FromMilliseconds(dur));
        }
        public Task FrameAnimate(float start, float end, int dur)
        {
            double diff = end - start;
            return FrameAnimate((t) => (float)(start + diff * t), TimeSpan.FromMilliseconds(dur));
        }
        public Task FrameAnimate(int start, int end, int dur)
        {
            double diff = end - start;
            return FrameAnimate((t) => (int)(start + diff * t), TimeSpan.FromMilliseconds(dur));
        }

        public Task SmoothAnimate(double start, double end, int dur)
        {
            double diff = end - start;
            return SmoothAnimate((t) => (double)(start + diff * t), TimeSpan.FromMilliseconds(dur));
        }
        public Task SmoothAnimate(float start, float end, int dur)
        {
            double diff = end - start;
            return SmoothAnimate((t) => (float)(start + diff * t), TimeSpan.FromMilliseconds(dur));
        }
        public Task SmoothAnimate(int start, int end, int dur)
        {
            double diff = end - start;
            return SmoothAnimate((t) => (int)(start + diff * t), TimeSpan.FromMilliseconds(dur));
        }

        public Task Animate(double start, double end, int dur) =>
            SmoothAnimate(start, end, dur);
        public Task Animate(float start, float end, int dur) =>
            SmoothAnimate(start, end, dur);
        public Task Animate(int start, int end, int dur) =>
            SmoothAnimate(start, end, dur);

        public void SyncFrameAnimate(double start, double end, int dur)
        {
            double diff = end - start;
            SyncFrameAnimate((t) => (double)(start + diff * t), TimeSpan.FromMilliseconds(dur));
        }
        public void SyncFrameAnimate(float start, float end, int dur)
        {
            double diff = end - start;
            SyncFrameAnimate((t) => (float)(start + diff * t), TimeSpan.FromMilliseconds(dur));
        }
        public void SyncFrameAnimate(int start, int end, int dur)
        {
            double diff = end - start;
            SyncFrameAnimate((t) => (int)(start + diff * t), TimeSpan.FromMilliseconds(dur));
        }

        public void SyncSmoothAnimate(double start, double end, int dur)
        {
            double diff = end - start;
            SyncSmoothAnimate((t) => (double)(start + diff * t), TimeSpan.FromMilliseconds(dur));
        }
        public void SyncSmoothAnimate(float start, float end, int dur)
        {
            double diff = end - start;
            SyncSmoothAnimate((t) => (float)(start + diff * t), TimeSpan.FromMilliseconds(dur));
        }
        public void SyncSmoothAnimate(int start, int end, int dur)
        {
            double diff = end - start;
            SyncSmoothAnimate((t) => (int)(start + diff * t), TimeSpan.FromMilliseconds(dur));
        }

        public void SyncAnimate(double start, double end, int dur) =>
            SyncSmoothAnimate(start, end, dur);
        public void SyncAnimate(float start, float end, int dur) =>
            SyncSmoothAnimate(start, end, dur);
        public void SyncAnimate(int start, int end, int dur) =>
            SyncSmoothAnimate(start, end, dur);
    }

    //public class TickAnimator<T> : TickAnimator where T : class
    //{
    //    public new T TargetInstance => base.TargetInstance as T;
    //    public void SetTargetInstance(T obj) => base.SetTargetInstance(obj);

    //    public TickAnimator(ITicker ticker, T obj, PropertyInfo prop) : base(ticker, obj, prop) { }
    //    public TickAnimator(T obj, PropertyInfo prop) : base(obj, prop) { }
    //    public TickAnimator(ITicker ticker, T obj, string propName) : base(ticker, obj, propName) { }
    //    public TickAnimator(T obj, string propName) : base(obj, propName) { }
    //}
}
