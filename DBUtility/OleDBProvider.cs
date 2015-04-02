using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;

namespace DBUtility
{
    /// <summary>
    /// ���ݿ���ʵ�ʵ����(OLEDB)
    /// </summary>
    public class OleDBProvider : AbstractDBProvider
    {
        #region Private Members
        /// <summary>
        /// ���ݿ����Ӷ���
        /// </summary>
        private OleDbConnection conn;
        /// <summary>
        /// �����������
        /// </summary>
        private OleDbTransaction trans;
        /// <summary>
        /// ָʾ��ǰ�Ƿ�������������
        /// </summary>
        private bool inTransaction = false; 
        #endregion

        #region Protected Methods
        /// <summary>
        /// ����ִ��SQL�����command����
        /// </summary>
        /// <param name="commandString">SQL����</param>
        /// <returns>����ִ��SQL�����command����(֧������)</returns>
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
        /// ���캯��(�������Ӷ���)
        /// </summary>
        /// <param name="connectionString">�����ַ���</param>
        public OleDBProvider(string connectionString)
        {
            this.conn = new OleDbConnection(connectionString);
        }

        /// <summary>
        /// �����ݿ�����
        /// </summary>
        public override void Open()
        {
            if (conn.State != ConnectionState.Open)
                this.conn.Open();
        }

        /// <summary>
        /// �ر����ݿ�����
        /// </summary>
        public override void Close()
        {
            if (conn.State == ConnectionState.Open)
                this.conn.Close();

        }

        /// <summary>
        /// ��ʼһ������
        /// </summary>
        public override void BeginTrans()
        {
            trans = conn.BeginTransaction();
            inTransaction = true;
        }

        /// <summary>
        /// �ύһ������
        /// </summary>
        public override void CommitTrans()
        {
            trans.Commit();
            this.Close();
            inTransaction = false;
        }

        /// <summary>
        /// �ع�һ������
        /// </summary>
        public override void RollBackTrans()
        {
            trans.Rollback();
            this.Close();
            inTransaction = false;
        }

        /// <summary>
        /// ִ��SQL������� DataSet
        /// </summary>
        /// <param name="commandString">SQL����</param>
        /// <param name="tableName">Ҫ���ı���</param>
        /// <returns>���� DataSet</returns>
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
        /// ִ��SQL���������Ӱ�������
        /// </summary>
        /// <param name="commandString">SQL����</param>
        /// <param name="rowsAffected">����Ӱ�������</param>
        public override void RunCommand(string commandString, out int rowsAffected)
        {
            this.Open();
            OleDbCommand command = BuildCommand(commandString) as OleDbCommand;
            rowsAffected = command.ExecuteNonQuery();

            if (this.inTransaction == false)
                this.Close();

        }

        /// <summary>
        /// ִ��SQL�������һ��ֵ
        /// </summary>
        /// <param name="commandString">SQL����</param>
        /// <returns>ִ��SQL�����Է���һ�� Object ֵ</returns>
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
