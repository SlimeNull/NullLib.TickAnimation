using System;
using System.Windows.Forms;

namespace NullLib.TickAnimation.WinForm
{
    public static class WinFormAnimationExtensions
    {
        public static void UseWinForm(this TickAnimator animator, Control control)
        {
            animator.SetPropertySetter((ac) => control.Invoke(ac));
        }
    }
}
