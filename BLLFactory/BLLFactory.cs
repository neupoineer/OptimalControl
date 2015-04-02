using System;
using System.Collections.Generic;
using System.Text;

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
        public IBLL.IOperatorManager BuildOperatorManager()
        {
            return new BLL.OperatorManager();
        }

        /// <summary>
        /// 权限组业务逻辑管理类的生产方法
        /// </summary>
        /// <returns>权限组业务逻辑管理类实例</returns>
        public IBLL.IRightsGroupManager BuildRightsGroupManager()
        {
            return new BLL.RightsGroupManager();
        }

        /// <summary>
        /// 权限关系业务逻辑管理类的生产方法
        /// </summary>
        /// <returns>权限关系业务逻辑管理类实例</returns>
        public IBLL.IRightsRelationManager BuildRightsRelationManager()
        {
            return new BLL.RightsRelationManager();
        }
    }
}
