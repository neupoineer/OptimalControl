using System;
using System.Collections.Generic;
using System.Data;
using Model;

namespace IDAL
{

    /// <summary>
    /// 日志数据访问操作接口
    /// </summary>
    public interface ILogService
    {
        /// <summary>
        /// 根据日志时间获取日志实体
        /// </summary>
        /// <param name="startTime">日志开始时间.</param>
        /// <param name="endTime">日志结束时间.</param>
        /// <returns>
        /// 日志实体
        /// </returns>
        List<Log> GetLogInfoByTime(DateTime startTime, DateTime endTime);

        /// <summary>
        /// 获取所有日志信息
        /// </summary>
        /// <returns>日志实体集合</returns>
        List<Log> GetAllLogInfo();

        /// <summary>
        /// 获取最新的日志信息
        /// </summary>
        /// <param name="logCount">日志条数.</param>
        /// <returns>
        /// 日志实体集合
        /// </returns>
        List<Log> GetLastLogInfos(int logCount);

        /// <summary>
        /// 添加日志
        /// </summary>
        /// <param name="addLog">要添加的日志实体</param>
        /// <returns>True:成功/False:失败</returns>
        bool AddLog(Log addLog);
    }
}
