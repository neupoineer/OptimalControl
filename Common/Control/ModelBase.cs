using System;

namespace Common.Control
{
    /// <summary>
    /// ʵ��ģ�ͻ���
    /// </summary>
    [Serializable]
    public class ModelBase
    {
        #region Private Members
        int _id;
        string _modelName; 
        #endregion

        #region Public Properties
        /// <summary>
        /// ʵ��ģ�� ID
        /// </summary>
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        /// <summary>
        /// ʵ��ģ������
        /// </summary>
        public string ModelName
        {
            get { return _modelName; }
            set { _modelName = value; }
        } 
        #endregion

        #region Public Methods
        /// <summary>
        /// �޲ι���
        /// </summary>
        public ModelBase() { }

        /// <summary>
        /// ���ι���
        /// </summary>
        /// <param name="id">ʵ��ģ�� ID</param>
        /// <param name="modelName">ʵ��ģ������</param>
        public ModelBase(int id, string modelName)
        {
            this.Id = id;
            this.ModelName = modelName;
        } 
        #endregion
    }
}
