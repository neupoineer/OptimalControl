using System;
using System.Collections.Generic;
using System.Windows.Forms;
using IBLL.Control;
using Utility;
using Rule = Model.Control.Rule;

namespace OptimalControl.Forms
{
    public partial class frmRulesManager : Form
    {

        /// <summary>
        /// 创建工厂类
        /// </summary>
        private BLLFactory.BLLFactory _bllFactory = new BLLFactory.BLLFactory();

        public frmRulesManager()
        {
            InitializeComponent();
            UpdateRulesGrid();
        }

        private delegate void UpdateRulesGridDelegate();

        private void UpdateRulesGrid()
        {
            if (InvokeRequired)
            {
                Invoke(new UpdateRulesGridDelegate(UpdateRulesGrid));
                return;
            }
            try
            {
                // 创建权限组管理类实例
                IRuleManager ruleManager = _bllFactory.BuildRuleManager();
                // 调用实例方法
                List<Rule> ruleCollection = ruleManager.GetAllRuleInfo();

                // 清除原有的列
                dgv_oc_rules.Columns.Clear();

                // 手动创建数据列
                DataGridViewTextBoxColumn dgvId = new DataGridViewTextBoxColumn
                {
                    Name = "Id",
                    HeaderText = "序号",
                    DataPropertyName = "ID",
                    MinimumWidth = 50,
                    FillWeight = 100,
                    Visible = false,
                };
                DataGridViewTextBoxColumn dgvPriority = new DataGridViewTextBoxColumn
                {
                    Name = "Priority",
                    HeaderText = "优先级",
                    DataPropertyName = "Priority",
                    MinimumWidth = 50,
                    FillWeight = 100,
                };
                DataGridViewTextBoxColumn dgvName = new DataGridViewTextBoxColumn
                {
                    Name = "Name",
                    HeaderText = "名称",
                    DataPropertyName = "Name",
                    MinimumWidth = 50,
                    FillWeight = 200,
                };
                DataGridViewTextBoxColumn dgvExpression = new DataGridViewTextBoxColumn
                {
                    Name = "Expression",
                    HeaderText = "控制规则",
                    DataPropertyName = "Expression",
                    MinimumWidth = 100,
                    FillWeight = 400,
                };
                dgvExpression.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                DataGridViewTextBoxColumn dgvOperation = new DataGridViewTextBoxColumn
                {
                    Name = "Operation",
                    HeaderText = "执行动作",
                    DataPropertyName = "Operation",
                    MinimumWidth = 100,
                    FillWeight = 400,
                };
                dgvOperation.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                DataGridViewTextBoxColumn dgvPeriod = new DataGridViewTextBoxColumn
                {
                    Name = "Period",
                    HeaderText = "控制周期",
                    DataPropertyName = "Period",
                    MinimumWidth = 50,
                    FillWeight = 100,
                };
                DataGridViewCheckBoxColumn dgvState = new DataGridViewCheckBoxColumn
                {
                    Name = "State",
                    HeaderText = "启用",
                    DataPropertyName = "State",
                    MinimumWidth = 50,
                    FillWeight = 100,
                };
                DataGridViewCheckBoxColumn dgvType = new DataGridViewCheckBoxColumn
                {
                    Name = "Type",
                    HeaderText = "类型",
                    DataPropertyName = "Type",
                    MinimumWidth = 50,
                    FillWeight = 100,
                };
                DataGridViewCheckBoxColumn dgvIsLogged = new DataGridViewCheckBoxColumn
                {
                    Name = "IsLogged",
                    HeaderText = "写入日志",
                    DataPropertyName = "IsLogged",
                    MinimumWidth = 50,
                    FillWeight = 100,
                };

                dgv_oc_rules.Columns.AddRange(new DataGridViewColumn[]
            {
                dgvPriority,
                dgvName,
                dgvExpression,
                dgvOperation,
                dgvPeriod,
                dgvState,
                dgvType,
                dgvIsLogged,
                dgvId,
            });

                for (int index = 0; index < ruleCollection.Count; index++)
                {
                    dgv_oc_rules.Rows.Add();
                    dgv_oc_rules.Rows[index].Cells["Id"].Value = ruleCollection[index].Id;
                    dgv_oc_rules.Rows[index].Cells["Priority"].Value = ruleCollection[index].Priority;
                    dgv_oc_rules.Rows[index].Cells["Name"].Value = ruleCollection[index].Name;
                    dgv_oc_rules.Rows[index].Cells["Expression"].Value = ruleCollection[index].Expression;
                    dgv_oc_rules.Rows[index].Cells["Operation"].Value = ruleCollection[index].Operation;
                    if (!ruleCollection[index].Period.Equals(-1))
                        dgv_oc_rules.Rows[index].Cells["Period"].Value = ruleCollection[index].Period;
                    dgv_oc_rules.Rows[index].Cells["State"].Value = ruleCollection[index].State;
                    dgv_oc_rules.Rows[index].Cells["Type"].Value = ruleCollection[index].Type;
                    dgv_oc_rules.Rows[index].Cells["IsLogged"].Value = ruleCollection[index].IsLogged;
                }

                status_Label.Text = string.Format("查询到 {0} 行数据", ruleCollection.Count);

            }
            catch (Exception ex)
            {
                RecordLog.WriteLogFile("UpdateRulesGrid", ex.Message);
            }

        }

        private Rule GetSelectedRule()
        {
            try
            {
                if (dgv_oc_rules.CurrentRow != null)
                {
                    int selectRowIndex = dgv_oc_rules.CurrentRow.Index;
                    Rule rule = new Rule
                    {
                        Id = Convert.ToInt32(dgv_oc_rules.Rows[selectRowIndex].Cells["Id"].Value),
                        Name = Convert.ToString(dgv_oc_rules.Rows[selectRowIndex].Cells["Name"].Value),
                        Expression = Convert.ToString(dgv_oc_rules.Rows[selectRowIndex].Cells["Expression"].Value),
                        Operation = Convert.ToString(dgv_oc_rules.Rows[selectRowIndex].Cells["Operation"].Value),
                        Period = Convert.ToString(dgv_oc_rules.Rows[selectRowIndex].Cells["Period"].Value) != ""
                            ? Convert.ToInt32(dgv_oc_rules.Rows[selectRowIndex].Cells["Period"].Value)
                            : -1,
                        State = Convert.ToBoolean(dgv_oc_rules.Rows[selectRowIndex].Cells["State"].Value),
                        Priority = Convert.ToInt32(dgv_oc_rules.Rows[selectRowIndex].Cells["Priority"].Value),
                        Type = Convert.ToBoolean(dgv_oc_rules.Rows[selectRowIndex].Cells["Type"].Value),
                        IsLogged = Convert.ToBoolean(dgv_oc_rules.Rows[selectRowIndex].Cells["IsLogged"].Value),
                    };
                    return rule;
                }
            }
            catch (Exception ex)
            {
                RecordLog.WriteLogFile("GetSelectedRule", ex.Message);
            }
            return new Rule();
        }

        private void tsbtn_rule_add_Click(object sender, EventArgs e)
        {
            Rule rule = GetSelectedRule();
            if (rule.Name == "") return;
            frmRuleEditor addRuleForm = new frmRuleEditor(DataOperateMode.Insert, rule);
            if (addRuleForm.ShowDialog() == DialogResult.OK)
            {
                if (addRuleForm.Result)
                {
                    status_Label.Text = "插入数据成功";
                    UpdateRulesGrid();
                }
            }
        }

        private void tsbtn_rule_edit_Click(object sender, EventArgs e)
        {
            Rule rule = GetSelectedRule();
            if (rule.Name == null) return;
            frmRuleEditor editParameterForm = new frmRuleEditor(DataOperateMode.Edit, rule);
            if (editParameterForm.ShowDialog() == DialogResult.OK)
            {
                if (editParameterForm.Result)
                {
                    status_Label.Text = "编辑数据成功";
                    UpdateRulesGrid();
                }
            }
        }

        private void tsbtn_rule_delete_Click(object sender, EventArgs e)
        {
            Rule rule = GetSelectedRule();
            if (rule.Name == null) return;
            frmRuleEditor deleteParameterForm = new frmRuleEditor(DataOperateMode.Delete, rule);
            if (deleteParameterForm.ShowDialog() == DialogResult.OK)
            {
                if (deleteParameterForm.Result)
                {
                    status_Label.Text = "删除数据成功";
                    UpdateRulesGrid();
                }
            }
        }

        private void tsbtn_rule_update_Click(object sender, EventArgs e)
        {
            UpdateRulesGrid();
        }

        private void tsbtn_rule_paras_Click(object sender, EventArgs e)
        {
            frmParametersManager parametersForm = new frmParametersManager();
            parametersForm.ShowDialog();
        }

        private void dgv_oc_rules_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            tsbtn_rule_edit_Click(sender, e);
        }
    }
}
