using System;
using System.Drawing;
using System.Windows.Forms;

namespace NullLib.TickAnimation.WinForm
{
    public class CubicBezierView : BezierViewBase
    {
        public PointF ControlPoint1 { get; set; } = new PointF(0.25f, 0.75f);
        public PointF ControlPoint2 { get; set; } = new PointF(0.75f, 0.25f);

        protected override void PaintCore(Graphics g)
        {
            g.Clear(BackColor);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            Point startPoint = new Point(0, Height - 1);
            Point endPoint = new Point(Width - 1, 0);
            Point cp1 = GetPixelPointFromControlPoint(ControlPoint1);
            Point cp2 = GetPixelPointFromControlPoint(ControlPoint2);

            if (ControlHandleEnabled)
            {
                g.DrawLine(ControlHandleStickPen, startPoint, cp1);
                g.DrawLine(ControlHandleStickPen, endPoint, cp2);

                g.DrawBezier(CurvePen, startPoint, cp1, cp2, endPoint);

                Rectangle
                    handleRect1 = GetRectangleFromCenterPoint(cp1, ControlHandleSize / 2),
                    handleRect2 = GetRectangleFromCenterPoint(cp2, ControlHandleSize / 2);
                g.FillEllipse(ControlHandleBrush, handleRect1);
                g.FillEllipse(ControlHandleBrush, handleRect2);
            }
            else
            {
                g.DrawBezier(CurvePen, startPoint, cp1, cp2, endPoint);
            }
        }

        bool cp1Captured, cp2Captured;
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (ControlHandleEnabled)
            {
                Point cp1 = GetPixelPointFromControlPoint(ControlPoint1);
                Point cp2 = GetPixelPointFromControlPoint(ControlPoint2);
                Rectangle
                    handleRect1 = GetRectangleFromCenterPoint(cp1, ControlHandleSize / 2),
                    handleRect2 = GetRectangleFromCenterPoint(cp2, ControlHandleSize / 2);
                if (handleRect1.Contains(e.Location))
                {
                    cp1Captured = true;
                }
                else if (handleRect2.Contains(e.Location))
                {
                    cp2Captured = true;
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
                    if (cp1Captured)
                    {
                        ControlPoint1 = GetControlPointFromPixelPoint(e.Location);
                        Invalidate();
                    }
                    else if (cp2Captured)
                    {
                        ControlPoint2 = GetControlPointFromPixelPoint(e.Location);
                        Invalidate();
                    }
                }
            }

            base.OnMouseMove(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            cp1Captured = cp2Captured = false;

            base.OnMouseUp(e);
        }
    }
}
