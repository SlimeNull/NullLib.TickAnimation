namespace NullLib.TickAnimation
{
    public class CubicTicker : FuncTickerBase
    {
        public CubicTicker()
        {
        }

        public CubicTicker(EasingMode mode) : base(mode)
        {
        }

        protected override double CalcInTick(double x)
        {
            return x * x * x;
        }
    }
}
