namespace Model.Control
{
    /// <summary>
    /// 规则模型
    /// </summary>
    public class Rule : ModelBase
    {
        #region Private Members

        private string _expression;
        private string _operation;
        private int _period;
        private bool _state;
        private int _delayTime;
        private int _priority;
        private bool _type;
        private bool _isLogged;

        #endregion

        #region Public Properties

        /// <summary>
        /// 条件表达式
        /// </summary>
        public string Expression
        {
            get { return _expression; }
            set { _expression = value; }
        }

        /// <summary>
        /// 操作表达式
        /// </summary>
        public string Operation
        {
            get { return _operation; }
            set { _operation = value; }
        }

        /// <summary>
        /// 控制周期
        /// </summary>
        public int Period
        {
            get { return _period; }
            set { _period = value; }
        }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool State
        {
            get { return _state; }
            set { _state = value; }
        }

        /// <summary>
        /// 延时时间
        /// </summary>
        public int DelayTime
        {
            get { return _delayTime; }
            set { _delayTime = value; }
        }

        /// <summary>
        /// 优先级
        /// </summary>
        public int Priority
        {
            get { return _priority; }
            set { _priority = value; }
        }

        /// <summary>
        /// 规则类型
        /// </summary>
        public bool Type
        {
            get { return _type; }
            set { _type = value; }
        }

        /// <summary>
        /// 是否记录日志
        /// </summary>
        public bool IsLogged
        {
            get { return _isLogged; }
            set { _isLogged = value; }
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
        public Rule(
            int id, 
            string name, 
            string expression, 
            string operatioin, 
            int period, 
            bool state, 
            int delayTime,
            int priority, 
            bool type, 
            bool isLogged)
        {
            Id = id;
            Name = name;
            Expression = expression;
            Operation = operatioin;
            Period = period;
            State = state;
            DelayTime = delayTime;
            Priority = priority;
            Type = type;
            IsLogged = isLogged;
        }

        #endregion

    }
}
