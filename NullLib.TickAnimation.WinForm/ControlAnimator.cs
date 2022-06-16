using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NullLib.TickAnimation;

namespace NullLib.TickAnimation.WinForm
{
    public class ControlAnimator : DrawingTickAnimator
    {
        public ControlAnimator(Control obj, PropertyInfo prop) : base(obj, prop)
        {
            this.UseWinForm(obj);
        }

        public ControlAnimator(Control obj, string propName) : base(obj, propName)
        {
            this.UseWinForm(obj);
        }

        public ControlAnimator(ITicker ticker, Control obj, PropertyInfo prop) : base(ticker, obj, prop)
        {
            this.UseWinForm(obj);
        }

        public ControlAnimator(ITicker ticker, Control obj, string propName) : base(ticker, obj, propName)
        {
            this.UseWinForm(obj);
        }
    }
}
