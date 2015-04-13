namespace Model.Control
{
    public class Rule : ModelBase
    {
        #region Private Members

        private string _expression;
        private string _operation;
        private int _period;
        private bool _enabled;
        private int _delayTime;
        #endregion

        #region Public Properties

        public string Expression
        {
            get { return _expression; }
            set { _expression = value; }
        }

        public string Operation
        {
            get { return _operation; }
            set { _operation = value; }
        }

        public int Period
        {
            get { return _period; }
            set { _period = value; }
        }

        public bool Enabled
        {
            get { return _enabled; }
            set { _enabled = value; }
        }

        public int DelayTime
        {
            get { return _delayTime; }
            set { _delayTime = value; }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 无参构造
        /// </summary>
        public Rule()
        {
        }

        /// <summary>
        /// 带参构造
        /// </summary>
        /// <param name="id">实体模型 ID</param>
        /// <param name="name">The name.</param>
        /// <param name="expression">The expression.</param>
        /// <param name="operatioin">The operatioin.</param>
        /// <param name="period">The period.</param>
        /// <param name="enabled">if set to <c>true</c> [enabled].</param>
        /// <param name="delayTime">The delay time.</param>
        public Rule(int id, string name, string expression, string operatioin, int period, bool enabled, int delayTime)
        {
            Id = id;
            Name = name;
            Expression = expression;
            Operation = operatioin;
            Period = period;
            Enabled = enabled;
            DelayTime = delayTime;
        }

        #endregion




    }
}
