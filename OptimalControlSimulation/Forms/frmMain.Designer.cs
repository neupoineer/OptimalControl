using Utility;

namespace OptimalControl.Forms
{
    partial class FrmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.statusStrip_main = new System.Windows.Forms.StatusStrip();
            this.status_Label = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip_main = new System.Windows.Forms.ToolStrip();
            this.btn_run = new System.Windows.Forms.ToolStripButton();
            this.btn_stop = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
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
            this.splitContainerH1 = new System.Windows.Forms.SplitContainer();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.textBox10 = new System.Windows.Forms.TextBox();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.statusStrip_main.SuspendLayout();
            this.toolStrip_main.SuspendLayout();
            this.msMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_main)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerH1)).BeginInit();
            this.splitContainerH1.Panel2.SuspendLayout();
            this.splitContainerH1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip_main
            // 
            this.statusStrip_main.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.status_Label});
            this.statusStrip_main.Location = new System.Drawing.Point(0, 539);
            this.statusStrip_main.Name = "statusStrip_main";
            this.statusStrip_main.Size = new System.Drawing.Size(784, 22);
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
            this.btn_config,
            this.btn_info,
            this.btn_quit});
            this.toolStrip_main.Location = new System.Drawing.Point(0, 33);
            this.toolStrip_main.Name = "toolStrip_main";
            this.toolStrip_main.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolStrip_main.Size = new System.Drawing.Size(784, 44);
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
            this.msMain.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.msMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            this.msMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_file,
            this.menu_control,
            this.menu_config,
            this.menu_help});
            this.msMain.Location = new System.Drawing.Point(0, 0);
            this.msMain.Name = "msMain";
            this.msMain.Padding = new System.Windows.Forms.Padding(3, 7, 1, 6);
            this.msMain.Size = new System.Drawing.Size(784, 33);
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
            this.menu_file.Size = new System.Drawing.Size(76, 20);
            this.menu_file.Text = "文件(&F)";
            // 
            // menu_file_login
            // 
            this.menu_file_login.Image = ((System.Drawing.Image)(resources.GetObject("menu_file_login.Image")));
            this.menu_file_login.Name = "menu_file_login";
            this.menu_file_login.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
            this.menu_file_login.Size = new System.Drawing.Size(188, 22);
            this.menu_file_login.Text = "登录(&I)";
            this.menu_file_login.Click += new System.EventHandler(this.menu_file_login_Click);
            // 
            // menu_file_logoff
            // 
            this.menu_file_logoff.Image = ((System.Drawing.Image)(resources.GetObject("menu_file_logoff.Image")));
            this.menu_file_logoff.Name = "menu_file_logoff";
            this.menu_file_logoff.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.menu_file_logoff.Size = new System.Drawing.Size(188, 22);
            this.menu_file_logoff.Text = "注销(&F)";
            this.menu_file_logoff.Click += new System.EventHandler(this.menu_file_logoff_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(185, 6);
            // 
            // menu_file_lockscreen
            // 
            this.menu_file_lockscreen.Image = ((System.Drawing.Image)(resources.GetObject("menu_file_lockscreen.Image")));
            this.menu_file_lockscreen.Name = "menu_file_lockscreen";
            this.menu_file_lockscreen.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
            this.menu_file_lockscreen.Size = new System.Drawing.Size(188, 22);
            this.menu_file_lockscreen.Text = "锁屏(&L)";
            this.menu_file_lockscreen.Click += new System.EventHandler(this.menu_file_lockscreen_Click);
            // 
            // menu_file_quit
            // 
            this.menu_file_quit.Image = ((System.Drawing.Image)(resources.GetObject("menu_file_quit.Image")));
            this.menu_file_quit.Name = "menu_file_quit";
            this.menu_file_quit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.menu_file_quit.Size = new System.Drawing.Size(188, 22);
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
            this.menu_control.Size = new System.Drawing.Size(76, 20);
            this.menu_control.Text = "波形(&G)";
            // 
            // menu_control_run
            // 
            this.menu_control_run.Image = ((System.Drawing.Image)(resources.GetObject("menu_control_run.Image")));
            this.menu_control_run.Name = "menu_control_run";
            this.menu_control_run.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.menu_control_run.Size = new System.Drawing.Size(220, 22);
            this.menu_control_run.Text = "实时显示(&R)";
            this.menu_control_run.Click += new System.EventHandler(this.menu_control_run_Click);
            // 
            // menu_control_stop
            // 
            this.menu_control_stop.Enabled = false;
            this.menu_control_stop.Image = ((System.Drawing.Image)(resources.GetObject("menu_control_stop.Image")));
            this.menu_control_stop.Name = "menu_control_stop";
            this.menu_control_stop.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
            this.menu_control_stop.Size = new System.Drawing.Size(220, 22);
            this.menu_control_stop.Text = "停止显示(&T)";
            this.menu_control_stop.Click += new System.EventHandler(this.menu_control_stop_Click);
            // 
            // menu_control_clear
            // 
            this.menu_control_clear.Image = ((System.Drawing.Image)(resources.GetObject("menu_control_clear.Image")));
            this.menu_control_clear.Name = "menu_control_clear";
            this.menu_control_clear.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.menu_control_clear.Size = new System.Drawing.Size(220, 22);
            this.menu_control_clear.Text = "清空波形(&C)";
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
            this.menu_config.Size = new System.Drawing.Size(76, 20);
            this.menu_config.Text = "设置(&O)";
            // 
            // menu_config_config
            // 
            this.menu_config_config.Image = ((System.Drawing.Image)(resources.GetObject("menu_config_config.Image")));
            this.menu_config_config.Name = "menu_config_config";
            this.menu_config_config.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.menu_config_config.Size = new System.Drawing.Size(220, 22);
            this.menu_config_config.Text = "参数设置(&S)";
            this.menu_config_config.Click += new System.EventHandler(this.menu_config_config_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(217, 6);
            // 
            // menu_config_user
            // 
            this.menu_config_user.Image = ((System.Drawing.Image)(resources.GetObject("menu_config_user.Image")));
            this.menu_config_user.Name = "menu_config_user";
            this.menu_config_user.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.U)));
            this.menu_config_user.Size = new System.Drawing.Size(220, 22);
            this.menu_config_user.Text = "用户设置(&U)";
            this.menu_config_user.Click += new System.EventHandler(this.menu_config_user_Click);
            // 
            // menu_config_password
            // 
            this.menu_config_password.Image = ((System.Drawing.Image)(resources.GetObject("menu_config_password.Image")));
            this.menu_config_password.Name = "menu_config_password";
            this.menu_config_password.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.menu_config_password.Size = new System.Drawing.Size(220, 22);
            this.menu_config_password.Text = "修改密码(&P)";
            this.menu_config_password.Click += new System.EventHandler(this.menu_config_password_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(217, 6);
            // 
            // menu_config_devices
            // 
            this.menu_config_devices.Image = ((System.Drawing.Image)(resources.GetObject("menu_config_devices.Image")));
            this.menu_config_devices.Name = "menu_config_devices";
            this.menu_config_devices.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            this.menu_config_devices.Size = new System.Drawing.Size(220, 22);
            this.menu_config_devices.Text = "设备管理(&D)";
            this.menu_config_devices.Click += new System.EventHandler(this.menu_config_devices_Click);
            // 
            // menu_config_parameters
            // 
            this.menu_config_parameters.Image = global::OptimalControl.Properties.Resources.graph;
            this.menu_config_parameters.Name = "menu_config_parameters";
            this.menu_config_parameters.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.menu_config_parameters.Size = new System.Drawing.Size(220, 22);
            this.menu_config_parameters.Text = "变量管理(&V)";
            this.menu_config_parameters.Click += new System.EventHandler(this.menu_config_parameters_Click);
            // 
            // menu_help
            // 
            this.menu_help.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_help_about});
            this.menu_help.Name = "menu_help";
            this.menu_help.Size = new System.Drawing.Size(76, 20);
            this.menu_help.Text = "帮助(&H)";
            // 
            // menu_help_about
            // 
            this.menu_help_about.Image = ((System.Drawing.Image)(resources.GetObject("menu_help_about.Image")));
            this.menu_help_about.Name = "menu_help_about";
            this.menu_help_about.Size = new System.Drawing.Size(164, 22);
            this.menu_help_about.Text = "关于软件(&A)";
            this.menu_help_about.Click += new System.EventHandler(this.menu_help_about_Click);
            // 
            // pictureBox_main
            // 
            this.pictureBox_main.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox_main.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox_main.Image")));
            this.pictureBox_main.Location = new System.Drawing.Point(1020, 11);
            this.pictureBox_main.Name = "pictureBox_main";
            this.pictureBox_main.Size = new System.Drawing.Size(60, 60);
            this.pictureBox_main.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_main.TabIndex = 16;
            this.pictureBox_main.TabStop = false;
            // 
            // splitContainerH1
            // 
            this.splitContainerH1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerH1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainerH1.IsSplitterFixed = true;
            this.splitContainerH1.Location = new System.Drawing.Point(0, 77);
            this.splitContainerH1.Name = "splitContainerH1";
            this.splitContainerH1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.splitContainerH1.Panel1MinSize = 0;
            // 
            // splitContainerH1.Panel2
            // 
            this.splitContainerH1.Panel2.Controls.Add(this.groupBox2);
            this.splitContainerH1.Panel2.Controls.Add(this.groupBox1);
            this.splitContainerH1.Panel2MinSize = 0;
            this.splitContainerH1.Size = new System.Drawing.Size(784, 462);
            this.splitContainerH1.SplitterDistance = 25;
            this.splitContainerH1.TabIndex = 43;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.textBox6);
            this.groupBox2.Controls.Add(this.textBox10);
            this.groupBox2.Controls.Add(this.textBox7);
            this.groupBox2.Controls.Add(this.textBox9);
            this.groupBox2.Controls.Add(this.textBox8);
            this.groupBox2.Location = new System.Drawing.Point(440, 60);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(280, 300);
            this.groupBox2.TabIndex = 45;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "输出变量";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 245);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 16);
            this.label6.TabIndex = 9;
            this.label6.Text = "筛下+80目";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 195);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(88, 16);
            this.label7.TabIndex = 8;
            this.label7.Text = "筛下-200目";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(45, 145);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(56, 16);
            this.label8.TabIndex = 7;
            this.label8.Text = "顽石量";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(29, 95);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(72, 16);
            this.label9.TabIndex = 6;
            this.label9.Text = "磨矿浓度";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(45, 45);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(56, 16);
            this.label10.TabIndex = 5;
            this.label10.Text = "填充率";
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(107, 42);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(100, 26);
            this.textBox6.TabIndex = 0;
            // 
            // textBox10
            // 
            this.textBox10.Location = new System.Drawing.Point(107, 242);
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new System.Drawing.Size(100, 26);
            this.textBox10.TabIndex = 4;
            // 
            // textBox7
            // 
            this.textBox7.Location = new System.Drawing.Point(107, 92);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(100, 26);
            this.textBox7.TabIndex = 1;
            // 
            // textBox9
            // 
            this.textBox9.Location = new System.Drawing.Point(107, 192);
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new System.Drawing.Size(100, 26);
            this.textBox9.TabIndex = 3;
            // 
            // textBox8
            // 
            this.textBox8.Location = new System.Drawing.Point(107, 142);
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new System.Drawing.Size(100, 26);
            this.textBox8.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.textBox5);
            this.groupBox1.Controls.Add(this.textBox2);
            this.groupBox1.Controls.Add(this.textBox4);
            this.groupBox1.Controls.Add(this.textBox3);
            this.groupBox1.Location = new System.Drawing.Point(60, 60);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(280, 300);
            this.groupBox1.TabIndex = 44;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "输入变量";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(29, 245);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 16);
            this.label5.TabIndex = 9;
            this.label5.Text = "仿真步长";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(29, 195);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 16);
            this.label4.TabIndex = 8;
            this.label4.Text = "矿石性质";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(45, 145);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 16);
            this.label3.TabIndex = 7;
            this.label3.Text = "给水量";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(45, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 16);
            this.label2.TabIndex = 6;
            this.label2.Text = "总矿量";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(45, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 16);
            this.label1.TabIndex = 5;
            this.label1.Text = "原矿量";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(107, 42);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 26);
            this.textBox1.TabIndex = 0;
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(107, 242);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(100, 26);
            this.textBox5.TabIndex = 4;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(107, 92);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 26);
            this.textBox2.TabIndex = 1;
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(107, 192);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(100, 26);
            this.textBox4.TabIndex = 3;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(107, 142);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(100, 26);
            this.textBox3.TabIndex = 2;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(213, 45);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(32, 16);
            this.label11.TabIndex = 10;
            this.label11.Text = "t/h";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(213, 95);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(32, 16);
            this.label12.TabIndex = 11;
            this.label12.Text = "t/h";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(213, 145);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(32, 16);
            this.label13.TabIndex = 12;
            this.label13.Text = "t/h";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(213, 145);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(32, 16);
            this.label14.TabIndex = 11;
            this.label14.Text = "t/h";
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.pictureBox_main);
            this.Controls.Add(this.splitContainerH1);
            this.Controls.Add(this.toolStrip_main);
            this.Controls.Add(this.statusStrip_main);
            this.Controls.Add(this.msMain);
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "半自磨在线仿真模型";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.statusStrip_main.ResumeLayout(false);
            this.statusStrip_main.PerformLayout();
            this.toolStrip_main.ResumeLayout(false);
            this.toolStrip_main.PerformLayout();
            this.msMain.ResumeLayout(false);
            this.msMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_main)).EndInit();
            this.splitContainerH1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerH1)).EndInit();
            this.splitContainerH1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
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
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.SplitContainer splitContainerH1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.TextBox textBox10;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.TextBox textBox9;
        private System.Windows.Forms.TextBox textBox8;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
    }
}

