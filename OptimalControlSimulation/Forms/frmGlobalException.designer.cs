namespace OptimalControl.Forms
{
    partial class frmGlobalException
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmGlobalException));
            this.lblHelpLink = new System.Windows.Forms.Label();
            this.lblSource = new System.Windows.Forms.Label();
            this.txtStackTrace = new System.Windows.Forms.TextBox();
            this.txtTargeSite = new System.Windows.Forms.TextBox();
            this.lblCurrentDirectory = new System.Windows.Forms.Label();
            this.lblMachineName = new System.Windows.Forms.Label();
            this.lblOSVersion = new System.Windows.Forms.Label();
            this.lblSystemDirectory = new System.Windows.Forms.Label();
            this.lblUserName = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.btnAbort = new System.Windows.Forms.Button();
            this.btnIgnore = new System.Windows.Forms.Button();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.lblMessage = new System.Windows.Forms.Label();
            this.lblStackTrace = new System.Windows.Forms.Label();
            this.lblTargeSite = new System.Windows.Forms.Label();
            this.txtSource = new System.Windows.Forms.TextBox();
            this.linkLblHelpLink = new System.Windows.Forms.LinkLabel();
            this.linkLblCurrentDirectory = new System.Windows.Forms.LinkLabel();
            this.linkLblSystemDirectory = new System.Windows.Forms.LinkLabel();
            this.lblInfo = new System.Windows.Forms.Label();
            this.pbEnvironment = new System.Windows.Forms.PictureBox();
            this.pbInfo = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbEnvironment)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // lblHelpLink
            // 
            this.lblHelpLink.AutoSize = true;
            this.lblHelpLink.Location = new System.Drawing.Point(78, 58);
            this.lblHelpLink.Name = "lblHelpLink";
            this.lblHelpLink.Size = new System.Drawing.Size(59, 12);
            this.lblHelpLink.TabIndex = 4;
            this.lblHelpLink.Text = "帮助链接:";
            // 
            // lblSource
            // 
            this.lblSource.AutoSize = true;
            this.lblSource.Location = new System.Drawing.Point(102, 80);
            this.lblSource.Name = "lblSource";
            this.lblSource.Size = new System.Drawing.Size(35, 12);
            this.lblSource.TabIndex = 6;
            this.lblSource.Text = "对象:";
            // 
            // txtStackTrace
            // 
            this.txtStackTrace.BackColor = System.Drawing.SystemColors.Window;
            this.txtStackTrace.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtStackTrace.Location = new System.Drawing.Point(134, 101);
            this.txtStackTrace.Multiline = true;
            this.txtStackTrace.Name = "txtStackTrace";
            this.txtStackTrace.ReadOnly = true;
            this.txtStackTrace.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtStackTrace.Size = new System.Drawing.Size(448, 116);
            this.txtStackTrace.TabIndex = 9;
            // 
            // txtTargeSite
            // 
            this.txtTargeSite.BackColor = System.Drawing.SystemColors.Window;
            this.txtTargeSite.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtTargeSite.Location = new System.Drawing.Point(134, 220);
            this.txtTargeSite.Name = "txtTargeSite";
            this.txtTargeSite.ReadOnly = true;
            this.txtTargeSite.Size = new System.Drawing.Size(448, 21);
            this.txtTargeSite.TabIndex = 11;
            // 
            // lblCurrentDirectory
            // 
            this.lblCurrentDirectory.AutoSize = true;
            this.lblCurrentDirectory.Location = new System.Drawing.Point(78, 247);
            this.lblCurrentDirectory.Name = "lblCurrentDirectory";
            this.lblCurrentDirectory.Size = new System.Drawing.Size(59, 12);
            this.lblCurrentDirectory.TabIndex = 12;
            this.lblCurrentDirectory.Text = "当前路径:";
            // 
            // lblMachineName
            // 
            this.lblMachineName.AutoSize = true;
            this.lblMachineName.Location = new System.Drawing.Point(90, 270);
            this.lblMachineName.Name = "lblMachineName";
            this.lblMachineName.Size = new System.Drawing.Size(47, 12);
            this.lblMachineName.TabIndex = 14;
            this.lblMachineName.Text = "机器名:";
            // 
            // lblOSVersion
            // 
            this.lblOSVersion.AutoSize = true;
            this.lblOSVersion.Location = new System.Drawing.Point(78, 295);
            this.lblOSVersion.Name = "lblOSVersion";
            this.lblOSVersion.Size = new System.Drawing.Size(59, 12);
            this.lblOSVersion.TabIndex = 15;
            this.lblOSVersion.Text = "操作系统:";
            // 
            // lblSystemDirectory
            // 
            this.lblSystemDirectory.AutoSize = true;
            this.lblSystemDirectory.Location = new System.Drawing.Point(78, 318);
            this.lblSystemDirectory.Name = "lblSystemDirectory";
            this.lblSystemDirectory.Size = new System.Drawing.Size(59, 12);
            this.lblSystemDirectory.TabIndex = 16;
            this.lblSystemDirectory.Text = "系统路径:";
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.Location = new System.Drawing.Point(90, 340);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(47, 12);
            this.lblUserName.TabIndex = 18;
            this.lblUserName.Text = "用户名:";
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Location = new System.Drawing.Point(78, 362);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(59, 12);
            this.lblVersion.TabIndex = 19;
            this.lblVersion.Text = ".NET版本:";
            // 
            // btnAbort
            // 
            this.btnAbort.Location = new System.Drawing.Point(336, 389);
            this.btnAbort.Name = "btnAbort";
            this.btnAbort.Size = new System.Drawing.Size(120, 21);
            this.btnAbort.TabIndex = 20;
            this.btnAbort.Text = "中止程序运行(&A)";
            this.btnAbort.UseVisualStyleBackColor = true;
            this.btnAbort.Click += new System.EventHandler(this.btnAbort_Click);
            // 
            // btnIgnore
            // 
            this.btnIgnore.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnIgnore.Location = new System.Drawing.Point(462, 389);
            this.btnIgnore.Name = "btnIgnore";
            this.btnIgnore.Size = new System.Drawing.Size(120, 21);
            this.btnIgnore.TabIndex = 21;
            this.btnIgnore.Text = "忽略当前错误(&I)";
            this.btnIgnore.UseVisualStyleBackColor = true;
            this.btnIgnore.Click += new System.EventHandler(this.btnIgnore_Click);
            // 
            // txtMessage
            // 
            this.txtMessage.BackColor = System.Drawing.SystemColors.Window;
            this.txtMessage.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtMessage.Location = new System.Drawing.Point(134, 32);
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.ReadOnly = true;
            this.txtMessage.Size = new System.Drawing.Size(448, 21);
            this.txtMessage.TabIndex = 3;
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Location = new System.Drawing.Point(102, 35);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(35, 12);
            this.lblMessage.TabIndex = 2;
            this.lblMessage.Text = "信息:";
            // 
            // lblStackTrace
            // 
            this.lblStackTrace.AutoSize = true;
            this.lblStackTrace.Location = new System.Drawing.Point(102, 104);
            this.lblStackTrace.Name = "lblStackTrace";
            this.lblStackTrace.Size = new System.Drawing.Size(35, 12);
            this.lblStackTrace.TabIndex = 8;
            this.lblStackTrace.Text = "堆栈:";
            // 
            // lblTargeSite
            // 
            this.lblTargeSite.AutoSize = true;
            this.lblTargeSite.Location = new System.Drawing.Point(102, 223);
            this.lblTargeSite.Name = "lblTargeSite";
            this.lblTargeSite.Size = new System.Drawing.Size(35, 12);
            this.lblTargeSite.TabIndex = 10;
            this.lblTargeSite.Text = "方法:";
            // 
            // txtSource
            // 
            this.txtSource.BackColor = System.Drawing.SystemColors.Window;
            this.txtSource.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtSource.Location = new System.Drawing.Point(134, 77);
            this.txtSource.Name = "txtSource";
            this.txtSource.ReadOnly = true;
            this.txtSource.Size = new System.Drawing.Size(448, 21);
            this.txtSource.TabIndex = 7;
            // 
            // linkLblHelpLink
            // 
            this.linkLblHelpLink.AutoSize = true;
            this.linkLblHelpLink.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.linkLblHelpLink.Location = new System.Drawing.Point(132, 58);
            this.linkLblHelpLink.Name = "linkLblHelpLink";
            this.linkLblHelpLink.Size = new System.Drawing.Size(0, 12);
            this.linkLblHelpLink.TabIndex = 5;
            this.linkLblHelpLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLblHelp_LinkClicked);
            // 
            // linkLblCurrentDirectory
            // 
            this.linkLblCurrentDirectory.AutoSize = true;
            this.linkLblCurrentDirectory.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.linkLblCurrentDirectory.Location = new System.Drawing.Point(132, 247);
            this.linkLblCurrentDirectory.Name = "linkLblCurrentDirectory";
            this.linkLblCurrentDirectory.Size = new System.Drawing.Size(0, 12);
            this.linkLblCurrentDirectory.TabIndex = 13;
            this.linkLblCurrentDirectory.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLblCurrentDirectory_LinkClicked);
            // 
            // linkLblSystemDirectory
            // 
            this.linkLblSystemDirectory.AutoSize = true;
            this.linkLblSystemDirectory.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.linkLblSystemDirectory.Location = new System.Drawing.Point(132, 318);
            this.linkLblSystemDirectory.Name = "linkLblSystemDirectory";
            this.linkLblSystemDirectory.Size = new System.Drawing.Size(0, 12);
            this.linkLblSystemDirectory.TabIndex = 17;
            this.linkLblSystemDirectory.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLblSystemDirectory_LinkClicked);
            // 
            // lblInfo
            // 
            this.lblInfo.Location = new System.Drawing.Point(12, 9);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(570, 12);
            this.lblInfo.TabIndex = 1;
            this.lblInfo.Text = "{0} 遇到问题需要关闭。我们对此引起的不便表示抱歉。请将此问题报告给 {1}。";
            this.lblInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pbEnvironment
            // 
            this.pbEnvironment.Image = ((System.Drawing.Image)(resources.GetObject("pbEnvironment.Image")));
            this.pbEnvironment.Location = new System.Drawing.Point(12, 247);
            this.pbEnvironment.Name = "pbEnvironment";
            this.pbEnvironment.Size = new System.Drawing.Size(60, 60);
            this.pbEnvironment.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbEnvironment.TabIndex = 0;
            this.pbEnvironment.TabStop = false;
            // 
            // pbInfo
            // 
            this.pbInfo.Image = ((System.Drawing.Image)(resources.GetObject("pbInfo.Image")));
            this.pbInfo.Location = new System.Drawing.Point(12, 32);
            this.pbInfo.Name = "pbInfo";
            this.pbInfo.Size = new System.Drawing.Size(60, 60);
            this.pbInfo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbInfo.TabIndex = 0;
            this.pbInfo.TabStop = false;
            // 
            // frmGlobalException
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnIgnore;
            this.ClientSize = new System.Drawing.Size(594, 422);
            this.Controls.Add(this.linkLblSystemDirectory);
            this.Controls.Add(this.linkLblCurrentDirectory);
            this.Controls.Add(this.linkLblHelpLink);
            this.Controls.Add(this.btnIgnore);
            this.Controls.Add(this.btnAbort);
            this.Controls.Add(this.txtSource);
            this.Controls.Add(this.txtMessage);
            this.Controls.Add(this.txtTargeSite);
            this.Controls.Add(this.txtStackTrace);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.lblUserName);
            this.Controls.Add(this.lblSystemDirectory);
            this.Controls.Add(this.lblOSVersion);
            this.Controls.Add(this.lblMachineName);
            this.Controls.Add(this.lblCurrentDirectory);
            this.Controls.Add(this.lblTargeSite);
            this.Controls.Add(this.lblStackTrace);
            this.Controls.Add(this.lblSource);
            this.Controls.Add(this.lblHelpLink);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.pbEnvironment);
            this.Controls.Add(this.pbInfo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmGlobalException";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "程序错误";
            ((System.ComponentModel.ISupportInitialize)(this.pbEnvironment)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbInfo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbInfo;
        private System.Windows.Forms.PictureBox pbEnvironment;
        private System.Windows.Forms.Label lblHelpLink;
        private System.Windows.Forms.Label lblSource;
        private System.Windows.Forms.TextBox txtStackTrace;
        private System.Windows.Forms.TextBox txtTargeSite;
        private System.Windows.Forms.Label lblCurrentDirectory;
        private System.Windows.Forms.Label lblMachineName;
        private System.Windows.Forms.Label lblOSVersion;
        private System.Windows.Forms.Label lblSystemDirectory;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Button btnAbort;
        private System.Windows.Forms.Button btnIgnore;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Label lblStackTrace;
        private System.Windows.Forms.Label lblTargeSite;
        private System.Windows.Forms.TextBox txtSource;
        private System.Windows.Forms.LinkLabel linkLblHelpLink;
        private System.Windows.Forms.LinkLabel linkLblCurrentDirectory;
        private System.Windows.Forms.LinkLabel linkLblSystemDirectory;
        private System.Windows.Forms.Label lblInfo;
    }
}