using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;
using IBLL.Control;
using Model.Control;
using Model.Modbus;

namespace OptimalControl.Forms
{
    public partial class frmParameterEditor : Form
    {
        private readonly DataOperateMode _mode;
        private Variable _parameter;
        private BLLFactory.BLLFactory _bllFactory = new BLLFactory.BLLFactory();
        public bool Result { get; private set; }

        public frmParameterEditor(DataOperateMode mode, Variable parameter)
        {
            _mode = mode;
            _parameter = parameter;
            InitializeComponent();
        }

        private void LoadUI(Variable parameter, string formText, DataOperateMode mode)
        {
            cb_para_device.Items.Clear();
            cb_para_device.Items.Add("0 服务器");
            IDeviceManager deviceManager = _bllFactory.BuildDeviceManager();
            List<Device> devices = deviceManager.GetAllDeviceInfo();

            foreach (Device device in devices)
            {
                cb_para_device.Items.Add(string.Format("{0} {1}", device.Id, device.Name));
            }
            if (mode != DataOperateMode.Insert)
            {
                Text = formText;
                tb_para_code.Text = parameter.Code;
                tb_para_code.Enabled = (mode != DataOperateMode.Delete);
                tb_para_name.Text = parameter.Name;
                tb_para_name.Enabled = (mode != DataOperateMode.Delete);
                ntb_para_address.Text = parameter.Address.ToString(CultureInfo.InvariantCulture);
                ntb_para_address.Enabled = (mode != DataOperateMode.Delete);

                if (parameter.DeviceID == 0)
                {
                    cb_para_device.Text = "0 服务器";
                }
                else
                {
                    Device device = deviceManager.GetDeviceInfoById(Convert.ToInt32(parameter.DeviceID));
                    cb_para_device.Text = string.Format("{0} {1}", Convert.ToString(device.Id),
                        Convert.ToString(device.Name));
                }
                cb_para_device.Enabled = (mode != DataOperateMode.Delete);
                tb_para_ratio.Text = parameter.Ratio.ToString(CultureInfo.InvariantCulture);
                tb_para_ratio.Enabled = (mode != DataOperateMode.Delete);

                cb_para_isenabled.Checked = parameter.IsEnabled;
                cb_para_isenabled.Enabled = (mode != DataOperateMode.Delete);
                cb_para_isread.Checked = parameter.IsRead;
                cb_para_isread.Enabled = (mode != DataOperateMode.Delete);
                cb_para_isoutput.Checked = parameter.IsOutput;
                cb_para_isoutput.Enabled = (mode != DataOperateMode.Delete);
                cb_para_isdisplayed.Checked = parameter.IsDisplayed;
                cb_para_isdisplayed.Enabled = (mode != DataOperateMode.Delete);
                cb_para_issaved.Checked = parameter.IsSaved;
                cb_para_issaved.Enabled = (mode != DataOperateMode.Delete);
                cb_para_isfiltered.Checked = parameter.IsFiltered;
                cb_para_isfiltered.Enabled = (mode != DataOperateMode.Delete);
                tb_trend_length.Text = parameter.TrendLength.ToString(CultureInfo.InvariantCulture);
                tb_trend_length.Enabled = (mode != DataOperateMode.Delete);
                tb_trend_interval.Text = parameter.TrendInterval.ToString(CultureInfo.InvariantCulture);
                tb_trend_interval.Enabled = (mode != DataOperateMode.Delete);
                tb_trend_highter.Text = parameter.TrendHigherLimit.ToString(CultureInfo.InvariantCulture);
                tb_trend_highter.Enabled = (mode != DataOperateMode.Delete);
                tb_trend_lower.Text = parameter.TrendLowerLimit.ToString(CultureInfo.InvariantCulture);
                tb_trend_lower.Enabled = (mode != DataOperateMode.Delete);
                tb_trend_listlength.Text = parameter.TrendListLength.ToString(CultureInfo.InvariantCulture);
                tb_trend_listlength.Enabled = (mode != DataOperateMode.Delete);
                tb_history_listlength.Text = parameter.HistoryListLength.ToString(CultureInfo.InvariantCulture);
                tb_history_listlength.Enabled = (mode != DataOperateMode.Delete);

                if (!parameter.ControlPeriod.Equals(-1))
                {
                    ntb_para_period.Text = parameter.ControlPeriod.ToString(CultureInfo.InvariantCulture);
                }
                ntb_para_period.Enabled = (mode != DataOperateMode.Delete);
                if (!parameter.OperateDelay.Equals(-1))
                {
                    ntb_para_delay.Text = parameter.OperateDelay.ToString(CultureInfo.InvariantCulture);
                }
                ntb_para_delay.Enabled = (mode != DataOperateMode.Delete);

                if (!parameter.Limit.HigherLimit.Equals(-1))
                {
                    tb_para_upperlimit.Text = parameter.Limit.HigherLimit.ToString(CultureInfo.InvariantCulture);
                }
                tb_para_upperlimit.Enabled = (mode != DataOperateMode.Delete);
                if (!parameter.Limit.LowerLimit.Equals(-1))
                {
                    tb_para_lowerlimit.Text = parameter.Limit.LowerLimit.ToString(CultureInfo.InvariantCulture);
                }
                tb_para_lowerlimit.Enabled = (mode != DataOperateMode.Delete);
                if (!parameter.Limit.UltimateHighLimit.Equals(-1))
                {
                    tb_para_uulimit.Text = parameter.Limit.UltimateHighLimit.ToString(CultureInfo.InvariantCulture);
                }
                tb_para_uulimit.Enabled = (mode != DataOperateMode.Delete);
                if (!parameter.Limit.UltimateLowLimit.Equals(-1))
                {
                    tb_para_ullimit.Text = parameter.Limit.UltimateLowLimit.ToString(CultureInfo.InvariantCulture);
                }
                tb_para_ullimit.Enabled = (mode != DataOperateMode.Delete);
            }
        }

        private Variable GetCurrentParameter()
        {
            Variable variable = new Variable()
            {
                Id = _parameter.Id,
                Code = tb_para_code.Text.Trim(),
                Name = tb_para_name.Text.Trim(),
                Address = Convert.ToInt32(ntb_para_address.Text.Trim()),
                Ratio = Convert.ToDouble(tb_para_ratio.Text.Trim()),
                IsEnabled = cb_para_isenabled.Checked,
                IsRead = cb_para_isread.Checked,
                IsOutput = cb_para_isoutput.Checked,
                IsDisplayed = cb_para_isdisplayed.Checked,
                IsSaved = cb_para_issaved.Checked,
                Limit = new Variable.VariableLimit()
                {
                    HigherLimit = tb_para_upperlimit.Text != "" ? Convert.ToDouble(tb_para_upperlimit.Text.Trim()) : -1,
                    LowerLimit = tb_para_lowerlimit.Text != "" ? Convert.ToDouble(tb_para_lowerlimit.Text.Trim()) : -1,
                    UltimateHighLimit = tb_para_uulimit.Text != "" ? Convert.ToDouble(tb_para_uulimit.Text.Trim()) : -1,
                    UltimateLowLimit = tb_para_ullimit.Text != "" ? Convert.ToDouble(tb_para_ullimit.Text.Trim()) : -1,
                },
                ControlPeriod = ntb_para_period.Text != "" ? Convert.ToInt32(ntb_para_period.Text.Trim()) : -1,
                OperateDelay = ntb_para_delay.Text != "" ? Convert.ToInt32(ntb_para_delay.Text.Trim()) : -1,
                DeviceID = Convert.ToUInt32(cb_para_device.Text.Split(' ')[0]),
                IsFiltered = cb_para_isfiltered.Checked,
                TrendLength = Convert.ToInt32(tb_trend_length.Text),
                TrendInterval = Convert.ToInt32(tb_trend_interval.Text),
                TrendHigherLimit = Convert.ToDouble(tb_trend_highter.Text),
                TrendLowerLimit = Convert.ToDouble(tb_trend_lower.Text),
                TrendListLength = Convert.ToInt32(tb_trend_listlength.Text),
                HistoryListLength = Convert.ToInt32(tb_history_listlength.Text),
            };
            return variable;
        }

        private void frmEditParameter_Load(object sender, System.EventArgs e)
        {
            switch (_mode)
            {
                case DataOperateMode.Insert:
                    LoadUI(_parameter, "添加变量", _mode);
                    break;
                case DataOperateMode.Edit:
                    LoadUI(_parameter, "编辑变量", _mode);
                    break;
                case DataOperateMode.Delete:
                    LoadUI(_parameter, "删除变量", _mode);
                    break;
                default:
                    break;
            }
        }

        private void btn_ok_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (tb_para_code.Text.Length < 1)
                {
                    MessageBox.Show("必须输入变量编码！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
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
                    MessageBox.Show("放大倍数错误！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (
                    ((tb_para_uulimit.Text != "")
                     && (tb_para_upperlimit.Text != "")
                     && ((Convert.ToDouble(tb_para_uulimit.Text) < Convert.ToDouble(tb_para_upperlimit.Text))))
                    || ((tb_para_upperlimit.Text != "")
                        && (tb_para_lowerlimit.Text != "")
                        && ((Convert.ToDouble(tb_para_upperlimit.Text) < Convert.ToDouble(tb_para_lowerlimit.Text))))
                    || ((tb_para_lowerlimit.Text != "")
                        && (tb_para_ullimit.Text != "")
                        && ((Convert.ToDouble(tb_para_lowerlimit.Text) < Convert.ToDouble(tb_para_ullimit.Text))))
                    || ((tb_para_uulimit.Text != "")
                        && (tb_para_lowerlimit.Text != "")
                        && ((Convert.ToDouble(tb_para_uulimit.Text) < Convert.ToDouble(tb_para_lowerlimit.Text))))
                    || ((tb_para_uulimit.Text != "")
                        && (tb_para_ullimit.Text != "")
                        && ((Convert.ToDouble(tb_para_uulimit.Text) < Convert.ToDouble(tb_para_ullimit.Text))))
                    || ((tb_para_upperlimit.Text != "")
                        && (tb_para_ullimit.Text != "")
                        && ((Convert.ToDouble(tb_para_upperlimit.Text) < Convert.ToDouble(tb_para_ullimit.Text))))
                    )

                {
                    MessageBox.Show("变量上下限设置错误！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                IVariableManager variableManager = _bllFactory.BuildIVariableManager();

                switch (_mode)
                {
                    case DataOperateMode.Insert:
                        Result = variableManager.AddVariable(GetCurrentParameter());
                        this.DialogResult = DialogResult.OK;
                        this.Dispose();
                        break;
                    case DataOperateMode.Edit:
                        Result = variableManager.ModifyVariable(GetCurrentParameter());
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
                            Result = variableManager.DeleteVariableById(_parameter.Id);
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
