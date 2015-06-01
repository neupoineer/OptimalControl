using BLL;
using BLL.Control;
using IBLL;
using IBLL.Control;

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

        /// <summary>
        /// 曲线业务逻辑管理类的生产方法
        /// </summary>
        /// <returns>曲线业务逻辑管理类实例</returns>
        public ICurveManager BuildCurveManager()
        {
            return new CurveManager();
        }

        /// <summary>
        /// 设备业务逻辑管理类的生产方法
        /// </summary>
        /// <returns>设备业务逻辑管理类实例</returns>
        public IDeviceManager BuildDeviceManager()
        {
            return new DeviceManager();
        }

        /// <summary>
        /// 规则业务逻辑管理类的生产方法
        /// </summary>
        /// <returns>规则业务逻辑管理类实例</returns>
        public IRuleManager BuildRuleManager()
        {
            return new RuleManager();
        }

        /// <summary>
        /// 变量业务逻辑管理类的生产方法
        /// </summary>
        /// <returns>变量业务逻辑管理类实例</returns>
        public IVariableManager BuildIVariableManager()
        {
            return new VariableManager();
        }

        /// <summary>
        /// 日志业务逻辑管理类的生产方法
        /// </summary>
        /// <returns>日志业务逻辑管理类实例</returns>
        public ILogManager BuildLogManager()
        {
            return new LogManager();
        }

        /// <summary>
        /// 数据业务逻辑管理类的生产方法
        /// </summary>
        /// <returns>数据业务逻辑管理类实例</returns>
        public IDataManager BuildDataManager()
        {
            return new DataManager();
        }
    }
}
