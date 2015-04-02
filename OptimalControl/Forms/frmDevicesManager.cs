using System;
using System.Data;
using System.Globalization;
using System.Windows.Forms;
using OptimalControl.Common;

namespace OptimalControl.Forms
{
    /// <summary>
    /// 数据操作类型
    /// </summary>
    public enum DataOperateMode
    {
        Insert,
        Edit,
        Delete
    }

    public partial class frmDevicesManager : Form
    {
        private string SQLGetDevices = ConfigAppSettings.GetSettingString("SQLGetDevices", "SELECT * FROM @DevicesTable");
        private string DevicesTable = ConfigAppSettings.GetSettingString("DevicesTable", "Devices");

        public frmDevicesManager()
        {
            InitializeComponent();
            UpdateUI();
        }

        private string GetDevicesCommand(string sqlCmd, string table)
        {
            string sql = sqlCmd;
            sql = sql.Replace("@DevicesTable", table);
            return sql;
        }

        private void UpdateUI()
        {
            DataTable deviceDataTable = SQLHelper.ExcuteDataTable(SQLHelper.ConnectionStringLocalTransaction,
                GetDevicesCommand(SQLGetDevices, DevicesTable));
            UpdateDevicesGrid(deviceDataTable, "1=1");
            tssl_device_manager.Text = string.Format("查询到 {0} 行数据", deviceDataTable.Rows.Count);
        }

        private void UpdateDevicesGrid(DataTable dataTable, string filter)
        {
            DataRow[] data = dataTable.Select(filter);
            DataTable table = dataTable.Clone();
            foreach (DataRow row in data)
            {
                table.Rows.Add(row.ItemArray);
            }

            dataGridView_devices.DataSource = table;

            foreach (DataGridViewColumn column in dataGridView_devices.Columns)
            {
                switch (column.HeaderText) //更改列名
                {
                    case "Id":
                        column.HeaderText = "序号";
                        break;
                    case "Name":
                        column.HeaderText = "设备名";
                        break;
                    case "State":
                        column.HeaderText = "启用";
                        break;
                    case "SyncState":
                        column.HeaderText = "同步数据";
                        break;
                    case "IP":
                        column.HeaderText = "IP地址";
                        break;
                    case "Port":
                        column.HeaderText = "端口";
                        break;
                    case "UnitID":
                        column.HeaderText = "从站号";
                        break;
                    default:
                        break;
                }
            }
        }

        private Device GetSelectedDevice()
        {
            if (dataGridView_devices.CurrentRow != null)
            {
                int selectRowIndex = dataGridView_devices.CurrentRow.Index;
                Device device = new Device
                {
                    Id = Convert.ToInt32(dataGridView_devices.Rows[selectRowIndex].Cells[0].Value),
                    Name = Convert.ToString(dataGridView_devices.Rows[selectRowIndex].Cells[1].Value),
                    State = Convert.ToBoolean(dataGridView_devices.Rows[selectRowIndex].Cells[2].Value),
                    SyncState = Convert.ToBoolean(dataGridView_devices.Rows[selectRowIndex].Cells[3].Value),
                    ModbusTcpDevice =
                    {
                        IPAddress = Convert.ToString(dataGridView_devices.Rows[selectRowIndex].Cells[4].Value),
                        PortName = Convert.ToInt32(dataGridView_devices.Rows[selectRowIndex].Cells[5].Value),
                        UnitID = Convert.ToByte(dataGridView_devices.Rows[selectRowIndex].Cells[6].Value)
                    }
                };
                return device;
            }
            return new Device();
        }

        private void tsbtn_device_add_Click(object sender, EventArgs e)
        {
            Device device = GetSelectedDevice();
            if (device.Name == "") return;
            frmEditDevice addDeviceForm = new frmEditDevice(DataOperateMode.Insert, device);
            if (addDeviceForm.ShowDialog() == DialogResult.OK)
            {
                tssl_device_manager.Text = string.Format("插入 {0} 行数据",
                    addDeviceForm.Result.ToString(CultureInfo.InvariantCulture));
                UpdateUI();
            }
        }

        private void tsbtn_device_edit_Click(object sender, EventArgs e)
        {
            Device device = GetSelectedDevice();
            if (device.Name == "") return;
            frmEditDevice addDeviceForm = new frmEditDevice(DataOperateMode.Edit, device);
            if (addDeviceForm.ShowDialog() == DialogResult.OK)
            {
                tssl_device_manager.Text = string.Format("编辑 {0} 行数据",
                    addDeviceForm.Result.ToString(CultureInfo.InvariantCulture));
                UpdateUI();
            }
        }


        private void tsbtn_device_delete_Click(object sender, EventArgs e)
        {
            Device device = GetSelectedDevice();
            if (device.Name == "") return;
            frmEditDevice addDeviceForm = new frmEditDevice(DataOperateMode.Delete, device);
            if (addDeviceForm.ShowDialog() == DialogResult.OK)
            {
                tssl_device_manager.Text = string.Format("删除 {0} 行数据",
                    addDeviceForm.Result.ToString(CultureInfo.InvariantCulture));
                UpdateUI();
            }
        }

        private void tsbtn_device_update_Click(object sender, EventArgs e)
        {
            UpdateUI();
        }

        private void dataGridView_devices_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            tsbtn_device_edit_Click(sender, e);
        }

        private void frmDevicesManager_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)027)
            {
                this.Close();
                this.Dispose();
            }
        }

        private void frmDevicesManager_Load(object sender, EventArgs e)
        {

        }
    }
}
