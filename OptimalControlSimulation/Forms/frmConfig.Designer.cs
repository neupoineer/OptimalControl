using Utility;

namespace OptimalControl.Forms
{
    partial class frmConfig
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
            this.btn_OK = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.tabPage_other = new System.Windows.Forms.TabPage();
            this.tb_UpdateVariableTime = new Utility.NumbericTextbox();
            this.label16 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.tabPage_other.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_OK
            // 
            this.btn_OK.Location = new System.Drawing.Point(310, 381);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(87, 27);
            this.btn_OK.TabIndex = 4;
            this.btn_OK.Text = "确定";
            this.btn_OK.UseVisualStyleBackColor = true;
            this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Cancel.Location = new System.Drawing.Point(405, 381);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(87, 27);
            this.btn_Cancel.TabIndex = 4;
            this.btn_Cancel.Text = "取消";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tabControl);
            this.panel1.Location = new System.Drawing.Point(14, 14);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(478, 360);
            this.panel1.TabIndex = 6;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPage_other);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(478, 360);
            this.tabControl.TabIndex = 5;
            // 
            // groupBox6
            // 
            this.groupBox6.BackColor = System.Drawing.Color.Transparent;
            this.groupBox6.Controls.Add(this.tb_UpdateVariableTime);
            this.groupBox6.Controls.Add(this.label16);
            this.groupBox6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox6.Location = new System.Drawing.Point(3, 3);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(464, 326);
            this.groupBox6.TabIndex = 37;
            this.groupBox6.TabStop = false;
            // 
            // tabPage_other
            // 
            this.tabPage_other.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage_other.Controls.Add(this.groupBox6);
            this.tabPage_other.Location = new System.Drawing.Point(4, 24);
            this.tabPage_other.Name = "tabPage_other";
            this.tabPage_other.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_other.Size = new System.Drawing.Size(470, 332);
            this.tabPage_other.TabIndex = 2;
            this.tabPage_other.Text = "其它设置";
            // 
            // tb_UpdateVariableTime
            // 
            this.tb_UpdateVariableTime.Location = new System.Drawing.Point(131, 32);
            this.tb_UpdateVariableTime.Name = "tb_UpdateVariableTime";
            this.tb_UpdateVariableTime.Size = new System.Drawing.Size(50, 23);
            this.tb_UpdateVariableTime.TabIndex = 27;
            this.tb_UpdateVariableTime.Text = "10000";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(40, 35);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(259, 14);
            this.label16.TabIndex = 26;
            this.label16.Text = "数据更新间隔        毫秒 (≥500毫秒)";
            // 
            // frmConfig
            // 
            this.AcceptButton = this.btn_OK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_Cancel;
            this.ClientSize = new System.Drawing.Size(504, 411);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_OK);
            this.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(520, 450);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(520, 450);
            this.Name = "frmConfig";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = " ";
            this.panel1.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.tabPage_other.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_OK;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPage_other;
        private System.Windows.Forms.GroupBox groupBox6;
        private NumbericTextbox tb_UpdateVariableTime;
        private System.Windows.Forms.Label label16;
    }
}