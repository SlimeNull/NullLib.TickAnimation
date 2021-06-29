namespace NullLib.TickAnimation
{
    public class QuinticTicker : FuncTickerBase
    {
        public QuinticTicker()
        {
        }

        public QuinticTicker(EasingMode mode) : base(mode)
        {
        }

        protected override double CalcInTick(double x)
        {
            return x * x * x * x * x;
        }
    }
}
