using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;
using IBLL.Control;
using Model.Control;
using Model.Modbus;

namespace OptimalControl.Forms
{
    public partial class frmParametersManager : Form
    {
        BLLFactory.BLLFactory _bllFactory = new BLLFactory.BLLFactory();

        public frmParametersManager()
        {
            InitializeComponent();
            UpdateUI();
        }

        private void UpdateUI()
        {
            IDeviceManager deviceManager = _bllFactory.BuildDeviceManager();
            List<Device> devices = deviceManager.GetAllDeviceInfo();

            treeView.Nodes.Clear();
            TreeNode rootNode = treeView.Nodes.Add("全部");
            TreeNode secondNode = rootNode.Nodes.Add("设备");
            for (int index = 0; index < devices.Count; index++)
            {
                if (devices[index].State)
                {
                    secondNode.Nodes.Add(Convert.ToString(devices[index].Id), devices[index].Name);
                }
            }
            rootNode.Nodes.Add("服务器");
            treeView.ExpandAll();
            UpdatePatameterGrid(-1);
        }

        private void UpdatePatameterGrid(int deviceId)
        {
            IVariableManager variableManager = _bllFactory.BuildIVariableManager();
            List<Variable> variables = new List<Variable>();
            if (deviceId == -1)
            {
                variables = variableManager.GetAllVariableInfo();
            }
            else
            {
                variables = variableManager.GetVariableByDeviceId(deviceId);
            }

            // 清除原有的列
            dataGridView_parameters.Columns.Clear();
            // 手动创建数据列
            DataGridViewTextBoxColumn dgvId = new DataGridViewTextBoxColumn
            {
                Name = "Id",
                HeaderText = "序号",
                DataPropertyName = "Id"
            };
            DataGridViewTextBoxColumn dgvCode = new DataGridViewTextBoxColumn
            {
                Name = "Code",
                HeaderText = "编码",
                DataPropertyName = "Code"
            };
            DataGridViewTextBoxColumn dgvName = new DataGridViewTextBoxColumn
            {
                Name = "Name",
                HeaderText = "参数名",
                DataPropertyName = "Name",
                DefaultCellStyle = new DataGridViewCellStyle() {Alignment = DataGridViewContentAlignment.MiddleLeft},
            };
            DataGridViewTextBoxColumn dgvAddress = new DataGridViewTextBoxColumn
            {
                Name = "Address",
                HeaderText = "寄存器地址",
                DataPropertyName = "Address"
            };
            DataGridViewTextBoxColumn dgvRatio = new DataGridViewTextBoxColumn
            {
                Name = "Ratio",
                HeaderText = "放大倍数",
                DataPropertyName = "Ratio"
            };

            DataGridViewTextBoxColumn dgvUUL = new DataGridViewTextBoxColumn
            {
                Name = "UltimateUpperLimit",
                HeaderText = "控制上上限",
                DataPropertyName = "UltimateUpperLimit",
            };
            DataGridViewTextBoxColumn dgvUL = new DataGridViewTextBoxColumn
            {
                Name = "UpperLimit",
                HeaderText = "控制上限",
                DataPropertyName = "UpperLimit"
            };
            DataGridViewTextBoxColumn dgvLL = new DataGridViewTextBoxColumn
            {
                Name = "LowerLimit",
                HeaderText = "控制下限",
                DataPropertyName = "LowerLimit"
            };
            DataGridViewTextBoxColumn dgvULL = new DataGridViewTextBoxColumn
            {
                Name = "UltimateLowerLimit",
                HeaderText = "控制下下限",
                DataPropertyName = "UltimateLowerLimit"
            };
            DataGridViewTextBoxColumn dgvControlPeriod = new DataGridViewTextBoxColumn
            {
                Name = "ControlPeriod",
                HeaderText = "控制周期",
                DataPropertyName = "ControlPeriod"
            };
            DataGridViewTextBoxColumn dgvOperateDelay = new DataGridViewTextBoxColumn
            {
                Name = "OperateDelay",
                HeaderText = "动作延时",
                DataPropertyName = "OperateDelay"
            };
            DataGridViewTextBoxColumn dgvDeviceId = new DataGridViewTextBoxColumn
            {
                Name = "DeviceId",
                HeaderText = "设备序号",
                DataPropertyName = "DeviceID"
            };
            DataGridViewCheckBoxColumn dgvIsDisplayed = new DataGridViewCheckBoxColumn
            {
                Name = "IsDisplayed",
                HeaderText = "显示变量",
                DataPropertyName = "IsDisplayed"
            };
            DataGridViewCheckBoxColumn dgvIsSaved = new DataGridViewCheckBoxColumn
            {
                Name = "IsSaved",
                HeaderText = "保存变量",
                DataPropertyName = "IsSaved"
            };
            DataGridViewCheckBoxColumn dgvIsFiltered = new DataGridViewCheckBoxColumn
            {
                Name = "IsFiltered",
                HeaderText = "是否滤波",
                DataPropertyName = "IsFiltered"
            };
            DataGridViewTextBoxColumn dgvHistoryListLength = new DataGridViewTextBoxColumn
            {
                Name = "HistoryListLength",
                HeaderText = "历史数据长度",
                DataPropertyName = "HistoryListLength"
            }; DataGridViewTextBoxColumn dgvTrendLength = new DataGridViewTextBoxColumn
            {
                Name = "TrendLength",
                HeaderText = "趋势计算点数",
                DataPropertyName = "TrendLength"
            }; DataGridViewTextBoxColumn dgvTrendInterval = new DataGridViewTextBoxColumn
            {
                Name = "TrendInterval",
                HeaderText = "趋势计算间隔",
                DataPropertyName = "TrendInterval"
            }; DataGridViewTextBoxColumn dgvTrendHigherLimit = new DataGridViewTextBoxColumn
            {
                Name = "TrendHigherLimit",
                HeaderText = "趋势判断上限",
                DataPropertyName = "TrendHigherLimit"
            }; DataGridViewTextBoxColumn dgvTrendLowerLimit = new DataGridViewTextBoxColumn
            {
                Name = "TrendLowerLimit",
                HeaderText = "趋势判断下限",
                DataPropertyName = "TrendLowerLimit"
            }; DataGridViewTextBoxColumn dgvTrendListLength = new DataGridViewTextBoxColumn
            {
                Name = "TrendListLength",
                HeaderText = "趋势判断点数",
                DataPropertyName = "TrendListLength"
            };

            // 添加新建的列
            dataGridView_parameters.Columns.AddRange(new DataGridViewColumn[]
            {
                dgvId,
                dgvCode,
                dgvName,
                dgvAddress,
                dgvRatio,
                dgvUUL,
                dgvUL,
                dgvLL,
                dgvULL,
                dgvControlPeriod,
                dgvOperateDelay,
                dgvDeviceId,
                dgvIsDisplayed,
                dgvIsSaved,
                dgvIsFiltered,
                dgvHistoryListLength,
                dgvTrendLength,
                dgvTrendInterval,
                dgvTrendHigherLimit,
                dgvTrendLowerLimit,
                dgvTrendListLength
            });

            for (int index = 0; index < variables.Count; index++)
            {
                dataGridView_parameters.Rows.Add();
                dataGridView_parameters.Rows[index].Cells["Id"].Value = variables[index].Id;
                dataGridView_parameters.Rows[index].Cells["Code"].Value = variables[index].Code;
                dataGridView_parameters.Rows[index].Cells["Name"].Value = variables[index].Name;
                dataGridView_parameters.Rows[index].Cells["Address"].Value = variables[index].Address;
                dataGridView_parameters.Rows[index].Cells["Ratio"].Value = variables[index].Ratio;
                if (!variables[index].Limit.UltimateHigherLimit.Equals(-1))
                    dataGridView_parameters.Rows[index].Cells["UltimateUpperLimit"].Value =
                        variables[index].Limit.UltimateHigherLimit;
                if (!variables[index].Limit.HigherLimit.Equals(-1))
                    dataGridView_parameters.Rows[index].Cells["UpperLimit"].Value = variables[index].Limit.HigherLimit;
                if (!variables[index].Limit.LowerLimit.Equals(-1))
                    dataGridView_parameters.Rows[index].Cells["LowerLimit"].Value = variables[index].Limit.LowerLimit;
                if (!variables[index].Limit.UltimateLowerLimit.Equals(-1))
                    dataGridView_parameters.Rows[index].Cells["UltimateLowerLimit"].Value =
                        variables[index].Limit.UltimateLowerLimit;
                if (!variables[index].ControlPeriod.Equals(-1))
                    dataGridView_parameters.Rows[index].Cells["ControlPeriod"].Value = variables[index].ControlPeriod;
                if (!variables[index].OperateDelay.Equals(-1))
                    dataGridView_parameters.Rows[index].Cells["OperateDelay"].Value = variables[index].OperateDelay;
                dataGridView_parameters.Rows[index].Cells["DeviceID"].Value = variables[index].DeviceID;
                dataGridView_parameters.Rows[index].Cells["IsDisplayed"].Value = variables[index].IsDisplayed;
                dataGridView_parameters.Rows[index].Cells["IsSaved"].Value = variables[index].IsSaved;

                dataGridView_parameters.Rows[index].Cells["IsFiltered"].Value = variables[index].IsFiltered;
                dataGridView_parameters.Rows[index].Cells["HistoryListLength"].Value = variables[index].HistoryListLength;
                dataGridView_parameters.Rows[index].Cells["TrendLength"].Value = variables[index].TrendLength;
                dataGridView_parameters.Rows[index].Cells["TrendInterval"].Value = variables[index].TrendInterval;
                dataGridView_parameters.Rows[index].Cells["TrendHigherLimit"].Value = variables[index].TrendHigherLimit;
                dataGridView_parameters.Rows[index].Cells["TrendLowerLimit"].Value = variables[index].TrendLowerLimit;
                dataGridView_parameters.Rows[index].Cells["TrendListLength"].Value = variables[index].TrendListLength;
            }
            tssl_parameters_manager.Text = string.Format("查询到 {0} 行数据", variables.Count);
        }

        private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            switch (treeView.SelectedNode.Text)
            {
                case "全部":
                    UpdatePatameterGrid(-1);
                    break;
                case "服务器":
                    UpdatePatameterGrid(0);
                    break;
                case "设备":
                    treeView.SelectedNode = treeView.SelectedNode.NextVisibleNode;
                    break;
                default:
                    UpdatePatameterGrid(Convert.ToInt32(treeView.SelectedNode.Name));
                    break;
            }
        }


        private Variable GetSelectedParameter()
        {
            if (dataGridView_parameters.CurrentRow != null)
            {
                int selectRowIndex = dataGridView_parameters.CurrentRow.Index;
                Variable parameter = new Variable
                {
                    Id = Convert.ToInt32(dataGridView_parameters.Rows[selectRowIndex].Cells["Id"].Value),
                    Code = Convert.ToString(dataGridView_parameters.Rows[selectRowIndex].Cells["Code"].Value),
                    Name = Convert.ToString(dataGridView_parameters.Rows[selectRowIndex].Cells["Name"].Value),
                    Address = Convert.ToUInt16(dataGridView_parameters.Rows[selectRowIndex].Cells["Address"].Value),
                    Ratio = Math.Round(Convert.ToDouble(dataGridView_parameters.Rows[selectRowIndex].Cells["Ratio"].Value), 2),
                    Limit = new Variable.VariableLimit()
                    {
                        HigherLimit = Convert.ToString(dataGridView_parameters.Rows[selectRowIndex].Cells["UpperLimit"].Value) != ""
                            ? Math.Round(Convert.ToDouble(dataGridView_parameters.Rows[selectRowIndex].Cells["UpperLimit"].Value), 2)
                            : -1,
                        LowerLimit = Convert.ToString(dataGridView_parameters.Rows[selectRowIndex].Cells["LowerLimit"].Value) != ""
                            ? Math.Round(Convert.ToDouble(dataGridView_parameters.Rows[selectRowIndex].Cells["LowerLimit"].Value), 2)
                            : -1,
                        UltimateHigherLimit = Convert.ToString(dataGridView_parameters.Rows[selectRowIndex].Cells["UltimateUpperLimit"].Value) != ""
                            ? Math.Round(Convert.ToDouble(dataGridView_parameters.Rows[selectRowIndex].Cells["UltimateUpperLimit"].Value), 2)
                            : -1,
                        UltimateLowerLimit = Convert.ToString(dataGridView_parameters.Rows[selectRowIndex].Cells["UltimateLowerLimit"].Value) != ""
                            ? Math.Round(Convert.ToDouble(dataGridView_parameters.Rows[selectRowIndex].Cells["UltimateLowerLimit"].Value), 2)
                            : -1,
                    },
                    ControlPeriod =  Convert.ToString(dataGridView_parameters.Rows[selectRowIndex].Cells["ControlPeriod"].Value) != ""
                    ? Convert.ToInt32(dataGridView_parameters.Rows[selectRowIndex].Cells["ControlPeriod"].Value)
                    : -1,
                    OperateDelay =  Convert.ToString(dataGridView_parameters.Rows[selectRowIndex].Cells["OperateDelay"].Value) != ""
                    ? Convert.ToInt32(dataGridView_parameters.Rows[selectRowIndex].Cells["OperateDelay"].Value)
                    : -1,
                    DeviceID = Convert.ToUInt32(dataGridView_parameters.Rows[selectRowIndex].Cells["DeviceID"].Value),
                    IsDisplayed = Convert.ToBoolean(dataGridView_parameters.Rows[selectRowIndex].Cells["IsDisplayed"].Value),
                    IsSaved = Convert.ToBoolean(dataGridView_parameters.Rows[selectRowIndex].Cells["IsSaved"].Value),
                    IsFiltered = Convert.ToBoolean(dataGridView_parameters.Rows[selectRowIndex].Cells["IsFiltered"].Value),
                    HistoryListLength = Convert.ToInt32(dataGridView_parameters.Rows[selectRowIndex].Cells["HistoryListLength"].Value),
                    TrendLength = Convert.ToInt32(dataGridView_parameters.Rows[selectRowIndex].Cells["TrendLength"].Value),
                    TrendInterval = Convert.ToInt32(dataGridView_parameters.Rows[selectRowIndex].Cells["TrendInterval"].Value),
                    TrendHigherLimit = Convert.ToDouble(dataGridView_parameters.Rows[selectRowIndex].Cells["TrendHigherLimit"].Value),
                    TrendLowerLimit = Convert.ToDouble(dataGridView_parameters.Rows[selectRowIndex].Cells["TrendLowerLimit"].Value),
                    TrendListLength = Convert.ToInt32(dataGridView_parameters.Rows[selectRowIndex].Cells["TrendListLength"].Value),
                };
                return parameter;
            }
            return new Variable();
        }
        
        private void tsbtn_para_add_Click(object sender, EventArgs e)
        {
            Variable parameter = GetSelectedParameter();
            if (parameter.Name == "") return;
            frmParameterEditor editParameterForm = new frmParameterEditor(DataOperateMode.Insert, parameter);
            if (editParameterForm.ShowDialog() == DialogResult.OK)
            {
                tssl_parameters_manager.Text = string.Format("插入 {0} 行数据",
                    editParameterForm.Result.ToString(CultureInfo.InvariantCulture));
                UpdateUI();
            }
        }

        private void tsbtn_para_edit_Click(object sender, EventArgs e)
        {
            Variable parameter = GetSelectedParameter();
            if (parameter.Name == null) return;
            frmParameterEditor editParameterForm = new frmParameterEditor(DataOperateMode.Edit, parameter);
            if (editParameterForm.ShowDialog() == DialogResult.OK)
            {
                tssl_parameters_manager.Text = string.Format("编辑 {0} 行数据",
                    editParameterForm.Result.ToString(CultureInfo.InvariantCulture));
                UpdateUI();
            }
        }

        private void tsbtn_para_delete_Click(object sender, EventArgs e)
        {
            Variable parameter = GetSelectedParameter();
            if (parameter.Name == null) return;
            frmParameterEditor editParameterForm = new frmParameterEditor(DataOperateMode.Delete, parameter);
            if (editParameterForm.ShowDialog() == DialogResult.OK)
            {
                tssl_parameters_manager.Text = string.Format("删除 {0} 行数据",
                    editParameterForm.Result.ToString(CultureInfo.InvariantCulture));
                UpdateUI();
            }

        }

        private void tsbtn_para_update_Click(object sender, EventArgs e)
        {
            UpdateUI();
        }

        private void tsbtn_para_devices_Click(object sender, EventArgs e)
        {
            frmDevicesManager devicesForm = new frmDevicesManager();
            devicesForm.ShowDialog();
            tsbtn_para_update_Click(sender, e);
        }

        private void dataGridView_parameters_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            tsbtn_para_edit_Click(sender, e);
        }

        private void frmParametersManager_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char) 027)
            {
                this.Close();
                this.Dispose();
            }
        }
    }
}
