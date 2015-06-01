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
                return;
            }
            try
            {
                BLLFactory.BLLFactory bllFactory = new BLLFactory.BLLFactory();
                IDeviceManager deviceManager = bllFactory.BuildDeviceManager();
                List<Device> deviceCollection = deviceManager.GetAllDeviceInfo();
                // 如果包含信息
                if (deviceCollection.Count > 0)
                {
                    dataGridView_devices.Columns.Clear();
                    // 手动创建数据列
                    DataGridViewTextBoxColumn dgvId = new DataGridViewTextBoxColumn
                    {
                        Name = "Id",
                        HeaderText = "序号",
                        DataPropertyName = "Id",
                        MinimumWidth = 50,
                        FillWeight = 100,
                    };
                    DataGridViewTextBoxColumn dgvName = new DataGridViewTextBoxColumn
                    {
                        Name = "Name",
                        HeaderText = "设备名",
                        DataPropertyName = "Name",
                        MinimumWidth = 50,
                        FillWeight = 200,
                    };
                    DataGridViewCheckBoxColumn dgvState = new DataGridViewCheckBoxColumn
                    {
                        Name = "State",
                        HeaderText = "启用",
                        DataPropertyName = "State",
                        MinimumWidth = 50,
                        FillWeight = 100,
                    };
                    DataGridViewCheckBoxColumn dgvSyncState = new DataGridViewCheckBoxColumn
                    {
                        Name = "SyncState",
                        HeaderText = "同步数据",
                        DataPropertyName = "SyncState",
                        MinimumWidth = 50,
                        FillWeight = 150,
                    };
                    DataGridViewTextBoxColumn dgvIP = new DataGridViewTextBoxColumn
                    {
                        Name = "IP",
                        HeaderText = "IP地址",
                        DataPropertyName = "IP",
                        MinimumWidth = 50,
                        FillWeight = 200,
                    };
                    DataGridViewTextBoxColumn dgvPort = new DataGridViewTextBoxColumn
                    {
                        Name = "Port",
                        HeaderText = "端口",
                        DataPropertyName = "Port",
                        MinimumWidth = 50,
                        FillWeight = 100,
                    };
                    DataGridViewTextBoxColumn dgvUnitID = new DataGridViewTextBoxColumn
                    {
                        Name = "UnitID",
                        HeaderText = "从站号",
                        DataPropertyName = "UnitID",
                        MinimumWidth = 50,
                        FillWeight = 150,
                    };

                    dataGridView_devices.Columns.AddRange(new DataGridViewColumn[]
                    {
                        dgvId,
                        dgvName,
                        dgvState,
                        dgvSyncState,
                        dgvIP,
                        dgvPort,
                        dgvUnitID,
                    });

                    for (int index = 0; index < deviceCollection.Count; index++)
                    {
                        dataGridView_devices.Rows.Add();
                        dataGridView_devices.Rows[index].Cells["Id"].Value = deviceCollection[index].Id;
                        dataGridView_devices.Rows[index].Cells["Name"].Value = deviceCollection[index].Name;
                        dataGridView_devices.Rows[index].Cells["State"].Value = deviceCollection[index].State;
                        dataGridView_devices.Rows[index].Cells["SyncState"].Value = deviceCollection[index].SyncState;
                        dataGridView_devices.Rows[index].Cells["IP"].Value = deviceCollection[index].ModbusTcpDevice.IP;
                        dataGridView_devices.Rows[index].Cells["Port"].Value = deviceCollection[index].ModbusTcpDevice.Port;
                        dataGridView_devices.Rows[index].Cells["UnitID"].Value = deviceCollection[index].ModbusTcpDevice.UnitID;
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
            if (device.Name == null) return;
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
            if (device.Name == null) return;
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
