﻿using System;
using System.Collections.Generic;
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
        # region Struct and Enum
        /// <summary>
        /// 变量状态
        /// </summary>
        public enum VariableState
        {
            LL = -2,
            L = -1,
            N = 0,
            H = 1,
            HH = 2,
        }

        /// <summary>
        /// 变量趋势
        /// </summary>
        public enum VariableTrend
        {
            Uptrend = 1,
            Stable = 0,
            Downtrend = -1,
        }

        /// <summary>
        /// 变量上下限
        /// </summary>
        public struct VariableLimit
        {
            /// <summary>
            /// 变量上限
            /// </summary>
            public double HigherLimit;

            /// <summary>
            /// 变量下限
            /// </summary>
            public double LowerLimit;

            /// <summary>
            /// 变量上上限
            /// </summary>
            public double UltimateHigherLimit;

            /// <summary>
            /// 变量下下限
            /// </summary>
            public double UltimateLowerLimit;
        }

        #endregion

        #region Private Members

        private string _code;
        private double _value;
        private double _ratio;
        private VariableLimit _limit = new VariableLimit();
        private int _controlPeriod;
        private int _operateDelay;
        private uint _deviceId;
        private int _address;
        private bool _isDisplayed;
        private bool _isSaved;

        private bool _isFiltered;
        private double _currentValue;
        private double _historyValue;
        private double _initialValue;
        private List<double> _historyValues = new List<double>();
        private int _historyListLength;

        private VariableState _state = new VariableState();
        private VariableTrend _trend = new VariableTrend();
        private double _trendValue;
        private int _trendInterval;
        private int _trendLength;
        private List<double> _trendHigherList = new List<double>();
        private List<double> _trendLowerList = new List<double>();
        private int _trendListLength;
        private double _trendHigherLimit;
        private double _trendLowerLimit;

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
                if (Math.Abs(_ratio) > 1E-06)
                {
                    if (((value > _limit.UltimateLowerLimit/_ratio) || (_limit.UltimateLowerLimit <= 0))
                        && ((value < _limit.UltimateHigherLimit/_ratio) || (_limit.UltimateHigherLimit <= 0)))
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
                    && ((value < _limit.UltimateHigherLimit) || (_limit.UltimateHigherLimit <= 0)))
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
        /// 当前值
        /// </summary>
        public double CurrentValue
        {
            get { return _currentValue; }
        }

        /// <summary>
        /// 历史值
        /// </summary>
        public double HistoryValue
        {
            get { return _historyValue; }
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

        /// <summary>
        /// 是否保存.
        /// </summary>
        public bool IsSaved
        {
            get { return _isSaved; }
            set { _isSaved = value; }
        }

        /// <summary>
        /// 变量状态(HH=-2、H=-1、N=0、L=1、LL=2).
        /// </summary>
        public VariableState State
        {
            get { return _state; }
        }

        /// <summary>
        /// 变量趋势(Uptrend=1、Stable=0、Downtrend=-1).
        /// </summary>
        public VariableTrend Trend
        {
            get { return _trend; }
        }

        /// <summary>
        /// 变量趋势值.
        /// </summary>
        public double TrendValue
        {
            get { return _trendValue; }
        }

        /// <summary>
        /// 历史数据数度长组
        /// </summary>
        public int HistoryListLength
        {
            get { return _historyListLength; }
            set { _historyListLength = value; }
        }

        /// <summary>
        /// 趋势计算间隔.
        /// </summary>
        public int TrendInterval
        {
            get { return _trendInterval; }
            set { _trendInterval = value; }
        }

        /// <summary>
        /// 计算趋势时选择的数据点个数.
        /// </summary>
        public int TrendLength
        {
            get { return _trendLength; }
            set { _trendLength = value; }
        }

        /// <summary>
        /// 是否滤波.
        /// </summary>
        public bool IsFiltered
        {
            get { return _isFiltered; }
            set { _isFiltered = value; }
        }

        /// <summary>
        /// 计算趋势时超过判断限的点的个数.
        /// </summary>
        public int TrendListLength
        {
            get { return _trendListLength; }
            set { _trendListLength = value; }
        }

        /// <summary>
        /// 趋势判断下限.
        /// </summary>
        public double TrendLowerLimit
        {
            get { return _trendLowerLimit; }
            set { _trendLowerLimit = value; }
        }

        /// <summary>
        /// 趋势判断上限.
        /// </summary>
        public double TrendHigherLimit
        {
            get { return _trendHigherLimit; }
            set { _trendHigherLimit = value; }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 检测变量状态
        /// </summary>
        /// <returns>变量状态</returns>
        private void CheckVariableState()
        {
            double tmpValue = _value*_ratio;
            if (_isFiltered)
            {
                tmpValue = CurrentValue;
            }

            if (_limit.UltimateLowerLimit >= 0 && tmpValue < _limit.UltimateLowerLimit)
            {
                _state = VariableState.LL;
            }
            else if (_limit.UltimateHigherLimit > 0 && tmpValue > _limit.UltimateHigherLimit)
            {
                _state = VariableState.HH;
            }
            else if (_limit.LowerLimit > 0 && tmpValue < _limit.LowerLimit)
            {
                _state = VariableState.L;
            } 
            else if (_limit.HigherLimit > 0 && tmpValue > _limit.HigherLimit)
            {
                _state = VariableState.H;
            }
            else
            {
                _state = VariableState.N;
            }
        }

        /// <summary>
        /// 检查变量趋势.
        /// </summary>
        private void CheckVariableTrand()
        {
            if (_trendHigherList.Count > TrendListLength)
            {
                _trend = VariableTrend.Uptrend;
                _trendValue = LeastSquareMethod(_historyValues);
            }
            else if (_trendLowerList.Count > TrendListLength)
            {
                _trend = VariableTrend.Downtrend;
                _trendValue = LeastSquareMethod(_historyValues);
            }
            else
            {
                _trend = VariableTrend.Stable;
                _trendValue = 0;
            }
        }

        /// <summary>
        /// 最小二乗法
        /// </summary>
        /// <param name="dataList">数据.</param>
        /// <returns></returns>
        private double LeastSquareMethod(List<double> dataList)
        {
            int listlength = dataList.Count;
            double sumX = 0;
            double sumY = 0;
            double sumXY = 0;
            double sumXX = 0;

            if (listlength < 2)
            {
                return 0;
            }

            for (int index = 0; index <= listlength - 1; index++)
            {
                //X和
                sumX += index;
                //Y和
                sumY += dataList[index];
                //XY和
                sumXY += index * dataList[index];
                //XX和
                sumXX += index * index;
            }

            //nΣxx-(Σx)2
            double divisor = listlength * sumXX - sumX * sumX;
            if (Math.Abs(divisor) > 1E-06)
            {
                //(nΣxy - ΣxΣy)/[nΣx2-(Σx)2]
                return ((listlength*sumXY - sumX*sumY)/divisor);
            }
            return 0;
        }

        /// <summary>
        /// 计算变量状态和变化趋势
        /// </summary>
        public void ProcessValueData()
        {
            _historyValues.Add(RealValue);

            if (_historyValues.Count > HistoryListLength)
            {
                _historyValues.RemoveAt(0);
            }

            if (_historyValues.Count >= TrendLength*2 + TrendInterval)
            {
                double sum1 = 0;
                double sum2 = 0;
                for (int index = 0; index < TrendLength; index++)
                {
                    sum1 += _historyValues[index];
                    sum2 += _historyValues[TrendLength + TrendInterval + index];
                }

                double trendValue = (sum2 - sum1)/_trendLength;
                if (trendValue > TrendHigherLimit)
                {
                    _trendHigherList.Add(trendValue);
                    _trendLowerList.Clear();
                }
                else if (trendValue < _trendLowerLimit)
                {
                    _trendLowerList.Add(trendValue);
                    _trendHigherList.Clear();
                }
                else
                {
                    _trendHigherList.Clear();
                    _trendLowerList.Clear();
                }

                _currentValue = sum2 / _trendLength;

                if (_isFiltered)
                {
                    _historyValue = sum1/_trendLength;
                }
                else
                {
                    if (_historyValues.Count > 1)
                    {
                        _historyValue = _historyValues[_historyValues.Count - 2];
                    }
                }
            }
            else
            {
                if (_historyValues.Count > 1)
                {
                    _historyValue = _historyValues[_historyValues.Count - 2];
                }
                _currentValue = _value*_ratio;
            }

            CheckVariableState();
            CheckVariableTrand();
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

        #region Constructor
        /// <summary>
        /// 无参构造
        /// </summary>
        public Variable()
        {
            _historyListLength = 24;
            _trendInterval = 12;
            _trendLength = 6;
            _trendListLength = 6;
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
            double initialValue,
            string code,
            bool isDisplayed,
            bool isSaved,
            int historyListLength,
            int trendInterval,
            int trendLength,
            bool isFiltered,
            double trendHigherLimit,
            double trendLowerLimit)
            : base(variableId, variableName)
        {
            _historyListLength = 24;
            _trendInterval = 12;
            _trendLength = 6;
            _trendListLength = 6;
            Value = variableValue;
            Ratio = variableRatio;
            Limit = variableLimit;
            ControlPeriod = variableControlPeriod;
            OperateDelay = variableOperateDelay;
            DeviceID = variableDeviceID;
            Address = variableAddress;
            InitialValue = initialValue;
            Code = code;
            IsDisplayed = isDisplayed;
            IsSaved = isSaved;
            HistoryListLength = historyListLength;
            TrendInterval = trendInterval;
            TrendLength = trendLength;
            IsFiltered = isFiltered;
            TrendHigherLimit = trendHigherLimit;
            TrendLowerLimit = trendLowerLimit;
        }

        #endregion

    }
}
