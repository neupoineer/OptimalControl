using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Log
    {
        public enum LogType
        {
            严重 = 0,
            报警 = 1,
            建议 = 2,
            提示 = 3,
        }

        #region Private Members

        private int _id;
        private DateTime _logTime;
        private LogType _type;
        private string _content;
        private bool _state;

        #endregion

        #region Public Properties

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public DateTime LogTime
        {
            get { return _logTime; }
            set { _logTime = value; }
        }

        public LogType Type
        {
            get { return _type; }
            set { _type = value; }
        }

        public string Content
        {
            get { return _content; }
            set { _content = value; }
        }

        public bool State
        {
            get { return _state; }
            set { _state = value; }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 无参构造
        /// </summary>
        public Log()
        {

        }

        /// <summary>
        /// 带参构造
        /// </summary>
        /// <param name="id">实体模型 ID</param>
        /// <param name="time">The time.</param>
        /// <param name="type">The type.</param>
        /// <param name="content">The content.</param>
        /// <param name="state">if set to <c>true</c> [state].</param>
        public Log(int id, DateTime time, LogType type, string content, bool state)
        {
            Id = id;
            LogTime = time;
            Type = type;
            Content = content;
            State = state;
        }

        #endregion

    }
}
