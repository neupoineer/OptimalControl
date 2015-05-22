namespace OptimalControl.Forms
{
    partial class frmOperatorManager
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
            this.btnSubmit = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.chkOperatorState = new System.Windows.Forms.CheckBox();
            this.gbOperatorInfo = new System.Windows.Forms.GroupBox();
            this.txtValidatePwd = new System.Windows.Forms.TextBox();
            this.txtOperatorPwd = new System.Windows.Forms.TextBox();
            this.lblValidatePwd = new System.Windows.Forms.Label();
            this.txtOperatorName = new System.Windows.Forms.TextBox();
            this.lblOperatorPwd = new System.Windows.Forms.Label();
            this.lblOperatorName = new System.Windows.Forms.Label();
            this.gbOperatorInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(156, 140);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(60, 21);
            this.btnSubmit.TabIndex = 2;
            this.btnSubmit.Text = "添加";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnASubmit_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(222, 140);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(60, 21);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // toolTip
            // 
            this.toolTip.ShowAlways = true;
            // 
            // chkOperatorState
            // 
            this.chkOperatorState.AutoSize = true;
            this.chkOperatorState.Location = new System.Drawing.Point(12, 143);
            this.chkOperatorState.Name = "chkOperatorState";
            this.chkOperatorState.Size = new System.Drawing.Size(126, 16);
            this.chkOperatorState.TabIndex = 1;
            this.chkOperatorState.Text = "同时激活此用户(&A)";
            this.chkOperatorState.UseVisualStyleBackColor = true;
            // 
            // gbOperatorInfo
            // 
            this.gbOperatorInfo.Controls.Add(this.txtValidatePwd);
            this.gbOperatorInfo.Controls.Add(this.txtOperatorPwd);
            this.gbOperatorInfo.Controls.Add(this.lblValidatePwd);
            this.gbOperatorInfo.Controls.Add(this.txtOperatorName);
            this.gbOperatorInfo.Controls.Add(this.lblOperatorPwd);
            this.gbOperatorInfo.Controls.Add(this.lblOperatorName);
            this.gbOperatorInfo.Location = new System.Drawing.Point(12, 12);
            this.gbOperatorInfo.Name = "gbOperatorInfo";
            this.gbOperatorInfo.Size = new System.Drawing.Size(270, 115);
            this.gbOperatorInfo.TabIndex = 0;
            this.gbOperatorInfo.TabStop = false;
            this.gbOperatorInfo.Text = "用户信息";
            // 
            // txtValidatePwd
            // 
            this.txtValidatePwd.Location = new System.Drawing.Point(108, 81);
            this.txtValidatePwd.Name = "txtValidatePwd";
            this.txtValidatePwd.PasswordChar = '*';
            this.txtValidatePwd.Size = new System.Drawing.Size(135, 21);
            this.txtValidatePwd.TabIndex = 5;
            // 
            // txtOperatorPwd
            // 
            this.txtOperatorPwd.Location = new System.Drawing.Point(108, 51);
            this.txtOperatorPwd.Name = "txtOperatorPwd";
            this.txtOperatorPwd.PasswordChar = '*';
            this.txtOperatorPwd.Size = new System.Drawing.Size(135, 21);
            this.txtOperatorPwd.TabIndex = 3;
            // 
            // lblValidatePwd
            // 
            this.lblValidatePwd.AutoSize = true;
            this.lblValidatePwd.Location = new System.Drawing.Point(25, 84);
            this.lblValidatePwd.Name = "lblValidatePwd";
            this.lblValidatePwd.Size = new System.Drawing.Size(77, 12);
            this.lblValidatePwd.TabIndex = 4;
            this.lblValidatePwd.Text = "确认密码(&P):";
            // 
            // txtOperatorName
            // 
            this.txtOperatorName.Location = new System.Drawing.Point(108, 21);
            this.txtOperatorName.Name = "txtOperatorName";
            this.txtOperatorName.Size = new System.Drawing.Size(135, 21);
            this.txtOperatorName.TabIndex = 1;
            // 
            // lblOperatorPwd
            // 
            this.lblOperatorPwd.AutoSize = true;
            this.lblOperatorPwd.Location = new System.Drawing.Point(25, 54);
            this.lblOperatorPwd.Name = "lblOperatorPwd";
            this.lblOperatorPwd.Size = new System.Drawing.Size(77, 12);
            this.lblOperatorPwd.TabIndex = 2;
            this.lblOperatorPwd.Text = "用户密码(&P):";
            // 
            // lblOperatorName
            // 
            this.lblOperatorName.AutoSize = true;
            this.lblOperatorName.Location = new System.Drawing.Point(25, 24);
            this.lblOperatorName.Name = "lblOperatorName";
            this.lblOperatorName.Size = new System.Drawing.Size(77, 12);
            this.lblOperatorName.TabIndex = 0;
            this.lblOperatorName.Text = "登录名称(&N):";
            // 
            // frmOperatorManager
            // 
            this.AcceptButton = this.btnSubmit;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(294, 175);
            this.Controls.Add(this.chkOperatorState);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.gbOperatorInfo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(300, 200);
            this.Name = "frmOperatorManager";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "添加用户";
            this.gbOperatorInfo.ResumeLayout(false);
            this.gbOperatorInfo.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.CheckBox chkOperatorState;
        private System.Windows.Forms.GroupBox gbOperatorInfo;
        private System.Windows.Forms.TextBox txtValidatePwd;
        private System.Windows.Forms.TextBox txtOperatorPwd;
        private System.Windows.Forms.Label lblValidatePwd;
        private System.Windows.Forms.TextBox txtOperatorName;
        private System.Windows.Forms.Label lblOperatorPwd;
        private System.Windows.Forms.Label lblOperatorName;

    }
}