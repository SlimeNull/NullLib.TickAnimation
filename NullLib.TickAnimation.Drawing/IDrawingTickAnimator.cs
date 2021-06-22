using System.Drawing;

namespace NullLib.TickAnimation
{
    public interface IDrawingTickAnimator :
        ITickAnimator<PointF>,
        ITickAnimator<Point>,
        ITickAnimator<SizeF>,
        ITickAnimator<Size>,
        ITickAnimator<RectangleF>,
        ITickAnimator<Rectangle>,
        ITickAnimator<Color>
    {
        // just for fun~~~
    }
}
