using System;

namespace Model.Control
{
    /// <summary>
    /// 数据模型
    /// </summary>
    public class Data
    {
        #region Private Members

        private string _variableCode;
        private DateTime _timeValue;
        private double _value;
        private int _deviceID;
        private Variable.VariableState _state;
        private Variable.VariableTrend _trend;
        private double _trendValue;
        #endregion

        #region Public Properties

        /// <summary>
        /// 时间
        /// </summary>
        public DateTime TimeValue
        {
            get { return _timeValue; }
            set { _timeValue = value; }
        }

        /// <summary>
        /// 数值
        /// </summary>
        public double Value
        {
            get { return _value; }
            set { _value = value; }
        }

        /// <summary>
        /// 设备ID
        /// </summary>
        public int DeviceID
        {
            get { return _deviceID; }
            set { _deviceID = value; }
        }

        /// <summary>
        /// 变量编码
        /// </summary>
        public string VariableCode
        {
            get { return _variableCode; }
            set { _variableCode = value; }
        }

        /// <summary>
        /// 变量状态
        /// </summary>
        public Variable.VariableState State
        {
            get { return _state; }
            set { _state = value; }
        }

        /// <summary>
        /// 变量趋势
        /// </summary>
        public Variable.VariableTrend Trend
        {
            get { return _trend; }
            set { _trend = value; }
        }

        /// <summary>
        /// 变量趋势值
        /// </summary>
        public double TrendValue
        {
            get { return _trendValue; }
            set { _trendValue = value; }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 无参构造
        /// </summary>
        public Data()
        {
        }

        /// <summary>
        /// 带参构造
        /// </summary>
        public Data(
            string variableCode,
            DateTime timeValue,
            double value,
            int deviceId, 
            Variable.VariableState state, 
            Variable.VariableTrend trend, 
            double trendValue)
        {
            VariableCode = variableCode;
            TimeValue = timeValue;
            Value = value;
            DeviceID = deviceId;
            State = state;
            Trend = trend;
            TrendValue = trendValue;
        }

        #endregion
    }
}
