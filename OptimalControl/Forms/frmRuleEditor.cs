using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using IBLL.Control;
using Model.Control;
using Utility;
using Rule = Model.Control.Rule;

namespace OptimalControl.Forms
{
    public partial class frmRuleEditor : Form
    {
        private readonly DataOperateMode _mode;
        private Rule _rule;
        private List<Variable> _parameters;
        private BLLFactory.BLLFactory _bllFactory = new BLLFactory.BLLFactory();
        public bool Result { get; private set; }

        public frmRuleEditor(DataOperateMode mode, Rule rule)
        {
            IVariableManager variableManager = _bllFactory.BuildIVariableManager();
            _mode = mode;
            _rule = rule;
            _parameters = variableManager.GetAllVariableInfo();
            InitializeComponent();
        }

        private void LoadUI(Rule rule, List<Variable> parameters, string formText, DataOperateMode mode)
        {
            cb_parameter.Items.Clear();
            foreach (Variable variable in parameters)
            {
                cb_parameter.Items.Add(string.Format("{0}", variable.Name));
            }

            cb_operator.Items.Clear();
            cb_operator.Items.AddRange(new object[]
            {
                "(", ")", "*", "/", "%", "+", "-", "<", "<=", ">", ">=", "=", "<>", "!", "&", "|", "tan", "atan"
            });

            Text = formText;
            if (mode != DataOperateMode.Insert)
            {
                tb_rule_name.Text = rule.Name;
                tb_rule_name.Enabled = (mode != DataOperateMode.Delete);
                cb_rule_enabled.Checked = rule.State;
                cb_rule_enabled.Enabled = (mode != DataOperateMode.Delete);
                tb_rule_expression.Text = rule.Expression;
                tb_rule_expression.Enabled = (mode != DataOperateMode.Delete);
                tb_rule_operation.Text = rule.Operation;
                tb_rule_operation.Enabled = (mode != DataOperateMode.Delete);
                ntb_rule_period.Text = rule.Period.ToString(CultureInfo.InvariantCulture);
                ntb_rule_period.Enabled = (mode != DataOperateMode.Delete);
                ntb_rule_priority.Text = rule.Priority.ToString(CultureInfo.InvariantCulture);
                ntb_rule_priority.Enabled = (mode != DataOperateMode.Delete);
                splitContainer3.Panel2Collapsed = (mode == DataOperateMode.Delete);
                this.ClientSize =
                    new Size((mode == DataOperateMode.Delete) ? (this.ClientSize.Width - 250) : this.ClientSize.Width,
                        this.ClientSize.Height);
                //90,210
                btn_ok.Location = new Point((mode == DataOperateMode.Delete) ? 90 : btn_ok.Location.X, btn_ok.Location.Y);
                btn_cancel.Location = new Point((mode == DataOperateMode.Delete) ? 210 : btn_cancel.Location.X,
                    btn_cancel.Location.Y);
            }
        }

        private Rule GetCurrentRule()
        {
            Rule rule = new Rule()
            {
                Id = _rule.Id,
                Name = tb_rule_name.Text.Trim(),
                Expression = tb_rule_expression.Text.Trim(),
                Operation = tb_rule_operation.Text.Trim(),
                Period = Convert.ToInt32(ntb_rule_period.Text.Trim()),
                State = cb_rule_enabled.Checked,
                Priority = Convert.ToInt32(ntb_rule_priority.Text.Trim()),
            };
            return rule;
        }

        private void frmAddDevice_Load(object sender, System.EventArgs e)
        {
            switch (_mode)
            {
                case DataOperateMode.Insert:
                    LoadUI(_rule, _parameters, "添加规则", DataOperateMode.Insert);
                    break;
                case DataOperateMode.Edit:
                    LoadUI(_rule, _parameters, "编辑规则", DataOperateMode.Edit);
                    break;
                case DataOperateMode.Delete:
                    LoadUI(_rule, _parameters, "删除规则", DataOperateMode.Delete);
                    break;
                default:
                    break;
            }
        }

        private void btn_ok_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (tb_rule_name.Text.Length < 1)
                {
                    MessageBox.Show("请输入规则名称！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (tb_rule_expression.Text.Length < 1)
                {
                    MessageBox.Show("请输入控制规则！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (tb_rule_operation.Text.Length < 1)
                {
                    MessageBox.Show("请输入执行动作！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (ntb_rule_priority.Text.Length < 1)
                {
                    MessageBox.Show("请输入优先级！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                IRuleManager ruleManager = _bllFactory.BuildRuleManager();
                switch (_mode)
                {
                    case DataOperateMode.Insert:
                        Result = ruleManager.AddRule(GetCurrentRule());
                        break;
                    case DataOperateMode.Edit:
                        Result = ruleManager.ModifyRule(GetCurrentRule());
                        break;
                    case DataOperateMode.Delete:
                        if (
                            MessageBox.Show(
                                string.Format("确认删除规则'{0}'？", _rule.Name),
                                "数据删除警告",
                                MessageBoxButtons.OKCancel,
                                MessageBoxIcon.Warning)
                            == DialogResult.OK)
                        {
                            Result = ruleManager.DeleteRuleById(_rule.Id);
                        }
                        break;
                    default:
                        break;
                }
                this.DialogResult = DialogResult.OK;
                this.Dispose();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void btn_cancel_Click(object sender, System.EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Dispose();
        }

        private void btn_add_parameter_Click(object sender, EventArgs e)
        {
            if (!tb_rule_expression.ReadOnly)
            {
                if (cb_parameter.Text.Length > 0)
                {
                    tb_rule_expression.Text += string.Format("[@{0}]",cb_parameter.Text);
                }
            }
            else if (!tb_rule_operation.ReadOnly)
            {
                if (cb_parameter.Text.Length > 0)
                {
                    tb_rule_operation.Text += string.Format("[@{0}]",cb_parameter.Text);
                }
            }
            else
            {
                MessageBox.Show(
                    "请先双击控制规则或执行动作输入框！",
                    "警告",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Warning);
            }
        }

        private void btn_add_operator_Click(object sender, EventArgs e)
        {
            if (!tb_rule_expression.ReadOnly)
            {
                if (cb_operator.Text.Length > 0)
                {
                    tb_rule_expression.Text += cb_operator.Text;
                }
            }
            else if (!tb_rule_operation.ReadOnly)
            {
                if (cb_operator.Text.Length > 0)
                {
                    tb_rule_operation.Text += cb_operator.Text;
                }
            }
            else
            {
                MessageBox.Show(
                    "请先双击控制规则或执行动作输入框！",
                    "警告",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Warning);
            }
        }

        private void btn_add_value_Click(object sender, EventArgs e)
        {
            if (!tb_rule_expression.ReadOnly)
            {
                if (tb_value.Text.Length > 0)
                {
                    tb_rule_expression.Text += tb_value.Text;
                }
            }
            else if (!tb_rule_operation.ReadOnly)
            {
                if (tb_value.Text.Length > 0)
                {
                    tb_rule_operation.Text += tb_value.Text;
                }
            }
            else
            {
                MessageBox.Show(
                    "请先双击控制规则或执行动作输入框！",
                    "警告",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Warning);
            }
        }

        private void tb_rule_operation_DoubleClick(object sender, EventArgs e)
        {
            tb_rule_operation.ReadOnly = !tb_rule_operation.ReadOnly;
            if (!tb_rule_operation.ReadOnly)
            {
                if (!tb_rule_expression.ReadOnly)
                    tb_rule_expression.ReadOnly = !tb_rule_expression.ReadOnly;
            }
        }

        private void tb_rule_expression_DoubleClick(object sender, EventArgs e)
        {
            tb_rule_expression.ReadOnly = !tb_rule_expression.ReadOnly;
            if (!tb_rule_expression.ReadOnly)
            {
                if (!tb_rule_operation.ReadOnly)
                    tb_rule_operation.ReadOnly = !tb_rule_operation.ReadOnly;
            }
        }

        private void tb_rule_expression_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled =
                !(Char.IsNumber(e.KeyChar)
                  || (new Char[] { '(', ')', '*', '/', '%', '+', '-', '<', '>', '=', '!', '&', '|', '.' }).Contains(e.KeyChar)
                  || e.KeyChar == (char) Keys.Back
                  || e.KeyChar == (char) Keys.Delete);
        }

        private void tb_value_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled =
                !(Char.IsNumber(e.KeyChar)
                  || (new Char[] {'.'}).Contains(e.KeyChar)
                  || e.KeyChar == (char) Keys.Back
                  || e.KeyChar == (char) Keys.Delete);
        }

        private void tb_rule_operation_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled =
                !(Char.IsNumber(e.KeyChar)
                  || (new Char[] { '(', ')', '*', '/', '%', '+', '-', '<', '>', '=', '!', '&', '|', '.' }).Contains(e.KeyChar)
                  || e.KeyChar == (char) Keys.Back
                  || e.KeyChar == (char) Keys.Delete);
        }
    }
}
