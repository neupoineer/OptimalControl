using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExpertSystem
{
    public class Rule
    {
        #region Private Members

        private int _id;
        private string _name;
        private string _expression;
        private string _operatioin;
        private int _period;
        private bool _enabled;

        #endregion

        #region Public Properties

        /// <summary>
        /// 实体模型 ID
        /// </summary>
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        /// <summary>
        /// 实体模型名称
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Expression
        {
            get { return _expression; }
            set { _expression = value; }
        }

        public string Operatioin
        {
            get { return _operatioin; }
            set { _operatioin = value; }
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
        public Rule(int id, string name, string expression, string operatioin, int period, bool enabled)
        {
            Id = id;
            Name = name;
            Expression = expression;
            Operatioin = operatioin;
            Period = period;
            Enabled = enabled;
        }

        #endregion




    }
}
