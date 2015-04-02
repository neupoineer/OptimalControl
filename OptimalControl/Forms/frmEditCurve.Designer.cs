namespace OptimalControl.Forms
{
    partial class frmEditCurve
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
            this.cb_curve_name = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.tb_curve_xtitle = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.tb_curve_ytitle = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.cb_curve_symbol = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cb_curve_type = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cb_curve_color = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cb_curve_device = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tb_curve_ymin = new System.Windows.Forms.TextBox();
            this.tb_curve_ymax = new System.Windows.Forms.TextBox();
            this.tb_curve_symbolsize = new System.Windows.Forms.TextBox();
            this.tb_curve_size = new System.Windows.Forms.TextBox();
            this.ntb_curve_address = new OptimalControl.Common.NumbericTextbox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_ok
            // 
            this.btn_ok.Location = new System.Drawing.Point(200, 355);
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
            this.btn_cancel.Location = new System.Drawing.Point(360, 355);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_cancel.TabIndex = 6;
            this.btn_cancel.Text = "取消";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cb_curve_name);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.tb_curve_xtitle);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.tb_curve_ytitle);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.cb_curve_symbol);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.cb_curve_type);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.cb_curve_color);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.cb_curve_device);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.ntb_curve_address);
            this.groupBox1.Controls.Add(this.tb_curve_ymin);
            this.groupBox1.Controls.Add(this.tb_curve_ymax);
            this.groupBox1.Controls.Add(this.tb_curve_symbolsize);
            this.groupBox1.Controls.Add(this.tb_curve_size);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(625, 325);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "曲线信息";
            // 
            // cb_curve_name
            // 
            this.cb_curve_name.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_curve_name.FormattingEnabled = true;
            this.cb_curve_name.Location = new System.Drawing.Point(68, 37);
            this.cb_curve_name.Name = "cb_curve_name";
            this.cb_curve_name.Size = new System.Drawing.Size(121, 20);
            this.cb_curve_name.TabIndex = 40;
            this.cb_curve_name.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cb_curve_name_DrawItem);
            this.cb_curve_name.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.cb_curve_name_MeasureItem);
            this.cb_curve_name.SelectedIndexChanged += new System.EventHandler(this.cb_curve_name_SelectedIndexChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(320, 280);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(65, 12);
            this.label12.TabIndex = 34;
            this.label12.Text = "纵轴最小值";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(10, 280);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(65, 12);
            this.label11.TabIndex = 32;
            this.label11.Text = "纵轴最大值";
            // 
            // tb_curve_xtitle
            // 
            this.tb_curve_xtitle.Location = new System.Drawing.Point(69, 217);
            this.tb_curve_xtitle.Name = "tb_curve_xtitle";
            this.tb_curve_xtitle.Size = new System.Drawing.Size(230, 21);
            this.tb_curve_xtitle.TabIndex = 31;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(10, 220);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 12);
            this.label10.TabIndex = 30;
            this.label10.Text = "横轴名称";
            // 
            // tb_curve_ytitle
            // 
            this.tb_curve_ytitle.Location = new System.Drawing.Point(379, 217);
            this.tb_curve_ytitle.Name = "tb_curve_ytitle";
            this.tb_curve_ytitle.Size = new System.Drawing.Size(230, 21);
            this.tb_curve_ytitle.TabIndex = 29;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(320, 220);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 12);
            this.label9.TabIndex = 28;
            this.label9.Text = "纵轴名称";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(240, 160);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 26;
            this.label8.Text = "符号大小";
            // 
            // cb_curve_symbol
            // 
            this.cb_curve_symbol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_curve_symbol.FormattingEnabled = true;
            this.cb_curve_symbol.Location = new System.Drawing.Point(69, 157);
            this.cb_curve_symbol.Name = "cb_curve_symbol";
            this.cb_curve_symbol.Size = new System.Drawing.Size(120, 20);
            this.cb_curve_symbol.TabIndex = 25;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 160);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 24;
            this.label7.Text = "符号类型";
            // 
            // cb_curve_type
            // 
            this.cb_curve_type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_curve_type.FormattingEnabled = true;
            this.cb_curve_type.Location = new System.Drawing.Point(299, 97);
            this.cb_curve_type.Name = "cb_curve_type";
            this.cb_curve_type.Size = new System.Drawing.Size(120, 20);
            this.cb_curve_type.TabIndex = 23;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(240, 100);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 22;
            this.label6.Text = "曲线类型";
            // 
            // cb_curve_color
            // 
            this.cb_curve_color.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_curve_color.FormattingEnabled = true;
            this.cb_curve_color.Location = new System.Drawing.Point(69, 97);
            this.cb_curve_color.Name = "cb_curve_color";
            this.cb_curve_color.Size = new System.Drawing.Size(120, 20);
            this.cb_curve_color.TabIndex = 21;
            this.cb_curve_color.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cb_curve_color_DrawItem);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 100);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 20;
            this.label5.Text = "曲线颜色";
            // 
            // cb_curve_device
            // 
            this.cb_curve_device.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_curve_device.Enabled = false;
            this.cb_curve_device.FormattingEnabled = true;
            this.cb_curve_device.Location = new System.Drawing.Point(299, 37);
            this.cb_curve_device.Name = "cb_curve_device";
            this.cb_curve_device.Size = new System.Drawing.Size(120, 20);
            this.cb_curve_device.TabIndex = 18;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(470, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 15;
            this.label2.Text = "存储地址";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(470, 100);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 14;
            this.label4.Text = "曲线宽度";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(240, 40);
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
            this.label1.Text = "曲线名称";
            // 
            // tb_curve_ymin
            // 
            this.tb_curve_ymin.Location = new System.Drawing.Point(391, 277);
            this.tb_curve_ymin.Name = "tb_curve_ymin";
            this.tb_curve_ymin.Size = new System.Drawing.Size(80, 21);
            this.tb_curve_ymin.TabIndex = 9;
            // 
            // tb_curve_ymax
            // 
            this.tb_curve_ymax.Location = new System.Drawing.Point(81, 277);
            this.tb_curve_ymax.Name = "tb_curve_ymax";
            this.tb_curve_ymax.Size = new System.Drawing.Size(80, 21);
            this.tb_curve_ymax.TabIndex = 9;
            // 
            // tb_curve_symbolsize
            // 
            this.tb_curve_symbolsize.Location = new System.Drawing.Point(299, 157);
            this.tb_curve_symbolsize.Name = "tb_curve_symbolsize";
            this.tb_curve_symbolsize.Size = new System.Drawing.Size(80, 21);
            this.tb_curve_symbolsize.TabIndex = 9;
            // 
            // tb_curve_size
            // 
            this.tb_curve_size.Location = new System.Drawing.Point(529, 97);
            this.tb_curve_size.Name = "tb_curve_size";
            this.tb_curve_size.Size = new System.Drawing.Size(80, 21);
            this.tb_curve_size.TabIndex = 9;
            // 
            // ntb_curve_address
            // 
            this.ntb_curve_address.Location = new System.Drawing.Point(529, 37);
            this.ntb_curve_address.Name = "ntb_curve_address";
            this.ntb_curve_address.ReadOnly = true;
            this.ntb_curve_address.Size = new System.Drawing.Size(80, 21);
            this.ntb_curve_address.TabIndex = 10;
            // 
            // frmEditCurve
            // 
            this.AcceptButton = this.btn_ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_cancel;
            this.ClientSize = new System.Drawing.Size(650, 395);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.btn_ok);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximumSize = new System.Drawing.Size(660, 425);
            this.MinimumSize = new System.Drawing.Size(660, 425);
            this.Name = "frmEditCurve";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "添加曲线";
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
        private Common.NumbericTextbox ntb_curve_address;
        private System.Windows.Forms.ComboBox cb_curve_device;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox tb_curve_xtitle;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tb_curve_ytitle;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cb_curve_symbol;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cb_curve_type;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cb_curve_color;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tb_curve_symbolsize;
        private System.Windows.Forms.TextBox tb_curve_size;
        private System.Windows.Forms.ComboBox cb_curve_name;
        private System.Windows.Forms.TextBox tb_curve_ymin;
        private System.Windows.Forms.TextBox tb_curve_ymax;
    }
}