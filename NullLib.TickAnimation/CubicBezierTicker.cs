using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace NullLib.TickAnimation
{
    public class CubicBezierTicker : BezierTickerBase
    {
        private double x1;
        private double y1;
        private double x2;
        private double y2;

        private void GetBasicCurve(CubicBezierCurve curve, EasingMode mode, out double x1, out double y1, out double x2, out double y2)
        {
            var values = mode switch
            {
                EasingMode.Ease => curve switch
                {
                    CubicBezierCurve.Ease => (0.25, 0.1, 0.25, 1.0),                    // Ease
                    CubicBezierCurve.Sine => (0.45, 0.275, 0.55, 0.725),                // InOutSine         (half)
                    CubicBezierCurve.Quadratic => (0.46, 0.265, 0.52, 0.73),            // InOutQuadratic    (half)
                    CubicBezierCurve.Cubic => (0.65, 0.275, 0.36, 0.75),                // InOutCubic        (half)
                    CubicBezierCurve.Back => (0.68, -0.275, 0.27, 1.275),               // InOutBack         (half)
                    CubicBezierCurve.Speed => (0.4, 0.25, 0.2, 0.75),                   // FastOutSlowIn     (half)
                    _ => (0, 0, 1, 1)
                },
                EasingMode.EaseIn => curve switch
                {
                    CubicBezierCurve.Ease => (0.42, 0, 1, 1),                           // EaseIn
                    CubicBezierCurve.Sine => (0.47, 0, 0.75, 0.72),                     // InSine
                    CubicBezierCurve.Quadratic => (0.55, 0.09, 0.68, 0.53),             // InQuadratic
                    CubicBezierCurve.Cubic => (0.55, 0.06, 0.68, 0.19),                 // InCubic
                    CubicBezierCurve.Back => (0.6, -0.28, 0.74, 0.05),                  // InBack
                    CubicBezierCurve.Speed => (0.4, 0, 1, 1),                           // FastOutLinearIn
                    _ => (0, 0, 1, 1)
                },
                EasingMode.EaseOut => curve switch
                {
                    CubicBezierCurve.Ease => (0, 0, 0.58, 1.0),                         // EaseOut
                    CubicBezierCurve.Sine => (0.39, 0.58, 0.57, 1),                     // OutSine
                    CubicBezierCurve.Quadratic => (0.25, 0.46, 0.45, 0.94),             // OutQuadratic
                    CubicBezierCurve.Cubic => (0.22, 0.61, 0.36, 1),                    // OutCubic
                    CubicBezierCurve.Back => (0.18, 0.89, 0.32, 1.28),                  // OutBack
                    CubicBezierCurve.Speed => (0, 0, 0.2, 1),                           // LinearOutSlowIn
                    _ => (0, 0, 1, 1)
                },
                EasingMode.EaseInOut => curve switch
                {
                    CubicBezierCurve.Ease => (0.42, 0, 0.58, 1.0),                      // EaseInOut
                    CubicBezierCurve.Sine => (0.45, 0.05, 0.55, 0.95),                  // InOutSine
                    CubicBezierCurve.Quadratic => (0.46, 0.03, 0.52, 0.96),             // InOutQuadratic
                    CubicBezierCurve.Cubic => (0.65, 0.05, 0.36, 1),                    // InOutCubic
                    CubicBezierCurve.Back => (0.68, -0.55, 0.27, 1.55),                 // InOutBack
                    CubicBezierCurve.Speed => (0.4, 0, 0.2, 1),                         // FastOutSlowIn
                    _ => (0, 0, 1, 1)
                },
                _ => throw new ArgumentOutOfRangeException(nameof(mode))
            };
            x1 = values.Item1;
            y1 = values.Item2;
            x2 = values.Item3;
            y2 = values.Item4;
        }

        public double X1 { get => x1; set => x1 = value; }
        public double Y1 { get => y1; set => y1 = value; }
        public double X2 { get => x2; set => x2 = value; }
        public double Y2 { get => y2; set => y2 = value; }

        private double MathCbrt(double num)
        {
            return num < 0 ? -Math.Pow(-num, 1d / 3) : Math.Pow(num, 1d / 3);
        }
        private double PickAppropriateRate(double cp1, double cp2, double p, params double[] rates)
        {
            double result = double.NaN;
            double diffNow = double.MaxValue;
            foreach (var rate in rates)
                if (!double.IsNaN(rate) && Math.Abs(GetSamplePoint(cp1, cp2, rate) - p) < diffNow)
                    result = rate;
            return result;
        }
        private double GetSampleRate(double cp1, double cp2, double p)
        {
            double
                a = 3 * cp1 - 3 * cp2 + 1,
                b = -6 * cp1 + 3 * cp2,
                c = 3 * cp1,
                d = -p;
            double
                A = b * b - 3 * a * c,
                B = b * c - 9 * a * d,
                C = c * c - 3 * b * d;
            double
                delta = B * B - 4 * A * C;
            if (A == B)
            {
                double x = -c / b; // -b/3a -c/b -3d/c
                double rst = x;
                return rst;
            }
            else if (delta > 0)
            {
                //double I = double.NaN;
                double
                    y1 = A * b + 3 * a * ((-B + Math.Sqrt(delta)) / 2),
                    y2 = A * b + 3 * a * ((-B - Math.Sqrt(delta)) / 2);
                double
                    xtmp1 = MathCbrt(y1) + MathCbrt(y2); //,
                                                         //xtmp2 = Cubic(y1) - Cubic(y2);
                double   // what the fuck is I? virtual number wtf... imposible for now (((
                    x1 = (-b - xtmp1) / (3 * a); //,
                                                 //x2 = (-2 * b + xtmp1 + Math.Sqrt(3) * xtmp2 * I) / (6 * a),
                                                 //x3 = (-2 * b + xtmp1 - Math.Sqrt(3) * xtmp2 * I) / (6 * a);
                double rst = x1;
                return rst;
            }
            else if (delta == 0)
            {
                double k = B / A;
                double
                    x1 = -b / a + k,
                    x2 = -k / 2;
                double rst = PickAppropriateRate(cp1, cp2, p, x1, x2);
                return rst;
            }
            else  // delta < 0
            {
                double
                    t = (2 * A * b - 3 * a * B) / (2 * A * Math.Sqrt(A)),
                    sita = Math.Acos(t);
                double
                    x1 = (-b - 2 * Math.Sqrt(A) * Math.Cos(sita / 3)) / (3 * a),
                    x2 = (-b + Math.Sqrt(A) * (Math.Cos(sita / 3) + Math.Sqrt(3) * Math.Sin(sita / 3))) / (3 * a),
                    x3 = (-b + Math.Sqrt(A) * (Math.Cos(sita / 3) - Math.Sqrt(3) * Math.Sin(sita / 3))) / (3 * a);
                double rst = PickAppropriateRate(cp1, cp2, p, x1, x2, x3);
                return rst;
            }
        }
        private double GetSamplePoint(double cp1, double cp2, double rate)
        {
            return 3 * cp1 * rate * (1 - rate) * (1 - rate) + 3 * cp2 * rate * rate * (1 - rate) + rate * rate * rate;
            // return 3 * cp1 * rate - 3 * cp1 * 2 * rate * rate + 3 * cp1 * rate * rate * rate + 3 * cp2 * rate * rate - 3 * cp2 * rate * rate * rate + rate * rate * rate;
        }

        public CubicBezierTicker(double x1, double y1, double x2, double y2) => SetBezier(x1, y1, x2, y2);
        public CubicBezierTicker(CubicBezierCurve curve, EasingMode mode) => SetBezier(curve, mode);
        public CubicBezierTicker(CubicBezierCurve curve) : this(curve, EasingMode.Ease) { }
        public CubicBezierTicker() : this(CubicBezierCurve.Linear) { }
        public void SetBezier(double x1, double y1, double x2, double y2)
        {
            this.X1 = x1;
            this.Y1 = y1;
            this.X2 = x2;
            this.Y2 = y2;
        }
        public void SetBezier(CubicBezierCurve curve, EasingMode mode)
        {
            GetBasicCurve(curve, mode, out x1, out y1, out x2, out y2);
        }

        protected override double GetSampleRate(double x)
        {
            return GetSampleRate(X1, X2, x);
        }
        protected override double GetSampleValue(double rate)
        {
            return GetSamplePoint(Y1, Y2, rate);
        }
    }
}
