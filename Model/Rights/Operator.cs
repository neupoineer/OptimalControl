using System;
using System.Collections.Generic;

namespace Model.Rights
{
    /// <summary>
    /// ����Աʵ����
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
        /// ����Ա����
        /// </summary>
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }
        /// <summary>
        /// Ȩ�޼���(��ֵ���ڴ洢�˵�/��������� Name ����)
        /// </summary>
        public Dictionary<string, Rights> RightsCollection
        {
            get { return _rightsCollection; }
            set { _rightsCollection = value; }
        }
        /// <summary>
        /// ����Ա״̬
        /// </summary>
        public bool State
        {
            get { return _state; }
            set { _state = value; }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// �޲ι���(�������Ը�ֵ˵����Id - ����Ա ID / ModelName - ����Ա����)
        /// </summary>
        public Operator() { }

        /// <summary>
        /// ���ι���
        /// </summary>
        /// <param name="operatorId">����Ա ID</param>
        /// <param name="name">����Ա����</param>
        /// <param name="password">����Ա����</param>
        /// <param name="rightsCollection">Ȩ�޼���(��ֵ���ڴ洢�˵�/��������� Name ����)</param>
        /// <param name="state">����Ա״̬</param>
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
