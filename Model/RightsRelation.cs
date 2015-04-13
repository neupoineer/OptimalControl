using System;

namespace Model
{
    /// <summary>
    /// Ȩ�޹�ϵʵ����
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
        /// Ȩ�޹�ϵ ID
        /// </summary>
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        /// <summary>
        /// ����Ա ID
        /// </summary>
        public int OperatorId
        {
            get { return _operatorId; }
            set { _operatorId = value; }
        }
        /// <summary>
        /// ����Ա����
        /// </summary>
        public string OperatorName
        {
            get { return _operatorName; }
            set { _operatorName = value; }
        }
        /// <summary>
        /// Ȩ���� ID
        /// </summary>
        public int RightsGroupId
        {
            get { return _rightsGroupId; }
            set { _rightsGroupId = value; }
        }
        /// <summary>
        /// Ȩ��������
        /// </summary>
        public string RightsGroupName
        {
            get { return rightsGroupName; }
            set { rightsGroupName = value; }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// �޲ι���
        /// </summary>
        public RightsRelation() { }
        /// <summary>
        /// ���ι���
        /// </summary>
        /// <param name="id">Ȩ�޹�ϵ ID</param>
        /// <param name="operatorId">����Ա ID</param>
        /// <param name="operatorName">����Ա����</param>
        /// <param name="rightsGroupId">Ȩ���� ID</param>
        /// <param name="rightsGroupName">Ȩ��������</param>
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
