using System;
using MS.Internal;

namespace NullLib.TickAnimation
{
    public class ElasticTicker : FuncTickerBase
    {
        private int oscillations = 3;
        private double springiness = 3.0;

        public ElasticTicker()
        {
        }

        public ElasticTicker(EasingMode mode) : base(mode)
        {
        }

        public int Oscillations { get => oscillations; set => oscillations = value; }
        public double Springiness { get => springiness; set => springiness = value; }
        protected override double CalcInTick(double x)
        {
            double num = Math.Max(0.0, Oscillations);
            double num2 = Math.Max(0.0, Springiness);
            double num3 = ((!DoubleUtil.IsZero(num2)) ? ((Math.Exp(num2 * x) - 1.0) / (Math.Exp(num2) - 1.0)) : x);
            return num3 * Math.Sin((Math.PI * 2.0 * num + Math.PI / 2.0) * x);
        }
    }
}
