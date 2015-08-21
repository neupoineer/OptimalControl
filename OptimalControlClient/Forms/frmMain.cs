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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Net.Sockets;
using System.Timers;
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

    public struct DisplayedParameter
    {
        public int TextboxID;
        public int DeviceID;
        public string VariableCode;
    }

    #endregion

    /// <summary>
    /// Class MainForm
    /// </summary>
    public partial class FrmMain : Form
    {
        #region 全局变量

        private string[] _args;

        /// <summary>
        /// The _isPass
        /// </summary>
        private bool _isPass;

        /// <summary>
        /// 当前登录操作员实体
        /// </summary>
        private Operator _currentOperator;

        /// <summary>
        /// 创建工厂类
        /// </summary>
        private BLLFactory.BLLFactory _bllFactory = new BLLFactory.BLLFactory();

        /// <summary>
        /// The software is running
        /// </summary>
        private bool _isRunning;

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

        private System.Threading.Timer _timerFirstRound;

        /// <summary>
        /// The real timer flag
        /// </summary>
        private bool _realTimerFlag;

        private bool _updateGraphFlag;

        private bool _isFirstRoundFlag = true;
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

        private Thread _modbusTcpSlaveThread;

        private ModbusTcpDevice _modbusTcpDevice;

        private List<Variable> _modbusTcpVariables = new List<Variable>(); 
        
        /// <summary>
        /// The devices
        /// </summary>
        private List<Device> _devices = new List<Device>();

        /// <summary>
        /// The modbus Tcp slave created flag
        /// </summary>
        private bool _modbusTcpSlaveCreated;

        /// <summary>
        /// The DCS name displayed in list
        /// </summary>
        private string _dcsName;
        private string _clientName;

        /// <summary>
        /// The master pane graph
        /// </summary>
        private MasterPane _masterPaneGraphRealtime = new MasterPane();

        private MasterPane _masterPaneGraphHistory = new MasterPane();

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

        private List<DisplayedParameter> _displayedParas = new List<DisplayedParameter>();

        private List<DisplayedParameter> _displayedStatus = new List<DisplayedParameter>();

        private string[] _displayedParaVariableCode = new string[]
        {
            "CS010100020101","CS010100020201","CS010100020301","CS060100040201","CS060100050201",
            "CS040100020101","CS040100020201","CS040100020301","CS060100040202","CS060100050202",
            "CS040200020101","CS040200020201","CS040200020301","CS060100040203","CS060100050203",
            "CS040200030103","CS060200030104","CS060200030102",
            "CS060100030111","CS060100030112","CS060100030113",
            "CS060100030103","CS060100030105","CS060200030101",
            "CS020200080111","CS020200070111","CS060200030112",
            "CS020200080121","CS020200070121","CS060200030122"
        };

        private int[] _displayedParaDeviceId = new int[]
        {
            0, 0, 0, 0, 0,
            0, 0, 0, 0, 0,
            0, 0, 0, 0, 0,
            0, 3, 0,
            0, 0, 0,
            0, 0, 0,
            0, 0, 0,
            0, 0, 0
        };

        private int[] _displayedParaTextboxId = new int[]
        {
            101,102,103,104,105,
            111,112,113,114,115,
            121,122,123,124,125,
            301,302,303,
            311,312,313,
            321,322,323,
            331,332,333,
            341,342,343
        };

        private string[] _displayedStausVariableCode = new string[]
        {
            "CS060100030101", "CS040100030103", "CS010100060101", "CS060100030104"
        };

        private int[] _displayedStausDeviceId = new int[]
        {
            0, 0, 0, 2
        };

        private int[] _displayedStausTextboxId = new int[]
        {
            201,202,203,204,205,
            211,212,213,214,215,
            221,222,223,224,225,
            231,232,233,234,235
        };

        private string _optimalControlEnabledVariable;
        private string _optimalControlEnabledClientVariable;
        private string _workStatusVariable;

        #endregion

        #region 构造函数

        /// <summary>
        /// Initializes a new instance of the <see cref="FrmMain"/> class.
        /// </summary>
        public FrmMain(string[] args, bool isPass, Operator currentOperator)
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

                _clientName = ConfigExeSettings.GetSettingString("ClientName", "管理端");

                // 创建类实例
                IDeviceManager deviceManager = _bllFactory.BuildDeviceManager();
                _devices = deviceManager.GetAllDeviceInfo();
                IVariableManager variableManager = _bllFactory.BuildIVariableManager();
                foreach (Device device in _devices)
                {
                    device.Variables = variableManager.GetVariableByDeviceId(device.Id);
                    if (device.Name == _clientName)
                    {
                        _modbusTcpDevice = new ModbusTcpDevice()
                        {
                            IP = device.ModbusTcpDevice.IP,
                            Port = device.ModbusTcpDevice.Port,
                            UnitID = device.ModbusTcpDevice.UnitID,
                        };
                    }
                }

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
        }

        /// <summary>
        /// Loads the settings.
        /// </summary>
        private void LoadSettings()
        {
            try
            {
                _dcsName = ConfigExeSettings.GetSettingString("DCSName", "磨机工况信息");
                _clientName = ConfigExeSettings.GetSettingString("ClientName", "管理端");

                // 创建类实例
                IVariableManager variableManager = _bllFactory.BuildIVariableManager();

                foreach (Device device in _devices)
                {
                    device.Variables = variableManager.GetVariableByDeviceId(device.Id);
                    if (device.Name == _clientName)
                    {
                        _modbusTcpDevice.IP = device.ModbusTcpDevice.IP;
                        _modbusTcpDevice.Port = device.ModbusTcpDevice.Port;
                        _modbusTcpDevice.UnitID = device.ModbusTcpDevice.UnitID;
                    }
                }

                _modbusTcpVariables = variableManager.GetVariableByDeviceId(0);

                string tempString = ConfigExeSettings.GetValue("LogoffMenulist").Trim();
                if (tempString.Length > 0)
                {
                    _logoffMenuList = tempString.Split(',');
                }

                _logoffTime = ConfigExeSettings.GetSettingInt("LogoffTime", _logoffTime);
                _updateVariableTimerInterval = ConfigExeSettings.GetSettingInt("UpdateVariableTime", _updateVariableTimerInterval); //时间间隔
                _realTimerInterval = ConfigExeSettings.GetSettingInt("RealTime", _realTimerInterval); //时间间隔

                _optimalControlEnabledVariable = ConfigExeSettings.GetSettingString("OptimalControlEnabledVariable", "").Trim();
                _optimalControlEnabledClientVariable = ConfigExeSettings.GetSettingString("OptimalControlEnabledClientVariable", "").Trim();
                _workStatusVariable = ConfigExeSettings.GetSettingString("WorkStatusVariable", "").Trim();

                _masterPaneGraphRealtime.Title.Text = ConfigExeSettings.GetSettingString("MasterTitle", "My MasterPane Title");
                _masterPaneGraphRealtime.Title.FontSpec.Size = ConfigExeSettings.GetSettingSingle("MasterTitleSize", 12);

                tb_oc_104.Text = ConfigExeSettings.GetSettingString("FeedMax", "");
                tb_oc_105.Text = ConfigExeSettings.GetSettingString("FeedMin", "");
                tb_oc_114.Text = ConfigExeSettings.GetSettingString("FeedWaterMax", "");
                tb_oc_115.Text = ConfigExeSettings.GetSettingString("FeedWaterMin", "");
                tb_oc_124.Text = ConfigExeSettings.GetSettingString("SupWaterMax", "");
                tb_oc_125.Text = ConfigExeSettings.GetSettingString("SupWaterMin", "");

                tempString = ConfigExeSettings.GetValue("CurveList").Trim();
                if (tempString.Length > 0)
                {
                    string[] tempstrings = tempString.Split(',');
                    _curveList = new int[tempstrings.Length];
                    for (int index = 0; index < tempstrings.Length; index++)
                    {
                        _curveList[index] = Convert.ToInt32(tempstrings[index]);
                    }
                }

                tempString = ConfigExeSettings.GetValue("Proportion").Trim();
                if (tempString.Length > 0)
                {
                    string[] tempstrings = tempString.Split(',');
                    _proportion = new float[tempstrings.Length];
                    for (int index = 0; index < tempstrings.Length; index++)
                    {
                        _proportion[index] = Convert.ToSingle(tempstrings[index]);
                    }
                }

                _dataListLength = ConfigExeSettings.GetSettingInt("DataListLength", 720);

                ICurveManager curveManager = _bllFactory.BuildCurveManager();
                _curves.Clear();
                _curves = curveManager.GetAllCurveInfo();

                for (int index = 0; index < _displayedParaTextboxId.Length; index++)
                {
                    DisplayedParameter displayedParameter = new DisplayedParameter()
                    {
                        TextboxID = _displayedParaTextboxId[index],
                        DeviceID = _displayedParaDeviceId[index],
                        VariableCode = _displayedParaVariableCode[index],
                    };
                    _displayedParas.Add(displayedParameter);
                }

                tempString = ConfigExeSettings.GetValue("DisplayedParaVariableCode").Trim();
                if (tempString.Length > 0)
                {
                    string[] tempstrings = tempString.Split(',');
                    _displayedParaVariableCode = new string[tempstrings.Length];
                    for (int index = 0; index < tempstrings.Length; index++)
                    {
                        _displayedParaVariableCode[index] = tempstrings[index];
                    }
                }

                tempString = ConfigExeSettings.GetValue("DisplayedParaDeviceId").Trim();
                if (tempString.Length > 0)
                {
                    string[] tempstrings = tempString.Split(',');
                    _displayedParaDeviceId = new int[tempstrings.Length];
                    for (int index = 0; index < tempstrings.Length; index++)
                    {
                        _displayedParaDeviceId[index] = Convert.ToInt32(tempstrings[index]);
                    }
                }

                tempString = ConfigExeSettings.GetValue("DisplayedParaTextboxId").Trim();
                if (tempString.Length > 0)
                {
                    string[] tempstrings = tempString.Split(',');
                    _displayedParaTextboxId = new int[tempstrings.Length];
                    for (int index = 0; index < tempstrings.Length; index++)
                    {
                        _displayedParaTextboxId[index] = Convert.ToInt32(tempstrings[index]);
                    }
                }

                tempString = ConfigExeSettings.GetValue("DisplayedStausVariableCode").Trim();
                if (tempString.Length > 0)
                {
                    string[] tempstrings = tempString.Split(',');
                    _displayedStausVariableCode = new string[tempstrings.Length];
                    for (int index = 0; index < tempstrings.Length; index++)
                    {
                        _displayedStausVariableCode[index] = tempstrings[index];
                    }
                }

                tempString = ConfigExeSettings.GetValue("DisplayedStausDeviceId").Trim();
                if (tempString.Length > 0)
                {
                    string[] tempstrings = tempString.Split(',');
                    _displayedStausDeviceId = new int[tempstrings.Length];
                    for (int index = 0; index < tempstrings.Length; index++)
                    {
                        _displayedStausDeviceId[index] = Convert.ToInt32(tempstrings[index]);
                    }
                }

                tempString = ConfigExeSettings.GetValue("DisplayedStausTextboxId").Trim();
                if (tempString.Length > 0)
                {
                    string[] tempstrings = tempString.Split(',');
                    _displayedStausTextboxId = new int[tempstrings.Length];
                    for (int index = 0; index < tempstrings.Length; index++)
                    {
                        _displayedStausTextboxId[index] = Convert.ToInt32(tempstrings[index]);
                    }
                }

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
        /// Modbus TCP Listener是否创建成功
        /// </returns>
        private bool ModbusTcpCreateListener(ref ModbusTcpDevice modbusTcpDevice)
        {
            try
            {
                if ((modbusTcpDevice.IP.Equals("0.0.0.0")) || (modbusTcpDevice.Port <= 0))
                {
                    statusStrip_main.Text = "Modbus TCP 地址或端口错误！";
                }
                else
                {
                    IPAddress modbusIpAddress;
                    if (IPAddress.TryParse(modbusTcpDevice.IP, out modbusIpAddress))
                    {
                        // create and start the TCP slave
                        modbusTcpDevice.TcpListener = new TcpListener(modbusIpAddress, modbusTcpDevice.Port);
                        modbusTcpDevice.TcpListener.Start();

                        modbusTcpDevice.ModbusTcpSlave = ModbusTcpSlave.CreateTcp(modbusTcpDevice.UnitID,
                            modbusTcpDevice.TcpListener);
                        modbusTcpDevice.ModbusTcpSlave.DataStore = DataStoreFactory.CreateDefaultDataStore();
                        //modbusTcpDevice.ModbusTcpSlave.DataStore.DataStoreWrittenTo += ModbusTCP_DataStoreWriteTo_Event;
                        _modbusTcpSlaveThread = new Thread(modbusTcpDevice.ModbusTcpSlave.Listen);
                        _modbusTcpSlaveThread.Start();

                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                RecordLog.WriteLogFile("ModbusCreatTCPListener", ex.Message);
            }
            return false;
        }


        /// <summary>
        /// Get the Modbus Tcp values.
        /// </summary>
        private void ModbusTcpGetValue()
        {
            try
            {
                if (_modbusTcpSlaveCreated)
                {
                    foreach (Variable variable in _modbusTcpVariables)
                    {
                        ushort[] register = new ushort[2];
                        try
                        {
                            //读寄存器
                            register[0] =
                                _modbusTcpDevice.ModbusTcpSlave.DataStore.HoldingRegisters[variable.Address];
                            register[1] =
                                _modbusTcpDevice.ModbusTcpSlave.DataStore.HoldingRegisters[variable.Address + 1];
                        }
                        catch (Exception)
                        {
                            ModbusTcpStopComm(); //处理连接错误，重试连接
                            _modbusTcpSlaveCreated = ModbusTcpCreateListener(ref _modbusTcpDevice);
                            continue;
                        }
                        byte[] byteString = new byte[4];
                        for (int j = 0; j < 2; j++)
                        {
                            byte[] tempByte = BitConverter.GetBytes(register[j]);
                            byteString[2*j] = tempByte[0];
                            byteString[2*j + 1] = tempByte[1];
                        }
                        float value = BitConverter.ToSingle(byteString, 0); //还原用2个8位寄存器保存的1个浮点数
                        variable.Value = value;
                        variable.ProcessValueData();
                    }
                    if (_devices.Count > 0)
                    {
                        foreach (Device device in _devices)
                        {
                            if (device.Name == _clientName)
                            {
                                continue;
                            }
                            if (device.Variables.Count > 0)
                            {
                                foreach (Variable variable in device.Variables)
                                {
                                    ushort[] register = new ushort[2];
                                    try
                                    {
                                        //读寄存器
                                        register[0] =
                                            _modbusTcpDevice.ModbusTcpSlave.DataStore.HoldingRegisters[variable.Address];
                                        register[1] =
                                            _modbusTcpDevice.ModbusTcpSlave.DataStore.HoldingRegisters[variable.Address + 1];
                                    }
                                    catch (Exception)
                                    {
                                        ModbusTcpStopComm(); //处理连接错误，重试连接
                                        _modbusTcpSlaveCreated = ModbusTcpCreateListener(ref _modbusTcpDevice);
                                        continue;
                                    }
                                    byte[] byteString = new byte[4];
                                    for (int j = 0; j < 2; j++)
                                    {
                                        byte[] tempByte = BitConverter.GetBytes(register[j]);
                                        byteString[2*j] = tempByte[0];
                                        byteString[2*j + 1] = tempByte[1];
                                    }
                                    float value = BitConverter.ToSingle(byteString, 0); //还原用2个8位寄存器保存的1个浮点数
                                    variable.Value = value; 
                                    variable.ProcessValueData();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                RecordLog.WriteLogFile("ModbusTcpGetValue", ex.Message);
            }
        }

        /// <summary>
        /// Stops the communication.
        /// </summary>
        private void ModbusTcpStopComm() //通讯停止
        {
            try
            {
                if (_modbusTcpSlaveCreated)
                {
                    //_modbusTcpDevice.ModbusTcpSlave.ModbusSlaveRequestReceived -= ModbusTcp_Request_Event;
                    //_modbusTcpDevice.ModbusTcpSlave.DataStore.DataStoreWrittenTo -= ModbusTcp_DataStoreWriteTo_Event;
                    if (_modbusTcpDevice.ModbusTcpSlave != null)
                    {
                        _modbusTcpDevice.ModbusTcpSlave.Dispose();
                    }
                    _modbusTcpDevice.TcpListener.Stop();
                    _modbusTcpSlaveThread.Abort();
                    _modbusTcpSlaveCreated = false;
                }
            }
            catch (Exception ex)
            {
                RecordLog.WriteLogFile("StopModbusRTUComm", ex.Message);
            }
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
                ListViewGroup[] listGroups = new ListViewGroup[_devices.Count + 1];
                for (int index = 0; index < (_devices.Count); index++)
                {
                    if (_devices[index].State)
                    {
                        listGroups[index] = new ListViewGroup(_devices[index].Name, HorizontalAlignment.Center);
                    }
                }
                listGroups[_devices.Count] = new ListViewGroup(_dcsName, HorizontalAlignment.Center);
                listview_parainfo.Groups.AddRange(listGroups);

                if (_modbusTcpSlaveCreated)
                {
                    for (int deviceIndex = 0; deviceIndex < (_devices.Count); deviceIndex++)
                    {
                        if (_devices[deviceIndex].State)
                        {
                            List<ListViewItem> listViewItems = new List<ListViewItem>();
                            for (int paraIndex = 0; paraIndex < _devices[deviceIndex].Variables.Count; paraIndex++)
                            {
                                if (_devices[deviceIndex].Variables[paraIndex].IsEnabled &&
                                    _devices[deviceIndex].Variables[paraIndex].IsDisplayed)
                                {
                                    listViewItems.Add(
                                        new ListViewItem(
                                            new string[]
                                            {
                                                _devices[deviceIndex].Variables[paraIndex].Name,
                                                _devices[deviceIndex].Variables[paraIndex].Value.ToString("F02")
                                            },
                                            listGroups[deviceIndex])
                                        {
                                            BackColor = (paraIndex%2 == 0 ? Color.White : Color.Cyan)
                                        });
                                }
                            }
                            if (listViewItems.Count > 0)
                            {
                                listview_parainfo.Items.AddRange(listViewItems.ToArray());
                            }
                        }
                    }

                    List<ListViewItem> items = new List<ListViewItem>();
                    bool isEvenItem = false;

                    foreach (Variable variable in _modbusTcpVariables)
                    {
                        if (variable.IsDisplayed)
                        {
                            ListViewItem item =
                                new ListViewItem(
                                    new string[]
                                    {
                                        variable.Name,
                                        variable.Value.ToString("F02")
                                    },
                                    listGroups[_devices.Count])
                                {
                                    BackColor = (isEvenItem ? Color.White : Color.Cyan)
                                };
                            items.Add(item);
                            isEvenItem = !isEvenItem;
                        }
                    }
                    listview_parainfo.Items.AddRange(items.ToArray());
                }
                listview_parainfo.EndUpdate();
                //listview_parainfo.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            }
            catch (Exception ex)
            {
                RecordLog.WriteLogFile("UpdateRegisterList", ex.Message);
            }
        }

        /// <summary>
        /// Clears the graph.
        /// </summary>
        private void ClearGraph() //清空图形和信息
        {
            //zedGraphControl.MasterPane.PaneList.Clear(); //清波形图
            //zedGraphControl.MasterPane.GraphObjList.Clear();
            zgc_realtime.Invalidate(); //清空zedgraph信息
            //zgc_history.Invalidate();
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
            foreach (DataColumn column in dataTable.Columns)
            {
                foreach (Variable variable in _modbusTcpVariables)
                {
                    if (variable.Code == column.ColumnName)
                    {
                        column.ColumnName = variable.Name;
                        break;
                    }
                }

                foreach (Device device in _devices)
                {
                    foreach (Variable variable in device.Variables)
                    {
                        if (variable.Code == column.ColumnName)
                        {
                            column.ColumnName = variable.Name;
                            break;
                        }
                    }
                }
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
                foreach (Curve curve in _curves)
                {
                    PointPairList tmpDataList = new PointPairList();
                    for (int j = 1; j < dataTable.Columns.Count; j++)
                    {
                        if (dataTable.Columns[j].ColumnName.Equals(curve.Name))
                        {
                            for (int i = 0; i < dataTable.Rows.Count; i++)
                            {
                                tmpDataList.Add(new XDate(DateTime.Parse(dataTable.Rows[i][0].ToString())),
                                    Convert.ToDouble(dataTable.Rows[i][j]));
                            }
                            break;
                        }
                    }

                    Curve tmpCurve = new Curve
                    {
                        Id = curve.Id,
                        VariableCode = curve.VariableCode,
                        Name = curve.Name,
                        DeviceID = curve.DeviceID,
                        Address = curve.Address,
                        LineColor = curve.LineColor,
                        LineType = curve.LineType,
                        LineWidth = curve.LineWidth,
                        SymbolSize = curve.SymbolSize,
                        SymbolType = curve.SymbolType,
                        XTitle = curve.XTitle,
                        YTitle = curve.YTitle,
                        YMax = curve.YMax,
                        YMin = curve.YMin,
                        DataList = tmpDataList,
                    };
                    //将数据集转换成实体集合
                    curves.Add(tmpCurve);
                }
            }
            catch (Exception ex)
            {
                RecordLog.WriteLogFile("LoadHistoryCurves", ex.Message);
            }
            return curves;
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
                foreach (DisplayedParameter displayedPara in _displayedParas)
                {
                    if (displayedPara.DeviceID == 0)
                    {
                        foreach (Variable parameter in _modbusTcpVariables)
                        {
                            if (parameter.Code == displayedPara.VariableCode)
                            {
                                Control[] controls =
                                    this.Controls.Find(string.Format("tb_oc_{0}", displayedPara.TextboxID), true);
                                if (controls.Length > 0)
                                {
                                    controls[0].Text = parameter.RealValue.ToString("F");
                                }
                                break;
                            }
                        }
                    }
                    else
                    {
                        foreach (Device device in _devices)
                        {
                            if (device.Id == displayedPara.DeviceID)
                            {
                                foreach (Variable variable in device.Variables)
                                {
                                    if (variable.Code == displayedPara.VariableCode)
                                    {
                                        Control[] controls =
                                            this.Controls.Find(string.Format("tb_oc_{0}", displayedPara.TextboxID), true);
                                        if (controls.Length > 0)
                                        {
                                            controls[0].Text = variable.RealValue.ToString("F");
                                        }
                                        break;
                                    }
                                }
                                break;
                            }
                        }
                    }
                }

                for (int i = 0; i < _displayedStausVariableCode.Length; i++)
                {
                    if (_displayedStausDeviceId[i] == 0)
                    {
                        foreach (Variable parameter in _modbusTcpVariables)
                        {
                            if (parameter.Code == _displayedStausVariableCode[i])
                            {
                                Control[] controls =
                                    this.Controls.Find(string.Format("tb_oc_{0}", _displayedStausTextboxId[5*i + 0]),
                                        true);
                                if (controls.Length > 0)
                                {
                                    controls[0].Text = parameter.RealValue.ToString("F");
                                }
                                controls =
                                    this.Controls.Find(string.Format("tb_oc_{0}", _displayedStausTextboxId[5*i + 1]),
                                        true);
                                if (controls.Length > 0)
                                {
                                    controls[0].Text = parameter.Limit.HigherLimit.ToString("F");
                                }
                                controls =
                                    this.Controls.Find(string.Format("tb_oc_{0}", _displayedStausTextboxId[5*i + 2]),
                                        true);
                                if (controls.Length > 0)
                                {
                                    controls[0].Text = parameter.Limit.LowerLimit.ToString("F");
                                }
                                controls =
                                    this.Controls.Find(string.Format("tb_oc_{0}", _displayedStausTextboxId[5*i + 3]),
                                        true);
                                if (controls.Length > 0)
                                {
                                    switch (parameter.State)
                                    {
                                        case Variable.VariableState.HH:
                                            controls[0].BackColor = Color.Red;
                                            controls[0].ForeColor = Color.Yellow;
                                            controls[0].Text = "HH";
                                            break;
                                        case Variable.VariableState.H:
                                            controls[0].BackColor = Color.Red;
                                            controls[0].ForeColor = Color.Yellow;
                                            controls[0].Text = "H";
                                            break;
                                        case Variable.VariableState.N:
                                            controls[0].BackColor = Color.Green;
                                            controls[0].ResetForeColor();
                                            controls[0].Text = "N";
                                            break;
                                        case Variable.VariableState.L:
                                            controls[0].BackColor = Color.Blue;
                                            controls[0].ForeColor = Color.Yellow;
                                            controls[0].Text = "L";
                                            break;
                                        case Variable.VariableState.LL:
                                            controls[0].BackColor = Color.Blue;
                                            controls[0].ForeColor = Color.Yellow;
                                            controls[0].Text = "LL";
                                            break;
                                        default:
                                            break;
                                    }
                                }
                                controls =
                                    this.Controls.Find(string.Format("tb_oc_{0}", _displayedStausTextboxId[5*i + 4]),
                                        true);
                                if (controls.Length > 0)
                                {
                                    switch (parameter.Trend)
                                    {
                                        case Variable.VariableTrend.Uptrend:
                                            controls[0].BackColor = Color.Red;
                                            controls[0].ForeColor = Color.Yellow;
                                            break;
                                        case Variable.VariableTrend.Stable:
                                            controls[0].BackColor = Color.Green;
                                            controls[0].ResetForeColor();
                                            break;
                                        case Variable.VariableTrend.Downtrend:
                                            controls[0].BackColor = Color.Blue;
                                            controls[0].ForeColor = Color.Yellow;
                                            break;
                                        default:
                                            break;
                                    }
                                    controls[0].Text = parameter.TrendValue.ToString("F4");
                                }

                                controls = this.Controls.Find(string.Format("cb_oc_2{0}1", i), true);
                                if (controls.Length > 0)
                                {
                                    ((CheckBox) controls[0]).Checked = parameter.IsEnabled;
                                }
                                controls = this.Controls.Find(string.Format("pb_oc_2{0}1", i), true);
                                if (controls.Length > 0)
                                {
                                    controls[0].BackColor = parameter.IsValid ? Color.Green : Color.Red;
                                }
                                break;
                            }
                        }
                    }
                    else
                    {
                        foreach (Device device in _devices)
                        {
                            if (device.Id == _displayedStausDeviceId[i])
                            {
                                foreach (Variable parameter in device.Variables)
                                {
                                    if (parameter.Code == _displayedStausVariableCode[i])
                                    {
                                        Control[] controls =
                                            this.Controls.Find(
                                                string.Format("tb_oc_{0}", _displayedStausTextboxId[5*i + 0]),
                                                true);
                                        if (controls.Length > 0)
                                        {
                                            controls[0].Text = parameter.RealValue.ToString("F");
                                        }
                                        controls =
                                            this.Controls.Find(
                                                string.Format("tb_oc_{0}", _displayedStausTextboxId[5*i + 1]),
                                                true);
                                        if (controls.Length > 0)
                                        {
                                            controls[0].Text = parameter.Limit.HigherLimit.ToString("F");
                                        }
                                        controls =
                                            this.Controls.Find(
                                                string.Format("tb_oc_{0}", _displayedStausTextboxId[5*i + 2]),
                                                true);
                                        if (controls.Length > 0)
                                        {
                                            controls[0].Text = parameter.Limit.LowerLimit.ToString("F");
                                        }
                                        controls =
                                            this.Controls.Find(
                                                string.Format("tb_oc_{0}", _displayedStausTextboxId[5*i + 3]),
                                                true);
                                        if (controls.Length > 0)
                                        {
                                            switch (parameter.State)
                                            {
                                                case Variable.VariableState.HH:
                                                    controls[0].BackColor = Color.Red;
                                                    controls[0].ForeColor = Color.Yellow;
                                                    controls[0].Text = "HH";
                                                    break;
                                                case Variable.VariableState.H:
                                                    controls[0].BackColor = Color.Red;
                                                    controls[0].ForeColor = Color.Yellow;
                                                    controls[0].Text = "H";
                                                    break;
                                                case Variable.VariableState.N:
                                                    controls[0].BackColor = Color.Green;
                                                    controls[0].ResetForeColor();
                                                    controls[0].Text = "N";
                                                    break;
                                                case Variable.VariableState.L:
                                                    controls[0].BackColor = Color.Blue;
                                                    controls[0].ForeColor = Color.Yellow;
                                                    controls[0].Text = "L";
                                                    break;
                                                case Variable.VariableState.LL:
                                                    controls[0].BackColor = Color.Blue;
                                                    controls[0].ForeColor = Color.Yellow;
                                                    controls[0].Text = "LL";
                                                    break;
                                                default:
                                                    break;
                                            }
                                        }
                                        controls =
                                            this.Controls.Find(
                                                string.Format("tb_oc_{0}", _displayedStausTextboxId[5*i + 4]),
                                                true);
                                        if (controls.Length > 0)
                                        {
                                            switch (parameter.Trend)
                                            {
                                                case Variable.VariableTrend.Uptrend:
                                                    controls[0].BackColor = Color.Red;
                                                    controls[0].ForeColor = Color.Yellow;
                                                    break;
                                                case Variable.VariableTrend.Stable:
                                                    controls[0].BackColor = Color.Green;
                                                    controls[0].ResetForeColor();
                                                    break;
                                                case Variable.VariableTrend.Downtrend:
                                                    controls[0].BackColor = Color.Blue;
                                                    controls[0].ForeColor = Color.Yellow;
                                                    break;
                                                default:
                                                    break;
                                            }
                                            controls[0].Text = parameter.TrendValue.ToString("F4");
                                        }

                                        controls = this.Controls.Find(string.Format("cb_oc_2{0}1", i), true);
                                        if (controls.Length > 0)
                                        {
                                            ((CheckBox)controls[0]).Checked = parameter.IsEnabled;
                                        }
                                        controls = this.Controls.Find(string.Format("pb_oc_2{0}1", i), true);
                                        if (controls.Length > 0)
                                        {
                                            controls[0].BackColor = parameter.IsValid ? Color.Green : Color.Red;
                                        }
                                        break;
                                    }
                                }
                                break;
                            }
                        }
                    }
                }

                pb_status_1.BackColor = SystemColors.Control;//Color.Red;//
                pb_status_2.BackColor = SystemColors.Control; //Color.Orange;//
                pb_status_3.BackColor = SystemColors.Control; //Color.Green;//
                pb_status_4.BackColor = SystemColors.Control; //Color.LightBlue;//
                pb_status_5.BackColor = SystemColors.Control; //Color.Blue;//

                foreach (Variable variable in _modbusTcpVariables)
                {
                    if (variable.Code == _workStatusVariable)
                    {
                        switch ((int)variable.RealValue)
                        {
                            case 2:
                                pb_status_1.BackColor = Color.Red;
                                break;
                            case 1:
                                pb_status_2.BackColor = Color.Orange;
                                break;
                            case 0:
                                pb_status_3.BackColor = Color.Green;
                                break;
                            case -1:
                                pb_status_4.BackColor = Color.LightBlue;
                                break;
                            case -2:
                                pb_status_5.BackColor = Color.Blue;
                                break;
                            default:
                                break;
                        }
                        continue;
                    }

                    for (int i = 0; i < 3; i++)
                    {
                        if (variable.Code == _displayedParaVariableCode[2 + 5 * i])
                        {
                            Control[] controls =
                                this.Controls.Find(string.Format("pb_oc_{0}", _displayedParaTextboxId[5 * i]), true);
                            if (controls.Length > 0)
                            {
                                ((CheckBox)controls[0]).Checked = variable.IsOutput;
                            }
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                RecordLog.WriteLogFile("UpdateOcTextBox", ex.Message);
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
                // 创建权限组管理类实例
                ILogManager logManager = _bllFactory.BuildLogManager();
                // 调用实例方法
                List<Log> logCollectionTrue = logManager.GetLastLogInfos(6, true);

                // 如果包含权限组信息
                if (logCollectionTrue.Count > 0)
                {
                    // 绑定权限组数据显示
                    BindingSource source = new BindingSource();
                    source.DataSource = logCollectionTrue;
                    dgv_logs_true.DataSource = source;

                    // 设置中文列名
                    dgv_logs_true.Columns["Id"].HeaderText = "编号";
                    dgv_logs_true.Columns["Id"].DisplayIndex = 0;
                    dgv_logs_true.Columns["Id"].Visible = false;

                    dgv_logs_true.Columns["LogTime"].HeaderText = "时间";
                    dgv_logs_true.Columns["LogTime"].DisplayIndex = 1;
                    dgv_logs_true.Columns["LogTime"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss";
                    dgv_logs_true.Columns["LogTime"].MinimumWidth = 100;
                    dgv_logs_true.Columns["LogTime"].FillWeight = 200;

                    dgv_logs_true.Columns["Type"].HeaderText = "等级";
                    dgv_logs_true.Columns["Type"].DisplayIndex = 2;
                    dgv_logs_true.Columns["Type"].MinimumWidth = 50;
                    dgv_logs_true.Columns["Type"].FillWeight = 100;

                    dgv_logs_true.Columns["Content"].HeaderText = "内容";
                    dgv_logs_true.Columns["Content"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    dgv_logs_true.Columns["Content"].DisplayIndex = 3;
                    dgv_logs_true.Columns["Content"].MinimumWidth = 200;
                    dgv_logs_true.Columns["Content"].FillWeight = 1200;

                    dgv_logs_true.Columns["State"].HeaderText = "状态";
                    dgv_logs_true.Columns["State"].DisplayIndex = 4;
                    dgv_logs_true.Columns["State"].MinimumWidth = 50;
                    dgv_logs_true.Columns["State"].FillWeight = 100;
                }

                // 调用实例方法
                List<Log> logCollectionFalse = logManager.GetLastLogInfos(6, false);

                // 如果包含权限组信息
                if (logCollectionFalse.Count > 0)
                {
                    // 绑定权限组数据显示
                    BindingSource source = new BindingSource();
                    source.DataSource = logCollectionFalse;
                    dgv_logs_false.DataSource = source;

                    // 设置中文列名
                    dgv_logs_false.Columns["Id"].HeaderText = "编号";
                    dgv_logs_false.Columns["Id"].DisplayIndex = 0;
                    dgv_logs_false.Columns["Id"].Visible = false;

                    dgv_logs_false.Columns["LogTime"].HeaderText = "时间";
                    dgv_logs_false.Columns["LogTime"].DisplayIndex = 1;
                    dgv_logs_false.Columns["LogTime"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss";
                    dgv_logs_false.Columns["LogTime"].MinimumWidth = 100;
                    dgv_logs_false.Columns["LogTime"].FillWeight = 200;

                    dgv_logs_false.Columns["Type"].HeaderText = "等级";
                    dgv_logs_false.Columns["Type"].DisplayIndex = 2;
                    dgv_logs_false.Columns["Type"].MinimumWidth = 50;
                    dgv_logs_false.Columns["Type"].FillWeight = 100;

                    dgv_logs_false.Columns["Content"].HeaderText = "内容";
                    dgv_logs_false.Columns["Content"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    dgv_logs_false.Columns["Content"].DisplayIndex = 3;
                    dgv_logs_false.Columns["Content"].MinimumWidth = 200;
                    dgv_logs_false.Columns["Content"].FillWeight = 1200;

                    dgv_logs_false.Columns["State"].HeaderText = "状态";
                    dgv_logs_false.Columns["State"].DisplayIndex = 4;
                    dgv_logs_false.Columns["State"].MinimumWidth = 50;
                    dgv_logs_false.Columns["State"].FillWeight = 100;
                }
            }
            catch (Exception ex)
            {
                RecordLog.WriteLogFile("UpdateLogGrid", ex.Message);
            }
        }

        private delegate void UpdateHistoryLogGridDelegate(DateTime starTime, DateTime endTime);

        private void UpdateHistoryLogGrid(DateTime starTime, DateTime endTime)
        {
            if (InvokeRequired)
            {
                Invoke(new UpdateHistoryLogGridDelegate(UpdateHistoryLogGrid));
                return;
            }
            try
            {
                // 创建权限组管理类实例
                ILogManager logManager = _bllFactory.BuildLogManager();
                // 调用实例方法
                List<Log> logCollectionTrue = logManager.GetLogInfoByTime(starTime, endTime);

                // 如果包含权限组信息
                if (logCollectionTrue.Count > 0)
                {
                    // 绑定权限组数据显示
                    BindingSource source = new BindingSource();
                    source.DataSource = logCollectionTrue;
                    dgv_log_history.DataSource = source;

                    // 设置中文列名
                    dgv_log_history.Columns["Id"].HeaderText = "编号";
                    dgv_log_history.Columns["Id"].DisplayIndex = 0;
                    dgv_log_history.Columns["Id"].Visible = false;

                    dgv_log_history.Columns["LogTime"].HeaderText = "时间";
                    dgv_log_history.Columns["LogTime"].DisplayIndex = 1;
                    dgv_log_history.Columns["LogTime"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss";
                    dgv_log_history.Columns["LogTime"].MinimumWidth = 100;
                    dgv_log_history.Columns["LogTime"].FillWeight = 200;

                    dgv_log_history.Columns["Type"].HeaderText = "等级";
                    dgv_log_history.Columns["Type"].DisplayIndex = 2;
                    dgv_log_history.Columns["Type"].MinimumWidth = 50;
                    dgv_log_history.Columns["Type"].FillWeight = 100;

                    dgv_log_history.Columns["Content"].HeaderText = "内容";
                    dgv_log_history.Columns["Content"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    dgv_log_history.Columns["Content"].DisplayIndex = 3;
                    dgv_log_history.Columns["Content"].MinimumWidth = 200;
                    dgv_log_history.Columns["Content"].FillWeight = 1200;

                    dgv_log_history.Columns["State"].HeaderText = "状态";
                    dgv_log_history.Columns["State"].DisplayIndex = 4;
                    dgv_log_history.Columns["State"].MinimumWidth = 50;
                    dgv_log_history.Columns["State"].FillWeight = 100;
                }
                }
            catch (Exception ex)
            {
                RecordLog.WriteLogFile("UpdateHistoryLogGrid", ex.Message);
            }
        }
        /// <summary>
        /// 更新变量列表
        /// </summary>
        private void UpdateVariableConfig()
        {
            try
            {
                IVariableManager variableManager = _bllFactory.BuildIVariableManager();
                List<Variable> tmpVariables = variableManager.GetVariableByDeviceId(0);
                foreach (Variable variable in _modbusTcpVariables)
                {
                    List<Variable> variablesSelected = tmpVariables.Where(v => v.Id == variable.Id).ToList();
                    if (variablesSelected.Count > 0)
                    {
                        Variable tmpVariable = variablesSelected[0];
                        variable.Name = tmpVariable.Name;
                        variable.Address = tmpVariable.Address;
                        variable.Code = tmpVariable.Code;
                        variable.ControlPeriod = tmpVariable.ControlPeriod;
                        variable.DeviceID = tmpVariable.DeviceID;
                        variable.HistoryListLength = tmpVariable.HistoryListLength;
                        variable.IsDisplayed = tmpVariable.IsDisplayed;
                        variable.IsEnabled = tmpVariable.IsEnabled;
                        variable.IsFiltered = tmpVariable.IsFiltered;
                        variable.IsOutput = tmpVariable.IsOutput;
                        variable.IsRead = tmpVariable.IsRead;
                        variable.IsSaved = tmpVariable.IsSaved;
                        variable.Limit = tmpVariable.Limit;
                        variable.OperateDelay = tmpVariable.OperateDelay;
                        variable.Ratio = tmpVariable.Ratio;
                        variable.TrendHigherLimit = tmpVariable.TrendHigherLimit;
                        variable.TrendInterval = tmpVariable.TrendInterval;
                        variable.TrendLength = tmpVariable.TrendLength;
                        variable.TrendListLength = tmpVariable.TrendListLength;
                        variable.TrendLowerLimit = tmpVariable.TrendLowerLimit;
                    }
                }
                foreach (Device device in _devices)
                {
                    List<Variable> variables = variableManager.GetVariableByDeviceId(device.Id);
                    foreach (Variable variable in device.Variables)
                    {
                        List<Variable> variablesSelected = variables.Where(v => v.Id == variable.Id).ToList();
                        if (variablesSelected.Count > 0)
                        {
                            Variable tmpVariable = variablesSelected[0];
                            variable.Name = tmpVariable.Name;
                            variable.Address = tmpVariable.Address;
                            variable.Code = tmpVariable.Code;
                            variable.ControlPeriod = tmpVariable.ControlPeriod;
                            variable.DeviceID = tmpVariable.DeviceID;
                            variable.HistoryListLength = tmpVariable.HistoryListLength;
                            variable.IsDisplayed = tmpVariable.IsDisplayed;
                            variable.IsEnabled = tmpVariable.IsEnabled;
                            variable.IsFiltered = tmpVariable.IsFiltered;
                            variable.IsOutput = tmpVariable.IsOutput;
                            variable.IsRead = tmpVariable.IsRead;
                            variable.IsSaved = tmpVariable.IsSaved;
                            variable.Limit = tmpVariable.Limit;
                            variable.OperateDelay = tmpVariable.OperateDelay;
                            variable.Ratio = tmpVariable.Ratio;
                            variable.TrendHigherLimit = tmpVariable.TrendHigherLimit;
                            variable.TrendInterval = tmpVariable.TrendInterval;
                            variable.TrendLength = tmpVariable.TrendLength;
                            variable.TrendListLength = tmpVariable.TrendListLength;
                            variable.TrendLowerLimit = tmpVariable.TrendLowerLimit;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                RecordLog.WriteLogFile("UpdateVariableConfig", ex.Message);
            }
        }

        /// <summary>
        /// 从Modbus更新所有变量
        /// </summary>
        private void UpdateParameterValue()
        {
            try
            {
                ModbusTcpGetValue();
            }
            catch (Exception ex)
            {
                RecordLog.WriteLogFile("UpdateModbus", ex.Message);
            }
        }

        private void UpdateCurveData()
        {
            try
            {
                if (_isFirstRoundFlag)
                {
                    return;
                }
                double tmpTime = new XDate(DateTime.Now);
                foreach (Curve curve in _curves)
                {
                    if (curve.DataList.Count >= _dataListLength) //数组长度限制
                    {
                        curve.DataList.RemoveRange(0, (curve.DataList.Count - _dataListLength + 1));
                    }
                    if (curve.DeviceID == 0)
                    {
                        foreach (Variable parameter in _modbusTcpVariables)
                        {
                            if (parameter.Address != curve.Address) continue;
                            //curve.Name = parameter.Name;
                            curve.DataList.Add(tmpTime, parameter.RealValue);
                            break;
                        }
                    }
                    else
                    {
                        foreach (Device device in _devices)
                        {
                            if (device.Id == curve.DeviceID)
                            {
                                foreach (Variable parameter in device.Variables)
                                {
                                    if (parameter.Address != curve.Address) continue;
                                    //curve.Name = parameter.Name;
                                    curve.DataList.Add(tmpTime, parameter.RealValue);
                                    break;
                                }
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


        /// <summary>
        /// 将数据导出到文件.
        /// </summary>
        /// <param name="dataTable">数据表.</param>
        /// <param name="fileName">文件名.</param>
        /// <param name="splitChar">分隔符.</param>
        /// <param name="isOutputName">是否导出列名.</param>
        private void SaveDataToFlie(DataGridView dataTable, string fileName, string splitChar, bool isOutputName)
        {
            try
            {
                FileStream fs = new FileStream(fileName, FileMode.Create); //实例化一个文件流--->与写入文件相关联  
                Encoding encode = Encoding.GetEncoding("gb2312"); //实例化一个StreamWriter-->与fs相关联 
                StreamWriter sw = new StreamWriter(fs, encode);

                if (isOutputName)
                {
                    //write data column name 第一行导出列名
                    string columnname = string.Empty;
                    for (int j = 0; j < dataTable.Columns.Count; j++)
                    {
                        if (j == dataTable.Columns.Count - 1)
                            columnname += dataTable.Columns[j].Name;
                        else
                            columnname += dataTable.Columns[j].Name + splitChar;
                    }
                    sw.WriteLine(columnname);
                }
                // Call Read before accessing data.
                foreach (DataGridViewRow dataRow in dataTable.Rows)
                {
                    string linecontent = string.Empty;
                    for (int j = 0; j < dataTable.Columns.Count; j++)
                    {
                        if (j == dataTable.Columns.Count - 1)
                            linecontent += dataRow.Cells[j].Value.ToString();
                        else
                            linecontent += dataRow.Cells[j].Value.ToString() + splitChar;
                    }
                    sw.WriteLine(linecontent);
                }
                //清空缓冲区
                sw.Flush();
                //关闭流  
                sw.Close();
                fs.Close();
            }
            catch (Exception ex)
            {
                RecordLog.WriteLogFile("WriteCSVFlie", ex.Message);
            }
        }


        void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            // 这里是后台线程， 是在另一个线程上完成的
            // 这里是真正做事的工作线程
            // 可以在这里做一些费时的，复杂的操作

            //e.Result = e.Argument + "工作线程完成";
        }

        void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //这时后台线程已经完成，并返回了主线程，所以可以直接使用UI控件了 
            //this.label4.Text = e.Result.ToString();
        }



        #endregion 

        #region 控件响应

        /// <summary>
        /// Handles the Tick event of the timer_realtime control.
        /// </summary>
        private void TimerUpdateVariableElapsed(object o)
        {
            if (!_realTimerFlag) return;
            try
            {
                UpdateVariableConfig();
                UpdateParameterValue();
                UpdateRegisterList();
                UpdateCurveData();
                UpdateOcTextBox();
                UpdateLogGrid();
                IDataManager dataManager = _bllFactory.BuildDataManager();

                foreach (Variable variable in _modbusTcpVariables)
                {
                    if (variable.Code == _optimalControlEnabledVariable)
                    {
                        if (variable.Value > 0)
                        {
                            this.Invoke((EventHandler) (delegate
                            {
                                //tsbtn_rule_run.Enabled = false;
                                tsbtn_rule_stop.Enabled = true;
                            }));
                        }
                        else
                        {
                            this.Invoke((EventHandler) (delegate
                            {
                                //tsbtn_rule_run.Enabled = true;
                                tsbtn_rule_stop.Enabled = false;
                            }));
                        }
                        continue;
                    }
                }

                foreach (Device device in _devices)
                {
                    if (device.Name == _clientName)
                    {
                        foreach (Variable variable in device.Variables)
                        {
                            if (variable.Code == _displayedParaVariableCode[3])
                            {
                                double tmpValue;
                                if (double.TryParse(tb_oc_104.Text.Trim(), out tmpValue))
                                {
                                    variable.RealValue = tmpValue;
                                }
                                if (_modbusTcpSlaveCreated)
                                {
                                    byte[] tempByte = BitConverter.GetBytes(Convert.ToSingle(variable.Value));
                                    _modbusTcpDevice.ModbusTcpSlave.DataStore.HoldingRegisters[variable.Address] =
                                        Convert.ToUInt16(tempByte[1] * 256 + tempByte[0]);
                                    _modbusTcpDevice.ModbusTcpSlave.DataStore.HoldingRegisters[variable.Address + 1] =
                                        Convert.ToUInt16(tempByte[3] * 256 + tempByte[2]);
                                }
                                continue;
                            }
                            if (variable.Code == _displayedParaVariableCode[4])
                            {
                                double tmpValue;
                                if (double.TryParse(tb_oc_105.Text.Trim(), out tmpValue))
                                {
                                    variable.RealValue = tmpValue;
                                }
                                if (_modbusTcpSlaveCreated)
                                {
                                    byte[] tempByte = BitConverter.GetBytes(Convert.ToSingle(variable.Value));
                                    _modbusTcpDevice.ModbusTcpSlave.DataStore.HoldingRegisters[variable.Address] =
                                        Convert.ToUInt16(tempByte[1] * 256 + tempByte[0]);
                                    _modbusTcpDevice.ModbusTcpSlave.DataStore.HoldingRegisters[variable.Address + 1] =
                                        Convert.ToUInt16(tempByte[3] * 256 + tempByte[2]);
                                }
                                continue;
                            }
                            if (variable.Code == _displayedParaVariableCode[8])
                            {
                                double tmpValue;
                                if (double.TryParse(tb_oc_114.Text.Trim(), out tmpValue))
                                {
                                    variable.RealValue = tmpValue;
                                }
                                if (_modbusTcpSlaveCreated)
                                {
                                    byte[] tempByte = BitConverter.GetBytes(Convert.ToSingle(variable.Value));
                                    _modbusTcpDevice.ModbusTcpSlave.DataStore.HoldingRegisters[variable.Address] =
                                        Convert.ToUInt16(tempByte[1] * 256 + tempByte[0]);
                                    _modbusTcpDevice.ModbusTcpSlave.DataStore.HoldingRegisters[variable.Address + 1] =
                                        Convert.ToUInt16(tempByte[3] * 256 + tempByte[2]);
                                }
                                continue;
                            }
                            if (variable.Code == _displayedParaVariableCode[9])
                            {
                                double tmpValue;
                                if (double.TryParse(tb_oc_115.Text.Trim(), out tmpValue))
                                {
                                    variable.RealValue = tmpValue;
                                }
                                if (_modbusTcpSlaveCreated)
                                {
                                    byte[] tempByte = BitConverter.GetBytes(Convert.ToSingle(variable.Value));
                                    _modbusTcpDevice.ModbusTcpSlave.DataStore.HoldingRegisters[variable.Address] =
                                        Convert.ToUInt16(tempByte[1] * 256 + tempByte[0]);
                                    _modbusTcpDevice.ModbusTcpSlave.DataStore.HoldingRegisters[variable.Address + 1] =
                                        Convert.ToUInt16(tempByte[3] * 256 + tempByte[2]);
                                }
                                continue;
                            }
                            if (variable.Code == _displayedParaVariableCode[13])
                            {
                                double tmpValue;
                                if (double.TryParse(tb_oc_124.Text.Trim(), out tmpValue))
                                {
                                    variable.RealValue = tmpValue;
                                }
                                if (_modbusTcpSlaveCreated)
                                {
                                    byte[] tempByte = BitConverter.GetBytes(Convert.ToSingle(variable.Value));
                                    _modbusTcpDevice.ModbusTcpSlave.DataStore.HoldingRegisters[variable.Address] =
                                        Convert.ToUInt16(tempByte[1] * 256 + tempByte[0]);
                                    _modbusTcpDevice.ModbusTcpSlave.DataStore.HoldingRegisters[variable.Address + 1] =
                                        Convert.ToUInt16(tempByte[3] * 256 + tempByte[2]);
                                }
                                continue;
                            }
                            if (variable.Code == _displayedParaVariableCode[14])
                            {
                                double tmpValue;
                                if (double.TryParse(tb_oc_125.Text.Trim(), out tmpValue))
                                {
                                    variable.RealValue = tmpValue;
                                }
                                if (_modbusTcpSlaveCreated)
                                {
                                    byte[] tempByte = BitConverter.GetBytes(Convert.ToSingle(variable.Value));
                                    _modbusTcpDevice.ModbusTcpSlave.DataStore.HoldingRegisters[variable.Address] =
                                        Convert.ToUInt16(tempByte[1] * 256 + tempByte[0]);
                                    _modbusTcpDevice.ModbusTcpSlave.DataStore.HoldingRegisters[variable.Address + 1] =
                                        Convert.ToUInt16(tempByte[3] * 256 + tempByte[2]);
                                }
                                continue;
                            }
                        }
                        break;
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
        /// Handles the Elapsed event of the LogoffTimer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Timers.ElapsedEventArgs" /> instance containing the event data.</param>
        private void TimerFirstRoundElapsed(object sender)
        {
            if (_timerFirstRound != null)
            {
                _timerFirstRound.Dispose();
            }
            _isFirstRoundFlag = false;
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
                        if (_timerUpdateVariable != null)
                        {
                            _timerUpdateVariable.Dispose();
                        }
                        if (_timerFirstRound != null)
                        {
                            _timerFirstRound.Dispose();
                        }
                        ModbusTcpStopComm();
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
            LoadSettings();
            try
            {
                IDataManager dataManager = _bllFactory.BuildDataManager();

                //读取历史数据
                //DateTime endTime = DateTime.Now;
                //DateTime startTime = endTime.AddSeconds((-1) * (_updateVariableTimerInterval / 1000) * _dataListLength);
                //IDataManager dataManager = _bllFactory.BuildDataManager();
                //foreach (Curve curve in _curves)
                //{
                //    List<Data> dataCollection = dataManager.GetDataByVariableCode(curve.VariableCode,
                //        curve.DeviceID, startTime, endTime);
                //    foreach (Data tmpData in dataCollection)
                //    {
                //        curve.DataList.Add(
                //            new XDate(DateTime.Parse(tmpData.TimeValue.ToString())),
                //            Convert.ToDouble(tmpData.Value));
                //    }
                //}

                if (!_modbusTcpSlaveCreated)
                {
                    _modbusTcpSlaveCreated = ModbusTcpCreateListener(ref _modbusTcpDevice);
                }
                if (_modbusTcpSlaveCreated)
                {
                    _realTimerFlag = true;
                    _timerUpdateVariable = new System.Threading.Timer(TimerUpdateVariableElapsed, null, 0,
                        _updateVariableTimerInterval);

                    _timerFirstRound = new System.Threading.Timer(TimerFirstRoundElapsed, null, 8, 0);

                    foreach (Device device in _devices)
                    {
                        if (device.Name == _clientName)
                        {
                            foreach (Variable variable in device.Variables)
                            {
                                if (variable.Code == _optimalControlEnabledClientVariable)
                                {
                                    Data data = dataManager.GetLastDataByVariableCode(_optimalControlEnabledVariable, 0);
                                    if (data.TimeValue - DateTime.Now < TimeSpan.FromSeconds(10))
                                    {
                                        variable.Value = data.Value;
                                        byte[] tempByte = BitConverter.GetBytes(Convert.ToSingle(variable.Value));
                                        _modbusTcpDevice.ModbusTcpSlave.DataStore.HoldingRegisters[variable.Address] =
                                            Convert.ToUInt16(tempByte[1]*256 + tempByte[0]);
                                        _modbusTcpDevice.ModbusTcpSlave.DataStore.HoldingRegisters[variable.Address + 1]
                                            =
                                            Convert.ToUInt16(tempByte[3]*256 + tempByte[2]);
                                        break;
                                    }
                                }
                            }
                            break;
                        }
                    }

                    // 加载权限菜单
                    RightsMenuDataManager rmManager = new RightsMenuDataManager();
                    rmManager.LoadMenuRightsItem(msMain, _currentOperator.RightsCollection);
                    menu_control_run.Enabled = false;
                    menu_config_config.Enabled = false;
                    menu_config_user.Enabled = false;
                    menu_config_devices.Enabled = false;
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

        /// <summary>
        /// Handles the Click event of the tool_btn_rtstop control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btn_stop_Click(object sender, EventArgs e)
        {
            _realTimerFlag = false;
            if (_timerUpdateVariable != null)
            {
                _timerUpdateVariable.Dispose();
            }
            // 加载权限菜单
            RightsMenuDataManager rmManager = new RightsMenuDataManager();
            rmManager.LoadMenuRightsItem(msMain, _currentOperator.RightsCollection);
            menu_control_stop.Enabled = false;
            menu_file_login.Enabled = false;
            menu_file_logoff.Enabled = true;
            SynchroButton();

            ClearGraph();
            //ModbusTcpStopComm();
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

                //using (BackgroundWorker backgroundWorker = new BackgroundWorker())
                //{
                //    backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
                //    backgroundWorker.DoWork += new DoWorkEventHandler(bw_DoWork);
                //    backgroundWorker.RunWorkerAsync();
                //} 

                DataTable dataTable = LoadHistoryData(startTime, endTime);
                dgv_data.DataSource = dataTable;
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
                UpdateHistoryLogGrid(startTime, endTime);
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
                UpdateHistoryLogGrid(startTime, endTime);
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
                UpdateHistoryLogGrid(startTime, endTime);
                dtp_data_start.Value = startTime;
                dtp_data_end.Value = endTime;
                status_Label.Text = string.Format("查询到从{0}到{1}共计{2}行数据！",
                    startTime.ToString("yyyy-MM-dd HH:mm:ss"),
                    endTime.AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss"),
                    dataTable.Rows.Count);
            }
        }


        private void btn_use_rate_Click(object sender, EventArgs e)
        {
            DateTime startTime = dtp_data_start.Value; //查询起始时间
            DateTime endTime = dtp_data_end.Value; //查询截止时间
            if (endTime > startTime)
            {
                IDataManager dataManager = _bllFactory.BuildDataManager();
                double useRate = dataManager.GetUseRateByTime(_optimalControlEnabledVariable, startTime, endTime);
                tb_use_rate.Text = useRate.ToString("P");
            }
        }

        private void btn_use_prev_Click(object sender, EventArgs e)
        {
            DateTime endTime = dtp_data_start.Value; //查询截止时间
            DateTime startTime = dtp_data_start.Value - (dtp_data_end.Value - dtp_data_start.Value); //查询起始时间
            if (endTime > startTime)
            {
                IDataManager dataManager = _bllFactory.BuildDataManager();
                double useRate = dataManager.GetUseRateByTime(_optimalControlEnabledVariable, startTime, endTime);
                tb_use_rate.Text = useRate.ToString("P");
                dtp_data_start.Value = startTime;
                dtp_data_end.Value = endTime;
            }
        }

        private void btn_use_next_Click(object sender, EventArgs e)
        {
            DateTime startTime = dtp_data_end.Value; //查询起始时间
            DateTime endTime = dtp_data_end.Value + (dtp_data_end.Value - dtp_data_start.Value); //查询截止时间
            if (endTime > startTime)
            {
                IDataManager dataManager = _bllFactory.BuildDataManager();
                double useRate = dataManager.GetUseRateByTime(_optimalControlEnabledVariable, startTime, endTime);
                tb_use_rate.Text = useRate.ToString("P");
                dtp_data_start.Value = startTime;
                dtp_data_end.Value = endTime;
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
            if (_currentOperator != null)
            {
                RecordLog.WriteLogFile("Logoff", string.Format("{0} Logoff!", _currentOperator.Name));
                _currentOperator = null;
            }
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

        private void menu_config_rules_Click(object sender, EventArgs e)
        {
            frmRulesManager rulesForm = new frmRulesManager();
            rulesForm.ShowDialog();
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

        private void tsbtn_rule_run_Click(object sender, EventArgs e)
        {
            //tsbtn_rule_run.Enabled = false;
            tsbtn_rule_stop.Enabled = true;
            foreach (Device device in _devices)
            {
                if (device.Name == _clientName)
                {
                    foreach (Variable variable in device.Variables)
                    {
                        if (variable.Code == _optimalControlEnabledClientVariable)
                        {
                            variable.Value = 1;
                            byte[] tempByte = BitConverter.GetBytes(Convert.ToSingle(variable.Value));
                            if (_modbusTcpSlaveCreated)
                            {
                                _modbusTcpDevice.ModbusTcpSlave.DataStore.HoldingRegisters[variable.Address] =
                                    Convert.ToUInt16(tempByte[1]*256 + tempByte[0]);
                                _modbusTcpDevice.ModbusTcpSlave.DataStore.HoldingRegisters[variable.Address + 1] =
                                    Convert.ToUInt16(tempByte[3]*256 + tempByte[2]);
                            }
                            break;
                        }
                    }
                    break;
                }
            }
        }

        private void tsbtn_rule_stop_Click(object sender, EventArgs e)
        {
            //tsbtn_rule_run.Enabled = true;
            tsbtn_rule_stop.Enabled = false;
            foreach (Device device in _devices)
            {
                if (device.Name == _clientName)
                {
                    foreach (Variable variable in device.Variables)
                    {
                        if (variable.Code == _optimalControlEnabledClientVariable)
                        {
                            variable.Value = 0;
                            byte[] tempByte = BitConverter.GetBytes(Convert.ToSingle(variable.Value));
                            if (_modbusTcpSlaveCreated)
                            {
                                _modbusTcpDevice.ModbusTcpSlave.DataStore.HoldingRegisters[variable.Address] =
                                    Convert.ToUInt16(tempByte[1] * 256 + tempByte[0]);
                                _modbusTcpDevice.ModbusTcpSlave.DataStore.HoldingRegisters[variable.Address + 1] =
                                    Convert.ToUInt16(tempByte[3] * 256 + tempByte[2]);
                            }
                            break;
                        }
                    }
                    break;
                }
            }
        }

        private void btn_data_export_Click(object sender, EventArgs e)
        {
            if (dgv_data.RowCount > 0)
            {
                FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog
                {
                    SelectedPath = AppDomain.CurrentDomain.BaseDirectory
                };
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK) //选定存储目录
                {
                    string saveDir = folderBrowserDialog.SelectedPath; //选定存储目录
                    string fileName = string.Format("{0}\\{1}.csv", saveDir, DateTime.Now.ToString("yyyyMMddHHmmss"));
                    SaveDataToFlie(dgv_data,fileName,",",true);
                }
            }
        }


        private void cb_oc_2x1_CheckedChanged(object sender, EventArgs e)
        {
            /*
            CheckBox checkBox = (CheckBox) sender;
            string variableCode = "";
            switch (checkBox.Name)
            {
                case "cb_oc_201":
                    variableCode = _displayedStausVariableCode[0];
                    break;
                case "cb_oc_211":
                    variableCode = _displayedStausVariableCode[1];
                    break;
                case "cb_oc_221":
                    variableCode = _displayedStausVariableCode[2];
                    break;
                case "cb_oc_231":
                    variableCode = _displayedStausVariableCode[3];
                    break;
                default:
                    break;
            }
            foreach (Variable parameter in _modbusTcpVariables)
            {
                if (parameter.Code == variableCode)
                {
                    parameter.IsEnabled = checkBox.Checked;
                    IVariableManager variableManager = _bllFactory.BuildIVariableManager();
                    variableManager.ModifyVariable(parameter);
                    parameter.SetValueToModbusTcpMaster(ref _modbusTcpDevice);
                    return;
                }
            }
            foreach (Device device in _devices)
            {
                foreach (Variable variable in device.Variables)
                {
                    if (variable.Code == variableCode)
                    {
                        variable.IsEnabled = checkBox.Checked;
                        IVariableManager variableManager = _bllFactory.BuildIVariableManager();
                        variableManager.ModifyVariable(variable);
                        variable.SetValueToModbusTcpMaster(ref _modbusTcpDevice);
                        return;
                    }
                }
            }
             * */
        }

        private void cb_oc_1x1_CheckedChanged(object sender, EventArgs e)
        {
            /*
            CheckBox checkBox = (CheckBox)sender;
            string variableCode = "";
            switch (checkBox.Name)
            {
                case "cb_oc_101":
                    variableCode = _displayedParaVariableCode[2];
                    break;
                case "cb_oc_111":
                    variableCode = _displayedParaVariableCode[7];
                    break;
                case "cb_oc_121":
                    variableCode = _displayedParaVariableCode[12];
                    break;
                default:
                    break;
            }
            foreach (Variable parameter in _modbusTcpVariables)
            {
                if (parameter.Code == variableCode)
                {
                    parameter.IsOutput = checkBox.Checked;
                    IVariableManager variableManager = _bllFactory.BuildIVariableManager();
                    variableManager.ModifyVariable(parameter);
                    parameter.SetValueToModbusTcpMaster(ref _modbusTcpDevice);
                    return;
                }
            }
            * */
        }


        private void tb_oc_104_TextChanged(object sender, EventArgs e)
        {
            ConfigExeSettings.SetSettingInt("FeedMax", tb_oc_104.Text.Trim());
        }

        private void tb_oc_105_TextChanged(object sender, EventArgs e)
        {
            ConfigExeSettings.SetSettingInt("FeedMin", tb_oc_105.Text.Trim());
        }

        private void tb_oc_114_TextChanged(object sender, EventArgs e)
        {
            ConfigExeSettings.SetSettingInt("FeedWaterMax", tb_oc_114.Text.Trim());
        }

        private void tb_oc_115_TextChanged(object sender, EventArgs e)
        {
            ConfigExeSettings.SetSettingInt("FeedWaterMin", tb_oc_115.Text.Trim());
        }

        private void tb_oc_124_TextChanged(object sender, EventArgs e)
        {
            ConfigExeSettings.SetSettingInt("SupWaterMax", tb_oc_124.Text.Trim());
        }

        private void tb_oc_125_TextChanged(object sender, EventArgs e)
        {
            ConfigExeSettings.SetSettingInt("SupWaterMin", tb_oc_125.Text.Trim());
        }
        #endregion


    }
}