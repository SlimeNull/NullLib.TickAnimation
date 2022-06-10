using NullLib.TickAnimation.WinForm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestForm.View
{
    internal class TestControl : DoubleBufferedControlBase
    {
        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            base.OnPaintBackground(new PaintEventArgs(BufferedGraphics.Graphics, pevent.ClipRectangle));
            BufferedGraphics.Render();
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            
        }
    }
}
