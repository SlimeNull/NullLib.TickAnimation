using System;
using System.Reflection;
using System.Threading.Tasks;

namespace NullLib.TickAnimation
{
    public class TickerBuilder
    {
        private ITicker ticker;

        private TickerBuilder()
        {
        }
        public TickerBuilder Linear()
        {
            ticker = new LinearTicker();
            return this;
        }

        public TickerBuilder QuadraticBezier(double x1, double y1)
        {
            ticker = new QuadraticBezierTicker(x1, y1);
            return this;
        }
        public TickerBuilder QuadraticBezier(QuadraticBezierCurve curve, EasingMode mode = EasingMode.Ease)
        {
            ticker = new QuadraticBezierTicker(curve, mode);
            return this;
        }

        public TickerBuilder CubicBezier(double x1, double y1, double x2, double y2)
        {
            ticker = new CubicBezierTicker(x1, y1, x2, y2);
            return this;
        }
        public TickerBuilder CubicBezier(CubicBezierCurve curve, EasingMode mode = EasingMode.Ease)
        {
            ticker = new CubicBezierTicker(curve, mode);
            return this;
        }

        public ITicker Build()
        {
            return ticker;
        }

        public static TickerBuilder Builder()
        {
            return new TickerBuilder();
        }
    }

    public class AnimatorBuilder
    {
        private object obj;
        private PropertyInfo prop;
        private ITicker ticker;

        public delegate ITicker TickerBuilderDelegate(TickerBuilder builder);
        private AnimatorBuilder()
        {
        }

        public AnimatorBuilder Linear()
        {
            ticker = new LinearTicker();
            return this;
        }

        public AnimatorBuilder Ticker(ITicker ticker)
        {
            this.ticker = ticker;
            return this;
        }

        public AnimatorBuilder Ticker(TickerBuilderDelegate tickerBuilderDelegate)
        {
            this.ticker = tickerBuilderDelegate(TickerBuilder.Builder());
            return this;
        }

        public AnimatorBuilder Binding(object obj, PropertyInfo prop)
        {
            this.obj = obj;
            this.prop = prop;
            return this;
        }

        public AnimatorBuilder Binding(object obj, string propName)
        {
            PropertyInfo prop = obj.GetType().GetProperty(propName);
            if (prop == null)
                throw new ArgumentOutOfRangeException(propName, "Property not found");
            return Binding(obj, prop);
        }

        public TickAnimator Build() => new TickAnimator(ticker, obj, prop);

        public Task Animate<VT>(Func<double, VT> tickPicker, TimeSpan timeSpan) =>
            Build().Animate(tickPicker, timeSpan);
        public Task Animate(double start, double end, int dur) => Build().Animate(start, end, dur);
        public Task Animate(float start, float end, int dur) => Build().Animate(start, end, dur);
        public Task Animate(int start, int end, int dur) => Build().Animate(start, end, dur);
        public Task Animate(bool start, bool end, int dur)
        {
            if (start != end)
                return Build().Animate(t => start ^ t >= 1, TimeSpan.FromMilliseconds(dur));
            else
                return Build().Animate(t => start, TimeSpan.FromMilliseconds(dur));
        }
        public Task Animate(double end, int dur) => Build().Animate(end, dur);
        public Task Animate(float end, int dur) => Build().Animate(end, dur);
        public Task Animate(int end, int dur) => Build().Animate(end, dur);
        public Task Animate(bool end, int dur)
        {
            var animator = Build();
            var start = animator.GetPropertyValue<bool>();
            if (start != end)
                return animator.Animate(t => start ^ t >= 1, TimeSpan.FromMilliseconds(dur));
            else
                return animator.Animate(t => start, TimeSpan.FromMilliseconds(dur));
        }

        public static AnimatorBuilder Builder()
        {
            return new AnimatorBuilder()
                .Linear();
        }
    }
}