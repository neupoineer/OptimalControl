using System;
using System.Data;
using System.Globalization;
using System.Windows.Forms;
using Common;

namespace OptimalControl.Forms
{
    public partial class frmParametersManager : Form
    {
        DataTable ParameterDataTable = new DataTable();
        DataTable DeviceDataTable = new DataTable();

        private string SQLGetDevices = ConfigAppSettings.GetSettingString("SQLGetDevices", "SELECT * FROM @DevicesTable");
        private string DevicesTable = ConfigAppSettings.GetSettingString("DevicesTable", "Devices");

        private string SQLGetParameters = ConfigAppSettings.GetSettingString("SQLGetParameters",
                    "SELECT * FROM @ParametersTable WHERE DeviceID = @DeviceID");
        private string ParametersTable = ConfigAppSettings.GetSettingString("ParametersTable", "Parameters");

        public frmParametersManager()
        {
            InitializeComponent();
            UpdateUI();
        }

        private void UpdateUI()
        {
            DeviceDataTable = SQLHelper.ExcuteDataTable(SQLHelper.ConnectionStringLocalTransaction,
                GetDevicesCommand(SQLGetDevices, DevicesTable));
            treeView.Nodes.Clear();
            TreeNode rootNode = treeView.Nodes.Add("全部");
            TreeNode secondNode = rootNode.Nodes.Add("设备");
            for (int index = 0; index < DeviceDataTable.Rows.Count; index++)
            {
                if (Convert.ToBoolean(DeviceDataTable.Rows[index][2]))
                {
                    secondNode.Nodes.Add(Convert.ToString(DeviceDataTable.Rows[index][0]),
                        Convert.ToString(DeviceDataTable.Rows[index][1]));
                }
            }
            rootNode.Nodes.Add("服务器");
            treeView.ExpandAll();

            ParameterDataTable = SQLHelper.ExcuteDataTable(SQLHelper.ConnectionStringLocalTransaction,
                GetParametersCommand(SQLGetParameters, ParametersTable));
            UpdatePatameterGrid(ParameterDataTable, string.Format("0=0"));

            tssl_parameters_manager.Text = string.Format("查询到 {0} 行数据", ParameterDataTable.Rows.Count);

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
                        column.HeaderText = "计算比例";
                        break;
                    case "DeviceID":
                        column.HeaderText = "设备序号";
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
                    UpdatePatameterGrid(ParameterDataTable,string.Format("0=0"));
                    break;
                case "设备":
                    UpdatePatameterGrid(ParameterDataTable, string.Format("DeviceID>{0}", 0));
                    break;
                case "服务器":
                    UpdatePatameterGrid(ParameterDataTable, string.Format("DeviceID={0}", 0));
                    break;
                default:
                    UpdatePatameterGrid(ParameterDataTable, string.Format("DeviceID={0}", treeView.SelectedNode.Name));
                    break;
            }
        }


        private Parameter GetSelectedParameter()
        {
            if (dataGridView_parameters.CurrentRow != null)
            {
                int selectRowIndex = dataGridView_parameters.CurrentRow.Index;
                Parameter parameter = new Parameter
                {
                    Id = Convert.ToInt32(dataGridView_parameters.Rows[selectRowIndex].Cells[0].Value),
                    Name = Convert.ToString(dataGridView_parameters.Rows[selectRowIndex].Cells[1].Value),
                    Address = Convert.ToUInt16(dataGridView_parameters.Rows[selectRowIndex].Cells[2].Value),
                    Ratio = Math.Round(Convert.ToDouble(dataGridView_parameters.Rows[selectRowIndex].Cells[3].Value),2),
                    DeviceID = Convert.ToInt32(dataGridView_parameters.Rows[selectRowIndex].Cells[4].Value),
                };
                return parameter;
            }
            return new Parameter();
        }
        
        private void tsbtn_para_add_Click(object sender, EventArgs e)
        {
            Parameter parameter = GetSelectedParameter();
            if (parameter.Name == "") return;
            frmEditParameter editParameterForm = new frmEditParameter(DataOperateMode.Insert, parameter, DeviceDataTable);
            if (editParameterForm.ShowDialog() == DialogResult.OK)
            {
                tssl_parameters_manager.Text = string.Format("插入 {0} 行数据",
                    editParameterForm.Result.ToString(CultureInfo.InvariantCulture));
                UpdateUI();
            }
        }

        private void tsbtn_para_edit_Click(object sender, EventArgs e)
        {
            Parameter parameter = GetSelectedParameter();
            if (parameter.Name == "") return;
            frmEditParameter editParameterForm = new frmEditParameter(DataOperateMode.Edit, parameter, DeviceDataTable);
            if (editParameterForm.ShowDialog() == DialogResult.OK)
            {
                tssl_parameters_manager.Text = string.Format("编辑 {0} 行数据",
                    editParameterForm.Result.ToString(CultureInfo.InvariantCulture));
                UpdateUI();
            }
        }

        private void tsbtn_para_delete_Click(object sender, EventArgs e)
        {
            Parameter parameter = GetSelectedParameter();
            if (parameter.Name == "") return;
            frmEditParameter editParameterForm = new frmEditParameter(DataOperateMode.Delete, parameter, DeviceDataTable);
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
