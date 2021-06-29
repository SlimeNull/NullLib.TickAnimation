namespace NullLib.TickAnimation
{
    public class QuadraticTicker : FuncTickerBase
    {
        public QuadraticTicker()
        {
        }

        public QuadraticTicker(EasingMode mode) : base(mode)
        {
        }

        protected override double CalcInTick(double x)
        {
            return x * x;
        }
    }
}
