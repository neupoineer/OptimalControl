// ***********************************************************************
// Assembly         : frmMain.cs
// Author           : Jeffrey
// Created          : 05-24-2014
//
// Last Modified By : Jeffrey
// Last Modified On : 06-21-2014
// ***********************************************************************
// <copyright file="Form1.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.IO.Ports;
using System.Net.Sockets;
using System.Timers;
using Modbus.Device;
using Modbus.Data;
using Model;
using ZedGraph;
using OptimalControl.Common;
using Model.Rights;

namespace OptimalControl.Forms
{

    #region 结构体

    /// <summary>
    /// Struct Parameter
    /// </summary>
    public struct Parameter
    {
        /// <summary>
        /// The identifier
        /// </summary>
        public int Id;

        /// <summary>
        /// The Parameter Name string
        /// </summary>
        public string Name;

        /// <summary>
        /// The Parameter address
        /// </summary>
        public ushort Address;

        /// <summary>
        /// The Parameter ratio
        /// </summary>
        public double Ratio;

        /// <summary>
        /// The parameter value
        /// </summary>
        public double Value;

        /// <summary>
        /// The device identifier
        /// </summary>
        public int DeviceID;
    }

    /// <summary>
    /// Modbus RTU Device
    /// </summary>
    public struct ModbusRTUDevice
    {
        /// <summary>
        /// The serial port object
        /// </summary>
        public SerialPort SerialPortObject;

        /// <summary>
        /// The device identifier
        /// </summary>
        public byte UnitID;
    }

    /// <summary>
    /// Modbus TCP Device
    /// </summary>
    public struct ModbusTCPDevice
    {
        /// <summary>
        /// The modbus IP address
        /// </summary>
        public string IPAddress;

        /// <summary>
        /// The modbus port
        /// </summary>
        public int PortName;

        /// <summary>
        /// The modbus device ID
        /// </summary>
        public byte UnitID;

        /// <summary>
        /// The TCP client
        /// </summary>
        public TcpClient TCPClient;

        /// <summary>
        /// The modbus IP master
        /// </summary>
        public ModbusIpMaster ModbusTCPMaster;
    }

    /// <summary>
    /// Struct Device
    /// </summary>
    public struct Device
    {
        /// <summary>
        /// The identifier
        /// </summary>
        public int Id;

        /// <summary>
        /// The device name
        /// </summary>
        public string Name;

        /// <summary>
        /// The Device enabled
        /// </summary>
        public bool State;

        /// <summary>
        /// The DeviceEnabledn enabled
        /// </summary>
        public bool SyncState;

        /// <summary>
        /// The modbus IP master
        /// </summary>
        public ModbusTCPDevice ModbusTcpDevice;

        /// <summary>
        /// The modbus TCP master created
        /// </summary>
        public bool ModbusTCPMasterCreated;

        /// <summary>
        /// The modbus TCP master updated
        /// </summary>
        public bool ModbusTCPMasterUpdated;

        /// <summary>
        /// The Parameters
        /// </summary>
        public Parameter[] Parameters;
    }

    /// <summary>
    /// The Curve property
    /// </summary>
    public struct Curve
    {
        /// <summary>
        /// The identifier
        /// </summary>
        public int Id;

        /// <summary>
        /// The name of the curve
        /// </summary>
        public string Name;

        /// <summary>
        /// The curve list
        /// </summary>
        public PointPairList DataList;

        /// <summary>
        /// The device identifier
        /// </summary>
        public int DeviceID;

        /// <summary>
        /// The address
        /// </summary>
        public ushort Address;

        /// <summary>
        /// The curve line colour
        /// </summary>
        public Color LineColour;

        /// <summary>
        /// The curve line type
        /// </summary>
        public bool LineType;

        /// <summary>
        /// The curve line width
        /// </summary>
        public float LineWidth;

        /// <summary>
        /// The curve symbol type
        /// </summary>
        public SymbolType CurveSymbolType;

        /// <summary>
        /// The curve symbol size
        /// </summary>
        public float SymbolSize;

        /// <summary>
        /// The x axis title
        /// </summary>
        public string XAxisTitle;

        /// <summary>
        /// The y axis title
        /// </summary>
        public string YAxisTitle;

        /// <summary>
        /// The pane Y axis max
        /// </summary>
        public double YAxisMax;

        /// <summary>
        /// The pane Y axis min
        /// </summary>
        public double YAxisMin;
    }

    public enum GraphPaneType
    {
        Normal = 0,
        First = 1,
        Last = 2,
    }

    #endregion

    /// <summary>
    /// Class MainForm
    /// </summary>
    public partial class frmMain : Form
    {
        #region 全局变量

        private string[] _args;

        /// <summary>
        /// The _isPass
        /// </summary>
        private bool _isPass = false;

        /// <summary>
        /// 当前登录操作员实体
        /// </summary>
        private Operator _currentOperator = null;

        /// <summary>
        /// 创建工厂类
        /// </summary>
        private BLLFactory.BLLFactory _bllFactory = null;

        /// <summary>
        /// The software is running
        /// </summary>
        private bool _isRunning = false;

        /// <summary>
        /// The log file
        /// </summary>
        private string LogFile = AppDomain.CurrentDomain.BaseDirectory + "cache\\event.log"; //错误日志文件

        /// <summary>
        /// The logoff time
        /// </summary>
        private int LogoffTime = 900; //注销时间，默认900s

        /// <summary>
        /// The Logoff timer
        /// </summary>
        private System.Timers.Timer TimerLogoff = new System.Timers.Timer(900000);

        /// <summary>
        /// The real timer
        /// </summary>
        private System.Timers.Timer TimerRealtime = new System.Timers.Timer(2000);

        /// <summary>
        /// The real timer flag
        /// </summary>
        private bool RealTimerFlag = false;

        private bool UpdateGraphFlag = false;
        
        /// <summary>
        /// The MessageFilter
        /// </summary>
        private MessageFilter MyFilter = new MessageFilter();

        /// <summary>
        /// The logoff menu list
        /// </summary>
        private string[] LogoffMenuList =
        {
            "menu_file_quit", "menu_file_lockscreen", "menu_control_run", "menu_control_stop", "menu_config_config",
            "menu_config_password", "menu_config_user", "menu_config_devices", "menu_config_parameters",
            "menu_control_history", "menu_control_clear"
        };

        /// <summary>
        /// The real timer interval
        /// </summary>
        private int RealTimerInterval = 2000; //设置定时器间隔，默认为2000ms

        /// <summary>
        /// The modbus rtu slave thread
        /// </summary>
        private Thread ModbusRTUSlaveThread;

        /// <summary>
        /// The modbus rtu slave
        /// </summary>
        private ModbusSlave ModbusRTUSlave;

        /// <summary>
        /// The modbus rtu device
        /// </summary>
        private ModbusRTUDevice ModbusRtuDevice = new ModbusRTUDevice();

        /// <summary>
        /// The modbus rtu parameters
        /// </summary>
        private Parameter[] ModbusRTUParameters = new Parameter[0];

        /// <summary>
        /// The modbus rtu slave created flag
        /// </summary>
        private bool ModbusRTUSlaveCreated;

        /// <summary>
        /// The modbus rtu slave updated
        /// </summary>
        private bool ModbusRTUSlaveUpdated;

        /// <summary>
        /// The devices
        /// </summary>
        private Device[] Devices = new Device[0];

        /// <summary>
        /// The SQL to save parameter
        /// </summary>
        private string[] SQLSaveData =
        {
            "INSERT INTO @DataTable (Time, ParameterName, Value, DeviceID) VALUES ",
            ",",
            "('@Time', '@ParameterName', '@Value', '@DeviceID')"
        };

        /// <summary>
        /// The SQL get history data
        /// </summary>
        private string SQLGetHistoryData =
            "SELECT * FROM @DataTable WHERE ParameterName='@ParameterName' AND DeviceID='@DeviceID' AND Time >= '@StartTime' AND Time <= '@EndTime'";

        private string SQLGetHistoryData1 =
            "DECLARE @sql1 varchar(8000); SELECT @sql1 = ISNULL(@sql1 + '],[' , '') + [Name] FROM [@CurvesTable] GROUP BY [Name]; SET @sql1 = '[' + @sql1 + ']';";

        private string SQLGetHistoryData2 =
            "DECLARE @sql2 varchar(8000); SELECT @sql2 = ISNULL(@sql2 + ''',MAX([' , '') + [Name] +']) AS ''' + [Name]  FROM [@CurvesTable] GROUP BY [Name]; SET @sql2 = 'MAX([' + @sql2 + '''';";

        private string SQLGetHistoryData3 =
            "EXEC ('SELECT [Time] AS ''时间'',' + @sql2 + ' 	FROM (SELECT * FROM [@DataTable] WHERE [Time] &gt;= ''@StartTime'' AND [Time] &lt; ''@EndTime'') AS a PIVOT (MAX([Value]) FOR [ParameterName] IN (' + @sql1 + ')) b GROUP BY [Time] ORDER BY [Time]');";

        /// <summary>
        /// The data table name
        /// </summary>
        private string DataTable = "Data";

        private string CurvesTable = "Curves";

        /// <summary>
        /// The DCS name displayed in list
        /// </summary>
        private string DCSName = "磨机工况信息";

        /// <summary>
        /// The master pane graph
        /// </summary>
        private MasterPane MasterPaneGraphRealtime = new MasterPane();

        private MasterPane MasterPaneGraphHistory = new MasterPane();

        /// <summary>
        /// The rotator
        /// </summary>
        private ColorSymbolRotator Rotator = new ColorSymbolRotator();

        /// <summary>
        /// The curve count
        /// </summary>
        private int CurveCount = 6;

        /// <summary>
        /// The curve list
        /// </summary>
        private int[] CurveList = {3, 3};

        /// <summary>
        /// The proportion
        /// </summary>
        private float[] Proportion = {2, 1};

        /// <summary>
        /// The curves
        /// </summary>
        private Curve[] Curves = new Curve[6];

        private Curve[] HisoryCurves;

        /// <summary>
        /// The data list length
        /// </summary>
        private int DataListLength = 720;

        #endregion

        #region 构造函数

        /// <summary>
        /// Initializes a new instance of the <see cref="frmMain"/> class.
        /// </summary>
        public frmMain(string[] args, bool isPass, Operator currentOperator)
        {
            this._args = args;
            // 保存当前登录操作员实体
            this._currentOperator = currentOperator;
            this._isPass = isPass;

            InitializeComponent();

            splitContainerH1.SplitterDistance = 5;
            splitContainerH1_2H2.SplitterDistance = splitContainerH1_2H2.Height - 6;
            try
            {
                if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "cache")) //检查cache目录是否已创建
                    Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "cache"); //若尚未创建，则创建目录

                ofd_history.InitialDirectory = (AppDomain.CurrentDomain.BaseDirectory + "cache");

                LogFile = ConfigAppSettings.GetSettingString("LogFile", LogFile);

                //label_info1_info.Text = "";
                //label_info1_load.Text = "";
                //label_info1_voltage.Text = "";
                status_Label.Text = "";

                MasterPaneGraphRealtime = zgc_realtime.MasterPane;
                zgc_realtime.ContextMenuBuilder += ZgcContextMenuBuilder;

                MasterPaneGraphHistory = zgc_history.MasterPane;
                zgc_history.ContextMenuBuilder += ZgcContextMenuBuilder;

                LoadSettings();

                // Remove the default pane that comes with the ZedGraphControl.MasterPane
                MasterPaneGraphRealtime.PaneList.Clear();
                //MasterPaneGraphRealtime.Fill = new Fill(Color.White, Color.MediumSlateBlue, 45.0F);
                MasterPaneGraphRealtime.Title.IsVisible = false;
                MasterPaneGraphRealtime.Margin.All = 10;
                MasterPaneGraphRealtime.InnerPaneGap = 0;
                //MasterPaneGraphRealtime.Legend.IsVisible = true;
                //MasterPaneGraphRealtime.Legend.Position = LegendPos.TopCenter;
                UpdateGraph(ref MasterPaneGraphRealtime, ref zgc_realtime, Curves);

                MasterPaneGraphHistory.PaneList.Clear();
                //MasterPaneGraphHistory.Fill = new Fill(Color.White, Color.MediumSlateBlue, 45.0F);
                MasterPaneGraphHistory.Title.IsVisible = false;
                MasterPaneGraphHistory.Margin.All = 10;
                MasterPaneGraphHistory.InnerPaneGap = 0;
                //MasterPaneGraphHistory.Legend.IsVisible = true;
                //MasterPaneGraphHistory.Legend.Position = LegendPos.TopCenter;
                UpdateGraph(ref MasterPaneGraphHistory, ref zgc_realtime, Curves);

                dtp_curve_start.Value = DateTime.Today;
                dtp_curve_end.Value = DateTime.Today.AddDays(1);
                dtp_data_start.Value = DateTime.Today;
                dtp_data_end.Value = DateTime.Today.AddDays(1);

                if (_isPass)
                {
                    //创建工厂类实例
                    _bllFactory = new BLLFactory.BLLFactory();
                    WriteLogFile(LogFile, "Login", string.Format("{0} Login!", _currentOperator.ModelName));

                    // 加载权限菜单
                    RightsMenuDataManager rmManager = new RightsMenuDataManager();
                    rmManager.LoadMenuRightsItem(msMain, _currentOperator.RightsCollection);

                    menu_control_stop.Enabled = false;
                    menu_file_login.Enabled = false;
                    menu_file_logoff.Enabled = true;
                    SynchroButton();
                }
                else
                {
                    LoadMenuRightsItem(msMain, LogoffMenuList);
                    menu_control_stop.Enabled = false;
                    menu_file_login.Enabled = true;
                    menu_file_logoff.Enabled = false;
                    SynchroButton();
                }

                Application.AddMessageFilter(MyFilter);

                TimerRealtime = new System.Timers.Timer(RealTimerInterval);
                TimerRealtime.Elapsed += TimerRealtimeElapsed;

                TimerLogoff = new System.Timers.Timer(LogoffTime*1000);
                TimerLogoff.Elapsed += TimerLogoffElapsed;
                TimerLogoff.Start();
            }
            catch (Exception ex)
            {
                WriteLogFile(LogFile, "Initialization", ex.Message);
            }
        }

        #endregion

        #region 方法

        /// <summary>
        /// Zedgraph 右键菜单去掉恢复默认.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="menustrip">The menustrip.</param>
        /// <param name="mousePt">The mouse pt.</param>
        /// <param name="objState">State of the obj.</param>
        private static void ZgcContextMenuBuilder(ZedGraphControl sender, ContextMenuStrip menustrip, Point mousePt,
            ZedGraphControl.ContextMenuObjectState objState)
        {
            foreach (ToolStripMenuItem item in menustrip.Items)
            {
                if (Convert.ToString(item.Tag).Equals("set_default"))
                {
                    //menuStrip.Items.Remove(item);
                    item.Visible = false;
                    break;
                }
            }
        }

        /// <summary>
        /// Loads the menu rights item.
        /// </summary>
        /// <param name="msCurrentMenu">The ms current menu.</param>
        /// <param name="menuList">The menu list.</param>
        private void LoadMenuRightsItem(MenuStrip msCurrentMenu, string[] menuList)
        {
            foreach (ToolStripMenuItem tsmiRootLevel in msCurrentMenu.Items)
            {
                if (tsmiRootLevel != null)
                {
                    // 如果内部名称相同
                    foreach (string menu in menuList)
                    {
                        if (menu.Equals(tsmiRootLevel.Name))
                        {
                            // 设置名称和显隐状态
                            //tsmiRootLevel.Text = tmpRights.RightsCaption;
                            //tsmiRootLevel.Visible = false;
                            tsmiRootLevel.Enabled = false; // 防止菜单项快捷键激发事件
                            break;
                        }
                    }
                    // 使用递归加载所有子项
                    LoadMenuRightsChildrenItem(tsmiRootLevel, menuList);
                }
            }
        }

        /// <summary>
        /// Loads the menu rights children item.
        /// </summary>
        /// <param name="tsmiRootLevel">The tsmi root level.</param>
        /// <param name="menuList">The menu list.</param>
        private void LoadMenuRightsChildrenItem(ToolStripMenuItem tsmiRootLevel, string[] menuList)
        {
            // 使用 ToolStripItem 基类遍历获取下级菜单项
            foreach (ToolStripItem tsmiNextLevel in tsmiRootLevel.DropDownItems)
            {
                // 如果是菜单项而不是其它菜单项类型
                if (tsmiNextLevel is ToolStripMenuItem)
                {
                    // 如果内部名称相同
                    foreach (string menu in menuList)
                    {
                        if (menu.Equals(tsmiNextLevel.Name))
                        {
                            // 设置名称和显隐状态
                            //tsmiNextLevel.Text = tmpRights.RightsCaption;
                            //tsmiNextLevel.Visible = false;
                            tsmiNextLevel.Enabled = false; // 防止菜单项快捷键激发事件
                            break;
                        }
                    }
                    // 使用递归加载所有次级子项
                    LoadMenuRightsChildrenItem(tsmiNextLevel as ToolStripMenuItem, menuList);
                }
                    // 如果是分隔项而不是其它菜单项类型
                else if (tsmiNextLevel is ToolStripSeparator)
                {
                    // 如果内部名称相同
                    foreach (string menu in menuList)
                    {
                        if (menu.Equals(tsmiNextLevel.Name))
                        {
                            // 设置显隐状态
                            tsmiNextLevel.Enabled = false;
                            break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Synchroes the buttons.
        /// </summary>
        private void SynchroButton()
        {
            btn_run.Enabled = menu_control_run.Enabled;
            btn_stop.Enabled = menu_control_stop.Enabled;
            btn_curve_history.Enabled = menu_control_history.Enabled;
            btn_config.Enabled = menu_config_config.Enabled;
            btn_info.Enabled = menu_help_about.Enabled;
            btn_quit.Enabled = menu_file_quit.Enabled;

            //btn_run.Visible = menu_control_run.Enabled;
            //btn_stop.Visible = menu_control_stop.Enabled;
            //btn_history.Visible = menu_control_history.Enabled;
            //btn_config.Visible = menu_config_config.Enabled;
            //btn_info.Visible = menu_help_about.Enabled;
            //btn_quit.Visible = menu_file_quit.Enabled;
        }

        /// <summary>
        /// 保存日志文件.
        /// </summary>
        /// <param name="fileName">日志文件名.</param>
        /// <param name="category">日志类型.</param>
        /// <param name="content">日志内容.</param>
        private void WriteLogFile(string fileName, string category, string content)
        {
            try
            {
                if (!File.Exists(fileName))
                {
                    string directory = fileName.Substring(0, fileName.LastIndexOf("\\", System.StringComparison.Ordinal));
                    if (!Directory.Exists(directory)) //检查cache目录是否已创建
                        Directory.CreateDirectory(directory); //若尚未创建，则创建目录
                    FileStream f = File.Create(fileName);
                    f.Close();
                    f.Dispose();
                }

                StreamWriter fs = new StreamWriter(fileName, true, System.Text.Encoding.GetEncoding("gb2312"));
                string timeNow = DateTime.Now.ToString("yy/MM/dd HH:mm:ss");
                fs.WriteLine(timeNow + " " + category + ": " + content);

                fs.Close(); //关闭文件
                fs.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Loads the settings.
        /// </summary>
        private void LoadSettings()
        {
            try
            {
                string SQLGetDevices = ConfigAppSettings.GetSettingString("SQLGetDevices", "SELECT * FROM @DevicesTable");
                string DevicesTable = ConfigAppSettings.GetSettingString("DevicesTable", "Devices");

                string SQLGetParameters = ConfigAppSettings.GetSettingString("SQLGetParameters",
                    "SELECT * FROM @ParametersTable WHERE DeviceID = @DeviceID");
                string ParametersTable = ConfigAppSettings.GetSettingString("ParametersTable", "Parameters");

                SQLSaveData[0] = ConfigAppSettings.GetSettingString("SQLSaveData0", ",");
                SQLSaveData[1] = ConfigAppSettings.GetSettingString("SQLSaveData1",
                    "INSERT INTO @DataTable (Time, ParameterName, Value, DeviceID) VALUES ");
                SQLSaveData[2] = ConfigAppSettings.GetSettingString("SQLSaveData2",
                    "('@Time', '@ParameterName', '@Value', '@DeviceID')");
                SQLGetHistoryData = ConfigAppSettings.GetSettingString("SQLGetHistoryData",
                    "SELECT * FROM @DataTable WHERE ParameterName='@ParameterName' AND DeviceID='@DeviceID' AND Time >= '@StartTime' AND Time <= '@EndTime'");

                SQLGetHistoryData1 = ConfigAppSettings.GetSettingString("SQLGetHistoryData1",
                    "DECLARE @sql1 varchar(8000); SELECT @sql1 = ISNULL(@sql1 + '],[' , '') + [Name] FROM [@CurvesTable] GROUP BY [Name]; SET @sql1 = '[' + @sql1 + ']';");
                SQLGetHistoryData2 = ConfigAppSettings.GetSettingString("SQLGetHistoryData2",
                    "DECLARE @sql2 varchar(8000); SELECT @sql2 = ISNULL(@sql2 + ''',MAX([' , '') + [Name] +']) AS ''' + [Name]  FROM [@CurvesTable] GROUP BY [Name]; SET @sql2 = 'MAX([' + @sql2 + '''';");
                SQLGetHistoryData3 = ConfigAppSettings.GetSettingString("SQLGetHistoryData3",
                    "EXEC ('SELECT [Time] AS ''时间'',' + @sql2 + ' 	FROM (SELECT * FROM [@DataTable] WHERE [Time] &gt;= ''@StartTime'' AND [Time] &lt; ''@EndTime'') AS a PIVOT (MAX([Value]) FOR [ParameterName] IN (' + @sql1 + ')) b GROUP BY [Time] ORDER BY [Time]');");
                
                DataTable = ConfigAppSettings.GetSettingString("DataTable", "Data");

                CurvesTable = ConfigAppSettings.GetSettingString("CurvesTable", "Curves");

                DCSName = ConfigAppSettings.GetSettingString("DCSName", "磨机工况信息");

                DataTable deviceDataTable = SQLHelper.ExcuteDataTable(SQLHelper.ConnectionStringLocalTransaction,
                    GetDevicesCommand(SQLGetDevices, DevicesTable));

                Devices = new Device[deviceDataTable.Rows.Count];
                for (int deviceID = 0; deviceID < deviceDataTable.Rows.Count; deviceID++)
                {
                    Devices[deviceID].Id = Convert.ToInt32(deviceDataTable.Rows[deviceID][0]);
                    Devices[deviceID].Name = Convert.ToString(deviceDataTable.Rows[deviceID][1]);
                    Devices[deviceID].State = Convert.ToBoolean(deviceDataTable.Rows[deviceID][2]);
                    Devices[deviceID].SyncState = Convert.ToBoolean(deviceDataTable.Rows[deviceID][3]);
                    Devices[deviceID].ModbusTcpDevice = new ModbusTCPDevice
                    {
                        IPAddress = Convert.ToString(deviceDataTable.Rows[deviceID][4]),
                        PortName = Convert.ToInt32(deviceDataTable.Rows[deviceID][5]),
                        UnitID = Convert.ToByte(deviceDataTable.Rows[deviceID][6]),
                        TCPClient = new TcpClient()
                    };
                    Devices[deviceID].ModbusTCPMasterCreated = false;
                    Devices[deviceID].ModbusTCPMasterUpdated = false;

                    DataTable parameterDataTable = SQLHelper.ExcuteDataTable(SQLHelper.ConnectionStringLocalTransaction,
                        GetParametersCommand(SQLGetParameters, ParametersTable,
                            Convert.ToString(deviceDataTable.Rows[deviceID][0])));
                    Devices[deviceID].Parameters = new Parameter[parameterDataTable.Rows.Count];
                    for (int index = 0; index < parameterDataTable.Rows.Count; index++)
                    {
                        Devices[deviceID].Parameters[index].Id = Convert.ToInt32(parameterDataTable.Rows[index][0]);
                        Devices[deviceID].Parameters[index].Name = Convert.ToString(parameterDataTable.Rows[index][1]);
                        Devices[deviceID].Parameters[index].Address = Convert.ToUInt16(parameterDataTable.Rows[index][2]);
                        Devices[deviceID].Parameters[index].Ratio = Convert.ToDouble(parameterDataTable.Rows[index][3]);
                        Devices[deviceID].Parameters[index].DeviceID = Convert.ToInt32(parameterDataTable.Rows[index][4]);
                        Devices[deviceID].Parameters[index].Value = 0;
                    }
                }

                DataTable modbusRTUParaDataTable = SQLHelper.ExcuteDataTable(SQLHelper.ConnectionStringLocalTransaction,
                    GetParametersCommand(SQLGetParameters, ParametersTable,
                        0.ToString(CultureInfo.InvariantCulture)));
                ModbusRTUParameters = new Parameter[modbusRTUParaDataTable.Rows.Count];
                for (int index = 0; index < modbusRTUParaDataTable.Rows.Count; index++)
                {
                    ModbusRTUParameters[index].Name = Convert.ToString(modbusRTUParaDataTable.Rows[index][1]);
                    ModbusRTUParameters[index].Address = Convert.ToUInt16(modbusRTUParaDataTable.Rows[index][2]);
                    ModbusRTUParameters[index].Ratio = Convert.ToDouble(modbusRTUParaDataTable.Rows[index][3]);
                    ModbusRTUParameters[index].DeviceID = Convert.ToInt32(modbusRTUParaDataTable.Rows[index][4]);
                    ModbusRTUParameters[index].Value = 0;
                }

                string tempString = ConfigAppSettings.GetValue("LogoffMenulist").Trim();
                if (tempString.Length > 0)
                {
                    LogoffMenuList = tempString.Split(',');
                }
                LogoffTime = ConfigAppSettings.GetSettingInt("LogoffTime", LogoffTime);

                RealTimerInterval = ConfigAppSettings.GetSettingInt("RealTime", RealTimerInterval); //时间间隔

                TimerRealtime.Interval = RealTimerInterval;

                ModbusRtuDevice.SerialPortObject = new SerialPort
                    (
                    ConfigAppSettings.GetSettingString("ModbusRTUPortName", "COM1"),
                    ConfigAppSettings.GetSettingInt("ModbusRTUBaudRate", 19200),
                    Parity.None,
                    ConfigAppSettings.GetSettingInt("ModbusRTUDataBits", 8),
                    (StopBits) ConfigAppSettings.GetSettingSingle("ModbusRTUStopBits", 1)
                    );

                ModbusRtuDevice.UnitID = ConfigAppSettings.GetSettingByte("ModbusRTUDeviceID", 1);

                MasterPaneGraphRealtime.Title.Text = ConfigAppSettings.GetSettingString("MasterTitle", "My MasterPane Title");
                MasterPaneGraphRealtime.Title.FontSpec.Size = ConfigAppSettings.GetSettingSingle("MasterTitleSize", 12);

                tempString = ConfigAppSettings.GetValue("CurveList").Trim();
                if (tempString.Length > 0)
                {
                    string[] tempstrings = tempString.Split(',');
                    CurveList = new int[tempstrings.Length];
                    for (int index = 0; index < tempstrings.Length; index++)
                    {
                        CurveList[index] = Convert.ToInt32(tempstrings[index]);
                    }
                }

                tempString = ConfigAppSettings.GetValue("Proportion").Trim();
                if (tempString.Length > 0)
                {
                    string[] tempstrings = tempString.Split(',');
                    Proportion = new float[tempstrings.Length];
                    for (int index = 0; index < tempstrings.Length; index++)
                    {
                        Proportion[index] = Convert.ToSingle(tempstrings[index]);
                    }
                }

                DataListLength = ConfigAppSettings.GetSettingInt("DataListLength", 720);

                string SQLGetCurves = ConfigAppSettings.GetSettingString("SQLGetCurves", "SELECT * FROM @CurvesTable");
                DataTable curvesDataTable = SQLHelper.ExcuteDataTable(SQLHelper.ConnectionStringLocalTransaction,
                    GetCurvesCommand(SQLGetCurves, CurvesTable));

                CurveCount = curvesDataTable.Rows.Count;
                Curves = new Curve[CurveCount];
                for (int index = 0; index < CurveCount; index++)
                {
                    Curves[index] = new Curve
                    {
                        Id = Convert.ToInt32(curvesDataTable.Rows[index][0]),
                        Name = Convert.ToString(curvesDataTable.Rows[index][1]),
                        DataList = new PointPairList(),
                        DeviceID = Convert.ToInt32(curvesDataTable.Rows[index][2]),
                        Address = Convert.ToUInt16(curvesDataTable.Rows[index][3]),
                        LineColour = string.IsNullOrEmpty(Convert.ToString(curvesDataTable.Rows[index][4]))
                            ? Rotator.NextColor
                            : Color.FromName(Convert.ToString(curvesDataTable.Rows[index][4])),
                        LineType = string.IsNullOrEmpty(Convert.ToString(curvesDataTable.Rows[index][5])) ||
                                   Convert.ToBoolean(curvesDataTable.Rows[index][5]),
                        LineWidth = string.IsNullOrEmpty(Convert.ToString(curvesDataTable.Rows[index][6]))
                            ? 2
                            : Convert.ToSingle(curvesDataTable.Rows[index][6]),
                        CurveSymbolType = Rotator.NextSymbol,
                        SymbolSize = string.IsNullOrEmpty(Convert.ToString(curvesDataTable.Rows[index][8]))
                            ? 4
                            : Convert.ToSingle(curvesDataTable.Rows[index][8]),
                        XAxisTitle = Convert.ToString(curvesDataTable.Rows[index][9]),
                        YAxisTitle = Convert.ToString(curvesDataTable.Rows[index][10]),
                        YAxisMax = Convert.ToDouble(curvesDataTable.Rows[index][11]),
                        YAxisMin = Convert.ToDouble(curvesDataTable.Rows[index][12])
                    };
                    if (!(string.IsNullOrEmpty(Convert.ToString(curvesDataTable.Rows[index][7]))))
                    {
                        switch (Convert.ToString(curvesDataTable.Rows[index][7]))
                        {
                            case "Diamond":
                                Curves[index].CurveSymbolType = SymbolType.Diamond;
                                break;
                            case "Circle":
                                Curves[index].CurveSymbolType = SymbolType.Circle;
                                break;
                            case "Square":
                                Curves[index].CurveSymbolType = SymbolType.Square;
                                break;
                            case "Star":
                                Curves[index].CurveSymbolType = SymbolType.Star;
                                break;
                            case "Triangle":
                                Curves[index].CurveSymbolType = SymbolType.Triangle;
                                break;
                            case "Plus":
                                Curves[index].CurveSymbolType = SymbolType.Plus;
                                break;
                            case "None":
                                Curves[index].CurveSymbolType = SymbolType.None;
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLogFile(LogFile, "LoadSettings", ex.Message);
            }
        }

        /// <summary>
        /// Gets the devices command.
        /// </summary>
        /// <param name="sqlCmd">The SQL command.</param>
        /// <param name="table">The table.</param>
        /// <returns>
        /// Command
        /// </returns>
        private string GetDevicesCommand(string sqlCmd, string table)
        {
            string sql = sqlCmd;
            sql = sql.Replace("@DevicesTable", table);
            return sql;
        }

        /// <summary>
        /// Gets the parameters command.
        /// </summary>
        /// <param name="sqlCmd">The SQL command.</param>
        /// <param name="table">The table.</param>
        /// <param name="deviceID">The device identifier.</param>
        /// <returns>
        /// Command
        /// </returns>
        private string GetParametersCommand(string sqlCmd, string table, string deviceID)
        {
            string sql = sqlCmd;
            sql = sql.Replace("@ParametersTable", table);
            sql = sql.Replace("@DeviceID", deviceID);
            return sql;
        }

        /// <summary>
        /// Gets the save parameter command.
        /// </summary>
        /// <param name="sqlCmd">The SQL command.</param>
        /// <param name="table">The table.</param>
        /// <param name="time">The time.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// Command
        /// </returns>
        private string GetSaveDataCommand(string[] sqlCmd, string table, string time, Parameter[] parameters)
        {
            string sql = "";
            if (parameters.Length <= 0) return sql;
            sql = sqlCmd[0].Replace("@DataTable", table);
            for (int index = 0; index < parameters.Length; index++)
            {
                if (index > 0)
                {
                    sql += sqlCmd[1];
                }
                sql += sqlCmd[2]
                    .Replace("@Time", time)
                    .Replace("@ParameterName", parameters[index].Name)
                    .Replace("@Value", parameters[index].Value.ToString(CultureInfo.InvariantCulture))
                    .Replace("@DeviceID", parameters[index].DeviceID.ToString(CultureInfo.InvariantCulture));
            }
            return sql;
        }

        /// <summary>
        /// Gets the curves command.
        /// </summary>
        /// <param name="sqlCmd">The SQL command.</param>
        /// <param name="table">The table.</param>
        /// <returns>Command</returns>
        private string GetCurvesCommand(string sqlCmd, string table)
        {
            string sql = sqlCmd;
            sql = sql.Replace("@CurvesTable", table);
            return sql;
        }

        /// <summary>
        /// Gets the history data command.
        /// </summary>
        /// <param name="sqlCmd">The SQL command.</param>
        /// <param name="dataTable">The data table.</param>
        /// <param name="curveTable">The curce table.</param>
        /// <param name="statrTime">The statr time.</param>
        /// <param name="endTime">The end time.</param>
        /// <returns>
        /// Command
        /// </returns>
        private string GetHistoryDataCommand(string sqlCmd, string dataTable, string curveTable, DateTime statrTime, DateTime endTime)
        {
            string sql = sqlCmd;
            sql = sql.Replace("@DataTable", dataTable)
                .Replace("@CurvesTable", curveTable)
                .Replace("@StartTime", statrTime.ToString("yyyy-MM-dd HH:mm:ss.fff"))
                .Replace("@EndTime", endTime.ToString("yyyy-MM-dd HH:mm:ss.fff"));
            return sql;
        }

        /// <summary>
        /// Gets the history data command.
        /// </summary>
        /// <param name="sqlCmd">The SQL command.</param>
        /// <param name="datatable">The table.</param>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="deviceID">The device identifier.</param>
        /// <param name="statrTime">The statr time.</param>
        /// <param name="endTime">The end time.</param>
        /// <returns>
        /// Command
        /// </returns>
        private string GetHistoryDataCommand(string sqlCmd, string datatable, string parameterName, int deviceID, DateTime statrTime, DateTime endTime)
        {
            string sql = sqlCmd;
            sql = sql.Replace("@DataTable", datatable)
                .Replace("@ParameterName", parameterName)
                .Replace("@DeviceID", deviceID.ToString(CultureInfo.InvariantCulture))
                .Replace("@StartTime", statrTime.ToString("yyyy-MM-dd HH:mm:ss.fff"))
                .Replace("@EndTime", endTime.ToString("yyyy-MM-dd HH:mm:ss.fff"));
            return sql;
        }

        /// <summary>
        /// 建立Modbus监听.
        /// </summary>
        /// <returns>
        /// Modbus RTU Listener是否创建成功
        /// </returns>
        private bool ModbusRTUCreatListener(ModbusRTUDevice modbusRtuDevice)
        {
            try
            {
                modbusRtuDevice.SerialPortObject.Parity = Parity.None;

                if (modbusRtuDevice.SerialPortObject.PortName.Equals(string.Empty))
                {
                    statusStrip_main.Text = string.Format("未打开端口{0}(端口不存在)!", modbusRtuDevice.SerialPortObject.PortName);
                }
                else if (modbusRtuDevice.SerialPortObject.IsOpen)
                {
                    statusStrip_main.Text = string.Format("未打开端口{0}(已经被打开)!", modbusRtuDevice.SerialPortObject.PortName);
                }
                else
                {
                    modbusRtuDevice.SerialPortObject.Open();
                }
                if (modbusRtuDevice.SerialPortObject.IsOpen)
                {
                    // create modbus slave
                    ModbusRTUSlave = ModbusSerialSlave.CreateRtu(modbusRtuDevice.UnitID,
                        modbusRtuDevice.SerialPortObject);
                    ModbusRTUSlave.ModbusSlaveRequestReceived += ModbusRTU_Request_Event;
                    ModbusRTUSlave.DataStore = DataStoreFactory.CreateDefaultDataStore();
                    ModbusRTUSlave.DataStore.DataStoreWrittenTo += ModbusRTU_DataStoreWriteTo_Event;

                    ModbusRTUSlaveThread = new Thread(ModbusRTUSlave.Listen);
                    ModbusRTUSlaveThread.Start();
                    return true;
                }
            }
            catch (Exception ex)
            {
                status_Label.Text = ex.Message;
                //WriteLogFile(LogFile, "ModbusRTUCreatListener", ex.Message);
            }
            return false;
        }

        /// <summary>
        /// Handles the Event event of the ModbusRTU_DataStoreWriteTo control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Modbus.Data.DataStoreEventArgs" /> instance containing the event data.</param>
        private void ModbusRTU_DataStoreWriteTo_Event(object sender, DataStoreEventArgs e)
        {
            ModbusRTUSlaveUpdated = true;
        }

        /// <summary>
        /// Handles the Event of the Modbus_Request control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The instance containing the event data.</param>
        private void ModbusRTU_Request_Event(object sender, ModbusSlaveRequestEventArgs e)
        {

        }

        /// <summary>
        /// Get the Modbuses rtu values.
        /// </summary>
        private void ModbusRTUGetValue()
        {
            try
            {
                if (ModbusRTUSlaveCreated)
                {
                    for (int paraIndex = 0; paraIndex < ModbusRTUParameters.Length; paraIndex++)
                    {
                        ushort[] register = new ushort[2];
                        try
                        {
                            //读寄存器
                            register[0] =
                                ModbusRTUSlave.DataStore.HoldingRegisters[ModbusRTUParameters[paraIndex].Address];
                            register[1] =
                                ModbusRTUSlave.DataStore.HoldingRegisters[ModbusRTUParameters[paraIndex].Address + 1];
                        }
                        catch (Exception)
                        {
                            ModbusRTUStopComm(); //处理连接错误，重试连接
                            ModbusRTUCreatListener(ModbusRtuDevice);
                            //WriteLogFile(LogFile, "ModbusRTU->ModbusTCP", ex.Message);
                            continue;
                        }
                        for (int deviceIndex = 0; deviceIndex < Devices.Length; deviceIndex++)
                        {
                            if (!Devices[deviceIndex].State ||
                                !Devices[deviceIndex].SyncState ||
                                !Devices[deviceIndex].ModbusTCPMasterCreated)
                                continue;
                            try
                            {
                                //读寄存器
                                Devices[deviceIndex].ModbusTcpDevice.ModbusTCPMaster.WriteMultipleRegisters(
                                    Devices[deviceIndex].ModbusTcpDevice.UnitID,
                                    (ushort) (ModbusRTUParameters[paraIndex].Address - 1),
                                    register);
                            }
                            catch (Exception)
                            {
                                ModbusTCPStopComm(Devices[deviceIndex].ModbusTcpDevice); //处理连接错误，重试连接
                                ModbusTCPCreateClient(ref Devices[deviceIndex].ModbusTcpDevice);
                            }
                        }
                        byte[] byteString = new byte[4];
                        for (int j = 0; j < 2; j++)
                        {
                            byte[] tempByte = BitConverter.GetBytes(register[j]);
                            byteString[2*j] = tempByte[0];
                            byteString[2*j + 1] = tempByte[1];
                        }
                        float value = BitConverter.ToSingle(byteString, 0); //还原用2个8位寄存器保存的1个浮点数
                        ModbusRTUParameters[paraIndex].Value = value*ModbusRTUParameters[paraIndex].Ratio;
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLogFile(LogFile, "ModbusRTUGetValue", ex.Message);
            }
        }

        /// <summary>
        /// Stops the communication.
        /// </summary>
        private void ModbusRTUStopComm() //通讯停止
        {
            try
            {
                if (ModbusRTUSlaveCreated)
                {
                    ModbusRTUSlave.ModbusSlaveRequestReceived -= ModbusRTU_Request_Event;
                    ModbusRTUSlave.DataStore.DataStoreWrittenTo -= ModbusRTU_DataStoreWriteTo_Event;
                    ModbusRtuDevice.SerialPortObject.Close();
                    ModbusRTUSlave.Dispose();
                    ModbusRTUSlaveThread.Abort();
                    ModbusRTUSlaveCreated = false;
                }
            }
            catch (Exception ex)
            {
                WriteLogFile(LogFile, "StopModbusRTUComm", ex.Message);
            }
        }

        /// <summary>
        /// 建立Modbus TCP 客户端.
        /// </summary>
        /// <param name="modbusTcpDevice">The modbus TCP device.</param>
        /// <returns>
        /// Modbus TCP 客户端是否创建成功
        /// </returns>
        private bool ModbusTCPCreateClient(ref ModbusTCPDevice modbusTcpDevice)
        {
            try
            {
                if (string.IsNullOrEmpty(modbusTcpDevice.IPAddress) || modbusTcpDevice.PortName.Equals(0))
                {
                    return false;
                }
                modbusTcpDevice.TCPClient = new TcpClient(modbusTcpDevice.IPAddress, modbusTcpDevice.PortName);

                modbusTcpDevice.ModbusTCPMaster = ModbusIpMaster.CreateIp(modbusTcpDevice.TCPClient);
                // create Modbus TCP Master with the tcp client
                modbusTcpDevice.ModbusTCPMaster.Transport.ReadTimeout = 1000;
                return true;
            }
            catch (Exception ex)
            {
                status_Label.Text = ex.Message;
                //WriteLogFile(LogFile, "ModbusTCPCreateClient", ex.Message);
            }
            return false;
        }

        /// <summary>
        /// Get The Modbus TCP  value.
        /// </summary>
        /// <param name="device">The device.</param>
        private void ModbusTCPGetValue(ref Device device)
        {
            try
            {
                if (!device.State) return;
                for (int paraIndex = 0; paraIndex < device.Parameters.Length; paraIndex++)
                {
                    ushort[] register;
                    try
                    {
                        //读寄存器
                        register =
                            device.ModbusTcpDevice.ModbusTCPMaster.ReadHoldingRegisters(
                                device.ModbusTcpDevice.UnitID,
                                (ushort) (device.Parameters[paraIndex].Address - 1), 2);
                        device.ModbusTCPMasterUpdated = true;
                    }
                    catch (Exception)
                    {
                        device.ModbusTCPMasterCreated = !ModbusTCPStopComm(device.ModbusTcpDevice); //处理连接错误，重试连接
                        device.ModbusTCPMasterCreated = ModbusTCPCreateClient(ref device.ModbusTcpDevice);
                        continue;
                    }

                    if (ModbusRTUSlaveCreated)
                    {
                        try
                        {
                            ModbusRTUSlave.DataStore.HoldingRegisters[device.Parameters[paraIndex].Address] =
                                register[0];
                            ModbusRTUSlave.DataStore.HoldingRegisters[device.Parameters[paraIndex].Address + 1] =
                                register[1];
                        }
                        catch (Exception)
                        {
                            ModbusRTUStopComm(); //处理连接错误，重试连接
                            ModbusRTUCreatListener(ModbusRtuDevice);
                            //WriteLogFile(LogFile, "ModbusTCP->ModbusRTU", ex.Message);
                        }
                    }
                    byte[] byteString = new byte[4];
                    for (int j = 0; j < 2; j++)
                    {
                        byte[] tempByte = BitConverter.GetBytes(register[j]);
                        byteString[2*j] = tempByte[0];
                        byteString[2*j + 1] = tempByte[1];
                    }
                    float value = BitConverter.ToSingle(byteString, 0); //还原用2个8位寄存器保存的1个浮点数
                    device.Parameters[paraIndex].Value = value*device.Parameters[paraIndex].Ratio;
                }
            }
            catch (Exception ex)
            {
                WriteLogFile(LogFile, "ModbusTCPGetValue", ex.Message);
            }
        }

        /// <summary>
        /// Stops the communication.
        /// </summary>
        /// <param name="modbusTcpDevice">The modbus TCP device.</param>
        /// <returns></returns>
        private bool ModbusTCPStopComm(ModbusTCPDevice modbusTcpDevice) //通讯停止
        {
            try
            {
                if (modbusTcpDevice.ModbusTCPMaster != null)
                    modbusTcpDevice.ModbusTCPMaster.Dispose();
                if (modbusTcpDevice.TCPClient != null)
                    modbusTcpDevice.TCPClient.Close();
                return true;
            }
            catch (Exception ex)
            {
                WriteLogFile(LogFile, "StopModbusTCPComm", ex.Message);
            }
            return false;
        }

        /// <summary>
        /// 刷新工况列表.托管
        /// </summary>
        private delegate void UpdateRegisterListDelegate();

        /// <summary>
        /// 刷新工况列表.
        /// </summary>
        private void UpdateRegisterList()
        {
            if (InvokeRequired)
            {
                Invoke(new UpdateRegisterListDelegate(UpdateRegisterList));
                return;
            }
            try
            {
                ImageList imageList = new ImageList {ImageSize = new System.Drawing.Size(1, 20)};
                listview_parainfo.SmallImageList = imageList; //这里设置listView的SmallImageList ,用imgList将其撑大。

                listview_parainfo.BeginUpdate();
                listview_parainfo.Columns.Clear();
                listview_parainfo.Groups.Clear();
                listview_parainfo.Items.Clear();
                listview_parainfo.View = View.Details;

                if (listview_parainfo.Columns.Count < 2)
                {
                    ColumnHeader columnHeader0 = new ColumnHeader {Text = "变量", Width = 170};
                    ColumnHeader columnHeader1 = new ColumnHeader {Text = "数值", Width = 60};
                    listview_parainfo.Columns.AddRange(new ColumnHeader[] {columnHeader0, columnHeader1});
                }
                ListViewGroup[] group = new ListViewGroup[Devices.Length + 1];
                for (int index = 0; index < (Devices.Length); index++)
                {
                    group[index] = new ListViewGroup(Devices[index].Name, HorizontalAlignment.Center);
                }
                group[Devices.Length] = new ListViewGroup(DCSName, HorizontalAlignment.Center);
                listview_parainfo.Groups.AddRange(group);

                for (int deviceIndex = 0; deviceIndex < (Devices.Length); deviceIndex++)
                {
                    if (Devices[deviceIndex].State)
                    {
                        listview_parainfo.Items.Add(
                            new ListViewItem(
                                new string[]
                                {
                                    "状态",
                                    Devices[deviceIndex].ModbusTCPMasterCreated ? "在线" : "离线"
                                },
                                group[deviceIndex]
                                )
                            {
                                BackColor = Devices[deviceIndex].ModbusTCPMasterCreated ? Color.LightGreen : Color.Pink,
                                Font = new Font(DefaultFont.FontFamily, 9)
                            }
                            );

                        ListViewItem[] items = new ListViewItem[Devices[deviceIndex].Parameters.Length];
                        for (int paraIndex = 0; paraIndex < Devices[deviceIndex].Parameters.Length; paraIndex++)
                        {
                            items[paraIndex] =
                                new ListViewItem(
                                    new string[]
                                    {
                                        Devices[deviceIndex].Parameters[paraIndex].Name,
                                        Devices[deviceIndex].Parameters[paraIndex].Value.ToString("F02")
                                    },
                                    group[deviceIndex])
                                {BackColor = (paraIndex%2 == 0 ? Color.White : Color.Cyan)};
                        }
                        listview_parainfo.Items.AddRange(items);
                    }
                }

                if (ModbusRTUSlaveCreated)
                {
                    ListViewItem[] items = new ListViewItem[ModbusRTUParameters.Length];

                    for (int paraIndex = 0; paraIndex < ModbusRTUParameters.Length; paraIndex++)
                    {
                        items[paraIndex] =
                            new ListViewItem(
                                new string[]
                                {
                                    ModbusRTUParameters[paraIndex].Name,
                                    ModbusRTUParameters[paraIndex].Value.ToString("F02")
                                },
                                group[Devices.Length])
                            {
                                BackColor = (paraIndex%2 == 0 ? Color.White : Color.Cyan)
                            };
                    }
                    listview_parainfo.Items.AddRange(items);
                }

                listview_parainfo.EndUpdate();

                //listview_parainfo.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);

            }
            catch (Exception ex)
            {
                WriteLogFile(LogFile, "RefreshRegisterList", ex.Message);
            }
        }

        /// <summary>
        /// Saves the parameter.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="time">The time.</param>
        /// <returns>
        /// Result rows
        /// </returns>
        private int SaveParameter(Parameter[] parameters, DateTime time)
        {
            string sql = GetSaveDataCommand(SQLSaveData, DataTable,
                time.ToString(CultureInfo.InvariantCulture), parameters);
            int count = SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, sql);
            return count;
        }

        /// <summary>
        /// Clears the graph.
        /// </summary>
        private void ClearGraph() //清空图形和信息
        {
            //label_info1_info.Text = ""; //清空波形图信息
            //label_info1_load.Text = "";
            //label_info1_voltage.Text = "";
            //zedGraphControl.MasterPane.PaneList.Clear(); //清波形图
            //zedGraphControl.MasterPane.GraphObjList.Clear();
            zgc_realtime.Invalidate(); //清空zedgraph信息
            zgc_history.Invalidate();
        }

        private delegate void UpdateGraphDelegate(ref MasterPane masterPane, ref ZedGraphControl zgc, Curve[] curves);

        /// <summary>
        /// 刷新曲线.
        /// </summary>
        private void UpdateGraph(ref MasterPane masterPane,ref ZedGraphControl zgc, Curve[] curves)
        {
            if (InvokeRequired)
            {
                Invoke(new UpdateGraphDelegate(UpdateGraph));
                return;
            }
            try
            {
                masterPane.PaneList.Clear();
                masterPane.GraphObjList.Clear();
                for (int index = 0; index < CurveCount; index++)
                {
                    if (index == 0)
                    {
                        masterPane.Add(CreatGraphPane(curves[index],GraphPaneType.First));
                    }
                    else if (index == CurveCount - 1)
                    {
                        masterPane.Add(CreatGraphPane(curves[index], GraphPaneType.Last));
                    }
                    else
                    {
                        bool graphPaneCreated = false;
                        for (int i = 0; i < CurveList.Length; i++)
                        {
                            int temp = 0;
                            for (int j = 0; j < i; j++)
                            {
                                temp += CurveList[j];
                            }
                            if (index == temp)
                            {
                                masterPane.Add(CreatGraphPane(curves[index], GraphPaneType.First));
                                graphPaneCreated = true;
                                break;
                            }
                            if (index == temp - 1)
                            {
                                masterPane.Add(CreatGraphPane(curves[index], GraphPaneType.Last));
                                graphPaneCreated = true;
                                break;
                            }
                        }
                        if (!graphPaneCreated)
                        {
                            masterPane.Add(CreatGraphPane(curves[index], GraphPaneType.Normal));
                        }
                    }
                }

                using (Graphics g = zgc.CreateGraphics())
                {
                    masterPane.SetLayout(g, false, CurveList, Proportion);
                    //masterPane.AxisChange(g);
                    //masterPane.IsCommonScaleFactor = true;
                    //g.Dispose();
                }

                zgc.AxisChange();
                zgc.Refresh();
                zgc.IsAutoScrollRange = true;
                zgc.IsShowHScrollBar = true;
                zgc.IsShowVScrollBar = true;
                zgc.IsSynchronizeXAxes = true;
            }
            catch (Exception ex)
            {
                WriteLogFile(LogFile, "UpdateGraph", ex.Message);
            }
        }

        /// <summary>
        /// Creats the graph pane.
        /// </summary>
        /// <param name="curve">The curve.</param>
        /// <param name="graphPaneType">Type of the graph pane.</param>
        /// <returns>
        /// The Graph Pane
        /// </returns>
        private GraphPane CreatGraphPane(Curve curve , GraphPaneType graphPaneType)
        {
            // Create a new graph with topLeft at (40,40) and size 600x400
            GraphPane graphPane = new GraphPane(new Rectangle(10, 10, 600, 400), curve.Name, curve.XAxisTitle, curve.YAxisTitle)
            {
                Fill = new Fill(Color.White, Color.LightCyan, 45.0F),
                BaseDimension = 6.0F
            };
            try
            {
                if (curve.DataList.Count > 1)
                {
                    graphPane.Legend.IsVisible = false;
                    graphPane.Border.IsVisible = false;
                    graphPane.Title.IsVisible = false;
                    graphPane.Margin.All = 4;

                    graphPane.XAxis.Title.IsVisible = false;
                    graphPane.XAxis.Scale.IsVisible = false;
                    graphPane.XAxis.Scale.Format = "HH:mm:ss";
                    //graphPane.XAxis.Scale.FontSpec.Angle = 30;
                    graphPane.XAxis.Scale.FontSpec.Size = 12;
                    graphPane.XAxis.Type = AxisType.Date;
                    graphPane.XAxis.MajorTic.IsOutside = false;
                    graphPane.XAxis.MinorTic.IsOutside = false;
                    graphPane.XAxis.MajorGrid.IsVisible = true;
                    graphPane.XAxis.MinorGrid.IsVisible = true;
                    graphPane.XAxis.Scale.Min = curve.DataList[0].X;
                    graphPane.XAxis.Scale.Max = curve.DataList[curve.DataList.Count - 1].X;
                    graphPane.XAxis.Scale.IsSkipLastLabel = true;

                    if (graphPaneType == GraphPaneType.First)
                    {
                        graphPane.Margin.Top = 20;
                    }
                    if (graphPaneType == GraphPaneType.Last)
                    {
                        //graphPane.XAxis.Title.IsVisible = true;
                        graphPane.XAxis.Scale.IsVisible = true;
                        graphPane.Margin.Bottom = 10;
                    }
                    if (graphPaneType != GraphPaneType.First)
                    {
                        graphPane.YAxis.Scale.IsSkipLastLabel = true;
                    }

                    // This sets the minimum amount of space for the left and right side, respectively
                    // The reason for this is so that the ChartRect's all end up being the same size.
                    graphPane.YAxis.MinSpace = 60;
                    graphPane.Y2Axis.MinSpace = 20;

                    graphPane.YAxis.MajorGrid.IsVisible = true;
                    graphPane.YAxis.Scale.Min = curve.YAxisMin;
                    if (curve.YAxisMax > curve.YAxisMin)
                    {
                        graphPane.YAxis.Scale.Max = curve.YAxisMax;
                    }
                    LineItem tmpCurve = graphPane.AddCurve(curve.Name, curve.DataList, curve.LineColour, curve.CurveSymbolType);
                    tmpCurve.Symbol.Fill = new Fill(curve.LineColour);
                    tmpCurve.Symbol.Size = curve.SymbolSize;
                    tmpCurve.Line.IsVisible = curve.LineType;
                    tmpCurve.Line.Width = curve.LineWidth;
                    tmpCurve.Line.StepType = StepType.ForwardStep;
                }
            }
            catch (Exception ex)
            {
                WriteLogFile(LogFile, "CreatGraphPane", ex.Message);
            }
            return graphPane;
        }

        /// <summary>
        /// Loads the history data.
        /// </summary>
        /// <param name="startTime">The start time.</param>
        /// <param name="endTime">The end time.</param>
        /// <returns></returns>
        private DataTable LoadHistoryData(DateTime startTime, DateTime endTime)
        {
            string sql = GetHistoryDataCommand(SQLGetHistoryData1 + SQLGetHistoryData2 + SQLGetHistoryData3, DataTable,
                CurvesTable, startTime, endTime);
            DataTable dataTable = SQLHelper.ExcuteDataTable(SQLHelper.ConnectionStringLocalTransaction, sql);
            return dataTable;
        }

        /// <summary>
        /// Loads the history curves.
        /// </summary>
        /// <param name="dataTable">The data table.</param>
        /// <returns></returns>
        private Curve[] LoadHistoryCurves(DataTable dataTable)
        {
            Curve[] curves = new Curve[CurveCount];
            
            for (int index = 0; index < CurveCount; index++)
            {
                curves[index] = Curves[index];
                curves[index].DataList.Clear();
                for (int j = 1; j < dataTable.Columns.Count; j++)
                {
                    if (dataTable.Columns[j].ColumnName.Equals(curves[index].Name))
                    {
                        for (int i = 0; i < dataTable.Rows.Count; i++)
                        {
                            curves[index].DataList.Add(new XDate(DateTime.Parse(dataTable.Rows[i][0].ToString())),
                                Convert.ToDouble(dataTable.Rows[i][j]));
                        }
                        break;
                    }
                }
            }
            return curves;
        }

        #endregion

        #region 控件响应

        /// <summary>
        /// Handles the Tick event of the timer_realtime control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void TimerRealtimeElapsed(object sender, EventArgs e)
        {
            if (!RealTimerFlag) return;
            try
            {
                for (int deviceID = 0; deviceID < Devices.Length; deviceID++)
                {
                    if (Devices[deviceID].State)
                    {
                        ModbusTCPGetValue(ref (Devices[deviceID]));
                    }
                }
                if (ModbusRTUSlaveCreated)
                {
                    ModbusRTUGetValue();
                }
                UpdateRegisterList();

                DateTime time = DateTime.Now;
                bool modbusTCPMasterUpdated = false;
                for (int index = 0; index < Devices.Length; index++)
                {
                    if (Devices[index].ModbusTCPMasterUpdated)
                    {
                        modbusTCPMasterUpdated = true;
                        break;
                    }
                }
                if (ModbusRTUSlaveUpdated || modbusTCPMasterUpdated)
                {
                    if (SaveParameter(ModbusRTUParameters, time) > 0)
                    {
                        ModbusRTUSlaveUpdated = false;
                        status_Label.Text = string.Format("{0}数据已保存", DCSName);
                    }
                    for (int index = 0; index < Devices.Length; index++)
                    {
                        if (!Devices[index].State) continue;
                        if (SaveParameter(Devices[index].Parameters, time) <= 0) continue;
                        Devices[index].ModbusTCPMasterUpdated = false;
                        status_Label.Text = string.Format("{0}数据已保存", Devices[index].Name);
                    }
                }

                double tmpTime = new XDate(time);
                for (int index = 0; index < CurveCount; index++)
                {
                    if (Curves[index].DataList.Count >= DataListLength) //数组长度限制
                    {
                        Curves[index].DataList.RemoveRange(0, (Curves[index].DataList.Count - DataListLength + 1));
                    }
                    if (Curves[index].DeviceID == 0)
                    {
                        foreach (Parameter parameter in ModbusRTUParameters)
                        {
                            if (parameter.Address != Curves[index].Address) continue;
                            Curves[index].Name = parameter.Name;
                            Curves[index].DataList.Add(tmpTime, parameter.Value);
                            break;
                        }
                    }
                    else
                    {
                        foreach (Device device in Devices)
                        {
                            if (device.Id != Curves[index].DeviceID) continue;
                            foreach (Parameter parameter in device.Parameters)
                            {
                                if (parameter.Address != Curves[index].Address) continue;
                                Curves[index].Name = parameter.Name;
                                Curves[index].DataList.Add(tmpTime, parameter.Value);
                                break;
                            }
                        }
                    }
                }
                if (UpdateGraphFlag)
                    UpdateGraph(ref MasterPaneGraphRealtime,ref zgc_realtime, Curves);

                if (MyFilter.isActive)
                {
                    TimerLogoff.Stop();
                    MyFilter.isActive = false;
                    TimerLogoff.Start();
                }
            }
            catch (Exception ex)
            {
                WriteLogFile(LogFile, "RealTimer", ex.Message); //未能正常写入文件，反馈信息到消息栏
            }
        }

        /// <summary>
        /// Handles the Elapsed event of the LogoffTimer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Timers.ElapsedEventArgs" /> instance containing the event data.</param>
        private void TimerLogoffElapsed(object sender, ElapsedEventArgs e)
        {
            TimerLogoff.Stop();
            this.Invoke((EventHandler) (delegate
            {
                menu_file_logoff_Click(sender, e);
            }));
        }

        /// <summary>
        /// Handles the Load event of the MainForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void frmMain_Load(object sender, EventArgs e)
        {
            if (_args.Length < 1) return;
            foreach (string arg in _args)
            {
                switch (arg)
                {
                    case "-auto":
                        btn_run_Click(sender, e);
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Handles the FormClosing event of the MainForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="FormClosingEventArgs"/> instance containing the event data.</param>
        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                frmLogin frmLogin = new frmLogin();
                DialogResult dialogResult = frmLogin.ShowDialog();
                if (dialogResult.Equals(DialogResult.OK))
                {
                    if (frmLogin.isPass && frmLogin.currentOperator.RightsCollection[menu_file_quit.Name].RightsState)
                    {
                        TimerRealtime.Stop();
                        for (int deviceID = 0; deviceID < Devices.Length; deviceID++)
                        {
                            Devices[deviceID].ModbusTCPMasterCreated =
                                ModbusTCPStopComm(Devices[deviceID].ModbusTcpDevice);
                        }
                        ModbusRTUStopComm();
                        WriteLogFile(LogFile, "Closed", "Software Closed!");
                        Dispose(); //释放内存，退出程序
                    }
                    else
                    {
                        e.Cancel = true;
                    }
                }
                else
                {
                    e.Cancel = true;
                }
            }
            catch (Exception ex)
            {
                WriteLogFile(LogFile, "frmMain_FormClosing", ex.Message);
            }
        }

        /// <summary>
        /// Handles the Click event of the tool_btn_rtwave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btn_run_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(ModbusRtuDevice.SerialPortObject.PortName))
            {
                MessageBox.Show("未设置Modbus RTU通讯端口！", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            }
            else
            {
                LoadSettings();
                //ClearGraph();
                try
                {
                    DateTime endTime = DateTime.Now;
                    DateTime startTime = endTime.AddSeconds((-1)*(RealTimerInterval/1000)*DataListLength);

                    for (int index = 0; index < CurveCount; index++)
                    {
                        string sql = GetHistoryDataCommand(SQLGetHistoryData, DataTable, Curves[index].Name,
                            Curves[index].DeviceID, startTime, endTime);
                        DataTable dataTable = SQLHelper.ExcuteDataTable(SQLHelper.ConnectionStringLocalTransaction, sql);
                        for (int i = 0; i < dataTable.Rows.Count; i++)
                        {
                            Curves[index].DataList.Add(new XDate(DateTime.Parse(dataTable.Rows[i][1].ToString())),
                                Convert.ToDouble(dataTable.Rows[i][3]));
                        }
                    }

                    ModbusRTUSlaveCreated = ModbusRTUCreatListener(ModbusRtuDevice);
                    if (ModbusRTUSlaveCreated)
                    {
                        for (int deviceID = 0; deviceID < Devices.Length; deviceID++)
                        {
                            if (Devices[deviceID].State)
                            {
                                Devices[deviceID].ModbusTCPMasterCreated =
                                    ModbusTCPCreateClient(ref Devices[deviceID].ModbusTcpDevice);
                            }
                        }
                        RealTimerFlag = true;
                        TimerRealtimeElapsed(sender, e);
                        TimerRealtime.Start();

                        // 加载权限菜单
                        RightsMenuDataManager rmManager = new RightsMenuDataManager();
                        rmManager.LoadMenuRightsItem(msMain, _currentOperator.RightsCollection);
                        menu_control_run.Enabled = false;
                        menu_control_history.Enabled = false;
                        menu_config_config.Enabled = false;
                        menu_config_user.Enabled = false;
                        menu_config_devices.Enabled = false;
                        menu_config_parameters.Enabled = false;
                        menu_file_quit.Enabled = false;
                        menu_file_login.Enabled = false;
                        menu_file_logoff.Enabled = true;
                        SynchroButton();

                        status_Label.Text = "运行中...";
                        _isRunning = true;
                        WriteLogFile(LogFile, "Start",
                            string.Format("Software started by {0}!", _currentOperator.ModelName));
                    }
                }
                catch (Exception ex)
                {
                    WriteLogFile(LogFile, "Run_Click", ex.Message);
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the tool_btn_rtstop control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btn_stop_Click(object sender, EventArgs e)
        {
            RealTimerFlag = false;
            TimerRealtime.Stop();

            // 加载权限菜单
            RightsMenuDataManager rmManager = new RightsMenuDataManager();
            rmManager.LoadMenuRightsItem(msMain, _currentOperator.RightsCollection);
            menu_control_stop.Enabled = false;
            menu_file_login.Enabled = false;
            menu_file_logoff.Enabled = true;
            SynchroButton();

            //label_info1_load.Text = "";
            //label_info1_voltage.Text = "";
            //label_info1_info.Text = "";
            ClearGraph();
            for (int deviceID = 0; deviceID < Devices.Length; deviceID++)
            {
                Device device = Devices[deviceID];
                device.ModbusTCPMasterCreated = ModbusTCPStopComm(Devices[deviceID].ModbusTcpDevice);
                Devices[deviceID] = device;
            }
            ModbusRTUStopComm();
            //listview_parainfo.Items.Clear();
            status_Label.Text = "停止.";
            _isRunning = false;
            WriteLogFile(LogFile, "Stop", string.Format("Software stoped by {0}!", _currentOperator.ModelName));
        }

        /// <summary>
        /// Handles the Click event of the btn_curve_realtime control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btn_curve_realtime_Click(object sender, EventArgs e)
        {
            zgc_realtime.Invalidate();
            UpdateGraphFlag = true;
        }

        /// <summary>
        /// Handles the Click event of the btn_curve_stop control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btn_curve_stop_Click(object sender, EventArgs e)
        {
            zgc_realtime.Invalidate();
            UpdateGraphFlag = false;
        }

        /// <summary>
        /// Handles the Click event of the tool_btn_history control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btn_history_Click(object sender, EventArgs e)
        {
            ofd_history.Title = "请选择Beta值历史文件";
            ofd_history.Filter = "数据文件 (*.beta)|*.beta";
            ofd_history.FilterIndex = 1;
            ofd_history.RestoreDirectory = true;
            try
            {
                DialogResult dr = ofd_history.ShowDialog();

                if (dr == DialogResult.OK)
                {

                    if (btn_stop.Enabled) //停止实时显示
                        btn_stop_Click(sender, e);

                    ClearGraph();

                    zgc_realtime.AxisChange();
                    zgc_realtime.Refresh();

                    //label_info1_load.Text = "";
                    label_info1_title.Text = "";

                    status_Label.Text = "选择的Beta值历史文件为: " + ofd_history.FileName;
                }
            }
            catch (Exception ex)
            {
                WriteLogFile(LogFile, "tool_btn_history_Click", ex.Message);
            }

        }

        /// <summary>
        /// Handles the Click event of the tool_btn_config control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btn_config_Click(object sender, EventArgs e)
        {
            try
            {
                frmConfig settingForm = new frmConfig();

                if (settingForm.ShowDialog() == DialogResult.OK) //设置面板确定
                {
                    LoadSettings(); //配置更新
                }
            }
            catch (Exception ex)
            {
                WriteLogFile(LogFile, "tool_btn_config_Click", ex.Message);
            }
        }

        /// <summary>
        /// Handles the Click event of the tool_btn_exit control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btn_quit_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Handles the Click event of the tool_btn_help control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btn_info_Click(object sender, EventArgs e)
        {
            MessageBox.Show("磨机监测系统 V3.0.0.1" + Environment.NewLine + "北京矿冶研究总院", "关于软件", MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        /// <summary>
        /// Handles the Click event of the btn_curve_search control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btn_curve_search_Click(object sender, EventArgs e)
        {
            DateTime startTime = dtp_curve_start.Value;    //查询起始时间
            DateTime endTime = dtp_curve_end.Value;    //查询截止时间
            if (endTime > startTime)
            {
                HisoryCurves = new Curve[CurveCount];
                DataTable dataTable = LoadHistoryData(startTime, endTime);
                dgv_data.DataSource = dataTable;
                dgv_data.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
                HisoryCurves = LoadHistoryCurves(dataTable);
                UpdateGraph(ref MasterPaneGraphHistory, ref zgc_history, HisoryCurves);
                status_Label.Text = string.Format("查询到从{0}到{1}共计{2}行数据！",
                                                  startTime.ToString("yyyy-MM-dd HH:mm:ss"),
                                                  endTime.AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss"),
                                                  dataTable.Rows.Count);
            }
        }

        /// <summary>
        /// Handles the Click event of the btn_curve_prev control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btn_curve_prev_Click(object sender, EventArgs e)
        {
            DateTime endTime = dtp_curve_start.Value;    //查询截止时间
            DateTime startTime = dtp_curve_start.Value - (dtp_curve_end.Value - dtp_curve_start.Value);    //查询起始时间
            if (endTime > startTime)
            {
                HisoryCurves = new Curve[CurveCount];
                DataTable dataTable = LoadHistoryData(startTime, endTime);
                dgv_data.DataSource = dataTable;
                dgv_data.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
                HisoryCurves = LoadHistoryCurves(dataTable);
                UpdateGraph(ref MasterPaneGraphHistory, ref zgc_history, HisoryCurves);
                dtp_curve_start.Value = startTime;
                dtp_curve_end.Value = endTime;
                status_Label.Text = string.Format("查询到从{0}到{1}共计{2}行数据！",
                                  startTime.ToString("yyyy-MM-dd HH:mm:ss"),
                                  endTime.AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss"),
                                  dataTable.Rows.Count);
            }
        }

        /// <summary>
        /// Handles the Click event of the btn_curve_next control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btn_curve_next_Click(object sender, EventArgs e)
        {
            DateTime startTime = dtp_curve_end.Value;    //查询起始时间
            DateTime endTime = dtp_curve_end.Value + (dtp_curve_end.Value - dtp_curve_start.Value);    //查询截止时间
            if (endTime > startTime)
            {
                HisoryCurves = new Curve[CurveCount];
                DataTable dataTable = LoadHistoryData(startTime, endTime);
                dgv_data.DataSource = dataTable;
                dgv_data.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
                HisoryCurves = LoadHistoryCurves(dataTable);
                UpdateGraph(ref MasterPaneGraphHistory, ref zgc_history, HisoryCurves);
                dtp_curve_start.Value = startTime;
                dtp_curve_end.Value = endTime;
                status_Label.Text = string.Format("查询到从{0}到{1}共计{2}行数据！",
                                  startTime.ToString("yyyy-MM-dd HH:mm:ss"),
                                  endTime.AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss"),
                                  dataTable.Rows.Count);
            }
        }

        /// <summary>
        /// Handles the Click event of the btn_data_search control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btn_data_search_Click(object sender, EventArgs e)
        {
            DateTime startTime = dtp_data_start.Value;    //查询起始时间
            DateTime endTime = dtp_data_end.Value;    //查询截止时间
            if (endTime > startTime)
            {
                DataTable dataTable = LoadHistoryData(startTime, endTime);
                dgv_data.DataSource = dataTable;
                dgv_data.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
                status_Label.Text = string.Format("查询到从{0}到{1}共计{2}行数据！",
                                                  startTime.ToString("yyyy-MM-dd HH:mm:ss"),
                                                  endTime.AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss"),
                                                  dataTable.Rows.Count);
            }
        }

        /// <summary>
        /// Handles the Click event of the btn_data_prev control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btn_data_prev_Click(object sender, EventArgs e)
        {
            DateTime endTime = dtp_data_start.Value;    //查询截止时间
            DateTime startTime = dtp_data_start.Value - (dtp_data_end.Value - dtp_data_start.Value);    //查询起始时间
            if (endTime > startTime)
            {
                DataTable dataTable = LoadHistoryData(startTime, endTime);
                dgv_data.DataSource = dataTable;
                dgv_data.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
                dtp_data_start.Value = startTime;
                dtp_data_end.Value = endTime;
                status_Label.Text = string.Format("查询到从{0}到{1}共计{2}行数据！",
                                  startTime.ToString("yyyy-MM-dd HH:mm:ss"),
                                  endTime.AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss"),
                                  dataTable.Rows.Count);
            }
        }

        /// <summary>
        /// Handles the Click event of the btn_data_next control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btn_data_next_Click(object sender, EventArgs e)
        {
            DateTime startTime = dtp_data_end.Value;    //查询起始时间
            DateTime endTime = dtp_data_end.Value + (dtp_data_end.Value - dtp_data_start.Value);    //查询截止时间
            if (endTime > startTime)
            {
                DataTable dataTable = LoadHistoryData(startTime, endTime);
                dgv_data.DataSource = dataTable;
                dgv_data.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
                dtp_data_start.Value = startTime;
                dtp_data_end.Value = endTime;
                status_Label.Text = string.Format("查询到从{0}到{1}共计{2}行数据！",
                                  startTime.ToString("yyyy-MM-dd HH:mm:ss"),
                                  endTime.AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss"),
                                  dataTable.Rows.Count);
            }
        }

        /// <summary>
        /// Handles the Click event of the menu_file_login control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void menu_file_login_Click(object sender, EventArgs e)
        {
            frmLogin frmLogin = new frmLogin();
            DialogResult dialogResult = frmLogin.ShowDialog();
            if (dialogResult.Equals(DialogResult.OK))
            {
                if (frmLogin.isPass)
                {
                    _currentOperator = frmLogin.currentOperator;
                    WriteLogFile(LogFile, "Login", string.Format("{0} Login!", _currentOperator.ModelName));

                    // 加载权限菜单
                    RightsMenuDataManager rmManager = new RightsMenuDataManager();
                    rmManager.LoadMenuRightsItem(msMain, _currentOperator.RightsCollection);
                    if (_isRunning)
                    {
                        menu_control_run.Enabled = false;
                        menu_control_history.Enabled = false;
                        menu_config_config.Enabled = false;
                        menu_file_quit.Enabled = false;
                    }
                    else
                    {
                        menu_control_stop.Enabled = false;
                    }
                    menu_file_login.Enabled = false;
                    menu_file_logoff.Enabled = true;
                    SynchroButton();

                    TimerLogoff.Start();
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the menu_file_logoff control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void menu_file_logoff_Click(object sender, EventArgs e)
        {
            WriteLogFile(LogFile, "Logoff", string.Format("{0} Logoff!", _currentOperator.ModelName));
            _currentOperator = null;
            LoadMenuRightsItem(msMain, LogoffMenuList);
            SynchroButton();
            menu_file_login.Enabled = true;
            menu_file_logoff.Enabled = false;
        }

        /// <summary>
        /// Handles the Click event of the menu_file_lockscreen control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void menu_file_lockscreen_Click(object sender, EventArgs e)
        {
            frmLockScreen frmLockScreen = new frmLockScreen(this, _currentOperator);
            frmLockScreen.Show();
        }

        /// <summary>
        /// Handles the Click event of the menu_file_quit control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void menu_file_quit_Click(object sender, EventArgs e)
        {
            btn_quit_Click(sender, e);
        }

        /// <summary>
        /// Handles the Click event of the menu_control_run control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void menu_control_run_Click(object sender, EventArgs e)
        {
            btn_run_Click(sender, e);
        }

        /// <summary>
        /// Handles the Click event of the menu_control_stop control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void menu_control_stop_Click(object sender, EventArgs e)
        {
            btn_stop_Click(sender, e);
        }

        /// <summary>
        /// Handles the Click event of the menu_control_history control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void menu_control_history_Click(object sender, EventArgs e)
        {
            btn_history_Click(sender, e);
        }

        /// <summary>
        /// Handles the Click event of the menu_control_clear control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void menu_control_clear_Click(object sender, EventArgs e)
        {
            ClearGraph();
            status_Label.Text = "清空波形";
        }

        /// <summary>
        /// Handles the Click event of the menu_config_config control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void menu_config_config_Click(object sender, EventArgs e)
        {
            btn_config_Click(sender, e);
        }

        /// <summary>
        /// Handles the Click event of the menu_config_user control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void menu_config_user_Click(object sender, EventArgs e)
        {
            frmRightsManager frmRightsManager = new frmRightsManager(this.msMain, this, _currentOperator)
            {
                ShowInTaskbar = false
            };
            frmRightsManager.ShowDialog();

            try
            {
                IBLL.IOperatorManager operatorManager = _bllFactory.BuildOperatorManager();
                _currentOperator = operatorManager.GetOperatorInfoByName(_currentOperator.ModelName,
                    _currentOperator.Password);
                // 加载权限菜单
                RightsMenuDataManager rmManager = new RightsMenuDataManager();
                rmManager.LoadMenuRightsItem(msMain, _currentOperator.RightsCollection);
                if (_isRunning)
                {
                    menu_control_run.Enabled = false;
                    menu_control_history.Enabled = false;
                    menu_config_config.Enabled = false;
                    menu_file_quit.Enabled = false;
                }
                else
                {
                    menu_control_stop.Enabled = false;
                }
                menu_file_login.Enabled = false;
                menu_file_logoff.Enabled = true;
                SynchroButton();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "读取失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Handles the Click event of the menu_config_password control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void menu_config_password_Click(object sender, EventArgs e)
        {
            frmOperatorManager frmModifyOperatorPassword = new frmOperatorManager(_currentOperator, this.msMain, true);
            frmModifyOperatorPassword.Show();
        }

        /// <summary>
        /// Handles the Click event of the menu_config_devices control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void menu_config_devices_Click(object sender, EventArgs e)
        {
            frmDevicesManager devicesForm = new frmDevicesManager();
            devicesForm.ShowDialog();
        }

        /// <summary>
        /// Handles the Click event of the menu_config_parameters control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void menu_config_parameters_Click(object sender, EventArgs e)
        {
            frmParametersManager parametersForm = new frmParametersManager();
            parametersForm.ShowDialog();
        }

        /// <summary>
        /// Handles the Click event of the menu_help_about control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void menu_help_about_Click(object sender, EventArgs e)
        {
            btn_info_Click(sender, e);
        }

        /// <summary>
        /// Handles the CellFormatting event of the dgv_data control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridViewCellFormattingEventArgs"/> instance containing the event data.</param>
        private void dgv_data_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value != null)
            {
                if (dgv_data.Columns[e.ColumnIndex].HeaderText.Contains("时间") ||
                    dgv_data.Columns[e.ColumnIndex].HeaderText.Contains("Time"))
                {

                    e.Value = Convert.ToDateTime(e.Value).ToString("yyyy:MM:dd HH:mm:ss");
                }
                else
                {
                    e.Value = string.Format("{0:F2}", e.Value);
                }
            }
        }
        #endregion

    }
}