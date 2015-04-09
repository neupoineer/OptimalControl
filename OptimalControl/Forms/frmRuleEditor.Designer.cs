using Utility;

namespace OptimalControl.Forms
{
    partial class frmRuleEditor
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ntb_rule_period = new Utility.NumbericTextbox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tb_rule_expression = new System.Windows.Forms.TextBox();
            this.cb_rule_enabled = new System.Windows.Forms.CheckBox();
            this.ntb_rule_operation = new Utility.NumbericTextbox();
            this.tb_rule_name = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btn_add_value = new System.Windows.Forms.Button();
            this.cb_operator = new System.Windows.Forms.ComboBox();
            this.btn_add_operator = new System.Windows.Forms.Button();
            this.cb_parameter = new System.Windows.Forms.ComboBox();
            this.btn_add_parameter = new System.Windows.Forms.Button();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.btn_ok = new System.Windows.Forms.Button();
            this.btn_clear = new System.Windows.Forms.Button();
            this.btn_backspace = new System.Windows.Forms.Button();
            this.tb_value = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(630, 270);
            this.splitContainer1.SplitterDistance = 25;
            this.splitContainer1.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.splitContainer3);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.btn_cancel);
            this.splitContainer2.Panel2.Controls.Add(this.btn_ok);
            this.splitContainer2.Size = new System.Drawing.Size(630, 241);
            this.splitContainer2.SplitterDistance = 190;
            this.splitContainer2.TabIndex = 1;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer3.Size = new System.Drawing.Size(630, 190);
            this.splitContainer3.SplitterDistance = 377;
            this.splitContainer3.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ntb_rule_period);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.tb_rule_expression);
            this.groupBox1.Controls.Add(this.cb_rule_enabled);
            this.groupBox1.Controls.Add(this.ntb_rule_operation);
            this.groupBox1.Controls.Add(this.tb_rule_name);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(377, 190);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "规则信息";
            // 
            // ntb_rule_period
            // 
            this.ntb_rule_period.Location = new System.Drawing.Point(69, 157);
            this.ntb_rule_period.Name = "ntb_rule_period";
            this.ntb_rule_period.Size = new System.Drawing.Size(50, 21);
            this.ntb_rule_period.TabIndex = 18;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 160);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 15;
            this.label2.Text = "控制周期";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 120);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 14;
            this.label4.Text = "执行动作";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 17;
            this.label3.Text = "控制规则";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 16;
            this.label1.Text = "规则名称";
            // 
            // tb_rule_expression
            // 
            this.tb_rule_expression.Location = new System.Drawing.Point(69, 77);
            this.tb_rule_expression.Name = "tb_rule_expression";
            this.tb_rule_expression.Size = new System.Drawing.Size(300, 21);
            this.tb_rule_expression.TabIndex = 13;
            // 
            // cb_rule_enabled
            // 
            this.cb_rule_enabled.AutoSize = true;
            this.cb_rule_enabled.Location = new System.Drawing.Point(321, 39);
            this.cb_rule_enabled.Name = "cb_rule_enabled";
            this.cb_rule_enabled.Size = new System.Drawing.Size(48, 16);
            this.cb_rule_enabled.TabIndex = 12;
            this.cb_rule_enabled.Text = "启用";
            this.cb_rule_enabled.UseVisualStyleBackColor = true;
            // 
            // ntb_rule_operation
            // 
            this.ntb_rule_operation.Location = new System.Drawing.Point(69, 117);
            this.ntb_rule_operation.Name = "ntb_rule_operation";
            this.ntb_rule_operation.Size = new System.Drawing.Size(300, 21);
            this.ntb_rule_operation.TabIndex = 10;
            // 
            // tb_rule_name
            // 
            this.tb_rule_name.Location = new System.Drawing.Point(69, 37);
            this.tb_rule_name.Name = "tb_rule_name";
            this.tb_rule_name.Size = new System.Drawing.Size(240, 21);
            this.tb_rule_name.TabIndex = 9;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tb_value);
            this.groupBox2.Controls.Add(this.btn_backspace);
            this.groupBox2.Controls.Add(this.btn_clear);
            this.groupBox2.Controls.Add(this.btn_add_value);
            this.groupBox2.Controls.Add(this.cb_operator);
            this.groupBox2.Controls.Add(this.btn_add_operator);
            this.groupBox2.Controls.Add(this.cb_parameter);
            this.groupBox2.Controls.Add(this.btn_add_parameter);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(249, 190);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "编辑";
            // 
            // btn_add_value
            // 
            this.btn_add_value.Location = new System.Drawing.Point(162, 115);
            this.btn_add_value.Name = "btn_add_value";
            this.btn_add_value.Size = new System.Drawing.Size(75, 23);
            this.btn_add_value.TabIndex = 4;
            this.btn_add_value.Text = "添加数值";
            this.btn_add_value.UseVisualStyleBackColor = true;
            // 
            // cb_operator
            // 
            this.cb_operator.FormattingEnabled = true;
            this.cb_operator.Location = new System.Drawing.Point(6, 77);
            this.cb_operator.Name = "cb_operator";
            this.cb_operator.Size = new System.Drawing.Size(150, 20);
            this.cb_operator.TabIndex = 3;
            // 
            // btn_add_operator
            // 
            this.btn_add_operator.Location = new System.Drawing.Point(162, 75);
            this.btn_add_operator.Name = "btn_add_operator";
            this.btn_add_operator.Size = new System.Drawing.Size(75, 23);
            this.btn_add_operator.TabIndex = 2;
            this.btn_add_operator.Text = "添加运算";
            this.btn_add_operator.UseVisualStyleBackColor = true;
            // 
            // cb_parameter
            // 
            this.cb_parameter.FormattingEnabled = true;
            this.cb_parameter.Location = new System.Drawing.Point(6, 37);
            this.cb_parameter.Name = "cb_parameter";
            this.cb_parameter.Size = new System.Drawing.Size(150, 20);
            this.cb_parameter.TabIndex = 1;
            // 
            // btn_add_parameter
            // 
            this.btn_add_parameter.Location = new System.Drawing.Point(162, 35);
            this.btn_add_parameter.Name = "btn_add_parameter";
            this.btn_add_parameter.Size = new System.Drawing.Size(75, 23);
            this.btn_add_parameter.TabIndex = 0;
            this.btn_add_parameter.Text = "添加变量";
            this.btn_add_parameter.UseVisualStyleBackColor = true;
            // 
            // btn_cancel
            // 
            this.btn_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_cancel.Location = new System.Drawing.Point(400, 15);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_cancel.TabIndex = 10;
            this.btn_cancel.Text = "取消";
            this.btn_cancel.UseVisualStyleBackColor = true;
            // 
            // btn_ok
            // 
            this.btn_ok.Location = new System.Drawing.Point(150, 15);
            this.btn_ok.Name = "btn_ok";
            this.btn_ok.Size = new System.Drawing.Size(75, 23);
            this.btn_ok.TabIndex = 11;
            this.btn_ok.Text = "确定";
            this.btn_ok.UseVisualStyleBackColor = true;
            // 
            // btn_clear
            // 
            this.btn_clear.Location = new System.Drawing.Point(45, 155);
            this.btn_clear.Name = "btn_clear";
            this.btn_clear.Size = new System.Drawing.Size(75, 23);
            this.btn_clear.TabIndex = 6;
            this.btn_clear.Text = "全部清空";
            this.btn_clear.UseVisualStyleBackColor = true;
            // 
            // btn_backspace
            // 
            this.btn_backspace.Location = new System.Drawing.Point(162, 155);
            this.btn_backspace.Name = "btn_backspace";
            this.btn_backspace.Size = new System.Drawing.Size(75, 23);
            this.btn_backspace.TabIndex = 7;
            this.btn_backspace.Text = "退格";
            this.btn_backspace.UseVisualStyleBackColor = true;
            // 
            // tb_value
            // 
            this.tb_value.Location = new System.Drawing.Point(6, 117);
            this.tb_value.Name = "tb_value";
            this.tb_value.Size = new System.Drawing.Size(150, 21);
            this.tb_value.TabIndex = 8;
            // 
            // frmRuleEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(630, 270);
            this.ControlBox = false;
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "frmRuleEditor";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "添加规则";
            this.Load += new System.EventHandler(this.frmAddDevice_Load);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.GroupBox groupBox1;
        private NumbericTextbox ntb_rule_period;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tb_rule_expression;
        private System.Windows.Forms.CheckBox cb_rule_enabled;
        private NumbericTextbox ntb_rule_operation;
        private System.Windows.Forms.TextBox tb_rule_name;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btn_add_value;
        private System.Windows.Forms.ComboBox cb_operator;
        private System.Windows.Forms.Button btn_add_operator;
        private System.Windows.Forms.ComboBox cb_parameter;
        private System.Windows.Forms.Button btn_add_parameter;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.Button btn_ok;
        private System.Windows.Forms.Button btn_backspace;
        private System.Windows.Forms.Button btn_clear;
        private System.Windows.Forms.TextBox tb_value;

    }
}