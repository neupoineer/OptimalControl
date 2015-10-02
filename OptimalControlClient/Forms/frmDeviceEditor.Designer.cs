using Utility;

namespace OptimalControl.Forms
{
    partial class frmDeviceEditor
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
            this.btn_ok = new System.Windows.Forms.Button();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tb_device_ip = new System.Windows.Forms.TextBox();
            this.cb_device_sync = new System.Windows.Forms.CheckBox();
            this.cb_device_state = new System.Windows.Forms.CheckBox();
            this.ntb_device_port = new Utility.NumbericTextbox();
            this.tb_device_name = new System.Windows.Forms.TextBox();
            this.nud_device_unitid = new System.Windows.Forms.NumericUpDown();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nud_device_unitid)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_ok
            // 
            this.btn_ok.Location = new System.Drawing.Point(94, 169);
            this.btn_ok.Name = "btn_ok";
            this.btn_ok.Size = new System.Drawing.Size(75, 23);
            this.btn_ok.TabIndex = 6;
            this.btn_ok.Text = "确定";
            this.btn_ok.UseVisualStyleBackColor = true;
            this.btn_ok.Click += new System.EventHandler(this.btn_ok_Click);
            // 
            // btn_cancel
            // 
            this.btn_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_cancel.Location = new System.Drawing.Point(234, 169);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_cancel.TabIndex = 6;
            this.btn_cancel.Text = "取消";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.tb_device_ip);
            this.groupBox1.Controls.Add(this.cb_device_sync);
            this.groupBox1.Controls.Add(this.cb_device_state);
            this.groupBox1.Controls.Add(this.ntb_device_port);
            this.groupBox1.Controls.Add(this.tb_device_name);
            this.groupBox1.Controls.Add(this.nud_device_unitid);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(380, 148);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "设备信息";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(175, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 15;
            this.label2.Text = "从站号";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(175, 100);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 14;
            this.label4.Text = "端口号";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 100);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 17;
            this.label3.Text = "IP地址";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 16;
            this.label1.Text = "设备名";
            // 
            // tb_device_ip
            // 
            this.tb_device_ip.Location = new System.Drawing.Point(57, 95);
            this.tb_device_ip.Name = "tb_device_ip";
            this.tb_device_ip.Size = new System.Drawing.Size(100, 21);
            this.tb_device_ip.TabIndex = 13;
            // 
            // cb_device_sync
            // 
            this.cb_device_sync.AutoSize = true;
            this.cb_device_sync.Location = new System.Drawing.Point(280, 99);
            this.cb_device_sync.Name = "cb_device_sync";
            this.cb_device_sync.Size = new System.Drawing.Size(96, 16);
            this.cb_device_sync.TabIndex = 11;
            this.cb_device_sync.Text = "接收工况数据";
            this.cb_device_sync.UseVisualStyleBackColor = true;
            // 
            // cb_device_state
            // 
            this.cb_device_state.AutoSize = true;
            this.cb_device_state.Location = new System.Drawing.Point(280, 38);
            this.cb_device_state.Name = "cb_device_state";
            this.cb_device_state.Size = new System.Drawing.Size(72, 16);
            this.cb_device_state.TabIndex = 12;
            this.cb_device_state.Text = "启用设备";
            this.cb_device_state.UseVisualStyleBackColor = true;
            // 
            // ntb_device_port
            // 
            this.ntb_device_port.Location = new System.Drawing.Point(222, 97);
            this.ntb_device_port.Name = "ntb_device_port";
            this.ntb_device_port.Size = new System.Drawing.Size(40, 21);
            this.ntb_device_port.TabIndex = 10;
            // 
            // tb_device_name
            // 
            this.tb_device_name.Location = new System.Drawing.Point(57, 37);
            this.tb_device_name.Name = "tb_device_name";
            this.tb_device_name.Size = new System.Drawing.Size(100, 21);
            this.tb_device_name.TabIndex = 9;
            // 
            // nud_device_unitid
            // 
            this.nud_device_unitid.Location = new System.Drawing.Point(222, 37);
            this.nud_device_unitid.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nud_device_unitid.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nud_device_unitid.Name = "nud_device_unitid";
            this.nud_device_unitid.Size = new System.Drawing.Size(40, 21);
            this.nud_device_unitid.TabIndex = 8;
            this.nud_device_unitid.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // frmDeviceEditor
            // 
            this.AcceptButton = this.btn_ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_cancel;
            this.ClientSize = new System.Drawing.Size(400, 200);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.btn_ok);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximumSize = new System.Drawing.Size(420, 240);
            this.MinimumSize = new System.Drawing.Size(420, 240);
            this.Name = "frmDeviceEditor";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "添加设备";
            this.Load += new System.EventHandler(this.frmAddDevice_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nud_device_unitid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_ok;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tb_device_ip;
        private System.Windows.Forms.CheckBox cb_device_sync;
        private System.Windows.Forms.CheckBox cb_device_state;
        private NumbericTextbox ntb_device_port;
        private System.Windows.Forms.TextBox tb_device_name;
        private System.Windows.Forms.NumericUpDown nud_device_unitid;
    }
}