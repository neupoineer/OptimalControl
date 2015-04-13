using System;
using System.Collections.Generic;

namespace Model
{
    /// <summary>
    /// 权限组实体类
    /// </summary>
    [Serializable]
    public class RightsGroup : ModelBase
    {
        #region Private Members
        Dictionary<string, Rights> _groupRightsCollection; 
        #endregion

        #region Public Properties
        /// <summary>
        /// 组权限集合
        /// </summary>
        public Dictionary<string, Rights> GroupRightsCollection
        {
            get { return _groupRightsCollection; }
            set { _groupRightsCollection = value; }
        } 
        #endregion

        #region Public Methods
        /// <summary>
        /// 无参构造(基类属性赋值说明：Id - 权限组 ID / Name - 权限组名称)
        /// </summary>
        public RightsGroup() { }
        /// <summary>
        /// 带参构造
        /// </summary>
        /// <param name="groupId">权限组 ID</param>
        /// <param name="groupName">权限组名称</param>
        /// <param name="groupRightsCollection">组权限集合</param>
        public RightsGroup(
            int groupId,
            string groupName,
            Dictionary<string, Rights> groupRightsCollection)
            : base(groupId, groupName)
        {
            this.GroupRightsCollection = groupRightsCollection;
        } 
        #endregion
    }
}
