using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Windows.Forms;
using IBLL.Control;
using Model.Modbus;
using Utility;
using Model.Control;
using ZedGraph;

namespace OptimalControl.Forms
{
    public partial class frmCurveEditor : Form
    {
        private readonly DataOperateMode _mode;
        private Curve _curve;
        private BLLFactory.BLLFactory _bllFactory = new BLLFactory.BLLFactory();
        private List<Variable> _variableCollection = new List<Variable>();
        private List<Device> _deviceCollection = new List<Device>();
        private IDeviceManager _deviceManager;
        private IVariableManager _variableManager;

        public bool Result { get; private set; }

        public frmCurveEditor(DataOperateMode mode, Curve curve)
        {
            _mode = mode;
            _curve = curve;
            _variableManager = _bllFactory.BuildIVariableManager();
            _deviceManager = _bllFactory.BuildDeviceManager();
            _variableCollection = _variableManager.GetAllVariableInfo();
            _deviceCollection = _deviceManager.GetAllDeviceInfo();
            InitializeComponent();
            cb_curve_name.DrawMode = DrawMode.OwnerDrawVariable;
            cb_curve_color.DrawMode = DrawMode.OwnerDrawVariable;
        }

        private void LoadUI(Curve curve, string formText, DataOperateMode mode)
        {
            cb_curve_name.Items.Clear();
            foreach (Variable variable in _variableCollection)
            {
                cb_curve_name.Items.Add(variable.Name);
            }

            cb_curve_device.Items.Clear();
            cb_curve_device.Items.Add("0 服务器");
            foreach (Device device in _deviceCollection)
            {
                cb_curve_device.Items.Add(string.Format("{0} {1}", device.Id, device.Name));
            }

            ArrayList colorList = new ArrayList();
            cb_curve_color.Items.Clear();
            cb_curve_color.Items.Add("");
            foreach (PropertyInfo propertyInfo in typeof (Color).GetProperties())
            {
                if (propertyInfo.PropertyType.FullName != "System.Drawing.Color") continue;
                if (propertyInfo.Name == "Transparent") continue;
                cb_curve_color.Items.Add(propertyInfo.Name);
                colorList.Add(propertyInfo.Name);
            }

            cb_curve_type.Items.Clear();
            cb_curve_type.Items.Add("");
            cb_curve_type.Items.Add("点线");
            cb_curve_type.Items.Add("散点");

            cb_curve_symbol.Items.Clear();
            cb_curve_symbol.Items.Add("");
            foreach (var type in Enum.GetValues(typeof (SymbolType)))
            {
                if (type.ToString().Equals("Default") || type.ToString().Equals("UserDefined")) continue;
                cb_curve_symbol.Items.Add(type);
            }

            if (mode == DataOperateMode.Insert)
            {
                tb_curve_ymax.Text = 0.ToString(CultureInfo.InvariantCulture);
                tb_curve_ymin.Text = 0.ToString(CultureInfo.InvariantCulture);
            }
            else
            {
                Text = formText;
                cb_curve_name.Text = curve.Name;
                cb_curve_name.Enabled = (mode != DataOperateMode.Delete);

                if (curve.DeviceID == 0)
                {
                    cb_curve_device.Text = "0 服务器";
                }
                else
                {
                    Device device = _deviceManager.GetDeviceInfoById(Convert.ToInt32(curve.DeviceID));
                    cb_curve_device.Text = string.Format("{0} {1}", Convert.ToString(device.Id), Convert.ToString(device.Name));
                }

                //cb_curve_device.Enabled = (mode != DataOperateMode.Delete);
                ntb_curve_address.Text = curve.Address.ToString(CultureInfo.InvariantCulture);
                ntb_curve_address.Enabled = (mode != DataOperateMode.Delete);
                if (curve.LineColor != Color.FromArgb(0))
                {
                    cb_curve_color.SelectedIndex = colorList.IndexOf(curve.LineColor.Name) + 1;
                    //cb_curve_color.Text = curve.LineColour.ToString();
                }
                cb_curve_color.Enabled = (mode != DataOperateMode.Delete);

                cb_curve_type.Text = curve.LineType ? "点线" : "散点";
                cb_curve_type.Enabled = (mode != DataOperateMode.Delete);

                if (!curve.LineWidth.Equals(-1))
                {
                    tb_curve_size.Text = curve.LineWidth.ToString(CultureInfo.InvariantCulture);
                }
                tb_curve_size.Enabled = (mode != DataOperateMode.Delete);

                if (curve.SymbolType != SymbolType.Default)
                {
                    cb_curve_symbol.Text = curve.SymbolType.ToString();
                }
                cb_curve_symbol.Enabled = (mode != DataOperateMode.Delete);

                if (!curve.SymbolSize.Equals(-1))
                {
                    tb_curve_symbolsize.Text = curve.SymbolSize.ToString(CultureInfo.InvariantCulture);
                }
                tb_curve_symbolsize.Enabled = (mode != DataOperateMode.Delete);

                tb_curve_xtitle.Text = curve.XTitle;
                tb_curve_xtitle.Enabled = (mode != DataOperateMode.Delete);
                tb_curve_ytitle.Text = curve.YTitle;
                tb_curve_ytitle.Enabled = (mode != DataOperateMode.Delete);

                tb_curve_ymax.Text = curve.YMax.ToString(CultureInfo.InvariantCulture);
                tb_curve_ymax.Enabled = (mode != DataOperateMode.Delete);
                tb_curve_ymin.Text = curve.YMin.ToString(CultureInfo.InvariantCulture);
                tb_curve_ymin.Enabled = (mode != DataOperateMode.Delete);
            }
        }

        private Curve GetCurrentCurve()
        {
            SymbolType symbol;
            Curve curve = new Curve()
            {
                Id = _curve.Id,
                Name = cb_curve_name.Text.Trim(),
                DeviceID = Convert.ToInt32(cb_curve_device.Text.Split(' ')[0].Trim()),
                Address = Convert.ToUInt16(ntb_curve_address.Text.Trim()),
                LineWidth = tb_curve_size.Text.Trim() != "" ? Convert.ToSingle(tb_curve_size.Text.Trim()) : -1,
                LineColor = cb_curve_color.Text.Trim() != "" ? Color.FromName(cb_curve_color.Text.Trim()) : Color.FromArgb(0),
                LineType = cb_curve_type.Text == "点线",
                SymbolSize = tb_curve_symbolsize.Text != "" ? Convert.ToSingle(tb_curve_symbolsize.Text.Trim()) : -1,
                SymbolType = SymbolType.TryParse(cb_curve_symbol.Text.Trim(), out symbol) ? symbol : SymbolType.UserDefined,
                XTitle = tb_curve_xtitle.Text.Trim(),
                YTitle = tb_curve_ytitle.Text.Trim(),
                YMax = Convert.ToDouble(tb_curve_ymax.Text.Trim()),
                YMin = Convert.ToDouble(tb_curve_ymin.Text.Trim()),
            };
            return curve;
        }

        private void frmEditParameter_Load(object sender, System.EventArgs e)
        {
            switch (_mode)
            {
                case DataOperateMode.Insert:
                    LoadUI(_curve, "添加曲线", _mode);
                    break;
                case DataOperateMode.Edit:
                    LoadUI(_curve, "编辑曲线", _mode);
                    break;
                case DataOperateMode.Delete:
                    LoadUI(_curve, "删除曲线", _mode);
                    break;
                default:
                    break;
            }
        }

        private void btn_ok_Click(object sender, System.EventArgs e)
        {
            try
            {

                if (cb_curve_name.Text.Length < 1)
                {
                    MessageBox.Show("请输入变量名！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (Convert.ToInt32(ntb_curve_address.Text) < 1 || Convert.ToInt32(ntb_curve_address.Text) > 9999)
                {
                    MessageBox.Show("变量地址错误！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                float tempFloat;
                if (!(float.TryParse(tb_curve_size.Text.Trim(), out tempFloat) || tb_curve_size.Text == ""))
                {
                    MessageBox.Show("曲线宽度错误！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (!(float.TryParse(tb_curve_symbolsize.Text.Trim(), out tempFloat) || tb_curve_symbolsize.Text == ""))
                {
                    MessageBox.Show("符号大小错误！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                double tempDouble;
                if (!double.TryParse(tb_curve_ymax.Text.Trim(), out tempDouble))
                {
                    MessageBox.Show("纵轴最大值错误！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (!double.TryParse(tb_curve_ymin.Text.Trim(), out tempDouble))
                {
                    MessageBox.Show("纵轴最小值错误！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (Convert.ToDouble(tb_curve_ymax.Text) < Convert.ToDouble(tb_curve_ymin.Text))
                {
                    MessageBox.Show("最大值应不小于最小值！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                ICurveManager curveManager = _bllFactory.BuildCurveManager();
                switch (_mode)
                {
                    case DataOperateMode.Insert:
                        Result = curveManager.AddCurve(GetCurrentCurve());
                        this.DialogResult = DialogResult.OK;
                        this.Dispose();
                        break;
                    case DataOperateMode.Edit:
                        Result = curveManager.ModifyCurve(GetCurrentCurve());
                        this.DialogResult = DialogResult.OK;
                        this.Dispose();
                        break;
                    case DataOperateMode.Delete:
                        if (
                            MessageBox.Show(
                                string.Format("确认删除曲线？"),
                                "数据删除警告",
                                MessageBoxButtons.OKCancel,
                                MessageBoxIcon.Warning)
                            == DialogResult.OK)
                        {
                            Result = curveManager.DeleteCurveById(_curve.Id);
                            this.DialogResult = DialogResult.OK;
                            this.Dispose();
                        }
                        break;
                }

            }
            catch (Exception ex)
            {
                RecordLog.WriteLogFile("frmCurveEdirtor", ex.Message);
            }
        }

        private void btn_cancel_Click(object sender, System.EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Dispose();
        }

        private void cb_curve_name_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_variableCollection[cb_curve_name.SelectedIndex].DeviceID <= 0)
            {
                cb_curve_device.Text = "0 服务器";
            }
            else
            {
                Device device =
                    _deviceManager.GetDeviceInfoById(
                        Convert.ToInt32(_variableCollection[cb_curve_name.SelectedIndex].DeviceID));
                cb_curve_device.Text = string.Format("{0} {1}", Convert.ToString(device.Id),
                    Convert.ToString(device.Name));
            }

            ntb_curve_address.Text =
                _variableCollection[cb_curve_name.SelectedIndex].Address.ToString(CultureInfo.InvariantCulture);
            tb_curve_xtitle.Text = "时间/(秒)";
            tb_curve_ytitle.Text = cb_curve_name.Text;
        }

        private void cb_curve_name_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0)
            {
                return;
            }
            e.DrawBackground();
            StringFormat strFmt = new System.Drawing.StringFormat {Alignment = StringAlignment.Center};
            e.DrawFocusRectangle();
            e.Graphics.DrawString(cb_curve_name.Items[e.Index].ToString(), e.Font, new SolidBrush(e.ForeColor),
                e.Bounds.X, e.Bounds.Y + 3);
        }

        private void cb_curve_name_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            e.ItemHeight = 16;
        }

        private void cb_curve_color_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index<0)
            {
                return;
            }
            string colorName = cb_curve_color.Items[e.Index].ToString();  //子项的颜色名
            SolidBrush brush = new SolidBrush(Color.FromName(colorName)); //画笔

            Brush brushs = Brushes.Black;
            Rectangle rect = e.Bounds;                                                               //获得需要重绘的区域
            rect.Inflate(-2, -2);                                                                             //缩放一定大小
            Rectangle rectColor = new Rectangle(rect.Location, new Size(20, rect.Height));
            e.Graphics.FillRectangle(brush, rectColor);                                                  // 填充颜色  
            e.Graphics.DrawRectangle(Pens.Black, rectColor);                                       // 绘制边框  
            //绘制文字
            e.Graphics.DrawString(colorName, e.Font, brushs, (rect.X + 22), rect.Y);
        }
    }
}
