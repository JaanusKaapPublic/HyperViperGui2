namespace HyperViperGuiHypercall
{
    partial class DriverControlComponent
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
            this.btnClear = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtRefresh = new System.Windows.Forms.TextBox();
            this.txtRecord = new System.Windows.Forms.TextBox();
            this.btnRecord = new System.Windows.Forms.Button();
            this.btnHook = new System.Windows.Forms.Button();
            this.table = new System.Windows.Forms.DataGridView();
            this.nr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.count = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.elementsCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fast = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.slow = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.breakpoint = new System.Windows.Forms.DataGridViewButtonColumn();
            this.log = new System.Windows.Forms.DataGridViewButtonColumn();
            this.logFile = new System.Windows.Forms.DataGridViewButtonColumn();
            this.clear = new System.Windows.Forms.DataGridViewButtonColumn();
            this.optHideSlow = new System.Windows.Forms.CheckBox();
            this.optHideFast = new System.Windows.Forms.CheckBox();
            this.optHideZero = new System.Windows.Forms.CheckBox();
            this.btnAllToFile = new System.Windows.Forms.Button();
            this.btnAllBreak = new System.Windows.Forms.Button();
            this.btnAllDbg = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.table)).BeginInit();
            this.SuspendLayout();
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(3, 33);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 24;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(614, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 19;
            this.label1.Text = "Refresh rate:";
            // 
            // txtRefresh
            // 
            this.txtRefresh.Location = new System.Drawing.Point(688, 30);
            this.txtRefresh.Name = "txtRefresh";
            this.txtRefresh.Size = new System.Drawing.Size(56, 20);
            this.txtRefresh.TabIndex = 18;
            this.txtRefresh.Text = "2000";
            this.txtRefresh.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtRefresh_KeyUp);
            this.txtRefresh.Leave += new System.EventHandler(this.txtRefresh_Leave);
            // 
            // txtRecord
            // 
            this.txtRecord.Location = new System.Drawing.Point(216, 4);
            this.txtRecord.Name = "txtRecord";
            this.txtRecord.Size = new System.Drawing.Size(528, 20);
            this.txtRecord.TabIndex = 15;
            this.txtRecord.TextChanged += new System.EventHandler(this.txtRecord_TextChanged);
            // 
            // btnRecord
            // 
            this.btnRecord.Location = new System.Drawing.Point(85, 3);
            this.btnRecord.Name = "btnRecord";
            this.btnRecord.Size = new System.Drawing.Size(124, 23);
            this.btnRecord.TabIndex = 14;
            this.btnRecord.Text = "Start recording";
            this.btnRecord.UseVisualStyleBackColor = true;
            this.btnRecord.Click += new System.EventHandler(this.btnRecord_Click);
            // 
            // btnHook
            // 
            this.btnHook.Location = new System.Drawing.Point(3, 3);
            this.btnHook.Name = "btnHook";
            this.btnHook.Size = new System.Drawing.Size(75, 23);
            this.btnHook.TabIndex = 13;
            this.btnHook.Text = "Hook";
            this.btnHook.UseVisualStyleBackColor = true;
            this.btnHook.Click += new System.EventHandler(this.btnHook_Click);
            // 
            // table
            // 
            this.table.AllowUserToAddRows = false;
            this.table.AllowUserToDeleteRows = false;
            this.table.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.table.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nr,
            this.count,
            this.pid,
            this.elementsCount,
            this.fast,
            this.slow,
            this.breakpoint,
            this.log,
            this.logFile,
            this.clear});
            this.table.Location = new System.Drawing.Point(3, 99);
            this.table.Name = "table";
            this.table.Size = new System.Drawing.Size(750, 508);
            this.table.TabIndex = 29;
            this.table.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.table_CellContentClick);
            // 
            // nr
            // 
            this.nr.FillWeight = 40F;
            this.nr.HeaderText = "Nr";
            this.nr.Name = "nr";
            this.nr.ReadOnly = true;
            this.nr.Width = 40;
            // 
            // count
            // 
            this.count.HeaderText = "Count";
            this.count.Name = "count";
            this.count.ReadOnly = true;
            this.count.Width = 60;
            // 
            // pid
            // 
            this.pid.HeaderText = "Last PID";
            this.pid.Name = "pid";
            this.pid.ReadOnly = true;
            this.pid.Width = 80;
            // 
            // elementsCount
            // 
            this.elementsCount.HeaderText = "Elements count";
            this.elementsCount.Name = "elementsCount";
            this.elementsCount.ReadOnly = true;
            this.elementsCount.Width = 110;
            // 
            // fast
            // 
            this.fast.HeaderText = "Fast";
            this.fast.Name = "fast";
            this.fast.ReadOnly = true;
            this.fast.Width = 40;
            // 
            // slow
            // 
            this.slow.HeaderText = "Slow";
            this.slow.Name = "slow";
            this.slow.ReadOnly = true;
            this.slow.Width = 40;
            // 
            // breakpoint
            // 
            this.breakpoint.HeaderText = "Break";
            this.breakpoint.Name = "breakpoint";
            this.breakpoint.Width = 60;
            // 
            // log
            // 
            this.log.HeaderText = "Debug log";
            this.log.Name = "log";
            this.log.Width = 80;
            // 
            // logFile
            // 
            this.logFile.HeaderText = "Log to file";
            this.logFile.Name = "logFile";
            this.logFile.Width = 80;
            // 
            // clear
            // 
            this.clear.HeaderText = "Clear";
            this.clear.Name = "clear";
            // 
            // optHideSlow
            // 
            this.optHideSlow.AutoSize = true;
            this.optHideSlow.Location = new System.Drawing.Point(344, 32);
            this.optHideSlow.Name = "optHideSlow";
            this.optHideSlow.Size = new System.Drawing.Size(72, 17);
            this.optHideSlow.TabIndex = 30;
            this.optHideSlow.Text = "Hide slow";
            this.optHideSlow.UseVisualStyleBackColor = true;
            this.optHideSlow.CheckedChanged += new System.EventHandler(this.optCheckedChanged);
            // 
            // optHideFast
            // 
            this.optHideFast.AutoSize = true;
            this.optHideFast.Location = new System.Drawing.Point(422, 32);
            this.optHideFast.Name = "optHideFast";
            this.optHideFast.Size = new System.Drawing.Size(68, 17);
            this.optHideFast.TabIndex = 31;
            this.optHideFast.Text = "Hide fast";
            this.optHideFast.UseVisualStyleBackColor = true;
            this.optHideFast.CheckedChanged += new System.EventHandler(this.optCheckedChanged);
            // 
            // optHideZero
            // 
            this.optHideZero.AutoSize = true;
            this.optHideZero.Location = new System.Drawing.Point(496, 32);
            this.optHideZero.Name = "optHideZero";
            this.optHideZero.Size = new System.Drawing.Size(102, 17);
            this.optHideZero.TabIndex = 32;
            this.optHideZero.Text = "Hide count == 0";
            this.optHideZero.UseVisualStyleBackColor = true;
            this.optHideZero.CheckedChanged += new System.EventHandler(this.optCheckedChanged);
            // 
            // btnAllToFile
            // 
            this.btnAllToFile.Location = new System.Drawing.Point(555, 70);
            this.btnAllToFile.Name = "btnAllToFile";
            this.btnAllToFile.Size = new System.Drawing.Size(75, 23);
            this.btnAllToFile.TabIndex = 33;
            this.btnAllToFile.Text = "Log to file";
            this.btnAllToFile.UseVisualStyleBackColor = true;
            this.btnAllToFile.Click += new System.EventHandler(this.btnAllToFile_Click);
            // 
            // btnAllBreak
            // 
            this.btnAllBreak.Location = new System.Drawing.Point(415, 70);
            this.btnAllBreak.Name = "btnAllBreak";
            this.btnAllBreak.Size = new System.Drawing.Size(47, 23);
            this.btnAllBreak.TabIndex = 34;
            this.btnAllBreak.Text = "Break";
            this.btnAllBreak.UseVisualStyleBackColor = true;
            this.btnAllBreak.Click += new System.EventHandler(this.btnAllBreak_Click);
            // 
            // btnAllDbg
            // 
            this.btnAllDbg.Location = new System.Drawing.Point(477, 70);
            this.btnAllDbg.Name = "btnAllDbg";
            this.btnAllDbg.Size = new System.Drawing.Size(68, 23);
            this.btnAllDbg.TabIndex = 35;
            this.btnAllDbg.Text = "Debug log";
            this.btnAllDbg.UseVisualStyleBackColor = true;
            this.btnAllDbg.Click += new System.EventHandler(this.btnAllDbg_Click);
            // 
            // DriverControlComponent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnAllDbg);
            this.Controls.Add(this.btnAllBreak);
            this.Controls.Add(this.btnAllToFile);
            this.Controls.Add(this.optHideZero);
            this.Controls.Add(this.optHideFast);
            this.Controls.Add(this.optHideSlow);
            this.Controls.Add(this.table);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtRefresh);
            this.Controls.Add(this.txtRecord);
            this.Controls.Add(this.btnRecord);
            this.Controls.Add(this.btnHook);
            this.Name = "DriverControlComponent";
            this.Size = new System.Drawing.Size(756, 610);
            ((System.ComponentModel.ISupportInitialize)(this.table)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtRefresh;
        private System.Windows.Forms.TextBox txtRecord;
        private System.Windows.Forms.Button btnRecord;
        private System.Windows.Forms.Button btnHook;
        private System.Windows.Forms.DataGridView table;
        private System.Windows.Forms.CheckBox optHideSlow;
        private System.Windows.Forms.CheckBox optHideFast;
        private System.Windows.Forms.CheckBox optHideZero;
        private System.Windows.Forms.DataGridViewTextBoxColumn nr;
        private System.Windows.Forms.DataGridViewTextBoxColumn count;
        private System.Windows.Forms.DataGridViewTextBoxColumn pid;
        private System.Windows.Forms.DataGridViewTextBoxColumn elementsCount;
        private System.Windows.Forms.DataGridViewCheckBoxColumn fast;
        private System.Windows.Forms.DataGridViewCheckBoxColumn slow;
        private System.Windows.Forms.DataGridViewButtonColumn breakpoint;
        private System.Windows.Forms.DataGridViewButtonColumn log;
        private System.Windows.Forms.DataGridViewButtonColumn logFile;
        private System.Windows.Forms.DataGridViewButtonColumn clear;
        private System.Windows.Forms.Button btnAllToFile;
        private System.Windows.Forms.Button btnAllBreak;
        private System.Windows.Forms.Button btnAllDbg;
    }
}
