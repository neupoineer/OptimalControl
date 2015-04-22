using System;
using System.Collections.Generic;
using System.Text;
using DAL;
using IDAL;
using DAL.Control;
using IDAL.Control;

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
        public override IOperatorService BuildOperatorService()
        {
            return new OperatorService();
        }

        /// <summary>
        /// 权限组数据访问操作类的生产方法
        /// </summary>
        /// <returns>权限组数据访问操作类实例</returns>
        public override IRightsGroupService BuildRightsGroupService()
        {
            return new RightsGroupService();
        }

        /// <summary>
        /// 权限关系数据访问操作类的生产方法
        /// </summary>
        /// <returns>权限关系数据访问操作类实例</returns>
        public override IRightsRelationService BuildRightsRelationService()
        {
            return new RightsRelationService();
        }
        public override ICurveService BuildCurveService()
        {
            return new CurveService();
        }
        public override IDeviceService BuildDeviceService()
        {
            return new DeviceService();
        }
        public override IRuleService BuildRuleService()
        {
            return new RuleService();
        }
        public override IVariableService BuildVariableService()
        {
            return new VariableService();
        }
        public override ILogService BuildLogService()
        {
            return new LogService();
        }
        public override IDataService BuildDataService()
        {
            return new DataService();
        }
    }
}
