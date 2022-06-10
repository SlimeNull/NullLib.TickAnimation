using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NullLib.TickAnimation.WinForm
{
    public class QuadraticBezierView : BezierViewBase
    {
        public PointF ControlPoint { get; set; } = new PointF(0.25f, 0.75f);

        protected override void PaintCore(Graphics g)
        {
            g.Clear(BackColor);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            Point startPoint = new Point(0, Height - 1);
            Point endPoint = new Point(Width - 1, 0);
            Point cp = GetPixelPointFromControlPoint(ControlPoint);

            if (ControlHandleEnabled)
            {
                g.DrawLine(ControlHandleStickPen, cp, endPoint);

                g.DrawBezier(CurvePen, startPoint, startPoint, cp, endPoint);

                Rectangle handleRect = GetRectangleFromCenterPoint(cp, ControlHandleSize / 2);
                g.FillEllipse(ControlHandleBrush, handleRect);
            }
            else
            {
                g.DrawBezier(CurvePen, startPoint, startPoint, cp, endPoint);
            }
        }

        bool cpCaptured;
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (ControlHandleEnabled)
            {
                Point cp = GetPixelPointFromControlPoint(ControlPoint);
                Rectangle handleRect = GetRectangleFromCenterPoint(cp, ControlHandleSize);
                if (handleRect.Contains(e.Location))
                {
                    cpCaptured = true;
                }
            }

            base.OnMouseDown(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (ControlHandleEnabled)
            {
                if (ClientRectangle.Contains(e.Location))
                {
                    if (cpCaptured)
                    {
                        ControlPoint = GetControlPointFromPixelPoint(e.Location);
                        Invalidate();
                    }
                }
            }

            base.OnMouseMove(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            cpCaptured = false;

            base.OnMouseUp(e);
        }
    }
}
