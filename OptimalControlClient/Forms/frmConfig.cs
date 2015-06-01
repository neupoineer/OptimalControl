using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using IBLL.Control;
using Utility;
using Model.Control;
using ZedGraph;

namespace OptimalControl.Forms
{
    public partial class frmConfig : Form
    {
        #region 构造函数
        private BLLFactory.BLLFactory _bllFactory = new BLLFactory.BLLFactory();
        private IVariableManager _variableManager;
        private List<Variable> _variableCollection;
        private List<string> _variableCodes = new List<string>();

        public frmConfig()
        {
            InitializeComponent();
            LoadSetting();
        }

        #endregion

        #region 私有函数

        private delegate void UpdateCurveGridDelegate();

        private void UpdateCurveGrid()
        {
            if (InvokeRequired)
            {
                Invoke(new UpdateCurveGridDelegate(UpdateCurveGrid));
                return;
            }
            try
            {
                ICurveManager curveManager = _bllFactory.BuildCurveManager();
                List<Curve> curveCollection = curveManager.GetAllCurveInfo();
                // 如果包含信息
                if (curveCollection.Count >= 0)
                {
                    BindingSource source = new BindingSource {DataSource = curveCollection};
                    dataGridView_Curve.DataSource = source;
                    foreach (DataGridViewColumn column in dataGridView_Curve.Columns)
                    {
                        switch (column.HeaderText) //更改列名
                        {
                            case "Id":
                                column.HeaderText = "序号";
                                column.DisplayIndex = 0;
                                break;
                            case "Name":
                                column.HeaderText = "名称";
                                column.DisplayIndex = 1;
                                break;
                            case "VariableCode":
                                column.HeaderText = "变量编码";
                                break;
                            case "DeviceID":
                                column.HeaderText = "设备序号";
                                break;
                            case "Address":
                                column.HeaderText = "变量地址";
                                break;
                            case "LineColor":
                                column.HeaderText = "颜色";
                                break;
                            case "LineType":
                                column.HeaderText = "线型";
                                break;
                            case "LineWidth":
                                column.HeaderText = "线宽";
                                break;
                            case "SymbolType":
                                column.HeaderText = "符号类型";
                                break;
                            case "SymbolSize":
                                column.HeaderText = "符号大小";
                                break;
                            case "XTitle":
                                column.HeaderText = "横轴名";
                                break;
                            case "YTitle":
                                column.HeaderText = "纵轴名";
                                break;
                            case "YMax":
                                column.HeaderText = "纵轴最大值";
                                break;
                            case "YMin":
                                column.HeaderText = "纵轴最小值";
                                break;
                            default:
                                break;
                        }
                    }
                    label_Curve_Status.Text = string.Format("查询到 {0} 行数据", curveCollection.Count);
                }
            }
            catch (Exception ex)
            {
                RecordLog.WriteLogFile("UpdateDevicesGrid", ex.Message);
            }
        }

        private Curve GetSelectedCurve()
        {
            Curve curve = new Curve();
            if (dataGridView_Curve.CurrentRow != null)
            {
                int selectRowIndex = dataGridView_Curve.CurrentRow.Index;
                curve = new Curve
                {
                    Id = Convert.ToInt32(dataGridView_Curve.Rows[selectRowIndex].Cells["Id"].Value),
                    VariableCode = Convert.ToString(dataGridView_Curve.Rows[selectRowIndex].Cells["VariableCode"].Value),
                    Name = Convert.ToString(dataGridView_Curve.Rows[selectRowIndex].Cells["Name"].Value),
                    DeviceID = Convert.ToInt32(dataGridView_Curve.Rows[selectRowIndex].Cells["DeviceID"].Value),
                    Address = Convert.ToUInt16(dataGridView_Curve.Rows[selectRowIndex].Cells["Address"].Value),
                    LineColor = Convert.ToString(dataGridView_Curve.Rows[selectRowIndex].Cells["LineColor"].Value) != ""
                        ? Color.FromName(Convert.ToString(dataGridView_Curve.Rows[selectRowIndex].Cells["LineColor"].Value))
                        : new Color(),
                    LineType = Convert.ToString(dataGridView_Curve.Rows[selectRowIndex].Cells["LineType"].Value) == "" ||
                               Convert.ToBoolean(dataGridView_Curve.Rows[selectRowIndex].Cells["LineType"].Value),
                    LineWidth = Convert.ToString(dataGridView_Curve.Rows[selectRowIndex].Cells["LineWidth"].Value) != ""
                        ? Convert.ToSingle(dataGridView_Curve.Rows[selectRowIndex].Cells["LineWidth"].Value)
                        : -1,
                    SymbolSize = Convert.ToString(dataGridView_Curve.Rows[selectRowIndex].Cells["SymbolSize"].Value) != ""
                        ? Convert.ToSingle(dataGridView_Curve.Rows[selectRowIndex].Cells["SymbolSize"].Value)
                        : -1,
                    XTitle = Convert.ToString(dataGridView_Curve.Rows[selectRowIndex].Cells["XTitle"].Value),
                    YTitle = Convert.ToString(dataGridView_Curve.Rows[selectRowIndex].Cells["YTitle"].Value),
                    YMax = Convert.ToDouble(dataGridView_Curve.Rows[selectRowIndex].Cells["YMax"].Value),
                    YMin = Convert.ToDouble(dataGridView_Curve.Rows[selectRowIndex].Cells["YMin"].Value)
                };

                if (Convert.ToString(dataGridView_Curve.Rows[selectRowIndex].Cells["SymbolType"].Value) != "")
                {
                    switch (Convert.ToString(dataGridView_Curve.Rows[selectRowIndex].Cells["SymbolType"].Value))
                    {
                        case "Diamond":
                            curve.SymbolType = SymbolType.Diamond;
                            break;
                        case "Circle":
                            curve.SymbolType = SymbolType.Circle;
                            break;
                        case "Square":
                            curve.SymbolType = SymbolType.Square;
                            break;
                        case "Star":
                            curve.SymbolType = SymbolType.Star;
                            break;
                        case "Triangle":
                            curve.SymbolType = SymbolType.Triangle;
                            break;
                        case "Plus":
                            curve.SymbolType = SymbolType.Plus;
                            break;
                        case "None":
                            curve.SymbolType = SymbolType.None;
                            break;
                    }
                }
                else
                {
                    curve.SymbolType = SymbolType.Default;
                }
            }
            return curve;
        }

        private void LoadSetting()
        {
            _variableManager = _bllFactory.BuildIVariableManager();
            _variableCollection = _variableManager.GetAllVariableInfo();

            foreach (Variable variable in _variableCollection)
            {
                cb_control.Items.Add(variable.Name);
                cb_heartbeat.Items.Add(variable.Name);
                cb_feed.Items.Add(variable.Name);
                cb_oc_feed.Items.Add(variable.Name);
                cb_feedWater.Items.Add(variable.Name);
                cb_oc_feedWater.Items.Add(variable.Name);
                cb_supWater.Items.Add(variable.Name);
                cb_oc_supWater.Items.Add(variable.Name);
                _variableCodes.Add(variable.Code);
            }

            string[] tempStrings = System.IO.Ports.SerialPort.GetPortNames();
            cb_portname.Items.AddRange(tempStrings);

            cb_portname.Text = ConfigExeSettings.GetSettingString("ModbusRTUPortName","COM1").Trim();
            cb_baudrate.Text = ConfigExeSettings.GetSettingInt("ModbusRTUBaudrate", 19200).ToString(CultureInfo.InvariantCulture);
            cb_databits.Text = ConfigExeSettings.GetSettingInt("ModbusRTUDatabits", 8).ToString(CultureInfo.InvariantCulture);
            cb_stopbits.Text = ConfigExeSettings.GetSettingInt("ModbusRTUStopbits", 1).ToString(CultureInfo.InvariantCulture);
            nud_device_id.Text = ConfigExeSettings.GetSettingInt("ModbusRTUDeviceID", 1).ToString(CultureInfo.InvariantCulture);

            string tmpCode = ConfigExeSettings.GetSettingString("OptimalControlEnabledVariable", "").Trim();
            int tmpindex = _variableCodes.IndexOf(tmpCode);
            cb_control.Text = tmpindex == -1 ? "" : _variableCollection[tmpindex].Name;

            tmpCode = ConfigExeSettings.GetSettingString("OptimalControlHeartBeatVariable", "").Trim();
            tmpindex = _variableCodes.IndexOf(tmpCode);
            cb_heartbeat.Text = tmpindex == -1 ? "" : _variableCollection[tmpindex].Name;

            tmpCode = ConfigExeSettings.GetSettingString("FeedVariable", "").Trim();
            tmpindex = _variableCodes.IndexOf(tmpCode);
            cb_feed.Text = tmpindex == -1 ? "" : _variableCollection[tmpindex].Name;

            tmpCode = ConfigExeSettings.GetSettingString("OptimalControlFeedVariable", "").Trim();
            tmpindex = _variableCodes.IndexOf(tmpCode);
            cb_oc_feed.Text = tmpindex == -1 ? "" : _variableCollection[tmpindex].Name;

            tmpCode = ConfigExeSettings.GetSettingString("FeedWaterVariable", "").Trim();
            tmpindex = _variableCodes.IndexOf(tmpCode);
            cb_feedWater.Text = tmpindex == -1 ? "" : _variableCollection[tmpindex].Name;

            tmpCode = ConfigExeSettings.GetSettingString("OptimalControlFeedWaterVariable", "").Trim();
            tmpindex = _variableCodes.IndexOf(tmpCode);
            cb_oc_feedWater.Text = tmpindex == -1 ? "" : _variableCollection[tmpindex].Name;

            tmpCode = ConfigExeSettings.GetSettingString("SupWaterVariable", "").Trim();
            tmpindex = _variableCodes.IndexOf(tmpCode);
            cb_supWater.Text = tmpindex == -1 ? "" : _variableCollection[tmpindex].Name;

            tmpCode = ConfigExeSettings.GetSettingString("OptimalControlSupWaterVariable", "").Trim();
            tmpindex = _variableCodes.IndexOf(tmpCode);
            cb_oc_supWater.Text = tmpindex == -1 ? "" : _variableCollection[tmpindex].Name;

            tb_UpdateVariableTime.Text = ConfigExeSettings.GetSettingInt("UpdateVariableTime", 10000).ToString(CultureInfo.InvariantCulture);
            tb_Realtime.Text = ConfigExeSettings.GetSettingInt("RealTime", 2000).ToString(CultureInfo.InvariantCulture);

            UpdateCurveGrid();
        }

        private void SaveSetting()
        {
            ConfigExeSettings.SetSettingString("ModbusRTUPortName", cb_portname.Text.Trim());
            ConfigExeSettings.SetSettingInt("ModbusRTUBaudrate", cb_baudrate.Text);
            ConfigExeSettings.SetSettingInt("ModbusRTUDataBits", cb_databits.Text);
            ConfigExeSettings.SetSettingInt("ModbusRTUStopBits", cb_stopbits.Text);
            ConfigExeSettings.SetSettingInt("ModbusRTUDeviceID", nud_device_id.Text);

            ConfigExeSettings.SetSettingString("OptimalControlEnabledVariable",
                cb_control.SelectedIndex == -1 ? "" : _variableCodes[cb_control.SelectedIndex]);
            ConfigExeSettings.SetSettingString("OptimalControlHeartBeatVariable",
                cb_heartbeat.SelectedIndex == -1 ? "" : _variableCodes[cb_heartbeat.SelectedIndex]);

            ConfigExeSettings.SetSettingString("FeedVariable",
                cb_feed.SelectedIndex == -1 ? "" : _variableCodes[cb_feed.SelectedIndex]);
            ConfigExeSettings.SetSettingString("OptimalControlFeedVariable",
                cb_oc_feed.SelectedIndex == -1 ? "" : _variableCodes[cb_oc_feed.SelectedIndex]);
            ConfigExeSettings.SetSettingString("FeedWaterVariable",
                cb_feedWater.SelectedIndex == -1 ? "" : _variableCodes[cb_feedWater.SelectedIndex]);
            ConfigExeSettings.SetSettingString("OptimalControlFeedWaterVariable",
                cb_oc_feedWater.SelectedIndex == -1 ? "" : _variableCodes[cb_oc_feedWater.SelectedIndex]);
            ConfigExeSettings.SetSettingString("SupWaterVariable",
                cb_supWater.SelectedIndex == -1 ? "" : _variableCodes[cb_supWater.SelectedIndex]);
            ConfigExeSettings.SetSettingString("OptimalControlSupWaterVariable",
                cb_oc_supWater.SelectedIndex == -1 ? "" : _variableCodes[cb_oc_supWater.SelectedIndex]);

            if (tb_UpdateVariableTime.Text.Equals("") || tb_UpdateVariableTime.Text.Equals("0") || Convert.ToInt32(tb_UpdateVariableTime.Text) < 500)
            {
                MessageBox.Show("数据更新间隔格式错误！未保存该配置", "参数错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                ConfigExeSettings.SetValue("RealTime", tb_UpdateVariableTime.Text.Trim());
            }
            if (tb_Realtime.Text.Equals("") || tb_Realtime.Text.Equals("0") || Convert.ToInt32(tb_Realtime.Text) < 500)
            {
                MessageBox.Show("规则扫描间隔格式错误！未保存该配置", "参数错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                ConfigExeSettings.SetValue("RealTime", tb_Realtime.Text.Trim());
            }
        }

        #endregion

        #region 控件响应

        private void btn_OK_Click(object sender, EventArgs e)
        {
            SaveSetting();
            this.DialogResult = DialogResult.OK;
            this.Dispose();
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btn_Curve_Add_Click(object sender, EventArgs e)
        {
            Curve curve = GetSelectedCurve();
            if (curve.Name == "") return;
            frmCurveEditor addCurveForm = new frmCurveEditor(DataOperateMode.Insert, curve);
            if (addCurveForm.ShowDialog() == DialogResult.OK)
            {
                label_Curve_Status.Text = string.Format("插入 {0} 行数据",
                    addCurveForm.Result.ToString(CultureInfo.InvariantCulture));
                UpdateCurveGrid();
            }
        }

        private void btn_Curve_Edit_Click(object sender, EventArgs e)
        {
            Curve curve = GetSelectedCurve();
            if (curve.Name == null) return;
            frmCurveEditor editCurveForm = new frmCurveEditor(DataOperateMode.Edit, curve);
            if (editCurveForm.ShowDialog() == DialogResult.OK)
            {
                label_Curve_Status.Text = string.Format("编辑 {0} 行数据",
                    editCurveForm.Result.ToString(CultureInfo.InvariantCulture));
                UpdateCurveGrid();
            }
        }

        private void btn_Curve_Delete_Click(object sender, EventArgs e)
        {
            Curve curve = GetSelectedCurve();
            if (curve.Name == null) return;
            frmCurveEditor deleteCurveForm = new frmCurveEditor(DataOperateMode.Delete, curve);
            if (deleteCurveForm.ShowDialog() == DialogResult.OK)
            {
                label_Curve_Status.Text = string.Format("删除 {0} 行数据",
                    deleteCurveForm.Result.ToString(CultureInfo.InvariantCulture));
                UpdateCurveGrid();
            }
        }

        private void btn_Curve_Update_Click(object sender, EventArgs e)
        {
            UpdateCurveGrid();
        }

        private void dataGridView_Curve_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btn_Curve_Edit_Click(sender, e);
        }

        #endregion
    }
}