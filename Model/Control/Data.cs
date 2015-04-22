using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Control
{
    public class Data
    {
        #region Private Members

        private string _ParameterName;
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

        public string ParameterName
        {
            get { return _ParameterName; }
            set { _ParameterName = value; }
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
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="timeValue">The time value.</param>
        /// <param name="value">The value.</param>
        /// <param name="deviceId">The device identifier.</param>

        public Data(string parameterName, DateTime timeValue, double value, int deviceId )
        {
            ParameterName = parameterName;
            TimeValue = timeValue;
            Value = value;
            DeviceID = deviceId;
        }

        #endregion

    }

}
