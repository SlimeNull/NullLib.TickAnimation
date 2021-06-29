using System;
using MS.Internal;

namespace NullLib.TickAnimation
{
    public class BounceTicker : FuncTickerBase
    {
        private int bounces = 3;
        private double bounciness = 2;

        public BounceTicker()
        {
        }

        public BounceTicker(EasingMode mode) : base(mode)
        {
        }

        public int Bounces { get => bounces; set => bounces = value; }
        public double Bounciness { get => bounciness; set => bounciness = value; }
        protected override double CalcInTick(double x)
		{
			double num = Math.Max(0.0, Bounces);
			double num2 = Bounciness;
			if (num2 < 1.0 || DoubleUtil.IsOne(num2))
			{
				num2 = 1.001;
			}
			double num3 = Math.Pow(num2, num);
			double num4 = 1.0 - num2;
			double num5 = (1.0 - num3) / num4 + num3 * 0.5;
			double num6 = x * num5;
			double d = Math.Log((0.0 - num6) * (1.0 - num2) + 1.0, num2);
			double num7 = Math.Floor(d);
			double y = num7 + 1.0;
			double num8 = (1.0 - Math.Pow(num2, num7)) / (num4 * num5);
			double num9 = (1.0 - Math.Pow(num2, y)) / (num4 * num5);
			double num10 = (num8 + num9) * 0.5;
			double num11 = x - num10;
			double num12 = num10 - num8;
			double num13 = Math.Pow(1.0 / num2, num - num7);
			return (0.0 - num13) / (num12 * num12) * (num11 - num12) * (num11 + num12);
		}
    }
}
