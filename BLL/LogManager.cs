using System;
using System.Collections.Generic;
using IDAL;
using Model;
using IBLL;

namespace BLL
{

    /// <summary>
    /// 日志数据访问操作接口
    /// </summary>
    public class LogManager:ILogManager
    {
        /// <summary>
        /// 根据日志时间获取日志实体
        /// </summary>
        /// <param name="startTime">日志开始时间.</param>
        /// <param name="endTime">日志结束时间.</param>
        /// <returns>
        /// 日志实体
        /// </returns>
        public List<Log> GetLogInfoByTime(DateTime startTime, DateTime endTime) //定义并实例化抽象工厂类
        {
            DALFactory.AbstractDALFactory absDALFactory = DALFactory.AbstractDALFactory.Instance();
            //调用工厂方法生成实例
            ILogService logService = absDALFactory.BuildLogService();
            //调用实例方法
            return logService.GetLogInfoByTime(startTime,endTime);
        }

        /// <summary>
        /// 获取所有日志信息
        /// </summary>
        /// <returns>日志实体集合</returns>
        public List<Log> GetAllLogInfo()
        {
            DALFactory.AbstractDALFactory absDALFactory = DALFactory.AbstractDALFactory.Instance();
            //调用工厂方法生成实例
            ILogService logService = absDALFactory.BuildLogService();
            //调用实例方法
            return logService.GetAllLogInfo();
        }

        /// <summary>
        /// 获取最新的日志信息
        /// </summary>
        /// <param name="logCount">日志条数.</param>
        /// <returns>
        /// 日志实体集合
        /// </returns>
        public List<Log> GetLastLogInfos(int logCount)
        {
            DALFactory.AbstractDALFactory absDALFactory = DALFactory.AbstractDALFactory.Instance();
            //调用工厂方法生成实例
            ILogService logService = absDALFactory.BuildLogService();
            //调用实例方法
            return logService.GetLastLogInfos(logCount);
        }

        /// <summary>
        /// 添加日志
        /// </summary>
        /// <param name="addLog">要添加的日志实体</param>
        /// <returns>True:成功/False:失败</returns>
        public bool AddLog(Log addLog)
        {
            DALFactory.AbstractDALFactory absDALFactory = DALFactory.AbstractDALFactory.Instance();
            //调用工厂方法生成实例
            ILogService logService = absDALFactory.BuildLogService();
            //调用实例方法
            return logService.AddLog(addLog);
        }
    }
}
