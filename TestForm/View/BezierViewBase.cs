using System;
using System.Drawing;
using System.Windows.Forms;

namespace NullLib.TickAnimation.WinForm
{
    public abstract class BezierViewBase : DoubleBufferedControlBase
    {
        public bool ControlHandleEnabled
        {
            get => controlHandleEnabled;
            set
            {
                if (controlHandleEnabled == value)
                    return;
                controlHandleEnabled = value;
                ((EventHandler)Events[EventControlHandleEnabled])?.Invoke(this, EventArgs.Empty);
                Invalidate();
            }
        }
        public int ControlHandleSize { get; set; } = 20;
        public Brush ControlHandleBrush { get; set; } = new SolidBrush(Color.FromArgb(196, 125, 208));

        public Pen ControlHandleStickPen { get; set; } = new Pen(Color.FromArgb(196, 125, 208), 3);
        public Pen CurvePen { get; set; } = new Pen(Color.Black, 5);

        private static readonly object
            EventControlHandleEnabled = new object();
        private bool controlHandleEnabled;

        public event EventHandler EventControlHandleEnabledChanged
        {
            add => Events.AddHandler(EventControlHandleEnabled, value);
            remove => Events.RemoveHandler(EventControlHandleEnabled, value);
        }

        protected abstract void PaintCore(Graphics g);

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            Invalidate();
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            Graphics g = BufferedGraphics.Graphics;
            base.OnPaintBackground(new PaintEventArgs(g, pevent.ClipRectangle));
            PaintCore(g);

            BufferedGraphics.Render();
        }

        protected PointF GetControlPointFromPixelPoint(Point p)
        {
            return new PointF(p.X / (float)Width, 1 - p.Y / (float)Height);
        }

        protected Point GetPixelPointFromControlPoint(PointF p)
        {
            return new Point((int)(p.X * Width), Height - (int)(p.Y * Height));
        }

        protected Rectangle GetRectangleFromCenterPoint(Point p, int r)
        {
            return new Rectangle(p.X - r, p.Y - r, r * 2 + 1, r * 2 + 1);
        }
    }
}