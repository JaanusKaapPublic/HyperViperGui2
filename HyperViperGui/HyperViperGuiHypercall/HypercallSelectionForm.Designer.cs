namespace HyperViperGuiHypercall
{
    partial class HypercallSelectionForm
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
            this.table = new System.Windows.Forms.DataGridView();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hypercallNr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fast = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.count = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.start = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.input = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.open = new System.Windows.Forms.DataGridViewButtonColumn();
            this.btnOpen = new System.Windows.Forms.Button();
            this.chkFuzzDbg = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.txtFuzzMax = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtFuzzMin = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtFuzzCount = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtFuzzSeed = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbFuzzType = new System.Windows.Forms.ComboBox();
            this.lblCount = new System.Windows.Forms.Label();
            this.txtFuzzMaxPos = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.table)).BeginInit();
            this.SuspendLayout();
            // 
            // table
            // 
            this.table.AllowUserToAddRows = false;
            this.table.AllowUserToDeleteRows = false;
            this.table.AllowUserToOrderColumns = true;
            this.table.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.table.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.hypercallNr,
            this.fast,
            this.count,
            this.start,
            this.input,
            this.open});
            this.table.Location = new System.Drawing.Point(12, 12);
            this.table.Name = "table";
            this.table.Size = new System.Drawing.Size(698, 309);
            this.table.TabIndex = 0;
            this.table.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.table_CellContentClick);
            // 
            // id
            // 
            this.id.HeaderText = "ID";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Width = 60;
            // 
            // hypercallNr
            // 
            this.hypercallNr.HeaderText = "Hypercall nr";
            this.hypercallNr.Name = "hypercallNr";
            this.hypercallNr.ReadOnly = true;
            // 
            // fast
            // 
            this.fast.HeaderText = "Fast";
            this.fast.Name = "fast";
            this.fast.ReadOnly = true;
            this.fast.Width = 60;
            // 
            // count
            // 
            this.count.HeaderText = "Count";
            this.count.Name = "count";
            this.count.ReadOnly = true;
            // 
            // start
            // 
            this.start.HeaderText = "Start";
            this.start.Name = "start";
            this.start.ReadOnly = true;
            // 
            // input
            // 
            this.input.HeaderText = "Input size";
            this.input.Name = "input";
            this.input.ReadOnly = true;
            // 
            // open
            // 
            this.open.HeaderText = "Open";
            this.open.Name = "open";
            this.open.ReadOnly = true;
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(13, 327);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(697, 23);
            this.btnOpen.TabIndex = 1;
            this.btnOpen.Text = "Open selected";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // chkFuzzDbg
            // 
            this.chkFuzzDbg.AutoSize = true;
            this.chkFuzzDbg.Location = new System.Drawing.Point(12, 511);
            this.chkFuzzDbg.Name = "chkFuzzDbg";
            this.chkFuzzDbg.Size = new System.Drawing.Size(201, 17);
            this.chkFuzzDbg.TabIndex = 36;
            this.chkFuzzDbg.Text = "Show debug messages(much slower)";
            this.chkFuzzDbg.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(298, 502);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 26);
            this.button1.TabIndex = 35;
            this.button1.Text = "FUZZ";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtFuzzMax
            // 
            this.txtFuzzMax.Enabled = false;
            this.txtFuzzMax.Location = new System.Drawing.Point(123, 454);
            this.txtFuzzMax.Name = "txtFuzzMax";
            this.txtFuzzMax.Size = new System.Drawing.Size(250, 20);
            this.txtFuzzMax.TabIndex = 34;
            this.txtFuzzMax.Text = "1";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(9, 457);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(71, 13);
            this.label10.TabIndex = 33;
            this.label10.Text = "Max changes";
            // 
            // txtFuzzMin
            // 
            this.txtFuzzMin.Enabled = false;
            this.txtFuzzMin.Location = new System.Drawing.Point(123, 433);
            this.txtFuzzMin.Name = "txtFuzzMin";
            this.txtFuzzMin.Size = new System.Drawing.Size(250, 20);
            this.txtFuzzMin.TabIndex = 32;
            this.txtFuzzMin.Text = "1";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(9, 436);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(68, 13);
            this.label9.TabIndex = 31;
            this.label9.Text = "Min changes";
            // 
            // txtFuzzCount
            // 
            this.txtFuzzCount.Enabled = false;
            this.txtFuzzCount.Location = new System.Drawing.Point(123, 412);
            this.txtFuzzCount.Name = "txtFuzzCount";
            this.txtFuzzCount.Size = new System.Drawing.Size(250, 20);
            this.txtFuzzCount.TabIndex = 30;
            this.txtFuzzCount.Text = "0";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(9, 415);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(38, 13);
            this.label8.TabIndex = 29;
            this.label8.Text = "Count:";
            // 
            // txtFuzzSeed
            // 
            this.txtFuzzSeed.Enabled = false;
            this.txtFuzzSeed.Location = new System.Drawing.Point(123, 391);
            this.txtFuzzSeed.Name = "txtFuzzSeed";
            this.txtFuzzSeed.Size = new System.Drawing.Size(250, 20);
            this.txtFuzzSeed.TabIndex = 28;
            this.txtFuzzSeed.Text = "0";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 394);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(99, 13);
            this.label7.TabIndex = 27;
            this.label7.Text = "Start position/seed:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 373);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 13);
            this.label6.TabIndex = 25;
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
            this.cmbFuzzType.Location = new System.Drawing.Point(123, 370);
            this.cmbFuzzType.Name = "cmbFuzzType";
            this.cmbFuzzType.Size = new System.Drawing.Size(250, 21);
            this.cmbFuzzType.TabIndex = 37;
            this.cmbFuzzType.SelectedIndexChanged += new System.EventHandler(this.cmbFuzzType_SelectedIndexChanged);
            // 
            // lblCount
            // 
            this.lblCount.AutoSize = true;
            this.lblCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCount.Location = new System.Drawing.Point(410, 412);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(71, 37);
            this.lblCount.TabIndex = 38;
            this.lblCount.Text = "???";
            // 
            // txtFuzzMaxPos
            // 
            this.txtFuzzMaxPos.Location = new System.Drawing.Point(123, 476);
            this.txtFuzzMaxPos.Name = "txtFuzzMaxPos";
            this.txtFuzzMaxPos.Size = new System.Drawing.Size(250, 20);
            this.txtFuzzMaxPos.TabIndex = 40;
            this.txtFuzzMaxPos.Text = "16";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 479);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 13);
            this.label1.TabIndex = 39;
            this.label1.Text = "Max position to fuzz";
            // 
            // HypercallSelectionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(722, 543);
            this.Controls.Add(this.txtFuzzMaxPos);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblCount);
            this.Controls.Add(this.cmbFuzzType);
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
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnOpen);
            this.Controls.Add(this.table);
            this.Name = "HypercallSelectionForm";
            this.Text = "Hypercalls selection";
            ((System.ComponentModel.ISupportInitialize)(this.table)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView table;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn hypercallNr;
        private System.Windows.Forms.DataGridViewCheckBoxColumn fast;
        private System.Windows.Forms.DataGridViewTextBoxColumn count;
        private System.Windows.Forms.DataGridViewTextBoxColumn start;
        private System.Windows.Forms.DataGridViewTextBoxColumn input;
        private System.Windows.Forms.DataGridViewButtonColumn open;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.CheckBox chkFuzzDbg;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtFuzzMax;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtFuzzMin;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtFuzzCount;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtFuzzSeed;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbFuzzType;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.TextBox txtFuzzMaxPos;
        private System.Windows.Forms.Label label1;
    }
}