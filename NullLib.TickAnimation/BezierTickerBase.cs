namespace NullLib.TickAnimation
{
    public abstract class BezierTickerBase : ITicker
    {
        protected abstract double GetSampleRate(double x);
        protected abstract double GetSampleValue(double rate);

        public double CalcTick(double x)
        {
            return GetSampleValue(GetSampleRate(x));
        }
    }
}