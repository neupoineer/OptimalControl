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
    /// 权限组数据访问操作类
    /// </summary>
    public class RightsGroupService : IDAL.IRightsGroupService
    {
        #region IRightsGroupService 成员

        /// <summary>
        /// 获取所有权限组信息
        /// </summary>
        /// <returns>权限组实体集合</returns>
        public Dictionary<string, RightsGroup> GetAllRightsGroupInfo()
        {
            //SQL命令
            string sqltxt = "Select Id, GroupName, GroupRightsList From RightsGroup";
            //创建权限组实体集合
            Dictionary<string, RightsGroup> rightsGroupCollection = new Dictionary<string, RightsGroup>();
            //定义权限组实体
            RightsGroup tmpRightsGroup = null;

            // 转换数据库存储的 二进制数据为 Byte[] 数组 以便进而转换为权限组权限集合
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
                        // 创建权限组实体
                        tmpRightsGroup = new RightsGroup();
                        //将数据集转换成实体集合
                        tmpRightsGroup.Id = Convert.ToInt32(myReader["Id"]);
                        tmpRightsGroup.ModelName = Convert.ToString(myReader["GroupName"]);

                        // 读取权限集合
                        System.Data.SqlTypes.SqlBytes bytes = myReader.GetSqlBytes(2); // 只能指定列序号
                        // 将流反序列化为权限集合对象
                        BinaryFormatter bf = new BinaryFormatter();
                        if (!bytes.IsNull)
                            tmpRightsGroup.GroupRightsCollection = (bf.Deserialize(bytes.Stream) as Dictionary<string, Rights>);

                        // 添加到权限组实体集合
                        rightsGroupCollection.Add(tmpRightsGroup.ModelName, tmpRightsGroup);
                    }
                }
            }

            // 返回结果
            return rightsGroupCollection;
        }

        /// <summary>
        /// 添加权限组
        /// </summary>
        /// <param name="addRightsGroup">要添加的权限组实体</param>
        /// <returns>True:成功/False:失败</returns>
        public bool AddRightsGroup(RightsGroup addRightsGroup)
        {
            // 转换权限组权限集合为数据库可存取的 Byte[] 数组
            MemoryStream ms = new MemoryStream();
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(ms, addRightsGroup.GroupRightsCollection);
            byte[] rigthsByteArray = new byte[(int)(ms.Length)];
            ms.Position = 0;
            ms.Read(rigthsByteArray, 0, (int)(ms.Length));
            ms.Close();

            // 拼接 SQL 命令
            string sqlTxt = "Insert Into RightsGroup (GroupName, GroupRightsList) Values " +
                "(@GroupName, @GroupRightsList)";

            // 从配置文件读取连接字符串
            string connectionString = ConfigurationManager.ConnectionStrings["SQLSERVER"].ConnectionString;
            // 执行 SQL 命令
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
        /// 删除权限组
        /// </summary>
        /// <param name="id">要删除的权限组 ID</param>
        /// <returns>True:成功/False:失败</returns>
        public bool DeleteRightsGroupByID(int id)
        {
            // 删除单个信息 SQL 命令
            string sqlTxt = string.Format("Delete From RightsGroup Where Id = {0}", id);
            // 创建 SQL 执行对象
            DBUtility.AbstractDBProvider dbProvider = DBUtility.AbstractDBProvider.Instance();
            // 执行 删除操作
            int rowsAffected;
            dbProvider.RunCommand(sqlTxt, out rowsAffected);

            if (rowsAffected >= 1)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 修改权限组
        /// </summary>
        /// <param name="currentRightsGroup">要修改的权限组实体</param>
        /// <returns>True:成功/False:失败</returns>
        public bool ModifyRightsGroup(RightsGroup currentRightsGroup)
        {
            // 转换权限组权限集合为数据库可存取的 Byte[] 数组
            MemoryStream ms = new MemoryStream();
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(ms, currentRightsGroup.GroupRightsCollection);
            byte[] rigthsByteArray = new byte[(int)(ms.Length)];
            ms.Position = 0;
            ms.Read(rigthsByteArray, 0, (int)(ms.Length));
            ms.Close();

            // 拼接 SQL 命令
            string sqlTxt = "Update RightsGroup Set GroupName = @GroupName, GroupRightsList = @GroupRightsList Where Id = @Id";

            // 从配置文件读取连接字符串
            string connectionString = ConfigurationManager.ConnectionStrings["SQLSERVER"].ConnectionString;
            // 执行 SQL 命令
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
        /// 根据权限组名称校验权限组是否已经存在
        /// </summary>
        /// <param name="rightsGroupName">权限组名称</param>
        /// <returns>True:存在/False:不存在</returns>
        public bool CheckRightsGroupExist(string rightsGroupName)
        {
            //SQL命令
            string sqlTxt = string.Format("Select Count(*) From RightsGroup Where GroupName = '{0}'", rightsGroupName);

            //创建SQL执行对象
            DBUtility.AbstractDBProvider dbProvider = DBUtility.AbstractDBProvider.Instance();
            //执行查询操作
            int result = Convert.ToInt32(dbProvider.RunCommand(sqlTxt));

            if (result >= 1)
                return true;
            else
                return false;
        }

        #endregion
    }
}
