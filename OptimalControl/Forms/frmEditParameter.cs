using System;
using System.Data;
using System.Globalization;
using System.Windows.Forms;
using Common;
using OptimalControl.Common;

namespace OptimalControl.Forms
{
    public partial class frmEditParameter : Form
    {
        private readonly DataOperateMode _mode;
        private Parameter _parameter;
        private readonly DataTable _devicesDataTable;

        public int Result { get; private set; }

        public frmEditParameter(DataOperateMode mode, Parameter parameter, DataTable devicesDataTable)
        {
            _mode = mode;
            _parameter = parameter;
            _devicesDataTable = devicesDataTable.Copy();

            DataRow row = _devicesDataTable.NewRow();
            row["Id"] = "0";
            row["Name"] = "服务器";
            _devicesDataTable.Rows.InsertAt(row,0);

            InitializeComponent();
        }

        private void LoadUI(Parameter parameter, DataTable deviceDataTable, string formText, DataOperateMode mode)
        {
            cb_para_device.Items.Clear();
            for (int index = 0; index < deviceDataTable.Rows.Count; index++)
            {
                cb_para_device.Items.Add(string.Format("{0} {1}",
                    Convert.ToString(deviceDataTable.Rows[index][0]),
                    Convert.ToString(deviceDataTable.Rows[index][1])));
            }
            if (mode != DataOperateMode.Insert)
            {

                Text = formText;
                tb_para_name.Text = parameter.Name;
                tb_para_name.Enabled = (mode != DataOperateMode.Delete);
                ntb_para_address.Text = parameter.Address.ToString(CultureInfo.InvariantCulture);
                ntb_para_address.Enabled = (mode != DataOperateMode.Delete);
                DataRow[] dataRows = deviceDataTable.Select(string.Format("Id={0}", parameter.DeviceID));
                cb_para_device.Text = string.Format("{0} {1}",
                    Convert.ToString(dataRows[0][0]),
                    Convert.ToString(dataRows[0][1]));
                cb_para_device.Enabled = (mode != DataOperateMode.Delete);
                tb_para_ratio.Text = parameter.Ratio.ToString(CultureInfo.InvariantCulture);
                tb_para_ratio.Enabled = (mode != DataOperateMode.Delete);
            }
        }

        private string GetSQLCommand(string sqlName)
        {
            string sql = ConfigAppSettings.GetSettingString(sqlName, "");
            sql = sql.Replace("@ParametersTable", ConfigAppSettings.GetSettingString("ParametersTable","Parameters"));
            sql = sql.Replace("@Id", _parameter.Id.ToString(CultureInfo.InvariantCulture));
            sql = sql.Replace("@Name", tb_para_name.Text.Trim());
            sql = sql.Replace("@Address", ntb_para_address.Text.Trim());
            sql = sql.Replace("@Ratio", tb_para_ratio.Text.Trim());
            sql = sql.Replace("@DeviceID", Convert.ToString(_devicesDataTable.Rows[cb_para_device.SelectedIndex][0]));
            return sql;
        }

        private void frmEditParameter_Load(object sender, System.EventArgs e)
        {
            switch (_mode)
            {
                case DataOperateMode.Insert:
                    LoadUI(_parameter, _devicesDataTable, "添加变量", _mode);
                    break;
                case DataOperateMode.Edit:
                    LoadUI(_parameter, _devicesDataTable, "编辑变量", _mode);
                    break;
                case DataOperateMode.Delete:
                    LoadUI(_parameter, _devicesDataTable, "删除变量", _mode);
                    break;
                default:
                    break;
            }
        }

        private void btn_ok_Click(object sender, System.EventArgs e)
        {
            try
            {

                if (tb_para_name.Text.Length < 1)
                {
                    MessageBox.Show("请输入变量名！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (Convert.ToInt32(ntb_para_address.Text) < 1 || Convert.ToInt32(ntb_para_address.Text) > 9999)
                {
                    MessageBox.Show("变量地址错误！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                double ratio;
                if (!double.TryParse(tb_para_ratio.Text.Trim(), out ratio))
                {
                    MessageBox.Show("计算比例错误！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string sql;
                switch (_mode)
                {
                    case DataOperateMode.Insert:
                        sql = GetSQLCommand("SQLInserttParameters");
                        Result = SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text,
                            sql);
                        this.DialogResult = DialogResult.OK;
                        this.Dispose();
                        break;
                    case DataOperateMode.Edit:
                        sql = GetSQLCommand("SQLEdittParameters");
                        Result = SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text,
                            sql);
                        this.DialogResult = DialogResult.OK;
                        this.Dispose();
                        break;
                    case DataOperateMode.Delete:
                        if (
                            MessageBox.Show(
                                string.Format("确认删除变量？" ),
                                "数据删除警告",
                                MessageBoxButtons.OKCancel,
                                MessageBoxIcon.Warning)
                            == DialogResult.OK)
                        {
                            sql = GetSQLCommand("SQLDeletetParameters");
                            Result = SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionStringLocalTransaction,
                                CommandType.Text, sql);
                            this.DialogResult = DialogResult.OK;
                            this.Dispose();
                        }
                        break;
                    default:
                        break;
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
