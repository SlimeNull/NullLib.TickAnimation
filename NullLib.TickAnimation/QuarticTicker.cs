namespace NullLib.TickAnimation
{
    public class QuarticTicker : FuncTickerBase
    {
        public QuarticTicker()
        {
        }

        public QuarticTicker(EasingMode mode) : base(mode)
        {
        }

        protected override double CalcInTick(double x)
        {
            return x * x * x * x;
        }
    }
}
