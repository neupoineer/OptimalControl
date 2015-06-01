using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using IDAL;
using Model;

namespace DAL
{
    /// <summary>
    /// 曲线数据访问操作类
    /// </summary>
    public class LogService:ILogService
    {
        #region ILogService 成员
        /// <summary>
        /// 根据日志时间获取日志实体
        /// </summary>
        /// <param name="startTime">日志开始时间.</param>
        /// <param name="endTime">日志结束时间.</param>
        /// <returns>
        /// 日志实体
        /// </returns>
        public List<Log> GetLogInfoByTime(DateTime startTime, DateTime endTime)
        {
            List<Log> logList = new List<Log>();
            //SQL命令
            string sqltxt = string.Format("Select * From Log Where Time >= '{0}' & Time <= '{1}'", startTime, endTime);

            // 从配置文件读取连接字符串
            string connectionString = ConfigurationManager.ConnectionStrings["SQLSERVER"].ConnectionString;

            // 执行 SQL 命令
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(sqltxt, conn);
                conn.Open();

                //创建曲线实体
                Log tmpLog = new Log();

                using (SqlDataReader myReader = cmd.ExecuteReader(
                    CommandBehavior.CloseConnection))
                {
                    while (myReader.Read())
                    {
                        //将数据集转换成实体集合
                        tmpLog.Id = Convert.ToInt32(myReader["Id"]);
                        tmpLog.LogTime = Convert.ToDateTime(myReader["LogTime"]);//yyyy-MM-dd hh:mm:ss
                        switch (Convert.ToInt32(myReader["Type"]))
                        {
                            case 0:
                                tmpLog.Type = Log.LogType.严重;
                                break;
                            case 1:
                                tmpLog.Type = Log.LogType.报警;
                                break;
                            case 2:
                                tmpLog.Type = Log.LogType.建议;
                                break;
                            case 3:
                                tmpLog.Type = Log.LogType.提示;
                                break;
                            default:
                                tmpLog.Type = Log.LogType.严重;
                                break;
                        }
                        tmpLog.Content = Convert.ToString(myReader["Content"]);
                        tmpLog.State = Convert.ToBoolean(myReader["State"]);
                        logList.Add(tmpLog);
                    }
                }
            }
            // 返回结果
            return logList;
        }

        /// <summary>
        /// 添加日志
        /// </summary>
        /// <param name="addLog">要添加的日志实体</param>
        /// <returns>True:成功/False:失败</returns>
        public bool AddLog(Log addLog)
        {
            // 拼接 SQL 命令
            const string sqlTxt = "INSERT INTO Log (LogTime,Type,Content,State) VALUES " +
                                  "(@LogTime,@Type,@Content,@State)";
            // 从配置文件读取连接字符串
            string connectionString = ConfigurationManager.ConnectionStrings["SQLSERVER"].ConnectionString;
            // 执行 SQL 命令
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(sqlTxt, conn);
                SqlParameter prm1 = new SqlParameter("@LogTime", SqlDbType.DateTime) { Value = addLog.LogTime };
                SqlParameter prm2 = new SqlParameter("@Type", SqlDbType.Int) { Value = Convert.ToInt32(addLog.Type) };
                SqlParameter prm3 = new SqlParameter("@Content", SqlDbType.NVarChar, 500) { Value = addLog.Content };
                SqlParameter prm4 = new SqlParameter("@State", SqlDbType.Bit) { Value = addLog.State };

                cmd.Parameters.AddRange(new SqlParameter[] { prm1, prm2, prm3, prm4 });
                conn.Open();

                if (cmd.ExecuteNonQuery() >= 1)
                    return true;
                else
                    return false;
            }
        }

        /// <summary>
        /// 获取所有日志信息
        /// </summary>
        /// <returns>日志实体集合</returns>
        public List<Log> GetAllLogInfo()
        {
            //SQL命令
            const string sqltxt = "SELECT * FROM Log";
            //创建日志实体集合
            List<Log> logCollection = new List<Log>();
            //定义日志实体

            // 从配置文件读取连接字符串
            string connectionString = ConfigurationManager.ConnectionStrings["SQLSERVER"].ConnectionString;
            // 执行 SQL 命令
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(sqltxt, conn);
                conn.Open();

                using (SqlDataReader myReader = cmd.ExecuteReader(
                    CommandBehavior.CloseConnection))
                {
                    while (myReader.Read())
                    {
                        // 创建曲线实体
                        Log tmpLog = new Log();
                        //将数据集转换成实体集合
                        tmpLog.Id = Convert.ToInt32(myReader["Id"]);
                        tmpLog.LogTime = Convert.ToDateTime(myReader["LogTime"]);//yyyy-MM-dd hh:mm:ss
                        switch (Convert.ToInt32(myReader["Type"]))
                        {
                            case 0:
                                tmpLog.Type = Log.LogType.严重;
                                break;
                            case 1:
                                tmpLog.Type = Log.LogType.报警;
                                break;
                            case 2:
                                tmpLog.Type = Log.LogType.建议;
                                break;
                            case 3:
                                tmpLog.Type = Log.LogType.提示;
                                break;
                            default:
                                tmpLog.Type = Log.LogType.严重;
                                break;
                        }
                        tmpLog.Content = Convert.ToString(myReader["Content"]);
                        tmpLog.State = Convert.ToBoolean(myReader["State"]);

                        // 添加到曲线实体集合
                        logCollection.Add(tmpLog);
                    }
                }
            }

            // 返回结果
            return logCollection;
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
            //SQL命令
            string sqltxt = string.Format("SELECT TOP {0} * FROM Log ORDER BY LogTime DESC", logCount);
            //创建日志实体集合
            List<Log> LogCollection = new List<Log>();
            //定义日志实体

            // 从配置文件读取连接字符串
            string connectionString = ConfigurationManager.ConnectionStrings["SQLSERVER"].ConnectionString;
            // 执行 SQL 命令
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(sqltxt, conn);
                conn.Open();

                using (SqlDataReader myReader = cmd.ExecuteReader(
                    CommandBehavior.CloseConnection))
                {
                    while (myReader.Read())
                    {
                        // 创建曲线实体
                        Log tmpLog = new Log();
                        //将数据集转换成实体集合
                        tmpLog.Id = Convert.ToInt32(myReader["Id"]);
                        tmpLog.LogTime = Convert.ToDateTime(myReader["LogTime"]);//yyyy-MM-dd hh:mm:ss
                        switch (Convert.ToInt32(myReader["Type"]))
                        {
                            case 0:
                                tmpLog.Type = Log.LogType.严重;
                                break;
                            case 1:
                                tmpLog.Type = Log.LogType.报警;
                                break;
                            case 2:
                                tmpLog.Type = Log.LogType.建议;
                                break;
                            case 3:
                                tmpLog.Type = Log.LogType.提示;
                                break;
                            default:
                                tmpLog.Type = Log.LogType.严重;
                                break;
                        }
                        tmpLog.Content = Convert.ToString(myReader["Content"]);
                        tmpLog.State = Convert.ToBoolean(myReader["State"]);

                        // 添加到曲线实体集合
                        LogCollection.Add(tmpLog);
                    }
                }
            }

            // 返回结果
            return LogCollection;
        }
        #endregion
    }
}
