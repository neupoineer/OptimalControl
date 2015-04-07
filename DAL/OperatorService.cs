using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Runtime.Serialization.Formatters.Binary;

namespace DAL
{
    /// <summary>
    /// ����Ա���ݷ��ʲ�����
    /// </summary>
    public class OperatorService : IDAL.IOperatorService
    {

        #region IOperatorService ��Ա

        /// <summary>
        /// ���ݲ���Ա���ƺ������ȡ����Աʵ��
        /// </summary>
        /// <param name="name">����Ա����</param>
        /// <param name="pwd">����Ա����</param>
        /// <returns>����Աʵ��</returns>
        public Model.Operator GetOperatorInfoByName(string name, string pwd)
        {
            //SQL����
            string sqltxt = string.Format("Select Id, OperatorName, Password, RightsList, State From Operator Where OperatorName = '{0}' And Password = '{1}'", name, pwd);

            //��������Աʵ��
            Model.Operator tmpOperator = new Model.Operator();

            // ת�����ݿ�洢�� ����������Ϊ Byte[] ���� �Ա����ת��Ϊ����ԱȨ�޼���
            // �������ļ���ȡ�����ַ���
            string connectionString = ConfigurationManager.ConnectionStrings["SQLSERVER"].ConnectionString;

            // ִ�� SQL ����
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(sqltxt, conn);
                conn.Open();

                using (SqlDataReader myReader = cmd.ExecuteReader(
                    CommandBehavior.CloseConnection | 
                    CommandBehavior.SingleResult | 
                    CommandBehavior.SingleRow))
                {
                    if (myReader.Read())
                    {
                        //�����ݼ�ת����ʵ�弯��
                        tmpOperator.Id = Convert.ToInt32(myReader["Id"]);
                        tmpOperator.ModelName = Convert.ToString(myReader["OperatorName"]);
                        tmpOperator.Password = Convert.ToString(myReader["Password"]);
                        tmpOperator.State = Convert.ToBoolean(myReader["State"]);

                        // ��ȡȨ�޼���
                        System.Data.SqlTypes.SqlBytes bytes = myReader.GetSqlBytes(3); // ֻ��ָ�������
                        // ���������л�ΪȨ�޼��϶���
                        BinaryFormatter bf = new BinaryFormatter();
                        if (!bytes.IsNull)
                            tmpOperator.RightsCollection = (bf.Deserialize(bytes.Stream) as Dictionary<string, Model.Rights>);
                        //else
                        //    throw new Exception(string.Format("����Ա [{0}] û���κ�Ȩ�ޣ���ֹ��¼��", tmpOperator.ModelName));
                    }
                    else
                        //���û�ж�ȡ���������׳��쳣
                        throw new Exception("��¼���ƻ��û����벻��ȷ��");
                }
            }
                
            // �������Ա�Ѿ�������
            if (!tmpOperator.State)
                throw new Exception(string.Format("����Ա [{0}] �ѱ����ã��������Ա��ϵ��", tmpOperator.ModelName));
            // ���ؽ��
            return tmpOperator;
        }

        /// <summary>
        /// ��Ӳ���Ա
        /// </summary>
        /// <param name="addOperator">Ҫ��ӵĲ���Աʵ��</param>
        /// <returns>True:�ɹ�/False:ʧ��</returns>
        public bool AddOperator(Model.Operator addOperator)
        {
            // ��֤���볤��
            if (addOperator.Password.Trim().Length < 6)
                throw new Exception("�û����볤�Ȳ���С����λ��");
            // ת������ԱȨ�޼���Ϊ���ݿ�ɴ�ȡ�� Byte[] ����
            MemoryStream ms = new MemoryStream();
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(ms, addOperator.RightsCollection);
            byte[] rigthsByteArray = new byte[(int)(ms.Length)];
            ms.Position = 0;
            ms.Read(rigthsByteArray, 0, (int)(ms.Length));
            ms.Close();

            // ƴ�� SQL ����
            string sqlTxt = "Insert Into Operator (OperatorName, Password, RightsList, State) Values " +
                "(@OperatorName, @Password, @RightsList, @State)";

            // �������ļ���ȡ�����ַ���
            string connectionString = ConfigurationManager.ConnectionStrings["SQLSERVER"].ConnectionString;
            // ִ�� SQL ����
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(sqlTxt, conn);
                SqlParameter prm1 = new SqlParameter("@OperatorName", SqlDbType.NVarChar, 50);
                prm1.Value = addOperator.ModelName;
                SqlParameter prm2 = new SqlParameter("@Password", SqlDbType.NVarChar, 50);
                prm2.Value = addOperator.Password;
                SqlParameter prm3 = new SqlParameter("@RightsList", SqlDbType.VarBinary, rigthsByteArray.Length,
                    ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Current, rigthsByteArray);
                SqlParameter prm4 = new SqlParameter("@State", SqlDbType.Bit);
                prm4.Value = addOperator.State;

                cmd.Parameters.AddRange(new SqlParameter[] { prm1, prm2, prm3, prm4 });
                conn.Open();

                if (cmd.ExecuteNonQuery() >= 1)
                    return true;
                else
                    return false;
            }
        }

        /// <summary>
        /// ɾ������Ա
        /// </summary>
        /// <param name="id">Ҫɾ���Ĳ���Ա ID</param>
        /// <returns>True:�ɹ�/False:ʧ��</returns>
        public bool DeleteOperatorByID(int id)
        {
            // ɾ��������Ϣ SQL ����
            string sqlTxt = string.Format("Delete From Operator Where Id = {0}", id);
            // ���� SQL ִ�ж���
            DBUtility.AbstractDBProvider dbProvider = DBUtility.AbstractDBProvider.Instance();
            // ִ�� ɾ������
            int rowsAffected;
            dbProvider.RunCommand(sqlTxt, out rowsAffected);

            if (rowsAffected >= 1)
                return true;
            else
                return false;
        }

        /// <summary>
        /// �޸Ĳ���Ա
        /// </summary>
        /// <param name="currentOperator">Ҫ�޸ĵĲ���Աʵ��</param>
        /// <returns>True:�ɹ�/False:ʧ��</returns>
        public bool ModifyOperator(Model.Operator currentOperator)
        {
            // ��֤���볤��
            if (currentOperator.Password.Trim().Length < 6)
                throw new Exception("�û����볤�Ȳ���С����λ��");
            // ת������ԱȨ�޼���Ϊ���ݿ�ɴ�ȡ�� Byte[] ����
            MemoryStream ms = new MemoryStream();
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(ms, currentOperator.RightsCollection);
            byte[] rigthsByteArray = new byte[(int)(ms.Length)];
            ms.Position = 0;
            ms.Read(rigthsByteArray, 0, (int)(ms.Length));
            ms.Close();

            // ƴ�� SQL ����
            string sqlTxt = "Update Operator Set OperatorName = @OperatorName, " +
                "Password = @Password, RightsList = @RightsList, State = @State Where Id = @Id";

            // �������ļ���ȡ�����ַ���
            string connectionString = ConfigurationManager.ConnectionStrings["SQLSERVER"].ConnectionString;
            // ִ�� SQL ����
            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(sqlTxt, conn);
                SqlParameter prm1 = new SqlParameter("@OperatorName", SqlDbType.NVarChar, 50);
                prm1.Value = currentOperator.ModelName;
                SqlParameter prm2 = new SqlParameter("@Password", SqlDbType.NVarChar, 50);
                prm2.Value = currentOperator.Password;
                SqlParameter prm3 = new SqlParameter("@RightsList", SqlDbType.VarBinary, rigthsByteArray.Length, 
                    ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Current, rigthsByteArray);
                SqlParameter prm4 = new SqlParameter("@State", SqlDbType.Bit);
                prm4.Value = currentOperator.State;
                SqlParameter prm5 = new SqlParameter("@Id", SqlDbType.Int);
                prm5.Value = currentOperator.Id;

                cmd.Parameters.AddRange(new SqlParameter[] { prm1, prm2, prm3, prm4, prm5 });
                conn.Open();

                if (cmd.ExecuteNonQuery() >= 1)
                    return true;
                else
                    return false;
            }
        }

        /// <summary>
        /// ��ȡ���в���Ա��Ϣ
        /// </summary>
        /// <returns>����Աʵ�弯��</returns>
        public Dictionary<string, Model.Operator> GetAllOperatorInfo()
        {
            //SQL����
            string sqltxt = "Select Id, OperatorName, Password, RightsList, State From Operator";
            //��������Աʵ�弯��
            Dictionary<string, Model.Operator> operatorCollection = new Dictionary<string, Model.Operator>();
            //�������Աʵ��
            Model.Operator tmpOperator = null;

            // ת�����ݿ�洢�� ����������Ϊ Byte[] ���� �Ա����ת��Ϊ����ԱȨ�޼���
            // �������ļ���ȡ�����ַ���
            string connectionString = ConfigurationManager.ConnectionStrings["SQLSERVER"].ConnectionString;
            // ִ�� SQL ����
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(sqltxt, conn);
                conn.Open();

                using (SqlDataReader myReader = cmd.ExecuteReader(
                    CommandBehavior.CloseConnection))
                {
                    while (myReader.Read())
                    {
                        // ��������Աʵ��
                        tmpOperator = new Model.Operator();
                        //�����ݼ�ת����ʵ�弯��
                        tmpOperator.Id = Convert.ToInt32(myReader["Id"]);
                        tmpOperator.ModelName = Convert.ToString(myReader["OperatorName"]);
                        tmpOperator.Password = Convert.ToString(myReader["Password"]);
                        tmpOperator.State = Convert.ToBoolean(myReader["State"]);

                        // ��ȡȨ�޼���
                        System.Data.SqlTypes.SqlBytes bytes = myReader.GetSqlBytes(3); // ֻ��ָ�������
                        // ���������л�ΪȨ�޼��϶���
                        BinaryFormatter bf = new BinaryFormatter();
                        if (!bytes.IsNull)
                            tmpOperator.RightsCollection = (bf.Deserialize(bytes.Stream) as Dictionary<string, Model.Rights>);

                        // ��ӵ�����Աʵ�弯��
                        operatorCollection.Add(tmpOperator.ModelName, tmpOperator);
                    }
                }
            }

            // ���ؽ��
            return operatorCollection;
        }

        /// <summary>
        /// ���ݲ���Ա����У�����Ա�Ƿ����
        /// </summary>
        /// <param name="operatorName">����Ա����</param>
        /// <returns>True:����/Flase:������</returns>
        public bool CheckOperatorExist(string operatorName)
        {
            //������ѯ��Ϣ�� SQL
            string sqlTxt = string.Format(
                "Select Count(*) From Operator Where OperatorName = '{0}'", 
                operatorName);
            //����SQLִ�ж���
            DBUtility.AbstractDBProvider dbProvider = DBUtility.AbstractDBProvider.Instance();
            //ִ�в�ѯ����
            int result = Convert.ToInt32(dbProvider.RunCommand(sqlTxt));

            if (result >= 1)
                return true;
            else
                return false;
        }

        #endregion
    }
}
