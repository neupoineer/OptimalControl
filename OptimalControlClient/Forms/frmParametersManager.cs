using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Windows.Forms;
using IBLL.Control;
using Model.Control;
using Model.Modbus;
using Utility;

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
            // 添加新建的列
            dataGridView_parameters.Columns.AddRange(new DataGridViewColumn[]
            {
                dgvId,
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
            });

            for (int index = 0; index < variables.Count; index++)
            {
                dataGridView_parameters.Rows.Add();
                dataGridView_parameters.Rows[index].Cells["Id"].Value = variables[index].Id;
                dataGridView_parameters.Rows[index].Cells["Name"].Value = variables[index].Name;
                dataGridView_parameters.Rows[index].Cells["Address"].Value = variables[index].Address;
                dataGridView_parameters.Rows[index].Cells["Ratio"].Value = variables[index].Ratio;
                if (!variables[index].Limit.UltimateUpperLimit.Equals(-1))
                    dataGridView_parameters.Rows[index].Cells["UltimateUpperLimit"].Value =
                        variables[index].Limit.UltimateUpperLimit;
                if (!variables[index].Limit.UpperLimit.Equals(-1))
                    dataGridView_parameters.Rows[index].Cells["UpperLimit"].Value = variables[index].Limit.UpperLimit;
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
                    Name = Convert.ToString(dataGridView_parameters.Rows[selectRowIndex].Cells["Name"].Value),
                    Address = Convert.ToUInt16(dataGridView_parameters.Rows[selectRowIndex].Cells["Address"].Value),
                    Ratio = Math.Round(Convert.ToDouble(dataGridView_parameters.Rows[selectRowIndex].Cells["Ratio"].Value), 2),
                    Limit = new Variable.VariableLimit()
                    {
                        UpperLimit = Convert.ToString(dataGridView_parameters.Rows[selectRowIndex].Cells["UpperLimit"].Value) != ""
                            ? Math.Round(Convert.ToDouble(dataGridView_parameters.Rows[selectRowIndex].Cells["UpperLimit"].Value), 2)
                            : -1,
                        LowerLimit = Convert.ToString(dataGridView_parameters.Rows[selectRowIndex].Cells["LowerLimit"].Value) != ""
                            ? Math.Round(Convert.ToDouble(dataGridView_parameters.Rows[selectRowIndex].Cells["LowerLimit"].Value), 2)
                            : -1,
                        UltimateUpperLimit = Convert.ToString(dataGridView_parameters.Rows[selectRowIndex].Cells["UltimateUpperLimit"].Value) != ""
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
            if (parameter.Name == "") return;
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
            if (parameter.Name == "") return;
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
