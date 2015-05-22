namespace OptimalControl.Forms
{
    partial class frmRightsManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRightsManager));
            this.scRightsManager = new System.Windows.Forms.SplitContainer();
            this.gbOperatorList = new System.Windows.Forms.GroupBox();
            this.dgvOperatorList = new System.Windows.Forms.DataGridView();
            this.gbRightsList = new System.Windows.Forms.GroupBox();
            this.dgvRightsList = new System.Windows.Forms.DataGridView();
            this.gbRightsView = new System.Windows.Forms.GroupBox();
            this.tvRightsView = new System.Windows.Forms.TreeView();
            this.cmsRightsTreeView = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiSelectAll = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCancelAll = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiExpandAll = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCollapseAll = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiReload = new System.Windows.Forms.ToolStripMenuItem();
            this.tsRightsManager = new System.Windows.Forms.ToolStrip();
            this.tsbtnRefreshRightsList = new System.Windows.Forms.ToolStripButton();
            this.tsbtnRefreshOperator = new System.Windows.Forms.ToolStripButton();
            this.tsSpr1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtnAddOperator = new System.Windows.Forms.ToolStripButton();
            this.tsbtnDeleteOperator = new System.Windows.Forms.ToolStripButton();
            this.tsSpr2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtnRightsMode = new System.Windows.Forms.ToolStripSplitButton();
            this.tsmiGroupMode = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOperatorMode = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbtnRightsGroupManager = new System.Windows.Forms.ToolStripButton();
            this.tsSpr3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtnSaveAllChanged = new System.Windows.Forms.ToolStripButton();
            this.tsbtnCloseWindow = new System.Windows.Forms.ToolStripButton();
            this.scRightsManager.Panel1.SuspendLayout();
            this.scRightsManager.Panel2.SuspendLayout();
            this.scRightsManager.SuspendLayout();
            this.gbOperatorList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOperatorList)).BeginInit();
            this.gbRightsList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRightsList)).BeginInit();
            this.gbRightsView.SuspendLayout();
            this.cmsRightsTreeView.SuspendLayout();
            this.tsRightsManager.SuspendLayout();
            this.SuspendLayout();
            // 
            // scRightsManager
            // 
            this.scRightsManager.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.scRightsManager.BackColor = System.Drawing.Color.Transparent;
            this.scRightsManager.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.scRightsManager.Location = new System.Drawing.Point(0, 62);
            this.scRightsManager.Name = "scRightsManager";
            // 
            // scRightsManager.Panel1
            // 
            this.scRightsManager.Panel1.Controls.Add(this.gbOperatorList);
            this.scRightsManager.Panel1.Controls.Add(this.gbRightsList);
            this.scRightsManager.Panel1MinSize = 5;
            // 
            // scRightsManager.Panel2
            // 
            this.scRightsManager.Panel2.Controls.Add(this.gbRightsView);
            this.scRightsManager.Panel2MinSize = 5;
            this.scRightsManager.Size = new System.Drawing.Size(632, 391);
            this.scRightsManager.SplitterDistance = 453;
            this.scRightsManager.TabIndex = 0;
            // 
            // gbOperatorList
            // 
            this.gbOperatorList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbOperatorList.Controls.Add(this.dgvOperatorList);
            this.gbOperatorList.Location = new System.Drawing.Point(12, 259);
            this.gbOperatorList.Name = "gbOperatorList";
            this.gbOperatorList.Size = new System.Drawing.Size(440, 120);
            this.gbOperatorList.TabIndex = 13;
            this.gbOperatorList.TabStop = false;
            this.gbOperatorList.Text = "操作员列表";
            // 
            // dgvOperatorList
            // 
            this.dgvOperatorList.AllowUserToAddRows = false;
            this.dgvOperatorList.AllowUserToDeleteRows = false;
            this.dgvOperatorList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvOperatorList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOperatorList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvOperatorList.Location = new System.Drawing.Point(3, 17);
            this.dgvOperatorList.MultiSelect = false;
            this.dgvOperatorList.Name = "dgvOperatorList";
            this.dgvOperatorList.RowHeadersWidth = 25;
            this.dgvOperatorList.RowTemplate.Height = 23;
            this.dgvOperatorList.Size = new System.Drawing.Size(434, 100);
            this.dgvOperatorList.TabIndex = 5;
            this.dgvOperatorList.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvOperatorList_CellEndEdit);
            this.dgvOperatorList.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgvOperatorList_CellValidating);
            this.dgvOperatorList.SelectionChanged += new System.EventHandler(this.dgvOperatorList_SelectionChanged);
            // 
            // gbRightsList
            // 
            this.gbRightsList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbRightsList.Controls.Add(this.dgvRightsList);
            this.gbRightsList.Location = new System.Drawing.Point(11, 7);
            this.gbRightsList.Name = "gbRightsList";
            this.gbRightsList.Size = new System.Drawing.Size(440, 246);
            this.gbRightsList.TabIndex = 12;
            this.gbRightsList.TabStop = false;
            this.gbRightsList.Text = "权限列表";
            // 
            // dgvRightsList
            // 
            this.dgvRightsList.AllowUserToAddRows = false;
            this.dgvRightsList.AllowUserToDeleteRows = false;
            this.dgvRightsList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvRightsList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRightsList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvRightsList.Location = new System.Drawing.Point(3, 17);
            this.dgvRightsList.Name = "dgvRightsList";
            this.dgvRightsList.RowHeadersWidth = 25;
            this.dgvRightsList.RowTemplate.Height = 23;
            this.dgvRightsList.Size = new System.Drawing.Size(434, 226);
            this.dgvRightsList.TabIndex = 1;
            this.dgvRightsList.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dgvRightsList_CellBeginEdit);
            this.dgvRightsList.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvRightsList_CellEndEdit);
            this.dgvRightsList.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgvRightsList_CellValidating);
            this.dgvRightsList.SelectionChanged += new System.EventHandler(this.dgvRightsList_SelectionChanged);
            // 
            // gbRightsView
            // 
            this.gbRightsView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbRightsView.Controls.Add(this.tvRightsView);
            this.gbRightsView.Location = new System.Drawing.Point(3, 7);
            this.gbRightsView.Name = "gbRightsView";
            this.gbRightsView.Size = new System.Drawing.Size(162, 372);
            this.gbRightsView.TabIndex = 15;
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
            this.tvRightsView.LabelEdit = true;
            this.tvRightsView.Location = new System.Drawing.Point(3, 17);
            this.tvRightsView.Name = "tvRightsView";
            this.tvRightsView.ShowNodeToolTips = true;
            this.tvRightsView.Size = new System.Drawing.Size(156, 352);
            this.tvRightsView.TabIndex = 4;
            this.tvRightsView.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.tvRightsView_AfterLabelEdit);
            this.tvRightsView.BeforeCheck += new System.Windows.Forms.TreeViewCancelEventHandler(this.tvRightsView_BeforeCheck);
            this.tvRightsView.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.tvRightsView_AfterCheck);
            this.tvRightsView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvRightsView_AfterSelect);
            // 
            // cmsRightsTreeView
            // 
            this.cmsRightsTreeView.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiSelectAll,
            this.tsmiCancelAll,
            this.toolStripSeparator1,
            this.tsmiExpandAll,
            this.tsmiCollapseAll,
            this.toolStripSeparator2,
            this.tsmiReload});
            this.cmsRightsTreeView.Name = "cmsTreeView";
            this.cmsRightsTreeView.Size = new System.Drawing.Size(153, 148);
            this.cmsRightsTreeView.Text = "权限视图右键菜单";
            // 
            // tsmiSelectAll
            // 
            this.tsmiSelectAll.Image = ((System.Drawing.Image)(resources.GetObject("tsmiSelectAll.Image")));
            this.tsmiSelectAll.Name = "tsmiSelectAll";
            this.tsmiSelectAll.Size = new System.Drawing.Size(152, 22);
            this.tsmiSelectAll.Text = "全部勾选(&S)";
            this.tsmiSelectAll.Click += new System.EventHandler(this.tsmiTreeViewContentMenuItem_Click);
            // 
            // tsmiCancelAll
            // 
            this.tsmiCancelAll.Image = ((System.Drawing.Image)(resources.GetObject("tsmiCancelAll.Image")));
            this.tsmiCancelAll.Name = "tsmiCancelAll";
            this.tsmiCancelAll.Size = new System.Drawing.Size(152, 22);
            this.tsmiCancelAll.Text = "全部取消(&C)";
            this.tsmiCancelAll.Click += new System.EventHandler(this.tsmiTreeViewContentMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // tsmiExpandAll
            // 
            this.tsmiExpandAll.Name = "tsmiExpandAll";
            this.tsmiExpandAll.Size = new System.Drawing.Size(152, 22);
            this.tsmiExpandAll.Text = "全部展开(&E)";
            this.tsmiExpandAll.Click += new System.EventHandler(this.tsmiTreeViewContentMenuItem_Click);
            // 
            // tsmiCollapseAll
            // 
            this.tsmiCollapseAll.Name = "tsmiCollapseAll";
            this.tsmiCollapseAll.Size = new System.Drawing.Size(152, 22);
            this.tsmiCollapseAll.Text = "全部折叠(&X)";
            this.tsmiCollapseAll.Click += new System.EventHandler(this.tsmiTreeViewContentMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(149, 6);
            // 
            // tsmiReload
            // 
            this.tsmiReload.Image = ((System.Drawing.Image)(resources.GetObject("tsmiReload.Image")));
            this.tsmiReload.Name = "tsmiReload";
            this.tsmiReload.Size = new System.Drawing.Size(152, 22);
            this.tsmiReload.Text = "重新加载(&R)";
            this.tsmiReload.Click += new System.EventHandler(this.tsmiTreeViewContentMenuItem_Click);
            // 
            // tsRightsManager
            // 
            this.tsRightsManager.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsRightsManager.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnRefreshRightsList,
            this.tsbtnRefreshOperator,
            this.tsSpr1,
            this.tsbtnAddOperator,
            this.tsbtnDeleteOperator,
            this.tsSpr2,
            this.tsbtnRightsMode,
            this.tsbtnRightsGroupManager,
            this.tsSpr3,
            this.tsbtnSaveAllChanged,
            this.tsbtnCloseWindow});
            this.tsRightsManager.Location = new System.Drawing.Point(0, 0);
            this.tsRightsManager.Name = "tsRightsManager";
            this.tsRightsManager.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.tsRightsManager.Size = new System.Drawing.Size(632, 64);
            this.tsRightsManager.TabIndex = 1;
            this.tsRightsManager.Text = "权限管理工具栏";
            // 
            // tsbtnRefreshRightsList
            // 
            this.tsbtnRefreshRightsList.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnRefreshRightsList.Image")));
            this.tsbtnRefreshRightsList.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbtnRefreshRightsList.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnRefreshRightsList.Name = "tsbtnRefreshRightsList";
            this.tsbtnRefreshRightsList.Size = new System.Drawing.Size(60, 61);
            this.tsbtnRefreshRightsList.Text = "刷新权限";
            this.tsbtnRefreshRightsList.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbtnRefreshRightsList.Click += new System.EventHandler(this.tsbtnRefreshRightsList_Click);
            // 
            // tsbtnRefreshOperator
            // 
            this.tsbtnRefreshOperator.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnRefreshOperator.Image")));
            this.tsbtnRefreshOperator.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbtnRefreshOperator.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnRefreshOperator.Name = "tsbtnRefreshOperator";
            this.tsbtnRefreshOperator.Size = new System.Drawing.Size(60, 61);
            this.tsbtnRefreshOperator.Text = "刷新用户";
            this.tsbtnRefreshOperator.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbtnRefreshOperator.Click += new System.EventHandler(this.tsbtnRefreshOperator_Click);
            // 
            // tsSpr1
            // 
            this.tsSpr1.Name = "tsSpr1";
            this.tsSpr1.Size = new System.Drawing.Size(6, 64);
            // 
            // tsbtnAddOperator
            // 
            this.tsbtnAddOperator.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnAddOperator.Image")));
            this.tsbtnAddOperator.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbtnAddOperator.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnAddOperator.Name = "tsbtnAddOperator";
            this.tsbtnAddOperator.Size = new System.Drawing.Size(60, 61);
            this.tsbtnAddOperator.Text = "添加用户";
            this.tsbtnAddOperator.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbtnAddOperator.Click += new System.EventHandler(this.tsbtnAddOperator_Click);
            // 
            // tsbtnDeleteOperator
            // 
            this.tsbtnDeleteOperator.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnDeleteOperator.Image")));
            this.tsbtnDeleteOperator.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbtnDeleteOperator.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnDeleteOperator.Name = "tsbtnDeleteOperator";
            this.tsbtnDeleteOperator.Size = new System.Drawing.Size(60, 61);
            this.tsbtnDeleteOperator.Text = "删除用户";
            this.tsbtnDeleteOperator.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbtnDeleteOperator.Click += new System.EventHandler(this.tsbtnDeleteOperator_Click);
            // 
            // tsSpr2
            // 
            this.tsSpr2.Name = "tsSpr2";
            this.tsSpr2.Size = new System.Drawing.Size(6, 64);
            // 
            // tsbtnRightsMode
            // 
            this.tsbtnRightsMode.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiGroupMode,
            this.tsmiOperatorMode});
            this.tsbtnRightsMode.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnRightsMode.Image")));
            this.tsbtnRightsMode.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbtnRightsMode.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnRightsMode.Name = "tsbtnRightsMode";
            this.tsbtnRightsMode.Size = new System.Drawing.Size(72, 61);
            this.tsbtnRightsMode.Text = "权限模式";
            this.tsbtnRightsMode.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbtnRightsMode.ButtonClick += new System.EventHandler(this.tsbtnRightsMode_ButtonClick);
            // 
            // tsmiGroupMode
            // 
            this.tsmiGroupMode.Checked = true;
            this.tsmiGroupMode.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tsmiGroupMode.Name = "tsmiGroupMode";
            this.tsmiGroupMode.Size = new System.Drawing.Size(124, 22);
            this.tsmiGroupMode.Text = "分组模式";
            this.tsmiGroupMode.ToolTipText = "使用基于角色的权限管理方案";
            this.tsmiGroupMode.Click += new System.EventHandler(this.tsmiRightsMode_Click);
            // 
            // tsmiOperatorMode
            // 
            this.tsmiOperatorMode.Name = "tsmiOperatorMode";
            this.tsmiOperatorMode.Size = new System.Drawing.Size(124, 22);
            this.tsmiOperatorMode.Text = "用户模式";
            this.tsmiOperatorMode.ToolTipText = "使用基于用户的权限管理方案";
            this.tsmiOperatorMode.Click += new System.EventHandler(this.tsmiRightsMode_Click);
            // 
            // tsbtnRightsGroupManager
            // 
            this.tsbtnRightsGroupManager.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnRightsGroupManager.Image")));
            this.tsbtnRightsGroupManager.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbtnRightsGroupManager.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnRightsGroupManager.Name = "tsbtnRightsGroupManager";
            this.tsbtnRightsGroupManager.Size = new System.Drawing.Size(60, 61);
            this.tsbtnRightsGroupManager.Text = "分组管理";
            this.tsbtnRightsGroupManager.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbtnRightsGroupManager.Click += new System.EventHandler(this.tsbtnRightsGroupManager_Click);
            // 
            // tsSpr3
            // 
            this.tsSpr3.Name = "tsSpr3";
            this.tsSpr3.Size = new System.Drawing.Size(6, 64);
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
            // frmRightsManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(632, 453);
            this.Controls.Add(this.tsRightsManager);
            this.Controls.Add(this.scRightsManager);
            this.DoubleBuffered = true;
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(640, 480);
            this.Name = "frmRightsManager";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "权限管理";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmRightsManager_FormClosed);
            this.Load += new System.EventHandler(this.frmRightsManager_Load);
            this.scRightsManager.Panel1.ResumeLayout(false);
            this.scRightsManager.Panel2.ResumeLayout(false);
            this.scRightsManager.ResumeLayout(false);
            this.gbOperatorList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOperatorList)).EndInit();
            this.gbRightsList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRightsList)).EndInit();
            this.gbRightsView.ResumeLayout(false);
            this.cmsRightsTreeView.ResumeLayout(false);
            this.tsRightsManager.ResumeLayout(false);
            this.tsRightsManager.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer scRightsManager;
        private System.Windows.Forms.GroupBox gbOperatorList;
        private System.Windows.Forms.DataGridView dgvOperatorList;
        private System.Windows.Forms.GroupBox gbRightsList;
        private System.Windows.Forms.DataGridView dgvRightsList;
        private System.Windows.Forms.GroupBox gbRightsView;
        private System.Windows.Forms.TreeView tvRightsView;
        private System.Windows.Forms.ContextMenuStrip cmsRightsTreeView;
        private System.Windows.Forms.ToolStripMenuItem tsmiSelectAll;
        private System.Windows.Forms.ToolStripMenuItem tsmiCancelAll;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem tsmiExpandAll;
        private System.Windows.Forms.ToolStripMenuItem tsmiCollapseAll;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem tsmiReload;
        private System.Windows.Forms.ToolStrip tsRightsManager;
        private System.Windows.Forms.ToolStripButton tsbtnRefreshRightsList;
        private System.Windows.Forms.ToolStripButton tsbtnRefreshOperator;
        private System.Windows.Forms.ToolStripButton tsbtnSaveAllChanged;
        private System.Windows.Forms.ToolStripButton tsbtnCloseWindow;
        private System.Windows.Forms.ToolStripSeparator tsSpr1;
        private System.Windows.Forms.ToolStripSeparator tsSpr2;
        private System.Windows.Forms.ToolStripButton tsbtnAddOperator;
        private System.Windows.Forms.ToolStripButton tsbtnDeleteOperator;
        private System.Windows.Forms.ToolStripButton tsbtnRightsGroupManager;
        private System.Windows.Forms.ToolStripSeparator tsSpr3;
        private System.Windows.Forms.ToolStripSplitButton tsbtnRightsMode;
        private System.Windows.Forms.ToolStripMenuItem tsmiOperatorMode;
        private System.Windows.Forms.ToolStripMenuItem tsmiGroupMode;

    }
}