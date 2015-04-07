using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    /// <summary>
    /// 权限实体类
    /// </summary>
    [Serializable]
    public class Rights : ModelBase
    {
        #region Private Members
        string _rightsCaption;
        bool _rightsState;
        string _parentLevelRightsName = null;
        #endregion

        #region Public Properties
        /// <summary>
        /// 权限标题
        /// </summary>
        public string RightsCaption
        {
            get { return _rightsCaption; }
            set { _rightsCaption = value; }
        }
        /// <summary>
        /// 权限状态(True:显示 / False:隐藏)
        /// </summary>
        public bool RightsState
        {
            get { return _rightsState; }
            set { _rightsState = value; }
        }
        /// <summary>
        /// 父级权限
        /// </summary>
        public string ParentLevelRightsName
        {
            get { return _parentLevelRightsName; }
            set { _parentLevelRightsName = value; }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// 无参构造(基类属性赋值说明：Id - 权限 ID / ModelName - 权限名称)
        /// </summary>
        public Rights() { }

        /// <summary>
        /// 带参构造
        /// </summary>
        /// <param name="rightsId">权限 ID</param>
        /// <param name="rightsName">权限名称</param>
        /// <param name="rightsCaption">权限标题</param>
        /// <param name="rightsState">权限状态</param>
        /// <param name="parentLevelRightsName">父级权限名称</param>
        public Rights(
            int rightsId,
            string rightsName,
            string rightsCaption,
            bool rightsState,
            string parentLevelRightsName)
            : base(rightsId, rightsName)
        {
            this.RightsCaption = rightsCaption;
            this.RightsState = rightsState;
            this.ParentLevelRightsName = parentLevelRightsName;
        } 
        #endregion
    }
}
