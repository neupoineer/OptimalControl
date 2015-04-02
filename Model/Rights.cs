using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    /// <summary>
    /// Ȩ��ʵ����
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
        /// Ȩ�ޱ���
        /// </summary>
        public string RightsCaption
        {
            get { return _rightsCaption; }
            set { _rightsCaption = value; }
        }
        /// <summary>
        /// Ȩ��״̬(True:��ʾ / False:����)
        /// </summary>
        public bool RightsState
        {
            get { return _rightsState; }
            set { _rightsState = value; }
        }
        /// <summary>
        /// ����Ȩ��
        /// </summary>
        public string ParentLevelRightsName
        {
            get { return _parentLevelRightsName; }
            set { _parentLevelRightsName = value; }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// �޲ι���(�������Ը�ֵ˵����Id - Ȩ�� ID / ModelName - Ȩ������)
        /// </summary>
        public Rights() { }

        /// <summary>
        /// ���ι���
        /// </summary>
        /// <param name="rightsId">Ȩ�� ID</param>
        /// <param name="rightsName">Ȩ������</param>
        /// <param name="rightsCaption">Ȩ�ޱ���</param>
        /// <param name="rightsState">Ȩ��״̬</param>
        /// <param name="parentLevelRightsName">����Ȩ������</param>
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
