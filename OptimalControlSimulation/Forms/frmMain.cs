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
using System.Linq;
using System.Net;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Net.Sockets;
using System.Timers;
using IBLL;
using IBLL.Control;
using MathWorks.MATLAB.NET.Arrays;
using Modbus.Device;
using Modbus.Data;
using Model.Control;
using Model.Modbus;
using Model;
using Utility;
using OptimalControl.Common;

namespace OptimalControl.Forms
{
    public enum GetValueType
    {
        RealValue = 0,
        CurrentValue = 1,
        HistoryValue = 2,
        InitialValue = 3,
        State = 4,
        Trend = 5,
        TrendValue = 6,
        HigherLimit = 7,
        LowerLimit = 8,
        UltimateHighLimit = 9,
        UltimateLowLimit = 10,
        IsOutput = 11,
        IsValid = 12,
    }

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

        /// <summary>
        /// The real timer flag
        /// </summary>
        private bool _realTimerFlag;

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

        private string _clientName;

        private MWArray _inPara = new MWNumericArray(MWArrayComplexity.Real, 5);
        private MWArray _outPara = new MWNumericArray(MWArrayComplexity.Real, 81);
        private MWArray _inoutPara1 = new MWNumericArray(MWArrayComplexity.Real, 25);
        private MWArray _inoutPara2 = new MWNumericArray(MWArrayComplexity.Real, 1);

        private double[] _inputDouble = new double[5];
        private double[] _outputDouble = new double[81];

        private readonly int _errorValue = -8888888;

        private string[] _inputVariable = new string[5];

        private DashanSAG.Class1 _myFunc = new DashanSAG.Class1();

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

            try
            {
                if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "cache")) //检查cache目录是否已创建
                    Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "cache"); //若尚未创建，则创建目录

                ofd_history.InitialDirectory = (AppDomain.CurrentDomain.BaseDirectory + "cache");

                status_Label.Text = "";

                _clientName = "仿真模型";

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

                _timerLogoff = new System.Timers.Timer(_logoffTime*1000);
                _timerLogoff.Elapsed += TimerLogoffElapsed;
                _timerLogoff.Start();

                double[] inoutDouble1 = new double[]
                {
                    10.1845, 26.0613, 23.4841, 26.5585, 15.5917, 3.7754, 4.9364, 2.9173, 2.4530, 2.0779, 1.6827, 2.1499,
                    2.3013, 2.3465, 2.1909, 2.3046, 2.1924, 2.2873, 2.2659, 2.1000, 1.9334, 1.7176, 1.4578, 1.1315,
                    6.4885
                };
                double inoutDouble2 = 0.83;

                _inoutPara1 = (MWNumericArray) inoutDouble1;
                _inoutPara2 = (MWNumericArray) inoutDouble2;
            }
            catch (Exception ex)
            {
                RecordLog.WriteLogFile("Initialization", ex.Message);
            }
        }

        #endregion

        #region 方法
        
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
        /// 刷新按钮状态.托管
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
                _updateVariableTimerInterval = ConfigExeSettings.GetSettingInt("UpdateVariableTime",
                    _updateVariableTimerInterval); //时间间隔

                for (int index = 0; index < _inputVariable.Length; index++)
                {
                    _inputVariable[index] = ConfigAppSettings.GetSettingString("InputVariable" + (index + 1), "");
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
                                            _modbusTcpDevice.ModbusTcpSlave.DataStore.HoldingRegisters[
                                                variable.Address + 1];
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

        /// <summary>
        /// Gets the value of the variable by name.
        /// </summary>
        /// <param name="variableName">Name of the variable.</param>
        private object GetValueByName(string variableName)
        {
            try
            {
                GetValueType getValueType = GetValueType.RealValue;
                foreach (string name in Enum.GetNames(typeof(GetValueType)))
                {
                    if (variableName.EndsWith(name))
                    {
                        variableName = variableName.Replace(string.Format(".{0}", name), "");
                        getValueType = (GetValueType)Enum.Parse(typeof(GetValueType), name, true);
                        break;
                    }
                }

                foreach (Variable parameter in _modbusTcpVariables)
                {
                    if (parameter.Name == variableName)
                    {
                        switch (getValueType)
                        {
                            case GetValueType.RealValue:
                                return parameter.RealValue;
                                break;
                            case GetValueType.CurrentValue:
                                return parameter.CurrentValue;
                                break;
                            case GetValueType.HistoryValue:
                                return parameter.HistoryValue;
                                break;
                            case GetValueType.InitialValue:
                                return parameter.InitialValue;
                                break;
                            case GetValueType.State:
                                return (double)parameter.State;
                                break;
                            case GetValueType.Trend:
                                return (double)parameter.Trend;
                                break;
                            case GetValueType.TrendValue:
                                return parameter.TrendValue;
                                break;
                            case GetValueType.HigherLimit:
                                return parameter.Limit.HigherLimit;
                                break;
                            case GetValueType.LowerLimit:
                                return parameter.Limit.LowerLimit;
                                break;
                            case GetValueType.UltimateHighLimit:
                                return parameter.Limit.UltimateHighLimit;
                                break;
                            case GetValueType.UltimateLowLimit:
                                return parameter.Limit.UltimateLowLimit;
                                break;
                            case GetValueType.IsOutput:
                                return parameter.IsOutput;
                                break;
                            case GetValueType.IsValid:
                                return parameter.IsValid;
                                break;
                            default:
                                return parameter.RealValue;
                                break;
                        }
                    }
                }

                foreach (Device device in _devices)
                {
                    foreach (Variable parameter in device.Variables)
                    {
                        if (parameter.Name == variableName)
                        {
                            switch (getValueType)
                            {
                                case GetValueType.RealValue:
                                    return parameter.RealValue;
                                    break;
                                case GetValueType.CurrentValue:
                                    return parameter.CurrentValue;
                                    break;
                                case GetValueType.HistoryValue:
                                    return parameter.HistoryValue;
                                    break;
                                case GetValueType.InitialValue:
                                    return parameter.InitialValue;
                                    break;
                                case GetValueType.State:
                                    return (double)parameter.State;
                                    break;
                                case GetValueType.Trend:
                                    return (double)parameter.Trend;
                                    break;
                                case GetValueType.TrendValue:
                                    return parameter.TrendValue;
                                    break;
                                case GetValueType.IsOutput:
                                    return parameter.IsOutput;
                                    break;
                                case GetValueType.IsValid:
                                    return parameter.IsValid;
                                    break;
                                default:
                                    return parameter.RealValue;
                                    break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                RecordLog.WriteLogFile("GetValueByName", ex.Message);
            }
            return _errorValue;
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
                IDataManager dataManager = _bllFactory.BuildDataManager();

                for (int index = 0; index < _inputDouble.Length; index++)
                {
                    if (_inputVariable[index] != "")
                    {
                        _inputDouble[index] = Convert.ToDouble(GetValueByName(_inputVariable[index]));
                    }
                }

                _inputDouble[3] = 1;//矿性代码
                _inputDouble[4] = 5;//时间间隔

                _inPara = (MWNumericArray)_inputDouble;

                MWArray[] agrsOut = new MWArray[3];//3个输出参数，一定要写数量
                MWArray[] agrsIn = new MWArray[] { _inPara, _inoutPara1, _inoutPara2 };

                _myFunc.DashanSAG(3, ref agrsOut, agrsIn);
                _outPara = agrsOut[0];
                _inoutPara1 = agrsOut[1];
                _inoutPara2 = agrsOut[2];
                _outputDouble = (double[])((MWNumericArray)_outPara).ToVector(MWArrayComponent.Real);

                double[] result = new double[5];
                result[0] = _outputDouble[0];
                result[1] = _outputDouble[1];
                result[2] = _outputDouble[27];
                result[3] = _outputDouble[77] + _outputDouble[78] + _outputDouble[79];
                result[4] = 1 - _outputDouble[75] - _outputDouble[76] - result[3];

                foreach (Device device in _devices)
                {
                    if (device.Name == _clientName)
                    {
                        int index = 0;
                        foreach (Variable variable in device.Variables)
                        {
                            byte[] tempByte = BitConverter.GetBytes(Convert.ToSingle(result[index++]));
                            _modbusTcpDevice.ModbusTcpSlave.DataStore.HoldingRegisters[variable.Address] =
                                Convert.ToUInt16(tempByte[1]*256 + tempByte[0]);
                            _modbusTcpDevice.ModbusTcpSlave.DataStore.HoldingRegisters[variable.Address + 1] =
                                Convert.ToUInt16(tempByte[3]*256 + tempByte[2]);
                        }
                        break;
                    }
                }

                this.Invoke((EventHandler)(delegate
                {
                    textBox1.Text = _inputDouble[0].ToString("F");
                    textBox2.Text = _inputDouble[1].ToString("F");
                    textBox3.Text = _inputDouble[2].ToString("F");
                    textBox4.Text = _inputDouble[3].ToString("N");
                    textBox5.Text = _inputDouble[4].ToString("N");
                    textBox6.Text = result[0].ToString("P2");
                    textBox7.Text = result[1].ToString("P2");
                    textBox8.Text = result[2].ToString("F");
                    textBox9.Text = result[3].ToString("P2");
                    textBox10.Text = result[4].ToString("P2");
                }));

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
                if (!_modbusTcpSlaveCreated)
                {
                    _modbusTcpSlaveCreated = ModbusTcpCreateListener(ref _modbusTcpDevice);
                }
                if (_modbusTcpSlaveCreated)
                {
                    _realTimerFlag = true;
                    _timerUpdateVariable = new System.Threading.Timer(TimerUpdateVariableElapsed, null, 0,
                        _updateVariableTimerInterval);

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

            //ModbusTcpStopComm();
            status_Label.Text = "停止.";
            _isRunning = false;
            RecordLog.WriteLogFile("Stop", string.Format("Software stoped by {0}!", _currentOperator.Name));
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
               // frmConfig settingForm = new frmConfig();

                //if (settingForm.ShowDialog() == DialogResult.OK) //设置面板确定
                //{
                //    LoadSettings(); //配置更新
                //}
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
            MessageBox.Show("半自磨仿真模型软件 V1.0.0.1" + Environment.NewLine + "北京矿冶研究总院", "关于软件", MessageBoxButtons.OK,
                MessageBoxIcon.Information);
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
        
        #endregion

    }
}