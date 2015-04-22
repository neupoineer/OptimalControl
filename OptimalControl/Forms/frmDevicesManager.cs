using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Windows.Forms;
using IBLL.Control;
using Model.Modbus;
using Utility;

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

        public frmDevicesManager()
        {
            InitializeComponent();
            UpdateUI();
        }

        private void UpdateUI()
        {
            UpdateDevicesGrid();
        }

        private delegate void UpdateDevicesGridDelegate();

        private void UpdateDevicesGrid()
        {
            if (InvokeRequired)
            {
                Invoke(new UpdateDevicesGridDelegate(UpdateDevicesGrid));
            }
            try
            {
                BLLFactory.BLLFactory bllFactory = new BLLFactory.BLLFactory();
                IDeviceManager deviceManager = bllFactory.BuildDeviceManager();
                List<Device> deviceCollection = deviceManager.GetAllDeviceInfo();
                // 如果包含信息
                if (deviceCollection.Count > 0)
                {
                    BindingSource source = new BindingSource {DataSource = deviceCollection};
                    dataGridView_devices.DataSource = source;

                    foreach (DataGridViewColumn column in dataGridView_devices.Columns)
                    {
                        switch (column.HeaderText) //更改列名
                        {
                            case "Id":
                                column.HeaderText = "序号";
                                column.DisplayIndex = 0;
                                break;
                            case "Name":
                                column.HeaderText = "设备名";
                                column.DisplayIndex = 1;
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
                    tssl_device_manager.Text = string.Format("查询到 {0} 行数据", deviceCollection.Count);
                }
            }
            catch (Exception ex)
            {
                RecordLog.WriteLogFile("frmDevicesManager", ex.Message);
            }
        }

        private Device GetSelectedDevice()
        {
            if (dataGridView_devices.CurrentRow != null)
            {
                int selectRowIndex = dataGridView_devices.CurrentRow.Index;
                Device device = new Device
                {
                    Id = Convert.ToInt32(dataGridView_devices.Rows[selectRowIndex].Cells["Id"].Value),
                    Name = Convert.ToString(dataGridView_devices.Rows[selectRowIndex].Cells["Name"].Value),
                    State = Convert.ToBoolean(dataGridView_devices.Rows[selectRowIndex].Cells["State"].Value),
                    SyncState = Convert.ToBoolean(dataGridView_devices.Rows[selectRowIndex].Cells["SyncState"].Value),
                    ModbusTcpDevice = new ModbusTcpDevice()
                    {
                        IP = Convert.ToString(dataGridView_devices.Rows[selectRowIndex].Cells["IP"].Value),
                        Port = Convert.ToInt32(dataGridView_devices.Rows[selectRowIndex].Cells["Port"].Value),
                        UnitID = Convert.ToByte(dataGridView_devices.Rows[selectRowIndex].Cells["UnitID"].Value)
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
            frmDeviceEditor addDeviceForm = new frmDeviceEditor(DataOperateMode.Insert, device);
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
            frmDeviceEditor addDeviceForm = new frmDeviceEditor(DataOperateMode.Edit, device);
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
            frmDeviceEditor addDeviceForm = new frmDeviceEditor(DataOperateMode.Delete, device);
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
    }
}
