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
            int deviceId)
        {
            VariableCode = variableCode;
            TimeValue = timeValue;
            Value = value;
            DeviceID = deviceId;
        }

        #endregion
    }
}
