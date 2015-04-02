using OptimalControl.Common;

namespace OptimalControl.Forms
{
    partial class frmConfig
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btn_OK = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.nud_device_id = new System.Windows.Forms.NumericUpDown();
            this.cb_stopbits = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.cb_databits = new System.Windows.Forms.ComboBox();
            this.cb_baudrate = new System.Windows.Forms.ComboBox();
            this.label_function = new System.Windows.Forms.Label();
            this.label_serialnumber = new System.Windows.Forms.Label();
            this.cb_portname = new System.Windows.Forms.ComboBox();
            this.label19 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.splitContainerH1 = new System.Windows.Forms.SplitContainer();
            this.btn_Curve_Update = new System.Windows.Forms.Button();
            this.btn_Curve_Delete = new System.Windows.Forms.Button();
            this.btn_Curve_Edit = new System.Windows.Forms.Button();
            this.btn_Curve_Add = new System.Windows.Forms.Button();
            this.splitContainerH2 = new System.Windows.Forms.SplitContainer();
            this.dataGridView_Curve = new System.Windows.Forms.DataGridView();
            this.label_Curve_Status = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.tb_TimerInterval = new OptimalControl.Common.NumbericTextbox();
            this.label16 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nud_device_id)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerH1)).BeginInit();
            this.splitContainerH1.Panel1.SuspendLayout();
            this.splitContainerH1.Panel2.SuspendLayout();
            this.splitContainerH1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerH2)).BeginInit();
            this.splitContainerH2.Panel1.SuspendLayout();
            this.splitContainerH2.Panel2.SuspendLayout();
            this.splitContainerH2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Curve)).BeginInit();
            this.tabPage3.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_OK
            // 
            this.btn_OK.Location = new System.Drawing.Point(310, 381);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(87, 27);
            this.btn_OK.TabIndex = 4;
            this.btn_OK.Text = "确定";
            this.btn_OK.UseVisualStyleBackColor = true;
            this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Cancel.Location = new System.Drawing.Point(405, 381);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(87, 27);
            this.btn_Cancel.TabIndex = 4;
            this.btn_Cancel.Text = "取消";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Controls.Add(this.tabPage2);
            this.tabControl.Controls.Add(this.tabPage3);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(478, 360);
            this.tabControl.TabIndex = 5;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(470, 332);
            this.tabPage1.TabIndex = 1;
            this.tabPage1.Text = "通讯设置";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.nud_device_id);
            this.groupBox3.Controls.Add(this.cb_stopbits);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.label15);
            this.groupBox3.Controls.Add(this.cb_databits);
            this.groupBox3.Controls.Add(this.cb_baudrate);
            this.groupBox3.Controls.Add(this.label_function);
            this.groupBox3.Controls.Add(this.label_serialnumber);
            this.groupBox3.Controls.Add(this.cb_portname);
            this.groupBox3.Controls.Add(this.label19);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(3, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(464, 326);
            this.groupBox3.TabIndex = 16;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Modbus通讯参数";
            // 
            // nud_device_id
            // 
            this.nud_device_id.Location = new System.Drawing.Point(120, 148);
            this.nud_device_id.Name = "nud_device_id";
            this.nud_device_id.Size = new System.Drawing.Size(85, 23);
            this.nud_device_id.TabIndex = 41;
            // 
            // cb_stopbits
            // 
            this.cb_stopbits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_stopbits.FormattingEnabled = true;
            this.cb_stopbits.Items.AddRange(new object[] {
            "1",
            "2"});
            this.cb_stopbits.Location = new System.Drawing.Point(310, 97);
            this.cb_stopbits.Name = "cb_stopbits";
            this.cb_stopbits.Size = new System.Drawing.Size(85, 22);
            this.cb_stopbits.TabIndex = 39;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(240, 100);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 14);
            this.label1.TabIndex = 40;
            this.label1.Text = "停止位";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(50, 100);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(49, 14);
            this.label15.TabIndex = 38;
            this.label15.Text = "数据位";
            // 
            // cb_databits
            // 
            this.cb_databits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_databits.FormattingEnabled = true;
            this.cb_databits.Items.AddRange(new object[] {
            "6",
            "7",
            "8"});
            this.cb_databits.Location = new System.Drawing.Point(120, 97);
            this.cb_databits.Name = "cb_databits";
            this.cb_databits.Size = new System.Drawing.Size(85, 22);
            this.cb_databits.Sorted = true;
            this.cb_databits.TabIndex = 37;
            // 
            // cb_baudrate
            // 
            this.cb_baudrate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_baudrate.FormattingEnabled = true;
            this.cb_baudrate.Items.AddRange(new object[] {
            "9600",
            "19200",
            "38400",
            "57600",
            "115200"});
            this.cb_baudrate.Location = new System.Drawing.Point(310, 47);
            this.cb_baudrate.Name = "cb_baudrate";
            this.cb_baudrate.Size = new System.Drawing.Size(85, 22);
            this.cb_baudrate.TabIndex = 35;
            // 
            // label_function
            // 
            this.label_function.AutoSize = true;
            this.label_function.Location = new System.Drawing.Point(240, 50);
            this.label_function.Name = "label_function";
            this.label_function.Size = new System.Drawing.Size(49, 14);
            this.label_function.TabIndex = 36;
            this.label_function.Text = "波特率";
            // 
            // label_serialnumber
            // 
            this.label_serialnumber.AutoSize = true;
            this.label_serialnumber.Location = new System.Drawing.Point(50, 50);
            this.label_serialnumber.Name = "label_serialnumber";
            this.label_serialnumber.Size = new System.Drawing.Size(49, 14);
            this.label_serialnumber.TabIndex = 34;
            this.label_serialnumber.Text = "端口号";
            // 
            // cb_portname
            // 
            this.cb_portname.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_portname.FormattingEnabled = true;
            this.cb_portname.Location = new System.Drawing.Point(120, 47);
            this.cb_portname.Name = "cb_portname";
            this.cb_portname.Size = new System.Drawing.Size(85, 22);
            this.cb_portname.Sorted = true;
            this.cb_portname.TabIndex = 33;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(50, 150);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(49, 14);
            this.label19.TabIndex = 22;
            this.label19.Text = "从站号";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage2.Controls.Add(this.splitContainerH1);
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(470, 332);
            this.tabPage2.TabIndex = 0;
            this.tabPage2.Text = "曲线设置";
            // 
            // splitContainerH1
            // 
            this.splitContainerH1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerH1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainerH1.IsSplitterFixed = true;
            this.splitContainerH1.Location = new System.Drawing.Point(3, 3);
            this.splitContainerH1.Name = "splitContainerH1";
            this.splitContainerH1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerH1.Panel1
            // 
            this.splitContainerH1.Panel1.Controls.Add(this.btn_Curve_Update);
            this.splitContainerH1.Panel1.Controls.Add(this.btn_Curve_Delete);
            this.splitContainerH1.Panel1.Controls.Add(this.btn_Curve_Edit);
            this.splitContainerH1.Panel1.Controls.Add(this.btn_Curve_Add);
            this.splitContainerH1.Panel1MinSize = 40;
            // 
            // splitContainerH1.Panel2
            // 
            this.splitContainerH1.Panel2.Controls.Add(this.splitContainerH2);
            this.splitContainerH1.Size = new System.Drawing.Size(464, 326);
            this.splitContainerH1.SplitterDistance = 40;
            this.splitContainerH1.TabIndex = 0;
            // 
            // btn_Curve_Update
            // 
            this.btn_Curve_Update.Location = new System.Drawing.Point(247, 10);
            this.btn_Curve_Update.Name = "btn_Curve_Update";
            this.btn_Curve_Update.Size = new System.Drawing.Size(75, 23);
            this.btn_Curve_Update.TabIndex = 7;
            this.btn_Curve_Update.Text = "刷新";
            this.btn_Curve_Update.UseVisualStyleBackColor = true;
            this.btn_Curve_Update.Click += new System.EventHandler(this.btn_Curve_Update_Click);
            // 
            // btn_Curve_Delete
            // 
            this.btn_Curve_Delete.Location = new System.Drawing.Point(166, 10);
            this.btn_Curve_Delete.Name = "btn_Curve_Delete";
            this.btn_Curve_Delete.Size = new System.Drawing.Size(75, 23);
            this.btn_Curve_Delete.TabIndex = 6;
            this.btn_Curve_Delete.Text = "删除";
            this.btn_Curve_Delete.UseVisualStyleBackColor = true;
            this.btn_Curve_Delete.Click += new System.EventHandler(this.btn_Curve_Delete_Click);
            // 
            // btn_Curve_Edit
            // 
            this.btn_Curve_Edit.Location = new System.Drawing.Point(85, 10);
            this.btn_Curve_Edit.Name = "btn_Curve_Edit";
            this.btn_Curve_Edit.Size = new System.Drawing.Size(75, 23);
            this.btn_Curve_Edit.TabIndex = 5;
            this.btn_Curve_Edit.Text = "编辑";
            this.btn_Curve_Edit.UseVisualStyleBackColor = true;
            this.btn_Curve_Edit.Click += new System.EventHandler(this.btn_Curve_Edit_Click);
            // 
            // btn_Curve_Add
            // 
            this.btn_Curve_Add.Location = new System.Drawing.Point(4, 10);
            this.btn_Curve_Add.Name = "btn_Curve_Add";
            this.btn_Curve_Add.Size = new System.Drawing.Size(75, 23);
            this.btn_Curve_Add.TabIndex = 4;
            this.btn_Curve_Add.Text = "添加";
            this.btn_Curve_Add.UseVisualStyleBackColor = true;
            this.btn_Curve_Add.Click += new System.EventHandler(this.btn_Curve_Add_Click);
            // 
            // splitContainerH2
            // 
            this.splitContainerH2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerH2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainerH2.IsSplitterFixed = true;
            this.splitContainerH2.Location = new System.Drawing.Point(0, 0);
            this.splitContainerH2.Name = "splitContainerH2";
            this.splitContainerH2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerH2.Panel1
            // 
            this.splitContainerH2.Panel1.Controls.Add(this.dataGridView_Curve);
            // 
            // splitContainerH2.Panel2
            // 
            this.splitContainerH2.Panel2.Controls.Add(this.label_Curve_Status);
            this.splitContainerH2.Size = new System.Drawing.Size(464, 282);
            this.splitContainerH2.SplitterDistance = 252;
            this.splitContainerH2.TabIndex = 1;
            this.splitContainerH2.TabStop = false;
            // 
            // dataGridView_Curve
            // 
            this.dataGridView_Curve.AllowUserToAddRows = false;
            this.dataGridView_Curve.AllowUserToDeleteRows = false;
            this.dataGridView_Curve.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView_Curve.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView_Curve.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView_Curve.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView_Curve.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_Curve.Location = new System.Drawing.Point(0, 0);
            this.dataGridView_Curve.MultiSelect = false;
            this.dataGridView_Curve.Name = "dataGridView_Curve";
            this.dataGridView_Curve.ReadOnly = true;
            this.dataGridView_Curve.RowHeadersVisible = false;
            this.dataGridView_Curve.RowTemplate.Height = 23;
            this.dataGridView_Curve.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView_Curve.Size = new System.Drawing.Size(464, 252);
            this.dataGridView_Curve.TabIndex = 0;
            this.dataGridView_Curve.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_Curve_CellDoubleClick);
            // 
            // label_Curve_Status
            // 
            this.label_Curve_Status.AutoSize = true;
            this.label_Curve_Status.Location = new System.Drawing.Point(5, 6);
            this.label_Curve_Status.Name = "label_Curve_Status";
            this.label_Curve_Status.Size = new System.Drawing.Size(0, 14);
            this.label_Curve_Status.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage3.Controls.Add(this.groupBox6);
            this.tabPage3.Location = new System.Drawing.Point(4, 24);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(470, 332);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "其它设置";
            // 
            // groupBox6
            // 
            this.groupBox6.BackColor = System.Drawing.Color.Transparent;
            this.groupBox6.Controls.Add(this.tb_TimerInterval);
            this.groupBox6.Controls.Add(this.label16);
            this.groupBox6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox6.Location = new System.Drawing.Point(3, 3);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(464, 326);
            this.groupBox6.TabIndex = 37;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "其它设置";
            // 
            // tb_TimerInterval
            // 
            this.tb_TimerInterval.Location = new System.Drawing.Point(124, 51);
            this.tb_TimerInterval.Name = "tb_TimerInterval";
            this.tb_TimerInterval.Size = new System.Drawing.Size(50, 23);
            this.tb_TimerInterval.TabIndex = 27;
            this.tb_TimerInterval.Text = "2000";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(33, 54);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(259, 14);
            this.label16.TabIndex = 26;
            this.label16.Text = "数据更新间隔        毫秒 (≥500毫秒)";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tabControl);
            this.panel1.Location = new System.Drawing.Point(14, 14);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(478, 360);
            this.panel1.TabIndex = 6;
            // 
            // frmConfig
            // 
            this.AcceptButton = this.btn_OK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_Cancel;
            this.ClientSize = new System.Drawing.Size(514, 422);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_OK);
            this.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(520, 450);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(520, 450);
            this.Name = "frmConfig";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = " ";
            this.tabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nud_device_id)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.splitContainerH1.Panel1.ResumeLayout(false);
            this.splitContainerH1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerH1)).EndInit();
            this.splitContainerH1.ResumeLayout(false);
            this.splitContainerH2.Panel1.ResumeLayout(false);
            this.splitContainerH2.Panel2.ResumeLayout(false);
            this.splitContainerH2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerH2)).EndInit();
            this.splitContainerH2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Curve)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_OK;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label19;
        private NumbericTextbox tb_TimerInterval;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.ComboBox cb_stopbits;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ComboBox cb_databits;
        private System.Windows.Forms.ComboBox cb_baudrate;
        private System.Windows.Forms.Label label_function;
        private System.Windows.Forms.Label label_serialnumber;
        private System.Windows.Forms.ComboBox cb_portname;
        private System.Windows.Forms.NumericUpDown nud_device_id;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.SplitContainer splitContainerH1;
        private System.Windows.Forms.Button btn_Curve_Update;
        private System.Windows.Forms.Button btn_Curve_Delete;
        private System.Windows.Forms.Button btn_Curve_Edit;
        private System.Windows.Forms.Button btn_Curve_Add;
        private System.Windows.Forms.SplitContainer splitContainerH2;
        private System.Windows.Forms.DataGridView dataGridView_Curve;
        private System.Windows.Forms.Label label_Curve_Status;
    }
}