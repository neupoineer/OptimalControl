using Utility;

namespace OptimalControl.Forms
{
    partial class frmMain
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.statusStrip_main = new System.Windows.Forms.StatusStrip();
            this.status_Label = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip_main = new System.Windows.Forms.ToolStrip();
            this.btn_run = new System.Windows.Forms.ToolStripButton();
            this.btn_stop = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btn_curve_realtime = new System.Windows.Forms.ToolStripButton();
            this.btn_curve_stop = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.btn_config = new System.Windows.Forms.ToolStripButton();
            this.btn_info = new System.Windows.Forms.ToolStripButton();
            this.btn_quit = new System.Windows.Forms.ToolStripButton();
            this.ofd_history = new System.Windows.Forms.OpenFileDialog();
            this.msMain = new System.Windows.Forms.MenuStrip();
            this.menu_file = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_file_login = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_file_logoff = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.menu_file_lockscreen = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_file_quit = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_control = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_control_run = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_control_stop = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_control_clear = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_config = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_config_config = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menu_config_user = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_config_password = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.menu_config_devices = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_config_parameters = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_help = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_help_about = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox_main = new System.Windows.Forms.PictureBox();
            this.splitContainerH1_2H2 = new System.Windows.Forms.SplitContainer();
            this.splitContainerH1_2H2_1V1 = new System.Windows.Forms.SplitContainer();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage_optimal_control = new System.Windows.Forms.TabPage();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgv_oc_rules = new System.Windows.Forms.DataGridView();
            this.toolStrip_oc = new System.Windows.Forms.ToolStrip();
            this.tsbtn_oc_enabled = new System.Windows.Forms.ToolStripButton();
            this.tsbtn_oc_disabled = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtn_rule_add = new System.Windows.Forms.ToolStripButton();
            this.tsbtn_rule_edit = new System.Windows.Forms.ToolStripButton();
            this.tsbtn_rule_delete = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtn_rule_update = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtn_rule_paras = new System.Windows.Forms.ToolStripButton();
            this.tabPage_realtime_data = new System.Windows.Forms.TabPage();
            this.splitContainerH1_2H2_1V1_2V2 = new System.Windows.Forms.SplitContainer();
            this.listview_parainfo = new Utility.DoubleBufferListView();
            this.splitContainerH1_2H2_1V1_2V2_2V3 = new System.Windows.Forms.SplitContainer();
            this.zgc_realtime = new ZedGraph.ZedGraphControl();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label_info1_title = new System.Windows.Forms.Label();
            this.tabPage_history_curve = new System.Windows.Forms.TabPage();
            this.splitContainer_child = new System.Windows.Forms.SplitContainer();
            this.btn_curve_next = new System.Windows.Forms.Button();
            this.btn_curve_prev = new System.Windows.Forms.Button();
            this.btn_curve_search = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.dtp_curve_start = new System.Windows.Forms.DateTimePicker();
            this.dtp_curve_end = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.zgc_history = new ZedGraph.ZedGraphControl();
            this.tabPage_history_data = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btn_data_next = new System.Windows.Forms.Button();
            this.btn_data_prev = new System.Windows.Forms.Button();
            this.btn_data_search = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.dtp_data_start = new System.Windows.Forms.DateTimePicker();
            this.dtp_data_end = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.dgv_data = new System.Windows.Forms.DataGridView();
            this.splitContainerH1 = new System.Windows.Forms.SplitContainer();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dgv_oc_logs = new System.Windows.Forms.DataGridView();
            this.statusStrip_main.SuspendLayout();
            this.toolStrip_main.SuspendLayout();
            this.msMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_main)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerH1_2H2)).BeginInit();
            this.splitContainerH1_2H2.Panel1.SuspendLayout();
            this.splitContainerH1_2H2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerH1_2H2_1V1)).BeginInit();
            this.splitContainerH1_2H2_1V1.Panel2.SuspendLayout();
            this.splitContainerH1_2H2_1V1.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabPage_optimal_control.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_oc_rules)).BeginInit();
            this.toolStrip_oc.SuspendLayout();
            this.tabPage_realtime_data.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerH1_2H2_1V1_2V2)).BeginInit();
            this.splitContainerH1_2H2_1V1_2V2.Panel1.SuspendLayout();
            this.splitContainerH1_2H2_1V1_2V2.Panel2.SuspendLayout();
            this.splitContainerH1_2H2_1V1_2V2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerH1_2H2_1V1_2V2_2V3)).BeginInit();
            this.splitContainerH1_2H2_1V1_2V2_2V3.Panel1.SuspendLayout();
            this.splitContainerH1_2H2_1V1_2V2_2V3.Panel2.SuspendLayout();
            this.splitContainerH1_2H2_1V1_2V2_2V3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.tabPage_history_curve.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_child)).BeginInit();
            this.splitContainer_child.Panel1.SuspendLayout();
            this.splitContainer_child.Panel2.SuspendLayout();
            this.splitContainer_child.SuspendLayout();
            this.tabPage_history_data.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_data)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerH1)).BeginInit();
            this.splitContainerH1.Panel2.SuspendLayout();
            this.splitContainerH1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_oc_logs)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip_main
            // 
            this.statusStrip_main.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.status_Label});
            this.statusStrip_main.Location = new System.Drawing.Point(0, 710);
            this.statusStrip_main.Name = "statusStrip_main";
            this.statusStrip_main.Size = new System.Drawing.Size(1008, 22);
            this.statusStrip_main.TabIndex = 2;
            this.statusStrip_main.Text = "statusStrip1";
            // 
            // status_Label
            // 
            this.status_Label.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.status_Label.Name = "status_Label";
            this.status_Label.Size = new System.Drawing.Size(41, 17);
            this.status_Label.Text = "状态栏";
            // 
            // toolStrip_main
            // 
            this.toolStrip_main.BackColor = System.Drawing.SystemColors.Control;
            this.toolStrip_main.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.toolStrip_main.ImageScalingSize = new System.Drawing.Size(36, 36);
            this.toolStrip_main.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btn_run,
            this.btn_stop,
            this.toolStripSeparator4,
            this.btn_curve_realtime,
            this.btn_curve_stop,
            this.toolStripSeparator5,
            this.btn_config,
            this.btn_info,
            this.btn_quit});
            this.toolStrip_main.Location = new System.Drawing.Point(0, 31);
            this.toolStrip_main.Name = "toolStrip_main";
            this.toolStrip_main.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolStrip_main.Size = new System.Drawing.Size(1008, 44);
            this.toolStrip_main.TabIndex = 15;
            // 
            // btn_run
            // 
            this.btn_run.AutoSize = false;
            this.btn_run.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btn_run.Image = ((System.Drawing.Image)(resources.GetObject("btn_run.Image")));
            this.btn_run.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_run.Margin = new System.Windows.Forms.Padding(5, 2, 5, 2);
            this.btn_run.Name = "btn_run";
            this.btn_run.Size = new System.Drawing.Size(40, 40);
            this.btn_run.Text = "实时显示";
            this.btn_run.Click += new System.EventHandler(this.btn_run_Click);
            // 
            // btn_stop
            // 
            this.btn_stop.AutoSize = false;
            this.btn_stop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btn_stop.Enabled = false;
            this.btn_stop.Image = ((System.Drawing.Image)(resources.GetObject("btn_stop.Image")));
            this.btn_stop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_stop.Margin = new System.Windows.Forms.Padding(5, 2, 5, 2);
            this.btn_stop.Name = "btn_stop";
            this.btn_stop.Size = new System.Drawing.Size(40, 40);
            this.btn_stop.Text = "停止显示";
            this.btn_stop.Click += new System.EventHandler(this.btn_stop_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 44);
            // 
            // btn_curve_realtime
            // 
            this.btn_curve_realtime.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btn_curve_realtime.Image = ((System.Drawing.Image)(resources.GetObject("btn_curve_realtime.Image")));
            this.btn_curve_realtime.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_curve_realtime.Margin = new System.Windows.Forms.Padding(5, 2, 5, 2);
            this.btn_curve_realtime.Name = "btn_curve_realtime";
            this.btn_curve_realtime.Size = new System.Drawing.Size(40, 40);
            this.btn_curve_realtime.Text = "toolStripButton2";
            this.btn_curve_realtime.Click += new System.EventHandler(this.btn_curve_realtime_Click);
            // 
            // btn_curve_stop
            // 
            this.btn_curve_stop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btn_curve_stop.Image = ((System.Drawing.Image)(resources.GetObject("btn_curve_stop.Image")));
            this.btn_curve_stop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_curve_stop.Margin = new System.Windows.Forms.Padding(5, 2, 5, 2);
            this.btn_curve_stop.Name = "btn_curve_stop";
            this.btn_curve_stop.Size = new System.Drawing.Size(40, 40);
            this.btn_curve_stop.Text = "toolStripButton1";
            this.btn_curve_stop.Click += new System.EventHandler(this.btn_curve_stop_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 44);
            // 
            // btn_config
            // 
            this.btn_config.AutoSize = false;
            this.btn_config.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btn_config.Image = ((System.Drawing.Image)(resources.GetObject("btn_config.Image")));
            this.btn_config.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_config.Margin = new System.Windows.Forms.Padding(5, 2, 5, 2);
            this.btn_config.Name = "btn_config";
            this.btn_config.Size = new System.Drawing.Size(40, 40);
            this.btn_config.Text = "参数设置";
            this.btn_config.Click += new System.EventHandler(this.btn_config_Click);
            // 
            // btn_info
            // 
            this.btn_info.AutoSize = false;
            this.btn_info.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btn_info.Image = ((System.Drawing.Image)(resources.GetObject("btn_info.Image")));
            this.btn_info.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_info.Margin = new System.Windows.Forms.Padding(5, 2, 5, 2);
            this.btn_info.Name = "btn_info";
            this.btn_info.Size = new System.Drawing.Size(40, 40);
            this.btn_info.Text = "关于软件";
            this.btn_info.Click += new System.EventHandler(this.btn_info_Click);
            // 
            // btn_quit
            // 
            this.btn_quit.AutoSize = false;
            this.btn_quit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btn_quit.Image = ((System.Drawing.Image)(resources.GetObject("btn_quit.Image")));
            this.btn_quit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_quit.Margin = new System.Windows.Forms.Padding(5, 2, 5, 2);
            this.btn_quit.Name = "btn_quit";
            this.btn_quit.Size = new System.Drawing.Size(40, 40);
            this.btn_quit.Text = "退出程序";
            this.btn_quit.Click += new System.EventHandler(this.btn_quit_Click);
            // 
            // msMain
            // 
            this.msMain.BackColor = System.Drawing.SystemColors.MenuBar;
            this.msMain.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.msMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            this.msMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_file,
            this.menu_control,
            this.menu_config,
            this.menu_help});
            this.msMain.Location = new System.Drawing.Point(0, 0);
            this.msMain.Name = "msMain";
            this.msMain.Padding = new System.Windows.Forms.Padding(3, 7, 1, 6);
            this.msMain.Size = new System.Drawing.Size(1008, 31);
            this.msMain.TabIndex = 41;
            // 
            // menu_file
            // 
            this.menu_file.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_file_login,
            this.menu_file_logoff,
            this.toolStripSeparator3,
            this.menu_file_lockscreen,
            this.menu_file_quit});
            this.menu_file.Name = "menu_file";
            this.menu_file.ShortcutKeyDisplayString = "";
            this.menu_file.Size = new System.Drawing.Size(68, 18);
            this.menu_file.Text = "文件(&F)";
            // 
            // menu_file_login
            // 
            this.menu_file_login.Image = ((System.Drawing.Image)(resources.GetObject("menu_file_login.Image")));
            this.menu_file_login.Name = "menu_file_login";
            this.menu_file_login.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
            this.menu_file_login.Size = new System.Drawing.Size(172, 22);
            this.menu_file_login.Text = "登录(&I)";
            this.menu_file_login.Click += new System.EventHandler(this.menu_file_login_Click);
            // 
            // menu_file_logoff
            // 
            this.menu_file_logoff.Image = ((System.Drawing.Image)(resources.GetObject("menu_file_logoff.Image")));
            this.menu_file_logoff.Name = "menu_file_logoff";
            this.menu_file_logoff.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.menu_file_logoff.Size = new System.Drawing.Size(172, 22);
            this.menu_file_logoff.Text = "注销(&F)";
            this.menu_file_logoff.Click += new System.EventHandler(this.menu_file_logoff_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(169, 6);
            // 
            // menu_file_lockscreen
            // 
            this.menu_file_lockscreen.Image = ((System.Drawing.Image)(resources.GetObject("menu_file_lockscreen.Image")));
            this.menu_file_lockscreen.Name = "menu_file_lockscreen";
            this.menu_file_lockscreen.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
            this.menu_file_lockscreen.Size = new System.Drawing.Size(172, 22);
            this.menu_file_lockscreen.Text = "锁屏(&L)";
            this.menu_file_lockscreen.Click += new System.EventHandler(this.menu_file_lockscreen_Click);
            // 
            // menu_file_quit
            // 
            this.menu_file_quit.Image = ((System.Drawing.Image)(resources.GetObject("menu_file_quit.Image")));
            this.menu_file_quit.Name = "menu_file_quit";
            this.menu_file_quit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.menu_file_quit.Size = new System.Drawing.Size(172, 22);
            this.menu_file_quit.Text = "退出(&X)";
            this.menu_file_quit.Click += new System.EventHandler(this.menu_file_quit_Click);
            // 
            // menu_control
            // 
            this.menu_control.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_control_run,
            this.menu_control_stop,
            this.menu_control_clear});
            this.menu_control.Name = "menu_control";
            this.menu_control.Size = new System.Drawing.Size(68, 18);
            this.menu_control.Text = "波形(&G)";
            // 
            // menu_control_run
            // 
            this.menu_control_run.Image = ((System.Drawing.Image)(resources.GetObject("menu_control_run.Image")));
            this.menu_control_run.Name = "menu_control_run";
            this.menu_control_run.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.menu_control_run.Size = new System.Drawing.Size(200, 22);
            this.menu_control_run.Text = "实时显示(&R)";
            this.menu_control_run.Click += new System.EventHandler(this.menu_control_run_Click);
            // 
            // menu_control_stop
            // 
            this.menu_control_stop.Enabled = false;
            this.menu_control_stop.Image = ((System.Drawing.Image)(resources.GetObject("menu_control_stop.Image")));
            this.menu_control_stop.Name = "menu_control_stop";
            this.menu_control_stop.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
            this.menu_control_stop.Size = new System.Drawing.Size(200, 22);
            this.menu_control_stop.Text = "停止显示(&T)";
            this.menu_control_stop.Click += new System.EventHandler(this.menu_control_stop_Click);
            // 
            // menu_control_clear
            // 
            this.menu_control_clear.Image = ((System.Drawing.Image)(resources.GetObject("menu_control_clear.Image")));
            this.menu_control_clear.Name = "menu_control_clear";
            this.menu_control_clear.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.menu_control_clear.Size = new System.Drawing.Size(200, 22);
            this.menu_control_clear.Text = "清空波形(&C)";
            this.menu_control_clear.Click += new System.EventHandler(this.menu_control_clear_Click);
            // 
            // menu_config
            // 
            this.menu_config.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_config_config,
            this.toolStripSeparator1,
            this.menu_config_user,
            this.menu_config_password,
            this.toolStripSeparator2,
            this.menu_config_devices,
            this.menu_config_parameters});
            this.menu_config.Name = "menu_config";
            this.menu_config.Size = new System.Drawing.Size(68, 18);
            this.menu_config.Text = "设置(&O)";
            // 
            // menu_config_config
            // 
            this.menu_config_config.Image = ((System.Drawing.Image)(resources.GetObject("menu_config_config.Image")));
            this.menu_config_config.Name = "menu_config_config";
            this.menu_config_config.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.menu_config_config.Size = new System.Drawing.Size(200, 22);
            this.menu_config_config.Text = "参数设置(&S)";
            this.menu_config_config.Click += new System.EventHandler(this.menu_config_config_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(197, 6);
            // 
            // menu_config_user
            // 
            this.menu_config_user.Image = ((System.Drawing.Image)(resources.GetObject("menu_config_user.Image")));
            this.menu_config_user.Name = "menu_config_user";
            this.menu_config_user.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.U)));
            this.menu_config_user.Size = new System.Drawing.Size(200, 22);
            this.menu_config_user.Text = "用户设置(&U)";
            this.menu_config_user.Click += new System.EventHandler(this.menu_config_user_Click);
            // 
            // menu_config_password
            // 
            this.menu_config_password.Image = ((System.Drawing.Image)(resources.GetObject("menu_config_password.Image")));
            this.menu_config_password.Name = "menu_config_password";
            this.menu_config_password.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.menu_config_password.Size = new System.Drawing.Size(200, 22);
            this.menu_config_password.Text = "修改密码(&P)";
            this.menu_config_password.Click += new System.EventHandler(this.menu_config_password_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(197, 6);
            // 
            // menu_config_devices
            // 
            this.menu_config_devices.Image = ((System.Drawing.Image)(resources.GetObject("menu_config_devices.Image")));
            this.menu_config_devices.Name = "menu_config_devices";
            this.menu_config_devices.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            this.menu_config_devices.Size = new System.Drawing.Size(200, 22);
            this.menu_config_devices.Text = "设备管理(&D)";
            this.menu_config_devices.Click += new System.EventHandler(this.menu_config_devices_Click);
            // 
            // menu_config_parameters
            // 
            this.menu_config_parameters.Image = ((System.Drawing.Image)(resources.GetObject("menu_config_parameters.Image")));
            this.menu_config_parameters.Name = "menu_config_parameters";
            this.menu_config_parameters.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.menu_config_parameters.Size = new System.Drawing.Size(200, 22);
            this.menu_config_parameters.Text = "变量管理(&V)";
            this.menu_config_parameters.Click += new System.EventHandler(this.menu_config_parameters_Click);
            // 
            // menu_help
            // 
            this.menu_help.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_help_about});
            this.menu_help.Name = "menu_help";
            this.menu_help.Size = new System.Drawing.Size(68, 18);
            this.menu_help.Text = "帮助(&H)";
            // 
            // menu_help_about
            // 
            this.menu_help_about.Image = ((System.Drawing.Image)(resources.GetObject("menu_help_about.Image")));
            this.menu_help_about.Name = "menu_help_about";
            this.menu_help_about.Size = new System.Drawing.Size(151, 22);
            this.menu_help_about.Text = "关于软件(&A)";
            this.menu_help_about.Click += new System.EventHandler(this.menu_help_about_Click);
            // 
            // pictureBox_main
            // 
            this.pictureBox_main.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox_main.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox_main.Image")));
            this.pictureBox_main.Location = new System.Drawing.Point(946, 12);
            this.pictureBox_main.Name = "pictureBox_main";
            this.pictureBox_main.Size = new System.Drawing.Size(50, 50);
            this.pictureBox_main.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_main.TabIndex = 16;
            this.pictureBox_main.TabStop = false;
            // 
            // splitContainerH1_2H2
            // 
            this.splitContainerH1_2H2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerH1_2H2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainerH1_2H2.IsSplitterFixed = true;
            this.splitContainerH1_2H2.Location = new System.Drawing.Point(0, 0);
            this.splitContainerH1_2H2.Name = "splitContainerH1_2H2";
            this.splitContainerH1_2H2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerH1_2H2.Panel1
            // 
            this.splitContainerH1_2H2.Panel1.Controls.Add(this.splitContainerH1_2H2_1V1);
            this.splitContainerH1_2H2.Panel2MinSize = 0;
            this.splitContainerH1_2H2.Size = new System.Drawing.Size(1008, 619);
            this.splitContainerH1_2H2.SplitterDistance = 600;
            this.splitContainerH1_2H2.SplitterWidth = 1;
            this.splitContainerH1_2H2.TabIndex = 0;
            // 
            // splitContainerH1_2H2_1V1
            // 
            this.splitContainerH1_2H2_1V1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerH1_2H2_1V1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainerH1_2H2_1V1.IsSplitterFixed = true;
            this.splitContainerH1_2H2_1V1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerH1_2H2_1V1.Name = "splitContainerH1_2H2_1V1";
            this.splitContainerH1_2H2_1V1.Panel1MinSize = 0;
            // 
            // splitContainerH1_2H2_1V1.Panel2
            // 
            this.splitContainerH1_2H2_1V1.Panel2.Controls.Add(this.tabControl);
            this.splitContainerH1_2H2_1V1.Size = new System.Drawing.Size(1008, 600);
            this.splitContainerH1_2H2_1V1.SplitterDistance = 30;
            this.splitContainerH1_2H2_1V1.SplitterWidth = 1;
            this.splitContainerH1_2H2_1V1.TabIndex = 44;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPage_optimal_control);
            this.tabControl.Controls.Add(this.tabPage_realtime_data);
            this.tabControl.Controls.Add(this.tabPage_history_curve);
            this.tabControl.Controls.Add(this.tabPage_history_data);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(977, 600);
            this.tabControl.TabIndex = 0;
            // 
            // tabPage_optimal_control
            // 
            this.tabPage_optimal_control.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage_optimal_control.Controls.Add(this.splitContainer2);
            this.tabPage_optimal_control.Location = new System.Drawing.Point(4, 22);
            this.tabPage_optimal_control.Name = "tabPage_optimal_control";
            this.tabPage_optimal_control.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_optimal_control.Size = new System.Drawing.Size(969, 574);
            this.tabPage_optimal_control.TabIndex = 3;
            this.tabPage_optimal_control.Text = "优化控制";
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(3, 3);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.splitContainer3);
            this.splitContainer2.Panel1.Controls.Add(this.toolStrip_oc);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.groupBox3);
            this.splitContainer2.Size = new System.Drawing.Size(963, 568);
            this.splitContainer2.SplitterDistance = 365;
            this.splitContainer2.TabIndex = 2;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 38);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer3.Size = new System.Drawing.Size(963, 327);
            this.splitContainer3.SplitterDistance = 343;
            this.splitContainer3.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(343, 327);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "磨矿过程变量";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgv_oc_rules);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(616, 327);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "专家规则列表";
            // 
            // dgv_oc_rules
            // 
            this.dgv_oc_rules.AllowUserToAddRows = false;
            this.dgv_oc_rules.AllowUserToDeleteRows = false;
            this.dgv_oc_rules.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_oc_rules.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_oc_rules.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_oc_rules.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_oc_rules.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_oc_rules.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_oc_rules.Location = new System.Drawing.Point(3, 17);
            this.dgv_oc_rules.MultiSelect = false;
            this.dgv_oc_rules.Name = "dgv_oc_rules";
            this.dgv_oc_rules.ReadOnly = true;
            this.dgv_oc_rules.RowHeadersVisible = false;
            this.dgv_oc_rules.RowTemplate.Height = 23;
            this.dgv_oc_rules.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_oc_rules.ShowEditingIcon = false;
            this.dgv_oc_rules.Size = new System.Drawing.Size(610, 307);
            this.dgv_oc_rules.TabIndex = 1;
            this.dgv_oc_rules.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_oc_rules_CellDoubleClick);
            // 
            // toolStrip_oc
            // 
            this.toolStrip_oc.AutoSize = false;
            this.toolStrip_oc.ImageScalingSize = new System.Drawing.Size(25, 25);
            this.toolStrip_oc.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtn_oc_enabled,
            this.tsbtn_oc_disabled,
            this.toolStripSeparator7,
            this.tsbtn_rule_add,
            this.tsbtn_rule_edit,
            this.tsbtn_rule_delete,
            this.toolStripButton1,
            this.tsbtn_rule_update,
            this.toolStripSeparator6,
            this.tsbtn_rule_paras});
            this.toolStrip_oc.Location = new System.Drawing.Point(0, 0);
            this.toolStrip_oc.Name = "toolStrip_oc";
            this.toolStrip_oc.Size = new System.Drawing.Size(963, 38);
            this.toolStrip_oc.TabIndex = 4;
            this.toolStrip_oc.Text = "toolStrip1";
            // 
            // tsbtn_oc_enabled
            // 
            this.tsbtn_oc_enabled.Image = ((System.Drawing.Image)(resources.GetObject("tsbtn_oc_enabled.Image")));
            this.tsbtn_oc_enabled.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtn_oc_enabled.Name = "tsbtn_oc_enabled";
            this.tsbtn_oc_enabled.Size = new System.Drawing.Size(60, 35);
            this.tsbtn_oc_enabled.Text = "运行";
            this.tsbtn_oc_enabled.Click += new System.EventHandler(this.tsbtn_oc_enabled_Click);
            // 
            // tsbtn_oc_disabled
            // 
            this.tsbtn_oc_disabled.Image = ((System.Drawing.Image)(resources.GetObject("tsbtn_oc_disabled.Image")));
            this.tsbtn_oc_disabled.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtn_oc_disabled.Name = "tsbtn_oc_disabled";
            this.tsbtn_oc_disabled.Size = new System.Drawing.Size(60, 35);
            this.tsbtn_oc_disabled.Text = "停止";
            this.tsbtn_oc_disabled.Click += new System.EventHandler(this.tsbtn_oc_disabled_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 38);
            // 
            // tsbtn_rule_add
            // 
            this.tsbtn_rule_add.Image = ((System.Drawing.Image)(resources.GetObject("tsbtn_rule_add.Image")));
            this.tsbtn_rule_add.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtn_rule_add.Name = "tsbtn_rule_add";
            this.tsbtn_rule_add.Size = new System.Drawing.Size(84, 35);
            this.tsbtn_rule_add.Text = "增加规则";
            this.tsbtn_rule_add.ToolTipText = "增加规则";
            this.tsbtn_rule_add.Click += new System.EventHandler(this.tsbtn_rule_add_Click);
            // 
            // tsbtn_rule_edit
            // 
            this.tsbtn_rule_edit.Image = ((System.Drawing.Image)(resources.GetObject("tsbtn_rule_edit.Image")));
            this.tsbtn_rule_edit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtn_rule_edit.Name = "tsbtn_rule_edit";
            this.tsbtn_rule_edit.Size = new System.Drawing.Size(84, 35);
            this.tsbtn_rule_edit.Text = "编辑规则";
            this.tsbtn_rule_edit.Click += new System.EventHandler(this.tsbtn_rule_edit_Click);
            // 
            // tsbtn_rule_delete
            // 
            this.tsbtn_rule_delete.Image = ((System.Drawing.Image)(resources.GetObject("tsbtn_rule_delete.Image")));
            this.tsbtn_rule_delete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtn_rule_delete.Name = "tsbtn_rule_delete";
            this.tsbtn_rule_delete.Size = new System.Drawing.Size(84, 35);
            this.tsbtn_rule_delete.Text = "删除规则";
            this.tsbtn_rule_delete.Click += new System.EventHandler(this.tsbtn_rule_delete_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(6, 38);
            // 
            // tsbtn_rule_update
            // 
            this.tsbtn_rule_update.Image = ((System.Drawing.Image)(resources.GetObject("tsbtn_rule_update.Image")));
            this.tsbtn_rule_update.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtn_rule_update.Name = "tsbtn_rule_update";
            this.tsbtn_rule_update.Size = new System.Drawing.Size(84, 35);
            this.tsbtn_rule_update.Text = "刷新列表";
            this.tsbtn_rule_update.Click += new System.EventHandler(this.tsbtn_rule_update_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 38);
            // 
            // tsbtn_rule_paras
            // 
            this.tsbtn_rule_paras.Image = ((System.Drawing.Image)(resources.GetObject("tsbtn_rule_paras.Image")));
            this.tsbtn_rule_paras.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtn_rule_paras.Name = "tsbtn_rule_paras";
            this.tsbtn_rule_paras.Size = new System.Drawing.Size(84, 35);
            this.tsbtn_rule_paras.Text = "管理变量";
            this.tsbtn_rule_paras.Click += new System.EventHandler(this.tsbtn_rule_paras_Click);
            // 
            // tabPage_realtime_data
            // 
            this.tabPage_realtime_data.Controls.Add(this.splitContainerH1_2H2_1V1_2V2);
            this.tabPage_realtime_data.Location = new System.Drawing.Point(4, 22);
            this.tabPage_realtime_data.Name = "tabPage_realtime_data";
            this.tabPage_realtime_data.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_realtime_data.Size = new System.Drawing.Size(969, 574);
            this.tabPage_realtime_data.TabIndex = 0;
            this.tabPage_realtime_data.Text = "实时数据";
            this.tabPage_realtime_data.UseVisualStyleBackColor = true;
            // 
            // splitContainerH1_2H2_1V1_2V2
            // 
            this.splitContainerH1_2H2_1V1_2V2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerH1_2H2_1V1_2V2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainerH1_2H2_1V1_2V2.Location = new System.Drawing.Point(3, 3);
            this.splitContainerH1_2H2_1V1_2V2.Name = "splitContainerH1_2H2_1V1_2V2";
            // 
            // splitContainerH1_2H2_1V1_2V2.Panel1
            // 
            this.splitContainerH1_2H2_1V1_2V2.Panel1.Controls.Add(this.listview_parainfo);
            this.splitContainerH1_2H2_1V1_2V2.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            // 
            // splitContainerH1_2H2_1V1_2V2.Panel2
            // 
            this.splitContainerH1_2H2_1V1_2V2.Panel2.AllowDrop = true;
            this.splitContainerH1_2H2_1V1_2V2.Panel2.Controls.Add(this.splitContainerH1_2H2_1V1_2V2_2V3);
            this.splitContainerH1_2H2_1V1_2V2.Size = new System.Drawing.Size(963, 568);
            this.splitContainerH1_2H2_1V1_2V2.SplitterDistance = 255;
            this.splitContainerH1_2H2_1V1_2V2.SplitterWidth = 1;
            this.splitContainerH1_2H2_1V1_2V2.TabIndex = 14;
            this.splitContainerH1_2H2_1V1_2V2.TabStop = false;
            // 
            // listview_parainfo
            // 
            this.listview_parainfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listview_parainfo.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.listview_parainfo.FullRowSelect = true;
            this.listview_parainfo.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listview_parainfo.LabelWrap = false;
            this.listview_parainfo.Location = new System.Drawing.Point(0, 0);
            this.listview_parainfo.MultiSelect = false;
            this.listview_parainfo.Name = "listview_parainfo";
            this.listview_parainfo.Size = new System.Drawing.Size(255, 568);
            this.listview_parainfo.TabIndex = 2;
            this.listview_parainfo.UseCompatibleStateImageBehavior = false;
            this.listview_parainfo.View = System.Windows.Forms.View.Details;
            // 
            // splitContainerH1_2H2_1V1_2V2_2V3
            // 
            this.splitContainerH1_2H2_1V1_2V2_2V3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerH1_2H2_1V1_2V2_2V3.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainerH1_2H2_1V1_2V2_2V3.Location = new System.Drawing.Point(0, 0);
            this.splitContainerH1_2H2_1V1_2V2_2V3.Name = "splitContainerH1_2H2_1V1_2V2_2V3";
            // 
            // splitContainerH1_2H2_1V1_2V2_2V3.Panel1
            // 
            this.splitContainerH1_2H2_1V1_2V2_2V3.Panel1.Controls.Add(this.zgc_realtime);
            this.splitContainerH1_2H2_1V1_2V2_2V3.Panel1MinSize = 500;
            // 
            // splitContainerH1_2H2_1V1_2V2_2V3.Panel2
            // 
            this.splitContainerH1_2H2_1V1_2V2_2V3.Panel2.Controls.Add(this.groupBox4);
            this.splitContainerH1_2H2_1V1_2V2_2V3.Panel2MinSize = 200;
            this.splitContainerH1_2H2_1V1_2V2_2V3.Size = new System.Drawing.Size(707, 568);
            this.splitContainerH1_2H2_1V1_2V2_2V3.SplitterDistance = 503;
            this.splitContainerH1_2H2_1V1_2V2_2V3.SplitterWidth = 1;
            this.splitContainerH1_2H2_1V1_2V2_2V3.TabIndex = 0;
            // 
            // zgc_realtime
            // 
            this.zgc_realtime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.zgc_realtime.Font = new System.Drawing.Font("宋体", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.zgc_realtime.IsShowPointValues = true;
            this.zgc_realtime.IsSynchronizeXAxes = true;
            this.zgc_realtime.Location = new System.Drawing.Point(0, 0);
            this.zgc_realtime.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.zgc_realtime.Name = "zgc_realtime";
            this.zgc_realtime.PointDateFormat = "T";
            this.zgc_realtime.PointValueFormat = "n2";
            this.zgc_realtime.ScrollGrace = 0D;
            this.zgc_realtime.ScrollMaxX = 0D;
            this.zgc_realtime.ScrollMaxY = 0D;
            this.zgc_realtime.ScrollMaxY2 = 0D;
            this.zgc_realtime.ScrollMinX = 0D;
            this.zgc_realtime.ScrollMinY = 0D;
            this.zgc_realtime.ScrollMinY2 = 0D;
            this.zgc_realtime.Size = new System.Drawing.Size(503, 568);
            this.zgc_realtime.TabIndex = 12;
            this.zgc_realtime.TabStop = false;
            // 
            // groupBox4
            // 
            this.groupBox4.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox4.Controls.Add(this.label_info1_title);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point(0, 0);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(203, 568);
            this.groupBox4.TabIndex = 14;
            this.groupBox4.TabStop = false;
            // 
            // label_info1_title
            // 
            this.label_info1_title.AutoSize = true;
            this.label_info1_title.Font = new System.Drawing.Font("宋体", 12F);
            this.label_info1_title.Location = new System.Drawing.Point(6, 17);
            this.label_info1_title.Name = "label_info1_title";
            this.label_info1_title.Size = new System.Drawing.Size(72, 16);
            this.label_info1_title.TabIndex = 1;
            this.label_info1_title.Text = "报警信息";
            // 
            // tabPage_history_curve
            // 
            this.tabPage_history_curve.Controls.Add(this.splitContainer_child);
            this.tabPage_history_curve.Location = new System.Drawing.Point(4, 22);
            this.tabPage_history_curve.Name = "tabPage_history_curve";
            this.tabPage_history_curve.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_history_curve.Size = new System.Drawing.Size(969, 555);
            this.tabPage_history_curve.TabIndex = 1;
            this.tabPage_history_curve.Text = "历史曲线";
            this.tabPage_history_curve.UseVisualStyleBackColor = true;
            // 
            // splitContainer_child
            // 
            this.splitContainer_child.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer_child.IsSplitterFixed = true;
            this.splitContainer_child.Location = new System.Drawing.Point(3, 3);
            this.splitContainer_child.Name = "splitContainer_child";
            this.splitContainer_child.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer_child.Panel1
            // 
            this.splitContainer_child.Panel1.Controls.Add(this.btn_curve_next);
            this.splitContainer_child.Panel1.Controls.Add(this.btn_curve_prev);
            this.splitContainer_child.Panel1.Controls.Add(this.btn_curve_search);
            this.splitContainer_child.Panel1.Controls.Add(this.label1);
            this.splitContainer_child.Panel1.Controls.Add(this.dtp_curve_start);
            this.splitContainer_child.Panel1.Controls.Add(this.dtp_curve_end);
            this.splitContainer_child.Panel1.Controls.Add(this.label2);
            // 
            // splitContainer_child.Panel2
            // 
            this.splitContainer_child.Panel2.Controls.Add(this.zgc_history);
            this.splitContainer_child.Size = new System.Drawing.Size(963, 549);
            this.splitContainer_child.SplitterDistance = 36;
            this.splitContainer_child.SplitterWidth = 1;
            this.splitContainer_child.TabIndex = 19;
            this.splitContainer_child.TabStop = false;
            // 
            // btn_curve_next
            // 
            this.btn_curve_next.Location = new System.Drawing.Point(583, 6);
            this.btn_curve_next.Name = "btn_curve_next";
            this.btn_curve_next.Size = new System.Drawing.Size(25, 23);
            this.btn_curve_next.TabIndex = 25;
            this.btn_curve_next.Text = ">";
            this.btn_curve_next.UseVisualStyleBackColor = true;
            this.btn_curve_next.Click += new System.EventHandler(this.btn_curve_next_Click);
            // 
            // btn_curve_prev
            // 
            this.btn_curve_prev.Location = new System.Drawing.Point(552, 6);
            this.btn_curve_prev.Name = "btn_curve_prev";
            this.btn_curve_prev.Size = new System.Drawing.Size(25, 23);
            this.btn_curve_prev.TabIndex = 24;
            this.btn_curve_prev.Text = "<";
            this.btn_curve_prev.UseVisualStyleBackColor = true;
            this.btn_curve_prev.Click += new System.EventHandler(this.btn_curve_prev_Click);
            // 
            // btn_curve_search
            // 
            this.btn_curve_search.Location = new System.Drawing.Point(471, 6);
            this.btn_curve_search.Name = "btn_curve_search";
            this.btn_curve_search.Size = new System.Drawing.Size(75, 23);
            this.btn_curve_search.TabIndex = 23;
            this.btn_curve_search.Text = "查询";
            this.btn_curve_search.UseVisualStyleBackColor = true;
            this.btn_curve_search.Click += new System.EventHandler(this.btn_curve_search_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 21;
            this.label1.Text = "起始时间";
            // 
            // dtp_curve_start
            // 
            this.dtp_curve_start.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtp_curve_start.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_curve_start.Location = new System.Drawing.Point(71, 8);
            this.dtp_curve_start.Name = "dtp_curve_start";
            this.dtp_curve_start.Size = new System.Drawing.Size(155, 21);
            this.dtp_curve_start.TabIndex = 19;
            // 
            // dtp_curve_end
            // 
            this.dtp_curve_end.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtp_curve_end.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_curve_end.Location = new System.Drawing.Point(310, 8);
            this.dtp_curve_end.Name = "dtp_curve_end";
            this.dtp_curve_end.Size = new System.Drawing.Size(155, 21);
            this.dtp_curve_end.TabIndex = 20;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(241, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 22;
            this.label2.Text = "截止时间";
            // 
            // zgc_history
            // 
            this.zgc_history.Dock = System.Windows.Forms.DockStyle.Fill;
            this.zgc_history.Font = new System.Drawing.Font("宋体", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.zgc_history.IsShowPointValues = true;
            this.zgc_history.IsSynchronizeXAxes = true;
            this.zgc_history.Location = new System.Drawing.Point(0, 0);
            this.zgc_history.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.zgc_history.Name = "zgc_history";
            this.zgc_history.PointDateFormat = "T";
            this.zgc_history.PointValueFormat = "n2";
            this.zgc_history.ScrollGrace = 0D;
            this.zgc_history.ScrollMaxX = 0D;
            this.zgc_history.ScrollMaxY = 0D;
            this.zgc_history.ScrollMaxY2 = 0D;
            this.zgc_history.ScrollMinX = 0D;
            this.zgc_history.ScrollMinY = 0D;
            this.zgc_history.ScrollMinY2 = 0D;
            this.zgc_history.Size = new System.Drawing.Size(963, 512);
            this.zgc_history.TabIndex = 15;
            this.zgc_history.TabStop = false;
            // 
            // tabPage_history_data
            // 
            this.tabPage_history_data.Controls.Add(this.splitContainer1);
            this.tabPage_history_data.Location = new System.Drawing.Point(4, 22);
            this.tabPage_history_data.Name = "tabPage_history_data";
            this.tabPage_history_data.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_history_data.Size = new System.Drawing.Size(969, 555);
            this.tabPage_history_data.TabIndex = 2;
            this.tabPage_history_data.Text = "历史数据";
            this.tabPage_history_data.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.btn_data_next);
            this.splitContainer1.Panel1.Controls.Add(this.btn_data_prev);
            this.splitContainer1.Panel1.Controls.Add(this.btn_data_search);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.dtp_data_start);
            this.splitContainer1.Panel1.Controls.Add(this.dtp_data_end);
            this.splitContainer1.Panel1.Controls.Add(this.label4);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dgv_data);
            this.splitContainer1.Size = new System.Drawing.Size(963, 549);
            this.splitContainer1.SplitterDistance = 36;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 20;
            this.splitContainer1.TabStop = false;
            // 
            // btn_data_next
            // 
            this.btn_data_next.Location = new System.Drawing.Point(583, 6);
            this.btn_data_next.Name = "btn_data_next";
            this.btn_data_next.Size = new System.Drawing.Size(25, 23);
            this.btn_data_next.TabIndex = 25;
            this.btn_data_next.Text = ">";
            this.btn_data_next.UseVisualStyleBackColor = true;
            this.btn_data_next.Click += new System.EventHandler(this.btn_data_next_Click);
            // 
            // btn_data_prev
            // 
            this.btn_data_prev.Location = new System.Drawing.Point(552, 6);
            this.btn_data_prev.Name = "btn_data_prev";
            this.btn_data_prev.Size = new System.Drawing.Size(25, 23);
            this.btn_data_prev.TabIndex = 24;
            this.btn_data_prev.Text = "<";
            this.btn_data_prev.UseVisualStyleBackColor = true;
            this.btn_data_prev.Click += new System.EventHandler(this.btn_data_prev_Click);
            // 
            // btn_data_search
            // 
            this.btn_data_search.Location = new System.Drawing.Point(471, 6);
            this.btn_data_search.Name = "btn_data_search";
            this.btn_data_search.Size = new System.Drawing.Size(75, 23);
            this.btn_data_search.TabIndex = 23;
            this.btn_data_search.Text = "查询";
            this.btn_data_search.UseVisualStyleBackColor = true;
            this.btn_data_search.Click += new System.EventHandler(this.btn_data_search_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 21;
            this.label3.Text = "起始时间";
            // 
            // dtp_data_start
            // 
            this.dtp_data_start.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtp_data_start.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_data_start.Location = new System.Drawing.Point(71, 8);
            this.dtp_data_start.Name = "dtp_data_start";
            this.dtp_data_start.Size = new System.Drawing.Size(155, 21);
            this.dtp_data_start.TabIndex = 19;
            // 
            // dtp_data_end
            // 
            this.dtp_data_end.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtp_data_end.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_data_end.Location = new System.Drawing.Point(310, 8);
            this.dtp_data_end.Name = "dtp_data_end";
            this.dtp_data_end.Size = new System.Drawing.Size(155, 21);
            this.dtp_data_end.TabIndex = 20;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(241, 14);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 22;
            this.label4.Text = "截止时间";
            // 
            // dgv_data
            // 
            this.dgv_data.AllowUserToAddRows = false;
            this.dgv_data.AllowUserToDeleteRows = false;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            this.dgv_data.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_data.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgv_data.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_data.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_data.Location = new System.Drawing.Point(0, 0);
            this.dgv_data.Name = "dgv_data";
            this.dgv_data.ReadOnly = true;
            this.dgv_data.RowTemplate.Height = 23;
            this.dgv_data.Size = new System.Drawing.Size(963, 512);
            this.dgv_data.TabIndex = 0;
            this.dgv_data.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgv_data_CellFormatting);
            // 
            // splitContainerH1
            // 
            this.splitContainerH1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerH1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainerH1.IsSplitterFixed = true;
            this.splitContainerH1.Location = new System.Drawing.Point(0, 75);
            this.splitContainerH1.Name = "splitContainerH1";
            this.splitContainerH1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.splitContainerH1.Panel1MinSize = 0;
            // 
            // splitContainerH1.Panel2
            // 
            this.splitContainerH1.Panel2.Controls.Add(this.splitContainerH1_2H2);
            this.splitContainerH1.Size = new System.Drawing.Size(1008, 635);
            this.splitContainerH1.SplitterDistance = 15;
            this.splitContainerH1.SplitterWidth = 1;
            this.splitContainerH1.TabIndex = 42;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dgv_oc_logs);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(963, 199);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "日志记录";
            // 
            // dgv_oc_logs
            // 
            this.dgv_oc_logs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_oc_logs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_oc_logs.Location = new System.Drawing.Point(3, 17);
            this.dgv_oc_logs.Name = "dgv_oc_logs";
            this.dgv_oc_logs.RowTemplate.Height = 23;
            this.dgv_oc_logs.Size = new System.Drawing.Size(957, 179);
            this.dgv_oc_logs.TabIndex = 1;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1008, 732);
            this.Controls.Add(this.splitContainerH1);
            this.Controls.Add(this.pictureBox_main);
            this.Controls.Add(this.toolStrip_main);
            this.Controls.Add(this.statusStrip_main);
            this.Controls.Add(this.msMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "半自磨优化系统";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.statusStrip_main.ResumeLayout(false);
            this.statusStrip_main.PerformLayout();
            this.toolStrip_main.ResumeLayout(false);
            this.toolStrip_main.PerformLayout();
            this.msMain.ResumeLayout(false);
            this.msMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_main)).EndInit();
            this.splitContainerH1_2H2.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerH1_2H2)).EndInit();
            this.splitContainerH1_2H2.ResumeLayout(false);
            this.splitContainerH1_2H2_1V1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerH1_2H2_1V1)).EndInit();
            this.splitContainerH1_2H2_1V1.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.tabPage_optimal_control.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_oc_rules)).EndInit();
            this.toolStrip_oc.ResumeLayout(false);
            this.toolStrip_oc.PerformLayout();
            this.tabPage_realtime_data.ResumeLayout(false);
            this.splitContainerH1_2H2_1V1_2V2.Panel1.ResumeLayout(false);
            this.splitContainerH1_2H2_1V1_2V2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerH1_2H2_1V1_2V2)).EndInit();
            this.splitContainerH1_2H2_1V1_2V2.ResumeLayout(false);
            this.splitContainerH1_2H2_1V1_2V2_2V3.Panel1.ResumeLayout(false);
            this.splitContainerH1_2H2_1V1_2V2_2V3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerH1_2H2_1V1_2V2_2V3)).EndInit();
            this.splitContainerH1_2H2_1V1_2V2_2V3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.tabPage_history_curve.ResumeLayout(false);
            this.splitContainer_child.Panel1.ResumeLayout(false);
            this.splitContainer_child.Panel1.PerformLayout();
            this.splitContainer_child.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_child)).EndInit();
            this.splitContainer_child.ResumeLayout(false);
            this.tabPage_history_data.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_data)).EndInit();
            this.splitContainerH1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerH1)).EndInit();
            this.splitContainerH1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_oc_logs)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip_main;
        private System.Windows.Forms.ToolStrip toolStrip_main;
        private System.Windows.Forms.ToolStripButton btn_run;
        private System.Windows.Forms.ToolStripStatusLabel status_Label;
        private System.Windows.Forms.OpenFileDialog ofd_history;
        private System.Windows.Forms.ToolStripButton btn_stop;
        private System.Windows.Forms.ToolStripButton btn_info;
        private System.Windows.Forms.ToolStripButton btn_quit;
        private System.Windows.Forms.PictureBox pictureBox_main;
        private System.Windows.Forms.ToolStripButton btn_config;
        private System.Windows.Forms.MenuStrip msMain;
        private System.Windows.Forms.ToolStripMenuItem menu_file;
        private System.Windows.Forms.ToolStripMenuItem menu_file_login;
        private System.Windows.Forms.ToolStripMenuItem menu_file_logoff;
        private System.Windows.Forms.ToolStripMenuItem menu_file_lockscreen;
        private System.Windows.Forms.ToolStripMenuItem menu_file_quit;
        private System.Windows.Forms.ToolStripMenuItem menu_control;
        private System.Windows.Forms.ToolStripMenuItem menu_control_run;
        private System.Windows.Forms.ToolStripMenuItem menu_control_stop;
        private System.Windows.Forms.ToolStripMenuItem menu_control_clear;
        private System.Windows.Forms.ToolStripMenuItem menu_config;
        private System.Windows.Forms.ToolStripMenuItem menu_config_config;
        private System.Windows.Forms.ToolStripMenuItem menu_config_user;
        private System.Windows.Forms.ToolStripMenuItem menu_config_password;
        private System.Windows.Forms.ToolStripMenuItem menu_help;
        private System.Windows.Forms.ToolStripMenuItem menu_help_about;
        private System.Windows.Forms.ToolStripMenuItem menu_config_devices;
        private System.Windows.Forms.ToolStripMenuItem menu_config_parameters;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.SplitContainer splitContainerH1_2H2;
        private System.Windows.Forms.SplitContainer splitContainerH1_2H2_1V1;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPage_realtime_data;
        private System.Windows.Forms.SplitContainer splitContainerH1_2H2_1V1_2V2;
        private DoubleBufferListView listview_parainfo;
        private System.Windows.Forms.TabPage tabPage_history_curve;
        private System.Windows.Forms.SplitContainer splitContainerH1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton btn_curve_realtime;
        private System.Windows.Forms.ToolStripButton btn_curve_stop;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.TabPage tabPage_history_data;
        private System.Windows.Forms.SplitContainer splitContainer_child;
        private System.Windows.Forms.Button btn_curve_next;
        private System.Windows.Forms.Button btn_curve_prev;
        private System.Windows.Forms.Button btn_curve_search;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtp_curve_start;
        private System.Windows.Forms.DateTimePicker dtp_curve_end;
        private System.Windows.Forms.Label label2;
        private ZedGraph.ZedGraphControl zgc_history;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btn_data_next;
        private System.Windows.Forms.Button btn_data_prev;
        private System.Windows.Forms.Button btn_data_search;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtp_data_start;
        private System.Windows.Forms.DateTimePicker dtp_data_end;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dgv_data;
        private System.Windows.Forms.SplitContainer splitContainerH1_2H2_1V1_2V2_2V3;
        private ZedGraph.ZedGraphControl zgc_realtime;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label_info1_title;
        private System.Windows.Forms.TabPage tabPage_optimal_control;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgv_oc_rules;
        private System.Windows.Forms.ToolStrip toolStrip_oc;
        private System.Windows.Forms.ToolStripButton tsbtn_rule_add;
        private System.Windows.Forms.ToolStripButton tsbtn_rule_edit;
        private System.Windows.Forms.ToolStripButton tsbtn_rule_delete;
        private System.Windows.Forms.ToolStripSeparator toolStripButton1;
        private System.Windows.Forms.ToolStripButton tsbtn_rule_update;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripButton tsbtn_rule_paras;
        private System.Windows.Forms.ToolStripButton tsbtn_oc_enabled;
        private System.Windows.Forms.ToolStripButton tsbtn_oc_disabled;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView dgv_oc_logs;
    }
}

