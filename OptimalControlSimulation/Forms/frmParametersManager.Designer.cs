namespace OptimalControl.Forms
{
    partial class frmParametersManager
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmParametersManager));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbtn_para_add = new System.Windows.Forms.ToolStripButton();
            this.tsbtn_para_edit = new System.Windows.Forms.ToolStripButton();
            this.tsbtn_para_delete = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtn_para_update = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtn_para_devices = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tssl_parameters_manager = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.treeView = new System.Windows.Forms.TreeView();
            this.dataGridView_parameters = new System.Windows.Forms.DataGridView();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_parameters)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(30, 30);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtn_para_add,
            this.tsbtn_para_edit,
            this.tsbtn_para_delete,
            this.toolStripButton1,
            this.tsbtn_para_update,
            this.toolStripSeparator1,
            this.tsbtn_para_devices});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1008, 52);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbtn_para_add
            // 
            this.tsbtn_para_add.Image = ((System.Drawing.Image)(resources.GetObject("tsbtn_para_add.Image")));
            this.tsbtn_para_add.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtn_para_add.Name = "tsbtn_para_add";
            this.tsbtn_para_add.Size = new System.Drawing.Size(59, 49);
            this.tsbtn_para_add.Text = "增加变量";
            this.tsbtn_para_add.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbtn_para_add.ToolTipText = "增加变量";
            this.tsbtn_para_add.Click += new System.EventHandler(this.tsbtn_para_add_Click);
            // 
            // tsbtn_para_edit
            // 
            this.tsbtn_para_edit.Image = ((System.Drawing.Image)(resources.GetObject("tsbtn_para_edit.Image")));
            this.tsbtn_para_edit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtn_para_edit.Name = "tsbtn_para_edit";
            this.tsbtn_para_edit.Size = new System.Drawing.Size(59, 49);
            this.tsbtn_para_edit.Text = "编辑变量";
            this.tsbtn_para_edit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbtn_para_edit.Click += new System.EventHandler(this.tsbtn_para_edit_Click);
            // 
            // tsbtn_para_delete
            // 
            this.tsbtn_para_delete.Image = ((System.Drawing.Image)(resources.GetObject("tsbtn_para_delete.Image")));
            this.tsbtn_para_delete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtn_para_delete.Name = "tsbtn_para_delete";
            this.tsbtn_para_delete.Size = new System.Drawing.Size(59, 49);
            this.tsbtn_para_delete.Text = "删除变量";
            this.tsbtn_para_delete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbtn_para_delete.Click += new System.EventHandler(this.tsbtn_para_delete_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(6, 52);
            // 
            // tsbtn_para_update
            // 
            this.tsbtn_para_update.Image = ((System.Drawing.Image)(resources.GetObject("tsbtn_para_update.Image")));
            this.tsbtn_para_update.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtn_para_update.Name = "tsbtn_para_update";
            this.tsbtn_para_update.Size = new System.Drawing.Size(59, 49);
            this.tsbtn_para_update.Text = "刷新列表";
            this.tsbtn_para_update.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbtn_para_update.Click += new System.EventHandler(this.tsbtn_para_update_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 52);
            // 
            // tsbtn_para_devices
            // 
            this.tsbtn_para_devices.Image = ((System.Drawing.Image)(resources.GetObject("tsbtn_para_devices.Image")));
            this.tsbtn_para_devices.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtn_para_devices.Name = "tsbtn_para_devices";
            this.tsbtn_para_devices.Size = new System.Drawing.Size(59, 49);
            this.tsbtn_para_devices.Text = "管理设备";
            this.tsbtn_para_devices.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbtn_para_devices.Click += new System.EventHandler(this.tsbtn_para_devices_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tssl_parameters_manager});
            this.statusStrip1.Location = new System.Drawing.Point(0, 710);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 12, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1008, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tssl_parameters_manager
            // 
            this.tssl_parameters_manager.Name = "tssl_parameters_manager";
            this.tssl_parameters_manager.Size = new System.Drawing.Size(0, 17);
            this.tssl_parameters_manager.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer.IsSplitterFixed = true;
            this.splitContainer.Location = new System.Drawing.Point(0, 52);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.treeView);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.dataGridView_parameters);
            this.splitContainer.Size = new System.Drawing.Size(1008, 658);
            this.splitContainer.SplitterDistance = 160;
            this.splitContainer.TabIndex = 2;
            // 
            // treeView
            // 
            this.treeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.treeView.ItemHeight = 25;
            this.treeView.Location = new System.Drawing.Point(0, 0);
            this.treeView.Name = "treeView";
            this.treeView.Size = new System.Drawing.Size(160, 658);
            this.treeView.TabIndex = 0;
            this.treeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterSelect);
            // 
            // dataGridView_parameters
            // 
            this.dataGridView_parameters.AllowUserToAddRows = false;
            this.dataGridView_parameters.AllowUserToDeleteRows = false;
            this.dataGridView_parameters.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView_parameters.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView_parameters.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView_parameters.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView_parameters.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_parameters.Location = new System.Drawing.Point(0, 0);
            this.dataGridView_parameters.MultiSelect = false;
            this.dataGridView_parameters.Name = "dataGridView_parameters";
            this.dataGridView_parameters.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView_parameters.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView_parameters.RowHeadersVisible = false;
            this.dataGridView_parameters.RowTemplate.Height = 23;
            this.dataGridView_parameters.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView_parameters.Size = new System.Drawing.Size(844, 658);
            this.dataGridView_parameters.TabIndex = 0;
            this.dataGridView_parameters.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_parameters_CellDoubleClick);
            // 
            // frmParametersManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 732);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.KeyPreview = true;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "frmParametersManager";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "变量管理";
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.frmParametersManager_KeyPress);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_parameters)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbtn_para_add;
        private System.Windows.Forms.ToolStripButton tsbtn_para_edit;
        private System.Windows.Forms.ToolStripButton tsbtn_para_delete;
        private System.Windows.Forms.ToolStripButton tsbtn_para_update;
        private System.Windows.Forms.ToolStripSeparator toolStripButton1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbtn_para_devices;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tssl_parameters_manager;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.TreeView treeView;
        private System.Windows.Forms.DataGridView dataGridView_parameters;
    }
}