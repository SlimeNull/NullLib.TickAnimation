using System;
using System.Reflection;
using System.Threading.Tasks;

namespace NullLib.TickAnimation
{
    public class TickAnimator : TickAnimatorBase, ITickAnimator, ITickAnimator<double>, ITickAnimator<float>, ITickAnimator<int>
    {
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

        public VT GetPropertyValue<VT>()
        {
            if (prop.GetValue(obj) is VT rst)
                return rst;
            else
                throw new NotSupportedException($"Property type is not {typeof(VT).Name}");
        }

        public Task Animate(double start, double end, int dur)
        {
            double diff = end - start;
            return Animate((t) => (double)(start + diff * t), TimeSpan.FromMilliseconds(dur));
        }
        public Task Animate(float start, float end, int dur)
        {
            double diff = end - start;
            return Animate((t) => (float)(start + diff * t), TimeSpan.FromMilliseconds(dur));
        }
        public Task Animate(int start, int end, int dur)
        {
            double diff = end - start;
            return Animate((t) => (int)(start + diff * t), TimeSpan.FromMilliseconds(dur));
        }
        public Task Animate(double end, int dur) => Animate(GetPropertyValue<double>(), end, dur);
        public Task Animate(float end, int dur) => Animate(GetPropertyValue<float>(), end, dur);
        public Task Animate(int end, int dur) => Animate(GetPropertyValue<int>(), end, dur);

        public ITickAnimator<double> SyncAnimate(double start, double end, int dur)
        {
            double diff = end - start;
            SyncAnimate((t) => (double)(start + diff * t), TimeSpan.FromMilliseconds(dur));
            return this;
        }
        public ITickAnimator<float> SyncAnimate(float start, float end, int dur)
        {
            double diff = end - start;
            SyncAnimate((t) => (float)(start + diff * t), TimeSpan.FromMilliseconds(dur));
            return this;
        }
        public ITickAnimator<int> SyncAnimate(int start, int end, int dur)
        {
            double diff = end - start;
            SyncAnimate((t) => (int)(start + diff * t), TimeSpan.FromMilliseconds(dur));
            return this;
        }
        public ITickAnimator<double> SyncAnimate(double end, int dur) => SyncAnimate(GetPropertyValue<double>(), end, dur);
        public ITickAnimator<float> SyncAnimate(float end, int dur) => SyncAnimate(GetPropertyValue<float>(), end, dur);
        public ITickAnimator<int> SyncAnimate(int end, int dur) => SyncAnimate(GetPropertyValue<int>(), end, dur);
    }
}
