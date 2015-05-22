namespace OptimalControl.Forms
{
    partial class frmAddRightsRelation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAddRightsRelation));
            this.gbRelationInfo = new System.Windows.Forms.GroupBox();
            this.lblLinkBottom = new System.Windows.Forms.Label();
            this.lblLinkTop = new System.Windows.Forms.Label();
            this.pbLink = new System.Windows.Forms.PictureBox();
            this.cboGroupName = new System.Windows.Forms.ComboBox();
            this.cboOperatorName = new System.Windows.Forms.ComboBox();
            this.lblGroupName = new System.Windows.Forms.Label();
            this.lblOperatorName = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.gbRelationInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbLink)).BeginInit();
            this.SuspendLayout();
            // 
            // gbRelationInfo
            // 
            this.gbRelationInfo.Controls.Add(this.lblLinkBottom);
            this.gbRelationInfo.Controls.Add(this.lblLinkTop);
            this.gbRelationInfo.Controls.Add(this.pbLink);
            this.gbRelationInfo.Controls.Add(this.cboGroupName);
            this.gbRelationInfo.Controls.Add(this.cboOperatorName);
            this.gbRelationInfo.Controls.Add(this.lblGroupName);
            this.gbRelationInfo.Controls.Add(this.lblOperatorName);
            this.gbRelationInfo.Location = new System.Drawing.Point(12, 12);
            this.gbRelationInfo.Name = "gbRelationInfo";
            this.gbRelationInfo.Size = new System.Drawing.Size(270, 115);
            this.gbRelationInfo.TabIndex = 0;
            this.gbRelationInfo.TabStop = false;
            this.gbRelationInfo.Text = "关系信息";
            // 
            // lblLinkBottom
            // 
            this.lblLinkBottom.AutoSize = true;
            this.lblLinkBottom.Location = new System.Drawing.Point(224, 70);
            this.lblLinkBottom.Name = "lblLinkBottom";
            this.lblLinkBottom.Size = new System.Drawing.Size(17, 12);
            this.lblLinkBottom.TabIndex = 3;
            this.lblLinkBottom.Text = "┛";
            // 
            // lblLinkTop
            // 
            this.lblLinkTop.AutoSize = true;
            this.lblLinkTop.Location = new System.Drawing.Point(225, 37);
            this.lblLinkTop.Name = "lblLinkTop";
            this.lblLinkTop.Size = new System.Drawing.Size(17, 12);
            this.lblLinkTop.TabIndex = 3;
            this.lblLinkTop.Text = "┓";
            // 
            // pbLink
            // 
            this.pbLink.Image = ((System.Drawing.Image)(resources.GetObject("pbLink.Image")));
            this.pbLink.Location = new System.Drawing.Point(225, 52);
            this.pbLink.Name = "pbLink";
            this.pbLink.Size = new System.Drawing.Size(16, 16);
            this.pbLink.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbLink.TabIndex = 2;
            this.pbLink.TabStop = false;
            // 
            // cboGroupName
            // 
            this.cboGroupName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboGroupName.FormattingEnabled = true;
            this.cboGroupName.Location = new System.Drawing.Point(93, 67);
            this.cboGroupName.Name = "cboGroupName";
            this.cboGroupName.Size = new System.Drawing.Size(126, 20);
            this.cboGroupName.TabIndex = 1;
            // 
            // cboOperatorName
            // 
            this.cboOperatorName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboOperatorName.FormattingEnabled = true;
            this.cboOperatorName.Location = new System.Drawing.Point(93, 34);
            this.cboOperatorName.Name = "cboOperatorName";
            this.cboOperatorName.Size = new System.Drawing.Size(126, 20);
            this.cboOperatorName.TabIndex = 1;
            // 
            // lblGroupName
            // 
            this.lblGroupName.AutoSize = true;
            this.lblGroupName.Location = new System.Drawing.Point(25, 70);
            this.lblGroupName.Name = "lblGroupName";
            this.lblGroupName.Size = new System.Drawing.Size(65, 12);
            this.lblGroupName.TabIndex = 0;
            this.lblGroupName.Text = "权限组(&G):";
            // 
            // lblOperatorName
            // 
            this.lblOperatorName.AutoSize = true;
            this.lblOperatorName.Location = new System.Drawing.Point(25, 37);
            this.lblOperatorName.Name = "lblOperatorName";
            this.lblOperatorName.Size = new System.Drawing.Size(65, 12);
            this.lblOperatorName.TabIndex = 0;
            this.lblOperatorName.Text = "操作员(&O):";
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(149, 140);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(60, 21);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.Text = "添加";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(222, 140);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(60, 21);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // frmAddRightsRelation
            // 
            this.AcceptButton = this.btnAdd;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(294, 175);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.gbRelationInfo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(300, 200);
            this.Name = "frmAddRightsRelation";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "添加权限关系";
            this.Load += new System.EventHandler(this.frmAddRightsRelation_Load);
            this.gbRelationInfo.ResumeLayout(false);
            this.gbRelationInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbLink)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbRelationInfo;
        private System.Windows.Forms.Label lblGroupName;
        private System.Windows.Forms.Label lblOperatorName;
        private System.Windows.Forms.ComboBox cboOperatorName;
        private System.Windows.Forms.ComboBox cboGroupName;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.PictureBox pbLink;
        private System.Windows.Forms.Label lblLinkTop;
        private System.Windows.Forms.Label lblLinkBottom;
    }
}