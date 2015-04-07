// ***********************************************************************
// Assembly         : Form1.cs
// Author           : Jeffrey
// Created          : 03-24-2013
//
// Last Modified By : Jeffrey
// Last Modified On : 04-02-2013
// ***********************************************************************
// <copyright file="SQLHelper.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Common
{
    /// <summary>
    /// 数据库的通用访问代码
    /// 此类为抽象类abstract，不允许实例化，在应用时直接调用即可
    /// </summary>
    public static class SQLHelper
    {
        //获取数据库连接字符串，其属于静态变量且只读，项目中所有文档可以直接使用，但不能修改
        /// <summary>
        /// The connection string local transaction
        /// </summary>
        public static readonly string ConnectionStringLocalTransaction =
            ConfigurationManager.ConnectionStrings["SQLSERVER"].ConnectionString;

        /// <summary>
        /// The connection local transaction
        /// </summary>
        public static SqlConnection ConnectionLocalTransaction = null;

        // 哈希表用来存储缓存的参数信息，哈希表可以存储任意类型的参数。

        /// <summary>
        /// The parm cache
        /// </summary>
        private static Hashtable parmCache = Hashtable.Synchronized(new Hashtable());

        /// <summary>
        /// 执行一个不需要返回值的SqlCommand命令，通过指定专用的连接字符串。
        /// 使用参数数组形式提供参数列表
        /// </summary>
        /// <param name="connectionString">一个有效的数据库连接字符串</param>
        /// <param name="cmdType">Type of the CMD.</param>
        /// <param name="cmdText">The CMD text.</param>
        /// <param name="commandParameters">以数组形式提供SqlCommand命令中用到的参数列表</param>
        /// <returns>返回一个数值表示此SqlCommand命令执行后影响的行数</returns>
        /// <remarks>使用示例：
        /// int result = ExecuteNonQuery(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));</remarks>
        public static int ExecuteNonQuery(string connectionString, CommandType cmdType, string cmdText,
                                          params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                //通过PrePareCommand方法将参数逐个加入到SqlCommand的参数集合中
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                int val = cmd.ExecuteNonQuery();
                //清空SqlCommand中的参数列表
                cmd.Parameters.Clear();
                return val;
            }
        }

        /// <summary>
        /// 执行一条不返回结果的SqlCommand，通过一个已经存在的数据库连接
        /// 使用参数数组提供参数
        /// </summary>
        /// <param name="connection">The connection.</param>
        /// <param name="cmdType">Type of the CMD.</param>
        /// <param name="cmdText">The CMD text.</param>
        /// <param name="commandParameters">以数组形式提供SqlCommand命令中用到的参数列表</param>
        /// <returns>返回一个数值表示此SqlCommand命令执行后影响的行数</returns>
        /// <remarks>使用示例：
        /// int result = ExecuteNonQuery(conn, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));</remarks>
        public static int ExecuteNonQuery(SqlConnection connection, CommandType cmdType, string cmdText,
                                          params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }

        /// <summary>
        /// 执行一条不返回结果的SqlCommand，通过一个已经存在的数据库事物处理
        /// 使用参数数组提供参数
        /// </summary>
        /// <param name="trans">一个存在的 sql 事物处理</param>
        /// <param name="cmdType">Type of the CMD.</param>
        /// <param name="cmdText">The CMD text.</param>
        /// <param name="commandParameters">以数组形式提供SqlCommand命令中用到的参数列表</param>
        /// <returns>返回一个数值表示此SqlCommand命令执行后影响的行数</returns>
        /// <remarks>使用示例：
        /// int result = ExecuteNonQuery(trans, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));</remarks>
        public static int ExecuteNonQuery(SqlTransaction trans, CommandType cmdType, string cmdText,
                                          params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, commandParameters);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }

        /// <summary>
        /// 执行一条返回结果集的SqlCommand命令，通过专用的连接字符串。
        /// 使用参数数组提供参数
        /// </summary>
        /// <param name="connectionString">一个有效的数据库连接字符串</param>
        /// <param name="cmdType">Type of the CMD.</param>
        /// <param name="cmdText">The CMD text.</param>
        /// <param name="commandParameters">以数组形式提供SqlCommand命令中用到的参数列表</param>
        /// <returns>返回一个包含结果的SqlDataReader</returns>
        /// <remarks>使用示例：
        /// SqlDataReader r = ExecuteReader(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));</remarks>
        public static DataSet ExecuteReader(string connectionString, CommandType cmdType, string cmdText,
                                            params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection(connectionString);
            // 在这里使用try/catch处理是因为如果方法出现异常，则SqlDataReader就不存在，
            //CommandBehavior.CloseConnection的语句就不会执行，触发的异常由catch捕获。
            //关闭数据库连接，并通过throw再次引发捕捉到的异常。
            try
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                // SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                cmd.Parameters.Clear();
                return ds;
            }
            catch
            {
                conn.Close();
                throw;
            }
        }

        /// <summary>
        /// 执行一条返回第一条记录第一列的SqlCommand命令，通过专用的连接字符串。
        /// 使用参数数组提供参数
        /// </summary>
        /// <param name="connectionString">一个有效的数据库连接字符串</param>
        /// <param name="cmdType">Type of the CMD.</param>
        /// <param name="cmdText">The CMD text.</param>
        /// <param name="commandParameters">以数组形式提供SqlCommand命令中用到的参数列表</param>
        /// <returns>
        /// 返回一个object类型的数据，可以通过 Convert.To{Type}方法转换类型
        /// </returns>
        /// <remarks>
        /// 使用示例：
        /// Object obj = ExecuteScalar(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        public static object ExecuteScalar(string connectionString, CommandType cmdType, string cmdText,
                                           params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
                object val = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                return val;
            }
        }

        /// <summary>
        /// 执行一条返回第一条记录第一列的SqlCommand命令，通过已经存在的数据库连接。
        /// 使用参数数组提供参数
        /// </summary>
        /// <param name="connection">The connection.</param>
        /// <param name="cmdType">Type of the CMD.</param>
        /// <param name="cmdText">The CMD text.</param>
        /// <param name="commandParameters">以数组形式提供SqlCommand命令中用到的参数列表</param>
        /// <returns>
        /// 返回一个object类型的数据，可以通过 Convert.To{Type}方法转换类型
        /// </returns>
        /// <remarks>
        /// 使用示例：
        /// Object obj = ExecuteScalar(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        public static object ExecuteScalar(SqlConnection connection, CommandType cmdType, string cmdText,
                                           params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
            object val = cmd.ExecuteScalar();
            cmd.Parameters.Clear();
            return val;
        }

        /// <summary>
        /// 缓存参数数组
        /// </summary>
        /// <param name="cacheKey">参数缓存的键值</param>
        /// <param name="commandParameters">The command parameters.</param>
        public static void CacheParameters(string cacheKey, params SqlParameter[] commandParameters)
        {
            parmCache[cacheKey] = commandParameters;
        }

        /// <summary>
        /// 获取被缓存的参数
        /// </summary>
        /// <param name="cacheKey">用于查找参数的KEY值</param>
        /// <returns>返回缓存的参数数组</returns>
        public static SqlParameter[] GetCachedParameters(string cacheKey)
        {
            SqlParameter[] cachedParms = (SqlParameter[]) parmCache[cacheKey];
            if (cachedParms == null)
                return null;
            //新建一个参数的克隆列表
            SqlParameter[] clonedParms = new SqlParameter[cachedParms.Length];
            //通过循环为克隆参数列表赋值
            for (int i = 0, j = cachedParms.Length; i < j; i++)
                //使用clone方法复制参数列表中的参数
                clonedParms[i] = (SqlParameter) ((ICloneable) cachedParms[i]).Clone();
            return clonedParms;
        }

        /// <summary>
        /// 为执行命令准备参数
        /// </summary>
        /// <param name="cmd">SqlCommand 命令</param>
        /// <param name="conn">已经存在的数据库连接</param>
        /// <param name="trans">数据库事物处理</param>
        /// <param name="cmdType">SqlCommand命令类型 (存储过程， T-SQL语句， 等等。)</param>
        /// <param name="cmdText">Command text，T-SQL语句 例如 Select * from Products</param>
        /// <param name="cmdParms">返回带参数的命令</param>
        private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, CommandType cmdType,
                                           string cmdText, SqlParameter[] cmdParms)
        {
            //判断数据库连接状态
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            //判断是否需要事物处理
            if (trans != null)
                cmd.Transaction = trans;
            cmd.CommandType = cmdType;
            if (cmdParms != null)
            {
                foreach (SqlParameter parm in cmdParms)
                    cmd.Parameters.Add(parm);
            }
        }

        /// <summary>
        /// Test The SQL Connection
        /// </summary>
        /// <param name="connstr">Connection string</param>
        /// <returns>The result.</returns>
        public static bool TestConnection(string connstr)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = connstr;
                try
                {
                    conn.Open();
                }
                catch (Exception)
                {
                    //MessageBox.Show(ex.Message, "连接失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Excutes the data table.
        /// </summary>
        /// <param name="connstr">The connection string.</param>
        /// <param name="sql">The SQL Command.</param>
        /// <param name="parameters">The SQL parameters.</param>
        /// <returns>DataTable</returns>
        public static DataTable ExcuteDataTable(string connstr, string sql, params SqlParameter[] parameters)
        {
            DataSet dataset = new DataSet();
            try
            {
                SqlConnection conn = new SqlConnection {ConnectionString = connstr};
                conn.Open();
                if (SQLHelper.ConnectionLocalTransaction != null)
                {
                    if (SQLHelper.ConnectionLocalTransaction.State.Equals(ConnectionState.Open))
                    {
                        SQLHelper.ConnectionLocalTransaction.Close();
                        SQLHelper.ConnectionLocalTransaction.Dispose();
                    }
                }
                SQLHelper.ConnectionLocalTransaction = conn;
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    foreach (SqlParameter parameter in parameters)
                    {
                        cmd.Parameters.Add(parameter);
                    }
                    cmd.CommandTimeout = 300;
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dataset);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
                //MessageBox.Show(ex.Message, "连接失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return dataset.Tables[0];
        }

        /// <summary>
        /// Excutes the data table.
        /// </summary>
        /// <param name="conn">The conn.</param>
        /// <param name="sql">The SQL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>DataTable.</returns>
        public static DataTable ExcuteDataTable(SqlConnection conn, string sql, params SqlParameter[] parameters)
        {
            if (conn == null)
            {
                MessageBox.Show("所提供SqlConnection为空");
                return null;
            }
            else
            {
                if (!conn.State.Equals(ConnectionState.Open))
                {
                    MessageBox.Show("所提供SqlConnection未开启或已注销");
                    return null;
                }
                else
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = sql;
                        foreach (SqlParameter parameter in parameters)
                        {
                            cmd.Parameters.Add(parameter);
                        }
                        DataSet dataset = new DataSet();
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(dataset);

                        return dataset.Tables[0];
                    }
                }
            }
        }
    }
}
