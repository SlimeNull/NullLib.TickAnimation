using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NullLib.TickAnimation.WinForm
{
    public class TickerMotionView : DoubleBufferedControlBase
    {
        BackgroundWorker backgroundWorker = new BackgroundWorker();

        public ITicker? Ticker { get; set; }
        public int MotionDuration { get; set; } = 2000;
        public int MotionDrawDuration { get; set; } = 100;

        public int BallSize { get; set; } = 30;
        public Color BallColor { get; set; } = Color.FromArgb(156, 39, 176);

        void ViewMotionAction()
        {
            if (Ticker == null)
                return;
            Graphics g = BufferedGraphics.Graphics;
            int rectUp = (Height - BallSize) / 2;


            int drawTimes = 0;
            double motionDrawDurationValue = MotionDuration / (double)MotionDrawDuration;
            TickAnimationProc.SyncAnimate(Ticker, TimeSpan.FromMilliseconds(MotionDuration), v =>
            {
                if (v > motionDrawDurationValue * drawTimes)
                {
                    g.Clear(base.BackColor);
                        SolidBrush stepBallBrush = new SolidBrush(Color.FromArgb(26, BallColor));
                    for (int i = 0; i * motionDrawDurationValue <= v; i++)
                        g.FillEllipse(stepBallBrush, new Rectangle((int)((Width - BallSize) * (i * motionDrawDurationValue)), rectUp, BallSize, BallSize));
                    g.FillEllipse(stepBallBrush, new Rectangle((int)((Width - BallSize) * v), rectUp, BallSize, BallSize));
                    BufferedGraphics.Render();

                    drawTimes++;
                }
            });
        }

        public void ViewMotion()
        {
            Task.Run(ViewMotionAction);
        }
    }
}
