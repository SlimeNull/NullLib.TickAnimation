namespace NullLib.TickAnimation
{
    public abstract class BezierTickerBase : ITicker
    {
        protected int count;
        protected double[] ticks;
        public int Count
        {
            get => count;
            set
            {
                count = value;
                ResetSequence();
            }
        }

        protected void ResetSequence()
        {
            ticks = null;
        }
        protected abstract double GetSampleRate(double x);
        protected abstract double GetSampleValue(double rate);

        protected double[] GenTicks()
        {
            double[] result = new double[count];
            double count_d = count;
            for (int i = 1; i <= count; i++)
                result[i - 1] = GetSampleValue(GetSampleRate(i / count_d));
            return result;
        }
        public double CalcTick(double x)
        {
            return GetSampleValue(GetSampleRate(x));
        }
        public double[] GetTicks()
        {
            return ticks ??= GenTicks();
        }
    }
}