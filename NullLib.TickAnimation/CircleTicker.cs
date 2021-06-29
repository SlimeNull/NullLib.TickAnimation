using System;

namespace NullLib.TickAnimation
{
    public class CircleTicker : FuncTickerBase
    {
        public CircleTicker()
        {
        }

        public CircleTicker(EasingMode mode) : base(mode)
        {
        }

        protected override double CalcInTick(double x)
        {
            x = Math.Max(0.0, Math.Min(1.0, x));
            return 1.0 - Math.Sqrt(1.0 - x * x);
        }
    }
}
