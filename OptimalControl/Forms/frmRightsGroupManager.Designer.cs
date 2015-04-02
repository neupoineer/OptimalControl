namespace OptimalControl.Forms
{
    partial class frmRightsGroupManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRightsGroupManager));
            this.scRightsGroupManager = new System.Windows.Forms.SplitContainer();
            this.gbGroupView = new System.Windows.Forms.GroupBox();
            this.tvGroupView = new System.Windows.Forms.TreeView();
            this.gbRightsRelationList = new System.Windows.Forms.GroupBox();
            this.dgvRightsRelationList = new System.Windows.Forms.DataGridView();
            this.gbGroupList = new System.Windows.Forms.GroupBox();
            this.dgvGroupList = new System.Windows.Forms.DataGridView();
            this.gbRightsView = new System.Windows.Forms.GroupBox();
            this.tvRightsView = new System.Windows.Forms.TreeView();
            this.tsbtnRefreshGroup = new System.Windows.Forms.ToolStripButton();
            this.tsSpr1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtnAddGroup = new System.Windows.Forms.ToolStripButton();
            this.tsbtnDeleteGroup = new System.Windows.Forms.ToolStripButton();
            this.tsSpr2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtnSaveAllChanged = new System.Windows.Forms.ToolStripButton();
            this.tsbtnCloseWindow = new System.Windows.Forms.ToolStripButton();
            this.tsRightsGroupManager = new System.Windows.Forms.ToolStrip();
            this.tsbtnAddRelation = new System.Windows.Forms.ToolStripButton();
            this.tsbtnDeleteRelation = new System.Windows.Forms.ToolStripButton();
            this.tsSpr3 = new System.Windows.Forms.ToolStripSeparator();
            this.scRightsGroupManager.Panel1.SuspendLayout();
            this.scRightsGroupManager.Panel2.SuspendLayout();
            this.scRightsGroupManager.SuspendLayout();
            this.gbGroupView.SuspendLayout();
            this.gbRightsRelationList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRightsRelationList)).BeginInit();
            this.gbGroupList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGroupList)).BeginInit();
            this.gbRightsView.SuspendLayout();
            this.tsRightsGroupManager.SuspendLayout();
            this.SuspendLayout();
            // 
            // scRightsGroupManager
            // 
            this.scRightsGroupManager.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.scRightsGroupManager.BackColor = System.Drawing.Color.Transparent;
            this.scRightsGroupManager.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.scRightsGroupManager.Location = new System.Drawing.Point(0, 62);
            this.scRightsGroupManager.Name = "scRightsGroupManager";
            // 
            // scRightsGroupManager.Panel1
            // 
            this.scRightsGroupManager.Panel1.Controls.Add(this.gbGroupView);
            this.scRightsGroupManager.Panel1.Controls.Add(this.gbRightsRelationList);
            this.scRightsGroupManager.Panel1.Controls.Add(this.gbGroupList);
            this.scRightsGroupManager.Panel1MinSize = 145;
            // 
            // scRightsGroupManager.Panel2
            // 
            this.scRightsGroupManager.Panel2.Controls.Add(this.gbRightsView);
            this.scRightsGroupManager.Panel2MinSize = 5;
            this.scRightsGroupManager.Size = new System.Drawing.Size(632, 390);
            this.scRightsGroupManager.SplitterDistance = 472;
            this.scRightsGroupManager.TabIndex = 0;
            // 
            // gbGroupView
            // 
            this.gbGroupView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbGroupView.Controls.Add(this.tvGroupView);
            this.gbGroupView.Location = new System.Drawing.Point(339, 7);
            this.gbGroupView.Name = "gbGroupView";
            this.gbGroupView.Size = new System.Drawing.Size(130, 372);
            this.gbGroupView.TabIndex = 5;
            this.gbGroupView.TabStop = false;
            this.gbGroupView.Text = "权限组视图";
            // 
            // tvGroupView
            // 
            this.tvGroupView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvGroupView.FullRowSelect = true;
            this.tvGroupView.HotTracking = true;
            this.tvGroupView.Location = new System.Drawing.Point(3, 17);
            this.tvGroupView.Name = "tvGroupView";
            this.tvGroupView.ShowNodeToolTips = true;
            this.tvGroupView.Size = new System.Drawing.Size(124, 352);
            this.tvGroupView.TabIndex = 5;
            this.tvGroupView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvGroupView_AfterSelect);
            // 
            // gbRightsRelationList
            // 
            this.gbRightsRelationList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbRightsRelationList.Controls.Add(this.dgvRightsRelationList);
            this.gbRightsRelationList.Location = new System.Drawing.Point(12, 143);
            this.gbRightsRelationList.Name = "gbRightsRelationList";
            this.gbRightsRelationList.Size = new System.Drawing.Size(321, 236);
            this.gbRightsRelationList.TabIndex = 4;
            this.gbRightsRelationList.TabStop = false;
            this.gbRightsRelationList.Text = "权限关系列表";
            // 
            // dgvRightsRelationList
            // 
            this.dgvRightsRelationList.AllowUserToAddRows = false;
            this.dgvRightsRelationList.AllowUserToDeleteRows = false;
            this.dgvRightsRelationList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvRightsRelationList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRightsRelationList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvRightsRelationList.Location = new System.Drawing.Point(3, 17);
            this.dgvRightsRelationList.Name = "dgvRightsRelationList";
            this.dgvRightsRelationList.RowHeadersWidth = 25;
            this.dgvRightsRelationList.RowTemplate.Height = 23;
            this.dgvRightsRelationList.Size = new System.Drawing.Size(315, 216);
            this.dgvRightsRelationList.TabIndex = 2;
            this.dgvRightsRelationList.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvRightsRelationList_CellEndEdit);
            // 
            // gbGroupList
            // 
            this.gbGroupList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbGroupList.Controls.Add(this.dgvGroupList);
            this.gbGroupList.Location = new System.Drawing.Point(12, 7);
            this.gbGroupList.Name = "gbGroupList";
            this.gbGroupList.Size = new System.Drawing.Size(321, 130);
            this.gbGroupList.TabIndex = 3;
            this.gbGroupList.TabStop = false;
            this.gbGroupList.Text = "权限组列表";
            // 
            // dgvGroupList
            // 
            this.dgvGroupList.AllowUserToAddRows = false;
            this.dgvGroupList.AllowUserToDeleteRows = false;
            this.dgvGroupList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvGroupList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvGroupList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvGroupList.Location = new System.Drawing.Point(3, 17);
            this.dgvGroupList.MultiSelect = false;
            this.dgvGroupList.Name = "dgvGroupList";
            this.dgvGroupList.RowHeadersWidth = 25;
            this.dgvGroupList.RowTemplate.Height = 23;
            this.dgvGroupList.Size = new System.Drawing.Size(315, 110);
            this.dgvGroupList.TabIndex = 2;
            this.dgvGroupList.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dgvGroupList_CellBeginEdit);
            this.dgvGroupList.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvGroupList_CellEndEdit);
            this.dgvGroupList.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgvGroupList_CellValidating);
            this.dgvGroupList.SelectionChanged += new System.EventHandler(this.dgvGroupList_SelectionChanged);
            // 
            // gbRightsView
            // 
            this.gbRightsView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbRightsView.Controls.Add(this.tvRightsView);
            this.gbRightsView.Location = new System.Drawing.Point(3, 7);
            this.gbRightsView.Name = "gbRightsView";
            this.gbRightsView.Size = new System.Drawing.Size(141, 372);
            this.gbRightsView.TabIndex = 16;
            this.gbRightsView.TabStop = false;
            this.gbRightsView.Text = "权限视图";
            // 
            // tvRightsView
            // 
            this.tvRightsView.CheckBoxes = true;
            this.tvRightsView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvRightsView.FullRowSelect = true;
            this.tvRightsView.HideSelection = false;
            this.tvRightsView.HotTracking = true;
            this.tvRightsView.Location = new System.Drawing.Point(3, 17);
            this.tvRightsView.Name = "tvRightsView";
            this.tvRightsView.ShowNodeToolTips = true;
            this.tvRightsView.Size = new System.Drawing.Size(135, 352);
            this.tvRightsView.TabIndex = 4;
            this.tvRightsView.BeforeCheck += new System.Windows.Forms.TreeViewCancelEventHandler(this.tvRightsView_BeforeCheck);
            this.tvRightsView.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.tvRightsView_AfterCheck);
            // 
            // tsbtnRefreshGroup
            // 
            this.tsbtnRefreshGroup.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnRefreshGroup.Image")));
            this.tsbtnRefreshGroup.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbtnRefreshGroup.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnRefreshGroup.Name = "tsbtnRefreshGroup";
            this.tsbtnRefreshGroup.Size = new System.Drawing.Size(60, 61);
            this.tsbtnRefreshGroup.Text = "刷新分组";
            this.tsbtnRefreshGroup.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbtnRefreshGroup.Click += new System.EventHandler(this.tsbtnRefreshGroup_Click);
            // 
            // tsSpr1
            // 
            this.tsSpr1.Name = "tsSpr1";
            this.tsSpr1.Size = new System.Drawing.Size(6, 64);
            // 
            // tsbtnAddGroup
            // 
            this.tsbtnAddGroup.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnAddGroup.Image")));
            this.tsbtnAddGroup.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbtnAddGroup.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnAddGroup.Name = "tsbtnAddGroup";
            this.tsbtnAddGroup.Size = new System.Drawing.Size(60, 61);
            this.tsbtnAddGroup.Text = "添加分组";
            this.tsbtnAddGroup.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbtnAddGroup.Click += new System.EventHandler(this.tsbtnAddGroup_Click);
            // 
            // tsbtnDeleteGroup
            // 
            this.tsbtnDeleteGroup.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnDeleteGroup.Image")));
            this.tsbtnDeleteGroup.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbtnDeleteGroup.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnDeleteGroup.Name = "tsbtnDeleteGroup";
            this.tsbtnDeleteGroup.Size = new System.Drawing.Size(60, 61);
            this.tsbtnDeleteGroup.Text = "删除分组";
            this.tsbtnDeleteGroup.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbtnDeleteGroup.Click += new System.EventHandler(this.tsbtnDeleteGroup_Click);
            // 
            // tsSpr2
            // 
            this.tsSpr2.Name = "tsSpr2";
            this.tsSpr2.Size = new System.Drawing.Size(6, 64);
            // 
            // tsbtnSaveAllChanged
            // 
            this.tsbtnSaveAllChanged.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnSaveAllChanged.Image")));
            this.tsbtnSaveAllChanged.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbtnSaveAllChanged.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnSaveAllChanged.Name = "tsbtnSaveAllChanged";
            this.tsbtnSaveAllChanged.Size = new System.Drawing.Size(60, 61);
            this.tsbtnSaveAllChanged.Text = "保存更改";
            this.tsbtnSaveAllChanged.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbtnSaveAllChanged.Click += new System.EventHandler(this.tsbtnSaveAllChanged_Click);
            // 
            // tsbtnCloseWindow
            // 
            this.tsbtnCloseWindow.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnCloseWindow.Image")));
            this.tsbtnCloseWindow.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbtnCloseWindow.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnCloseWindow.Name = "tsbtnCloseWindow";
            this.tsbtnCloseWindow.Size = new System.Drawing.Size(60, 61);
            this.tsbtnCloseWindow.Text = "关闭窗口";
            this.tsbtnCloseWindow.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbtnCloseWindow.Click += new System.EventHandler(this.tsbtnCloseWindow_Click);
            // 
            // tsRightsGroupManager
            // 
            this.tsRightsGroupManager.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsRightsGroupManager.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnRefreshGroup,
            this.tsSpr1,
            this.tsbtnAddGroup,
            this.tsbtnDeleteGroup,
            this.tsSpr2,
            this.tsbtnAddRelation,
            this.tsbtnDeleteRelation,
            this.tsSpr3,
            this.tsbtnSaveAllChanged,
            this.tsbtnCloseWindow});
            this.tsRightsGroupManager.Location = new System.Drawing.Point(0, 0);
            this.tsRightsGroupManager.Name = "tsRightsGroupManager";
            this.tsRightsGroupManager.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.tsRightsGroupManager.Size = new System.Drawing.Size(632, 64);
            this.tsRightsGroupManager.TabIndex = 2;
            this.tsRightsGroupManager.Text = "权限管理工具栏";
            // 
            // tsbtnAddRelation
            // 
            this.tsbtnAddRelation.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnAddRelation.Image")));
            this.tsbtnAddRelation.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbtnAddRelation.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnAddRelation.Name = "tsbtnAddRelation";
            this.tsbtnAddRelation.Size = new System.Drawing.Size(60, 61);
            this.tsbtnAddRelation.Text = "添加关系";
            this.tsbtnAddRelation.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbtnAddRelation.Click += new System.EventHandler(this.tsbtnAddRelation_Click);
            // 
            // tsbtnDeleteRelation
            // 
            this.tsbtnDeleteRelation.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnDeleteRelation.Image")));
            this.tsbtnDeleteRelation.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbtnDeleteRelation.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnDeleteRelation.Name = "tsbtnDeleteRelation";
            this.tsbtnDeleteRelation.Size = new System.Drawing.Size(60, 61);
            this.tsbtnDeleteRelation.Text = "删除关系";
            this.tsbtnDeleteRelation.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbtnDeleteRelation.Click += new System.EventHandler(this.tsbtnDeleteRelation_Click);
            // 
            // tsSpr3
            // 
            this.tsSpr3.Name = "tsSpr3";
            this.tsSpr3.Size = new System.Drawing.Size(6, 64);
            // 
            // frmRightsGroupManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(632, 453);
            this.Controls.Add(this.tsRightsGroupManager);
            this.Controls.Add(this.scRightsGroupManager);
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(640, 480);
            this.Name = "frmRightsGroupManager";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "权限组管理";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmRightsGroupManager_FormClosing);
            this.Load += new System.EventHandler(this.frmRightsGroupManager_Load);
            this.scRightsGroupManager.Panel1.ResumeLayout(false);
            this.scRightsGroupManager.Panel2.ResumeLayout(false);
            this.scRightsGroupManager.ResumeLayout(false);
            this.gbGroupView.ResumeLayout(false);
            this.gbRightsRelationList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRightsRelationList)).EndInit();
            this.gbGroupList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvGroupList)).EndInit();
            this.gbRightsView.ResumeLayout(false);
            this.tsRightsGroupManager.ResumeLayout(false);
            this.tsRightsGroupManager.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer scRightsGroupManager;
        private System.Windows.Forms.GroupBox gbGroupList;
        private System.Windows.Forms.DataGridView dgvGroupList;
        private System.Windows.Forms.GroupBox gbRightsRelationList;
        private System.Windows.Forms.DataGridView dgvRightsRelationList;
        private System.Windows.Forms.GroupBox gbRightsView;
        private System.Windows.Forms.TreeView tvRightsView;
        private System.Windows.Forms.GroupBox gbGroupView;
        private System.Windows.Forms.TreeView tvGroupView;
        private System.Windows.Forms.ToolStripButton tsbtnRefreshGroup;
        private System.Windows.Forms.ToolStripSeparator tsSpr1;
        private System.Windows.Forms.ToolStripButton tsbtnAddGroup;
        private System.Windows.Forms.ToolStripButton tsbtnDeleteGroup;
        private System.Windows.Forms.ToolStripSeparator tsSpr2;
        private System.Windows.Forms.ToolStripButton tsbtnSaveAllChanged;
        private System.Windows.Forms.ToolStripButton tsbtnCloseWindow;
        private System.Windows.Forms.ToolStrip tsRightsGroupManager;
        private System.Windows.Forms.ToolStripButton tsbtnDeleteRelation;
        private System.Windows.Forms.ToolStripButton tsbtnAddRelation;
        private System.Windows.Forms.ToolStripSeparator tsSpr3;
    }
}