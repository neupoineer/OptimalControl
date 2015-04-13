using System;

namespace Model
{
    /// <summary>
    /// 权限关系实体类
    /// </summary>
    [Serializable]
    public class RightsRelation
    {
        #region Private Members
        int _id;
        int _operatorId;
        string _operatorName;
        int _rightsGroupId;
        string rightsGroupName;
        #endregion

        #region Public Properties
        /// <summary>
        /// 权限关系 ID
        /// </summary>
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        /// <summary>
        /// 操作员 ID
        /// </summary>
        public int OperatorId
        {
            get { return _operatorId; }
            set { _operatorId = value; }
        }
        /// <summary>
        /// 操作员名称
        /// </summary>
        public string OperatorName
        {
            get { return _operatorName; }
            set { _operatorName = value; }
        }
        /// <summary>
        /// 权限组 ID
        /// </summary>
        public int RightsGroupId
        {
            get { return _rightsGroupId; }
            set { _rightsGroupId = value; }
        }
        /// <summary>
        /// 权限组名称
        /// </summary>
        public string RightsGroupName
        {
            get { return rightsGroupName; }
            set { rightsGroupName = value; }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// 无参构造
        /// </summary>
        public RightsRelation() { }
        /// <summary>
        /// 带参构造
        /// </summary>
        /// <param name="id">权限关系 ID</param>
        /// <param name="operatorId">操作员 ID</param>
        /// <param name="operatorName">操作员名称</param>
        /// <param name="rightsGroupId">权限组 ID</param>
        /// <param name="rightsGroupName">权限组名称</param>
        public RightsRelation(
            int id, 
            int operatorId, 
            string operatorName,
            int rightsGroupId,
            string rightsGroupName)
        {
            this.Id = id;
            this.OperatorId = operatorId;
            this.OperatorName = operatorName;
            this.RightsGroupId = rightsGroupId;
            this.RightsGroupName = rightsGroupName;
        } 
        #endregion
    }
}
