namespace TestForm.View
{
    partial class BezierCurveTest
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BezierCurveTest));
            this.panel1 = new System.Windows.Forms.Panel();
            this.cubicBezierView1 = new NullLib.TickAnimation.WinForm.CubicBezierView();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.cubicBezierView1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 450);
            this.panel1.TabIndex = 0;
            // 
            // cubicBezierView1
            // 
            this.cubicBezierView1.ControlHandleEnabled = true;
            this.cubicBezierView1.ControlHandleSize = 20;
            this.cubicBezierView1.ControlPoint1 = ((System.Drawing.PointF)(resources.GetObject("cubicBezierView1.ControlPoint1")));
            this.cubicBezierView1.ControlPoint2 = ((System.Drawing.PointF)(resources.GetObject("cubicBezierView1.ControlPoint2")));
            this.cubicBezierView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cubicBezierView1.Location = new System.Drawing.Point(0, 0);
            this.cubicBezierView1.Name = "cubicBezierView1";
            this.cubicBezierView1.Size = new System.Drawing.Size(798, 448);
            this.cubicBezierView1.TabIndex = 0;
            this.cubicBezierView1.Text = "cubicBezierView1";
            // 
            // BezierCurveTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.Name = "BezierCurveTest";
            this.Text = "BezierCurveTest";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private NullLib.TickAnimation.WinForm.CubicBezierView cubicBezierView1;
    }
}