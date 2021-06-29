using System;

namespace NullLib.TickAnimation
{
    public class QuadraticBezierTicker : BezierTickerBase
    {
        private void GetBasicCurve(QuadraticBezierCurve curve, EasingMode mode, out double x, out double y)
        {
            var values = mode switch
            {
                EasingMode.Ease => (0, 0),
                EasingMode.EaseInOut => (0, 0),
                EasingMode.EaseIn => curve switch
                {
                    QuadraticBezierCurve.Ease => (0.42, 0),
                    QuadraticBezierCurve.Speed => (0.4, 0),
                    _ => (0, 0)
                },
                EasingMode.EaseOut => curve switch
                {
                    QuadraticBezierCurve.Ease => (0.58, 1),
                    QuadraticBezierCurve.Speed => (0.2, 1),
                    _ => (0, 0)
                },
                _ => throw new ArgumentOutOfRangeException(nameof(mode))
            };
            x = values.Item1;
            y = values.Item2;
        }

        private double y;
        private double x;

        public double X { get => x; set => x = value; }
        public double Y { get => y; set => y = value; }

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


        public QuadraticBezierTicker(double x, double y) => SetBezier(x, y);
        public QuadraticBezierTicker(QuadraticBezierCurve curve, EasingMode mode) => SetBezier(curve, mode);
        public QuadraticBezierTicker(QuadraticBezierCurve curve) : this(curve, EasingMode.Ease) { }
        public QuadraticBezierTicker() : this(QuadraticBezierCurve.Linear) { }

        public void SetBezier(double x, double y)
        {
            this.X = x;
            this.Y = y;
        }
        public void SetBezier(QuadraticBezierCurve curve, EasingMode mode)
        {
            GetBasicCurve(curve, mode, out x, out y);
        }

        protected override double GetSampleRate(double x) => GetSampleRate(this.X, x);
        protected override double GetSampleValue(double rate) => GetSamplePoint(this.X, rate);
    }
}
