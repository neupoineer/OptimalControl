using System;
using System.Collections.Generic;

namespace Model.Rights
{
    /// <summary>
    /// 操作员实体类
    /// </summary>
    [Serializable]
    public class Operator : ModelBase
    {
        #region Private Members
        string _password;
        Dictionary<string, Rights> _rightsCollection;
        bool _state;
        #endregion

        #region Public Properties
        /// <summary>
        /// 操作员密码
        /// </summary>
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }
        /// <summary>
        /// 权限集合(键值用于存储菜单/工具栏项的 Name 属性)
        /// </summary>
        public Dictionary<string, Rights> RightsCollection
        {
            get { return _rightsCollection; }
            set { _rightsCollection = value; }
        }
        /// <summary>
        /// 操作员状态
        /// </summary>
        public bool State
        {
            get { return _state; }
            set { _state = value; }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// 无参构造(基类属性赋值说明：Id - 操作员 ID / ModelName - 操作员名称)
        /// </summary>
        public Operator() { }

        /// <summary>
        /// 带参构造
        /// </summary>
        /// <param name="operatorId">操作员 ID</param>
        /// <param name="name">操作员名称</param>
        /// <param name="password">操作员密码</param>
        /// <param name="rightsCollection">权限集合(键值用于存储菜单/工具栏项的 Name 属性)</param>
        /// <param name="state">操作员状态</param>
        public Operator(
            int operatorId,
            string name,
            string password,
            Dictionary<string, Rights> rightsCollection,
            bool state)
            : base(operatorId, name)
        {
            this.Password = password;
            this.RightsCollection = rightsCollection;
            this.State = state;
        } 
        #endregion
    }
}
