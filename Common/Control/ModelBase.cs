using System;

namespace Common.Control
{
    /// <summary>
    /// 实体模型基类
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
        /// 实体模型 ID
        /// </summary>
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        /// <summary>
        /// 实体模型名称
        /// </summary>
        public string ModelName
        {
            get { return _modelName; }
            set { _modelName = value; }
        } 
        #endregion

        #region Public Methods
        /// <summary>
        /// 无参构造
        /// </summary>
        public ModelBase() { }

        /// <summary>
        /// 带参构造
        /// </summary>
        /// <param name="id">实体模型 ID</param>
        /// <param name="modelName">实体模型名称</param>
        public ModelBase(int id, string modelName)
        {
            this.Id = id;
            this.ModelName = modelName;
        } 
        #endregion
    }
}
