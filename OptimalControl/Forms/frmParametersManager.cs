using System;
using System.Data;
using System.Globalization;
using System.Windows.Forms;
using Model.Control;
using Utility;

namespace OptimalControl.Forms
{
    public partial class frmParametersManager : Form
    {
        DataTable _parameterDataTable = new DataTable();
        DataTable _deviceDataTable = new DataTable();

        private string SQLGetDevices = ConfigAppSettings.GetSettingString("SQLGetDevices", "SELECT * FROM @DevicesTable");
        private string DevicesTable = ConfigAppSettings.GetSettingString("DevicesTable", "Device");

        private string SQLGetParameters = ConfigAppSettings.GetSettingString("SQLGetParameters",
                    "SELECT * FROM @ParametersTable WHERE DeviceID = @DeviceID");
        private string ParametersTable = ConfigAppSettings.GetSettingString("ParametersTable", "Parameter");

        public frmParametersManager()
        {
            InitializeComponent();
            UpdateUI();
        }

        private void UpdateUI()
        {
            _deviceDataTable = SQLHelper.ExcuteDataTable(SQLHelper.ConnectionStringLocalTransaction,
                GetDevicesCommand(SQLGetDevices, DevicesTable));
            treeView.Nodes.Clear();
            TreeNode rootNode = treeView.Nodes.Add("全部");
            TreeNode secondNode = rootNode.Nodes.Add("设备");
            for (int index = 0; index < _deviceDataTable.Rows.Count; index++)
            {
                if (Convert.ToBoolean(_deviceDataTable.Rows[index][2]))
                {
                    secondNode.Nodes.Add(Convert.ToString(_deviceDataTable.Rows[index][0]),
                        Convert.ToString(_deviceDataTable.Rows[index][1]));
                }
            }
            rootNode.Nodes.Add("服务器");
            treeView.ExpandAll();

            _parameterDataTable = SQLHelper.ExcuteDataTable(SQLHelper.ConnectionStringLocalTransaction,
                GetParametersCommand(SQLGetParameters, ParametersTable));
            UpdatePatameterGrid(_parameterDataTable, string.Format("0=0"));

            tssl_parameters_manager.Text = string.Format("查询到 {0} 行数据", _parameterDataTable.Rows.Count);

        }

        private string GetDevicesCommand(string sqlCmd, string table)
        {
            string sql = sqlCmd;
            sql = sql.Replace("@DevicesTable", table);
            return sql;
        }

        private string GetParametersCommand(string sqlCmd, string table)
        {
            string sql = sqlCmd;
            sql = sql.Replace("@ParametersTable", table);
            sql = sql.Replace("@DeviceID", "1");
            sql = sql.Replace("DeviceID", "1");
            return sql;
        }

        private void UpdatePatameterGrid(DataTable dataTable, string filter)
        {
            

            DataRow[] data =  dataTable.Select(filter);
            DataTable table = dataTable.Clone();
            foreach (DataRow row in data)
            {
                table.Rows.Add(row.ItemArray);
            }

            dataGridView_parameters.DataSource = table;

            foreach (DataGridViewColumn column in dataGridView_parameters.Columns)
            {
                switch (column.HeaderText) //更改列名
                {
                    case "Id":
                        column.HeaderText = "序号";
                        break;
                    case "Name":
                        column.HeaderText = "参数名";
                        break;
                    case "Address":
                        column.HeaderText = "寄存器地址";
                        break;
                    case "Ratio":
                        column.HeaderText = "放大倍数";
                        break;
                    case "DeviceID":
                        column.HeaderText = "设备序号";
                        break;
                    case "ControlPeriod":
                        column.HeaderText = "控制周期";
                        break;
                    case "OperateDelay":
                        column.HeaderText = "动作延时";
                        break;
                    case "UpperLimit":
                        column.HeaderText = "控制上限";
                        break;
                    case "LowerLimit":
                        column.HeaderText = "控制下限";
                        break;
                    case "UltimateUpperLimit":
                        column.HeaderText = "控制上上限";
                        break;
                    case "UltimateLowerLimit":
                        column.HeaderText = "控制下下限";
                        break;
                    default:
                        break;
                }
            }
        }

        private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            switch (treeView.SelectedNode.Text)
            {
                case "全部":
                    UpdatePatameterGrid(_parameterDataTable,string.Format("0=0"));
                    break;
                case "设备":
                    UpdatePatameterGrid(_parameterDataTable, string.Format("DeviceID>{0}", 0));
                    break;
                case "服务器":
                    UpdatePatameterGrid(_parameterDataTable, string.Format("DeviceID={0}", 0));
                    break;
                default:
                    UpdatePatameterGrid(_parameterDataTable, string.Format("DeviceID={0}", treeView.SelectedNode.Name));
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
                    Id = Convert.ToInt32(dataGridView_parameters.Rows[selectRowIndex].Cells[0].Value),
                    Name = Convert.ToString(dataGridView_parameters.Rows[selectRowIndex].Cells[1].Value),
                    Address = Convert.ToUInt16(dataGridView_parameters.Rows[selectRowIndex].Cells[2].Value),
                    Ratio = Math.Round(Convert.ToDouble(dataGridView_parameters.Rows[selectRowIndex].Cells[3].Value), 2),
                    Limit = new Variable.VariableLimit()
                    {
                        UpperLimit = Convert.ToString(dataGridView_parameters.Rows[selectRowIndex].Cells[4].Value) != ""
                            ? Math.Round(Convert.ToDouble(dataGridView_parameters.Rows[selectRowIndex].Cells[4].Value),2)
                            : -1,
                        LowerLimit = Convert.ToString(dataGridView_parameters.Rows[selectRowIndex].Cells[5].Value) != ""
                            ? Math.Round(Convert.ToDouble(dataGridView_parameters.Rows[selectRowIndex].Cells[5].Value), 2)
                            : -1,
                        UltimateUpperLimit = Convert.ToString(dataGridView_parameters.Rows[selectRowIndex].Cells[6].Value) != ""
                            ? Math.Round(Convert.ToDouble(dataGridView_parameters.Rows[selectRowIndex].Cells[6].Value), 2)
                            : -1,
                        UltimateLowerLimit = Convert.ToString(dataGridView_parameters.Rows[selectRowIndex].Cells[7].Value) != ""
                            ? Math.Round(Convert.ToDouble(dataGridView_parameters.Rows[selectRowIndex].Cells[7].Value), 2)
                            : -1,
                    },
                    ControlPeriod =  Convert.ToString(dataGridView_parameters.Rows[selectRowIndex].Cells[8].Value) != ""
                    ? Convert.ToInt32(dataGridView_parameters.Rows[selectRowIndex].Cells[8].Value)
                    : -1,
                    OperateDelay =  Convert.ToString(dataGridView_parameters.Rows[selectRowIndex].Cells[9].Value) != ""
                    ? Convert.ToInt32(dataGridView_parameters.Rows[selectRowIndex].Cells[9].Value)
                    : -1,
                    DeviceID = Convert.ToUInt32(dataGridView_parameters.Rows[selectRowIndex].Cells[10].Value),
                };
                return parameter;
            }
            return new Variable();
        }
        
        private void tsbtn_para_add_Click(object sender, EventArgs e)
        {
            Variable parameter = GetSelectedParameter();
            if (parameter.Name == "") return;
            frmParameterEditor editParameterForm = new frmParameterEditor(DataOperateMode.Insert, parameter, _deviceDataTable);
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
            frmParameterEditor editParameterForm = new frmParameterEditor(DataOperateMode.Edit, parameter, _deviceDataTable);
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
            frmParameterEditor editParameterForm = new frmParameterEditor(DataOperateMode.Delete, parameter, _deviceDataTable);
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
