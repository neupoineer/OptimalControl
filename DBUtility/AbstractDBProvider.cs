using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Configuration;
using System.Windows.Forms;

namespace DBUtility
{
    /// <summary>
    /// 数据库操作(抽象类)
    /// </summary>
    public abstract class AbstractDBProvider
    {
        #region Public Enum
        /// <summary>
        /// 数据库连接类型
        /// </summary>
        public enum DBConnectionType
        {
            /// <summary>
            /// MS SQL Server
            /// </summary>
            SqlServer,
            /// <summary>
            /// MS Access
            /// </summary>
            Access,
            /// <summary>
            /// MS SQLite
            /// </summary>
            SQLite
        } 
        #endregion

        #region Protected Methods
        /// <summary>
        /// 返回执行SQL命令的command对象
        /// </summary>
        /// <param name="commandString">SQL命令</param>
        /// <returns>返回执行SQL命令的command对象(支持事务)</returns>
        protected abstract IDbCommand BuildCommand(string commandString);
        #endregion

        #region Public Methods
        /// <summary>
        /// 打开数据库连接
        /// </summary>
        public abstract void Open();

        /// <summary>
        /// 关闭数据库连接
        /// </summary>
        public abstract void Close();

        /// <summary>
        /// 开始事务
        /// </summary>
        public abstract void BeginTrans();

        /// <summary>
        /// 提交事务
        /// </summary>
        public abstract void CommitTrans();

        /// <summary>
        /// 回滚事务
        /// </summary>
        public abstract void RollBackTrans();

        /// <summary>
        /// 执行SQL命令并返回 DataSet
        /// </summary>
        /// <param name="commandString">SQL命令</param>
        /// <param name="tableName">要填充的表名</param>
        /// <returns>返回 DataSet</returns>
        public abstract DataSet RunCommand(string commandString, string tableName);

        /// <summary>
        /// 执行SQL命令并返回受影响的行数
        /// </summary>
        /// <param name="commandString">SQL命令</param>
        /// <param name="rowsAffected">返回影响的行数</param>
        public abstract void RunCommand(string commandString, out int rowsAffected);

        /// <summary>
        /// 执行SQL命令并返回一个值
        /// </summary>
        /// <param name="commandString">SQL命令</param>
        /// <returns>执行SQL命令以返回一个 Object 值</returns>
        public abstract object RunCommand(string commandString);

        /// <summary>
        /// 通过运行目录下的 config 配置文件实例化一个数据库操作类
        /// </summary>
        /// <returns>返回数据库操作类的实例</returns>
        public static AbstractDBProvider Instance()
        {
            string connectionString;

            string connectionKey = ConfigurationManager.AppSettings["ConnectionType"].ToString();
            if (connectionKey.ToUpper().Trim() == "SQLSERVER")
            {
                connectionString = ConfigurationManager.ConnectionStrings["SQLSERVER"].ConnectionString;
                return new SqlDBProvider(connectionString);
            }
            //else if (connectionKey.ToUpper().Trim() == "SQLITE")
            //{
            //    connectionString = ConfigurationManager.ConnectionStrings["SQLITE"].ConnectionString;
            //    return new LiteDBProvider(connectionString);
            //}
            else 
            {
                connectionString = ConfigurationManager.ConnectionStrings["ACCESS"].ConnectionString;
                return new OleDBProvider(connectionString);
            }
        }

        /// <summary>
        /// 实例化一个数据库操作类
        /// </summary>
        /// <param name="connectionType">连接类型</param>
        /// <param name="connectionString">连接字符串</param>
        /// <returns>返回数据库操作类的实例</returns>
        public static AbstractDBProvider Instance(DBConnectionType connectionType, string connectionString)
        {
            if (connectionType == DBConnectionType.SqlServer)
                return new SqlDBProvider(connectionString);
            //else if (connectionType == DBConnectionType.SQLite)
            //    return new LiteDBProvider(connectionString);
            else
                return new OleDBProvider(connectionString);
        }  
        #endregion
    }
}
