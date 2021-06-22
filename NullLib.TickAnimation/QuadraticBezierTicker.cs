using System;
using System.Linq;

namespace NullLib.TickAnimation
{
    public class QuadraticBezierTicker : BezierTickerBase
    {
        double x, y;

        public double X
        {
            get => x;
            set
            {
                x = value;
                ResetSequence();
            }
        }
        public double Y
        {
            get => y;
            set
            {
                y = value;
                ResetSequence();
            }
        }

        private double GetSamplePoint(double cp, double rate)
        {
            return 2 * rate * (1 - rate) * cp + rate * rate * 1;
        }
        private double PickAppropriateRate(double cp, double p, params double[] rates)
        {
            double result = double.NaN;
            double diffNow = double.MaxValue;
            foreach (var rate in rates)
                if (rate >= 0 && rate <= 1 && Math.Abs(GetSamplePoint(cp, rate) - p) < diffNow)
                    result = rate;
            return result;
        }
        private double GetSampleRate(double cp, double p)
        {
            double
                a = 1 - 2 * cp,
                b = 2 * cp,
                c = -p;
            double
                delta = b * b - 4 * a * c;
            if (delta > 0)
            {
                double
                    x1 = (-b + Math.Sqrt(delta)) / (2 * a),
                    x2 = (-b - Math.Sqrt(delta)) / (2 * a);
                double rst = PickAppropriateRate(cp, p, x1, x2);
                return rst;
            }
            else if (delta == 0)
            {
                double
                    x = -b / (2 * a);
                return x;
            }
            else
            {
                throw new Exception("Fuck");
            }
        }

        public QuadraticBezierTicker(double x, double y, int count) => SetOption(x, y, count);
        public QuadraticBezierTicker(double x, double y) : this(x, y, 15) { }
        public QuadraticBezierTicker(QuadraticCurves curve, int count)
        {
            switch (curve)
            {
                case QuadraticCurves.Linear: SetOption(0, 0, count); break;
                case QuadraticCurves.EaseIn: SetOption(0.42, 0, count); break;
                case QuadraticCurves.EaseOut: SetOption(0.58, 1.0, count); break;
                case QuadraticCurves.FastOutLinearIn: SetOption(0.4, 0, count); break;
                case QuadraticCurves.LinearOutSlowIn: SetOption(0, 0, count); break;
                default: throw new ArgumentOutOfRangeException(nameof(curve));
            }
        }
        public QuadraticBezierTicker(QuadraticCurves curve) : this(curve, 15) { }
        public QuadraticBezierTicker() : this(QuadraticCurves.Linear) { }

        public void SetBezier(double x, double y)
        {
            this.x = x;
            this.y = y;
            ResetSequence();
        }
        public void SetOption(double x, double y, int count)
        {
            this.x = x;
            this.y = y;
            this.count = count;
            ResetSequence();
        }

        protected override double GetSampleRate(double x) => GetSampleRate(this.x, x);
        protected override double GetSampleValue(double rate) => GetSamplePoint(this.x, rate);
    }
    public enum QuadraticCurves
    {
        /// <summary>
        /// X:0, Y:0
        /// </summary>
        Linear,           // 0, 0
        /// <summary>
        /// X:0.42, Y:0
        /// </summary>
        EaseIn,           // 0.42, 0
        /// <summary>
        /// X:0.58, Y:1.0
        /// </summary>
        EaseOut,          // 0.58, 1.0

        /// <summary>
        /// X:0.4, Y:0
        /// </summary>
        FastOutLinearIn,  // 0.4, 0
        /// <summary>
        /// X:0.2, Y:1.0
        /// </summary>
        LinearOutSlowIn,  // 0.2, 10
    }
}
