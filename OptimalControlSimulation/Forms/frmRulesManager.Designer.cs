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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRulesManager));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolStrip_oc = new System.Windows.Forms.ToolStrip();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtn_rule_add = new System.Windows.Forms.ToolStripButton();
            this.tsbtn_rule_edit = new System.Windows.Forms.ToolStripButton();
            this.tsbtn_rule_delete = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtn_rule_update = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtn_rule_paras = new System.Windows.Forms.ToolStripButton();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.status_Label = new System.Windows.Forms.ToolStripStatusLabel();
            this.dgv_oc_rules = new System.Windows.Forms.DataGridView();
            this.toolStrip_oc.SuspendLayout();
            this.statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_oc_rules)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip_oc
            // 
            this.toolStrip_oc.AutoSize = false;
            this.toolStrip_oc.ImageScalingSize = new System.Drawing.Size(25, 25);
            this.toolStrip_oc.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton4,
            this.tsbtn_rule_add,
            this.tsbtn_rule_edit,
            this.tsbtn_rule_delete,
            this.toolStripButton1,
            this.tsbtn_rule_update,
            this.toolStripSeparator6,
            this.tsbtn_rule_paras});
            this.toolStrip_oc.Location = new System.Drawing.Point(0, 0);
            this.toolStrip_oc.Name = "toolStrip_oc";
            this.toolStrip_oc.Size = new System.Drawing.Size(1004, 45);
            this.toolStrip_oc.TabIndex = 5;
            this.toolStrip_oc.Text = "toolStrip1";
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(6, 45);
            // 
            // tsbtn_rule_add
            // 
            this.tsbtn_rule_add.Image = ((System.Drawing.Image)(resources.GetObject("tsbtn_rule_add.Image")));
            this.tsbtn_rule_add.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtn_rule_add.Name = "tsbtn_rule_add";
            this.tsbtn_rule_add.Size = new System.Drawing.Size(85, 42);
            this.tsbtn_rule_add.Text = "增加规则";
            this.tsbtn_rule_add.ToolTipText = "增加规则";
            this.tsbtn_rule_add.Click += new System.EventHandler(this.tsbtn_rule_add_Click);
            // 
            // tsbtn_rule_edit
            // 
            this.tsbtn_rule_edit.Image = ((System.Drawing.Image)(resources.GetObject("tsbtn_rule_edit.Image")));
            this.tsbtn_rule_edit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtn_rule_edit.Name = "tsbtn_rule_edit";
            this.tsbtn_rule_edit.Size = new System.Drawing.Size(85, 42);
            this.tsbtn_rule_edit.Text = "编辑规则";
            this.tsbtn_rule_edit.Click += new System.EventHandler(this.tsbtn_rule_edit_Click);
            // 
            // tsbtn_rule_delete
            // 
            this.tsbtn_rule_delete.Image = ((System.Drawing.Image)(resources.GetObject("tsbtn_rule_delete.Image")));
            this.tsbtn_rule_delete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtn_rule_delete.Name = "tsbtn_rule_delete";
            this.tsbtn_rule_delete.Size = new System.Drawing.Size(85, 42);
            this.tsbtn_rule_delete.Text = "删除规则";
            this.tsbtn_rule_delete.Click += new System.EventHandler(this.tsbtn_rule_delete_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(6, 45);
            // 
            // tsbtn_rule_update
            // 
            this.tsbtn_rule_update.Image = ((System.Drawing.Image)(resources.GetObject("tsbtn_rule_update.Image")));
            this.tsbtn_rule_update.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtn_rule_update.Name = "tsbtn_rule_update";
            this.tsbtn_rule_update.Size = new System.Drawing.Size(85, 42);
            this.tsbtn_rule_update.Text = "刷新列表";
            this.tsbtn_rule_update.Click += new System.EventHandler(this.tsbtn_rule_update_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 45);
            // 
            // tsbtn_rule_paras
            // 
            this.tsbtn_rule_paras.Image = ((System.Drawing.Image)(resources.GetObject("tsbtn_rule_paras.Image")));
            this.tsbtn_rule_paras.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtn_rule_paras.Name = "tsbtn_rule_paras";
            this.tsbtn_rule_paras.Size = new System.Drawing.Size(85, 42);
            this.tsbtn_rule_paras.Text = "管理变量";
            this.tsbtn_rule_paras.Click += new System.EventHandler(this.tsbtn_rule_paras_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.status_Label});
            this.statusStrip.Location = new System.Drawing.Point(0, 706);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1004, 22);
            this.statusStrip.TabIndex = 7;
            this.statusStrip.Text = "statusStrip1";
            // 
            // status_Label
            // 
            this.status_Label.Name = "status_Label";
            this.status_Label.Size = new System.Drawing.Size(0, 17);
            // 
            // dgv_oc_rules
            // 
            this.dgv_oc_rules.AllowUserToAddRows = false;
            this.dgv_oc_rules.AllowUserToDeleteRows = false;
            this.dgv_oc_rules.AllowUserToOrderColumns = true;
            this.dgv_oc_rules.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
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
            this.dgv_oc_rules.Location = new System.Drawing.Point(0, 45);
            this.dgv_oc_rules.MultiSelect = false;
            this.dgv_oc_rules.Name = "dgv_oc_rules";
            this.dgv_oc_rules.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_oc_rules.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgv_oc_rules.RowHeadersVisible = false;
            this.dgv_oc_rules.RowTemplate.Height = 23;
            this.dgv_oc_rules.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_oc_rules.Size = new System.Drawing.Size(1004, 661);
            this.dgv_oc_rules.TabIndex = 8;
            this.dgv_oc_rules.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_oc_rules_CellDoubleClick);
            // 
            // frmRulesManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1004, 728);
            this.Controls.Add(this.dgv_oc_rules);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.toolStrip_oc);
            this.MinimizeBox = false;
            this.Name = "frmRulesManager";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "规则管理";
            this.toolStrip_oc.ResumeLayout(false);
            this.toolStrip_oc.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_oc_rules)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip_oc;
        private System.Windows.Forms.ToolStripSeparator toolStripButton4;
        private System.Windows.Forms.ToolStripButton tsbtn_rule_add;
        private System.Windows.Forms.ToolStripButton tsbtn_rule_edit;
        private System.Windows.Forms.ToolStripButton tsbtn_rule_delete;
        private System.Windows.Forms.ToolStripSeparator toolStripButton1;
        private System.Windows.Forms.ToolStripButton tsbtn_rule_update;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripButton tsbtn_rule_paras;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel status_Label;
        private System.Windows.Forms.DataGridView dgv_oc_rules;
    }
}