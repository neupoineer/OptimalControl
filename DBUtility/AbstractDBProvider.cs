using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Configuration;
using System.Windows.Forms;

namespace DBUtility
{
    /// <summary>
    /// ���ݿ����(������)
    /// </summary>
    public abstract class AbstractDBProvider
    {
        #region Public Enum
        /// <summary>
        /// ���ݿ���������
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
        /// ����ִ��SQL�����command����
        /// </summary>
        /// <param name="commandString">SQL����</param>
        /// <returns>����ִ��SQL�����command����(֧������)</returns>
        protected abstract IDbCommand BuildCommand(string commandString);
        #endregion

        #region Public Methods
        /// <summary>
        /// �����ݿ�����
        /// </summary>
        public abstract void Open();

        /// <summary>
        /// �ر����ݿ�����
        /// </summary>
        public abstract void Close();

        /// <summary>
        /// ��ʼ����
        /// </summary>
        public abstract void BeginTrans();

        /// <summary>
        /// �ύ����
        /// </summary>
        public abstract void CommitTrans();

        /// <summary>
        /// �ع�����
        /// </summary>
        public abstract void RollBackTrans();

        /// <summary>
        /// ִ��SQL������� DataSet
        /// </summary>
        /// <param name="commandString">SQL����</param>
        /// <param name="tableName">Ҫ���ı���</param>
        /// <returns>���� DataSet</returns>
        public abstract DataSet RunCommand(string commandString, string tableName);

        /// <summary>
        /// ִ��SQL���������Ӱ�������
        /// </summary>
        /// <param name="commandString">SQL����</param>
        /// <param name="rowsAffected">����Ӱ�������</param>
        public abstract void RunCommand(string commandString, out int rowsAffected);

        /// <summary>
        /// ִ��SQL�������һ��ֵ
        /// </summary>
        /// <param name="commandString">SQL����</param>
        /// <returns>ִ��SQL�����Է���һ�� Object ֵ</returns>
        public abstract object RunCommand(string commandString);

        /// <summary>
        /// ͨ������Ŀ¼�µ� config �����ļ�ʵ����һ�����ݿ������
        /// </summary>
        /// <returns>�������ݿ�������ʵ��</returns>
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
        /// ʵ����һ�����ݿ������
        /// </summary>
        /// <param name="connectionType">��������</param>
        /// <param name="connectionString">�����ַ���</param>
        /// <returns>�������ݿ�������ʵ��</returns>
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
