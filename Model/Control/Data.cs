using System;

namespace Model.Control
{
    public class Data
    {
        #region Private Members

        private string _parameterCode;
        private DateTime _timeValue;
        private double _value;
        private int _deviceID;
        #endregion

        #region Public Properties
        
        public DateTime TimeValue
        {
            get { return _timeValue; }
            set { _timeValue = value; }
        }

        public double Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public int DeviceID
        {
            get { return _deviceID; }
            set { _deviceID = value; }
        }

        public string ParameterCode
        {
            get { return _parameterCode; }
            set { _parameterCode = value; }
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
        /// <param name="parameterCode">Name of the parameter.</param>
        /// <param name="timeValue">The time value.</param>
        /// <param name="value">The value.</param>
        /// <param name="deviceId">The device identifier.</param>

        public Data(string parameterCode, DateTime timeValue, double value, int deviceId )
        {
            ParameterCode = parameterCode;
            TimeValue = timeValue;
            Value = value;
            DeviceID = deviceId;
        }

        #endregion

    }

}
