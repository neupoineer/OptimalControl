using System;
using Modbus.Device;
using Model.Modbus;
using Utility;

namespace Model.Control
{
    /// <summary>
    /// 变量实体类
    /// </summary>
    [Serializable]
    public class Variable : ModelBase
    {
        /// <summary>
        /// 变量状态
        /// </summary>
        public enum VariableState
        {
            正常 = 0,
            超上限 = 1,
            超下限 = 2,
            超上上限 = 3,
            超下下限 = 4,
        }

        /// <summary>
        /// 变量上下限
        /// </summary>
        public struct VariableLimit
        {
            /// <summary>
            /// 变量上限
            /// </summary>
            public double UpperLimit;

            /// <summary>
            /// 变量下限
            /// </summary>
            public double LowerLimit;

            /// <summary>
            /// 变量上上限
            /// </summary>
            public double UltimateUpperLimit;

            /// <summary>
            /// 变量下下限
            /// </summary>
            public double UltimateLowerLimit;
        }

        #region Private Members

        private string _code;
        private double _value;
        private double _historyValue;
        private double _initialValue;
        private double _ratio;
        private VariableLimit _limit;
        private int _controlPeriod;
        private int _operateDelay;
        private uint _deviceId;
        private int _address;
        private bool _isDisplayed;
        #endregion

        #region Public Properties
        /// <summary>
        /// 变量值
        /// </summary>
        public double Value
        {
            get { return _value; }
            set
            {
                if (_ratio != 0)
                {
                    if (((value > _limit.UltimateLowerLimit/_ratio) || (_limit.UltimateLowerLimit <= 0))
                        && ((value < _limit.UltimateUpperLimit/_ratio) || (_limit.UltimateUpperLimit <= 0)))
                    {
                        _value = value;
                    }
                }
            }
        }

        /// <summary>
        /// 变量真值=变量值*放大倍数
        /// </summary>
        public double RealValue
        {
            get { return _value*_ratio; }
            set
            {
                if (((value > _limit.UltimateLowerLimit) || (_limit.UltimateLowerLimit <= 0))
                    && ((value < _limit.UltimateUpperLimit) || (_limit.UltimateUpperLimit <= 0)))
                {
                    if (_ratio != 0)
                    {
                        _value = value/_ratio;
                    }
                    else
                    {
                        _value = value;
                    }
                }
            }
        }

        /// <summary>
        /// 放大倍数
        /// </summary>
        public double Ratio
        {
            get { return _ratio; }
            set { _ratio = value; }
        }

        /// <summary>
        /// 上下限
        /// </summary>
        public VariableLimit Limit
        {
            get { return _limit; }
            set { _limit = value; }
        }

        /// <summary>
        /// 控制周期.
        /// </summary>
        public int ControlPeriod
        {
            get { return _controlPeriod; }
            set { _controlPeriod = value; }
        }

        /// <summary>
        /// 动作延时
        /// </summary>
        public int OperateDelay
        {
            get { return _operateDelay; }
            set { _operateDelay = value; }
        }

        /// <summary>
        /// 设备ID
        /// </summary>
        public uint DeviceID
        {
            get { return _deviceId; }
            set { _deviceId = value; }
        }

        /// <summary>
        /// 变量地址
        /// </summary>
        public int Address
        {
            get { return _address; }
            set { _address = value; }
        }

        /// <summary>
        /// 历史值
        /// </summary>
        public double HistoryValue
        {
            get { return _historyValue; }
            set { _historyValue = value; }
        }

        /// <summary>
        /// 初始值.
        /// </summary>
        public double InitialValue
        {
            get { return _initialValue; }
            set { _initialValue = value; }
        }

        /// <summary>
        /// 变量编码
        /// </summary>
        public string Code
        {
            get { return _code; }
            set { _code = value; }
        }

        /// <summary>
        /// 是否显示
        /// </summary>
        public bool IsDisplayed
        {
            get { return _isDisplayed; }
            set { _isDisplayed = value; }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 无参构造
        /// </summary>
        public Variable()
        {
        }

        /// <summary>
        /// 带参构造
        /// </summary>
        public Variable(
            int variableId,
            string variableName,
            double variableValue,
            double variableRatio,
            VariableLimit variableLimit,
            int variableControlPeriod,
            int variableOperateDelay,
            uint variableDeviceID,
            int variableAddress,
            double historyValue,
            double initialValue, 
            string code, 
            bool isDisplayed)
            : base(variableId, variableName)
        {
            Value = variableValue;
            Ratio = variableRatio;
            Limit = variableLimit;
            ControlPeriod = variableControlPeriod;
            OperateDelay = variableOperateDelay;
            DeviceID = variableDeviceID;
            Address = variableAddress;
            HistoryValue = historyValue;
            InitialValue = initialValue;
            Code = code;
            IsDisplayed = isDisplayed;
        }

        /// <summary>
        /// 检测变量状态
        /// </summary>
        /// <returns>变量状态</returns>
        public VariableState CheckVariableState()
        {
            double tmpValue = _value * _ratio;
            if (_limit.UltimateUpperLimit > 0 && tmpValue > _limit.UltimateUpperLimit)
            {
                return VariableState.超上上限;
            }
            else if (_limit.UpperLimit > 0 && tmpValue > _limit.UpperLimit &&
                     (tmpValue < _limit.UltimateUpperLimit || _limit.UltimateUpperLimit <= 0))
            {
                return VariableState.超上限;
            }
            else if (_limit.UltimateLowerLimit >= 0 && tmpValue < _limit.UltimateLowerLimit)
            {
                return VariableState.超下下限;
            }
            else if (_limit.LowerLimit >= 0 && tmpValue < _limit.LowerLimit &&
                     (tmpValue > _limit.UltimateLowerLimit || _limit.UltimateLowerLimit <= 0))
            {
                return VariableState.超下限;
            }
            else
            {
                return VariableState.正常;
            }
        }

        /// <summary>
        /// 更新历史值
        /// </summary>
        public void UpdateHistoryValue()
        {
            _historyValue = _value;
        }
        
        /// <summary>
        /// Gets the value from modbus TCP master.
        /// </summary>
        /// <param name="modbusTcpDevice">The modbus TCP device.</param>
        /// <returns>Result</returns>
        public bool GetValueFromModbusTcpMaster(ref ModbusTcpDevice modbusTcpDevice)
        {
            try
            {
                //读寄存器
                ushort[] register = modbusTcpDevice.ModbusTcpMaster.ReadHoldingRegisters(
                    modbusTcpDevice.UnitID,
                    (ushort) (_address - 1), 2);
                byte[] byteString = new byte[4];
                for (int j = 0; j < 2; j++)
                {
                    byte[] tempByte = BitConverter.GetBytes(register[j]);
                    byteString[2 * j] = tempByte[0];
                    byteString[2 * j + 1] = tempByte[1];
                }
                _value = BitConverter.ToSingle(byteString, 0); //还原用2个8位寄存器保存的1个浮点数
                return true;
            }
            catch (Exception ex)
            {
                RecordLog.WriteLogFile("GetValueFromModbusTcpMaster", ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Sets the value from modbus TCP master.
        /// </summary>
        /// <param name="modbusTcpDevice">The modbus TCP device.</param>
        /// <returns></returns>
        public bool SetValueToModbusTcpMaster(ref ModbusTcpDevice modbusTcpDevice)
        {
            try
            {
                byte[] tempByte = BitConverter.GetBytes(Convert.ToSingle(_value));
                modbusTcpDevice.ModbusTcpMaster.WriteMultipleRegisters(
                    modbusTcpDevice.UnitID,
                    (ushort) (_address - 1),
                    new ushort[]
                    {
                        Convert.ToUInt16(tempByte[1]*256 + tempByte[0]),
                        Convert.ToUInt16(tempByte[3]*256 + tempByte[2])
                    }
                    );
                return true;
            }
            catch (Exception ex)
            {
                RecordLog.WriteLogFile("SetValueToModbusTcpMaster", ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Gets the value from modbus salve.
        /// </summary>
        /// <param name="modbusSlave">The modbus slave.</param>
        /// <returns></returns>
        public bool GetValueFromModbusSalve(ref ModbusSlave modbusSlave)
        {
            ushort[] register = new ushort[2];
            try
            {
                //读寄存器
                register[0] = modbusSlave.DataStore.HoldingRegisters[_address];
                register[1] = modbusSlave.DataStore.HoldingRegisters[_address + 1];

                byte[] byteString = new byte[4];
                for (int j = 0; j < 2; j++)
                {
                    byte[] tempByte = BitConverter.GetBytes(register[j]);
                    byteString[2*j] = tempByte[0];
                    byteString[2*j + 1] = tempByte[1];
                }
                _value = BitConverter.ToSingle(byteString, 0); //还原用2个8位寄存器保存的1个浮点数
                return true;
            }
            catch (Exception ex)
            {
                RecordLog.WriteLogFile("GetValueFromModbusSalve", ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Sets the value to modbus salve.
        /// </summary>
        /// <param name="modbusSlave">The modbus slave.</param>
        /// <returns></returns>
        public bool SetValueToModbusSalve(ref ModbusSlave modbusSlave)
        {
            try
            {
                byte[] tempByte = BitConverter.GetBytes(Convert.ToSingle(_value));
                modbusSlave.DataStore.HoldingRegisters[_address] = Convert.ToUInt16(tempByte[1]*256 + tempByte[0]);
                modbusSlave.DataStore.HoldingRegisters[_address + 1] = Convert.ToUInt16(tempByte[3]*256 + tempByte[2]);
                return true;
            }
            catch (Exception ex)
            {
                RecordLog.WriteLogFile("SetValueToModbusSalve", ex.Message);
                return false;
            }
        }
        #endregion

    }
}
