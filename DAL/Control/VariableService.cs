﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using IDAL.Control;
using Model.Control;

namespace DAL.Control
{
    /// <summary>
    /// 变量数据访问操作类
    /// </summary>
    public class VariableService : IVariableService
    {

        #region IVariableService 成员

        /// <summary>
        /// 根据变量ID获取变量实体
        /// </summary>
        /// <param name="id">变量ID</param>
        /// <returns>变量实体</returns>
        public Variable GetVariableInfoById(int id)
        {
            //SQL命令
            string sqltxt = string.Format("Select * From Variable Where Id = '{0}'", id);

            //创建变量实体
            Variable tmpVariable = new Variable();

            // 转换数据库存储的 二进制数据为 Byte[] 数组 以便进而转换为变量权限集合
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
                        tmpVariable.Id = Convert.ToInt32(myReader["Id"]);
                        tmpVariable.Name = Convert.ToString(myReader["Name"]);
                        tmpVariable.Address = Convert.ToInt32(myReader["Address"]);
                        tmpVariable.Ratio = Math.Round(Convert.ToDouble(myReader["Ratio"]), 2);
                        tmpVariable.Limit = new Variable.VariableLimit()
                        {
                            UpperLimit = Convert.ToString(myReader["UpperLimit"]) != ""
                                ? Math.Round(Convert.ToDouble(myReader["UpperLimit"]), 2)
                                : -1,
                            LowerLimit = Convert.ToString(myReader["LowerLimit"]) != ""
                                ? Math.Round(Convert.ToDouble(myReader["LowerLimit"]), 2)
                                : -1,
                            UltimateUpperLimit = Convert.ToString(myReader["UltimateUpperLimit"]) != ""
                                ? Math.Round(Convert.ToDouble(myReader["UltimateUpperLimit"]), 2)
                                : -1,
                            UltimateLowerLimit = Convert.ToString(myReader["UltimateLowerLimit"]) != ""
                                ? Math.Round(Convert.ToDouble(myReader["UltimateLowerLimit"]), 2)
                                : -1,
                        };
                        tmpVariable.ControlPeriod = Convert.ToString(myReader["ControlPeriod"]) != ""
                            ? Convert.ToInt32(myReader["ControlPeriod"])
                            : -1;
                        tmpVariable.OperateDelay = Convert.ToString(myReader["OperateDelay"]) != ""
                            ? Convert.ToInt32(myReader["OperateDelay"])
                            : -1;
                        tmpVariable.DeviceID = Convert.ToUInt32(myReader["DeviceID"]);
                    }
                    else
                        //如果没有读取到内容则抛出异常
                        throw new Exception("变量ID错误！");
                }
            }
            // 返回结果
            return tmpVariable;
        }

        /// <summary>
        /// 根据变量名获取变量实体
        /// </summary>
        /// <param name="name">变量名</param>
        /// <returns>变量实体</returns>
        public Variable GetVariableInfoByName(string name)
        {
            //SQL命令
            string sqltxt = string.Format("Select * From Variable Where Name = '{0}'", name);

            //创建变量实体
            Variable tmpVariable = new Variable();

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
                        tmpVariable.Id = Convert.ToInt32(myReader["Id"]);
                        tmpVariable.Name = Convert.ToString(myReader["Name"]);
                        tmpVariable.Address = Convert.ToInt32(myReader["Address"]);
                        tmpVariable.Ratio = Math.Round(Convert.ToDouble(myReader["Ratio"]), 2);
                        tmpVariable.Limit = new Variable.VariableLimit()
                        {
                            UpperLimit = Convert.ToString(myReader["UpperLimit"]) != ""
                                ? Math.Round(Convert.ToDouble(myReader["UpperLimit"]), 2)
                                : -1,
                            LowerLimit = Convert.ToString(myReader["LowerLimit"]) != ""
                                ? Math.Round(Convert.ToDouble(myReader["LowerLimit"]), 2)
                                : -1,
                            UltimateUpperLimit = Convert.ToString(myReader["UltimateUpperLimit"]) != ""
                                ? Math.Round(Convert.ToDouble(myReader["UltimateUpperLimit"]), 2)
                                : -1,
                            UltimateLowerLimit = Convert.ToString(myReader["UltimateLowerLimit"]) != ""
                                ? Math.Round(Convert.ToDouble(myReader["UltimateLowerLimit"]), 2)
                                : -1,
                        };
                        tmpVariable.ControlPeriod = Convert.ToString(myReader["ControlPeriod"]) != ""
                            ? Convert.ToInt32(myReader["ControlPeriod"])
                            : -1;
                        tmpVariable.OperateDelay = Convert.ToString(myReader["OperateDelay"]) != ""
                            ? Convert.ToInt32(myReader["OperateDelay"])
                            : -1;
                        tmpVariable.DeviceID = Convert.ToUInt32(myReader["DeviceID"]);
                    }
                    else
                        //如果没有读取到内容则抛出异常
                        throw new Exception("变量名错误！");
                }
            }
            // 返回结果
            return tmpVariable;
        }

        /// <summary>
        /// 添加变量
        /// </summary>
        /// <param name="addVariable">要添加的变量实体</param>
        /// <returns>True:成功/False:失败</returns>
        public bool AddVariable(Variable addVariable)
        {
            // 拼接 SQL 命令
            string sqlTxt =
                "INSERT INTO Variable (Name,Address,Ratio,UpperLimit,LowerLimit,UltimateUpperLimit,UltimateLowerLimit,ControlPeriod,OperateDelay,DeviceID) VALUES " +
                "(@Name,@Address,@Ratio,@UpperLimit,@LowerLimit,@UltimateUpperLimit,@UltimateLowerLimit,@ControlPeriod,@OperateDelay,@DeviceID)";
            // 从配置文件读取连接字符串
            string connectionString = ConfigurationManager.ConnectionStrings["SQLSERVER"].ConnectionString;
            // 执行 SQL 命令
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(sqlTxt, conn);
                SqlParameter prm1 = new SqlParameter("@Name", SqlDbType.NVarChar, 50) { Value = addVariable.Name };
                SqlParameter prm2 = new SqlParameter("@Address", SqlDbType.Int) { Value = addVariable.Address };
                SqlParameter prm3 = new SqlParameter("@Ratio", SqlDbType.Real) { Value = addVariable.Ratio };
                SqlParameter prm4 = new SqlParameter("@UltimateUpperLimit", SqlDbType.Real) { Value = addVariable.Limit.UltimateUpperLimit };
                SqlParameter prm5 = new SqlParameter("@UpperLimit", SqlDbType.Real) { Value = addVariable.Limit.UpperLimit };
                SqlParameter prm6 = new SqlParameter("@LowerLimit", SqlDbType.Real) { Value = addVariable.Limit.LowerLimit };
                SqlParameter prm7 = new SqlParameter("@UltimateLowerLimit", SqlDbType.Real) { Value = addVariable.Limit.UltimateLowerLimit };
                SqlParameter prm8 = new SqlParameter("@ControlPeriod", SqlDbType.Int) { Value = addVariable.ControlPeriod };
                SqlParameter prm9 = new SqlParameter("@OperateDelay", SqlDbType.Int) { Value = addVariable.OperateDelay };
                SqlParameter prm10 = new SqlParameter("@DeviceID", SqlDbType.Int) { Value = addVariable.DeviceID };

                cmd.Parameters.AddRange(new SqlParameter[] { prm1, prm2, prm3, prm4, prm5, prm6, prm7, prm8, prm9, prm10 });
                conn.Open();

                if (cmd.ExecuteNonQuery() >= 1)
                    return true;
                else
                    return false;
            }
        }

        /// <summary>
        /// 删除变量
        /// </summary>
        /// <param name="id">要删除的变量 ID</param>
        /// <returns>True:成功/False:失败</returns>
        public bool DeleteVariableById(int id)
        {
            // 删除单个信息 SQL 命令
            string sqlTxt = string.Format("Delete From Variable Where Id = {0}", id);
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
        /// 修改变量
        /// </summary>
        /// <param name="currentVariable">要修改的变量实体</param>
        /// <returns>True:成功/False:失败</returns>
        public bool ModifyVariable(Variable currentVariable)
        {
            // 拼接 SQL 命令
            string sqlTxt =
                "UPDATE Variable SET Name=@Name,Address=@Address,Ratio=@Ratio,UpperLimit=@UpperLimit,LowerLimit=@LowerLimit,UltimateUpperLimit=@UltimateUpperLimit,UltimateLowerLimit=@UltimateLowerLimit,ControlPeriod=@ControlPeriod,OperateDelay=@OperateDelay,DeviceID=@DeviceID WHERE Id=@Id";

            // 从配置文件读取连接字符串
            string connectionString = ConfigurationManager.ConnectionStrings["SQLSERVER"].ConnectionString;
            // 执行 SQL 命令
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(sqlTxt, conn);
                SqlParameter prm1 = new SqlParameter("@Name", SqlDbType.NVarChar, 50) { Value = currentVariable.Name };
                SqlParameter prm2 = new SqlParameter("@Address", SqlDbType.Int) { Value = currentVariable.Address };
                SqlParameter prm3 = new SqlParameter("@Ratio", SqlDbType.Real) { Value = currentVariable.Ratio };
                SqlParameter prm4 = new SqlParameter("@UltimateUpperLimit", SqlDbType.Real) { Value = currentVariable.Limit.UltimateUpperLimit };
                SqlParameter prm5 = new SqlParameter("@UpperLimit", SqlDbType.Real) { Value = currentVariable.Limit.UpperLimit };
                SqlParameter prm6 = new SqlParameter("@LowerLimit", SqlDbType.Real) { Value = currentVariable.Limit.LowerLimit };
                SqlParameter prm7 = new SqlParameter("@UltimateLowerLimit", SqlDbType.Real) { Value = currentVariable.Limit.UltimateLowerLimit };
                SqlParameter prm8 = new SqlParameter("@ControlPeriod", SqlDbType.Int) { Value = currentVariable.ControlPeriod };
                SqlParameter prm9 = new SqlParameter("@OperateDelay", SqlDbType.Int) { Value = currentVariable.OperateDelay };
                SqlParameter prm10 = new SqlParameter("@DeviceID", SqlDbType.Int) { Value = currentVariable.DeviceID };
                SqlParameter prm11 = new SqlParameter("@Id", SqlDbType.Int) { Value = currentVariable.Id };

                cmd.Parameters.AddRange(new SqlParameter[]
                {prm1, prm2, prm3, prm4, prm5, prm6, prm7, prm8, prm9, prm10, prm11});
                conn.Open();

                if (cmd.ExecuteNonQuery() >= 1)
                    return true;
                else
                    return false;
            }
        }

        /// <summary>
        /// 获取所有变量信息
        /// </summary>
        /// <returns>变量实体集合</returns>
        public List<Variable> GetAllVariableInfo()
        {
            //SQL命令
            string sqltxt = "SELECT * FROM Variable";
            //创建变量实体集合
            List<Variable> VariableCollection = new List<Variable>();
            //定义变量实体

            // 转换数据库存储的 二进制数据为 Byte[] 数组 以便进而转换为变量权限集合
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
                        // 创建变量实体
                        Variable tmpVariable = new Variable();
                        //将数据集转换成实体集合
                        tmpVariable.Id = Convert.ToInt32(myReader["Id"]);
                        tmpVariable.Name = Convert.ToString(myReader["Name"]);
                        tmpVariable.Address = Convert.ToInt32(myReader["Address"]);
                        tmpVariable.Ratio = Math.Round(Convert.ToDouble(myReader["Ratio"]), 2);
                        tmpVariable.Limit = new Variable.VariableLimit()
                        {
                            UpperLimit = Convert.ToString(myReader["UpperLimit"]) != ""
                                ? Math.Round(Convert.ToDouble(myReader["UpperLimit"]), 2)
                                : -1,
                            LowerLimit = Convert.ToString(myReader["LowerLimit"]) != ""
                                ? Math.Round(Convert.ToDouble(myReader["LowerLimit"]), 2)
                                : -1,
                            UltimateUpperLimit = Convert.ToString(myReader["UltimateUpperLimit"]) != ""
                                ? Math.Round(Convert.ToDouble(myReader["UltimateUpperLimit"]), 2)
                                : -1,
                            UltimateLowerLimit = Convert.ToString(myReader["UltimateLowerLimit"]) != ""
                                ? Math.Round(Convert.ToDouble(myReader["UltimateLowerLimit"]), 2)
                                : -1,
                        };
                        tmpVariable.ControlPeriod = Convert.ToString(myReader["ControlPeriod"]) != ""
                            ? Convert.ToInt32(myReader["ControlPeriod"])
                            : -1;
                        tmpVariable.OperateDelay = Convert.ToString(myReader["OperateDelay"]) != ""
                            ? Convert.ToInt32(myReader["OperateDelay"])
                            : -1;
                        tmpVariable.DeviceID = Convert.ToUInt32(myReader["DeviceID"]);
                        // 添加到变量实体集合
                        VariableCollection.Add(tmpVariable);
                    }
                }
            }

            // 返回结果
            return VariableCollection;
        }

        /// <summary>
        /// 获取Device的所有变量信息
        /// </summary>
        /// <returns>变量实体集合</returns>
        public List<Variable> GetVariableByDeviceId(int deviceId)
        {
            //SQL命令
            string sqltxt = string.Format("Select * From Variable Where DeviceID = '{0}'", deviceId);

            //创建变量实体集合
            List<Variable> variableCollection = new List<Variable>();
            //定义变量实体

            // 转换数据库存储的 二进制数据为 Byte[] 数组 以便进而转换为变量权限集合
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
                        // 创建变量实体
                        Variable tmpVariable = new Variable();
                        //将数据集转换成实体集合
                        tmpVariable.Id = Convert.ToInt32(myReader["Id"]);
                        tmpVariable.Name = Convert.ToString(myReader["Name"]);
                        tmpVariable.Address = Convert.ToInt32(myReader["Address"]);
                        tmpVariable.Ratio = Math.Round(Convert.ToDouble(myReader["Ratio"]), 2);
                        tmpVariable.Limit = new Variable.VariableLimit()
                        {
                            UpperLimit = Convert.ToString(myReader["UpperLimit"]) != ""
                                ? Math.Round(Convert.ToDouble(myReader["UpperLimit"]), 2)
                                : -1,
                            LowerLimit = Convert.ToString(myReader["LowerLimit"]) != ""
                                ? Math.Round(Convert.ToDouble(myReader["LowerLimit"]), 2)
                                : -1,
                            UltimateUpperLimit = Convert.ToString(myReader["UltimateUpperLimit"]) != ""
                                ? Math.Round(Convert.ToDouble(myReader["UltimateUpperLimit"]), 2)
                                : -1,
                            UltimateLowerLimit = Convert.ToString(myReader["UltimateLowerLimit"]) != ""
                                ? Math.Round(Convert.ToDouble(myReader["UltimateLowerLimit"]), 2)
                                : -1,
                        };
                        tmpVariable.ControlPeriod = Convert.ToString(myReader["ControlPeriod"]) != ""
                            ? Convert.ToInt32(myReader["ControlPeriod"])
                            : -1;
                        tmpVariable.OperateDelay = Convert.ToString(myReader["OperateDelay"]) != ""
                            ? Convert.ToInt32(myReader["OperateDelay"])
                            : -1;
                        tmpVariable.DeviceID = Convert.ToUInt32(myReader["DeviceID"]);
                        // 添加到变量实体集合
                        variableCollection.Add(tmpVariable);
                    }
                }
            }
            // 返回结果
            return variableCollection;
        }


        /// <summary>
        /// 根据变量名称校验变量是否存在
        /// </summary>
        /// <param name="variableName">变量名称</param>
        /// <returns>True:存在/Flase:不存在</returns>
        public bool CheckVariableExist(string variableName)
        {
            //创建查询信息的 SQL
            string sqlTxt = string.Format(
                "Select Count(*) From Variable Where Name = '{0}'", variableName);
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
