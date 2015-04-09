using Utility;

namespace OptimalControl.Forms
{
    partial class frmParameterEditor
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
            this.tb_para_ullimit = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.tb_para_lowerlimit = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tb_para_upperlimit = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tb_para_uulimit = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.ntb_para_delay = new NumbericTextbox();
            this.label6 = new System.Windows.Forms.Label();
            this.ntb_para_period = new NumbericTextbox();
            this.label5 = new System.Windows.Forms.Label();
            this.tb_para_ratio = new System.Windows.Forms.TextBox();
            this.cb_para_device = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ntb_para_address = new NumbericTextbox();
            this.tb_para_name = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_ok
            // 
            this.btn_ok.Location = new System.Drawing.Point(80, 225);
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
            this.btn_cancel.Location = new System.Drawing.Point(260, 225);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_cancel.TabIndex = 6;
            this.btn_cancel.Text = "取消";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tb_para_ullimit);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.tb_para_lowerlimit);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.tb_para_upperlimit);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.tb_para_uulimit);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.ntb_para_delay);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.ntb_para_period);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.tb_para_ratio);
            this.groupBox1.Controls.Add(this.cb_para_device);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.ntb_para_address);
            this.groupBox1.Controls.Add(this.tb_para_name);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(396, 200);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "变量信息";
            // 
            // tb_para_ullimit
            // 
            this.tb_para_ullimit.Location = new System.Drawing.Point(329, 157);
            this.tb_para_ullimit.Name = "tb_para_ullimit";
            this.tb_para_ullimit.Size = new System.Drawing.Size(50, 21);
            this.tb_para_ullimit.TabIndex = 31;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(282, 160);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(41, 12);
            this.label10.TabIndex = 30;
            this.label10.Text = "下下限";
            // 
            // tb_para_lowerlimit
            // 
            this.tb_para_lowerlimit.Location = new System.Drawing.Point(209, 157);
            this.tb_para_lowerlimit.Name = "tb_para_lowerlimit";
            this.tb_para_lowerlimit.Size = new System.Drawing.Size(50, 21);
            this.tb_para_lowerlimit.TabIndex = 29;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(150, 160);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 12);
            this.label9.TabIndex = 28;
            this.label9.Text = "变量下限";
            // 
            // tb_para_upperlimit
            // 
            this.tb_para_upperlimit.Location = new System.Drawing.Point(209, 117);
            this.tb_para_upperlimit.Name = "tb_para_upperlimit";
            this.tb_para_upperlimit.Size = new System.Drawing.Size(50, 21);
            this.tb_para_upperlimit.TabIndex = 27;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(150, 120);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 26;
            this.label8.Text = "变量上限";
            // 
            // tb_para_uulimit
            // 
            this.tb_para_uulimit.Location = new System.Drawing.Point(329, 117);
            this.tb_para_uulimit.Name = "tb_para_uulimit";
            this.tb_para_uulimit.Size = new System.Drawing.Size(50, 21);
            this.tb_para_uulimit.TabIndex = 25;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(282, 120);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 24;
            this.label7.Text = "上上限";
            // 
            // ntb_para_delay
            // 
            this.ntb_para_delay.Location = new System.Drawing.Point(69, 157);
            this.ntb_para_delay.Name = "ntb_para_delay";
            this.ntb_para_delay.Size = new System.Drawing.Size(50, 21);
            this.ntb_para_delay.TabIndex = 23;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 160);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(119, 12);
            this.label6.TabIndex = 22;
            this.label6.Text = "动作延时          s";
            // 
            // ntb_para_period
            // 
            this.ntb_para_period.Location = new System.Drawing.Point(69, 117);
            this.ntb_para_period.Name = "ntb_para_period";
            this.ntb_para_period.Size = new System.Drawing.Size(50, 21);
            this.ntb_para_period.TabIndex = 21;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 120);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(119, 12);
            this.label5.TabIndex = 20;
            this.label5.Text = "控制周期          s";
            // 
            // tb_para_ratio
            // 
            this.tb_para_ratio.Location = new System.Drawing.Point(299, 77);
            this.tb_para_ratio.Name = "tb_para_ratio";
            this.tb_para_ratio.Size = new System.Drawing.Size(80, 21);
            this.tb_para_ratio.TabIndex = 19;
            // 
            // cb_para_device
            // 
            this.cb_para_device.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_para_device.FormattingEnabled = true;
            this.cb_para_device.Location = new System.Drawing.Point(69, 77);
            this.cb_para_device.Name = "cb_para_device";
            this.cb_para_device.Size = new System.Drawing.Size(150, 20);
            this.cb_para_device.TabIndex = 18;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(240, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 15;
            this.label2.Text = "变量地址";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(240, 80);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 14;
            this.label4.Text = "放大倍数";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 17;
            this.label3.Text = "所属设备";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 16;
            this.label1.Text = "变量名称";
            // 
            // ntb_para_address
            // 
            this.ntb_para_address.Location = new System.Drawing.Point(299, 37);
            this.ntb_para_address.Name = "ntb_para_address";
            this.ntb_para_address.Size = new System.Drawing.Size(80, 21);
            this.ntb_para_address.TabIndex = 10;
            // 
            // tb_para_name
            // 
            this.tb_para_name.Location = new System.Drawing.Point(69, 37);
            this.tb_para_name.Name = "tb_para_name";
            this.tb_para_name.Size = new System.Drawing.Size(150, 21);
            this.tb_para_name.TabIndex = 9;
            // 
            // frmEditParameter
            // 
            this.AcceptButton = this.btn_ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_cancel;
            this.ClientSize = new System.Drawing.Size(420, 260);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.btn_ok);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "frmEditParameter";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "添加变量";
            this.Load += new System.EventHandler(this.frmEditParameter_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
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
        private NumbericTextbox ntb_para_address;
        private System.Windows.Forms.TextBox tb_para_name;
        private System.Windows.Forms.TextBox tb_para_ratio;
        private System.Windows.Forms.ComboBox cb_para_device;
        private System.Windows.Forms.TextBox tb_para_ullimit;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tb_para_lowerlimit;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tb_para_upperlimit;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tb_para_uulimit;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox ntb_para_delay;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox ntb_para_period;
        private System.Windows.Forms.Label label5;
    }
}