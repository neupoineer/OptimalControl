using System;
using System.Collections.Generic;
using System.Text;
using IDAL;
using DAL;
using Model;

namespace DALFactory
{
    /// <summary>
    /// 数据访问层工厂类
    /// </summary>
    public class DALFactory : AbstractDALFactory
    {
        /// <summary>
        /// 操作员数据访问操作类的生产方法
        /// </summary>
        /// <returns>操作员数据访问操作类实例</returns>
        public override IDAL.IOperatorService BuildOperatorService()
        {
            return new DAL.OperatorService();
        }

        /// <summary>
        /// 权限组数据访问操作类的生产方法
        /// </summary>
        /// <returns>权限组数据访问操作类实例</returns>
        public override IDAL.IRightsGroupService BuildRightsGroupService()
        {
            return new DAL.RightsGroupService();
        }

        /// <summary>
        /// 权限关系数据访问操作类的生产方法
        /// </summary>
        /// <returns>权限关系数据访问操作类实例</returns>
        public override IDAL.IRightsRelationService BuildRightsRelationService()
        {
            return new DAL.RightsRelationService();
        }
    }
}
