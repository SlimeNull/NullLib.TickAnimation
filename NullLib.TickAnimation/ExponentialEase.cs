using System;
using MS.Internal;

namespace NullLib.TickAnimation
{
    public class ExponentialEase : FuncTickerBase
    {
        private double exponent = 2;

        public ExponentialEase()
        {
        }

        public ExponentialEase(EasingMode mode) : base(mode)
        {
        }

        public double Exponent { get => exponent; set => exponent = value; }
        protected override double CalcInTick(double x)
        {
            double exponent = this.exponent;
            if (DoubleUtil.IsZero(exponent))
                return x;
            return (Math.Exp(exponent * x) - 1.0) / (Math.Exp(exponent) - 1.0);
        }
    }
}
