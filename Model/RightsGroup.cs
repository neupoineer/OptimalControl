using System;
using System.Collections.Generic;

namespace Model
{
    /// <summary>
    /// Ȩ����ʵ����
    /// </summary>
    [Serializable]
    public class RightsGroup : ModelBase
    {
        #region Private Members
        Dictionary<string, Rights> _groupRightsCollection; 
        #endregion

        #region Public Properties
        /// <summary>
        /// ��Ȩ�޼���
        /// </summary>
        public Dictionary<string, Rights> GroupRightsCollection
        {
            get { return _groupRightsCollection; }
            set { _groupRightsCollection = value; }
        } 
        #endregion

        #region Public Methods
        /// <summary>
        /// �޲ι���(�������Ը�ֵ˵����Id - Ȩ���� ID / Name - Ȩ��������)
        /// </summary>
        public RightsGroup() { }
        /// <summary>
        /// ���ι���
        /// </summary>
        /// <param name="groupId">Ȩ���� ID</param>
        /// <param name="groupName">Ȩ��������</param>
        /// <param name="groupRightsCollection">��Ȩ�޼���</param>
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
