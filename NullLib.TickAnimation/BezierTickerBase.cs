namespace NullLib.TickAnimation
{
    public abstract class BezierTickerBase : TickerBase
    {
        protected abstract double GetSampleRate(double x);
        protected abstract double GetSampleValue(double rate);

        public override double CalcTick(double x)
        {
            return GetSampleValue(GetSampleRate(x));
        }
    }
}