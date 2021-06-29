using System;

namespace NullLib.TickAnimation
{
    public class SineTicker : FuncTickerBase
    {
        public SineTicker()
        {
        }

        public SineTicker(EasingMode mode) : base(mode)
        {
        }

        protected override double CalcInTick(double x)
        {
            return 1.0 - Math.Sin(Math.PI / 2.0 * (1.0 - x));
        }
    }
}
