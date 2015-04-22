using System;
using System.Collections.Generic;
using System.Data;
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

        public frmConfig()
        {
            InitializeComponent();
            LoadSetting();
        }

        #endregion

        #region 私有函数

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
                ICurveManager curveManager = bllFactory.BuildCurveManager();
                List<Curve> curveCollection = curveManager.GetAllCurveInfo();
                // 如果包含信息
                if (curveCollection.Count > 0)
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
                            case "DeviceID":
                                column.HeaderText = "设备序号";
                                column.DisplayIndex = 2;
                                break;
                            case "Address":
                                column.HeaderText = "变量地址";
                                column.DisplayIndex = 3;
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
            UpdateDevicesGrid();
            string[] tempStrings = System.IO.Ports.SerialPort.GetPortNames();
            cb_portname.Items.AddRange(tempStrings);

            cb_portname.Text = ConfigAppSettings.GetSettingString("ModbusRTUPortName","COM1").Trim();
            cb_baudrate.Text = ConfigAppSettings.GetSettingInt("ModbusRTUBaudrate", 19200).ToString(CultureInfo.InvariantCulture);
            cb_databits.Text = ConfigAppSettings.GetSettingInt("ModbusRTUDatabits", 8).ToString(CultureInfo.InvariantCulture);
            cb_stopbits.Text = ConfigAppSettings.GetSettingInt("ModbusRTUStopbits", 1).ToString(CultureInfo.InvariantCulture);
            nud_device_id.Text = ConfigAppSettings.GetSettingInt("ModbusRTUDeviceID", 1).ToString(CultureInfo.InvariantCulture);

            tb_TimerInterval.Text = ConfigAppSettings.GetSettingInt("RealTime", 2000).ToString(CultureInfo.InvariantCulture);
        }

        private void SaveSetting()
        {

            ConfigAppSettings.SetSettingString("ModbusRTUPortName", cb_portname.Text.Trim());
            ConfigAppSettings.SetSettingInt("ModbusRTUBaudrate", cb_baudrate.Text);
            ConfigAppSettings.SetSettingInt("ModbusRTUDataBits", cb_databits.Text);
            ConfigAppSettings.SetSettingInt("ModbusRTUStopBits", cb_stopbits.Text);
            ConfigAppSettings.SetSettingInt("ModbusRTUDeviceID", nud_device_id.Text);

            if (tb_TimerInterval.Text.Equals("") || tb_TimerInterval.Text.Equals("0") || Convert.ToInt32(tb_TimerInterval.Text) < 500)
            {
                MessageBox.Show("数据更新间隔格式错误！未保存该配置", "参数错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                ConfigAppSettings.SetValue("RealTime", tb_TimerInterval.Text.Trim());
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

        #endregion

        private void btn_Curve_Add_Click(object sender, EventArgs e)
        {
            Curve curve = GetSelectedCurve();
            if (curve.Name == "") return;
            frmCurveEditor addCurveForm = new frmCurveEditor(DataOperateMode.Insert, curve);
            if (addCurveForm.ShowDialog() == DialogResult.OK)
            {
                label_Curve_Status.Text = string.Format("插入 {0} 行数据",
                    addCurveForm.Result.ToString(CultureInfo.InvariantCulture));
                UpdateDevicesGrid();
            }
        }

        private void btn_Curve_Edit_Click(object sender, EventArgs e)
        {
            Curve curve = GetSelectedCurve();
            if (curve.Name == "") return;
            frmCurveEditor editCurveForm = new frmCurveEditor(DataOperateMode.Edit, curve);
            if (editCurveForm.ShowDialog() == DialogResult.OK)
            {
                label_Curve_Status.Text = string.Format("编辑 {0} 行数据",
                    editCurveForm.Result.ToString(CultureInfo.InvariantCulture));
                UpdateDevicesGrid();
            }
        }

        private void btn_Curve_Delete_Click(object sender, EventArgs e)
        {
            Curve curve = GetSelectedCurve();
            if (curve.Name == "") return;
            frmCurveEditor deleteCurveForm = new frmCurveEditor(DataOperateMode.Delete, curve);
            if (deleteCurveForm.ShowDialog() == DialogResult.OK)
            {
                label_Curve_Status.Text = string.Format("删除 {0} 行数据",
                    deleteCurveForm.Result.ToString(CultureInfo.InvariantCulture));
                UpdateDevicesGrid();
            }
        }

        private void btn_Curve_Update_Click(object sender, EventArgs e)
        {
            UpdateDevicesGrid();
        }

        private void dataGridView_Curve_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btn_Curve_Edit_Click(sender, e);
        }
    }
}