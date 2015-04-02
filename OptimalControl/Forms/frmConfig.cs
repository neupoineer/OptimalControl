using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using OptimalControl.Common;
using ZedGraph;

namespace OptimalControl.Forms
{
    public partial class frmConfig : Form
    {
        DataTable DeviceDataTable = new DataTable();
        DataTable ParameterDataTable = new DataTable();

        private string SQLGetDevices = ConfigAppSettings.GetSettingString("SQLGetDevices", "SELECT * FROM @DevicesTable");
        private string DevicesTable = ConfigAppSettings.GetSettingString("DevicesTable", "Devices");

        private string SQLGetParameters = ConfigAppSettings.GetSettingString("SQLGetParameters",
            "SELECT * FROM @ParametersTable WHERE DeviceID = @DeviceID");
        string ParametersTable = ConfigAppSettings.GetSettingString("ParametersTable", "Parameters");

        private string SQLGetCurves = ConfigAppSettings.GetSettingString("SQLGetCurves", "SELECT * FROM @CurvesTable");
        private string CurvesTable = ConfigAppSettings.GetSettingString("CurvesTable", "Curves");


        #region 构造函数

        public frmConfig()
        {
            InitializeComponent();
            LoadSetting();
        }

        #endregion

        #region 私有函数

        private string GetDevicesCommand(string sqlCmd, string table)
        {
            string sql = sqlCmd;
            sql = sql.Replace("@DevicesTable", table);
            return sql;
        }

        private string GetParametersCommand(string sqlCmd, string table)
        {
            string sql = sqlCmd;
            sql = sql.Replace("@ParametersTable", table);
            sql = sql.Replace("@DeviceID", "1");
            sql = sql.Replace("DeviceID", "1");
            return sql;
        }

        private string GetCurvesCommand(string sqlCmd, string table)
        {
            string sql = sqlCmd;
            sql = sql.Replace("@CurvesTable", table);
            return sql;
        }

        private void UpdateCurves()
        {
            DeviceDataTable = SQLHelper.ExcuteDataTable(SQLHelper.ConnectionStringLocalTransaction,
                GetDevicesCommand(SQLGetDevices, DevicesTable));
            ParameterDataTable = SQLHelper.ExcuteDataTable(SQLHelper.ConnectionStringLocalTransaction,
                GetParametersCommand(SQLGetParameters, ParametersTable));
            DataTable curvesDataTable = SQLHelper.ExcuteDataTable(SQLHelper.ConnectionStringLocalTransaction,
                GetCurvesCommand(SQLGetCurves, CurvesTable));
            UpdateDevicesGrid(curvesDataTable, "1=1");
            label_Curve_Status.Text = string.Format("查询到 {0} 行数据", curvesDataTable.Rows.Count);
        }

        private void UpdateDevicesGrid(DataTable dataTable, string filter)
        {
            DataRow[] data = dataTable.Select(filter);
            DataTable table = dataTable.Clone();
            foreach (DataRow row in data)
            {
                table.Rows.Add(row.ItemArray);
            }

            dataGridView_Curve.DataSource = table;

            foreach (DataGridViewColumn column in dataGridView_Curve.Columns)
            {
                switch (column.HeaderText) //更改列名
                {
                    case "Id":
                        column.HeaderText = "序号";
                        break;
                    case "Name":
                        column.HeaderText = "名称";
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
        }

        private Curve GetSelectedCurve()
        {
            if (dataGridView_Curve.CurrentRow != null)
            {
                int selectRowIndex = dataGridView_Curve.CurrentRow.Index;
                Curve curve = new Curve
                {
                    Id = Convert.ToInt32(dataGridView_Curve.Rows[selectRowIndex].Cells[0].Value),
                    Name = Convert.ToString(dataGridView_Curve.Rows[selectRowIndex].Cells[1].Value),
                    DeviceID = Convert.ToInt32(dataGridView_Curve.Rows[selectRowIndex].Cells[2].Value),
                    Address = Convert.ToUInt16(dataGridView_Curve.Rows[selectRowIndex].Cells[3].Value),
                    LineColour = Convert.ToString(dataGridView_Curve.Rows[selectRowIndex].Cells[4].Value) != ""
                        ? Color.FromName(Convert.ToString(dataGridView_Curve.Rows[selectRowIndex].Cells[4].Value))
                        : new Color(),
                    LineType = Convert.ToString(dataGridView_Curve.Rows[selectRowIndex].Cells[5].Value) == "" ||
                               Convert.ToBoolean(dataGridView_Curve.Rows[selectRowIndex].Cells[5].Value),
                    LineWidth = Convert.ToString(dataGridView_Curve.Rows[selectRowIndex].Cells[6].Value) != ""
                        ? Convert.ToSingle(dataGridView_Curve.Rows[selectRowIndex].Cells[6].Value)
                        : -1,
                    SymbolSize = Convert.ToString(dataGridView_Curve.Rows[selectRowIndex].Cells[8].Value) != ""
                        ? Convert.ToSingle(dataGridView_Curve.Rows[selectRowIndex].Cells[8].Value)
                        : -1,
                    XAxisTitle = Convert.ToString(dataGridView_Curve.Rows[selectRowIndex].Cells[9].Value),
                    YAxisTitle = Convert.ToString(dataGridView_Curve.Rows[selectRowIndex].Cells[10].Value),
                    YAxisMax = Convert.ToDouble(dataGridView_Curve.Rows[selectRowIndex].Cells[11].Value),
                    YAxisMin = Convert.ToDouble(dataGridView_Curve.Rows[selectRowIndex].Cells[12].Value)
                };

                if (Convert.ToString(dataGridView_Curve.Rows[selectRowIndex].Cells[7].Value) != "")
                {
                    switch (Convert.ToString(dataGridView_Curve.Rows[selectRowIndex].Cells[7].Value))
                    {
                        case "Diamond":
                            curve.CurveSymbolType = SymbolType.Diamond;
                            break;
                        case "Circle":
                            curve.CurveSymbolType = SymbolType.Circle;
                            break;
                        case "Square":
                            curve.CurveSymbolType = SymbolType.Square;
                            break;
                        case "Star":
                            curve.CurveSymbolType = SymbolType.Star;
                            break;
                        case "Triangle":
                            curve.CurveSymbolType = SymbolType.Triangle;
                            break;
                        case "Plus":
                            curve.CurveSymbolType = SymbolType.Plus;
                            break;
                        case "None":
                            curve.CurveSymbolType = SymbolType.None;
                            break;
                    }
                }
                else
                {
                    curve.CurveSymbolType = SymbolType.Default;
                }
                return curve;
            }
            return new Curve();
        }


        private void LoadSetting()
        {
            UpdateCurves();
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
            frmEditCurve addCurveForm = new frmEditCurve(DataOperateMode.Insert, curve, DeviceDataTable,
                ParameterDataTable);
            if (addCurveForm.ShowDialog() == DialogResult.OK)
            {
                label_Curve_Status.Text = string.Format("插入 {0} 行数据",
                    addCurveForm.Result.ToString(CultureInfo.InvariantCulture));
                UpdateCurves();
            }
        }

        private void btn_Curve_Edit_Click(object sender, EventArgs e)
        {
            Curve curve = GetSelectedCurve();
            if (curve.Name == "") return;
            frmEditCurve addCurveForm = new frmEditCurve(DataOperateMode.Edit, curve, DeviceDataTable,
                ParameterDataTable);
            if (addCurveForm.ShowDialog() == DialogResult.OK)
            {
                label_Curve_Status.Text = string.Format("编辑 {0} 行数据",
                    addCurveForm.Result.ToString(CultureInfo.InvariantCulture));
                UpdateCurves();
            }
        }

        private void btn_Curve_Delete_Click(object sender, EventArgs e)
        {
            Curve curve = GetSelectedCurve();
            if (curve.Name == "") return;
            frmEditCurve addCurveForm = new frmEditCurve(DataOperateMode.Delete, curve, DeviceDataTable,
                ParameterDataTable);
            if (addCurveForm.ShowDialog() == DialogResult.OK)
            {
                label_Curve_Status.Text = string.Format("删除 {0} 行数据",
                    addCurveForm.Result.ToString(CultureInfo.InvariantCulture));
                UpdateCurves();
            }
        }

        private void btn_Curve_Update_Click(object sender, EventArgs e)
        {
            UpdateCurves();
        }

        private void dataGridView_Curve_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btn_Curve_Edit_Click(sender, e);
        }
    }
}