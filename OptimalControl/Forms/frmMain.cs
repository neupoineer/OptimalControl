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
using IBLL.Control;
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
        private BLLFactory.BLLFactory _bllFactory = new BLLFactory.BLLFactory();

        /// <summary>
        /// The software is running
        /// </summary>
        private bool _isRunning = false;

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
        private System.Threading.Timer _timerUpdateVariable;

        /// <summary>
        /// The real timer
        /// </summary>
        private System.Threading.Timer _timerRealtime;

        /// <summary>
        /// The real timer
        /// </summary>
        private System.Threading.Timer _timerRuleDelay;

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
        private int _updateVariableTimerInterval = 2000; //设置定时器间隔，默认为2000ms

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
        private List<Variable> _modbusRtuParameters = new List<Variable>();

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
        private List<Device> _devices = new List<Device>();

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
        private List<Curve> _curves = new List<Curve>();

        private List<Curve> _hisoryCurves = new List<Curve>();

        /// <summary>
        /// The data list length
        /// </summary>
        private int _dataListLength = 720;

        private List<Rule> _rules = new List<Rule>();

        private int _defaultControlPeriod = 5;
        
        private Log _historyLog = new Log();

        private string _optimalControlEnabledVariable;

        private string _optimalControlHeartBeatVariable;

        private int[] _displayedParaVariableId = new int[]
        {
            2, 1, 1,
            8, 7, 7,
            15, 14, 14,
            10, 35, 12, 5, 4, 4, 11, 16, 23, 21, 24
        };

        private int[] _displayedParaDeviceId = new int[]
        {
            0, 0, 0,
            0, 0, 0,
            0, 0, 0,
            0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0
        };
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

            //splitContainerH1.SplitterDistance = 5;
            splitContainerH1_2H2.SplitterDistance = splitContainerH1_2H2.Height - 6;
            try
            {
                if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "cache")) //检查cache目录是否已创建
                    Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "cache"); //若尚未创建，则创建目录

                ofd_history.InitialDirectory = (AppDomain.CurrentDomain.BaseDirectory + "cache");

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
                UpdateGraph(ref _masterPaneGraphHistory, ref zgc_history, _curves);

                dtp_curve_start.Value = DateTime.Today;
                dtp_curve_end.Value = DateTime.Today.AddDays(1);
                dtp_data_start.Value = DateTime.Today;
                dtp_data_end.Value = DateTime.Today.AddDays(1);

                if (_isPass)
                {
                    RecordLog.WriteLogFile("Login", string.Format("{0} Login!", _currentOperator.Name));

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

                //_timerUpdateVariable = new System.Timers.Timer(_updateVariableTimerInterval);
                //_timerUpdateVariable.Elapsed += TimerUpdateVariableElapsed;

                _timerLogoff = new System.Timers.Timer(_logoffTime*1000);
                _timerLogoff.Elapsed += TimerLogoffElapsed;
                _timerLogoff.Start();

                //_timerRealtime = new System.Threading.Timer(TimerRealtimeElapsed,null,0,_realTimerInterval);
                //_timerUpdateVariable.Elapsed += TimerRealtimeElapsed;
            }
            catch (Exception ex)
            {
                RecordLog.WriteLogFile("Initialization", ex.Message);
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
        /// 刷新工况列表.托管
        /// </summary>
        private delegate void SynchroButtonDelegate();

        /// <summary>
        /// Synchroes the buttons.
        /// </summary>
        private void SynchroButton()
        {
            if (InvokeRequired)
            {
                Invoke(new SynchroButtonDelegate(SynchroButton));
                return;
            }

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
        /// Loads the settings.
        /// </summary>
        private void LoadSettings()
        {
            try
            {
                _dcsName = ConfigAppSettings.GetSettingString("DCSName", "磨机工况信息");

                // 创建类实例
                IDeviceManager deviceManager = _bllFactory.BuildDeviceManager();
                _devices = deviceManager.GetAllDeviceInfo();
                IVariableManager variableManager = _bllFactory.BuildIVariableManager();

                foreach (Device device in _devices)
                {
                    device.ModbusTcpDevice.TcpClient = new TcpClient();
                    device.ModbusTcpMasterCreated = false;
                    device.ModbusTcpMasterUpdated = false;
                    device.Variables = variableManager.GetVariableByDeviceId(device.Id);
                }

                _modbusRtuParameters = variableManager.GetVariableByDeviceId(0);

                string tempString = ConfigAppSettings.GetValue("LogoffMenulist").Trim();
                if (tempString.Length > 0)
                {
                    _logoffMenuList = tempString.Split(',');
                }
                _logoffTime = ConfigAppSettings.GetSettingInt("LogoffTime", _logoffTime);

                _updateVariableTimerInterval = ConfigAppSettings.GetSettingInt("UpdateVariableTime", _updateVariableTimerInterval); //时间间隔

                //_timerUpdateVariable.Interval = _updateVariableTimerInterval;

                _realTimerInterval = ConfigAppSettings.GetSettingInt("RealTime", _realTimerInterval); //时间间隔

                //_timerRealtime.Interval = _realTimerInterval;

                _modbusRtuDevice.SerialPortObject = new SerialPort
                    (
                    ConfigAppSettings.GetSettingString("ModbusRTUPortName", "COM1"),
                    ConfigAppSettings.GetSettingInt("ModbusRTUBaudRate", 19200),
                    Parity.None,
                    ConfigAppSettings.GetSettingInt("ModbusRTUDataBits", 8),
                    (StopBits) ConfigAppSettings.GetSettingSingle("ModbusRTUStopBits", 1)
                    );

                _modbusRtuDevice.UnitID = ConfigAppSettings.GetSettingByte("ModbusRTUDeviceID", 1);

                _masterPaneGraphRealtime.Title.Text = ConfigAppSettings.GetSettingString("MasterTitle", "My MasterPane Title");
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

                _optimalControlEnabledVariable = ConfigAppSettings.GetSettingString("OptimalControlEnabledVariable", "").Trim();
                _optimalControlHeartBeatVariable = ConfigAppSettings.GetSettingString("OptimalControlHeartBeatVariable", "").Trim();

                ICurveManager curveManager = _bllFactory.BuildCurveManager();
                _curves = curveManager.GetAllCurveInfo();

                UpdateRulesGrid();
            }
            catch (Exception ex)
            {
                RecordLog.WriteLogFile("LoadSettings", ex.Message);
            }
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
                    _modbusRtuSlave = ModbusSerialSlave.CreateRtu(modbusRtuDevice.UnitID,
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
                //RecordLog.WriteLogFile(LogFile, "ModbusRTUCreatListener", ex.Message);
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
                    foreach (Variable variable in _modbusRtuParameters)
                    {
                        ushort[] register = new ushort[2];
                        try
                        {
                            //读寄存器
                            register[0] =
                                _modbusRtuSlave.DataStore.HoldingRegisters[variable.Address];
                            register[1] =
                                _modbusRtuSlave.DataStore.HoldingRegisters[variable.Address + 1];
                        }
                        catch (Exception)
                        {
                            ModbusRTUStopComm(); //处理连接错误，重试连接
                            ModbusRTUCreatListener(_modbusRtuDevice);
                            //RecordLog.WriteLogFile(LogFile, "ModbusRTU->ModbusTCP", ex.Message);
                            continue;
                        }
                        foreach (Device device in _devices)
                        {
                            if (!device.State ||
                                !device.SyncState ||
                                !device.ModbusTcpMasterCreated)
                                continue;
                            try
                            {
                                //读寄存器
                                device.ModbusTcpDevice.ModbusTcpMaster.WriteMultipleRegisters(
                                    device.ModbusTcpDevice.UnitID,
                                    (ushort) (variable.Address - 1),
                                    register);
                            }
                            catch (Exception)
                            {
                                ModbusTCPStopComm(device.ModbusTcpDevice); //处理连接错误，重试连接
                                ModbusTCPCreateClient(ref device.ModbusTcpDevice);
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
                        variable.Value = value*variable.Ratio;
                    }
                }
            }
            catch (Exception ex)
            {
                RecordLog.WriteLogFile("ModbusRTUGetValue", ex.Message);
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
                RecordLog.WriteLogFile("StopModbusRTUComm", ex.Message);
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
                if (string.IsNullOrEmpty(modbusTcpDevice.IP) || modbusTcpDevice.Port.Equals(0))
                {
                    return false;
                }
                modbusTcpDevice.TcpClient = new TcpClient(modbusTcpDevice.IP, modbusTcpDevice.Port);

                modbusTcpDevice.ModbusTcpMaster = ModbusIpMaster.CreateIp(modbusTcpDevice.TcpClient);
                // create Modbus TCP Master with the tcp client
                modbusTcpDevice.ModbusTcpMaster.Transport.ReadTimeout = 1000;
                return true;
            }
            catch (Exception ex)
            {
                status_Label.Text = ex.Message;
                //RecordLog.WriteLogFile(LogFile, "ModbusTCPCreateClient", ex.Message);
            }
            return false;
        }

        /// <summary>
        /// Get The Modbus TCP  value.
        /// </summary>
        /// <param name="device">The device.</param>
        private void ModbusTCPGetValue()
        {
            try
            {
                foreach (Device device in _devices)
                {
                    if (!device.State) return;
                    for (int paraIndex = 0; paraIndex < device.Variables.Count; paraIndex++)
                    {
                        ushort[] register;
                        try
                        {
                            //读寄存器
                            register =
                                device.ModbusTcpDevice.ModbusTcpMaster.ReadHoldingRegisters(
                                    device.ModbusTcpDevice.UnitID,
                                    (ushort)(device.Variables[paraIndex].Address - 1), 2);
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
                                //RecordLog.WriteLogFile(LogFile, "ModbusTCP->ModbusRTU", ex.Message);
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
                        device.Variables[paraIndex].Value = value * device.Variables[paraIndex].Ratio;
                    }
                }
            }
            catch (Exception ex)
            {
                RecordLog.WriteLogFile("ModbusTCPGetValue", ex.Message);
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
                RecordLog.WriteLogFile("StopModbusTCPComm", ex.Message);
            }
            return false;
        }

        /// <summary>
        /// Exectes the rules.
        /// </summary>
        private void ExecuteRules(List<Rule> rules)
        {
            try
            {
                foreach (Rule rule in rules)
                {
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
                                            double result = Convert.ToDouble(rpn.Evaluate());
                                            parameter.RealValue = result;

                                            if (_modbusRtuSlaveCreated)
                                            {
                                                parameter.SetValueToModbusSalve(ref _modbusRtuSlave);
                                            }
                                            Log addLog = new Log()
                                            {
                                                LogTime = DateTime.Now,
                                                Type = Log.LogType.提示,
                                                Content =
                                                    string.Format("触发规则\"{0}\",执行操作\"{1}={2}\"",
                                                        rule.Expression.Replace("@", ""), parameter.Name, result),
                                                State = true,
                                            };
                                            AddLogInfo(addLog);
                                            //MessageBox.Show(string.Format("{0} :\r\n {1} :\r\n {2}", expString, opString, result));
                                        }
                                    }
                                }
                            }
                        }
                    }
                    _timerRuleDelay =
                        new System.Threading.Timer(TimerRuleDelayElapsed, null, 0,
                            rule.Period > 0 ? rule.Period*1000 : _defaultControlPeriod*1000);
                    //_timerRuleDelay.Elapsed += TimerRuleDelayElapsed;
                    //_timerRuleDelay.Start();
                    _execteRulesFlag = false;
                    break;
                }
            }
            catch (Exception ex)
            {
                RecordLog.WriteLogFile("ExecuteRules", ex.Message);
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
                IVariableManager variableManager = _bllFactory.BuildIVariableManager();
                Variable tmpVariable = variableManager.GetVariableInfoByName(variableName);
                if (tmpVariable.DeviceID == 0)
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
                        if (device.Id == tmpVariable.DeviceID)
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
            catch (Exception ex)
            {
                RecordLog.WriteLogFile("GetValueByName", ex.Message);
            }
            return -1;
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
                ListViewGroup[] group = new ListViewGroup[_devices.Count + 1];
                for (int index = 0; index < (_devices.Count); index++)
                {
                    group[index] = new ListViewGroup(_devices[index].Name, HorizontalAlignment.Center);
                }
                group[_devices.Count] = new ListViewGroup(_dcsName, HorizontalAlignment.Center);
                listview_parainfo.Groups.AddRange(group);

                for (int deviceIndex = 0; deviceIndex < (_devices.Count); deviceIndex++)
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

                        ListViewItem[] items = new ListViewItem[_devices[deviceIndex].Variables.Count];
                        for (int paraIndex = 0; paraIndex < _devices[deviceIndex].Variables.Count; paraIndex++)
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
                    ListViewItem[] items = new ListViewItem[_modbusRtuParameters.Count];

                    for (int paraIndex = 0; paraIndex < _modbusRtuParameters.Count; paraIndex++)
                    {
                        items[paraIndex] =
                            new ListViewItem(
                                new string[]
                                {
                                    _modbusRtuParameters[paraIndex].Name,
                                    _modbusRtuParameters[paraIndex].Value.ToString("F02")
                                },
                                group[_devices.Count])
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
                RecordLog.WriteLogFile("RefreshRegisterList", ex.Message);
            }
        }

        /// <summary>
        /// 将变量保存到数据库中
        /// </summary>
        private void SaveParameters()
        {
            try
            {
                if (InvokeRequired)
                {
                    Invoke(new SaveParametersDelegate(SaveParameters));
                    return;
                }
                DateTime time = DateTime.Now;
                if (SaveParameter(_modbusRtuParameters, time))
                {
                    _modbusRtuSlaveUpdated = false;
                    status_Label.Text = string.Format("{0}数据已保存", _dcsName);
                }
                foreach (Device device in _devices)
                {
                    if (!device.State) continue;
                    if (!SaveParameter(device.Variables, time)) continue;
                    device.ModbusTcpMasterUpdated = false;
                    status_Label.Text = string.Format("{0}数据已保存", device.Name);
                }
            }
            catch (Exception ex)
            {
                RecordLog.WriteLogFile("SaveParameters", ex.Message);
            }
        }

        /// <summary>
        /// Saves the parameter.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="time">The time.</param>
        /// <returns>
        /// Result
        /// </returns>
        private bool SaveParameter(List<Variable> parameters, DateTime time)
        {
            List<Data> addData = new List<Data>();
            foreach (Variable parameter in parameters)
            {
                Data data = new Data()
                {
                    ParameterName = parameter.Name,
                    TimeValue = time,
                    Value = parameter.RealValue,
                    DeviceID = Convert.ToInt32(parameter.DeviceID),
                };
                addData.Add(data);
            }
            IDataManager dataManager = _bllFactory.BuildDataManager();
            bool result = dataManager.AddData(addData);
            return result;
        }

        /// <summary>
        /// Clears the graph.
        /// </summary>
        private void ClearGraph() //清空图形和信息
        {
            //zedGraphControl.MasterPane.PaneList.Clear(); //清波形图
            //zedGraphControl.MasterPane.GraphObjList.Clear();
            zgc_realtime.Invalidate(); //清空zedgraph信息
            zgc_history.Invalidate();
        }

        private delegate void UpdateGraphDelegate(ref MasterPane masterPane, ref ZedGraphControl zgc, List<Curve> curves);

        /// <summary>
        /// 刷新曲线.
        /// </summary>
        private void UpdateGraph(ref MasterPane masterPane, ref ZedGraphControl zgc, List<Curve> curves)
        {
            if (InvokeRequired)
            {
                Invoke(new UpdateGraphDelegate(UpdateGraph),masterPane,zgc,curves);
                return;
            }
            try
            {
                masterPane.PaneList.Clear();
                masterPane.GraphObjList.Clear();
                for (int index = 0; index < curves.Count; index++)
                {
                    if (index == 0)
                    {
                        masterPane.Add(CreatGraphPane(curves[index], GraphPaneType.First));
                    }
                    else if (index == curves.Count - 1)
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
                RecordLog.WriteLogFile("UpdateGraph", ex.Message);
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
                RecordLog.WriteLogFile("CreatGraphPane", ex.Message);
            }
            return graphPane;
        }

        private delegate void UpdateRulesGridDelegate();

        private void UpdateRulesGrid()
        {
            if (InvokeRequired)
            {
                Invoke(new UpdateRulesGridDelegate(UpdateRulesGrid));
                return;
            }
            try
            {
                // 创建权限组管理类实例
                IRuleManager ruleManager = _bllFactory.BuildRuleManager();
                // 调用实例方法
                List<Rule> ruleCollection = ruleManager.GetAllRuleInfo();

                // 如果包含信息
                if (ruleCollection.Count > 0)
                {
                    BindingSource source = new BindingSource {DataSource = ruleCollection};

                    dgv_oc_rules.DataSource = source;

                    foreach (DataGridViewColumn column in dgv_oc_rules.Columns)
                    {
                        switch (column.HeaderText) //更改列名
                        {
                            case "Id":
                                column.HeaderText = "序号";
                                column.DisplayIndex = 1;
                                break;
                            case "Name":
                                column.HeaderText = "名称";
                                column.DisplayIndex = 2;
                                break;
                            case "Expression":
                                column.HeaderText = "控制规则";
                                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                                column.DisplayIndex = 3;
                                break;
                            case "Operation":
                                column.HeaderText = "执行动作";
                                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                                column.DisplayIndex = 4;
                                break;
                            case "Period":
                                column.HeaderText = "控制周期";
                                column.DisplayIndex = 5;
                                break;
                            case "State":
                                column.HeaderText = "启用";
                                column.DisplayIndex = 6;
                                break;
                            case "Priority":
                                column.HeaderText = "优先级";
                                column.DisplayIndex = 0;
                                break;
                            case "DelayTime":
                                column.Visible = false;
                                break;
                            default:
                                break;
                        }
                    }
                    status_Label.Text = string.Format("查询到 {0} 行数据", ruleCollection.Count);
                }
            }
            catch (Exception ex)
            {
                RecordLog.WriteLogFile("UpdateRulesGrid", ex.Message); 
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
            DataTable dataTable = new DataTable();
            try
            {
                IDataManager dataManager = _bllFactory.BuildDataManager();
                dataTable = dataManager.GetAllDataInfoByTime(startTime, endTime);
            }
            catch (Exception ex)
            {
                RecordLog.WriteLogFile("LoadHistoryData", ex.Message);
            }
            return dataTable;
        }

        /// <summary>
        /// Loads the history curves.
        /// </summary>
        /// <param name="dataTable">The data table.</param>
        /// <returns></returns>
        private List<Curve> LoadHistoryCurves(DataTable dataTable)
        {
            List<Curve> curves = new List<Curve>();
            try
            {
                for (int index = 0; index < _curves.Count; index++)
                {
                    curves.Add(_curves[index]);
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
            }
            catch (Exception ex)
            {
                RecordLog.WriteLogFile("LoadHistoryCurves", ex.Message);
            }
            return curves;
        }

        private Rule GetSelectedRule()
        {
            try
            {
                if (dgv_oc_rules.CurrentRow != null)
                {
                    int selectRowIndex = dgv_oc_rules.CurrentRow.Index;
                    Rule rule = new Rule
                    {
                        Id = Convert.ToInt32(dgv_oc_rules.Rows[selectRowIndex].Cells["Id"].Value),
                        Name = Convert.ToString(dgv_oc_rules.Rows[selectRowIndex].Cells["Name"].Value),
                        Expression = Convert.ToString(dgv_oc_rules.Rows[selectRowIndex].Cells["Expression"].Value),
                        Operation = Convert.ToString(dgv_oc_rules.Rows[selectRowIndex].Cells["Operation"].Value),
                        Period = Convert.ToString(dgv_oc_rules.Rows[selectRowIndex].Cells["Period"].Value) != ""
                            ? Convert.ToInt32(dgv_oc_rules.Rows[selectRowIndex].Cells["Period"].Value)
                            : -1,
                        State = Convert.ToBoolean(dgv_oc_rules.Rows[selectRowIndex].Cells["State"].Value),
                        Priority = Convert.ToInt32(dgv_oc_rules.Rows[selectRowIndex].Cells["Priority"].Value),
                    };
                    return rule;
                }
            }
            catch (Exception ex)
            {
                RecordLog.WriteLogFile("GetSelectedRule", ex.Message);
            }
            return new Rule();
        }

        private delegate void UpdateOcTextBoxDelegate();

        private void UpdateOcTextBox()
        {
            if (InvokeRequired)
            {
                Invoke(new UpdateOcTextBoxDelegate(UpdateOcTextBox));
                return;
            }
            try
            {
                for (int i = 0; i < _displayedParaVariableId.Length; i++)
                {
                    if (_displayedParaDeviceId[i] == 0)
                    {
                        foreach (Variable parameter in _modbusRtuParameters)
                        {
                            if (parameter.Id == _displayedParaVariableId[i])
                            {
                                Control[] controls = this.Controls.Find(string.Format("tb_oc_{0}", i + 1), true);
                                if (controls.Length > 0)
                                    controls[0].Text = parameter.RealValue.ToString(CultureInfo.InvariantCulture);
                            }
                        }
                    }
                    else
                    {
                        foreach (Device device in _devices)
                        {
                            if (device.Id == _displayedParaDeviceId[i])
                            {
                                foreach (Variable variable in device.Variables)
                                {
                                    if (variable.Id == _displayedParaVariableId[i])
                                    {
                                        Control[] controls = this.Controls.Find(string.Format("tb_oc_{0}", i + 1), true);
                                        if (controls.Length > 0)
                                            controls[0].Text = variable.RealValue.ToString(CultureInfo.InvariantCulture);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                RecordLog.WriteLogFile("GetSelectedRule", ex.Message);
            }
        }

        private delegate void UpdateLogGridDelegate();

        private void UpdateLogGrid()
        {
            if (InvokeRequired)
            {
                Invoke(new UpdateLogGridDelegate(UpdateLogGrid));
                return;
            }
            try
            {
                // 创建工厂类实例
                BLLFactory.BLLFactory bllFactory = new BLLFactory.BLLFactory();
                // 创建权限组管理类实例
                ILogManager logManager = bllFactory.BuildLogManager();
                // 调用实例方法
                List<Log> logCollection = logManager.GetLastTwentyLogInfo();

                // 如果包含权限组信息
                if (logCollection.Count > 0)
                {
                    _historyLog = logCollection[0];

                    // 绑定权限组数据显示
                    BindingSource source = new BindingSource();
                    source.DataSource = logCollection;
                    dgv_oc_logs.DataSource = source;

                    // 设置中文列名
                    dgv_oc_logs.Columns["Id"].HeaderText = "编号";
                    dgv_oc_logs.Columns["Id"].DisplayIndex = 0;
                    dgv_oc_logs.Columns["Id"].Visible = false;
                    dgv_oc_logs.Columns["LogTime"].HeaderText = "时间";
                    dgv_oc_logs.Columns["LogTime"].DisplayIndex = 1;
                    dgv_oc_logs.Columns["LogTime"].DefaultCellStyle.Format = "yyyy-MM-dd hh:mm:ss";
                    dgv_oc_logs.Columns["LogTime"].Width = 200;
                    dgv_oc_logs.Columns["Type"].HeaderText = "等级";
                    dgv_oc_logs.Columns["Type"].DisplayIndex = 2;
                    dgv_oc_logs.Columns["Type"].Width = 100;
                    dgv_oc_logs.Columns["Content"].HeaderText = "内容";
                    dgv_oc_logs.Columns["Content"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    dgv_oc_logs.Columns["Content"].DisplayIndex = 3;
                    dgv_oc_logs.Columns["State"].HeaderText = "状态";
                    dgv_oc_logs.Columns["State"].DisplayIndex = 4;
                    dgv_oc_logs.Columns["State"].Width = 100;
                }
            }
            catch (Exception ex)
            {
                RecordLog.WriteLogFile("UpdateLogGrid", ex.Message);
            }
        }

        private void AddLogInfo(Log addLog)
        {
            if (addLog.Content == _historyLog.Content && addLog.LogTime - _historyLog.LogTime < TimeSpan.FromHours(1))
            {
                return;
            }
            // 创建工厂类实例
            BLLFactory.BLLFactory bllFactory = new BLLFactory.BLLFactory();

            try
            {
                // 创建权限关系管理类实例
                ILogManager logManager = bllFactory.BuildLogManager();
                // 调用实例方法
                // 执行添加操作
                logManager.AddLog(addLog);
            }
            catch (Exception ex)
            {
                RecordLog.WriteLogFile("AddLogInfo", ex.Message);
            }
        }

        /// <summary>
        /// 从Modbus更新所有变量，并保存到数据库
        /// </summary>
        private void UpdateParameterValue()
        {
            try
            {
                ModbusTCPGetValue();

                if (_modbusRtuSlaveCreated)
                {
                    ModbusRTUGetValue();
                }
                UpdateRegisterList();

                bool modbusTCPMasterUpdated = false;
                for (int index = 0; index < _devices.Count; index++)
                {
                    if (_devices[index].ModbusTcpMasterUpdated)
                    {
                        modbusTCPMasterUpdated = true;
                        break;
                    }
                }
                if (_modbusRtuSlaveUpdated || modbusTCPMasterUpdated)
                {
                    SaveParameters();
                }
            }
            catch (Exception ex)
            {
                RecordLog.WriteLogFile("UpdateModbus", ex.Message);
            }
        }

        private delegate void SaveParametersDelegate();

        private void UpdateCurveData()
        {
            try
            {
                double tmpTime = new XDate(DateTime.Now);
                foreach (Curve curve in _curves)
                {
                    if (curve.DataList.Count >= _dataListLength) //数组长度限制
                    {
                        curve.DataList.RemoveRange(0, (curve.DataList.Count - _dataListLength + 1));
                    }
                    if (curve.DeviceID == 0)
                    {
                        foreach (Variable parameter in _modbusRtuParameters)
                        {
                            if (parameter.Address != curve.Address) continue;
                            curve.Name = parameter.Name;
                            curve.DataList.Add(tmpTime, parameter.Value);
                            break;
                        }
                    }
                    else
                    {
                        foreach (Device device in _devices)
                        {
                            if (device.Id != curve.DeviceID) continue;
                            foreach (Variable parameter in device.Variables)
                            {
                                if (parameter.Address != curve.Address) continue;
                                curve.Name = parameter.Name;
                                curve.DataList.Add(tmpTime, parameter.Value);
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                RecordLog.WriteLogFile("UpdateCurveData", ex.Message);
            }
        }

        private void CheckParameterState()
        {
            try
            {
                foreach (Variable parameter in _modbusRtuParameters)
                {
                    Variable.VariableState state = parameter.CheckVariableState();
                    if (state != Variable.VariableState.正常)
                    {
                        Log addLog = new Log()
                        {
                            LogTime = DateTime.Now,
                            Content = string.Format("变量[{0}]={1}, {2}", parameter.Name, parameter.RealValue, state),
                            Type =
                                (state == Variable.VariableState.超上上限 || state == Variable.VariableState.超下下限)
                                    ? Log.LogType.严重
                                    : Log.LogType.报警,
                            State = true,
                        };

                        AddLogInfo(addLog);
                    }
                }
                if (_devices.Count > 0)
                {
                    foreach (Device device in _devices)
                    {
                        foreach (Variable parameter in device.Variables)
                        {
                            Variable.VariableState state = parameter.CheckVariableState();
                            if (state != Variable.VariableState.正常)
                            {
                                Log addLog = new Log()
                                {
                                    LogTime = DateTime.Now,
                                    Content =
                                        string.Format("变量[{0}]={1}, {2}", parameter.Name, parameter.RealValue, state),
                                    Type =
                                        (state == Variable.VariableState.超上上限 || state == Variable.VariableState.超下下限)
                                            ? Log.LogType.严重
                                            : Log.LogType.报警,
                                    State = true,
                                };
                                AddLogInfo(addLog);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                RecordLog.WriteLogFile("CheckParameterState", ex.Message);
            }
        }

        #endregion 

        #region 控件响应

        /// <summary>
        /// Handles the Tick event of the timer_realtime control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void TimerUpdateVariableElapsed(object o)
        {
            if (!_realTimerFlag) return;
            try
            {
                UpdateParameterValue();
                UpdateCurveData();
                //CheckParameterState();
                UpdateOcTextBox();
                foreach (Variable parameter in _modbusRtuParameters)
                {
                    if (parameter.Name == _optimalControlEnabledVariable)
                    {
                        _execteRulesFlag = parameter.Value > 0;
                    }
                    if (parameter.Name == _optimalControlHeartBeatVariable)
                    {
                        parameter.Value = 1;
                        if (_modbusRtuSlaveCreated)
                        {
                            parameter.SetValueToModbusSalve(ref _modbusRtuSlave);
                        }
                    }
                }

                if (_updateGraphFlag)
                    UpdateGraph(ref _masterPaneGraphRealtime, ref zgc_realtime, _curves);
                if (_myFilter.isActive)
                {
                    _timerLogoff.Stop();
                    _myFilter.isActive = false;
                    _timerLogoff.Start();
                }
                Console.WriteLine(string.Format("2:{0}", DateTime.Now));
            }
            catch (Exception ex)
            {
                RecordLog.WriteLogFile("UpdateVariableTimer", ex.Message); //未能正常写入文件，反馈信息到消息栏
            }
        }

        private void TimerRealtimeElapsed(object o)
        {
            try
            {
                if (_execteRulesFlag)
                    ExecuteRules(_rules);
                UpdateLogGrid();
                Console.WriteLine(string.Format("1:{0}",DateTime.Now));
            }
            catch (Exception ex)
            {
                RecordLog.WriteLogFile("RealTimer", ex.Message); //未能正常写入文件，反馈信息到消息栏
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
                        //_timerUpdateVariable.Stop();
                        _timerUpdateVariable.Dispose();
                        //_timerRealtime.Stop();
                        _timerRealtime.Dispose();

                        foreach (Device device in _devices)
                        {
                            device.ModbusTcpMasterCreated =
                                ModbusTCPStopComm(device.ModbusTcpDevice);
                        }
                        ModbusRTUStopComm();
                        RecordLog.WriteLogFile("Closed", "Software Closed!");
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
                RecordLog.WriteLogFile("frmMain_FormClosing", ex.Message);
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
                    DateTime startTime = endTime.AddSeconds((-1)*(_updateVariableTimerInterval/1000)*_dataListLength);
                    IDataManager dataManager = _bllFactory.BuildDataManager();
                    foreach (Curve curve in _curves)
                    {
                        List<Data> dataCollection = dataManager.GetDataByVariableName(curve.Name,
                            curve.DeviceID, startTime, endTime);
                        foreach (Data tmpData in dataCollection)
                        {
                            curve.DataList.Add(
                                new XDate(DateTime.Parse(tmpData.TimeValue.ToString())),
                                Convert.ToDouble(tmpData.Value));
                        }
                    }

                    _modbusRtuSlaveCreated = ModbusRTUCreatListener(_modbusRtuDevice);
                    if (_modbusRtuSlaveCreated)
                    {
                        foreach (Device device in _devices)
                        {
                            if (device.State)
                            {
                                device.ModbusTcpMasterCreated = ModbusTCPCreateClient(ref device.ModbusTcpDevice);
                            }
                        }
                        _realTimerFlag = true;
                        //TimerUpdateVariableElapsed(sender, e);
                        //_timerUpdateVariable.Start();
                        _timerUpdateVariable = new System.Threading.Timer(TimerUpdateVariableElapsed,null,0,_updateVariableTimerInterval);
                        //_timerRealtime.Start();
                        _timerRealtime = new System.Threading.Timer(TimerRealtimeElapsed, null, 0, _realTimerInterval);

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
                        RecordLog.WriteLogFile("Start",
                            string.Format("Software started by {0}!", _currentOperator.Name));
                    }
                }
                catch (Exception ex)
                {
                    RecordLog.WriteLogFile("Run_Click", ex.Message);
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
            //_timerUpdateVariable.Stop();
            _timerUpdateVariable.Dispose();
            //_timerRealtime.Stop();
            _timerRealtime.Dispose();
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
            for (int deviceID = 0; deviceID < _devices.Count; deviceID++)
            {
                Device device = _devices[deviceID];
                device.ModbusTcpMasterCreated = ModbusTCPStopComm(_devices[deviceID].ModbusTcpDevice);
                _devices[deviceID] = device;
            }
            ModbusRTUStopComm();
            //listview_parainfo.Items.Clear();
            status_Label.Text = "停止.";
            _isRunning = false;
            RecordLog.WriteLogFile("Stop", string.Format("Software stoped by {0}!", _currentOperator.Name));
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
            btn_curve_realtime.Enabled = false;
            btn_curve_stop.Enabled = true;
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
            btn_curve_realtime.Enabled = true;
            btn_curve_stop.Enabled = false;
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
                RecordLog.WriteLogFile("tool_btn_config_Click", ex.Message);
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
                    RecordLog.WriteLogFile("Login", string.Format("{0} Login!", _currentOperator.Name));

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
            RecordLog.WriteLogFile("Logoff", string.Format("{0} Logoff!", _currentOperator.Name));
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
            frmRuleEditor addRuleForm = new frmRuleEditor(DataOperateMode.Insert, rule);
            if (addRuleForm.ShowDialog() == DialogResult.OK)
            {
                if (addRuleForm.Result)
                {
                    status_Label.Text = "插入数据成功";
                    UpdateRulesGrid();
                }
            }
        }

        private void tsbtn_rule_edit_Click(object sender, EventArgs e)
        {
            Rule rule = GetSelectedRule();
            frmRuleEditor editParameterForm = new frmRuleEditor(DataOperateMode.Edit, rule);
            if (editParameterForm.ShowDialog() == DialogResult.OK)
            {
                if (editParameterForm.Result)
                {
                    status_Label.Text = "编辑数据成功";
                    UpdateRulesGrid();
                }
            }
        }

        private void tsbtn_rule_delete_Click(object sender, EventArgs e)
        {
            Rule rule = GetSelectedRule();
            frmRuleEditor deleteParameterForm = new frmRuleEditor(DataOperateMode.Delete, rule);
            if (deleteParameterForm.ShowDialog() == DialogResult.OK)
            {
                if (deleteParameterForm.Result)
                {
                    status_Label.Text = "删除数据成功";
                    UpdateRulesGrid();
                }
            }
        }

        private void tsbtn_rule_update_Click(object sender, EventArgs e)
        {
            UpdateRulesGrid();
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

        private void tsbtn_oc_enabled_Click(object sender, EventArgs e)
        {
            IRuleManager ruleManager = _bllFactory.BuildRuleManager();
            _rules = ruleManager.GetRuleInfoEnabled();
            _execteRulesFlag = true;
            tsbtn_oc_enabled.Enabled = false;
            tsbtn_oc_disabled.Enabled = true;
        }

        private void TimerRuleDelayElapsed(object o)
        {
            _execteRulesFlag = true;
            _timerRuleDelay.Dispose();
            //_timerRuleDelay.Stop();
            //_timerRuleDelay.Elapsed -= TimerRuleDelayElapsed;
        }

        private void tsbtn_oc_disabled_Click(object sender, EventArgs e)
        {
            _rules.Clear();
            _execteRulesFlag = false;
            tsbtn_oc_enabled.Enabled = true;
            tsbtn_oc_disabled.Enabled = false;
        }

        #endregion
    }
}