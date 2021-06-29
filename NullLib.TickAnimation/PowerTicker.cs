using System;

namespace NullLib.TickAnimation
{
    public class PowerTicker : FuncTickerBase
    {
        private double power = 2;

        public PowerTicker()
        {
        }

        public PowerTicker(EasingMode mode) : base(mode)
        {
        }

        public double Power { get => power; set => power = value; }
        protected override double CalcInTick(double x)
        {
            double y = Math.Max(0.0, power);
            return Math.Pow(x, y);
        }
    }
}
