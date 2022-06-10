
namespace TestForm
{
    partial class MainWindow
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.toggleBtn = new System.Windows.Forms.Button();
            this.clearBtn = new System.Windows.Forms.Button();
            this.drawCurveBtn = new System.Windows.Forms.Button();
            this.curvePn = new System.Windows.Forms.Panel();
            this.resetBtn = new System.Windows.Forms.Button();
            this.dataPanel = new System.Windows.Forms.GroupBox();
            this.funnyBtn = new System.Windows.Forms.Button();
            this.appAuthor = new System.Windows.Forms.Label();
            this.formTitle = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.navPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.button1 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.navContainer = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.notifyAnimEnded = new System.Windows.Forms.CheckBox();
            this.mask = new System.Windows.Forms.Panel();
            this.listTest = new System.Windows.Forms.FlowLayoutPanel();
            this.btn_testform = new System.Windows.Forms.Button();
            this.btn_bezier = new System.Windows.Forms.Button();
            this.aroundBtn = new System.Windows.Forms.Button();
            this.recordBtn = new System.Windows.Forms.Button();
            this.transBtn = new System.Windows.Forms.Button();
            this.randPosBtn = new System.Windows.Forms.Button();
            this.dataPanel.SuspendLayout();
            this.navPanel.SuspendLayout();
            this.navContainer.SuspendLayout();
            this.panel1.SuspendLayout();
            this.mask.SuspendLayout();
            this.SuspendLayout();
            // 
            // toggleBtn
            // 
            this.toggleBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.toggleBtn.Location = new System.Drawing.Point(24, 703);
            this.toggleBtn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.toggleBtn.Name = "toggleBtn";
            this.toggleBtn.Size = new System.Drawing.Size(81, 38);
            this.toggleBtn.TabIndex = 0;
            this.toggleBtn.Text = "Toggle";
            this.toggleBtn.UseVisualStyleBackColor = true;
            this.toggleBtn.Click += new System.EventHandler(this.toggleBtn_Click);
            // 
            // clearBtn
            // 
            this.clearBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.clearBtn.Location = new System.Drawing.Point(1058, 543);
            this.clearBtn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.clearBtn.Name = "clearBtn";
            this.clearBtn.Size = new System.Drawing.Size(38, 38);
            this.clearBtn.TabIndex = 8;
            this.clearBtn.Text = "C";
            this.clearBtn.UseVisualStyleBackColor = true;
            this.clearBtn.Click += new System.EventHandler(this.clearBtn_Click);
            // 
            // drawCurveBtn
            // 
            this.drawCurveBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.drawCurveBtn.Location = new System.Drawing.Point(1104, 543);
            this.drawCurveBtn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.drawCurveBtn.Name = "drawCurveBtn";
            this.drawCurveBtn.Size = new System.Drawing.Size(112, 38);
            this.drawCurveBtn.TabIndex = 6;
            this.drawCurveBtn.Text = "DrawCurve";
            this.drawCurveBtn.UseVisualStyleBackColor = true;
            this.drawCurveBtn.Click += new System.EventHandler(this.drawCurveBtn_Click);
            // 
            // curvePn
            // 
            this.curvePn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.curvePn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.curvePn.Location = new System.Drawing.Point(916, 200);
            this.curvePn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.curvePn.Name = "curvePn";
            this.curvePn.Size = new System.Drawing.Size(299, 332);
            this.curvePn.TabIndex = 5;
            this.curvePn.Paint += new System.Windows.Forms.PaintEventHandler(this.curvePn_Paint);
            this.curvePn.MouseEnter += new System.EventHandler(this.curvePn_MouseEnter);
            this.curvePn.MouseLeave += new System.EventHandler(this.curvePn_MouseLeave);
            // 
            // resetBtn
            // 
            this.resetBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.resetBtn.Location = new System.Drawing.Point(116, 703);
            this.resetBtn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.resetBtn.Name = "resetBtn";
            this.resetBtn.Size = new System.Drawing.Size(70, 38);
            this.resetBtn.TabIndex = 4;
            this.resetBtn.Text = "Reset";
            this.resetBtn.UseVisualStyleBackColor = true;
            // 
            // dataPanel
            // 
            this.dataPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataPanel.Controls.Add(this.funnyBtn);
            this.dataPanel.Location = new System.Drawing.Point(309, 185);
            this.dataPanel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dataPanel.Name = "dataPanel";
            this.dataPanel.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dataPanel.Size = new System.Drawing.Size(598, 468);
            this.dataPanel.TabIndex = 3;
            this.dataPanel.TabStop = false;
            this.dataPanel.Text = "Data Panel";
            // 
            // funnyBtn
            // 
            this.funnyBtn.Location = new System.Drawing.Point(148, 178);
            this.funnyBtn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.funnyBtn.Name = "funnyBtn";
            this.funnyBtn.Size = new System.Drawing.Size(112, 38);
            this.funnyBtn.TabIndex = 0;
            this.funnyBtn.Text = "Catch me";
            this.funnyBtn.UseVisualStyleBackColor = true;
            this.funnyBtn.MouseDown += new System.Windows.Forms.MouseEventHandler(this.funnyBtn_RandomMove);
            // 
            // appAuthor
            // 
            this.appAuthor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.appAuthor.AutoSize = true;
            this.appAuthor.Location = new System.Drawing.Point(1083, 117);
            this.appAuthor.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.appAuthor.Name = "appAuthor";
            this.appAuthor.Size = new System.Drawing.Size(105, 20);
            this.appAuthor.TabIndex = 2;
            this.appAuthor.Text = "- By SlimeNull";
            // 
            // formTitle
            // 
            this.formTitle.AutoSize = true;
            this.formTitle.Font = new System.Drawing.Font("Microsoft YaHei UI", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.formTitle.Location = new System.Drawing.Point(30, 52);
            this.formTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.formTitle.Name = "formTitle";
            this.formTitle.Size = new System.Drawing.Size(397, 52);
            this.formTitle.TabIndex = 1;
            this.formTitle.Text = "WinformAnimation";
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(1136, 703);
            this.button2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(81, 38);
            this.button2.TabIndex = 0;
            this.button2.Text = "Toggle";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.toggleBtn_Click);
            // 
            // navPanel
            // 
            this.navPanel.BackColor = System.Drawing.Color.Transparent;
            this.navPanel.Controls.Add(this.button1);
            this.navPanel.Controls.Add(this.button3);
            this.navPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.navPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.navPanel.Location = new System.Drawing.Point(0, 0);
            this.navPanel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.navPanel.Name = "navPanel";
            this.navPanel.Size = new System.Drawing.Size(10, 762);
            this.navPanel.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(4, 5);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(112, 38);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.Location = new System.Drawing.Point(35, 53);
            this.button3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(81, 38);
            this.button3.TabIndex = 0;
            this.button3.Text = "Toggle";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.toggleBtn_Click);
            // 
            // navContainer
            // 
            this.navContainer.BackColor = System.Drawing.SystemColors.ControlLight;
            this.navContainer.Controls.Add(this.navPanel);
            this.navContainer.Dock = System.Windows.Forms.DockStyle.Left;
            this.navContainer.Location = new System.Drawing.Point(0, 0);
            this.navContainer.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.navContainer.Name = "navContainer";
            this.navContainer.Size = new System.Drawing.Size(10, 762);
            this.navContainer.TabIndex = 5;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.notifyAnimEnded);
            this.panel1.Controls.Add(this.clearBtn);
            this.panel1.Controls.Add(this.navContainer);
            this.panel1.Controls.Add(this.formTitle);
            this.panel1.Controls.Add(this.drawCurveBtn);
            this.panel1.Controls.Add(this.toggleBtn);
            this.panel1.Controls.Add(this.curvePn);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.resetBtn);
            this.panel1.Controls.Add(this.appAuthor);
            this.panel1.Controls.Add(this.dataPanel);
            this.panel1.Controls.Add(this.mask);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1244, 762);
            this.panel1.TabIndex = 6;
            // 
            // notifyAnimEnded
            // 
            this.notifyAnimEnded.AutoSize = true;
            this.notifyAnimEnded.Location = new System.Drawing.Point(918, 625);
            this.notifyAnimEnded.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.notifyAnimEnded.Name = "notifyAnimEnded";
            this.notifyAnimEnded.Size = new System.Drawing.Size(197, 24);
            this.notifyAnimEnded.TabIndex = 9;
            this.notifyAnimEnded.Text = "Notify animation ended";
            this.notifyAnimEnded.UseVisualStyleBackColor = true;
            // 
            // mask
            // 
            this.mask.BackColor = System.Drawing.SystemColors.Control;
            this.mask.Controls.Add(this.listTest);
            this.mask.Controls.Add(this.btn_testform);
            this.mask.Controls.Add(this.btn_bezier);
            this.mask.Controls.Add(this.aroundBtn);
            this.mask.Controls.Add(this.recordBtn);
            this.mask.Controls.Add(this.transBtn);
            this.mask.Controls.Add(this.randPosBtn);
            this.mask.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mask.Location = new System.Drawing.Point(0, 0);
            this.mask.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.mask.Name = "mask";
            this.mask.Size = new System.Drawing.Size(1244, 762);
            this.mask.TabIndex = 10;
            // 
            // listTest
            // 
            this.listTest.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.listTest.Location = new System.Drawing.Point(39, 200);
            this.listTest.Name = "listTest";
            this.listTest.Size = new System.Drawing.Size(246, 453);
            this.listTest.TabIndex = 4;
            this.listTest.WrapContents = false;
            // 
            // btn_testform
            // 
            this.btn_testform.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_testform.Location = new System.Drawing.Point(350, 703);
            this.btn_testform.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_testform.Name = "btn_testform";
            this.btn_testform.Size = new System.Drawing.Size(112, 38);
            this.btn_testform.TabIndex = 3;
            this.btn_testform.Text = "Test";
            this.btn_testform.UseVisualStyleBackColor = true;
            this.btn_testform.Click += new System.EventHandler(this.btn_testform_Click);
            // 
            // btn_bezier
            // 
            this.btn_bezier.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_bezier.Location = new System.Drawing.Point(470, 703);
            this.btn_bezier.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_bezier.Name = "btn_bezier";
            this.btn_bezier.Size = new System.Drawing.Size(112, 38);
            this.btn_bezier.TabIndex = 3;
            this.btn_bezier.Text = "Bezier";
            this.btn_bezier.UseVisualStyleBackColor = true;
            this.btn_bezier.Click += new System.EventHandler(this.btn_bezier_Click);
            // 
            // aroundBtn
            // 
            this.aroundBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.aroundBtn.Location = new System.Drawing.Point(590, 703);
            this.aroundBtn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.aroundBtn.Name = "aroundBtn";
            this.aroundBtn.Size = new System.Drawing.Size(112, 38);
            this.aroundBtn.TabIndex = 3;
            this.aroundBtn.Text = "Around";
            this.aroundBtn.UseVisualStyleBackColor = true;
            this.aroundBtn.Click += new System.EventHandler(this.aroundBtn_Click);
            // 
            // recordBtn
            // 
            this.recordBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.recordBtn.Location = new System.Drawing.Point(711, 703);
            this.recordBtn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.recordBtn.Name = "recordBtn";
            this.recordBtn.Size = new System.Drawing.Size(128, 38);
            this.recordBtn.TabIndex = 2;
            this.recordBtn.Text = "Record State";
            this.recordBtn.UseVisualStyleBackColor = true;
            this.recordBtn.Click += new System.EventHandler(this.recordBtn_Click);
            // 
            // transBtn
            // 
            this.transBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.transBtn.Location = new System.Drawing.Point(848, 703);
            this.transBtn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.transBtn.Name = "transBtn";
            this.transBtn.Size = new System.Drawing.Size(112, 38);
            this.transBtn.TabIndex = 1;
            this.transBtn.Text = "Transform";
            this.transBtn.UseVisualStyleBackColor = true;
            this.transBtn.Click += new System.EventHandler(this.transBtn_Click);
            // 
            // randPosBtn
            // 
            this.randPosBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.randPosBtn.Location = new System.Drawing.Point(969, 703);
            this.randPosBtn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.randPosBtn.Name = "randPosBtn";
            this.randPosBtn.Size = new System.Drawing.Size(158, 38);
            this.randPosBtn.TabIndex = 0;
            this.randPosBtn.Text = "Random Position";
            this.randPosBtn.UseVisualStyleBackColor = true;
            this.randPosBtn.Click += new System.EventHandler(this.randPosBtn_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1244, 762);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MinimumSize = new System.Drawing.Size(1256, 789);
            this.Name = "MainWindow";
            this.Text = "WinformAnimationTest";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MainWindow_MouseDown);
            this.dataPanel.ResumeLayout(false);
            this.navPanel.ResumeLayout(false);
            this.navContainer.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.mask.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button toggleBtn;
        private System.Windows.Forms.Label appAuthor;
        private System.Windows.Forms.Label formTitle;
        private System.Windows.Forms.Button resetBtn;
        private System.Windows.Forms.GroupBox dataPanel;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.FlowLayoutPanel navPanel;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button drawCurveBtn;
        private System.Windows.Forms.Button clearBtn;
        private System.Windows.Forms.Panel navContainer;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox notifyAnimEnded;
        private System.Windows.Forms.Panel mask;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button funnyBtn;
        private System.Windows.Forms.Button randPosBtn;
        private System.Windows.Forms.Button transBtn;
        private System.Windows.Forms.Button recordBtn;
        private System.Windows.Forms.Button aroundBtn;
        private System.Windows.Forms.Panel curvePn;
        private System.Windows.Forms.Button btn_bezier;
        private System.Windows.Forms.Button btn_testform;
        private System.Windows.Forms.FlowLayoutPanel listTest;
    }
}

