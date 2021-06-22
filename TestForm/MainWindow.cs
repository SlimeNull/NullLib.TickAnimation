using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using NullLib.TickAnimation;
using TestForm.ViewModule;

namespace TestForm
{
    public partial class MainWindow : Form
    {
        public MainWindowModule ViewModule { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            InitBinding();

            navAnimator = new TickAnimator(ViewModule, nameof(ViewModule.NavWidth));
            maskAnimator = new DrawingTickAnimator(new QuadraticBezierTicker(QuadraticCurves.EaseOut, 30), mask, nameof(mask.BackColor));
            funnyBtnAnimator = new DrawingTickAnimator(new CubicBezierTicker(CubicCurves.OutBack), funnyBtn, nameof(funnyBtn.Location));
            //CheckForIllegalCrossThreadCalls = false;
            navAnimator.SetPropertySetter((setAction) => Invoke(setAction));              // cross-threads operation needs this. 跨线程操作需要这个
            funnyBtnAnimator.SetPropertySetter((setAction) => Invoke(setAction));         // invoke an action at main thread 在主线程上invoke一个action
        }

        private void InitBinding()
        {
            ViewModule = new MainWindowModule();
            navContainer.DataBindings.Add(new Binding(nameof(Width), ViewModule, nameof(ViewModule.NavWidth)));
            notifyAnimEnded.DataBindings.Add(new Binding(nameof(CheckBox.Checked), ViewModule, nameof(ViewModule.NotifyAnimationEnded)));
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            IsShowMenu = false;
        }

        TickAnimator navAnimator;
        DrawingTickAnimator maskAnimator;       // oops, winform doesn't support transparent panel (opacity:0.2) 噢, winform 不支持透明panel (不透明度:0.2)
        DrawingTickAnimator funnyBtnAnimator;
        CubicBezierTicker showTicker = new CubicBezierTicker(CubicCurves.OutBack, 30);
        ITicker closeTicker = new QuadraticBezierTicker(QuadraticCurves.EaseOut, 30);
        private bool isShowMenu = false;

        bool IsShowMenu
        {
            get => isShowMenu;
            set
            {
                isShowMenu = value;
                if (value)
                {
                    //mask.BackColor = Color.Transparent;
                    //mask.BringToFront();
                    //navContainer.BringToFront();
                    //maskAnimator.FrameAnimate(mask.BackColor, Color.FromArgb(50, 0, 0, 0), 100);
                    navAnimator.SetTicker(showTicker);
                    navAnimator.Animate(ViewModule.NavWidth, 200, 500).ContinueWith((t) =>    // show menu bar  显示菜单栏, 500ms
                    {
                        if (ViewModule.NotifyAnimationEnded)
                            MessageBox.Show("Animation ended");
                    });
                }
                else
                {
                    //maskAnimator.FrameAnimate(mask.BackColor, Color.Transparent, 100).ContinueWith((t) => this.Invoke((Action)(() => mask.SendToBack())));
                    navAnimator.SetTicker(closeTicker);
                    navAnimator.Animate(ViewModule.NavWidth, 0, 200).ContinueWith((t) =>      // hide menu bar  隐藏菜单栏, 200ms
                    {
                        if (ViewModule.NotifyAnimationEnded)
                            MessageBox.Show("Animation ended");
                    });
                }
            }
        }
        private void toggleBtn_Click(object sender, EventArgs e)
        {
            IsShowMenu ^= true;
        }

        private void curvePn_Paint(object sender, PaintEventArgs e)
        {

        }

        private void drawCurveBtn_Click(object sender, EventArgs e)
        {
            clearBtn_Click(null, null);
            double
                width = curvePn.Width,
                height = curvePn.Height;
            Graphics.FromHwnd(curvePn.Handle).DrawLines(Pens.Purple, Enumerable.Range(0, curvePn.Width).Select(i =>
            {
                double r = showTicker.CalcTick(i / width);
                return new PointF(i, (float)(r * height));
            }).ToArray());
        }

        private void clearBtn_Click(object sender, EventArgs e)
        {
            Graphics.FromHwnd(curvePn.Handle).Clear(curvePn.BackColor);
        }

        private void MainWindow_MouseDown(object sender, MouseEventArgs e)
        {
            IsShowMenu = false;
        }

        int RandInt(int start, int end)
        {
            return new Random(Guid.NewGuid().GetHashCode()).Next(start, end);
        }
        private void funnyBtn_Click(object sender, MouseEventArgs e)
        {
            Button sendernBtn = sender as Button;
            Rectangle clientRectangle = sendernBtn.Parent.ClientRectangle;
            clientRectangle.Width -= sendernBtn.Width;
            clientRectangle.Height -= sendernBtn.Height;
            Point newPoint = new Point(RandInt(clientRectangle.X,                                  // new position of button  按钮的新位置
                                               clientRectangle.X + clientRectangle.Width),
                                       RandInt(clientRectangle.Y,
                                               clientRectangle.Y + clientRectangle.Height));
            funnyBtnAnimator.Animate(funnyBtn.Location, newPoint, 200);   // do animation, 执行动画. 200ms
        }
    }
}
