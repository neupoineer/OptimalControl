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
            this.cb_para_isoutput = new System.Windows.Forms.CheckBox();
            this.cb_para_isenabled = new System.Windows.Forms.CheckBox();
            this.cb_para_isfiltered = new System.Windows.Forms.CheckBox();
            this.cb_para_issaved = new System.Windows.Forms.CheckBox();
            this.tb_history_listlength = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.tb_trend_highter = new System.Windows.Forms.TextBox();
            this.cb_para_isdisplayed = new System.Windows.Forms.CheckBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.tb_trend_lower = new System.Windows.Forms.TextBox();
            this.tb_para_code = new System.Windows.Forms.TextBox();
            this.tb_trend_listlength = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.tb_para_ullimit = new System.Windows.Forms.TextBox();
            this.ntb_para_delay = new Utility.NumbericTextbox();
            this.tb_trend_interval = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.ntb_para_period = new Utility.NumbericTextbox();
            this.label15 = new System.Windows.Forms.Label();
            this.tb_para_lowerlimit = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.tb_para_upperlimit = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tb_para_uulimit = new System.Windows.Forms.TextBox();
            this.tb_trend_length = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.tb_para_ratio = new System.Windows.Forms.TextBox();
            this.cb_para_device = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ntb_para_address = new Utility.NumbericTextbox();
            this.tb_para_name = new System.Windows.Forms.TextBox();
            this.cb_para_isread = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_ok
            // 
            this.btn_ok.Location = new System.Drawing.Point(170, 350);
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
            this.btn_cancel.Location = new System.Drawing.Point(350, 350);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_cancel.TabIndex = 6;
            this.btn_cancel.Text = "取消";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cb_para_isread);
            this.groupBox1.Controls.Add(this.cb_para_isoutput);
            this.groupBox1.Controls.Add(this.cb_para_isenabled);
            this.groupBox1.Controls.Add(this.cb_para_isfiltered);
            this.groupBox1.Controls.Add(this.cb_para_issaved);
            this.groupBox1.Controls.Add(this.tb_history_listlength);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.tb_trend_highter);
            this.groupBox1.Controls.Add(this.cb_para_isdisplayed);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.label17);
            this.groupBox1.Controls.Add(this.tb_trend_lower);
            this.groupBox1.Controls.Add(this.tb_para_code);
            this.groupBox1.Controls.Add(this.tb_trend_listlength);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.tb_para_ullimit);
            this.groupBox1.Controls.Add(this.ntb_para_delay);
            this.groupBox1.Controls.Add(this.tb_trend_interval);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.ntb_para_period);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.tb_para_lowerlimit);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.tb_para_upperlimit);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.tb_para_uulimit);
            this.groupBox1.Controls.Add(this.tb_trend_length);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.label7);
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
            this.groupBox1.Size = new System.Drawing.Size(580, 315);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "变量信息";
            // 
            // cb_para_isoutput
            // 
            this.cb_para_isoutput.AutoSize = true;
            this.cb_para_isoutput.Location = new System.Drawing.Point(489, 199);
            this.cb_para_isoutput.Name = "cb_para_isoutput";
            this.cb_para_isoutput.Size = new System.Drawing.Size(72, 16);
            this.cb_para_isoutput.TabIndex = 41;
            this.cb_para_isoutput.Text = "是否输出";
            this.cb_para_isoutput.UseVisualStyleBackColor = true;
            // 
            // cb_para_isenabled
            // 
            this.cb_para_isenabled.AutoSize = true;
            this.cb_para_isenabled.Location = new System.Drawing.Point(384, 79);
            this.cb_para_isenabled.Name = "cb_para_isenabled";
            this.cb_para_isenabled.Size = new System.Drawing.Size(96, 16);
            this.cb_para_isenabled.TabIndex = 40;
            this.cb_para_isenabled.Text = "是否启用变量";
            this.cb_para_isenabled.UseVisualStyleBackColor = true;
            // 
            // cb_para_isfiltered
            // 
            this.cb_para_isfiltered.AutoSize = true;
            this.cb_para_isfiltered.Location = new System.Drawing.Point(489, 159);
            this.cb_para_isfiltered.Name = "cb_para_isfiltered";
            this.cb_para_isfiltered.Size = new System.Drawing.Size(72, 16);
            this.cb_para_isfiltered.TabIndex = 36;
            this.cb_para_isfiltered.Text = "是否滤波";
            this.cb_para_isfiltered.UseVisualStyleBackColor = true;
            // 
            // cb_para_issaved
            // 
            this.cb_para_issaved.AutoSize = true;
            this.cb_para_issaved.Location = new System.Drawing.Point(384, 119);
            this.cb_para_issaved.Name = "cb_para_issaved";
            this.cb_para_issaved.Size = new System.Drawing.Size(96, 16);
            this.cb_para_issaved.TabIndex = 35;
            this.cb_para_issaved.Text = "是否保存变量";
            this.cb_para_issaved.UseVisualStyleBackColor = true;
            // 
            // tb_history_listlength
            // 
            this.tb_history_listlength.Location = new System.Drawing.Point(93, 277);
            this.tb_history_listlength.Name = "tb_history_listlength";
            this.tb_history_listlength.Size = new System.Drawing.Size(70, 21);
            this.tb_history_listlength.TabIndex = 39;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(198, 240);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(77, 12);
            this.label13.TabIndex = 30;
            this.label13.Text = "趋势判断上限";
            // 
            // tb_trend_highter
            // 
            this.tb_trend_highter.Location = new System.Drawing.Point(281, 237);
            this.tb_trend_highter.Name = "tb_trend_highter";
            this.tb_trend_highter.Size = new System.Drawing.Size(70, 21);
            this.tb_trend_highter.TabIndex = 31;
            // 
            // cb_para_isdisplayed
            // 
            this.cb_para_isdisplayed.AutoSize = true;
            this.cb_para_isdisplayed.Location = new System.Drawing.Point(384, 159);
            this.cb_para_isdisplayed.Name = "cb_para_isdisplayed";
            this.cb_para_isdisplayed.Size = new System.Drawing.Size(72, 16);
            this.cb_para_isdisplayed.TabIndex = 34;
            this.cb_para_isdisplayed.Text = "是否显示";
            this.cb_para_isdisplayed.UseVisualStyleBackColor = true;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(198, 280);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(77, 12);
            this.label14.TabIndex = 32;
            this.label14.Text = "趋势判断下限";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(10, 280);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(77, 12);
            this.label17.TabIndex = 38;
            this.label17.Text = "历史数据长度";
            // 
            // tb_trend_lower
            // 
            this.tb_trend_lower.Location = new System.Drawing.Point(281, 277);
            this.tb_trend_lower.Name = "tb_trend_lower";
            this.tb_trend_lower.Size = new System.Drawing.Size(70, 21);
            this.tb_trend_lower.TabIndex = 33;
            // 
            // tb_para_code
            // 
            this.tb_para_code.Location = new System.Drawing.Point(93, 37);
            this.tb_para_code.Name = "tb_para_code";
            this.tb_para_code.Size = new System.Drawing.Size(110, 21);
            this.tb_para_code.TabIndex = 33;
            // 
            // tb_trend_listlength
            // 
            this.tb_trend_listlength.Location = new System.Drawing.Point(281, 197);
            this.tb_trend_listlength.Name = "tb_trend_listlength";
            this.tb_trend_listlength.Size = new System.Drawing.Size(70, 21);
            this.tb_trend_listlength.TabIndex = 37;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(10, 40);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(53, 12);
            this.label11.TabIndex = 32;
            this.label11.Text = "变量编码";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(198, 200);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(77, 12);
            this.label16.TabIndex = 36;
            this.label16.Text = "趋势判断点数";
            // 
            // tb_para_ullimit
            // 
            this.tb_para_ullimit.Location = new System.Drawing.Point(93, 237);
            this.tb_para_ullimit.Name = "tb_para_ullimit";
            this.tb_para_ullimit.Size = new System.Drawing.Size(70, 21);
            this.tb_para_ullimit.TabIndex = 31;
            // 
            // ntb_para_delay
            // 
            this.ntb_para_delay.Location = new System.Drawing.Point(441, 277);
            this.ntb_para_delay.Name = "ntb_para_delay";
            this.ntb_para_delay.Size = new System.Drawing.Size(70, 21);
            this.ntb_para_delay.TabIndex = 23;
            // 
            // tb_trend_interval
            // 
            this.tb_trend_interval.Location = new System.Drawing.Point(281, 157);
            this.tb_trend_interval.Name = "tb_trend_interval";
            this.tb_trend_interval.Size = new System.Drawing.Size(70, 21);
            this.tb_trend_interval.TabIndex = 35;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(382, 280);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(149, 12);
            this.label6.TabIndex = 22;
            this.label6.Text = "动作延时              秒";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(10, 240);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(65, 12);
            this.label10.TabIndex = 30;
            this.label10.Text = "变量下下限";
            // 
            // ntb_para_period
            // 
            this.ntb_para_period.Location = new System.Drawing.Point(441, 237);
            this.ntb_para_period.Name = "ntb_para_period";
            this.ntb_para_period.Size = new System.Drawing.Size(70, 21);
            this.ntb_para_period.TabIndex = 21;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(198, 160);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(77, 12);
            this.label15.TabIndex = 34;
            this.label15.Text = "趋势间隔点数";
            // 
            // tb_para_lowerlimit
            // 
            this.tb_para_lowerlimit.Location = new System.Drawing.Point(93, 197);
            this.tb_para_lowerlimit.Name = "tb_para_lowerlimit";
            this.tb_para_lowerlimit.Size = new System.Drawing.Size(70, 21);
            this.tb_para_lowerlimit.TabIndex = 29;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(382, 240);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(149, 12);
            this.label5.TabIndex = 20;
            this.label5.Text = "控制周期              秒";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(10, 200);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 12);
            this.label9.TabIndex = 28;
            this.label9.Text = "变量下限";
            // 
            // tb_para_upperlimit
            // 
            this.tb_para_upperlimit.Location = new System.Drawing.Point(93, 157);
            this.tb_para_upperlimit.Name = "tb_para_upperlimit";
            this.tb_para_upperlimit.Size = new System.Drawing.Size(70, 21);
            this.tb_para_upperlimit.TabIndex = 27;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(10, 160);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 26;
            this.label8.Text = "变量上限";
            // 
            // tb_para_uulimit
            // 
            this.tb_para_uulimit.Location = new System.Drawing.Point(93, 117);
            this.tb_para_uulimit.Name = "tb_para_uulimit";
            this.tb_para_uulimit.Size = new System.Drawing.Size(70, 21);
            this.tb_para_uulimit.TabIndex = 25;
            // 
            // tb_trend_length
            // 
            this.tb_trend_length.Location = new System.Drawing.Point(281, 117);
            this.tb_trend_length.Name = "tb_trend_length";
            this.tb_trend_length.Size = new System.Drawing.Size(70, 21);
            this.tb_trend_length.TabIndex = 29;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(198, 120);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(77, 12);
            this.label12.TabIndex = 28;
            this.label12.Text = "趋势计算点数";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 120);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 24;
            this.label7.Text = "变量上上限";
            // 
            // tb_para_ratio
            // 
            this.tb_para_ratio.Location = new System.Drawing.Point(281, 77);
            this.tb_para_ratio.Name = "tb_para_ratio";
            this.tb_para_ratio.Size = new System.Drawing.Size(70, 21);
            this.tb_para_ratio.TabIndex = 19;
            // 
            // cb_para_device
            // 
            this.cb_para_device.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_para_device.FormattingEnabled = true;
            this.cb_para_device.Location = new System.Drawing.Point(441, 37);
            this.cb_para_device.Name = "cb_para_device";
            this.cb_para_device.Size = new System.Drawing.Size(120, 20);
            this.cb_para_device.TabIndex = 18;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(222, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 15;
            this.label2.Text = "变量地址";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(222, 80);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 14;
            this.label4.Text = "放大倍数";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(382, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 17;
            this.label3.Text = "所属设备";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 80);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 16;
            this.label1.Text = "变量名称";
            // 
            // ntb_para_address
            // 
            this.ntb_para_address.Location = new System.Drawing.Point(281, 37);
            this.ntb_para_address.Name = "ntb_para_address";
            this.ntb_para_address.Size = new System.Drawing.Size(70, 21);
            this.ntb_para_address.TabIndex = 10;
            // 
            // tb_para_name
            // 
            this.tb_para_name.Location = new System.Drawing.Point(93, 77);
            this.tb_para_name.Name = "tb_para_name";
            this.tb_para_name.Size = new System.Drawing.Size(110, 21);
            this.tb_para_name.TabIndex = 9;
            // 
            // cb_para_isread
            // 
            this.cb_para_isread.AutoSize = true;
            this.cb_para_isread.Location = new System.Drawing.Point(384, 199);
            this.cb_para_isread.Name = "cb_para_isread";
            this.cb_para_isread.Size = new System.Drawing.Size(72, 16);
            this.cb_para_isread.TabIndex = 42;
            this.cb_para_isread.Text = "是否读取";
            this.cb_para_isread.UseVisualStyleBackColor = true;
            // 
            // frmParameterEditor
            // 
            this.AcceptButton = this.btn_ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_cancel;
            this.ClientSize = new System.Drawing.Size(596, 386);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.btn_ok);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "frmParameterEditor";
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
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox cb_para_isdisplayed;
        private System.Windows.Forms.TextBox tb_para_code;
        private System.Windows.Forms.Label label11;
        private NumbericTextbox ntb_para_delay;
        private NumbericTextbox ntb_para_period;
        private System.Windows.Forms.CheckBox cb_para_issaved;
        private System.Windows.Forms.TextBox tb_trend_length;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox tb_trend_highter;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox tb_trend_lower;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox tb_trend_interval;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox tb_trend_listlength;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.CheckBox cb_para_isfiltered;
        private System.Windows.Forms.CheckBox cb_para_isoutput;
        private System.Windows.Forms.CheckBox cb_para_isenabled;
        private System.Windows.Forms.TextBox tb_history_listlength;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.CheckBox cb_para_isread;
    }
}