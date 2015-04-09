using System;

namespace Utility.Control
{
    /// <summary>
    /// ʵ��ģ�ͻ���
    /// </summary>
    [Serializable]
    public class ModelBase
    {
        #region Private Members
        int _id;
        string _name; 
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
        public string Name
        {
            get { return _name; }
            set { _name = value; }
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
        public ModelBase(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        } 
        #endregion
    }
}
