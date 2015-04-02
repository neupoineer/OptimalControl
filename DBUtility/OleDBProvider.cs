using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;

namespace DBUtility
{
    /// <summary>
    /// 数据库访问的实现类(OLEDB)
    /// </summary>
    public class OleDBProvider : AbstractDBProvider
    {
        #region Private Members
        /// <summary>
        /// 数据库连接对象
        /// </summary>
        private OleDbConnection conn;
        /// <summary>
        /// 事务处理类对象
        /// </summary>
        private OleDbTransaction trans;
        /// <summary>
        /// 指示当前是否正处于事务中
        /// </summary>
        private bool inTransaction = false; 
        #endregion

        #region Protected Methods
        /// <summary>
        /// 返回执行SQL命令的command对象
        /// </summary>
        /// <param name="commandString">SQL命令</param>
        /// <returns>返回执行SQL命令的command对象(支持事务)</returns>
        protected override IDbCommand BuildCommand(string commandString)
        {
            IDbCommand command = new OleDbCommand(commandString, this.conn);
            command.CommandType = CommandType.Text;
            if (this.inTransaction)
                command.Transaction = this.trans;
            return command;
        } 
        #endregion

        #region Public Methods
        /// <summary>
        /// 构造函数(创建连接对象)
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        public OleDBProvider(string connectionString)
        {
            this.conn = new OleDbConnection(connectionString);
        }

        /// <summary>
        /// 打开数据库连接
        /// </summary>
        public override void Open()
        {
            if (conn.State != ConnectionState.Open)
                this.conn.Open();
        }

        /// <summary>
        /// 关闭数据库连接
        /// </summary>
        public override void Close()
        {
            if (conn.State == ConnectionState.Open)
                this.conn.Close();

        }

        /// <summary>
        /// 开始一个事务
        /// </summary>
        public override void BeginTrans()
        {
            trans = conn.BeginTransaction();
            inTransaction = true;
        }

        /// <summary>
        /// 提交一个事务
        /// </summary>
        public override void CommitTrans()
        {
            trans.Commit();
            this.Close();
            inTransaction = false;
        }

        /// <summary>
        /// 回滚一个事务
        /// </summary>
        public override void RollBackTrans()
        {
            trans.Rollback();
            this.Close();
            inTransaction = false;
        }

        /// <summary>
        /// 执行SQL命令并返回 DataSet
        /// </summary>
        /// <param name="commandString">SQL命令</param>
        /// <param name="tableName">要填充的表名</param>
        /// <returns>返回 DataSet</returns>
        public override DataSet RunCommand(string commandString, string tableName)
        {
            DataSet dataSet = new DataSet();
            this.Open();

            OleDbDataAdapter sqlDA = new OleDbDataAdapter();

            OleDbCommand command = this.BuildCommand(commandString) as OleDbCommand;
            sqlDA.SelectCommand = command;

            sqlDA.Fill(dataSet, tableName);

            if (inTransaction == false)
                this.Close();

            return dataSet;
        }


        /// <summary>
        /// 执行SQL命令并返回受影响的行数
        /// </summary>
        /// <param name="commandString">SQL命令</param>
        /// <param name="rowsAffected">返回影响的行数</param>
        public override void RunCommand(string commandString, out int rowsAffected)
        {
            this.Open();
            OleDbCommand command = BuildCommand(commandString) as OleDbCommand;
            rowsAffected = command.ExecuteNonQuery();

            if (this.inTransaction == false)
                this.Close();

        }

        /// <summary>
        /// 执行SQL命令并返回一个值
        /// </summary>
        /// <param name="commandString">SQL命令</param>
        /// <returns>执行SQL命令以返回一个 Object 值</returns>
        public override object RunCommand(string commandString)
        {
            object myObject;
            this.Open();

            OleDbCommand command = BuildCommand(commandString) as OleDbCommand;
            myObject = command.ExecuteScalar();

            if (this.inTransaction == false)
                this.Close();

            return myObject;
        } 
        #endregion
    }
}
