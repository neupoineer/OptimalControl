using System;
using System.ComponentModel.Design;
using Modbus.Device;
using Model.Modbus;

namespace Model.Control
{
    /// <summary>
    /// 变量实体类
    /// </summary>
    [Serializable]
    public class Variable : ModelBase
    {
        public enum VariableState
        {
            正常 = 0,
            超上限 = 1,
            超下限 = 2,
            超上上限 = 3,
            超下下限 = 4,
        }

        /// <summary>
        /// 变量上下限结构体
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
        private double _value;
        private double _ratio;
        private VariableLimit _limit;
        private int _controlPeriod;
        private int _operateDelay;
        private uint _deviceId;
        private int _address;
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// 变量值.
        /// </value>
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
        /// Gets the real value.
        /// </summary>
        /// <value>
        /// 变量真值=变量值*放大倍数.
        /// </value>
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
                }
            }
        }

        /// <summary>
        /// Gets or sets the ratio.
        /// </summary>
        /// <value>
        /// 放大倍数.
        /// </value>
        public double Ratio
        {
            get { return _ratio; }
            set { _ratio = value; }
        }

        /// <summary>
        /// Gets or sets the limit.
        /// </summary>
        /// <value>
        /// 上下限.
        /// </value>
        public VariableLimit Limit
        {
            get { return _limit; }
            set { _limit = value; }
        }

        /// <summary>
        /// Gets or sets the control period.
        /// </summary>
        /// <value>
        /// 控制周期.
        /// </value>
        public int ControlPeriod
        {
            get { return _controlPeriod; }
            set { _controlPeriod = value; }
        }

        /// <summary>
        /// Gets or sets the operate delay.
        /// </summary>
        /// <value>
        /// 动作延时.
        /// </value>
        public int OperateDelay
        {
            get { return _operateDelay; }
            set { _operateDelay = value; }
        }

        /// <summary>
        /// Gets or sets the device identifier.
        /// </summary>
        /// <value>
        /// 设备ID.
        /// </value>
        public uint DeviceID
        {
            get { return _deviceId; }
            set { _deviceId = value; }
        }

        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        /// <value>
        /// 变量地址.
        /// </value>
        public int Address
        {
            get { return _address; }
            set { _address = value; }
        }

        #endregion


        #region Public Methods

        /// <summary>
        /// 无参构造(基类属性赋值说明：Id - 权限 ID / Name - 权限名称)
        /// </summary>
        public Variable()
        {

        }

        public VariableState CheckVariableState()
        {
            double tmpValue = _value*_ratio;
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
        /// 带参构造
        /// </summary>
        /// <param name="variableId">The variable identifier.</param>
        /// <param name="variableName">Name of the variable.</param>
        /// <param name="variableValue">The variable value.</param>
        /// <param name="variableRatio">The variable ratio.</param>
        /// <param name="variableLimit">The variable limit.</param>
        /// <param name="variableControlPeriod">The variable control period.</param>
        /// <param name="variableOperateDelay">The variable operate delay.</param>
        /// <param name="variableDeviceID">The variable device identifier.</param>
        /// <param name="variableAddress">The variable address.</param>
        public Variable(
            int variableId,
            string variableName,
            double variableValue,
            double variableRatio,
            VariableLimit variableLimit,
            int variableControlPeriod,
            int variableOperateDelay,
            uint variableDeviceID, int variableAddress)
            : base(variableId, variableName)
        {
            this.Value = variableValue;
            this.Ratio = variableRatio;
            this.Limit = variableLimit;
            this.ControlPeriod = variableControlPeriod;
            this.OperateDelay = variableOperateDelay;
            this.DeviceID = variableDeviceID;
            Address = variableAddress;
        }


        /// <summary>
        /// Gets the value from modbus TCP master.
        /// </summary>
        /// <param name="modbusTcpDevice">The modbus TCP device.</param>
        /// <returns>Result</returns>
        public bool GetValueFromModbusTcpMaster(ModbusTcpDevice modbusTcpDevice)
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
                return false;
            }
        }

        /// <summary>
        /// Sets the value from modbus TCP master.
        /// </summary>
        /// <param name="modbusTcpDevice">The modbus TCP device.</param>
        /// <returns></returns>
        public bool SetValueToModbusTcpMaster(ModbusTcpDevice modbusTcpDevice)
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
                return false;
            }
        }

        public bool GetValueFromModbusSalve(ModbusSlave modbusSlave)
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
                return false;
            }
        }

        public bool SetValueToModbusSalve(ModbusSlave modbusSlave)
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
                return false;
            }
        }
        #endregion



    }
}
