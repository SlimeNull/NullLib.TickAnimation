using System;
using System.Drawing;
using System.Windows.Forms;

namespace NullLib.TickAnimation.WinForm
{
    public abstract class DoubleBufferedControlBase : Control
    {
        readonly BufferedGraphicsContext context;
        private BufferedGraphics bufferedGraphics;
        protected BufferedGraphics BufferedGraphics { get => bufferedGraphics; }

        public DoubleBufferedControlBase() : base()
        {
            context = BufferedGraphicsManager.Current;
            bufferedGraphics = context.Allocate(Graphics.FromHwnd(Handle), new Rectangle(Point.Empty, Size));
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            if (bufferedGraphics != null)
                bufferedGraphics.Dispose();
            bufferedGraphics = context.Allocate(Graphics.FromHwnd(Handle), new Rectangle(Point.Empty, Size));
            base.OnSizeChanged(e);
        }
    }
}
