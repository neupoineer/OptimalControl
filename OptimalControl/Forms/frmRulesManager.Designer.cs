namespace OptimalControl.Forms
{
    partial class frmRulesManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDevicesManager));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.tsbtn_device_add = new System.Windows.Forms.ToolStripButton();
            this.tsbtn_device_edit = new System.Windows.Forms.ToolStripButton();
            this.tsbtn_device_delete = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtn_device_update = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripSeparator();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.tssl_device_manager = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dataGridView_devices = new System.Windows.Forms.DataGridView();
            this.toolStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_devices)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip
            // 
            this.toolStrip.ImageScalingSize = new System.Drawing.Size(30, 30);
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtn_device_add,
            this.tsbtn_device_edit,
            this.tsbtn_device_delete,
            this.toolStripButton1,
            this.tsbtn_device_update,
            this.toolStripButton2});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(510, 52);
            this.toolStrip.TabIndex = 3;
            this.toolStrip.Text = "toolStrip1";
            // 
            // tsbtn_device_add
            // 
            this.tsbtn_device_add.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.tsbtn_device_add.Image = ((System.Drawing.Image)(resources.GetObject("tsbtn_device_add.Image")));
            this.tsbtn_device_add.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtn_device_add.Name = "tsbtn_device_add";
            this.tsbtn_device_add.Size = new System.Drawing.Size(59, 49);
            this.tsbtn_device_add.Text = "添加设备";
            this.tsbtn_device_add.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbtn_device_add.ToolTipText = "添加设备";
            this.tsbtn_device_add.Click += new System.EventHandler(this.tsbtn_device_add_Click);
            // 
            // tsbtn_device_edit
            // 
            this.tsbtn_device_edit.Image = ((System.Drawing.Image)(resources.GetObject("tsbtn_device_edit.Image")));
            this.tsbtn_device_edit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtn_device_edit.Name = "tsbtn_device_edit";
            this.tsbtn_device_edit.Size = new System.Drawing.Size(59, 49);
            this.tsbtn_device_edit.Text = "编辑设备";
            this.tsbtn_device_edit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbtn_device_edit.Click += new System.EventHandler(this.tsbtn_device_edit_Click);
            // 
            // tsbtn_device_delete
            // 
            this.tsbtn_device_delete.Image = ((System.Drawing.Image)(resources.GetObject("tsbtn_device_delete.Image")));
            this.tsbtn_device_delete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtn_device_delete.Name = "tsbtn_device_delete";
            this.tsbtn_device_delete.Size = new System.Drawing.Size(59, 49);
            this.tsbtn_device_delete.Text = "删除设备";
            this.tsbtn_device_delete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbtn_device_delete.Click += new System.EventHandler(this.tsbtn_device_delete_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(6, 52);
            // 
            // tsbtn_device_update
            // 
            this.tsbtn_device_update.Image = ((System.Drawing.Image)(resources.GetObject("tsbtn_device_update.Image")));
            this.tsbtn_device_update.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtn_device_update.Name = "tsbtn_device_update";
            this.tsbtn_device_update.Size = new System.Drawing.Size(59, 49);
            this.tsbtn_device_update.Text = "刷新列表";
            this.tsbtn_device_update.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbtn_device_update.Click += new System.EventHandler(this.tsbtn_device_update_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(6, 52);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tssl_device_manager});
            this.statusStrip.Location = new System.Drawing.Point(0, 338);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(510, 22);
            this.statusStrip.TabIndex = 4;
            this.statusStrip.Text = "statusStrip1";
            // 
            // tssl_device_manager
            // 
            this.tssl_device_manager.Name = "tssl_device_manager";
            this.tssl_device_manager.Size = new System.Drawing.Size(0, 17);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dataGridView_devices);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 52);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(510, 286);
            this.panel1.TabIndex = 5;
            // 
            // dataGridView_devices
            // 
            this.dataGridView_devices.AllowUserToAddRows = false;
            this.dataGridView_devices.AllowUserToDeleteRows = false;
            this.dataGridView_devices.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView_devices.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridView_devices.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView_devices.DefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridView_devices.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_devices.Location = new System.Drawing.Point(0, 0);
            this.dataGridView_devices.MultiSelect = false;
            this.dataGridView_devices.Name = "dataGridView_devices";
            this.dataGridView_devices.ReadOnly = true;
            this.dataGridView_devices.RowHeadersVisible = false;
            this.dataGridView_devices.RowTemplate.Height = 23;
            this.dataGridView_devices.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView_devices.Size = new System.Drawing.Size(510, 286);
            this.dataGridView_devices.TabIndex = 2;
            this.dataGridView_devices.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_devices_CellDoubleClick);
            // 
            // frmDevicesManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(510, 360);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.toolStrip);
            this.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(520, 390);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(520, 390);
            this.Name = "frmDevicesManager";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "设备管理";
            this.Load += new System.EventHandler(this.frmDevicesManager_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.frmDevicesManager_KeyPress);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_devices)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton tsbtn_device_add;
        private System.Windows.Forms.ToolStripButton tsbtn_device_edit;
        private System.Windows.Forms.ToolStripButton tsbtn_device_delete;
        private System.Windows.Forms.ToolStripButton tsbtn_device_update;
        private System.Windows.Forms.ToolStripSeparator toolStripButton1;
        private System.Windows.Forms.ToolStripSeparator toolStripButton2;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel tssl_device_manager;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dataGridView_devices;

    }
}