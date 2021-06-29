using System;

namespace NullLib.TickAnimation
{
    public class BackTicker : FuncTickerBase
    {
        private double amplitude = 1.0;

        public BackTicker()
        {
        }

        public BackTicker(EasingMode mode) : base(mode)
        {
        }

        public double Amplitude { get => amplitude; set => amplitude = value; }
        protected override double CalcInTick(double x)
        {
            double num = Math.Max(0.0, amplitude);
            return Math.Pow(x, 3.0) - x * num * Math.Sin(Math.PI * x);
        }
    }
}
