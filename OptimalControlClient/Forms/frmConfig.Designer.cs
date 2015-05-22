using Utility;

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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btn_OK = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage_comm = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cb_heartbeat = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cb_control = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
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
            this.tabPage_curve = new System.Windows.Forms.TabPage();
            this.splitContainerH1 = new System.Windows.Forms.SplitContainer();
            this.btn_Curve_Update = new System.Windows.Forms.Button();
            this.btn_Curve_Delete = new System.Windows.Forms.Button();
            this.btn_Curve_Edit = new System.Windows.Forms.Button();
            this.btn_Curve_Add = new System.Windows.Forms.Button();
            this.splitContainerH2 = new System.Windows.Forms.SplitContainer();
            this.dataGridView_Curve = new System.Windows.Forms.DataGridView();
            this.label_Curve_Status = new System.Windows.Forms.Label();
            this.tabPage_textbox = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btn_Textbox_Update = new System.Windows.Forms.Button();
            this.btn_Textbox_Delete = new System.Windows.Forms.Button();
            this.btn_Textbox_Edit = new System.Windows.Forms.Button();
            this.btn_Textbox_Add = new System.Windows.Forms.Button();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.dataGridView_Textbox = new System.Windows.Forms.DataGridView();
            this.label5 = new System.Windows.Forms.Label();
            this.tabPage_other = new System.Windows.Forms.TabPage();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.cb_feed = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tb_Realtime = new Utility.NumbericTextbox();
            this.label4 = new System.Windows.Forms.Label();
            this.tb_UpdateVariableTime = new Utility.NumbericTextbox();
            this.label16 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cb_feedWater = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cb_supWater = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cb_oc_feed = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cb_oc_feedWater = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cb_oc_supWater = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.tabControl.SuspendLayout();
            this.tabPage_comm.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nud_device_id)).BeginInit();
            this.tabPage_curve.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerH1)).BeginInit();
            this.splitContainerH1.Panel1.SuspendLayout();
            this.splitContainerH1.Panel2.SuspendLayout();
            this.splitContainerH1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerH2)).BeginInit();
            this.splitContainerH2.Panel1.SuspendLayout();
            this.splitContainerH2.Panel2.SuspendLayout();
            this.splitContainerH2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Curve)).BeginInit();
            this.tabPage_textbox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Textbox)).BeginInit();
            this.tabPage_other.SuspendLayout();
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
            this.tabControl.Controls.Add(this.tabPage_comm);
            this.tabControl.Controls.Add(this.tabPage_curve);
            this.tabControl.Controls.Add(this.tabPage_textbox);
            this.tabControl.Controls.Add(this.tabPage_other);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(478, 360);
            this.tabControl.TabIndex = 5;
            // 
            // tabPage_comm
            // 
            this.tabPage_comm.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage_comm.Controls.Add(this.groupBox3);
            this.tabPage_comm.Location = new System.Drawing.Point(4, 24);
            this.tabPage_comm.Name = "tabPage_comm";
            this.tabPage_comm.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_comm.Size = new System.Drawing.Size(470, 332);
            this.tabPage_comm.TabIndex = 1;
            this.tabPage_comm.Text = "通讯设置";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cb_heartbeat);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.cb_control);
            this.groupBox3.Controls.Add(this.label2);
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
            // cb_heartbeat
            // 
            this.cb_heartbeat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_heartbeat.FormattingEnabled = true;
            this.cb_heartbeat.Location = new System.Drawing.Point(120, 247);
            this.cb_heartbeat.Name = "cb_heartbeat";
            this.cb_heartbeat.Size = new System.Drawing.Size(275, 22);
            this.cb_heartbeat.TabIndex = 44;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(50, 250);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 14);
            this.label3.TabIndex = 45;
            this.label3.Text = "心跳位";
            // 
            // cb_control
            // 
            this.cb_control.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_control.FormattingEnabled = true;
            this.cb_control.Location = new System.Drawing.Point(120, 197);
            this.cb_control.Name = "cb_control";
            this.cb_control.Size = new System.Drawing.Size(275, 22);
            this.cb_control.TabIndex = 42;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(50, 200);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 14);
            this.label2.TabIndex = 43;
            this.label2.Text = "控制位";
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
            // tabPage_curve
            // 
            this.tabPage_curve.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage_curve.Controls.Add(this.splitContainerH1);
            this.tabPage_curve.Location = new System.Drawing.Point(4, 24);
            this.tabPage_curve.Name = "tabPage_curve";
            this.tabPage_curve.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_curve.Size = new System.Drawing.Size(470, 332);
            this.tabPage_curve.TabIndex = 0;
            this.tabPage_curve.Text = "曲线设置";
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
            // tabPage_textbox
            // 
            this.tabPage_textbox.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage_textbox.Controls.Add(this.splitContainer1);
            this.tabPage_textbox.Location = new System.Drawing.Point(4, 24);
            this.tabPage_textbox.Name = "tabPage_textbox";
            this.tabPage_textbox.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_textbox.Size = new System.Drawing.Size(470, 332);
            this.tabPage_textbox.TabIndex = 4;
            this.tabPage_textbox.Text = "显示变量设置";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.btn_Textbox_Update);
            this.splitContainer1.Panel1.Controls.Add(this.btn_Textbox_Delete);
            this.splitContainer1.Panel1.Controls.Add(this.btn_Textbox_Edit);
            this.splitContainer1.Panel1.Controls.Add(this.btn_Textbox_Add);
            this.splitContainer1.Panel1MinSize = 40;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(464, 326);
            this.splitContainer1.SplitterDistance = 40;
            this.splitContainer1.TabIndex = 0;
            // 
            // btn_Textbox_Update
            // 
            this.btn_Textbox_Update.Location = new System.Drawing.Point(247, 10);
            this.btn_Textbox_Update.Name = "btn_Textbox_Update";
            this.btn_Textbox_Update.Size = new System.Drawing.Size(75, 23);
            this.btn_Textbox_Update.TabIndex = 7;
            this.btn_Textbox_Update.Text = "刷新";
            this.btn_Textbox_Update.UseVisualStyleBackColor = true;
            // 
            // btn_Textbox_Delete
            // 
            this.btn_Textbox_Delete.Location = new System.Drawing.Point(166, 10);
            this.btn_Textbox_Delete.Name = "btn_Textbox_Delete";
            this.btn_Textbox_Delete.Size = new System.Drawing.Size(75, 23);
            this.btn_Textbox_Delete.TabIndex = 6;
            this.btn_Textbox_Delete.Text = "删除";
            this.btn_Textbox_Delete.UseVisualStyleBackColor = true;
            // 
            // btn_Textbox_Edit
            // 
            this.btn_Textbox_Edit.Location = new System.Drawing.Point(85, 10);
            this.btn_Textbox_Edit.Name = "btn_Textbox_Edit";
            this.btn_Textbox_Edit.Size = new System.Drawing.Size(75, 23);
            this.btn_Textbox_Edit.TabIndex = 5;
            this.btn_Textbox_Edit.Text = "编辑";
            this.btn_Textbox_Edit.UseVisualStyleBackColor = true;
            // 
            // btn_Textbox_Add
            // 
            this.btn_Textbox_Add.Location = new System.Drawing.Point(4, 10);
            this.btn_Textbox_Add.Name = "btn_Textbox_Add";
            this.btn_Textbox_Add.Size = new System.Drawing.Size(75, 23);
            this.btn_Textbox_Add.TabIndex = 4;
            this.btn_Textbox_Add.Text = "添加";
            this.btn_Textbox_Add.UseVisualStyleBackColor = true;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer2.IsSplitterFixed = true;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.dataGridView_Textbox);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.label5);
            this.splitContainer2.Size = new System.Drawing.Size(464, 282);
            this.splitContainer2.SplitterDistance = 252;
            this.splitContainer2.TabIndex = 1;
            this.splitContainer2.TabStop = false;
            // 
            // dataGridView_Textbox
            // 
            this.dataGridView_Textbox.AllowUserToAddRows = false;
            this.dataGridView_Textbox.AllowUserToDeleteRows = false;
            this.dataGridView_Textbox.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView_Textbox.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView_Textbox.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView_Textbox.DefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridView_Textbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_Textbox.Location = new System.Drawing.Point(0, 0);
            this.dataGridView_Textbox.MultiSelect = false;
            this.dataGridView_Textbox.Name = "dataGridView_Textbox";
            this.dataGridView_Textbox.ReadOnly = true;
            this.dataGridView_Textbox.RowHeadersVisible = false;
            this.dataGridView_Textbox.RowTemplate.Height = 23;
            this.dataGridView_Textbox.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView_Textbox.Size = new System.Drawing.Size(464, 252);
            this.dataGridView_Textbox.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(5, 6);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(0, 14);
            this.label5.TabIndex = 0;
            // 
            // tabPage_other
            // 
            this.tabPage_other.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage_other.Controls.Add(this.groupBox6);
            this.tabPage_other.Location = new System.Drawing.Point(4, 24);
            this.tabPage_other.Name = "tabPage_other";
            this.tabPage_other.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_other.Size = new System.Drawing.Size(470, 332);
            this.tabPage_other.TabIndex = 2;
            this.tabPage_other.Text = "其它设置";
            // 
            // groupBox6
            // 
            this.groupBox6.BackColor = System.Drawing.Color.Transparent;
            this.groupBox6.Controls.Add(this.cb_oc_supWater);
            this.groupBox6.Controls.Add(this.label11);
            this.groupBox6.Controls.Add(this.cb_oc_feedWater);
            this.groupBox6.Controls.Add(this.label10);
            this.groupBox6.Controls.Add(this.cb_oc_feed);
            this.groupBox6.Controls.Add(this.label9);
            this.groupBox6.Controls.Add(this.cb_supWater);
            this.groupBox6.Controls.Add(this.label8);
            this.groupBox6.Controls.Add(this.cb_feedWater);
            this.groupBox6.Controls.Add(this.label7);
            this.groupBox6.Controls.Add(this.cb_feed);
            this.groupBox6.Controls.Add(this.label6);
            this.groupBox6.Controls.Add(this.tb_Realtime);
            this.groupBox6.Controls.Add(this.label4);
            this.groupBox6.Controls.Add(this.tb_UpdateVariableTime);
            this.groupBox6.Controls.Add(this.label16);
            this.groupBox6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox6.Location = new System.Drawing.Point(3, 3);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(464, 326);
            this.groupBox6.TabIndex = 37;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "其它设置";
            // 
            // cb_feed
            // 
            this.cb_feed.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_feed.FormattingEnabled = true;
            this.cb_feed.Location = new System.Drawing.Point(131, 107);
            this.cb_feed.Name = "cb_feed";
            this.cb_feed.Size = new System.Drawing.Size(239, 22);
            this.cb_feed.TabIndex = 46;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(40, 110);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(91, 14);
            this.label6.TabIndex = 47;
            this.label6.Text = "给矿量设定值";
            // 
            // tb_Realtime
            // 
            this.tb_Realtime.Location = new System.Drawing.Point(131, 67);
            this.tb_Realtime.Name = "tb_Realtime";
            this.tb_Realtime.Size = new System.Drawing.Size(50, 23);
            this.tb_Realtime.TabIndex = 29;
            this.tb_Realtime.Text = "2000";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(40, 70);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(259, 14);
            this.label4.TabIndex = 28;
            this.label4.Text = "规则扫描间隔        毫秒 (≥500毫秒)";
            // 
            // tb_UpdateVariableTime
            // 
            this.tb_UpdateVariableTime.Location = new System.Drawing.Point(131, 32);
            this.tb_UpdateVariableTime.Name = "tb_UpdateVariableTime";
            this.tb_UpdateVariableTime.Size = new System.Drawing.Size(50, 23);
            this.tb_UpdateVariableTime.TabIndex = 27;
            this.tb_UpdateVariableTime.Text = "10000";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(40, 35);
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
            // cb_feedWater
            // 
            this.cb_feedWater.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_feedWater.FormattingEnabled = true;
            this.cb_feedWater.Location = new System.Drawing.Point(131, 182);
            this.cb_feedWater.Name = "cb_feedWater";
            this.cb_feedWater.Size = new System.Drawing.Size(239, 22);
            this.cb_feedWater.TabIndex = 48;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(40, 185);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(91, 14);
            this.label7.TabIndex = 49;
            this.label7.Text = "前给水设定值";
            // 
            // cb_supWater
            // 
            this.cb_supWater.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_supWater.FormattingEnabled = true;
            this.cb_supWater.Location = new System.Drawing.Point(131, 257);
            this.cb_supWater.Name = "cb_supWater";
            this.cb_supWater.Size = new System.Drawing.Size(239, 22);
            this.cb_supWater.TabIndex = 50;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(40, 260);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(91, 14);
            this.label8.TabIndex = 51;
            this.label8.Text = "补加水设定值";
            // 
            // cb_oc_feed
            // 
            this.cb_oc_feed.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_oc_feed.FormattingEnabled = true;
            this.cb_oc_feed.Location = new System.Drawing.Point(131, 142);
            this.cb_oc_feed.Name = "cb_oc_feed";
            this.cb_oc_feed.Size = new System.Drawing.Size(239, 22);
            this.cb_oc_feed.TabIndex = 52;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(40, 145);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(91, 14);
            this.label9.TabIndex = 53;
            this.label9.Text = "给矿量优化值";
            // 
            // cb_oc_feedWater
            // 
            this.cb_oc_feedWater.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_oc_feedWater.FormattingEnabled = true;
            this.cb_oc_feedWater.Location = new System.Drawing.Point(131, 217);
            this.cb_oc_feedWater.Name = "cb_oc_feedWater";
            this.cb_oc_feedWater.Size = new System.Drawing.Size(239, 22);
            this.cb_oc_feedWater.TabIndex = 54;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(40, 220);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(91, 14);
            this.label10.TabIndex = 55;
            this.label10.Text = "前给水优化值";
            // 
            // cb_oc_supWater
            // 
            this.cb_oc_supWater.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_oc_supWater.FormattingEnabled = true;
            this.cb_oc_supWater.Location = new System.Drawing.Point(131, 292);
            this.cb_oc_supWater.Name = "cb_oc_supWater";
            this.cb_oc_supWater.Size = new System.Drawing.Size(239, 22);
            this.cb_oc_supWater.TabIndex = 56;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(40, 295);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(91, 14);
            this.label11.TabIndex = 57;
            this.label11.Text = "补加水优化值";
            // 
            // frmConfig
            // 
            this.AcceptButton = this.btn_OK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_Cancel;
            this.ClientSize = new System.Drawing.Size(504, 414);
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
            this.tabPage_comm.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nud_device_id)).EndInit();
            this.tabPage_curve.ResumeLayout(false);
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
            this.tabPage_textbox.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Textbox)).EndInit();
            this.tabPage_other.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_OK;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPage_comm;
        private System.Windows.Forms.TabPage tabPage_other;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label19;
        private NumbericTextbox tb_UpdateVariableTime;
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
        private System.Windows.Forms.TabPage tabPage_curve;
        private System.Windows.Forms.SplitContainer splitContainerH1;
        private System.Windows.Forms.Button btn_Curve_Update;
        private System.Windows.Forms.Button btn_Curve_Delete;
        private System.Windows.Forms.Button btn_Curve_Edit;
        private System.Windows.Forms.Button btn_Curve_Add;
        private System.Windows.Forms.SplitContainer splitContainerH2;
        private System.Windows.Forms.DataGridView dataGridView_Curve;
        private System.Windows.Forms.Label label_Curve_Status;
        private System.Windows.Forms.ComboBox cb_control;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cb_heartbeat;
        private System.Windows.Forms.Label label3;
        private NumbericTextbox tb_Realtime;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TabPage tabPage_textbox;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btn_Textbox_Update;
        private System.Windows.Forms.Button btn_Textbox_Delete;
        private System.Windows.Forms.Button btn_Textbox_Edit;
        private System.Windows.Forms.Button btn_Textbox_Add;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.DataGridView dataGridView_Textbox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cb_feed;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cb_supWater;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cb_feedWater;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cb_oc_supWater;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cb_oc_feedWater;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cb_oc_feed;
        private System.Windows.Forms.Label label9;
    }
}