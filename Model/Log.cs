using System;

namespace Model
{
    public class Log
    {
        /// <summary>
        /// 日志等级
        /// </summary>
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

        /// <summary>
        /// ID
        /// </summary>
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        /// <summary>
        /// 时间
        /// </summary>
        public DateTime LogTime
        {
            get { return _logTime; }
            set { _logTime = value; }
        }

        /// <summary>
        /// 日志等级
        /// </summary>
        public LogType Type
        {
            get { return _type; }
            set { _type = value; }
        }

        /// <summary>
        /// 日志内容
        /// </summary>
        public string Content
        {
            get { return _content; }
            set { _content = value; }
        }

        /// <summary>
        /// 日志状态
        /// </summary>
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
        public Log(
            int id, 
            DateTime time, 
            LogType type, 
            string content, 
            bool state)
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
