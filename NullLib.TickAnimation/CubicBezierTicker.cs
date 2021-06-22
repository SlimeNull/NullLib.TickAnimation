using System;
using System.Linq;

namespace NullLib.TickAnimation
{
    public class CubicBezierTicker : BezierTickerBase
    {
        double x1, y1, x2, y2;

        public double X1
        {
            get => x1;
            set
            {
                x1 = value;
                ResetSequence();
            }
        }
        public double Y1
        {
            get => y1;
            set
            {
                y1 = value;
                ResetSequence();
            }
        }
        public double X2
        {
            get => x2;
            set
            {
                x2 = value;
                ResetSequence();
            }
        }
        public double Y2
        {
            get => y2;
            set
            {
                y2 = value;
                ResetSequence();
            }
        }

        public int TickCount
        {
            get => count;
            set
            {
                count = value;
                ResetSequence();
            }
        }

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

        public CubicBezierTicker(double x1, double y1, double x2, double y2, int count) => SetOption(x1, y1, x2, y2, count);
        public CubicBezierTicker(double x1, double y1, double x2, double y2) : this(x1, y1, x2, y2, 15) { }
        public CubicBezierTicker(CubicCurves curve, int count)
        {
            switch (curve)
            {
                case CubicCurves.Ease: SetOption(0.25, 0.1, 0.25, 1, count); break;
                case CubicCurves.Linear: SetOption(0, 0, 1, 1, count); break;
                case CubicCurves.EaseIn: SetOption(0.42, 0, 1, 1, count); break;
                case CubicCurves.EaseOut: SetOption(0, 0, 0.58, 1.0, count); break;
                case CubicCurves.EaseInOut: SetOption(0.42, 0, 0.58, 1.0, count); break;
                case CubicCurves.InOutSine: SetOption(0.45, 0.05, 0.55, 0.95, count); break;
                case CubicCurves.InOutQuadratic: SetOption(0.46, 0.03, 0.52, 0.96, count); break;
                case CubicCurves.InOutCubic: SetOption(0.65, 0.05, 0.36, 1, count); break;
                case CubicCurves.FastOutSlowIn: SetOption(0.4, 0, 0.2, 1, count); break;
                case CubicCurves.InOutBack: SetOption(0.68, -0.55, 0.27, 1.55, count); break;
                case CubicCurves.InSine: SetOption(0.47, 0, 0.75, 0.72, count); break;
                case CubicCurves.InQuadratic: SetOption(0.55, 0.09, 0.68, 0.53, count); break;
                case CubicCurves.InCubic: SetOption(0.55, 0.06, 0.68, 0.19, count); break;
                case CubicCurves.FastOutLinearIn: SetOption(0.4, 0, 1, 1, count); break;
                case CubicCurves.InBack: SetOption(0.6, -0.28, 0.74, 0.05, count); break;
                case CubicCurves.OutSine: SetOption(0.39, 0.58, 0.57, 1, count); break;
                case CubicCurves.OutQuadratic: SetOption(0.25, 0.46, 0.45, 0.94, count); break;
                case CubicCurves.OutCubic: SetOption(0.22, 0.61, 0.36, 1, count); break;
                case CubicCurves.LinearOutSlowIn: SetOption(0, 0, 0.2, 1, count); break;
                case CubicCurves.OutBack: SetOption(0.18, 0.89, 0.32, 1.28, count); break;
                default: throw new ArgumentOutOfRangeException(nameof(curve));
            }
        }
        public CubicBezierTicker(CubicCurves curve) : this(curve, 15) { }
        public CubicBezierTicker() : this(CubicCurves.Linear) { }
        public void SetBezier(double x1, double y1, double x2, double y2)
        {
            this.x1 = x1;
            this.y1 = y1;
            this.x2 = x2;
            this.y2 = y2;
            ResetSequence();
        }
        public void SetOption(double x1, double y1, double x2, double y2, int count)
        {
            this.x1 = x1;
            this.y1 = y1;
            this.x2 = x2;
            this.y2 = y2;
            this.count = count;
            ResetSequence();
        }

        protected override double GetSampleRate(double x)
        {
            return GetSampleRate(x1, x2, x);
        }
        protected override double GetSampleValue(double rate)
        {
            return GetSamplePoint(y1, y2, rate);
        }
    }
    public enum CubicCurves
    {
        /// <summary>
        /// X1:0.25, Y1:0.1; X2:0.25, Y2:1.0
        /// </summary>
        Ease,              // 0.25, 0.1, 0.25, 1
        /// <summary>
        /// X1:0, Y1:0; X2:1, Y2:1
        /// </summary>
        Linear,            // 0, 0, 1, 1
        /// <summary>
        /// X1:0.42, Y1:0; X2:1, Y1:1
        /// </summary>
        EaseIn,            // 0.42, 0, 1, 1
        /// <summary>
        /// X1:0, Y1:0; X2:0.58, Y1:1.0
        /// </summary>
        EaseOut,           // 0, 0, 0.58, 1.0
        /// <summary>
        /// X1:0.42, Y1:0; X2:0.58, Y1:1.0
        /// </summary>
        EaseInOut,         // 0.42, 0, 0.58, 1.0

        /// <summary>
        /// X1:0.45, Y1:0.05; X2:0.55, Y1:0.95
        /// </summary>
        InOutSine,         // 0.45, 0.05, 0.55, 0.95
        /// <summary>
        /// X1:0.46, Y1:0.03; X2:0.52, Y1:0.96
        /// </summary>
        InOutQuadratic,    // 0.46, 0.03, 0.52, 0.96
        /// <summary>
        /// X1:0.65, Y1:0.05; X2:0.36, Y1:1
        /// </summary>
        InOutCubic,        // 0.65, 0.05, 0.36, 1
        /// <summary>
        /// X1:0.4, Y1:0; X2:0.2, Y1:1
        /// </summary>
        FastOutSlowIn,     // 0.4, 0, 0.2, 1
        /// <summary>
        /// X1:0.68, Y1:-0.55; X2:0.27, Y1:1.55
        /// </summary>
        InOutBack,         // 0.68, -0.55, 0.27, 1.55

        /// <summary>
        /// X1:0.47, Y1:0; X2:0.75, Y1:0.72
        /// </summary>
        InSine,            // 0.47, 0, 0.75, 0.72
        /// <summary>
        /// X1:0.55, Y1:0.09; X2:0.68, Y1:0.53
        /// </summary>
        InQuadratic,       // 0.55, 0.09, 0.68, 0.53
        /// <summary>
        /// X1:0.55, Y1:0.06; X2:0.68, Y1:0.19
        /// </summary>
        InCubic,           // 0.55, 0.06, 0.68, 0.19
        /// <summary>
        /// X1:0.4, Y1:0; X2:1, Y1:1
        /// </summary>
        FastOutLinearIn,   // 0.4, 0, 1, 1
        /// <summary>
        /// X1:0.6, Y1:-0.28; X2:0.74, Y1:0.05
        /// </summary>
        InBack,            // 0.6, -0.28, 0.74, 0.05

        /// <summary>
        /// X1:0.39, Y1:0.58; X2:0.57, Y1:1
        /// </summary>
        OutSine,           // 0.39, 0.58, 0.57, 1
        /// <summary>
        /// X1:0.25, Y1:0.46; X2:0.45, Y1:0.94
        /// </summary>
        OutQuadratic,      // 0.25, 0.46, 0.45, 0.94
        /// <summary>
        /// X1:0.22, Y1:0.61; X2:0.36, Y1:1
        /// </summary>
        OutCubic,          // 0.22, 0.61, 0.36, 1
        /// <summary>
        /// X1:0, Y1:0; X2:0.2, Y1:1
        /// </summary>
        LinearOutSlowIn,   // 0, 0, 0.2, 1
        /// <summary>
        /// X1:0.18, Y1:0.89; X2:0.32, Y1:1.28
        /// </summary>
        OutBack,           // 0.18, 0.89, 0.32, 1.28
    }
}
