using System;
using System.Data;
using System.Globalization;
using System.Net;
using System.Windows.Forms;
using Common;
using OptimalControl.Common;

namespace OptimalControl.Forms
{
    public partial class frmEditDevice : Form
    {
        private readonly DataOperateMode _mode;
        private Device _device;
        public int Result { get; private set; }

        public frmEditDevice(DataOperateMode mode, Device device)
        {
            _mode = mode;
            _device = device;
            InitializeComponent();
        }

        private void LoadUI(Device device, string formText, bool editable)
        {
            Text = formText;
            tb_device_name.Text = device.Name;
            tb_device_name.Enabled = editable;
            nud_device_unitid.Value = device.ModbusTcpDevice.UnitID;
            nud_device_unitid.Enabled = editable;
            cb_device_state.Checked = device.State;
            cb_device_state.Enabled = editable;
            tb_device_ip.Text = device.ModbusTcpDevice.IPAddress;
            tb_device_ip.Enabled = editable;
            ntb_device_port.Text = device.ModbusTcpDevice.PortName.ToString(CultureInfo.InvariantCulture);
            ntb_device_port.Enabled = editable;
            cb_device_sync.Checked = device.SyncState;
            cb_device_sync.Enabled = editable;
        }

        private string GetSQLCommand(string sqlName)
        {
            string sql = ConfigAppSettings.GetSettingString(sqlName, "");
            sql = sql.Replace("@DevicesTable", ConfigAppSettings.GetSettingString("DevicesTable", "Devices"));
            sql = sql.Replace("@Id", _device.Id.ToString(CultureInfo.InvariantCulture));
            sql = sql.Replace("@Name", tb_device_name.Text.Trim());
            sql = sql.Replace("@State", cb_device_state.Checked.ToString());
            sql = sql.Replace("@SyncState", cb_device_sync.Checked.ToString());
            sql = sql.Replace("@IP", tb_device_ip.Text);
            sql = sql.Replace("@Port", ntb_device_port.Text);
            sql = sql.Replace("@UnitID", nud_device_unitid.Text);
            return sql;
        }

        private void frmAddDevice_Load(object sender, System.EventArgs e)
        {
            switch (_mode)
            {
                case DataOperateMode.Insert:
                    break;
                case DataOperateMode.Edit:
                    LoadUI(_device, "编辑设备", true);
                    break;
                case DataOperateMode.Delete:
                    LoadUI(_device, "删除设备", false);
                    break;
                default:
                    break;
            }
        }

        private void btn_ok_Click(object sender, System.EventArgs e)
        {
            try
            {
                IPAddress ip;
                if (!IPAddress.TryParse(tb_device_ip.Text.Trim(), out ip))
                {
                    MessageBox.Show("IP地址格式错误！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (tb_device_name.Text.Length < 1)
                {
                    MessageBox.Show("请输入设备名！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (Convert.ToInt32(ntb_device_port.Text)>65535)
                {
                    MessageBox.Show("端口号错误！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (Convert.ToInt32(nud_device_unitid.Text) > 247)
                {
                    MessageBox.Show("从站号错误！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                string sql = "";
                switch (_mode)
                {
                    case DataOperateMode.Insert:
                        sql = GetSQLCommand("SQLInsertDevices");
                        break;
                    case DataOperateMode.Edit:
                        sql = GetSQLCommand("SQLEditDevices");
                        break;
                    case DataOperateMode.Delete:
                        if (
                            MessageBox.Show(
                                string.Format("确认删除设备'{0}'？\r\n\r\n设备所对应的变量也将全部被删除。", _device.Name),
                                "数据删除警告",
                                MessageBoxButtons.OKCancel,
                                MessageBoxIcon.Warning)
                            == DialogResult.OK)
                        {
                            sql = GetSQLCommand("SQLDeleteDevices");
                        }
                        break;
                    default:
                        break;
                }
                if (sql.Length > 0)
                {
                    Result = SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionStringLocalTransaction,
                        CommandType.Text, sql);

                    this.DialogResult = DialogResult.OK;
                    this.Dispose();
                }
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

    }
}
