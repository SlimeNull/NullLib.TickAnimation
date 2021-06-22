using System;
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
        public Task FrameAnimate(PointF start, PointF end, int dur)
        {
            double
                diffx = end.X - start.X,
                diffy = end.Y - start.Y;
            return FrameAnimate((t) => new PointF((float)(start.X + diffx * t), (float)(start.Y + diffy * t)), TimeSpan.FromMilliseconds(dur));
        }
        public Task FrameAnimate(Point start, Point end, int dur)
        {
            double
                diffx = end.X - start.X,
                diffy = end.Y - start.Y;
            return FrameAnimate((t) => new Point((int)(start.X + diffx * t), (int)(start.Y + diffy * t)), TimeSpan.FromMilliseconds(dur));
        }

        public Task SmoothAnimate(PointF start, PointF end, int dur)
        {
            double
                diffx = end.X - start.X,
                diffy = end.Y - start.Y;
            return SmoothAnimate((t) => new PointF((float)(start.X + diffx * t), (float)(start.Y + diffy * t)), TimeSpan.FromMilliseconds(dur));
        }
        public Task SmoothAnimate(Point start, Point end, int dur)
        {
            double
                diffx = end.X - start.X,
                diffy = end.Y - start.Y;
            return SmoothAnimate((t) => new Point((int)(start.X + diffx * t), (int)(start.Y + diffy * t)), TimeSpan.FromMilliseconds(dur));
        }

        public Task Animate(PointF start, PointF end, int dur) =>
            SmoothAnimate(start, end, dur);
        public Task Animate(Point start, Point end, int dur) =>
            SmoothAnimate(start, end, dur);

        public void SyncFrameAnimate(PointF start, PointF end, int dur)
        {
            double
                diffx = end.X - start.X,
                diffy = end.Y - start.Y;
            SyncFrameAnimate((t) => new PointF((float)(start.X + diffx * t), (float)(start.Y + diffy * t)), TimeSpan.FromMilliseconds(dur));
        }
        public void SyncFrameAnimate(Point start, Point end, int dur)
        {
            double
                diffx = end.X - start.X,
                diffy = end.Y - start.Y;
            SyncFrameAnimate((t) => new Point((int)(start.X + diffx * t), (int)(start.Y + diffy * t)), TimeSpan.FromMilliseconds(dur));
        }

        public void SyncSmoothAnimate(PointF start, PointF end, int dur)
        {
            double
                diffx = end.X - start.X,
                diffy = end.Y - start.Y;
            SyncSmoothAnimate((t) => new PointF((float)(start.X + diffx * t), (float)(start.Y + diffy * t)), TimeSpan.FromMilliseconds(dur));
        }
        public void SyncSmoothAnimate(Point start, Point end, int dur)
        {
            double
                diffx = end.X - start.X,
                diffy = end.Y - start.Y;
            SyncSmoothAnimate((t) => new Point((int)(start.X + diffx * t), (int)(start.Y + diffy * t)), TimeSpan.FromMilliseconds(dur));
        }

        public void SyncAnimate(PointF start, PointF end, int dur) =>
            SyncSmoothAnimate(start, end, dur);
        public void SyncAnimate(Point start, Point end, int dur) =>
            SyncSmoothAnimate(start, end, dur);
    }
    public partial class DrawingTickAnimator
    {
        public Task FrameAnimate(SizeF start, SizeF end, int dur)
        {
            double
                diffwidth = end.Width - start.Width,
                diffheight = end.Height - start.Height;
            return FrameAnimate((t) => new SizeF((float)(start.Width + diffwidth * t), (float)(start.Height + diffheight * t)), TimeSpan.FromMilliseconds(dur));
        }
        public Task FrameAnimate(Size start, Size end, int dur)
        {
            double
                diffwidth = end.Width - start.Width,
                diffheight = end.Height - start.Height;
            return FrameAnimate((t) => new Size((int)(start.Width + diffwidth * t), (int)(start.Height + diffheight * t)), TimeSpan.FromMilliseconds(dur));
        }
        public Task SmoothAnimate(SizeF start, SizeF end, int dur)
        {
            double
                diffwidth = end.Width - start.Width,
                diffheight = end.Height - start.Height;
            return SmoothAnimate((t) => new SizeF((float)(start.Width + diffwidth * t), (float)(start.Height + diffheight * t)), TimeSpan.FromMilliseconds(dur));
        }
        public Task SmoothAnimate(Size start, Size end, int dur)
        {
            double
                diffwidth = end.Width - start.Width,
                diffheight = end.Height - start.Height;
            return SmoothAnimate((t) => new Size((int)(start.Width + diffwidth * t), (int)(start.Height + diffheight * t)), TimeSpan.FromMilliseconds(dur));
        }
        public Task Animate(SizeF start, SizeF end, int dur) =>
            SmoothAnimate(start, end, dur);
        public Task Animate(Size start, Size end, int dur) =>
            SmoothAnimate(start, end, dur);

        public void SyncFrameAnimate(SizeF start, SizeF end, int dur)
        {
            double
                diffwidth = end.Width - start.Width,
                diffheight = end.Height - start.Height;
            SyncFrameAnimate((t) => new SizeF((float)(start.Width + diffwidth * t), (float)(start.Height + diffheight * t)), TimeSpan.FromMilliseconds(dur));
        }
        public void SyncFrameAnimate(Size start, Size end, int dur)
        {
            double
                diffwidth = end.Width - start.Width,
                diffheight = end.Height - start.Height;
            SyncFrameAnimate((t) => new Size((int)(start.Width + diffwidth * t), (int)(start.Height + diffheight * t)), TimeSpan.FromMilliseconds(dur));
        }
        public void SyncSmoothAnimate(SizeF start, SizeF end, int dur)
        {
            double
                diffwidth = end.Width - start.Width,
                diffheight = end.Height - start.Height;
            SyncSmoothAnimate((t) => new SizeF((float)(start.Width + diffwidth * t), (float)(start.Height + diffheight * t)), TimeSpan.FromMilliseconds(dur));
        }
        public void SyncSmoothAnimate(Size start, Size end, int dur)
        {
            double
                diffwidth = end.Width - start.Width,
                diffheight = end.Height - start.Height;
            SyncSmoothAnimate((t) => new Size((int)(start.Width + diffwidth * t), (int)(start.Height + diffheight * t)), TimeSpan.FromMilliseconds(dur));
        }
        public void SyncAnimate(SizeF start, SizeF end, int dur) =>
            SyncSmoothAnimate(start, end, dur);
        public void SyncAnimate(Size start, Size end, int dur) =>
            SyncSmoothAnimate(start, end, dur);
    }
    public partial class DrawingTickAnimator
    {
        public Task FrameAnimate(RectangleF start, RectangleF end, int dur)
        {
            double
                diffx = end.X - start.X,
                diffy = end.Y - start.Y,
                diffwidth = end.Width - start.Height,
                diffheight = end.Height - start.Height;
            return FrameAnimate((t) => new RectangleF((float)(start.X + diffx * t),
                                                      (float)(start.Y + diffy * t),
                                                      (float)(start.Width + diffwidth * t),
                                                      (float)(start.Height + diffheight * t)), TimeSpan.FromMilliseconds(dur));
        }
        public Task FrameAnimate(Rectangle start, Rectangle end, int dur)
        {
            double
                diffx = end.X - start.X,
                diffy = end.Y - start.Y,
                diffwidth = end.Width - start.Height,
                diffheight = end.Height - start.Height;
            return FrameAnimate((t) => new RectangleF((float)(start.X + diffx * t),
                                                      (float)(start.Y + diffy * t),
                                                      (float)(start.Width + diffwidth * t),
                                                      (float)(start.Height + diffheight * t)), TimeSpan.FromMilliseconds(dur));
        }
        public Task SmoothAnimate(RectangleF start, RectangleF end, int dur)
        {
            double
                diffx = end.X - start.X,
                diffy = end.Y - start.Y,
                diffwidth = end.Width - start.Height,
                diffheight = end.Height - start.Height;
            return SmoothAnimate((t) => new RectangleF((float)(start.X + diffx * t),
                                                      (float)(start.Y + diffy * t),
                                                      (float)(start.Width + diffwidth * t),
                                                      (float)(start.Height + diffheight * t)), TimeSpan.FromMilliseconds(dur));
        }
        public Task SmoothAnimate(Rectangle start, Rectangle end, int dur)
        {
            double
                diffx = end.X - start.X,
                diffy = end.Y - start.Y,
                diffwidth = end.Width - start.Height,
                diffheight = end.Height - start.Height;
            return SmoothAnimate((t) => new RectangleF((float)(start.X + diffx * t),
                                                      (float)(start.Y + diffy * t),
                                                      (float)(start.Width + diffwidth * t),
                                                      (float)(start.Height + diffheight * t)), TimeSpan.FromMilliseconds(dur));
        }
        public Task Animate(RectangleF start, RectangleF end, int dur) =>
            SmoothAnimate(start, end, dur);
        public Task Animate(Rectangle start, Rectangle end, int dur) =>
            SmoothAnimate(start, end, dur);

        public void SyncFrameAnimate(RectangleF start, RectangleF end, int dur)
        {
            double
                diffx = end.X - start.X,
                diffy = end.Y - start.Y,
                diffwidth = end.Width - start.Height,
                diffheight = end.Height - start.Height;
            SyncFrameAnimate((t) => new RectangleF((float)(start.X + diffx * t),
                                                      (float)(start.Y + diffy * t),
                                                      (float)(start.Width + diffwidth * t),
                                                      (float)(start.Height + diffheight * t)), TimeSpan.FromMilliseconds(dur));
        }
        public void SyncFrameAnimate(Rectangle start, Rectangle end, int dur)
        {
            double
                diffx = end.X - start.X,
                diffy = end.Y - start.Y,
                diffwidth = end.Width - start.Height,
                diffheight = end.Height - start.Height;
            SyncFrameAnimate((t) => new RectangleF((float)(start.X + diffx * t),
                                                      (float)(start.Y + diffy * t),
                                                      (float)(start.Width + diffwidth * t),
                                                      (float)(start.Height + diffheight * t)), TimeSpan.FromMilliseconds(dur));
        }
        public void SyncSmoothAnimate(RectangleF start, RectangleF end, int dur)
        {
            double
                diffx = end.X - start.X,
                diffy = end.Y - start.Y,
                diffwidth = end.Width - start.Height,
                diffheight = end.Height - start.Height;
            SyncSmoothAnimate((t) => new RectangleF((float)(start.X + diffx * t),
                                                      (float)(start.Y + diffy * t),
                                                      (float)(start.Width + diffwidth * t),
                                                      (float)(start.Height + diffheight * t)), TimeSpan.FromMilliseconds(dur));
        }
        public void SyncSmoothAnimate(Rectangle start, Rectangle end, int dur)
        {
            double
                diffx = end.X - start.X,
                diffy = end.Y - start.Y,
                diffwidth = end.Width - start.Height,
                diffheight = end.Height - start.Height;
            SyncSmoothAnimate((t) => new RectangleF((float)(start.X + diffx * t),
                                                      (float)(start.Y + diffy * t),
                                                      (float)(start.Width + diffwidth * t),
                                                      (float)(start.Height + diffheight * t)), TimeSpan.FromMilliseconds(dur));
        }
        public void SyncAnimate(RectangleF start, RectangleF end, int dur) =>
            SyncSmoothAnimate(start, end, dur);
        public void SyncAnimate(Rectangle start, Rectangle end, int dur) =>
            SyncSmoothAnimate(start, end, dur);
    }
    public partial class DrawingTickAnimator
    {
        public Task FrameAnimate(Color start, Color end, int dur)
        {
            double
                diffr = end.R - start.R,
                diffg = end.G - start.G,
                diffb = end.B - start.B,
                diffa = end.A - start.A;
            return FrameAnimate((t) => Color.FromArgb((int)(start.A + diffa * t),       // Fuck you world
                                                      (int)(start.R + diffr * t),
                                                      (int)(start.G + diffg * t),
                                                      (int)(start.B + diffb * t)), TimeSpan.FromMilliseconds(dur));
        }
        public Task SmoothAnimate(Color start, Color end, int dur)
        {
            double
                diffr = end.R - start.R,
                diffg = end.G - start.G,
                diffb = end.B - start.B,
                diffa = end.A - start.A;
            return SmoothAnimate((t) => Color.FromArgb((int)(start.A + diffa * t),       // Fuck you world
                                                       (int)(start.R + diffr * t),
                                                       (int)(start.G + diffg * t),
                                                       (int)(start.B + diffb * t)), TimeSpan.FromMilliseconds(dur));
        }
        public Task Animate(Color start, Color end, int dur) =>
            SmoothAnimate(start, end, dur);

        public void SyncFrameAnimate(Color start, Color end, int dur)
        {
            double
                diffr = end.R - start.R,
                diffg = end.G - start.G,
                diffb = end.B - start.B,
                diffa = end.A - start.A;
            SyncFrameAnimate((t) => Color.FromArgb((int)(start.A + diffa * t),       // Fuck you world
                                                   (int)(start.R + diffr * t),
                                                   (int)(start.G + diffg * t),
                                                   (int)(start.B + diffb * t)), TimeSpan.FromMilliseconds(dur));
        }
        public void SyncSmoothAnimate(Color start, Color end, int dur)
        {
            double
                diffr = end.R - start.R,
                diffg = end.G - start.G,
                diffb = end.B - start.B,
                diffa = end.A - start.A;
            SyncSmoothAnimate((t) => Color.FromArgb((int)(start.A + diffa * t),       // Fuck you world
                                                    (int)(start.R + diffr * t),
                                                    (int)(start.G + diffg * t),
                                                    (int)(start.B + diffb * t)), TimeSpan.FromMilliseconds(dur));
        }
        public void SyncAnimate(Color start, Color end, int dur) =>
            SyncSmoothAnimate(start, end, dur);
    }
}
