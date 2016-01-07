using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.IO;
using System.IO.Ports;
using System.Net.Sockets;
using System.Web.UI.WebControls;
using ExpertSystem;
using IBLL;
using IBLL.Control;
using Modbus.Device;
using Modbus.Data;
using Model.Control;
using Model.Modbus;
using Model;
using Utility;
using Rule = Model.Control.Rule;

namespace OptimalControlService
{
    public partial class OptimalControlService : ServiceBase
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

        #region 全局变量

        /// <summary>
        /// 创建工厂类
        /// </summary>
        private BLLFactory.BLLFactory _bllFactory = new BLLFactory.BLLFactory();
        private IRuleManager _ruleManager;

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
        private System.Threading.Timer _timerFirstRound;

        /// <summary>
        /// The real timer flag
        /// </summary>
        private bool _realTimerFlag = false;

        private bool _execteRulesFlag = false;


        /// <summary>
        /// The real timer interval
        /// </summary>
        private int _updateVariableTimerInterval = 10000; //设置定时器间隔，默认为10000ms

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
        private bool _isModbusRtuSlaveCreated;

        /// <summary>
        /// The modbus rtu slave updated
        /// </summary>
        private bool _isModbusRtuSlaveUpdated;

        /// <summary>
        /// The devices
        /// </summary>
        private List<Device> _devices = new List<Device>();

        private List<Rule> _rules = new List<Rule>();

        private int _defaultControlPeriod = 5;

        private Log _historyLog = new Log();

        private string _optimalControlEnabledVariable;
        private string _optimalControlHeartBeatVariable;

        private string _optimalControlFeedVariable;
        private string _optimalControlFeedMax;
        private string _optimalControlFeedMin;

        private string _optimalControlFeedWaterVariable;
        private string _optimalControlFeedWaterMax;
        private string _optimalControlFeedWaterMin;

        private string _optimalControlSupWaterVariable;
        private string _optimalControlSupWaterMax;
        private string _optimalControlSupWaterMin;

        private string _feedVariable;
        private string _feedWaterVariable;
        private string _supWaterVariable;

        private bool _isRuleTriggered = false;
        //private bool _isFirstRound = true;

        private string _optimalControlEnabledClientVariable;
        private string _workStatusVariableCode;
        private string _workStatusVariableName = "半自磨工作状态";
        private string _remoteStatusVariableCode;
        private string _clientName;

        private readonly int _errorValue = -8888888;
        #endregion

        #region 构造函数

        public OptimalControlService()
        {

            InitializeComponent();

            try
            {
                if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "cache")) //检查cache目录是否已创建
                    Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "cache"); //若尚未创建，则创建目录
                _ruleManager = _bllFactory.BuildRuleManager();
                LoadSettings();
            }
            catch (Exception ex)
            {
                RecordLog.WriteLogFile("Initialization", ex.Message);
            }

        }

        #endregion

        #region 方法

        /// <summary>
        /// Loads the settings.
        /// </summary>
        private void LoadSettings()
        {
            try
            {
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

                _updateVariableTimerInterval = ConfigAppSettings.GetSettingInt("UpdateVariableTime", _updateVariableTimerInterval); //时间间隔

                _realTimerInterval = ConfigAppSettings.GetSettingInt("RealTime", _realTimerInterval); //时间间隔

                _modbusRtuDevice.SerialPortObject = new SerialPort
                    (
                    ConfigAppSettings.GetSettingString("ModbusRTUPortName", "COM1"),
                    ConfigAppSettings.GetSettingInt("ModbusRTUBaudRate", 19200),
                    Parity.None,
                    ConfigAppSettings.GetSettingInt("ModbusRTUDataBits", 8),
                    (StopBits)ConfigAppSettings.GetSettingSingle("ModbusRTUStopBits", 1)
                    );

                _modbusRtuDevice.UnitID = ConfigAppSettings.GetSettingByte("ModbusRTUDeviceID", 1);


                _optimalControlEnabledVariable = ConfigAppSettings.GetSettingString("OptimalControlEnabledVariable", "").Trim();
                _optimalControlHeartBeatVariable = ConfigAppSettings.GetSettingString("OptimalControlHeartBeatVariable", "").Trim();
                _optimalControlFeedVariable = ConfigAppSettings.GetSettingString("OptimalControlFeedVariable", "").Trim();
                _optimalControlFeedMax = ConfigAppSettings.GetSettingString("OptimalControlFeedMax", "").Trim();
                _optimalControlFeedMin = ConfigAppSettings.GetSettingString("OptimalControlFeedMin", "").Trim();
                
                _optimalControlFeedWaterVariable = ConfigAppSettings.GetSettingString("OptimalControlFeedWaterVariable", "").Trim();
                _optimalControlFeedWaterMax = ConfigAppSettings.GetSettingString("OptimalControlFeedWaterMax", "").Trim();
                _optimalControlFeedWaterMin = ConfigAppSettings.GetSettingString("OptimalControlFeedWaterMin", "").Trim();

                _optimalControlSupWaterVariable = ConfigAppSettings.GetSettingString("OptimalControlSupWaterVariable", "").Trim();
                _optimalControlSupWaterMax = ConfigAppSettings.GetSettingString("OptimalControlSupWaterMax", "").Trim();
                _optimalControlSupWaterMin = ConfigAppSettings.GetSettingString("OptimalControlSupWaterMin", "").Trim();

                _feedVariable = ConfigAppSettings.GetSettingString("FeedVariable", "").Trim();
                _feedWaterVariable = ConfigAppSettings.GetSettingString("FeedWaterVariable", "").Trim();
                _supWaterVariable = ConfigAppSettings.GetSettingString("SupWaterVariable", "").Trim();

                _optimalControlEnabledClientVariable = ConfigAppSettings.GetSettingString("OptimalControlEnabledClientVariable", "").Trim();
                _workStatusVariableCode = ConfigAppSettings.GetSettingString("WorkStatusVariable", "").Trim();
                _clientName = ConfigAppSettings.GetSettingString("ClientName", "管理端");
                _remoteStatusVariableCode = ConfigAppSettings.GetSettingString("RemoteStatusVariableCode", "").Trim();
            }
            catch (Exception ex)
            {
                RecordLog.WriteLogFile("LoadSettings", ex.Message);
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
                foreach (Variable variable in _modbusRtuParameters)
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
                    if (variable.Code == _workStatusVariableCode)
                    {
                        _workStatusVariableName = variable.Name;
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
                    RecordLog.WriteLogFile("ModbusRTUCreatListener", string.Format("未打开端口{0}(端口不存在)!", modbusRtuDevice.SerialPortObject.PortName));
                }
                else if (modbusRtuDevice.SerialPortObject.IsOpen)
                {
                    RecordLog.WriteLogFile("ModbusRTUCreatListener", string.Format("未打开端口{0}(已经被打开)!", modbusRtuDevice.SerialPortObject.PortName));
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
                RecordLog.WriteLogFile("ModbusRTUCreatListener", ex.Message);
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
            _isModbusRtuSlaveUpdated = true;
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
                if (_isModbusRtuSlaveCreated)
                {
                    foreach (Variable variable in _modbusRtuParameters)
                    {
                        if (!variable.IsRead)
                        {
                            continue;
                        }
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
                                    (ushort)(variable.Address - 1),
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
                            byteString[2 * j] = tempByte[0];
                            byteString[2 * j + 1] = tempByte[1];
                        }
                        float value = BitConverter.ToSingle(byteString, 0); //还原用2个8位寄存器保存的1个浮点数
                        variable.Value = value;
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
                if (_isModbusRtuSlaveCreated)
                {
                    _modbusRtuSlave.ModbusSlaveRequestReceived -= ModbusRTU_Request_Event;
                    _modbusRtuSlave.DataStore.DataStoreWrittenTo -= ModbusRTU_DataStoreWriteTo_Event;
                    _modbusRtuDevice.SerialPortObject.Close();
                    if (_modbusRtuSlave != null)
                    {
                        _modbusRtuSlave.Dispose();
                    }
                    _modbusRtuSlaveThread.Abort();
                    _isModbusRtuSlaveCreated = false;
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
                return false;
            }
        }

        /// <summary>
        /// Get The Modbus TCP  value.
        /// </summary>
        private void ModbusTCPGetValue()
        {
            try
            {
                foreach (Device device in _devices)
                {
                    if (!device.State) return;
                    foreach (Variable variable in device.Variables)
                    {
                        if (!variable.IsRead)
                        {
                            continue;
                        }
                        ushort[] register;
                        try
                        {
                            //读寄存器
                            register =
                                device.ModbusTcpDevice.ModbusTcpMaster.ReadHoldingRegisters(
                                    device.ModbusTcpDevice.UnitID,
                                    (ushort)(variable.Address - 1), 2);
                            device.ModbusTcpMasterUpdated = true;
                        }
                        catch (Exception)
                        {
                            device.ModbusTcpMasterCreated = !ModbusTCPStopComm(device.ModbusTcpDevice); //处理连接错误，重试连接
                            device.ModbusTcpMasterCreated = ModbusTCPCreateClient(ref device.ModbusTcpDevice);
                            continue;
                        }

                        if (_isModbusRtuSlaveCreated)
                        {
                            try
                            {
                                _modbusRtuSlave.DataStore.HoldingRegisters[variable.Address] =
                                    register[0];
                                _modbusRtuSlave.DataStore.HoldingRegisters[variable.Address + 1] =
                                    register[1];
                            }
                            catch (Exception)
                            {
                                ModbusRTUStopComm(); //处理连接错误，重试连接
                                ModbusRTUCreatListener(_modbusRtuDevice);
                                //RecordLog.WriteLogFile(LogFile, "ModbusTCP->ModbusRTU", ex.Message);
                            }
                        }

                        foreach (Device d in _devices)
                        {
                            if (!d.State ||
                                !d.SyncState ||
                                !d.ModbusTcpMasterCreated ||
                                d.Id == device.Id)
                            {
                                continue;
                            }
                            try
                            {
                                //读寄存器
                                d.ModbusTcpDevice.ModbusTcpMaster.WriteMultipleRegisters(
                                    d.ModbusTcpDevice.UnitID,
                                    (ushort)(variable.Address - 1),
                                    register);
                            }
                            catch (Exception)
                            {
                                ModbusTCPStopComm(d.ModbusTcpDevice); //处理连接错误，重试连接
                                ModbusTCPCreateClient(ref d.ModbusTcpDevice);
                            }
                        }

                        byte[] byteString = new byte[4];
                        for (int j = 0; j < 2; j++)
                        {
                            byte[] tempByte = BitConverter.GetBytes(register[j]);
                            byteString[2 * j] = tempByte[0];
                            byteString[2 * j + 1] = tempByte[1];
                        }
                        float value = BitConverter.ToSingle(byteString, 0); //还原用2个8位寄存器保存的1个浮点数
                        variable.Value = value;
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
        private void ExecuteRules(List<Rule> rules, bool isPriorityZero)
        {
            try
            {
                List<int> periods = new List<int>();
                bool isRulePriority3Triggered = false;
                foreach (Rule rule in rules)
                {
                    if (isPriorityZero)
                    {
                        if (rule.Priority != 0)
                        {
                            continue;
                        }
                    }
                    else
                    {
                        if (rule.Priority == 0)
                        {
                            continue;
                        }
                    }
                    //优先级为0和3的规则不受_execteRulesFlag控制
                    if (!_execteRulesFlag && rule.Priority != 0 && rule.Priority != 3)
                    {
                        continue;
                    }

                    //优先级为3的规则的工况判断不受_isRuleTriggered控制
                    if (rule.Priority != 3)
                    {
                        if (_isRuleTriggered)
                        {
                            //Type为true的规则不受_isRuleTriggered控制
                            if (!rule.Type)
                            {
                                continue;
                            }
                        }
                    }
                    else
                    {
                        //优先级为3的规则受isRulePriority3Triggered控制
                        if (isRulePriority3Triggered)
                        {
                            continue;
                        }
                    }

                    bool isTure = false;
                    string expString = rule.Expression;
                    if (expString.ToLower() == "true" || expString == "1")
                    {
                        isTure = true;
                    }
                    else
                    {
                        StringBuilder expStringBuilder = new StringBuilder();
                        foreach (string s in expString.Trim(new char[] {'[', ']'}).Split(new char[] {'[', ']'}))
                        {
                            if (s.StartsWith("@"))
                            {
                                string valueString = GetValueByName(s.TrimStart('@')).ToString().ToLower();
                                if (valueString != "true" && valueString != "false")
                                {
                                    double value = 0;
                                    if (double.TryParse(valueString, out value))
                                    {
                                        if (value < 0)
                                        {
                                            if (Math.Abs(value - _errorValue) < 1E-06)
                                            {
                                                continue;
                                            }
                                            expStringBuilder.Append(string.Format("(0{0})", value));
                                        }
                                        else
                                        {
                                            expStringBuilder.Append(value);
                                        }
                                    }
                                    else
                                    {
                                        expStringBuilder.Append(valueString);
                                    }
                                }
                                else
                                {
                                    expStringBuilder.Append(valueString);
                                }
                            }
                            else
                            {
                                expStringBuilder.Append(s);
                            }
                        }
                        //AddLogInfo(new Log
                        //{
                        //    LogTime = DateTime.Now,
                        //    Content = string.Format("{0} {1}", rule.Name, expStringBuilder.ToString()),
                        //});

                        if (expStringBuilder.Length == 0) continue;

                        RPN expRPN = new RPN();
                        if (expRPN.Parse(expStringBuilder.ToString()))
                        {
                            bool.TryParse(expRPN.Evaluate().ToString(), out isTure);
                        }
                    }
                    if (!isTure)
                    {
                        continue;
                    }
                    string[] operationString = rule.Operation.Split(';');
                    List<Variable> opVariables = new List<Variable>();
                    foreach (string opString in operationString)
                    {
                        StringBuilder opStringBuilder = new StringBuilder();
                        string[] op = opString.Trim(new char[] {'[', ']'}).Split(new char[] {'[', ']'});
                        //如果是一条赋值的语句
                        if (op.Length > 1 && op[0].StartsWith("@") && op[1].StartsWith("="))
                        {
                            //如果优先级为3，而且处于过渡过程，则只判断工况，不执行操作
                            if (rule.Priority == 3)
                            {
                                if (_isRuleTriggered || (!_execteRulesFlag))
                                {
                                    if (op[0].TrimStart('@') != _workStatusVariableName)
                                    {
                                        continue;
                                    }
                                }
                            }

                            op[1] = op[1].TrimStart('=');
                            for (int index = 1; index < op.Length; index++)
                            {
                                if (op[index].StartsWith("@"))
                                {
                                    string valueString =
                                        GetValueByName(op[index].TrimStart('@')).ToString().ToLower();
                                    if (valueString != "true" && valueString != "false")
                                    {
                                        double value = 0;
                                        if (double.TryParse(valueString, out value))
                                        {
                                            if (value < 0)
                                            {
                                                if (Math.Abs(value - _errorValue) < 1E-06)
                                                {
                                                    continue;
                                                }
                                                opStringBuilder.Append(string.Format("(0{0})", value));
                                            }
                                            else
                                            {
                                                opStringBuilder.Append(value);
                                            }
                                        }
                                        else
                                        {
                                            opStringBuilder.Append(valueString);
                                        }
                                    }
                                    else
                                    {
                                        opStringBuilder.Append(valueString);
                                    }
                                }
                                else
                                {
                                    opStringBuilder.Append(op[index]);
                                }
                            }

                            //AddLogInfo(new Log
                            //{
                            //    LogTime = DateTime.Now,
                            //    Content = string.Format("{0} {1}", rule.Name, opStringBuilder.ToString()),
                            //});

                            if (opStringBuilder.Length == 0) continue;
                            RPN opRPN = new RPN();
                            if (opRPN.Parse(opStringBuilder.ToString()))
                            {
                                string resultString = opRPN.Evaluate().ToString().ToLower();
                                if (resultString != "true" && resultString != "false")
                                {
                                    opVariables.Add(new Variable()
                                    {
                                        Name = op[0].TrimStart('@'),
                                        Value = Convert.ToDouble(resultString),
                                        Ratio = 1,
                                    });
                                }
                                else
                                {
                                    opVariables.Add(new Variable()
                                    {
                                        Name = op[0].TrimStart('@'),
                                        IsValid = Convert.ToBoolean(resultString),
                                        Ratio = 0,
                                    });
                                }
                            }
                        }
                    }

                    //AddLogInfo(new Log
                    //{
                    //    LogTime = DateTime.Now,
                    //    Content = string.Format("{0} {1}", rule.Name, opVariables.Count),
                    //});

                    if (opVariables.Count <= 0)
                    {
                        continue;
                    }

                    StringBuilder tempString =
                        new StringBuilder(string.Format("触发规则\"{0}\",执行操作\"", rule.Name));
                    foreach (Variable opVariable in opVariables)
                    {
                        GetValueType getValueType = GetValueType.RealValue;
                        foreach (string name in Enum.GetNames(typeof (GetValueType)))
                        {
                            if (opVariable.Name.EndsWith(name))
                            {
                                opVariable.Name = opVariable.Name.Replace(string.Format(".{0}", name), "");
                                getValueType = (GetValueType) Enum.Parse(typeof (GetValueType), name, true);
                                break;
                            }
                        }

                        foreach (Variable parameter in _modbusRtuParameters)
                        {
                            if (parameter.Name == opVariable.Name)
                            {
                                IVariableManager variableManager = _bllFactory.BuildIVariableManager();
                                switch (getValueType)
                                {
                                    case GetValueType.RealValue:
                                        if (parameter.Code == _optimalControlFeedVariable)
                                        {
                                            double max = GetValueByCode(_optimalControlFeedMax);
                                            double min = GetValueByCode(_optimalControlFeedMin);
                                            if (opVariable.Value >= parameter.RealValue)
                                            {
                                                if ((Math.Abs(max - _errorValue) > 1E-06)
                                                    && (Math.Abs(max) > 1E-06))
                                                {
                                                    if (opVariable.Value > max)
                                                    {
                                                        opVariable.Value = max;
                                                    }
                                                }
                                            }
                                            if (opVariable.Value <= parameter.RealValue)
                                            {
                                                if ((Math.Abs(min - _errorValue) > 1E-06)
                                                    && (Math.Abs(min) > 1E-06))
                                                {
                                                    if (opVariable.Value < min)
                                                    {
                                                        opVariable.Value = min;
                                                    }
                                                }
                                            }
                                        }
                                        else if (parameter.Code == _optimalControlFeedWaterVariable)
                                        {
                                            double max = GetValueByCode(_optimalControlFeedWaterMax);
                                            double min = GetValueByCode(_optimalControlFeedWaterMin);
                                            if (opVariable.Value >= parameter.RealValue)
                                            {
                                                if ((Math.Abs(max - _errorValue) > 1E-06)
                                                    && (Math.Abs(max) > 1E-06))
                                                {
                                                    if (opVariable.Value > max)
                                                    {
                                                        opVariable.Value = max;
                                                    }
                                                }
                                            }
                                            if (opVariable.Value <= parameter.RealValue)
                                            {
                                                if ((Math.Abs(min - _errorValue) > 1E-06)
                                                    && (Math.Abs(min) > 1E-06))
                                                {
                                                    if (opVariable.Value < min)
                                                    {
                                                        opVariable.Value = min;
                                                    }
                                                }
                                            }
                                        }
                                        else if (parameter.Code == _optimalControlSupWaterVariable)
                                        {
                                            double max = GetValueByCode(_optimalControlSupWaterMax);
                                            double min = GetValueByCode(_optimalControlSupWaterMin);

                                            if (opVariable.Value >= parameter.RealValue)
                                            {
                                                if ((Math.Abs(max - _errorValue) > 1E-06)
                                                    && (Math.Abs(max) > 1E-06))
                                                {
                                                    if (opVariable.Value > max)
                                                    {
                                                        opVariable.Value = max;
                                                    }
                                                }
                                            }
                                            if (opVariable.Value <= parameter.RealValue)
                                            {
                                                if ((Math.Abs(min - _errorValue) > 1E-06)
                                                    && (Math.Abs(min) > 1E-06))
                                                {
                                                    if (opVariable.Value < min)
                                                    {
                                                        opVariable.Value = min;
                                                    }
                                                }
                                            }
                                        }
                                        parameter.RealValue = opVariable.Value;
                                        if (_isModbusRtuSlaveCreated)
                                        {
                                            parameter.SetValueToModbusSlave(ref _modbusRtuSlave);
                                        }
                                        foreach (Device device in _devices)
                                        {
                                            if (device.ModbusTcpMasterCreated)
                                            {
                                                parameter.SetValueToModbusTcpMaster(
                                                    ref device.ModbusTcpDevice);
                                            }
                                        }
                                        break;
                                    case GetValueType.InitialValue:
                                        parameter.InitialValue = opVariable.Value;
                                        break;
                                    case GetValueType.HigherLimit:
                                        parameter.Limit = new Variable.VariableLimit()
                                        {
                                            HigherLimit = opVariable.Value,
                                            LowerLimit = parameter.Limit.LowerLimit,
                                            UltimateHighLimit = parameter.Limit.UltimateHighLimit,
                                            UltimateLowLimit = parameter.Limit.UltimateLowLimit,
                                        };
                                        variableManager.ModifyVariable(parameter);
                                        break;
                                    case GetValueType.LowerLimit:
                                        parameter.Limit = new Variable.VariableLimit()
                                        {
                                            HigherLimit = parameter.Limit.HigherLimit,
                                            LowerLimit = opVariable.Value,
                                            UltimateHighLimit = parameter.Limit.UltimateHighLimit,
                                            UltimateLowLimit = parameter.Limit.UltimateLowLimit,
                                        };
                                        variableManager.ModifyVariable(parameter);
                                        break;
                                    case GetValueType.UltimateHighLimit:
                                        parameter.Limit = new Variable.VariableLimit()
                                        {
                                            HigherLimit = parameter.Limit.HigherLimit,
                                            LowerLimit = parameter.Limit.LowerLimit,
                                            UltimateHighLimit = opVariable.Value,
                                            UltimateLowLimit = parameter.Limit.UltimateLowLimit,
                                        };
                                        variableManager.ModifyVariable(parameter);
                                        break;
                                    case GetValueType.UltimateLowLimit:
                                        parameter.Limit = new Variable.VariableLimit()
                                        {
                                            HigherLimit = parameter.Limit.HigherLimit,
                                            LowerLimit = parameter.Limit.LowerLimit,
                                            UltimateHighLimit = parameter.Limit.UltimateHighLimit,
                                            UltimateLowLimit = opVariable.Value,
                                        };
                                        variableManager.ModifyVariable(parameter);
                                        break;
                                    case GetValueType.IsValid:
                                        parameter.IsValid = opVariable.IsValid;
                                        break;
                                    case GetValueType.IsOutput:
                                        parameter.IsOutput = opVariable.IsValid;
                                        break;
                                    case GetValueType.CurrentValue:
                                        break;
                                    case GetValueType.HistoryValue:
                                        break;
                                    case GetValueType.State:
                                        break;
                                    case GetValueType.Trend:
                                        break;
                                    case GetValueType.TrendValue:
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            }
                        }
                        tempString.Append(string.Format("{0}.{1}={2}; ", opVariable.Name,
                            getValueType.ToString(), opVariable.Value));
                    }

                    tempString.Append("\"");

                    if (rule.IsLogged)
                    {
                        if (((!_isRuleTriggered) || (rule.Priority != 3)) && _execteRulesFlag)
                        {
                            Log addLog = new Log()
                            {
                                LogTime = DateTime.Now,
                                Content = tempString.ToString(),
                                State = true,
                            };
                            switch (rule.Priority)
                            {
                                case 1:
                                    addLog.Type = Log.LogType.严重;
                                    break;
                                case 2:
                                    addLog.Type = Log.LogType.建议;
                                    break;
                                case 3:
                                    addLog.Type = Log.LogType.提示;
                                    break;
                                default:
                                    addLog.Type = Log.LogType.提示;
                                    break;
                            }
                            AddLogInfo(addLog);
                        }
                    }
                    //
                    if (((rule.Priority != 0) && (rule.Priority != 3)) ||
                        ((!_isRuleTriggered) && (rule.Priority == 3)))
                    {
                        _isRuleTriggered = true;
                        int period = rule.Period > 0 ? rule.Period*1000 : _defaultControlPeriod*1000;
                        periods.Add(period);
                    }

                    if (rule.Priority == 3)
                    {
                        isRulePriority3Triggered = true;
                    }
                }
                if (periods.Count > 0)
                {
                    //bool isVariableUpadated = false;
                    //foreach (Variable variable in _modbusRtuParameters)
                    //{
                    //    if (variable.Code == _optimalControlFeedVariable)
                    //    {
                    //        if (Math.Abs(variable.RealValue - variable.HistoryValue) > 1E-06)
                    //        {
                    //            isVariableUpadated = true;
                    //            break;
                    //        }
                    //        continue;
                    //    }
                    //    if (variable.Code == _optimalControlFeedWaterVariable)
                    //    {
                    //        if (Math.Abs(variable.RealValue - variable.HistoryValue) > 1E-06)
                    //        {
                    //            isVariableUpadated = true;
                    //            break;
                    //        }
                    //        continue;
                    //    }
                    //    if (variable.Code == _optimalControlSupWaterVariable)
                    //    {
                    //        if (Math.Abs(variable.RealValue - variable.HistoryValue) > 1E-06)
                    //        {
                    //            isVariableUpadated = true;
                    //            break;
                    //        }
                    //        continue;
                    //    }
                    //}

                    //if (isVariableUpadated)
                    //{
                    //    periods.Add(0);
                    //}

                    _timerRuleDelay = new System.Threading.Timer(TimerRuleDelayElapsed, null, periods.Min(), 0);

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
                        getValueType = (GetValueType) Enum.Parse(typeof(GetValueType),name,true);
                        break;
                    }
                }

                foreach (Variable parameter in _modbusRtuParameters)
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
                                return (double) parameter.State;
                                break;
                            case GetValueType.Trend:
                                return (double) parameter.Trend;
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

        /// <summary>
        /// Gets the value of the variable by code.
        /// </summary>
        /// <param name="variableCode">Code of the variable.</param>
        private double GetValueByCode(string variableCode)
        {
            try
            {
                IVariableManager variableManager = _bllFactory.BuildIVariableManager();
                Variable tmpVariable = variableManager.GetVariableInfoByCode(variableCode);
                if (tmpVariable.DeviceID == 0)
                {
                    foreach (Variable parameter in _modbusRtuParameters)
                    {
                        if (parameter.Code == variableCode)
                        {
                            return parameter.RealValue;
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
                                if (variable.Code == variableCode)
                                {
                                    return variable.RealValue;
                                }
                            }
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                RecordLog.WriteLogFile("GetValueByCode", ex.Message);
            }
            return _errorValue;
        }

        /// <summary>
        /// 将变量保存到数据库中
        /// </summary>
        private void SaveParameters()
        {
            try
            {
                bool modbusTcpMasterUpdated = false;
                for (int index = 0; index < _devices.Count; index++)
                {
                    if (_devices[index].ModbusTcpMasterUpdated)
                    {
                        modbusTcpMasterUpdated = true;
                        break;
                    }
                }
                if (_isModbusRtuSlaveUpdated || modbusTcpMasterUpdated)
                {
                    DateTime time = DateTime.Now;
                    if (SaveParameter(_modbusRtuParameters, time))
                    {
                        _isModbusRtuSlaveUpdated = false;
                    }
                    foreach (Device device in _devices)
                    {
                        if (!device.State) continue;
                        if (!SaveParameter(device.Variables, time)) continue;
                        device.ModbusTcpMasterUpdated = false;
                    }
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
                if (parameter.IsSaved)
                {
                    Data data = new Data()
                    {
                        VariableCode = parameter.Code,
                        TimeValue = time,
                        Value = parameter.RealValue,
                        DeviceID = Convert.ToInt32(parameter.DeviceID),
                        State = parameter.State,
                        Trend = parameter.Trend,
                        TrendValue = parameter.TrendValue,
                    };
                    addData.Add(data);
                }
            }
            if (addData.Count > 0)
            {
                IDataManager dataManager = _bllFactory.BuildDataManager();
                bool result = dataManager.AddData(addData);
                return result;
            }
            else
            {
                return true;
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
        /// 从Modbus更新所有变量
        /// </summary>
        private void UpdateVariableValue()
        {
            try
            {
                ModbusTCPGetValue();

                if (_isModbusRtuSlaveCreated)
                {
                    ModbusRTUGetValue();
                }
            }
            catch (Exception ex)
            {
                RecordLog.WriteLogFile("UpdateModbus", ex.Message);
            }
        }

        private void CheckParameterState()
        {
            try
            {
                foreach (Variable parameter in _modbusRtuParameters)
                {
                    Variable.VariableState state = parameter.State;
                    if (state == Variable.VariableState.HH || state == Variable.VariableState.LL)
                    {
                        Log addLog = new Log()
                        {
                            LogTime = DateTime.Now,
                            Content = string.Format("变量[{0}]={1}, {2}", parameter.Name, parameter.RealValue, state),
                            Type =
                                (state == Variable.VariableState.HH || state == Variable.VariableState.LL)
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
                            Variable.VariableState state = parameter.State;
                            if (state == Variable.VariableState.HH || state == Variable.VariableState.LL)
                            {
                                Log addLog = new Log()
                                {
                                    LogTime = DateTime.Now,
                                    Content =
                                        string.Format("变量[{0}]={1}, {2}", parameter.Name, parameter.RealValue, state),
                                    Type =
                                        (state == Variable.VariableState.HH || state == Variable.VariableState.LL)
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
        private void TimerUpdateVariableElapsed(object o)
        {
            if (!_realTimerFlag) return;
            try
            {
                UpdateVariableConfig();
                UpdateVariableValue();

                foreach (Variable parameter in _modbusRtuParameters)
                {
                    if (parameter.Code == _optimalControlEnabledVariable)
                    {
                        foreach (Device device in _devices)
                        {
                            if (device.Name == _clientName)
                            {
                                foreach (Variable variable in device.Variables)
                                {
                                    if (variable.Code == _optimalControlEnabledClientVariable)
                                    {
                                        //if ((variable.Value == 1) && (variable.HistoryValue == 0))
                                        //{
                                        //    parameter.Value = 1;
                                        //    if (_isModbusRtuSlaveCreated)
                                        //    {
                                        //        parameter.SetValueToModbusSlave(ref _modbusRtuSlave);
                                        //    }
                                        //    break;
                                        //}
                                        //else 
                                        if ((variable.Value == 0) && (variable.HistoryValue == 1))
                                        {
                                            parameter.Value = 0;
                                            if (_isModbusRtuSlaveCreated)
                                            {
                                                parameter.SetValueToModbusSlave(ref _modbusRtuSlave);
                                            }
                                            break;
                                        }

                                        if ((parameter.Value == 1) && (parameter.HistoryValue == 0))
                                        {
                                            variable.Value = 1;
                                            if (device.ModbusTcpMasterCreated)
                                            {
                                                variable.SetValueToModbusTcpMaster(ref device.ModbusTcpDevice);
                                            }
                                            break;
                                        }
                                        else if ((parameter.Value == 0) && (parameter.HistoryValue == 1))
                                        {
                                            variable.Value = 0;
                                            if (device.ModbusTcpMasterCreated)
                                            {
                                                variable.SetValueToModbusTcpMaster(ref device.ModbusTcpDevice);
                                            }
                                            break;
                                        }
                                    }
                                }
                                break;
                            }
                        }

                        double realValue = 0;
                        double histroyValue = 0;
                        foreach (Variable variable in _modbusRtuParameters)
                        {
                            if (variable.Code == _remoteStatusVariableCode)
                            {
                                realValue = variable.RealValue;
                                histroyValue = variable.HistoryValue;
                                break;
                            }
                        }
                        if (realValue == 1 && ((histroyValue == 0) || (histroyValue == 2)))
                        {
                            parameter.Value = 1;
                            if (_isModbusRtuSlaveCreated)
                            {
                                parameter.SetValueToModbusSlave(ref _modbusRtuSlave);
                            }
                        }
                        else if (realValue == 0)
                        {
                            parameter.Value = 0;
                            if (_isModbusRtuSlaveCreated)
                            {
                                parameter.SetValueToModbusSlave(ref _modbusRtuSlave);
                            }
                        }

                        if ((parameter.Value == 1) && (parameter.HistoryValue == 0))
                        {
                            _isRuleTriggered = false;
                            if (_timerRuleDelay != null)
                            {
                                _timerRuleDelay.Dispose();
                            } _execteRulesFlag = false;
                            _timerFirstRound = new System.Threading.Timer(TimerFirstRoundElapsed, null, 8, 0);
                            RecordLog.WriteLogFile("Start", "Optimal control started!");
                        }
                        else if (parameter.Value == 0)
                        {
                            _isRuleTriggered = false;
                            if (_timerRuleDelay != null)
                            {
                                _timerRuleDelay.Dispose();
                            } _execteRulesFlag = false;
                            //RecordLog.WriteLogFile("Stop", "Optimal control stoped!");
                        }
                        continue;
                    }

                    if (!_execteRulesFlag)
                    {
                        if (parameter.Code == _optimalControlFeedVariable)
                        {
                            double tmpValue = GetValueByCode(_feedVariable);
                            if (Math.Abs(tmpValue - _errorValue) > 1E-06)
                            {
                                parameter.RealValue = tmpValue;
                                parameter.InitialValue = tmpValue;
                            }
                            else
                            {
                                parameter.RealValue = parameter.Limit.LowerLimit;
                            }
                            if (_isModbusRtuSlaveCreated)
                            {
                                parameter.SetValueToModbusSlave(ref _modbusRtuSlave);
                            }
                            continue;
                        }
                        if (parameter.Code == _optimalControlFeedWaterVariable)
                        {
                            double tmpValue = GetValueByCode(_feedWaterVariable);
                            if (Math.Abs(tmpValue - _errorValue) > 1E-06)
                            {
                                parameter.RealValue = tmpValue;
                                parameter.InitialValue = tmpValue;
                            }
                            else
                            {
                                parameter.RealValue = parameter.Limit.LowerLimit;
                            }
                            if (_isModbusRtuSlaveCreated)
                            {
                                parameter.SetValueToModbusSlave(ref _modbusRtuSlave);
                            }
                            continue;
                        }
                        if (parameter.Code == _optimalControlSupWaterVariable)
                        {
                            double tmpValue = GetValueByCode(_supWaterVariable);
                            if (Math.Abs(tmpValue - _errorValue) > 1E-06)
                            {
                                parameter.RealValue = tmpValue;
                                parameter.InitialValue = tmpValue;
                            }
                            else
                            {
                                parameter.RealValue = parameter.Limit.LowerLimit;
                            }
                            if (_isModbusRtuSlaveCreated)
                            {
                                parameter.SetValueToModbusSlave(ref _modbusRtuSlave);
                            }
                            continue;
                        }
                    }
                    if ((parameter.Code == _feedVariable)
                        || (parameter.Code == _feedWaterVariable)
                        || (parameter.Code == _supWaterVariable))
                    {
                        double stepThreshold = 1E-06;
                        if (parameter.Code == _feedVariable)
                            stepThreshold = 5;
                        else if (parameter.Code == _feedWaterVariable)
                            stepThreshold = 1;
                        else if (parameter.Code == _supWaterVariable)
                            stepThreshold = 10;

                        if (Math.Abs(parameter.HistoryValue - parameter.RealValue) > stepThreshold)
                        {
                            //if (!_execteRulesFlag)
                            //{
                            //    long period = 300000;
                            //    if (parameter.ControlPeriod > 0)
                            //    {
                            //        period = parameter.ControlPeriod*1000;
                            //    }
                            //    if (_isRuleTriggered)
                            //    {
                            //        _timerRuleDelay.Dispose();
                            //    }
                            //    _isRuleTriggered = true;
                            //    _timerRuleDelay = new System.Threading.Timer(TimerRuleDelayElapsed, null, period, 0);
                            //}
                            Log addLog = new Log()
                            {
                                LogTime = DateTime.Now,
                                Type = Log.LogType.提示,
                                Content =
                                    string.Format("{0}由{1}修改为{2}",
                                        parameter.Name,
                                        parameter.HistoryValue,
                                        parameter.RealValue),
                                State = false,
                            };
                            AddLogInfo(addLog);
                        }
                        continue;
                    }
                    if (parameter.Code == _workStatusVariableCode)
                    {
                        parameter.Value = 0;
                        continue;
                    }
                }
                
                _rules = _ruleManager.GetRuleInfoEnabled();

                ExecuteRules(_rules, true);

                foreach (Variable variable in _modbusRtuParameters)
                {
                    variable.ProcessValueData();
                }
                foreach (Device device in _devices)
                {
                    foreach (Variable variable in device.Variables)
                    {
                        variable.ProcessValueData();
                    }
                }

                ExecuteRules(_rules, false);
                foreach (Variable variable in _modbusRtuParameters)
                {
                    variable.UpdateHistoryValue();
                }
                foreach (Device device in _devices)
                {
                    foreach (Variable variable in device.Variables)
                    {
                        variable.UpdateHistoryValue();
                    }
                }

                CheckParameterState();
                SaveParameters();

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
                if (_isModbusRtuSlaveCreated)
                {

                    foreach (Variable variable in _modbusRtuParameters)
                    {
                        if (!variable.IsRead)
                        {
                            continue;
                        }

                        if (variable.Code != _remoteStatusVariableCode)
                        {
                            continue;
                        }
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
                        variable.Value = value;
                    }
                }

                foreach (Variable parameter in _modbusRtuParameters)
                {
                    if (parameter.Code == _optimalControlEnabledVariable)
                    {
                        double realValue = 0;
                        double histroyValue = 0;
                        foreach (Variable variable in _modbusRtuParameters)
                        {
                            if (variable.Code == _remoteStatusVariableCode)
                            {
                                realValue = variable.RealValue;
                                histroyValue = variable.HistoryValue;
                                break;
                            }
                        }
                        if (realValue == 1 && ((histroyValue == 0)||(histroyValue ==2)))
                        {
                            parameter.Value = 1;
                            if (_isModbusRtuSlaveCreated)
                            {
                                parameter.SetValueToModbusSlave(ref _modbusRtuSlave);
                            }
                        }
                        else if (realValue == 0)
                        {
                            parameter.Value = 0;
                            if (_isModbusRtuSlaveCreated)
                            {
                                parameter.SetValueToModbusSlave(ref _modbusRtuSlave);
                            }
                        }

                        if ((parameter.Value == 1) && (parameter.HistoryValue == 0))
                        {

                            _isRuleTriggered = false;
                            if (_timerRuleDelay != null)
                            {
                                _timerRuleDelay.Dispose();
                            } _execteRulesFlag = false;
                            _timerFirstRound = new System.Threading.Timer(TimerFirstRoundElapsed, null, 8, 0);
                            RecordLog.WriteLogFile("Start", "Optimal control started!");
                        }
                        else if (parameter.Value == 0)
                        {
                            _isRuleTriggered = false;
                            if (_timerRuleDelay != null)
                            {
                                _timerRuleDelay.Dispose();
                            }
                            _execteRulesFlag = false;
                            //RecordLog.WriteLogFile("Stop", "Optimal control stoped!");
                        }

                        parameter.HistoryValue = parameter.Value;
                        foreach (Variable variable in _modbusRtuParameters)
                        {
                            if (variable.Code == _remoteStatusVariableCode)
                            {
                                variable.HistoryValue = variable.Value;
                                break;
                            }
                        }
                        continue;
                    }

                    //if (parameter.Code == _optimalControlHeartBeatVariable)
                    //{
                    //    parameter.Value = 1;
                    //    if (_isModbusRtuSlaveCreated)
                    //    {
                    //        parameter.SetValueToModbusSlave(ref _modbusRtuSlave);
                    //    }
                    //    continue;
                    //}
                }
            }
            catch (Exception ex)
            {
                RecordLog.WriteLogFile("RealTimer", ex.Message); //未能正常写入文件，反馈信息到消息栏
            }
        }

        private void TimerRuleDelayElapsed(object o)
        {
            _isRuleTriggered = false;
            if (_timerRuleDelay != null)
            {
                _timerRuleDelay.Dispose();
            }
        }

        private void TimerFirstRoundElapsed(object o)
        {
            _execteRulesFlag = true;
            if (_timerFirstRound != null)
            {
                _timerFirstRound.Dispose();
            }
        }

        protected override void OnStart(string[] args)
        {
            if (string.IsNullOrEmpty(_modbusRtuDevice.SerialPortObject.PortName))
            {
                RecordLog.WriteLogFile("OnStart", "未设置Modbus RTU通讯端口！");
            }
            else
            {
                LoadSettings();
                try
                {
                    _isModbusRtuSlaveCreated = ModbusRTUCreatListener(_modbusRtuDevice);
                    if (_isModbusRtuSlaveCreated)
                    {
                        foreach (Device device in _devices)
                        {
                            if (device.State)
                            {
                                device.ModbusTcpMasterCreated = ModbusTCPCreateClient(ref device.ModbusTcpDevice);
                            }
                        }

                        foreach (Variable parameter in _modbusRtuParameters)
                        {
                            if (parameter.Code == _optimalControlFeedVariable)
                            {
                                double tmpValue = GetValueByCode(_feedVariable);
                                if (Math.Abs(tmpValue - _errorValue) > 1E-06)
                                {
                                    parameter.RealValue = tmpValue;
                                    parameter.InitialValue = tmpValue;
                                }
                                else
                                {
                                    parameter.RealValue = parameter.Limit.LowerLimit;
                                }
                                if (_isModbusRtuSlaveCreated)
                                {
                                    parameter.SetValueToModbusSlave(ref _modbusRtuSlave);
                                }
                                continue;
                            }
                            if (parameter.Code == _optimalControlFeedWaterVariable)
                            {
                                double tmpValue = GetValueByCode(_feedWaterVariable);
                                if (Math.Abs(tmpValue - _errorValue) > 1E-06)
                                {
                                    parameter.RealValue = tmpValue;
                                    parameter.InitialValue = tmpValue;
                                }
                                else
                                {
                                    parameter.RealValue = parameter.Limit.LowerLimit;
                                }
                                if (_isModbusRtuSlaveCreated)
                                {
                                    parameter.SetValueToModbusSlave(ref _modbusRtuSlave);
                                }
                                continue;
                            }
                            if (parameter.Code == _optimalControlSupWaterVariable)
                            {
                                double tmpValue = GetValueByCode(_supWaterVariable);
                                if (Math.Abs(tmpValue - _errorValue) > 1E-06)
                                {
                                    parameter.RealValue = tmpValue;
                                    parameter.InitialValue = tmpValue;
                                }
                                else
                                {
                                    parameter.RealValue = parameter.Limit.LowerLimit;
                                }
                                if (_isModbusRtuSlaveCreated)
                                {
                                    parameter.SetValueToModbusSlave(ref _modbusRtuSlave);
                                }
                                continue;
                            }
                            if (parameter.Code == _optimalControlEnabledVariable)
                            {
                                parameter.Value = 0;
                                if (_isModbusRtuSlaveCreated)
                                {
                                    parameter.SetValueToModbusSlave(ref _modbusRtuSlave);
                                }
                                continue;
                            }
                            if (parameter.Code == _remoteStatusVariableCode)
                            {
                                parameter.HistoryValue = 1;
                                continue;
                            }
                        }

                        _realTimerFlag = true;
                        _timerRealtime = new System.Threading.Timer(TimerRealtimeElapsed, null, _realTimerInterval, _realTimerInterval);
                        _timerUpdateVariable = new System.Threading.Timer(TimerUpdateVariableElapsed, null, _updateVariableTimerInterval, _updateVariableTimerInterval);

                        RecordLog.WriteLogFile("Start", "Software started!");
                    }

                    _rules = _ruleManager.GetRuleInfoEnabled();
                    //_execteRulesFlag = true;
                    _isRuleTriggered = false;
                }
                catch (Exception ex)
                {
                    RecordLog.WriteLogFile("OnStart", ex.Message);
                }
            }
        }

        protected override void OnStop()
        {
            _rules.Clear();
            _execteRulesFlag = false;
            _isRuleTriggered = false;

            _realTimerFlag = false;
            if (_timerUpdateVariable != null)
            {
                _timerUpdateVariable.Dispose();
            }
            if (_timerRealtime != null)
            {
                _timerRealtime.Dispose();
            }
            foreach (Device device in _devices)
            {
                device.ModbusTcpMasterCreated =
                    ModbusTCPStopComm(device.ModbusTcpDevice);
            }
            ModbusRTUStopComm();
            RecordLog.WriteLogFile("OnStop", "Software Closed!");
            Dispose();

        }
        #endregion
    }
}