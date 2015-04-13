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
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.IO.Ports;
using System.Net.Sockets;
using System.Timers;
using ExpertSystem;
using IBLL;
using Modbus.Device;
using Modbus.Data;
using Model.Control;
using Model.Modbus;
using Model;
using Utility;
using ZedGraph;
using OptimalControl.Common;
using Rule = Model.Control.Rule;

namespace OptimalControl.Forms
{

    #region 结构体

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
        private string _logFile = AppDomain.CurrentDomain.BaseDirectory + "cache\\event.log"; //错误日志文件

        /// <summary>
        /// The logoff time
        /// </summary>
        private int _logoffTime = 900; //注销时间，默认900s

        /// <summary>
        /// The Logoff timer
        /// </summary>
        private System.Timers.Timer _timerLogoff = new System.Timers.Timer(900000);

        /// <summary>
        /// The real timer
        /// </summary>
        private System.Timers.Timer _timerRealtime = new System.Timers.Timer(2000);

        /// <summary>
        /// The real timer flag
        /// </summary>
        private bool _realTimerFlag = false;

        private bool _updateGraphFlag = false;

        private bool _execteRulesFlag = false;

        /// <summary>
        /// The MessageFilter
        /// </summary>
        private MessageFilter _myFilter = new MessageFilter();

        /// <summary>
        /// The logoff menu list
        /// </summary>
        private string[] _logoffMenuList =
        {
            "menu_file_quit", "menu_file_lockscreen", "menu_control_run", "menu_control_stop", "menu_config_config",
            "menu_config_password", "menu_config_user", "menu_config_devices", "menu_config_parameters",
            "menu_control_history", "menu_control_clear"
        };

        /// <summary>
        /// The real timer interval
        /// </summary>
        private int _realTimerInterval = 2000; //设置定时器间隔，默认为2000ms

        /// <summary>
        /// The modbus rtu slave thread
        /// </summary>
        private Thread _modbusRtuSlaveThread;

        /// <summary>
        /// The modbus rtu slave
        /// </summary>
        private ModbusSlave _modbusRtuSlave;

        /// <summary>
        /// The modbus rtu device
        /// </summary>
        private ModbusRtuDevice _modbusRtuDevice = new ModbusRtuDevice();

        /// <summary>
        /// The modbus rtu parameters
        /// </summary>
        private Variable[] _modbusRtuParameters = new Variable[0];

        /// <summary>
        /// The modbus rtu slave created flag
        /// </summary>
        private bool _modbusRtuSlaveCreated;

        /// <summary>
        /// The modbus rtu slave updated
        /// </summary>
        private bool _modbusRtuSlaveUpdated;

        /// <summary>
        /// The devices
        /// </summary>
        private Device[] _devices = new Device[0];

        /// <summary>
        /// The SQL to save parameter
        /// </summary>
        private string[] _sqlSaveData =
        {
            "INSERT INTO @DataTable (Time, ParameterName, Value, DeviceID) VALUES ",
            ",",
            "('@Time', '@ParameterName', '@Value', '@DeviceID')"
        };

        /// <summary>
        /// The SQL get history data
        /// </summary>
        private string _sqlGetHistoryData =
            "SELECT * FROM @DataTable WHERE ParameterName='@ParameterName' AND DeviceID='@DeviceID' AND Time >= '@StartTime' AND Time <= '@EndTime'";

        private string _sqlGetHistoryData1 =
            "DECLARE @sql1 varchar(8000); SELECT @sql1 = ISNULL(@sql1 + '],[' , '') + [Name] FROM [@CurvesTable] GROUP BY [Name]; SET @sql1 = '[' + @sql1 + ']';";

        private string _sqlGetHistoryData2 =
            "DECLARE @sql2 varchar(8000); SELECT @sql2 = ISNULL(@sql2 + ''',MAX([' , '') + [Name] +']) AS ''' + [Name]  FROM [@CurvesTable] GROUP BY [Name]; SET @sql2 = 'MAX([' + @sql2 + '''';";

        private string _sqlGetHistoryData3 =
            "EXEC ('SELECT [Time] AS ''时间'',' + @sql2 + ' 	FROM (SELECT * FROM [@DataTable] WHERE [Time] &gt;= ''@StartTime'' AND [Time] &lt; ''@EndTime'') AS a PIVOT (MAX([Value]) FOR [ParameterName] IN (' + @sql1 + ')) b GROUP BY [Time] ORDER BY [Time]');";

        private string _sqlGetParameters =
            "SELECT * FROM @ParametersTable WHERE DeviceID = @DeviceID";

        private string _sqlGetRules = "SELECT * FROM @RulesTable";

        /// <summary>
        /// The data table name
        /// </summary>
        private string _dataTable = "Data";

        private string _curvesTable = "Curve";

        private string _parametersTable = "Parameter";

        private string _rulesTable = "Rules";
        /// <summary>
        /// The DCS name displayed in list
        /// </summary>
        private string _dcsName = "磨机工况信息";

        /// <summary>
        /// The master pane graph
        /// </summary>
        private MasterPane _masterPaneGraphRealtime = new MasterPane();

        private MasterPane _masterPaneGraphHistory = new MasterPane();

        /// <summary>
        /// The rotator
        /// </summary>
        private ColorSymbolRotator _rotator = new ColorSymbolRotator();

        /// <summary>
        /// The curve count
        /// </summary>
        private int _curveCount = 6;

        /// <summary>
        /// The curve list
        /// </summary>
        private int[] _curveList = {3, 3};

        /// <summary>
        /// The proportion
        /// </summary>
        private float[] _proportion = {2, 1};

        /// <summary>
        /// The curves
        /// </summary>
        private Curve[] _curves = new Curve[6];

        private Curve[] _hisoryCurves;

        /// <summary>
        /// The data list length
        /// </summary>
        private int _dataListLength = 720;

        private DataTable _ruleDataTable;
        private DataTable _parameterDataTable;

        List<Rule> _rules = new List<Rule>();

        private int _defaultControlPeriod = 30;

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

                _logFile = ConfigAppSettings.GetSettingString("LogFile", _logFile);

                //label_info1_info.Text = "";
                //label_info1_load.Text = "";
                //label_info1_voltage.Text = "";
                status_Label.Text = "";

                _masterPaneGraphRealtime = zgc_realtime.MasterPane;
                zgc_realtime.ContextMenuBuilder += ZgcContextMenuBuilder;

                _masterPaneGraphHistory = zgc_history.MasterPane;
                zgc_history.ContextMenuBuilder += ZgcContextMenuBuilder;

                LoadSettings();

                // Remove the default pane that comes with the ZedGraphControl.MasterPane
                _masterPaneGraphRealtime.PaneList.Clear();
                //MasterPaneGraphRealtime.Fill = new Fill(Color.White, Color.MediumSlateBlue, 45.0F);
                _masterPaneGraphRealtime.Title.IsVisible = false;
                _masterPaneGraphRealtime.Margin.All = 10;
                _masterPaneGraphRealtime.InnerPaneGap = 0;
                //MasterPaneGraphRealtime.Legend.IsVisible = true;
                //MasterPaneGraphRealtime.Legend.Position = LegendPos.TopCenter;
                UpdateGraph(ref _masterPaneGraphRealtime, ref zgc_realtime, _curves);

                _masterPaneGraphHistory.PaneList.Clear();
                //MasterPaneGraphHistory.Fill = new Fill(Color.White, Color.MediumSlateBlue, 45.0F);
                _masterPaneGraphHistory.Title.IsVisible = false;
                _masterPaneGraphHistory.Margin.All = 10;
                _masterPaneGraphHistory.InnerPaneGap = 0;
                //MasterPaneGraphHistory.Legend.IsVisible = true;
                //MasterPaneGraphHistory.Legend.Position = LegendPos.TopCenter;
                UpdateGraph(ref _masterPaneGraphHistory, ref zgc_realtime, _curves);

                dtp_curve_start.Value = DateTime.Today;
                dtp_curve_end.Value = DateTime.Today.AddDays(1);
                dtp_data_start.Value = DateTime.Today;
                dtp_data_end.Value = DateTime.Today.AddDays(1);

                if (_isPass)
                {
                    //创建工厂类实例
                    _bllFactory = new BLLFactory.BLLFactory();
                    WriteLogFile(_logFile, "Login", string.Format("{0} Login!", _currentOperator.Name));

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
                    LoadMenuRightsItem(msMain, _logoffMenuList);
                    menu_control_stop.Enabled = false;
                    menu_file_login.Enabled = true;
                    menu_file_logoff.Enabled = false;
                    SynchroButton();
                }

                Application.AddMessageFilter(_myFilter);

                _timerRealtime = new System.Timers.Timer(_realTimerInterval);
                _timerRealtime.Elapsed += TimerRealtimeElapsed;

                _timerLogoff = new System.Timers.Timer(_logoffTime*1000);
                _timerLogoff.Elapsed += TimerLogoffElapsed;
                _timerLogoff.Start();
            }
            catch (Exception ex)
            {
                WriteLogFile(_logFile, "Initialization", ex.Message);
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
                string DevicesTable = ConfigAppSettings.GetSettingString("DevicesTable", "Device");

                _sqlSaveData[0] = ConfigAppSettings.GetSettingString("SQLSaveData0", ",");
                _sqlSaveData[1] = ConfigAppSettings.GetSettingString("SQLSaveData1",
                    "INSERT INTO @DataTable (Time, ParameterName, Value, DeviceID) VALUES ");
                _sqlSaveData[2] = ConfigAppSettings.GetSettingString("SQLSaveData2",
                    "('@Time', '@ParameterName', '@Value', '@DeviceID')");
                _sqlGetHistoryData = ConfigAppSettings.GetSettingString("SQLGetHistoryData",
                    "SELECT * FROM @DataTable WHERE ParameterName='@ParameterName' AND DeviceID='@DeviceID' AND Time >= '@StartTime' AND Time <= '@EndTime'");

                _sqlGetHistoryData1 = ConfigAppSettings.GetSettingString("SQLGetHistoryData1",
                    "DECLARE @sql1 varchar(8000); SELECT @sql1 = ISNULL(@sql1 + '],[' , '') + [Name] FROM [@CurvesTable] GROUP BY [Name]; SET @sql1 = '[' + @sql1 + ']';");
                _sqlGetHistoryData2 = ConfigAppSettings.GetSettingString("SQLGetHistoryData2",
                    "DECLARE @sql2 varchar(8000); SELECT @sql2 = ISNULL(@sql2 + ''',MAX([' , '') + [Name] +']) AS ''' + [Name]  FROM [@CurvesTable] GROUP BY [Name]; SET @sql2 = 'MAX([' + @sql2 + '''';");
                _sqlGetHistoryData3 = ConfigAppSettings.GetSettingString("SQLGetHistoryData3",
                    "EXEC ('SELECT [Time] AS ''时间'',' + @sql2 + ' 	FROM (SELECT * FROM [@DataTable] WHERE [Time] &gt;= ''@StartTime'' AND [Time] &lt; ''@EndTime'') AS a PIVOT (MAX([Value]) FOR [ParameterName] IN (' + @sql1 + ')) b GROUP BY [Time] ORDER BY [Time]');");

                _sqlGetParameters = ConfigAppSettings.GetSettingString("SQLGetParameters",
                    "SELECT * FROM @ParametersTable WHERE DeviceID = @DeviceID");

                _sqlGetRules = ConfigAppSettings.GetSettingString("SQLGetRules", "SELECT * FROM @RulesTable");

                _dataTable = ConfigAppSettings.GetSettingString("DataTable", "Data");

                _curvesTable = ConfigAppSettings.GetSettingString("CurvesTable", "Curve");

                _parametersTable = ConfigAppSettings.GetSettingString("ParametersTable", "Parameter");

                _rulesTable = ConfigAppSettings.GetSettingString("RulesTable", "Rules");

                _dcsName = ConfigAppSettings.GetSettingString("DCSName", "磨机工况信息");

                DataTable deviceDataTable = SQLHelper.ExcuteDataTable(SQLHelper.ConnectionStringLocalTransaction,
                    GetDevicesCommand(SQLGetDevices, DevicesTable));

                _devices = new Device[deviceDataTable.Rows.Count];

                _parameterDataTable = SQLHelper.ExcuteDataTable(SQLHelper.ConnectionStringLocalTransaction,
                    GetParametersCommand(_sqlGetParameters, _parametersTable));


                for (int deviceID = 0; deviceID < deviceDataTable.Rows.Count; deviceID++)
                {
                    _devices[deviceID] = new Device
                    {
                        Id = Convert.ToInt32(deviceDataTable.Rows[deviceID][0]),
                        Name = Convert.ToString(deviceDataTable.Rows[deviceID][1]),
                        State = Convert.ToBoolean(deviceDataTable.Rows[deviceID][2]),
                        SyncState = Convert.ToBoolean(deviceDataTable.Rows[deviceID][3]),
                        ModbusTcpDevice = new ModbusTcpDevice
                        {
                            IpAddress = Convert.ToString(deviceDataTable.Rows[deviceID][4]),
                            PortName = Convert.ToInt32(deviceDataTable.Rows[deviceID][5]),
                            UnitId = Convert.ToByte(deviceDataTable.Rows[deviceID][6]),
                            TcpClient = new TcpClient()
                        },
                        ModbusTcpMasterCreated = false,
                        ModbusTcpMasterUpdated = false
                    };

                    DataRow[] parameterDataTable = _parameterDataTable.Select(string.Format("DeviceID='{0}'",
                        Convert.ToString(deviceDataTable.Rows[deviceID][0])));
                    
                    _devices[deviceID].Variables = new Variable[parameterDataTable.Length];
                    for (int index = 0; index < parameterDataTable.Length; index++)
                    {
                        _devices[deviceID].Variables[index] = new Variable
                        {
                            Id = Convert.ToInt32(parameterDataTable[index][0]),
                            Name = Convert.ToString(parameterDataTable[index][1]),
                            Address = Convert.ToUInt16(parameterDataTable[index][2]),
                            Ratio = Math.Round(Convert.ToDouble(parameterDataTable[index][3]), 2),
                            Limit = new Variable.VariableLimit()
                            {
                                UpperLimit = Convert.ToString(parameterDataTable[index][4]) != ""
                                    ? Math.Round(Convert.ToDouble(parameterDataTable[index][4]), 2)
                                    : -1,
                                LowerLimit = Convert.ToString(parameterDataTable[index][5]) != ""
                                    ? Math.Round(Convert.ToDouble(parameterDataTable[index][5]), 2)
                                    : -1,
                                UltimateUpperLimit = Convert.ToString(parameterDataTable[index][6]) != ""
                                    ? Math.Round(Convert.ToDouble(parameterDataTable[index][6]), 2)
                                    : -1,
                                UltimateLowerLimit = Convert.ToString(parameterDataTable[index][7]) != ""
                                    ? Math.Round(Convert.ToDouble(parameterDataTable[index][7]), 2)
                                    : -1,
                            },
                            ControlPeriod = Convert.ToString(parameterDataTable[index][8]) != ""
                                ? Convert.ToInt32(parameterDataTable[index][8])
                                : -1,
                            OperateDelay = Convert.ToString(parameterDataTable[index][9]) != ""
                                ? Convert.ToInt32(parameterDataTable[index][9])
                                : -1,
                            DeviceID = Convert.ToUInt32(parameterDataTable[index][10]),
                        };
                    }
                }
                DataRow[] modbusRtuParaDataTable = _parameterDataTable.Select(string.Format("DeviceID='{0}'",0));
                _modbusRtuParameters = new Variable[modbusRtuParaDataTable.Length];
                for (int index = 0; index < modbusRtuParaDataTable.Length; index++)
                {
                    _modbusRtuParameters[index] = new Variable
                    {
                        Name = Convert.ToString(modbusRtuParaDataTable[index][1]),
                        Address = Convert.ToUInt16(modbusRtuParaDataTable[index][2]),
                        Ratio = Math.Round(Convert.ToDouble(modbusRtuParaDataTable[index][3]), 2),
                        Limit = new Variable.VariableLimit()
                        {
                            UpperLimit = Convert.ToString(modbusRtuParaDataTable[index][4]) != ""
                                ? Math.Round(Convert.ToDouble(modbusRtuParaDataTable[index][4]), 2)
                                : -1,
                            LowerLimit = Convert.ToString(modbusRtuParaDataTable[index][5]) != ""
                                ? Math.Round(Convert.ToDouble(modbusRtuParaDataTable[index][5]), 2)
                                : -1,
                            UltimateUpperLimit = Convert.ToString(modbusRtuParaDataTable[index][6]) != ""
                                ? Math.Round(Convert.ToDouble(modbusRtuParaDataTable[index][6]), 2)
                                : -1,
                            UltimateLowerLimit = Convert.ToString(modbusRtuParaDataTable[index][7]) != ""
                                ? Math.Round(Convert.ToDouble(modbusRtuParaDataTable[index][7]), 2)
                                : -1,
                        },
                        ControlPeriod = Convert.ToString(modbusRtuParaDataTable[index][8]) != ""
                            ? Convert.ToInt32(modbusRtuParaDataTable[index][8])
                            : -1,
                        OperateDelay = Convert.ToString(modbusRtuParaDataTable[index][9]) != ""
                            ? Convert.ToInt32(modbusRtuParaDataTable[index][9])
                            : -1,
                        DeviceID = Convert.ToUInt32(modbusRtuParaDataTable[index][10]),
                    };
                }

                string tempString = ConfigAppSettings.GetValue("LogoffMenulist").Trim();
                if (tempString.Length > 0)
                {
                    _logoffMenuList = tempString.Split(',');
                }
                _logoffTime = ConfigAppSettings.GetSettingInt("LogoffTime", _logoffTime);

                _realTimerInterval = ConfigAppSettings.GetSettingInt("RealTime", _realTimerInterval); //时间间隔

                _timerRealtime.Interval = _realTimerInterval;

                _modbusRtuDevice.SerialPortObject = new SerialPort
                    (
                    ConfigAppSettings.GetSettingString("ModbusRTUPortName", "COM1"),
                    ConfigAppSettings.GetSettingInt("ModbusRTUBaudRate", 19200),
                    Parity.None,
                    ConfigAppSettings.GetSettingInt("ModbusRTUDataBits", 8),
                    (StopBits) ConfigAppSettings.GetSettingSingle("ModbusRTUStopBits", 1)
                    );

                _modbusRtuDevice.UnitId = ConfigAppSettings.GetSettingByte("ModbusRTUDeviceID", 1);

                _masterPaneGraphRealtime.Title.Text = ConfigAppSettings.GetSettingString("MasterTitle",
                    "My MasterPane Title");
                _masterPaneGraphRealtime.Title.FontSpec.Size = ConfigAppSettings.GetSettingSingle("MasterTitleSize", 12);

                tempString = ConfigAppSettings.GetValue("CurveList").Trim();
                if (tempString.Length > 0)
                {
                    string[] tempstrings = tempString.Split(',');
                    _curveList = new int[tempstrings.Length];
                    for (int index = 0; index < tempstrings.Length; index++)
                    {
                        _curveList[index] = Convert.ToInt32(tempstrings[index]);
                    }
                }

                tempString = ConfigAppSettings.GetValue("Proportion").Trim();
                if (tempString.Length > 0)
                {
                    string[] tempstrings = tempString.Split(',');
                    _proportion = new float[tempstrings.Length];
                    for (int index = 0; index < tempstrings.Length; index++)
                    {
                        _proportion[index] = Convert.ToSingle(tempstrings[index]);
                    }
                }

                _dataListLength = ConfigAppSettings.GetSettingInt("DataListLength", 720);

                string SQLGetCurves = ConfigAppSettings.GetSettingString("SQLGetCurves", "SELECT * FROM @CurvesTable");
                DataTable curvesDataTable = SQLHelper.ExcuteDataTable(SQLHelper.ConnectionStringLocalTransaction,
                    GetCurvesCommand(SQLGetCurves, _curvesTable));

                _curveCount = curvesDataTable.Rows.Count;
                _curves = new Curve[_curveCount];
                for (int index = 0; index < _curveCount; index++)
                {
                    _curves[index] = new Curve
                    {
                        Id = Convert.ToInt32(curvesDataTable.Rows[index][0]),
                        Name = Convert.ToString(curvesDataTable.Rows[index][1]),
                        DataList = new PointPairList(),
                        DeviceId = Convert.ToInt32(curvesDataTable.Rows[index][2]),
                        Address = Convert.ToUInt16(curvesDataTable.Rows[index][3]),
                        LineColor = string.IsNullOrEmpty(Convert.ToString(curvesDataTable.Rows[index][4]))
                            ? _rotator.NextColor
                            : Color.FromName(Convert.ToString(curvesDataTable.Rows[index][4])),
                        LineType = string.IsNullOrEmpty(Convert.ToString(curvesDataTable.Rows[index][5])) ||
                                   Convert.ToBoolean(curvesDataTable.Rows[index][5]),
                        LineWidth = string.IsNullOrEmpty(Convert.ToString(curvesDataTable.Rows[index][6]))
                            ? 2
                            : Convert.ToSingle(curvesDataTable.Rows[index][6]),
                        SymbolType = _rotator.NextSymbol,
                        SymbolSize = string.IsNullOrEmpty(Convert.ToString(curvesDataTable.Rows[index][8]))
                            ? 4
                            : Convert.ToSingle(curvesDataTable.Rows[index][8]),
                        XTitle = Convert.ToString(curvesDataTable.Rows[index][9]),
                        YTitle = Convert.ToString(curvesDataTable.Rows[index][10]),
                        YMax = Convert.ToDouble(curvesDataTable.Rows[index][11]),
                        YMin = Convert.ToDouble(curvesDataTable.Rows[index][12])
                    };
                    if (!(string.IsNullOrEmpty(Convert.ToString(curvesDataTable.Rows[index][7]))))
                    {
                        switch (Convert.ToString(curvesDataTable.Rows[index][7]))
                        {
                            case "Diamond":
                                _curves[index].SymbolType = SymbolType.Diamond;
                                break;
                            case "Circle":
                                _curves[index].SymbolType = SymbolType.Circle;
                                break;
                            case "Square":
                                _curves[index].SymbolType = SymbolType.Square;
                                break;
                            case "Star":
                                _curves[index].SymbolType = SymbolType.Star;
                                break;
                            case "Triangle":
                                _curves[index].SymbolType = SymbolType.Triangle;
                                break;
                            case "Plus":
                                _curves[index].SymbolType = SymbolType.Plus;
                                break;
                            case "None":
                                _curves[index].SymbolType = SymbolType.None;
                                break;
                        }
                    }
                }
                UpdateRules();
            }
            catch (Exception ex)
            {
                WriteLogFile(_logFile, "LoadSettings", ex.Message);
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
        /// Gets the parameters command.
        /// </summary>
        /// <param name="sqlCmd">The SQL command.</param>
        /// <param name="table">The table.</param>
        /// <returns>
        /// Command
        /// </returns>
        private string GetParametersCommand(string sqlCmd, string table)
        {
            string sql = sqlCmd;
            sql = sql.Replace("@ParametersTable", table);
            sql = sql.Replace("@DeviceID", "1");
            sql = sql.Replace("DeviceID", "1");
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
        private string GetSaveDataCommand(string[] sqlCmd, string table, string time, Variable[] parameters)
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
        private string GetHistoryDataCommand(string sqlCmd, string dataTable, string curveTable, DateTime statrTime,
            DateTime endTime)
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
        private string GetHistoryDataCommand(string sqlCmd, string datatable, string parameterName, int deviceID,
            DateTime statrTime, DateTime endTime)
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
        /// Gets the rules command.
        /// </summary>
        /// <param name="sqlCmd">The SQL command.</param>
        /// <param name="table">The table.</param>
        /// <returns>
        /// Command
        /// </returns>
        private string GetRulesCommand(string sqlCmd, string table)
        {
            string sql = sqlCmd;
            sql = sql.Replace("@RulesTable", table);
            return sql;
        }

        /// <summary>
        /// 建立Modbus监听.
        /// </summary>
        /// <returns>
        /// Modbus RTU Listener是否创建成功
        /// </returns>
        private bool ModbusRTUCreatListener(ModbusRtuDevice modbusRtuDevice)
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
                    _modbusRtuSlave = ModbusSerialSlave.CreateRtu(modbusRtuDevice.UnitId,
                        modbusRtuDevice.SerialPortObject);
                    _modbusRtuSlave.ModbusSlaveRequestReceived += ModbusRTU_Request_Event;
                    _modbusRtuSlave.DataStore = DataStoreFactory.CreateDefaultDataStore();
                    _modbusRtuSlave.DataStore.DataStoreWrittenTo += ModbusRTU_DataStoreWriteTo_Event;

                    _modbusRtuSlaveThread = new Thread(_modbusRtuSlave.Listen);
                    _modbusRtuSlaveThread.Start();
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
            _modbusRtuSlaveUpdated = true;
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
                if (_modbusRtuSlaveCreated)
                {
                    for (int paraIndex = 0; paraIndex < _modbusRtuParameters.Length; paraIndex++)
                    {
                        ushort[] register = new ushort[2];
                        try
                        {
                            //读寄存器
                            register[0] =
                                _modbusRtuSlave.DataStore.HoldingRegisters[_modbusRtuParameters[paraIndex].Address];
                            register[1] =
                                _modbusRtuSlave.DataStore.HoldingRegisters[_modbusRtuParameters[paraIndex].Address + 1];
                        }
                        catch (Exception)
                        {
                            ModbusRTUStopComm(); //处理连接错误，重试连接
                            ModbusRTUCreatListener(_modbusRtuDevice);
                            //WriteLogFile(LogFile, "ModbusRTU->ModbusTCP", ex.Message);
                            continue;
                        }
                        for (int deviceIndex = 0; deviceIndex < _devices.Length; deviceIndex++)
                        {
                            if (!_devices[deviceIndex].State ||
                                !_devices[deviceIndex].SyncState ||
                                !_devices[deviceIndex].ModbusTcpMasterCreated)
                                continue;
                            try
                            {
                                //读寄存器
                                _devices[deviceIndex].ModbusTcpDevice.ModbusTcpMaster.WriteMultipleRegisters(
                                    _devices[deviceIndex].ModbusTcpDevice.UnitId,
                                    (ushort) (_modbusRtuParameters[paraIndex].Address - 1),
                                    register);
                            }
                            catch (Exception)
                            {
                                ModbusTCPStopComm(_devices[deviceIndex].ModbusTcpDevice); //处理连接错误，重试连接
                                ModbusTCPCreateClient(ref _devices[deviceIndex].ModbusTcpDevice);
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
                        _modbusRtuParameters[paraIndex].Value = value*_modbusRtuParameters[paraIndex].Ratio;
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLogFile(_logFile, "ModbusRTUGetValue", ex.Message);
            }
        }

        /// <summary>
        /// Stops the communication.
        /// </summary>
        private void ModbusRTUStopComm() //通讯停止
        {
            try
            {
                if (_modbusRtuSlaveCreated)
                {
                    _modbusRtuSlave.ModbusSlaveRequestReceived -= ModbusRTU_Request_Event;
                    _modbusRtuSlave.DataStore.DataStoreWrittenTo -= ModbusRTU_DataStoreWriteTo_Event;
                    _modbusRtuDevice.SerialPortObject.Close();
                    _modbusRtuSlave.Dispose();
                    _modbusRtuSlaveThread.Abort();
                    _modbusRtuSlaveCreated = false;
                }
            }
            catch (Exception ex)
            {
                WriteLogFile(_logFile, "StopModbusRTUComm", ex.Message);
            }
        }

        /// <summary>
        /// 建立Modbus TCP 客户端.
        /// </summary>
        /// <param name="modbusTcpDevice">The modbus TCP device.</param>
        /// <returns>
        /// Modbus TCP 客户端是否创建成功
        /// </returns>
        private bool ModbusTCPCreateClient(ref ModbusTcpDevice modbusTcpDevice)
        {
            try
            {
                if (string.IsNullOrEmpty(modbusTcpDevice.IpAddress) || modbusTcpDevice.PortName.Equals(0))
                {
                    return false;
                }
                modbusTcpDevice.TcpClient = new TcpClient(modbusTcpDevice.IpAddress, modbusTcpDevice.PortName);

                modbusTcpDevice.ModbusTcpMaster = ModbusIpMaster.CreateIp(modbusTcpDevice.TcpClient);
                // create Modbus TCP Master with the tcp client
                modbusTcpDevice.ModbusTcpMaster.Transport.ReadTimeout = 1000;
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
                for (int paraIndex = 0; paraIndex < device.Variables.Length; paraIndex++)
                {
                    ushort[] register;
                    try
                    {
                        //读寄存器
                        register =
                            device.ModbusTcpDevice.ModbusTcpMaster.ReadHoldingRegisters(
                                device.ModbusTcpDevice.UnitId,
                                (ushort) (device.Variables[paraIndex].Address - 1), 2);
                        device.ModbusTcpMasterUpdated = true;
                    }
                    catch (Exception)
                    {
                        device.ModbusTcpMasterCreated = !ModbusTCPStopComm(device.ModbusTcpDevice); //处理连接错误，重试连接
                        device.ModbusTcpMasterCreated = ModbusTCPCreateClient(ref device.ModbusTcpDevice);
                        continue;
                    }

                    if (_modbusRtuSlaveCreated)
                    {
                        try
                        {
                            _modbusRtuSlave.DataStore.HoldingRegisters[device.Variables[paraIndex].Address] =
                                register[0];
                            _modbusRtuSlave.DataStore.HoldingRegisters[device.Variables[paraIndex].Address + 1] =
                                register[1];
                        }
                        catch (Exception)
                        {
                            ModbusRTUStopComm(); //处理连接错误，重试连接
                            ModbusRTUCreatListener(_modbusRtuDevice);
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
                    device.Variables[paraIndex].Value = value*device.Variables[paraIndex].Ratio;
                }
            }
            catch (Exception ex)
            {
                WriteLogFile(_logFile, "ModbusTCPGetValue", ex.Message);
            }
        }

        /// <summary>
        /// Stops the communication.
        /// </summary>
        /// <param name="modbusTcpDevice">The modbus TCP device.</param>
        /// <returns></returns>
        private bool ModbusTCPStopComm(ModbusTcpDevice modbusTcpDevice) //通讯停止
        {
            try
            {
                if (modbusTcpDevice.ModbusTcpMaster != null)
                    modbusTcpDevice.ModbusTcpMaster.Dispose();
                if (modbusTcpDevice.TcpClient != null)
                    modbusTcpDevice.TcpClient.Close();
                return true;
            }
            catch (Exception ex)
            {
                WriteLogFile(_logFile, "StopModbusTCPComm", ex.Message);
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
                ListViewGroup[] group = new ListViewGroup[_devices.Length + 1];
                for (int index = 0; index < (_devices.Length); index++)
                {
                    group[index] = new ListViewGroup(_devices[index].Name, HorizontalAlignment.Center);
                }
                group[_devices.Length] = new ListViewGroup(_dcsName, HorizontalAlignment.Center);
                listview_parainfo.Groups.AddRange(group);

                for (int deviceIndex = 0; deviceIndex < (_devices.Length); deviceIndex++)
                {
                    if (_devices[deviceIndex].State)
                    {
                        listview_parainfo.Items.Add(
                            new ListViewItem(
                                new string[]
                                {
                                    "状态",
                                    _devices[deviceIndex].ModbusTcpMasterCreated ? "在线" : "离线"
                                },
                                group[deviceIndex]
                                )
                            {
                                BackColor = _devices[deviceIndex].ModbusTcpMasterCreated ? Color.LightGreen : Color.Pink,
                                Font = new Font(DefaultFont.FontFamily, 9)
                            }
                            );

                        ListViewItem[] items = new ListViewItem[_devices[deviceIndex].Variables.Length];
                        for (int paraIndex = 0; paraIndex < _devices[deviceIndex].Variables.Length; paraIndex++)
                        {
                            items[paraIndex] =
                                new ListViewItem(
                                    new string[]
                                    {
                                        _devices[deviceIndex].Variables[paraIndex].Name,
                                        _devices[deviceIndex].Variables[paraIndex].Value.ToString("F02")
                                    },
                                    group[deviceIndex])
                                {BackColor = (paraIndex%2 == 0 ? Color.White : Color.Cyan)};
                        }
                        listview_parainfo.Items.AddRange(items);
                    }
                }

                if (_modbusRtuSlaveCreated)
                {
                    ListViewItem[] items = new ListViewItem[_modbusRtuParameters.Length];

                    for (int paraIndex = 0; paraIndex < _modbusRtuParameters.Length; paraIndex++)
                    {
                        items[paraIndex] =
                            new ListViewItem(
                                new string[]
                                {
                                    _modbusRtuParameters[paraIndex].Name,
                                    _modbusRtuParameters[paraIndex].Value.ToString("F02")
                                },
                                group[_devices.Length])
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
                WriteLogFile(_logFile, "RefreshRegisterList", ex.Message);
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
        private int SaveParameter(Variable[] parameters, DateTime time)
        {
            string sql = GetSaveDataCommand(_sqlSaveData, _dataTable,
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
        private void UpdateGraph(ref MasterPane masterPane, ref ZedGraphControl zgc, Curve[] curves)
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
                for (int index = 0; index < _curveCount; index++)
                {
                    if (index == 0)
                    {
                        masterPane.Add(CreatGraphPane(curves[index], GraphPaneType.First));
                    }
                    else if (index == _curveCount - 1)
                    {
                        masterPane.Add(CreatGraphPane(curves[index], GraphPaneType.Last));
                    }
                    else
                    {
                        bool graphPaneCreated = false;
                        for (int i = 0; i < _curveList.Length; i++)
                        {
                            int temp = 0;
                            for (int j = 0; j < i; j++)
                            {
                                temp += _curveList[j];
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
                    masterPane.SetLayout(g, false, _curveList, _proportion);
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
                WriteLogFile(_logFile, "UpdateGraph", ex.Message);
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
        private GraphPane CreatGraphPane(Curve curve, GraphPaneType graphPaneType)
        {
            // Create a new graph with topLeft at (40,40) and size 600x400
            GraphPane graphPane = new GraphPane(new Rectangle(10, 10, 600, 400), curve.Name, curve.XTitle,
                curve.YTitle)
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
                    graphPane.YAxis.Scale.Min = curve.YMin;
                    if (curve.YMax > curve.YMin)
                    {
                        graphPane.YAxis.Scale.Max = curve.YMax;
                    }
                    LineItem tmpCurve = graphPane.AddCurve(curve.Name, curve.DataList, curve.LineColor,
                        curve.SymbolType);
                    tmpCurve.Symbol.Fill = new Fill(curve.LineColor);
                    tmpCurve.Symbol.Size = curve.SymbolSize;
                    tmpCurve.Line.IsVisible = curve.LineType;
                    tmpCurve.Line.Width = curve.LineWidth;
                    tmpCurve.Line.StepType = StepType.ForwardStep;
                }
            }
            catch (Exception ex)
            {
                WriteLogFile(_logFile, "CreatGraphPane", ex.Message);
            }
            return graphPane;
        }

        private void UpdateRules()
        {
            _ruleDataTable = SQLHelper.ExcuteDataTable(SQLHelper.ConnectionStringLocalTransaction,
                GetRulesCommand(_sqlGetRules, _rulesTable));
            UpdateRulesGrid(_ruleDataTable, "1=1");
            status_Label.Text = string.Format("查询到 {0} 行数据", _ruleDataTable.Rows.Count);
        }

        private void UpdateRulesGrid(DataTable dataTable, string filter)
        {
            DataRow[] data = dataTable.Select(filter);
            DataTable table = dataTable.Clone();
            foreach (DataRow row in data)
            {
                table.Rows.Add(row.ItemArray);
            }

            dgv_oc_rules.DataSource = table;
            foreach (DataGridViewColumn column in dgv_oc_rules.Columns)
            {
                switch (column.HeaderText) //更改列名
                {
                    case "Id":
                        column.HeaderText = "序号";
                        break;
                    case "Name":
                        column.HeaderText = "名称";
                        break;
                    case "Expression":
                        column.HeaderText = "控制规则";
                        column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                        break;
                    case "Operation":
                        column.HeaderText = "执行动作";
                        column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                        break;
                    case "Period":
                        column.HeaderText = "控制周期";
                        break;
                    case "State":
                        column.HeaderText = "启用";
                        break;
                    default:
                        break;
                }
            }
        }


        /// <summary>
        /// Loads the history data.
        /// </summary>
        /// <param name="startTime">The start time.</param>
        /// <param name="endTime">The end time.</param>
        /// <returns></returns>
        private DataTable LoadHistoryData(DateTime startTime, DateTime endTime)
        {
            string sql = GetHistoryDataCommand(_sqlGetHistoryData1 + _sqlGetHistoryData2 + _sqlGetHistoryData3,
                _dataTable,
                _curvesTable, startTime, endTime);
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
            Curve[] curves = new Curve[_curveCount];

            for (int index = 0; index < _curveCount; index++)
            {
                curves[index] = _curves[index];
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

        private Rule GetSelectedRule()
        {
            if (dgv_oc_rules.CurrentRow != null)
            {
                int selectRowIndex = dgv_oc_rules.CurrentRow.Index;
                Rule rule = new Rule
                {
                    Id = Convert.ToInt32(dgv_oc_rules.Rows[selectRowIndex].Cells[0].Value),
                    Name = Convert.ToString(dgv_oc_rules.Rows[selectRowIndex].Cells[1].Value),
                    Expression = Convert.ToString(dgv_oc_rules.Rows[selectRowIndex].Cells[2].Value),
                    Operation = Convert.ToString(dgv_oc_rules.Rows[selectRowIndex].Cells[3].Value),
                    Period = Convert.ToString(dgv_oc_rules.Rows[selectRowIndex].Cells[4].Value) != ""
                        ? Convert.ToInt32(dgv_oc_rules.Rows[selectRowIndex].Cells[4].Value)
                        : -1,
                    Enabled = Convert.ToBoolean(dgv_oc_rules.Rows[selectRowIndex].Cells[5].Value),
                };
                return rule;
            }
            return new Rule();
        }



        #endregion

        #region 控件响应

        /// <summary>
        /// Handles the Tick event of the timer_realtime control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private
            void TimerRealtimeElapsed(object sender, EventArgs e)
        {
            if (!_realTimerFlag) return;
            try
            {
                for (int deviceID = 0; deviceID < _devices.Length; deviceID++)
                {
                    if (_devices[deviceID].State)
                    {
                        ModbusTCPGetValue(ref (_devices[deviceID]));
                    }
                }
                if (_modbusRtuSlaveCreated)
                {
                    ModbusRTUGetValue();
                }
                UpdateRegisterList();

                DateTime time = DateTime.Now;
                bool modbusTCPMasterUpdated = false;
                for (int index = 0; index < _devices.Length; index++)
                {
                    if (_devices[index].ModbusTcpMasterUpdated)
                    {
                        modbusTCPMasterUpdated = true;
                        break;
                    }
                }
                if (_modbusRtuSlaveUpdated || modbusTCPMasterUpdated)
                {
                    if (SaveParameter(_modbusRtuParameters, time) > 0)
                    {
                        _modbusRtuSlaveUpdated = false;
                        status_Label.Text = string.Format("{0}数据已保存", _dcsName);
                    }
                    for (int index = 0; index < _devices.Length; index++)
                    {
                        if (!_devices[index].State) continue;
                        if (SaveParameter(_devices[index].Variables, time) <= 0) continue;
                        _devices[index].ModbusTcpMasterUpdated = false;
                        status_Label.Text = string.Format("{0}数据已保存", _devices[index].Name);
                    }
                }

                double tmpTime = new XDate(time);
                for (int index = 0; index < _curveCount; index++)
                {
                    if (_curves[index].DataList.Count >= _dataListLength) //数组长度限制
                    {
                        _curves[index].DataList.RemoveRange(0, (_curves[index].DataList.Count - _dataListLength + 1));
                    }
                    if (_curves[index].DeviceId == 0)
                    {
                        foreach (Variable parameter in _modbusRtuParameters)
                        {
                            if (parameter.Address != _curves[index].Address) continue;
                            _curves[index].Name = parameter.Name;
                            _curves[index].DataList.Add(tmpTime, parameter.Value);
                            break;
                        }
                    }
                    else
                    {
                        foreach (Device device in _devices)
                        {
                            if (device.Id != _curves[index].DeviceId) continue;
                            foreach (Variable parameter in device.Variables)
                            {
                                if (parameter.Address != _curves[index].Address) continue;
                                _curves[index].Name = parameter.Name;
                                _curves[index].DataList.Add(tmpTime, parameter.Value);
                                break;
                            }
                        }
                    }
                }

                if (_execteRulesFlag)
                {
                    ExecteRules(_rules);
                }

                if (_updateGraphFlag)
                    UpdateGraph(ref _masterPaneGraphRealtime, ref zgc_realtime, _curves);

                if (_myFilter.isActive)
                {
                    _timerLogoff.Stop();
                    _myFilter.isActive = false;
                    _timerLogoff.Start();
                }
            }
            catch (Exception ex)
            {
                WriteLogFile(_logFile, "RealTimer", ex.Message); //未能正常写入文件，反馈信息到消息栏
            }
        }

        /// <summary>
        /// Handles the Elapsed event of the LogoffTimer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Timers.ElapsedEventArgs" /> instance containing the event data.</param>
        private void TimerLogoffElapsed(object sender, ElapsedEventArgs e)
        {
            _timerLogoff.Stop();
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
                        _timerRealtime.Stop();
                        for (int deviceID = 0; deviceID < _devices.Length; deviceID++)
                        {
                            _devices[deviceID].ModbusTcpMasterCreated =
                                ModbusTCPStopComm(_devices[deviceID].ModbusTcpDevice);
                        }
                        ModbusRTUStopComm();
                        WriteLogFile(_logFile, "Closed", "Software Closed!");
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
                WriteLogFile(_logFile, "frmMain_FormClosing", ex.Message);
            }
        }

        /// <summary>
        /// Handles the Click event of the tool_btn_rtwave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btn_run_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(_modbusRtuDevice.SerialPortObject.PortName))
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
                    DateTime startTime = endTime.AddSeconds((-1)*(_realTimerInterval/1000)*_dataListLength);

                    for (int index = 0; index < _curveCount; index++)
                    {
                        string sql = GetHistoryDataCommand(_sqlGetHistoryData, _dataTable, _curves[index].Name,
                            _curves[index].DeviceId, startTime, endTime);
                        DataTable dataTable = SQLHelper.ExcuteDataTable(SQLHelper.ConnectionStringLocalTransaction, sql);
                        for (int i = 0; i < dataTable.Rows.Count; i++)
                        {
                            _curves[index].DataList.Add(new XDate(DateTime.Parse(dataTable.Rows[i][1].ToString())),
                                Convert.ToDouble(dataTable.Rows[i][3]));
                        }
                    }

                    _modbusRtuSlaveCreated = ModbusRTUCreatListener(_modbusRtuDevice);
                    if (_modbusRtuSlaveCreated)
                    {
                        for (int deviceID = 0; deviceID < _devices.Length; deviceID++)
                        {
                            if (_devices[deviceID].State)
                            {
                                _devices[deviceID].ModbusTcpMasterCreated =
                                    ModbusTCPCreateClient(ref _devices[deviceID].ModbusTcpDevice);
                            }
                        }
                        _realTimerFlag = true;
                        TimerRealtimeElapsed(sender, e);
                        _timerRealtime.Start();

                        // 加载权限菜单
                        RightsMenuDataManager rmManager = new RightsMenuDataManager();
                        rmManager.LoadMenuRightsItem(msMain, _currentOperator.RightsCollection);
                        menu_control_run.Enabled = false;
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
                        WriteLogFile(_logFile, "Start",
                            string.Format("Software started by {0}!", _currentOperator.Name));
                    }
                }
                catch (Exception ex)
                {
                    WriteLogFile(_logFile, "Run_Click", ex.Message);
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
            _realTimerFlag = false;
            _timerRealtime.Stop();

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
            for (int deviceID = 0; deviceID < _devices.Length; deviceID++)
            {
                Device device = _devices[deviceID];
                device.ModbusTcpMasterCreated = ModbusTCPStopComm(_devices[deviceID].ModbusTcpDevice);
                _devices[deviceID] = device;
            }
            ModbusRTUStopComm();
            //listview_parainfo.Items.Clear();
            status_Label.Text = "停止.";
            _isRunning = false;
            WriteLogFile(_logFile, "Stop", string.Format("Software stoped by {0}!", _currentOperator.Name));
        }

        /// <summary>
        /// Handles the Click event of the btn_curve_realtime control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btn_curve_realtime_Click(object sender, EventArgs e)
        {
            zgc_realtime.Invalidate();
            _updateGraphFlag = true;
        }

        /// <summary>
        /// Handles the Click event of the btn_curve_stop control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btn_curve_stop_Click(object sender, EventArgs e)
        {
            zgc_realtime.Invalidate();
            _updateGraphFlag = false;
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
                WriteLogFile(_logFile, "tool_btn_config_Click", ex.Message);
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
            DateTime startTime = dtp_curve_start.Value; //查询起始时间
            DateTime endTime = dtp_curve_end.Value; //查询截止时间
            if (endTime > startTime)
            {
                _hisoryCurves = new Curve[_curveCount];
                DataTable dataTable = LoadHistoryData(startTime, endTime);
                dgv_data.DataSource = dataTable;
                dgv_data.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
                _hisoryCurves = LoadHistoryCurves(dataTable);
                UpdateGraph(ref _masterPaneGraphHistory, ref zgc_history, _hisoryCurves);
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
            DateTime endTime = dtp_curve_start.Value; //查询截止时间
            DateTime startTime = dtp_curve_start.Value - (dtp_curve_end.Value - dtp_curve_start.Value); //查询起始时间
            if (endTime > startTime)
            {
                _hisoryCurves = new Curve[_curveCount];
                DataTable dataTable = LoadHistoryData(startTime, endTime);
                dgv_data.DataSource = dataTable;
                dgv_data.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
                _hisoryCurves = LoadHistoryCurves(dataTable);
                UpdateGraph(ref _masterPaneGraphHistory, ref zgc_history, _hisoryCurves);
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
            DateTime startTime = dtp_curve_end.Value; //查询起始时间
            DateTime endTime = dtp_curve_end.Value + (dtp_curve_end.Value - dtp_curve_start.Value); //查询截止时间
            if (endTime > startTime)
            {
                _hisoryCurves = new Curve[_curveCount];
                DataTable dataTable = LoadHistoryData(startTime, endTime);
                dgv_data.DataSource = dataTable;
                dgv_data.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
                _hisoryCurves = LoadHistoryCurves(dataTable);
                UpdateGraph(ref _masterPaneGraphHistory, ref zgc_history, _hisoryCurves);
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
            DateTime startTime = dtp_data_start.Value; //查询起始时间
            DateTime endTime = dtp_data_end.Value; //查询截止时间
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
            DateTime endTime = dtp_data_start.Value; //查询截止时间
            DateTime startTime = dtp_data_start.Value - (dtp_data_end.Value - dtp_data_start.Value); //查询起始时间
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
            DateTime startTime = dtp_data_end.Value; //查询起始时间
            DateTime endTime = dtp_data_end.Value + (dtp_data_end.Value - dtp_data_start.Value); //查询截止时间
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
                    WriteLogFile(_logFile, "Login", string.Format("{0} Login!", _currentOperator.Name));

                    // 加载权限菜单
                    RightsMenuDataManager rmManager = new RightsMenuDataManager();
                    rmManager.LoadMenuRightsItem(msMain, _currentOperator.RightsCollection);
                    if (_isRunning)
                    {
                        menu_control_run.Enabled = false;
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

                    _timerLogoff.Start();
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
            WriteLogFile(_logFile, "Logoff", string.Format("{0} Logoff!", _currentOperator.Name));
            _currentOperator = null;
            LoadMenuRightsItem(msMain, _logoffMenuList);
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
                IOperatorManager operatorManager = _bllFactory.BuildOperatorManager();
                _currentOperator = operatorManager.GetOperatorInfoByName(_currentOperator.Name,
                    _currentOperator.Password);
                // 加载权限菜单
                RightsMenuDataManager rmManager = new RightsMenuDataManager();
                rmManager.LoadMenuRightsItem(msMain, _currentOperator.RightsCollection);
                if (_isRunning)
                {
                    menu_control_run.Enabled = false;
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

        private void tsbtn_rule_add_Click(object sender, EventArgs e)
        {
            Rule rule = GetSelectedRule();
            if (rule.Name == "") return;
            frmRuleEditor addRuleForm = new frmRuleEditor(DataOperateMode.Insert, rule, _parameterDataTable);
            if (addRuleForm.ShowDialog() == DialogResult.OK)
            {
                status_Label.Text = string.Format("插入 {0} 行数据",
                    addRuleForm.Result.ToString(CultureInfo.InvariantCulture));
                UpdateRules();
            }
        }

        private void tsbtn_rule_edit_Click(object sender, EventArgs e)
        {
            Rule rule = GetSelectedRule();
            frmRuleEditor editParameterForm = new frmRuleEditor(DataOperateMode.Edit, rule, _parameterDataTable);
            if (editParameterForm.ShowDialog() == DialogResult.OK)
            {
                status_Label.Text = string.Format("编辑 {0} 行数据",
                    editParameterForm.Result.ToString(CultureInfo.InvariantCulture));
                UpdateRules();
            }
        }

        private void tsbtn_rule_delete_Click(object sender, EventArgs e)
        {
            Rule rule = GetSelectedRule();
            frmRuleEditor deleteParameterForm = new frmRuleEditor(DataOperateMode.Delete, rule, _parameterDataTable);
            if (deleteParameterForm.ShowDialog() == DialogResult.OK)
            {
                status_Label.Text = string.Format("删除 {0} 行数据",
                    deleteParameterForm.Result.ToString(CultureInfo.InvariantCulture));
                UpdateRules();
            }
        }

        private void tsbtn_rule_update_Click(object sender, EventArgs e)
        {
            UpdateRules();
        }

        private void tsbtn_rule_paras_Click(object sender, EventArgs e)
        {
            frmParametersManager parametersForm = new frmParametersManager();
            parametersForm.ShowDialog();
        }

        private void dgv_oc_rules_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            tsbtn_rule_edit_Click(sender, e);
        }
        #endregion

        private void tsbtn_oc_enabled_Click(object sender, EventArgs e)
        {
            DataRow[] data = _ruleDataTable.Select("State='True'");
            if (data.Length > 0)
            {
                _rules.Clear();
                foreach (DataRow row in data)
                {
                    _rules.Add(new Rule()
                    {
                        Id = Convert.ToInt32(row["Id"]),
                        Name = row["Name"].ToString(),
                        Expression = row["Expression"].ToString(),
                        Operation = row["Operation"].ToString(),
                        Period = Convert.ToInt32(row["Period"]),
                        Enabled = Convert.ToBoolean(row["State"]),
                        DelayTime =
                            (Convert.ToInt32(row["Period"]) > 0 ? Convert.ToInt32(row["Period"]) : _defaultControlPeriod),
                    });
                }
                _execteRulesFlag = true;
            }

        }

        /// <summary>
        /// Exectes the rules.
        /// </summary>
        private void ExecteRules(List<Rule> rules)
        {
            foreach (Rule rule in rules)
            {
                rule.DelayTime -= _realTimerInterval/1000;
                if (rule.DelayTime > 0)
                {
                    continue;
                }
                string expString = rule.Expression;
                StringBuilder expStringBuilder = new StringBuilder();
                foreach (string s in expString.Trim(new char[] {'[', ']'}).Split(new char[] {'[', ']'}))
                {
                    if (s.StartsWith("@"))
                    {
                        double value = GetValueByName(s.TrimStart('@'));
                        if (value < 0)
                        {
                            expStringBuilder.Clear(); //计算错误
                            break;
                        }
                        expStringBuilder.Append(value);
                    }
                    else
                    {
                        expStringBuilder.Append(s);
                    }
                }
                if (expStringBuilder.Length == 0) break;
                RPN rpn = new RPN();
                if (rpn.Parse(expStringBuilder.ToString()))
                {
                    bool isTure;
                    bool.TryParse(rpn.Evaluate().ToString(), out isTure);
                    if (isTure)
                    {
                        string opString = rule.Operation;
                        StringBuilder opStringBuilder = new StringBuilder();
                        string[] op = opString.Trim(new char[] {'[', ']'}).Split(new char[] {'[', ']'});
                        if (op.Length > 2 && op[0].StartsWith("@") && op[1].StartsWith("="))
                        {
                            op[1] = op[1].TrimStart('=');
                            for (int index = 1; index < op.Length; index++)
                            {
                                if (op[index].StartsWith("@"))
                                {
                                    double value = GetValueByName(op[index].TrimStart('@'));
                                    if (value < 0)
                                    {
                                        opStringBuilder.Clear(); //计算错误
                                        break;
                                    }
                                    opStringBuilder.Append(value);
                                }
                                else
                                {
                                    opStringBuilder.Append(op[index]);
                                }
                            }
                            if (opStringBuilder.Length == 0) break;
                            rpn = new RPN();
                            if (rpn.Parse(opStringBuilder.ToString()))
                            {
                                foreach (Variable parameter in _modbusRtuParameters)
                                {
                                    if (parameter.Name == op[0].TrimStart('@'))
                                    {
                                        double result = Convert.ToDouble(rpn.Evaluate())/parameter.Ratio;
                                        if (((result < parameter.Limit.UltimateUpperLimit) ||
                                             (parameter.Limit.UltimateUpperLimit <= 0)) &&
                                            ((result > parameter.Limit.UltimateLowerLimit) ||
                                             (parameter.Limit.UltimateLowerLimit <= 0)))
                                        {
                                            if (_modbusRtuSlaveCreated)
                                            {
                                                byte[] tempByte =
                                                    BitConverter.GetBytes(Convert.ToSingle(result));
                                                _modbusRtuSlave.DataStore.HoldingRegisters[parameter.Address] =
                                                    Convert.ToUInt16(tempByte[1]*256 + tempByte[0]);
                                                _modbusRtuSlave.DataStore.HoldingRegisters[parameter.Address + 1] =
                                                    Convert.ToUInt16(tempByte[3]*256 + tempByte[2]);
                                                ModbusRTUGetValue();
                                            }
                                            MessageBox.Show(string.Format("{0} :\r\n {1} :\r\n {2}", expString, opString,
                                                result));
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                rule.DelayTime = rule.Period > 0 ? rule.Period : _defaultControlPeriod;
            }
        }

        /// <summary>
        /// Gets the value of the variable by name.
        /// </summary>
        /// <param name="variableName">Name of the variable.</param>
        /// <returns>
        /// value(-1 for error)
        /// </returns>
        private double GetValueByName(string variableName)
        {
            try
            {
                DataRow[] dataRows = _parameterDataTable.Select(string.Format("Name='{0}'", variableName));
                if (dataRows.Length > 0)
                {
                    string deviceindex = dataRows[0]["DeviceID"].ToString();
                    if (deviceindex == "0")
                    {
                        foreach (Variable parameter in _modbusRtuParameters)
                        {
                            if (parameter.Name == variableName)
                            {
                                return parameter.Value;
                            }
                        }
                    }
                    else
                    {
                        foreach (Device device in _devices)
                        {
                            if (device.Id.ToString(CultureInfo.InvariantCulture) == deviceindex)
                            {
                                foreach (Variable variable in device.Variables)
                                {
                                    if (variable.Name == variableName)
                                    {
                                        return variable.Value;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLogFile(_logFile, "GetValueByName", ex.Message);
            }
            return -1;
        }

        private void tsbtn_oc_disabled_Click(object sender, EventArgs e)
        {

        }

    }
}