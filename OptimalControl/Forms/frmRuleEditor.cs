using System;
using System.Data;
using System.Globalization;
using System.Net;
using System.Windows.Forms;
using Utility;
using Utility.Modbus;

namespace OptimalControl.Forms
{
    public partial class frmRuleEditor : Form
    {
        private readonly DataOperateMode _mode;
        private ExpertSystem.Rule _rule;
        private readonly DataTable _parameterDataTable;
        public int Result { get; private set; }

        public frmRuleEditor(DataOperateMode mode, ExpertSystem.Rule rule, DataTable parameterDataTable)
        {
            _mode = mode;
            _rule = rule;
            _parameterDataTable = parameterDataTable.Copy();
            InitializeComponent();
        }

        private void LoadUI(ExpertSystem.Rule rule, DataTable parameterDataTable, string formText, bool editable)
        {
            cb_parameter.Items.Clear();
            for (int index = 0; index < parameterDataTable.Rows.Count; index++)
            {
                cb_parameter.Items.Add(Convert.ToString(parameterDataTable.Rows[index][1]));
            }

            cb_operator.Items.Clear();
            cb_operator.Items.AddRange(new object[]
            {"(", "tan", ")", "atan", "!", "*", "/", "%", "+", "-", "<", ">", "=", "&", "|"});

            Text = formText;
            tb_rule_name.Text = rule.Name;
            tb_rule_name.Enabled = editable;
            cb_rule_enabled.Checked = rule.Enabled;
            cb_rule_enabled.Enabled = editable;
            tb_rule_expression.Text = rule.Expression;
            tb_rule_expression.Enabled = editable;
            ntb_rule_operation.Text = rule.Operatioin;
            ntb_rule_operation.Enabled = editable;
        }

        private string GetSQLCommand(string sqlName)
        {
            string sql = ConfigAppSettings.GetSettingString(sqlName, "");
            sql = sql.Replace("@DevicesTable", ConfigAppSettings.GetSettingString("DevicesTable", "Devices"));
            //sql = sql.Replace("@Id", _device.Id.ToString(CultureInfo.InvariantCulture));
            sql = sql.Replace("@Name", tb_rule_name.Text.Trim());
            sql = sql.Replace("@State", cb_rule_enabled.Checked.ToString());
            //sql = sql.Replace("@SyncState", cb_device_sync.Checked.ToString());
            sql = sql.Replace("@IP", tb_rule_expression.Text);
            sql = sql.Replace("@Port", ntb_rule_operation.Text);
            //sql = sql.Replace("@UnitID", nud_device_unitid.Text);
            return sql;
        }

        private void frmAddDevice_Load(object sender, System.EventArgs e)
        {
            switch (_mode)
            {
                case DataOperateMode.Insert:
                    break;
                case DataOperateMode.Edit:
                    LoadUI(_rule, _parameterDataTable, "编辑设备", true);
                    break;
                case DataOperateMode.Delete:
                    LoadUI(_rule, _parameterDataTable, "删除设备", false);
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
                if (!IPAddress.TryParse(tb_rule_expression.Text.Trim(), out ip))
                {
                    MessageBox.Show("IP地址格式错误！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (tb_rule_name.Text.Length < 1)
                {
                    MessageBox.Show("请输入设备名！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (Convert.ToInt32(ntb_rule_operation.Text)>65535)
                {
                    MessageBox.Show("端口号错误！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                //if (Convert.ToInt32(nud_device_unitid.Text) > 247)
                //{
                //    MessageBox.Show("从站号错误！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    return;
                //}
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
                        //if (
                        //    MessageBox.Show(
                        //        string.Format("确认删除设备'{0}'？\r\n\r\n设备所对应的变量也将全部被删除。", _device.Name),
                        //        "数据删除警告",
                        //        MessageBoxButtons.OKCancel,
                        //        MessageBoxIcon.Warning)
                        //    == DialogResult.OK)
                        //{
                        //    sql = GetSQLCommand("SQLDeleteDevices");
                        //}
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
