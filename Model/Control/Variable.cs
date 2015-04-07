using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    /// <summary>
    /// 变量实体类
    /// </summary>
    [Serializable]
    public class Variable : ModelBase
    {

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
        private VariableLimit _limit;
        private uint _controlPeriod;
        private uint _operateDelay;
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
            set { _value = value; }
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
        public uint ControlPeriod
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
        public uint OperateDelay
        {
            get { return _operateDelay; }
            set { _operateDelay = value; }
        }
        #endregion


        #region Public Methods

        /// <summary>
        /// 无参构造(基类属性赋值说明：Id - 权限 ID / ModelName - 权限名称)
        /// </summary>
        public Variable()
        {
        }

        /// <summary>
        /// 带参构造
        /// </summary>
        /// <param name="variableId">The variable identifier.</param>
        /// <param name="variableName">Name of the variable.</param>
        /// <param name="variableValue">The variable value.</param>
        /// <param name="variableLimit">The variable limit.</param>
        /// <param name="variableControlPeriod">The variable control period.</param>
        /// <param name="variableOperateDelay">The variable operate delay.</param>
        public Variable(
            int variableId,
            string variableName,
            double variableValue,
            VariableLimit variableLimit,
            uint variableControlPeriod,
            uint variableOperateDelay)
            : base(variableId, variableName)
        {
            this.Value = variableValue;
            this.Limit = variableLimit;
            this.ControlPeriod = variableControlPeriod;
            this.OperateDelay = variableOperateDelay;
        }

        #endregion



    }
}
