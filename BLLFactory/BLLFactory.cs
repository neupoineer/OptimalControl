using System;
using System.Collections.Generic;
using System.Text;
using BLL;
using IBLL;

namespace BLLFactory
{
    /// <summary>
    /// 业务逻辑层工厂类
    /// </summary>
    public class BLLFactory
    {
        /// <summary>
        /// 操作员业务逻辑管理类的生产方法
        /// </summary>
        /// <returns>操作员业务逻辑管理类实例</returns>
        public IOperatorManager BuildOperatorManager()
        {
            return new OperatorManager();
        }

        /// <summary>
        /// 权限组业务逻辑管理类的生产方法
        /// </summary>
        /// <returns>权限组业务逻辑管理类实例</returns>
        public IRightsGroupManager BuildRightsGroupManager()
        {
            return new RightsGroupManager();
        }

        /// <summary>
        /// 权限关系业务逻辑管理类的生产方法
        /// </summary>
        /// <returns>权限关系业务逻辑管理类实例</returns>
        public IRightsRelationManager BuildRightsRelationManager()
        {
            return new RightsRelationManager();
        }
    }
}
