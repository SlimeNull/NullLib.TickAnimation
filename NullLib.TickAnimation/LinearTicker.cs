namespace NullLib.TickAnimation
{
    public class LinearTicker : TickerBase
    {
        public LinearTicker()
        {
        }

        public override double CalcTick(double x)
        {
            return x;
        }
    }
}