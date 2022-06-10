using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using NullLib.TickAnimation;
using NullLib.TickAnimation.WinForm;
using TestForm.View;
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
            maskAnimator = new DrawingTickAnimator(new QuadraticBezierTicker(QuadraticBezierCurve.Ease), mask, nameof(mask.BackColor));
            funnyBtnAnimator = new DrawingTickAnimator(new CubicBezierTicker(CubicBezierCurve.Back, EasingMode.EaseOut), funnyBtn, nameof(funnyBtn.Location));
            formAnimator = new DrawingTickAnimator(new CubicBezierTicker(CubicBezierCurve.Ease), this, nameof(Bounds));
            pnColorAnimator = new DrawingTickAnimator(new CubicTicker(EasingMode.EaseOut), this, nameof(PnColor));

            CheckForIllegalCrossThreadCalls = false;
            navAnimator.UseWinForm(this);
            funnyBtnAnimator.UseWinForm(funnyBtn);         // invoke an action at main thread 在主线程上invoke一个action
            formAnimator.UseWinForm(this);

            //funnyBtn.MouseEnter += funnyBtn_Click;
            funnyBtn.MouseEnter += funnyBtn_RandomMove;

            for (int i = 0; i < 10; i++)
            {
                Button btn = new Button()
                {
                    Text = "Hello?",
                    Width = 200,
                    Height = 45,
                };

                var animator = new TickAnimator(new BackTicker(EasingMode.EaseOut), btn, nameof(btn.Height));
                animator.UseWinForm(btn);

                btn.MouseEnter += (sender, e) => animator.Animate(85, 300);
                btn.MouseLeave += (sender, e) => animator.Animate(45, 300);

                listTest.Controls.Add(btn);
            }

            TickAnimator titleAnimator = new TickAnimator(new CircleTicker(EasingMode.EaseOut), formTitle, nameof(Left));
            titleAnimator.UseWinForm(formTitle);

            int originTitleLeft = formTitle.Left;
            formTitle.MouseEnter += (s, e) => titleAnimator.Animate(originTitleLeft + 50, 300);
            formTitle.MouseLeave += (s, e) => titleAnimator.Animate(originTitleLeft, 1000);
        }

        private void InitBinding()
        {
            ViewModule = new MainWindowModule();
            navContainer.DataBindings.Add(new Binding(nameof(Width), ViewModule, nameof(ViewModule.NavWidth)));
            notifyAnimEnded.DataBindings.Add(new Binding(nameof(CheckBox.Checked), ViewModule, nameof(ViewModule.NotifyAnimationEnded)));
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            BufferedGraphicsManager.Current.Allocate(Graphics.FromHwnd(IntPtr.Zero), new Rectangle(0, 0, Width, Height));
            IsShowMenu = false;
            recordedState = Bounds;
        }

        TickAnimator navAnimator;
        DrawingTickAnimator maskAnimator;       // oops, winform doesn't support transparent panel (opacity:0.2) 噢, winform 不支持透明panel (不透明度:0.2)
        DrawingTickAnimator funnyBtnAnimator;
        DrawingTickAnimator formAnimator;
        CubicBezierTicker showTicker = new CubicBezierTicker(CubicBezierCurve.Back, EasingMode.EaseOut);
        TickerBase closeTicker = new QuadraticBezierTicker(QuadraticBezierCurve.Ease);
        TickerBase tempTicker = new BackTicker(EasingMode.EaseOut) { Amplitude = 0.5 };
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
                    navAnimator.SetTicker(tempTicker);
                    navAnimator.Animate(300, 500).ContinueWith((t) =>    // show menu bar  显示菜单栏, 500ms
                    {
                        if (ViewModule.NotifyAnimationEnded)
                            MessageBox.Show("Animation ended");
                    });
                }
                else
                {
                    //maskAnimator.FrameAnimate(mask.BackColor, Color.Transparent, 100).ContinueWith((t) => this.Invoke((Action)(() => mask.SendToBack())));
                    navAnimator.SetTicker(closeTicker);
                    navAnimator.Animate(0, 200).ContinueWith((t) =>      // hide menu bar  隐藏菜单栏, 200ms
                    {
                        if (ViewModule.NotifyAnimationEnded)
                            MessageBox.Show("Animation ended");
                    });
                }
            }
        }
        private void toggleBtn_Click(object sender, EventArgs e)
        {
            IsShowMenu = !IsShowMenu;
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
        Point GetRandomPoint(Rectangle container, Size selfSize)
        {
            container.Size -= selfSize;
            return new Point(RandInt(container.X, container.X + container.Width), RandInt(container.Y, container.Y + container.Height));
        }
        private void funnyBtn_RandomMove(object sender, EventArgs e)
        {
            Button senderBtn = sender as Button;
            Point newPoint = GetRandomPoint(senderBtn.Parent.ClientRectangle, senderBtn.Size);

            funnyBtnAnimator.Animate(funnyBtn.Location, newPoint, 200);   // do animation, 执行动画. 200ms
        }

        private void randPosBtn_Click(object sender, EventArgs e)
        {
            Point newPoint = GetRandomPoint(Screen.PrimaryScreen.WorkingArea, Size);

            formAnimator.Animate(Bounds, new Rectangle(newPoint, Size), 200);
        }

        Rectangle recordedState;
        private void transBtn_Click(object sender, EventArgs e)
        {
            formAnimator.Animate(Bounds, recordedState, 200);
        }

        private void recordBtn_Click(object sender, EventArgs e)
        {
            recordedState = Bounds;
        }

        private void aroundBtn_Click(object sender, EventArgs e)
        {
            Rectangle start = Bounds;
            Size startSize = start.Size;
            Rectangle workingArea = Screen.PrimaryScreen.WorkingArea;
            int
                xend = workingArea.Width - Width,
                yend = workingArea.Height - Height;


            Task.Run(() =>
            {
                formAnimator
                    .SyncAnimate(new Rectangle(Point.Empty, startSize), 200)
                    .SyncAnimate(new Rectangle(new Point(xend, 0), startSize), 200)
                    .SyncAnimate(new Rectangle(new Point(xend, yend), startSize), 200)
                    .SyncAnimate(new Rectangle(new Point(0, yend), startSize), 200)
                    .SyncAnimate(workingArea, 200)
                    .SyncAnimate(start, 200);
            });
        }


        public Color PnColor
        {
            get => curvePn.BackColor; set
            {
                curvePn.BackColor = value;
                Graphics.FromHwnd(curvePn.Handle).Clear(value);
            }
        }
        DrawingTickAnimator pnColorAnimator;
        private void curvePn_MouseLeave(object sender, EventArgs e)
        {
            pnColorAnimator.Animate(Color.FromArgb(240, 240, 240), 200);
        }

        private void curvePn_MouseEnter(object sender, EventArgs e)
        {
            pnColorAnimator.Animate(Color.Pink, 200);
        }

        private void btn_bezier_Click(object sender, EventArgs e)
        {
            new BezierCurveTest().ShowDialog();
        }

        private void btn_testform_Click(object sender, EventArgs e)
        {
            new View.TestWindow().ShowDialog();
        }
    }
}
