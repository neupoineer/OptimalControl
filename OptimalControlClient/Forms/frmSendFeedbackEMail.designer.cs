namespace OptimalControl.Forms
{
    partial class frmSendFeedbackEMail
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
            this.gbEmailAddress = new System.Windows.Forms.GroupBox();
            this.lblConnection = new System.Windows.Forms.Label();
            this.cboAddressSuffix = new System.Windows.Forms.ComboBox();
            this.txtAddressPrefix = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.gbEmailAddress.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbEmailAddress
            // 
            this.gbEmailAddress.Controls.Add(this.lblConnection);
            this.gbEmailAddress.Controls.Add(this.cboAddressSuffix);
            this.gbEmailAddress.Controls.Add(this.txtAddressPrefix);
            this.gbEmailAddress.Location = new System.Drawing.Point(12, 12);
            this.gbEmailAddress.Name = "gbEmailAddress";
            this.gbEmailAddress.Size = new System.Drawing.Size(270, 53);
            this.gbEmailAddress.TabIndex = 0;
            this.gbEmailAddress.TabStop = false;
            this.gbEmailAddress.Text = "请输入您的邮件地址";
            // 
            // lblConnection
            // 
            this.lblConnection.AutoSize = true;
            this.lblConnection.Font = new System.Drawing.Font("Arial", 9F);
            this.lblConnection.Location = new System.Drawing.Point(159, 22);
            this.lblConnection.Name = "lblConnection";
            this.lblConnection.Size = new System.Drawing.Size(19, 15);
            this.lblConnection.TabIndex = 1;
            this.lblConnection.Text = "@";
            // 
            // cboAddressSuffix
            // 
            this.cboAddressSuffix.FormattingEnabled = true;
            this.cboAddressSuffix.Items.AddRange(new object[] {
            "gmail.com",
            "yahoo.com.cn",
            "163.com",
            "126.com",
            "sina.com.cn",
            "hotmail.com",
            "qq.com"});
            this.cboAddressSuffix.Location = new System.Drawing.Point(184, 21);
            this.cboAddressSuffix.Name = "cboAddressSuffix";
            this.cboAddressSuffix.Size = new System.Drawing.Size(80, 20);
            this.cboAddressSuffix.TabIndex = 2;
            // 
            // txtAddressPrefix
            // 
            this.txtAddressPrefix.Location = new System.Drawing.Point(6, 20);
            this.txtAddressPrefix.Name = "txtAddressPrefix";
            this.txtAddressPrefix.Size = new System.Drawing.Size(147, 21);
            this.txtAddressPrefix.TabIndex = 0;
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(156, 80);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(60, 21);
            this.btnSend.TabIndex = 1;
            this.btnSend.Text = "发送(&S)";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(222, 80);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(60, 21);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "取消(&C)";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // frmSendFeedbackEMail
            // 
            this.AcceptButton = this.btnSend;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(294, 113);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.gbEmailAddress);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSendFeedbackEMail";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "信息反馈";
            this.gbEmailAddress.ResumeLayout(false);
            this.gbEmailAddress.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbEmailAddress;
        private System.Windows.Forms.Label lblConnection;
        private System.Windows.Forms.ComboBox cboAddressSuffix;
        private System.Windows.Forms.TextBox txtAddressPrefix;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Button btnCancel;

    }
}