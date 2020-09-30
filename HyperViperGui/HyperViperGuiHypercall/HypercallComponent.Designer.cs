namespace HyperViperGuiHypercall
{
    partial class HypercallComponent
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlHexIn = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.optFast = new System.Windows.Forms.CheckBox();
            this.txtCallnr = new System.Windows.Forms.TextBox();
            this.txtCount = new System.Windows.Forms.TextBox();
            this.txtStart = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtOutSize = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.pnlHexOut = new System.Windows.Forms.Panel();
            this.txtResultStatus = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbFuzzType = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtFuzzSeed = new System.Windows.Forms.TextBox();
            this.txtFuzzCount = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtFuzzMin = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtFuzzMax = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.chkFuzzDbg = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // pnlHexIn
            // 
            this.pnlHexIn.Location = new System.Drawing.Point(3, 167);
            this.pnlHexIn.Name = "pnlHexIn";
            this.pnlHexIn.Size = new System.Drawing.Size(748, 227);
            this.pnlHexIn.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Call nr:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Element count:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Start index:";
            // 
            // optFast
            // 
            this.optFast.AutoSize = true;
            this.optFast.Location = new System.Drawing.Point(7, 76);
            this.optFast.Name = "optFast";
            this.optFast.Size = new System.Drawing.Size(91, 17);
            this.optFast.TabIndex = 4;
            this.optFast.Text = "Fast hypercall";
            this.optFast.UseVisualStyleBackColor = true;
            this.optFast.CheckedChanged += new System.EventHandler(this.optFast_CheckedChanged);
            // 
            // txtCallnr
            // 
            this.txtCallnr.Location = new System.Drawing.Point(91, 1);
            this.txtCallnr.Name = "txtCallnr";
            this.txtCallnr.Size = new System.Drawing.Size(100, 20);
            this.txtCallnr.TabIndex = 5;
            // 
            // txtCount
            // 
            this.txtCount.Location = new System.Drawing.Point(91, 23);
            this.txtCount.Name = "txtCount";
            this.txtCount.Size = new System.Drawing.Size(100, 20);
            this.txtCount.TabIndex = 6;
            // 
            // txtStart
            // 
            this.txtStart.Location = new System.Drawing.Point(91, 44);
            this.txtStart.Name = "txtStart";
            this.txtStart.Size = new System.Drawing.Size(100, 20);
            this.txtStart.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(0, 109);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Output size:";
            // 
            // txtOutSize
            // 
            this.txtOutSize.Location = new System.Drawing.Point(69, 106);
            this.txtOutSize.Name = "txtOutSize";
            this.txtOutSize.Size = new System.Drawing.Size(100, 20);
            this.txtOutSize.TabIndex = 9;
            this.txtOutSize.Text = "4096";
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(224, 29);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(68, 64);
            this.btnSend.TabIndex = 10;
            this.btnSend.Text = "SEND";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // pnlHexOut
            // 
            this.pnlHexOut.Location = new System.Drawing.Point(3, 426);
            this.pnlHexOut.Name = "pnlHexOut";
            this.pnlHexOut.Size = new System.Drawing.Size(748, 178);
            this.pnlHexOut.TabIndex = 1;
            // 
            // txtResultStatus
            // 
            this.txtResultStatus.Location = new System.Drawing.Point(103, 400);
            this.txtResultStatus.Name = "txtResultStatus";
            this.txtResultStatus.ReadOnly = true;
            this.txtResultStatus.Size = new System.Drawing.Size(100, 20);
            this.txtResultStatus.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(-1, 403);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(98, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Result status code:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(345, 4);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Fuzz type:";
            // 
            // cmbFuzzType
            // 
            this.cmbFuzzType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFuzzType.FormattingEnabled = true;
            this.cmbFuzzType.Items.AddRange(new object[] {
            "Increment single byte",
            "Bit flipping single bit",
            "Replace with single special value",
            "Increment multiple bytes randomly",
            "Bit flipping multiple bits randomly"});
            this.cmbFuzzType.Location = new System.Drawing.Point(459, 0);
            this.cmbFuzzType.Name = "cmbFuzzType";
            this.cmbFuzzType.Size = new System.Drawing.Size(250, 21);
            this.cmbFuzzType.TabIndex = 14;
            this.cmbFuzzType.SelectedIndexChanged += new System.EventHandler(this.cmbFuzzType_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(345, 25);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(99, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "Start position/seed:";
            // 
            // txtFuzzSeed
            // 
            this.txtFuzzSeed.Enabled = false;
            this.txtFuzzSeed.Location = new System.Drawing.Point(459, 22);
            this.txtFuzzSeed.Name = "txtFuzzSeed";
            this.txtFuzzSeed.Size = new System.Drawing.Size(250, 20);
            this.txtFuzzSeed.TabIndex = 16;
            this.txtFuzzSeed.Text = "0";
            // 
            // txtFuzzCount
            // 
            this.txtFuzzCount.Enabled = false;
            this.txtFuzzCount.Location = new System.Drawing.Point(459, 43);
            this.txtFuzzCount.Name = "txtFuzzCount";
            this.txtFuzzCount.Size = new System.Drawing.Size(250, 20);
            this.txtFuzzCount.TabIndex = 18;
            this.txtFuzzCount.Text = "0";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(345, 46);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(38, 13);
            this.label8.TabIndex = 17;
            this.label8.Text = "Count:";
            // 
            // txtFuzzMin
            // 
            this.txtFuzzMin.Enabled = false;
            this.txtFuzzMin.Location = new System.Drawing.Point(459, 64);
            this.txtFuzzMin.Name = "txtFuzzMin";
            this.txtFuzzMin.Size = new System.Drawing.Size(250, 20);
            this.txtFuzzMin.TabIndex = 20;
            this.txtFuzzMin.Text = "1";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(345, 67);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(68, 13);
            this.label9.TabIndex = 19;
            this.label9.Text = "Min changes";
            // 
            // txtFuzzMax
            // 
            this.txtFuzzMax.Enabled = false;
            this.txtFuzzMax.Location = new System.Drawing.Point(459, 85);
            this.txtFuzzMax.Name = "txtFuzzMax";
            this.txtFuzzMax.Size = new System.Drawing.Size(250, 20);
            this.txtFuzzMax.TabIndex = 22;
            this.txtFuzzMax.Text = "1";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(345, 88);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(71, 13);
            this.label10.TabIndex = 21;
            this.label10.Text = "Max changes";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(541, 135);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 26);
            this.button1.TabIndex = 23;
            this.button1.Text = "FUZZ";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // chkFuzzDbg
            // 
            this.chkFuzzDbg.AutoSize = true;
            this.chkFuzzDbg.Location = new System.Drawing.Point(459, 109);
            this.chkFuzzDbg.Name = "chkFuzzDbg";
            this.chkFuzzDbg.Size = new System.Drawing.Size(201, 17);
            this.chkFuzzDbg.TabIndex = 24;
            this.chkFuzzDbg.Text = "Show debug messages(much slower)";
            this.chkFuzzDbg.UseVisualStyleBackColor = true;
            // 
            // HypercallComponent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chkFuzzDbg);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtFuzzMax);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtFuzzMin);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtFuzzCount);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtFuzzSeed);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cmbFuzzType);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtResultStatus);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.pnlHexOut);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.txtOutSize);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtStart);
            this.Controls.Add(this.txtCount);
            this.Controls.Add(this.txtCallnr);
            this.Controls.Add(this.optFast);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pnlHexIn);
            this.Name = "HypercallComponent";
            this.Size = new System.Drawing.Size(754, 607);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlHexIn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox optFast;
        private System.Windows.Forms.TextBox txtCallnr;
        private System.Windows.Forms.TextBox txtCount;
        private System.Windows.Forms.TextBox txtStart;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtOutSize;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Panel pnlHexOut;
        private System.Windows.Forms.TextBox txtResultStatus;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbFuzzType;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtFuzzSeed;
        private System.Windows.Forms.TextBox txtFuzzCount;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtFuzzMin;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtFuzzMax;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox chkFuzzDbg;
    }
}
