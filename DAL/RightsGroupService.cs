using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Runtime.Serialization.Formatters.Binary;
using Model.Rights;

namespace DAL
{
    /// <summary>
    /// Ȩ�������ݷ��ʲ�����
    /// </summary>
    public class RightsGroupService : IDAL.IRightsGroupService
    {
        #region IRightsGroupService ��Ա

        /// <summary>
        /// ��ȡ����Ȩ������Ϣ
        /// </summary>
        /// <returns>Ȩ����ʵ�弯��</returns>
        public Dictionary<string, RightsGroup> GetAllRightsGroupInfo()
        {
            //SQL����
            string sqltxt = "Select Id, GroupName, GroupRightsList From RightsGroup";
            //����Ȩ����ʵ�弯��
            Dictionary<string, RightsGroup> rightsGroupCollection = new Dictionary<string, RightsGroup>();
            //����Ȩ����ʵ��
            RightsGroup tmpRightsGroup = null;

            // ת�����ݿ�洢�� ����������Ϊ Byte[] ���� �Ա����ת��ΪȨ����Ȩ�޼���
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
                        // ����Ȩ����ʵ��
                        tmpRightsGroup = new RightsGroup();
                        //�����ݼ�ת����ʵ�弯��
                        tmpRightsGroup.Id = Convert.ToInt32(myReader["Id"]);
                        tmpRightsGroup.ModelName = Convert.ToString(myReader["GroupName"]);

                        // ��ȡȨ�޼���
                        System.Data.SqlTypes.SqlBytes bytes = myReader.GetSqlBytes(2); // ֻ��ָ�������
                        // ���������л�ΪȨ�޼��϶���
                        BinaryFormatter bf = new BinaryFormatter();
                        if (!bytes.IsNull)
                            tmpRightsGroup.GroupRightsCollection = (bf.Deserialize(bytes.Stream) as Dictionary<string, Rights>);

                        // ��ӵ�Ȩ����ʵ�弯��
                        rightsGroupCollection.Add(tmpRightsGroup.ModelName, tmpRightsGroup);
                    }
                }
            }

            // ���ؽ��
            return rightsGroupCollection;
        }

        /// <summary>
        /// ���Ȩ����
        /// </summary>
        /// <param name="addRightsGroup">Ҫ��ӵ�Ȩ����ʵ��</param>
        /// <returns>True:�ɹ�/False:ʧ��</returns>
        public bool AddRightsGroup(RightsGroup addRightsGroup)
        {
            // ת��Ȩ����Ȩ�޼���Ϊ���ݿ�ɴ�ȡ�� Byte[] ����
            MemoryStream ms = new MemoryStream();
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(ms, addRightsGroup.GroupRightsCollection);
            byte[] rigthsByteArray = new byte[(int)(ms.Length)];
            ms.Position = 0;
            ms.Read(rigthsByteArray, 0, (int)(ms.Length));
            ms.Close();

            // ƴ�� SQL ����
            string sqlTxt = "Insert Into RightsGroup (GroupName, GroupRightsList) Values " +
                "(@GroupName, @GroupRightsList)";

            // �������ļ���ȡ�����ַ���
            string connectionString = ConfigurationManager.ConnectionStrings["SQLSERVER"].ConnectionString;
            // ִ�� SQL ����
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(sqlTxt, conn);
                SqlParameter prm1 = new SqlParameter("@GroupName", SqlDbType.NVarChar, 50);
                prm1.Value = addRightsGroup.ModelName;
                SqlParameter prm2 = new SqlParameter("@GroupRightsList", SqlDbType.VarBinary, rigthsByteArray.Length,
                    ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Current, rigthsByteArray);

                cmd.Parameters.AddRange(new SqlParameter[] { prm1, prm2 });
                conn.Open();

                if (cmd.ExecuteNonQuery() >= 1)
                    return true;
                else
                    return false;
            }
        }

        /// <summary>
        /// ɾ��Ȩ����
        /// </summary>
        /// <param name="id">Ҫɾ����Ȩ���� ID</param>
        /// <returns>True:�ɹ�/False:ʧ��</returns>
        public bool DeleteRightsGroupByID(int id)
        {
            // ɾ��������Ϣ SQL ����
            string sqlTxt = string.Format("Delete From RightsGroup Where Id = {0}", id);
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
        /// �޸�Ȩ����
        /// </summary>
        /// <param name="currentRightsGroup">Ҫ�޸ĵ�Ȩ����ʵ��</param>
        /// <returns>True:�ɹ�/False:ʧ��</returns>
        public bool ModifyRightsGroup(RightsGroup currentRightsGroup)
        {
            // ת��Ȩ����Ȩ�޼���Ϊ���ݿ�ɴ�ȡ�� Byte[] ����
            MemoryStream ms = new MemoryStream();
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(ms, currentRightsGroup.GroupRightsCollection);
            byte[] rigthsByteArray = new byte[(int)(ms.Length)];
            ms.Position = 0;
            ms.Read(rigthsByteArray, 0, (int)(ms.Length));
            ms.Close();

            // ƴ�� SQL ����
            string sqlTxt = "Update RightsGroup Set GroupName = @GroupName, GroupRightsList = @GroupRightsList Where Id = @Id";

            // �������ļ���ȡ�����ַ���
            string connectionString = ConfigurationManager.ConnectionStrings["SQLSERVER"].ConnectionString;
            // ִ�� SQL ����
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(sqlTxt, conn);
                SqlParameter prm1 = new SqlParameter("@GroupName", SqlDbType.NVarChar, 50);
                prm1.Value = currentRightsGroup.ModelName;
                SqlParameter prm2 = new SqlParameter("@GroupRightsList", SqlDbType.VarBinary, rigthsByteArray.Length,
                    ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Current, rigthsByteArray);
                SqlParameter prm3 = new SqlParameter("@Id", SqlDbType.Int);
                prm3.Value = currentRightsGroup.Id;

                cmd.Parameters.AddRange(new SqlParameter[] { prm1, prm2, prm3 });
                conn.Open();

                if (cmd.ExecuteNonQuery() >= 1)
                    return true;
                else
                    return false;
            }
        }

        /// <summary>
        /// ����Ȩ��������У��Ȩ�����Ƿ��Ѿ�����
        /// </summary>
        /// <param name="rightsGroupName">Ȩ��������</param>
        /// <returns>True:����/False:������</returns>
        public bool CheckRightsGroupExist(string rightsGroupName)
        {
            //SQL����
            string sqlTxt = string.Format("Select Count(*) From RightsGroup Where GroupName = '{0}'", rightsGroupName);

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
