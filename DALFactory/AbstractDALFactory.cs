using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using IDAL;

namespace DALFactory
{
    /// <summary>
    /// 数据访问层抽象工厂类
    /// </summary>
    public abstract class AbstractDALFactory
    {
        #region Abstract Methods
        /// <summary>
        /// 操作员数据访问操作类的生产方法
        /// </summary>
        /// <returns>操作员数据访问操作类实例</returns>
        public abstract IOperatorService BuildOperatorService();
        /// <summary>
        /// 权限组数据访问操作类的生产方法
        /// </summary>
        /// <returns>权限组数据访问操作类实例</returns>
        public abstract IRightsGroupService BuildRightsGroupService();
        /// <summary>
        /// 权限关系数据访问操作类的生产方法
        /// </summary>
        /// <returns>权限关系数据访问操作类实例</returns>
        public abstract IRightsRelationService BuildRightsRelationService();
        #endregion

        #region Static Methods
        /// <summary>
        /// 创建数据访问层抽象工厂类实例
        /// </summary>
        /// <returns>数据访问层抽象工厂类实例</returns>
        public static AbstractDALFactory Instance()
        {
            // 保存本系统所使用的数据类型
            string dbType = string.Empty;
            // 读取配置文件中保存的数据类型
            dbType = ConfigurationManager.AppSettings["ConnectionType"].ToString();
            // 根据保存的数据类型确定使用具体的数据访问层工厂类
            if (dbType.ToUpper().Trim() == "SQLSERVER")
                return new DALFactory();
            else if (dbType.ToUpper().Trim() == "SQLITE")
                return new DALFactory();
            else
                return null;
        } 
        #endregion
    }
}
