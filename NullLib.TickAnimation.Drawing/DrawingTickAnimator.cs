using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NullLib.TickAnimation
{
    public partial class DrawingTickAnimator : TickAnimator, IDrawingTickAnimator
    {
        public DrawingTickAnimator(ITicker ticker, object obj, PropertyInfo prop) : base(ticker, obj, prop) { }
        public DrawingTickAnimator(object obj, PropertyInfo prop) : base(obj, prop) { }
        public DrawingTickAnimator(ITicker ticker, object obj, string propName) : base(ticker, obj, propName) { }
        public DrawingTickAnimator(object obj, string propName) : base(obj, propName) { }
    }
    public partial class DrawingTickAnimator
    {

        public Task Animate(PointF start, PointF end, int dur)
        {
            double
                diffx = end.X - start.X,
                diffy = end.Y - start.Y;
            return Animate((t) => new PointF((float)(start.X + diffx * t), (float)(start.Y + diffy * t)), TimeSpan.FromMilliseconds(dur));
        }
        public Task Animate(Point start, Point end, int dur)
        {
            double
                diffx = end.X - start.X,
                diffy = end.Y - start.Y;
            return Animate((t) => new Point((int)(start.X + diffx * t), (int)(start.Y + diffy * t)), TimeSpan.FromMilliseconds(dur));
        }
        public Task Animate(PointF end, int dur) => Animate(GetPropertyValue<PointF>(), end, dur);
        public Task Animate(Point end, int dur) => Animate(GetPropertyValue<Point>(), end, dur);



        public ITickAnimator<PointF> SyncAnimate(PointF start, PointF end, int dur)
        {
            double
                diffx = end.X - start.X,
                diffy = end.Y - start.Y;
            SyncAnimate((t) => new PointF((float)(start.X + diffx * t), (float)(start.Y + diffy * t)), TimeSpan.FromMilliseconds(dur));
            return this;
        }
        public ITickAnimator<Point> SyncAnimate(Point start, Point end, int dur)
        {
            double
                diffx = end.X - start.X,
                diffy = end.Y - start.Y;
            SyncAnimate((t) => new Point((int)(start.X + diffx * t), (int)(start.Y + diffy * t)), TimeSpan.FromMilliseconds(dur));
            return this;
        }
        public ITickAnimator<PointF> SyncAnimate(PointF end, int dur) => SyncAnimate(GetPropertyValue<PointF>(), end, dur);
        public ITickAnimator<Point> SyncAnimate(Point end, int dur) => SyncAnimate(GetPropertyValue<Point>(), end, dur);

    }
    public partial class DrawingTickAnimator
    {
        public Task Animate(SizeF start, SizeF end, int dur)
        {
            double
                diffwidth = end.Width - start.Width,
                diffheight = end.Height - start.Height;
            return Animate((t) => new SizeF((float)(start.Width + diffwidth * t), (float)(start.Height + diffheight * t)), TimeSpan.FromMilliseconds(dur));
        }
        public Task Animate(Size start, Size end, int dur)
        {
            double
                diffwidth = end.Width - start.Width,
                diffheight = end.Height - start.Height;
            return Animate((t) => new Size((int)(start.Width + diffwidth * t), (int)(start.Height + diffheight * t)), TimeSpan.FromMilliseconds(dur));
        }
        public Task Animate(SizeF end, int dur) => Animate(GetPropertyValue<SizeF>(), end, dur);
        public Task Animate(Size end, int dur) => Animate(GetPropertyValue<Size>(), end, dur);

        public ITickAnimator<SizeF> SyncAnimate(SizeF start, SizeF end, int dur)
        {
            double
                diffwidth = end.Width - start.Width,
                diffheight = end.Height - start.Height;
            SyncAnimate((t) => new SizeF((float)(start.Width + diffwidth * t), (float)(start.Height + diffheight * t)), TimeSpan.FromMilliseconds(dur));
            return this;
        }
        public ITickAnimator<Size> SyncAnimate(Size start, Size end, int dur)
        {
            double
                diffwidth = end.Width - start.Width,
                diffheight = end.Height - start.Height;
            SyncAnimate((t) => new Size((int)(start.Width + diffwidth * t), (int)(start.Height + diffheight * t)), TimeSpan.FromMilliseconds(dur));
            return this;
        }
        public ITickAnimator<SizeF> SyncAnimate(SizeF end, int dur) => SyncAnimate(GetPropertyValue<SizeF>(), end, dur);
        public ITickAnimator<Size> SyncAnimate(Size end, int dur) => SyncAnimate(GetPropertyValue<Size>(), end, dur);
    }
    public partial class DrawingTickAnimator
    {
        public Task Animate(RectangleF start, RectangleF end, int dur)
        {
            double
                diffx = end.X - start.X,
                diffy = end.Y - start.Y,
                diffwidth = end.Width - start.Width,
                diffheight = end.Height - start.Height;
            return Animate((t) => new RectangleF((float)(start.X + diffx * t),
                                                 (float)(start.Y + diffy * t),
                                                 (float)(start.Width + diffwidth * t),
                                                 (float)(start.Height + diffheight * t)), TimeSpan.FromMilliseconds(dur));
        }
        public Task Animate(Rectangle start, Rectangle end, int dur)
        {
            double
                diffx = end.X - start.X,
                diffy = end.Y - start.Y,
                diffwidth = end.Width - start.Width,
                diffheight = end.Height - start.Height;
            return Animate((t) => new Rectangle((int)(start.X + diffx * t),
                                                (int)(start.Y + diffy * t),
                                                (int)(start.Width + diffwidth * t),
                                                (int)(start.Height + diffheight * t)), TimeSpan.FromMilliseconds(dur));
        }
        public Task Animate(RectangleF end, int dur) => Animate(GetPropertyValue<RectangleF>(), end, dur);
        public Task Animate(Rectangle end, int dur) => Animate(GetPropertyValue<Rectangle>(), end, dur);

        public ITickAnimator<RectangleF> SyncAnimate(RectangleF start, RectangleF end, int dur)
        {
            double
                diffx = end.X - start.X,
                diffy = end.Y - start.Y,
                diffwidth = end.Width - start.Width,
                diffheight = end.Height - start.Height;
            SyncAnimate((t) => new RectangleF((float)(start.X + diffx * t),
                                              (float)(start.Y + diffy * t),
                                              (float)(start.Width + diffwidth * t),
                                              (float)(start.Height + diffheight * t)), TimeSpan.FromMilliseconds(dur));
            return this;
        }
        public ITickAnimator<Rectangle> SyncAnimate(Rectangle start, Rectangle end, int dur)
        {
            double
                diffx = end.X - start.X,
                diffy = end.Y - start.Y,
                diffwidth = end.Width - start.Width,
                diffheight = end.Height - start.Height;
            SyncAnimate((t) => new Rectangle((int)(start.X + diffx * t),
                                             (int)(start.Y + diffy * t),
                                             (int)(start.Width + diffwidth * t),
                                             (int)(start.Height + diffheight * t)), TimeSpan.FromMilliseconds(dur));
            return this;
        }
        public ITickAnimator<RectangleF> SyncAnimate(RectangleF end, int dur) => SyncAnimate(GetPropertyValue<RectangleF>(), end, dur);
        public ITickAnimator<Rectangle> SyncAnimate(Rectangle end, int dur) => SyncAnimate(GetPropertyValue<Rectangle>(), end, dur);
    }
    public partial class DrawingTickAnimator
    {
        #region ColorLerp Definition
#if NET47
        static double Clamp(double value, double min, double max)
        {
            if (value < min)
                return min;
            if (max < value)
                return max;
            return value;
        }
        static Color ColorLerp(Color a, Color b, double t)
        {
            double
                diffr = b.R - a.R,
                diffg = b.G - a.G,
                diffb = b.B - a.B,
                diffa = b.A - a.A;

            return Color.FromArgb((int)Clamp(a.A + diffa * t, 0, 255),
                                  (int)Clamp(a.R + diffr * t, 0, 255),
                                  (int)Clamp(a.G + diffg * t, 0, 255),
                                  (int)Clamp(a.B + diffb * t, 0, 255));
        }
#endif
#if NETSTANDARD2_0
        static double Clamp(double value, double min, double max)
        {
            if (value < min)
                return min;
            if (max < value)
                return max;
            return value;
        }
        static Color ColorLerp(Color a, Color b, double t)
        {
            double
                diffr = b.R - a.R,
                diffg = b.G - a.G,
                diffb = b.B - a.B,
                diffa = b.A - a.A;

            return Color.FromArgb((int)Clamp(a.A + diffa * t, 0, 255),
                                  (int)Clamp(a.R + diffr * t, 0, 255),
                                  (int)Clamp(a.G + diffg * t, 0, 255),
                                  (int)Clamp(a.B + diffb * t, 0, 255));
        }
#endif
#if NET5_0
        static Color ColorLerp(Color a, Color b, double t)
        {
            double
                diffr = b.R - a.R,
                diffg = b.G - a.G,
                diffb = b.B - a.B,
                diffa = b.A - a.A;

            return Color.FromArgb((int)Math.Clamp(a.A + diffa * t, 0, 255),
                                  (int)Math.Clamp(a.R + diffr * t, 0, 255),
                                  (int)Math.Clamp(a.G + diffg * t, 0, 255),
                                  (int)Math.Clamp(a.B + diffb * t, 0, 255));
        }
#endif
#if NET6_0
        static Color ColorLerp(Color a, Color b, double t)
        {
            double
                diffr = b.R - a.R,
                diffg = b.G - a.G,
                diffb = b.B - a.B,
                diffa = b.A - a.A;

            return Color.FromArgb((int)Math.Clamp(a.A + diffa * t, 0, 255),
                                  (int)Math.Clamp(a.R + diffr * t, 0, 255),
                                  (int)Math.Clamp(a.G + diffg * t, 0, 255),
                                  (int)Math.Clamp(a.B + diffb * t, 0, 255));
        }
#endif
        #endregion

        public Task Animate(Color start, Color end, int dur)
        {
            return Animate((t) => ColorLerp(start, end, t), TimeSpan.FromMilliseconds(dur));
        }
        public Task Animate(Color end, int dur) => Animate(GetPropertyValue<Color>(), end, dur);

        public ITickAnimator<Color> SyncAnimate(Color start, Color end, int dur)
        {
            SyncAnimate((t) => ColorLerp(start, end, t), TimeSpan.FromMilliseconds(dur));
            return this;
        }
        public ITickAnimator<Color> SyncAnimate(Color end, int dur) => SyncAnimate(GetPropertyValue<Color>(), end, dur);
    }
}
