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
    /// 操作员数据访问操作类
    /// </summary>
    public class OperatorService : IDAL.IOperatorService
    {

        #region IOperatorService 成员

        /// <summary>
        /// 根据操作员名称和密码获取操作员实体
        /// </summary>
        /// <param name="name">操作员名称</param>
        /// <param name="pwd">操作员密码</param>
        /// <returns>操作员实体</returns>
        public Model.Operator GetOperatorInfoByName(string name, string pwd)
        {
            //SQL命令
            string sqltxt = string.Format("Select Id, OperatorName, Password, RightsList, State From Operator Where OperatorName = '{0}' And Password = '{1}'", name, pwd);

            //创建操作员实体
            Model.Operator tmpOperator = new Model.Operator();

            // 转换数据库存储的 二进制数据为 Byte[] 数组 以便进而转换为操作员权限集合
            // 从配置文件读取连接字符串
            string connectionString = ConfigurationManager.ConnectionStrings["SQLSERVER"].ConnectionString;

            // 执行 SQL 命令
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
                        //将数据集转换成实体集合
                        tmpOperator.Id = Convert.ToInt32(myReader["Id"]);
                        tmpOperator.ModelName = Convert.ToString(myReader["OperatorName"]);
                        tmpOperator.Password = Convert.ToString(myReader["Password"]);
                        tmpOperator.State = Convert.ToBoolean(myReader["State"]);

                        // 读取权限集合
                        System.Data.SqlTypes.SqlBytes bytes = myReader.GetSqlBytes(3); // 只能指定列序号
                        // 将流反序列化为权限集合对象
                        BinaryFormatter bf = new BinaryFormatter();
                        if (!bytes.IsNull)
                            tmpOperator.RightsCollection = (bf.Deserialize(bytes.Stream) as Dictionary<string, Model.Rights>);
                        //else
                        //    throw new Exception(string.Format("操作员 [{0}] 没有任何权限，禁止登录！", tmpOperator.ModelName));
                    }
                    else
                        //如果没有读取到内容则抛出异常
                        throw new Exception("登录名称或用户密码不正确！");
                }
            }
                
            // 如果操作员已经被禁用
            if (!tmpOperator.State)
                throw new Exception(string.Format("操作员 [{0}] 已被禁用，请与管理员联系！", tmpOperator.ModelName));
            // 返回结果
            return tmpOperator;
        }

        /// <summary>
        /// 添加操作员
        /// </summary>
        /// <param name="addOperator">要添加的操作员实体</param>
        /// <returns>True:成功/False:失败</returns>
        public bool AddOperator(Model.Operator addOperator)
        {
            // 验证密码长度
            if (addOperator.Password.Trim().Length < 6)
                throw new Exception("用户密码长度不能小于六位！");
            // 转换操作员权限集合为数据库可存取的 Byte[] 数组
            MemoryStream ms = new MemoryStream();
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(ms, addOperator.RightsCollection);
            byte[] rigthsByteArray = new byte[(int)(ms.Length)];
            ms.Position = 0;
            ms.Read(rigthsByteArray, 0, (int)(ms.Length));
            ms.Close();

            // 拼接 SQL 命令
            string sqlTxt = "Insert Into Operator (OperatorName, Password, RightsList, State) Values " +
                "(@OperatorName, @Password, @RightsList, @State)";

            // 从配置文件读取连接字符串
            string connectionString = ConfigurationManager.ConnectionStrings["SQLSERVER"].ConnectionString;
            // 执行 SQL 命令
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
        /// 删除操作员
        /// </summary>
        /// <param name="id">要删除的操作员 ID</param>
        /// <returns>True:成功/False:失败</returns>
        public bool DeleteOperatorByID(int id)
        {
            // 删除单个信息 SQL 命令
            string sqlTxt = string.Format("Delete From Operator Where Id = {0}", id);
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
        /// 修改操作员
        /// </summary>
        /// <param name="currentOperator">要修改的操作员实体</param>
        /// <returns>True:成功/False:失败</returns>
        public bool ModifyOperator(Model.Operator currentOperator)
        {
            // 验证密码长度
            if (currentOperator.Password.Trim().Length < 6)
                throw new Exception("用户密码长度不能小于六位！");
            // 转换操作员权限集合为数据库可存取的 Byte[] 数组
            MemoryStream ms = new MemoryStream();
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(ms, currentOperator.RightsCollection);
            byte[] rigthsByteArray = new byte[(int)(ms.Length)];
            ms.Position = 0;
            ms.Read(rigthsByteArray, 0, (int)(ms.Length));
            ms.Close();

            // 拼接 SQL 命令
            string sqlTxt = "Update Operator Set OperatorName = @OperatorName, " +
                "Password = @Password, RightsList = @RightsList, State = @State Where Id = @Id";

            // 从配置文件读取连接字符串
            string connectionString = ConfigurationManager.ConnectionStrings["SQLSERVER"].ConnectionString;
            // 执行 SQL 命令
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
        /// 获取所有操作员信息
        /// </summary>
        /// <returns>操作员实体集合</returns>
        public Dictionary<string, Model.Operator> GetAllOperatorInfo()
        {
            //SQL命令
            string sqltxt = "Select Id, OperatorName, Password, RightsList, State From Operator";
            //创建操作员实体集合
            Dictionary<string, Model.Operator> operatorCollection = new Dictionary<string, Model.Operator>();
            //定义操作员实体
            Model.Operator tmpOperator = null;

            // 转换数据库存储的 二进制数据为 Byte[] 数组 以便进而转换为操作员权限集合
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
                        // 创建操作员实体
                        tmpOperator = new Model.Operator();
                        //将数据集转换成实体集合
                        tmpOperator.Id = Convert.ToInt32(myReader["Id"]);
                        tmpOperator.ModelName = Convert.ToString(myReader["OperatorName"]);
                        tmpOperator.Password = Convert.ToString(myReader["Password"]);
                        tmpOperator.State = Convert.ToBoolean(myReader["State"]);

                        // 读取权限集合
                        System.Data.SqlTypes.SqlBytes bytes = myReader.GetSqlBytes(3); // 只能指定列序号
                        // 将流反序列化为权限集合对象
                        BinaryFormatter bf = new BinaryFormatter();
                        if (!bytes.IsNull)
                            tmpOperator.RightsCollection = (bf.Deserialize(bytes.Stream) as Dictionary<string, Model.Rights>);

                        // 添加到操作员实体集合
                        operatorCollection.Add(tmpOperator.ModelName, tmpOperator);
                    }
                }
            }

            // 返回结果
            return operatorCollection;
        }

        /// <summary>
        /// 根据操作员名称校验操作员是否存在
        /// </summary>
        /// <param name="operatorName">操作员名称</param>
        /// <returns>True:存在/Flase:不存在</returns>
        public bool CheckOperatorExist(string operatorName)
        {
            //创建查询信息的 SQL
            string sqlTxt = string.Format(
                "Select Count(*) From Operator Where OperatorName = '{0}'", 
                operatorName);
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
