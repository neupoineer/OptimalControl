using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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
                        tmpVariable.Code = Convert.ToString(myReader["Code"]);
                        tmpVariable.Name = Convert.ToString(myReader["Name"]);
                        tmpVariable.Address = Convert.ToInt32(myReader["Address"]);
                        tmpVariable.Ratio = Math.Round(Convert.ToDouble(myReader["Ratio"]), 2);
                        tmpVariable.Limit = new Variable.VariableLimit()
                        {
                            HigherLimit = Convert.ToString(myReader["UpperLimit"]) != ""
                                ? Math.Round(Convert.ToDouble(myReader["UpperLimit"]), 2)
                                : -1,
                            LowerLimit = Convert.ToString(myReader["LowerLimit"]) != ""
                                ? Math.Round(Convert.ToDouble(myReader["LowerLimit"]), 2)
                                : -1,
                            UltimateHighLimit = Convert.ToString(myReader["UltimateUpperLimit"]) != ""
                                ? Math.Round(Convert.ToDouble(myReader["UltimateUpperLimit"]), 2)
                                : -1,
                            UltimateLowLimit = Convert.ToString(myReader["UltimateLowerLimit"]) != ""
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

                        tmpVariable.IsEnabled = Convert.ToBoolean(myReader["IsEnabled"]);
                        tmpVariable.IsRead = Convert.ToBoolean(myReader["IsRead"]);
                        tmpVariable.IsOutput = Convert.ToBoolean(myReader["IsOutput"]);
                        tmpVariable.IsDisplayed = Convert.ToBoolean(myReader["IsDisplayed"]);
                        tmpVariable.IsSaved = Convert.ToBoolean(myReader["IsSaved"]);

                        tmpVariable.IsFiltered = Convert.ToBoolean(myReader["IsFiltered"]);
                        tmpVariable.HistoryListLength = Convert.ToInt32(myReader["HistoryListLength"]);
                        tmpVariable.TrendLength = Convert.ToInt32(myReader["TrendLength"]);
                        tmpVariable.TrendInterval = Convert.ToInt32(myReader["TrendInterval"]);

                        tmpVariable.TrendHigherLimit = Convert.ToDouble(myReader["TrendHigherLimit"]);
                        tmpVariable.TrendLowerLimit = Convert.ToDouble(myReader["TrendLowerLimit"]);
                        tmpVariable.TrendListLength = Convert.ToInt32(myReader["TrendListLength"]);
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
        /// 根据变量编码获取变量实体
        /// </summary>
        /// <param name="code">变量编码</param>
        /// <returns>变量实体</returns>
        public Variable GetVariableInfoByCode(string code)
        {
            //SQL命令
            string sqltxt = string.Format("Select * From Variable Where Code = '{0}'", code);

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
                        tmpVariable.Code = Convert.ToString(myReader["Code"]);
                        tmpVariable.Name = Convert.ToString(myReader["Name"]);
                        tmpVariable.Address = Convert.ToInt32(myReader["Address"]);
                        tmpVariable.Ratio = Math.Round(Convert.ToDouble(myReader["Ratio"]), 2);
                        tmpVariable.Limit = new Variable.VariableLimit()
                        {
                            HigherLimit = Convert.ToString(myReader["UpperLimit"]) != ""
                                ? Math.Round(Convert.ToDouble(myReader["UpperLimit"]), 2)
                                : -1,
                            LowerLimit = Convert.ToString(myReader["LowerLimit"]) != ""
                                ? Math.Round(Convert.ToDouble(myReader["LowerLimit"]), 2)
                                : -1,
                            UltimateHighLimit = Convert.ToString(myReader["UltimateUpperLimit"]) != ""
                                ? Math.Round(Convert.ToDouble(myReader["UltimateUpperLimit"]), 2)
                                : -1,
                            UltimateLowLimit = Convert.ToString(myReader["UltimateLowerLimit"]) != ""
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

                        tmpVariable.IsEnabled = Convert.ToBoolean(myReader["IsEnabled"]);
                        tmpVariable.IsRead = Convert.ToBoolean(myReader["IsRead"]);
                        tmpVariable.IsOutput = Convert.ToBoolean(myReader["IsOutput"]);
                        tmpVariable.IsDisplayed = Convert.ToBoolean(myReader["IsDisplayed"]);
                        tmpVariable.IsSaved = Convert.ToBoolean(myReader["IsSaved"]);

                        tmpVariable.IsFiltered = Convert.ToBoolean(myReader["IsFiltered"]);
                        tmpVariable.HistoryListLength = Convert.ToInt32(myReader["HistoryListLength"]);
                        tmpVariable.TrendLength = Convert.ToInt32(myReader["TrendLength"]);
                        tmpVariable.TrendInterval = Convert.ToInt32(myReader["TrendInterval"]);

                        tmpVariable.TrendHigherLimit = Convert.ToDouble(myReader["TrendHigherLimit"]);
                        tmpVariable.TrendLowerLimit = Convert.ToDouble(myReader["TrendLowerLimit"]);
                        tmpVariable.TrendListLength = Convert.ToInt32(myReader["TrendListLength"]);
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
            const string sqlTxt = "INSERT INTO Variable (Code,Name,Address,Ratio,UpperLimit,LowerLimit,UltimateUpperLimit,UltimateLowerLimit,ControlPeriod,OperateDelay,DeviceID,IsEnabled,IsRead,IsOutput,IsDisplayed,IsFiltered,HistoryListLength,TrendLength,TrendInterval,TrendHigherLimit,TrendLowerLimit,TrendListLength) VALUES " +
                                  "(@Code,@Name,@Address,@Ratio,@UpperLimit,@LowerLimit,@UltimateUpperLimit,@UltimateLowerLimit,@ControlPeriod,@OperateDelay,@DeviceID,@IsEnabled,@IsRead,@IsOutput,@IsDisplayed,@IsFiltered,@HistoryListLength,@TrendLength,@TrendInterval,@TrendHigherLimit,@TrendLowerLimit,@TrendListLength)";
            // 从配置文件读取连接字符串
            string connectionString = ConfigurationManager.ConnectionStrings["SQLSERVER"].ConnectionString;
            // 执行 SQL 命令
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(sqlTxt, conn);
                SqlParameter prm0 = new SqlParameter("@Code", SqlDbType.NVarChar, 16) { Value = addVariable.Code };
                SqlParameter prm1 = new SqlParameter("@Name", SqlDbType.NVarChar, 50) { Value = addVariable.Name };
                SqlParameter prm2 = new SqlParameter("@Address", SqlDbType.Int) { Value = addVariable.Address };
                SqlParameter prm3 = new SqlParameter("@Ratio", SqlDbType.Real) { Value = addVariable.Ratio };
                SqlParameter prm4 = new SqlParameter("@UltimateUpperLimit", SqlDbType.Real) { Value = IsParameterNull(addVariable.Limit.UltimateHighLimit) };
                SqlParameter prm5 = new SqlParameter("@UpperLimit", SqlDbType.Real) { Value = IsParameterNull(addVariable.Limit.HigherLimit) };
                SqlParameter prm6 = new SqlParameter("@LowerLimit", SqlDbType.Real) { Value = IsParameterNull(addVariable.Limit.LowerLimit) };
                SqlParameter prm7 = new SqlParameter("@UltimateLowerLimit", SqlDbType.Real) { Value = IsParameterNull(addVariable.Limit.UltimateLowLimit) };
                SqlParameter prm8 = new SqlParameter("@ControlPeriod", SqlDbType.Int) { Value = IsParameterNull(addVariable.ControlPeriod) };
                SqlParameter prm9 = new SqlParameter("@OperateDelay", SqlDbType.Int) { Value = IsParameterNull(addVariable.OperateDelay) };
                SqlParameter prm10 = new SqlParameter("@DeviceID", SqlDbType.Int) { Value = addVariable.DeviceID };

                SqlParameter prm11 = new SqlParameter("@IsEnabled", SqlDbType.Bit) { Value = addVariable.IsEnabled };
                SqlParameter prm12 = new SqlParameter("@IsRead", SqlDbType.Bit) { Value = addVariable.IsRead };
                SqlParameter prm13 = new SqlParameter("@IsOutput", SqlDbType.Bit) { Value = addVariable.IsOutput };
                SqlParameter prm14 = new SqlParameter("@IsDisplayed", SqlDbType.Bit) { Value = addVariable.IsDisplayed };
                SqlParameter prm15 = new SqlParameter("@IsSaved", SqlDbType.Bit) { Value = addVariable.IsSaved };

                SqlParameter prm16 = new SqlParameter("@IsFiltered", SqlDbType.Bit) { Value = addVariable.IsFiltered };
                SqlParameter prm17 = new SqlParameter("@HistoryListLength", SqlDbType.Int) { Value = addVariable.HistoryListLength };
                SqlParameter prm18 = new SqlParameter("@TrendLength", SqlDbType.Int) { Value = addVariable.TrendLength };
                SqlParameter prm19 = new SqlParameter("@TrendInterval", SqlDbType.Int) { Value = addVariable.TrendInterval };
                SqlParameter prm20 = new SqlParameter("@TrendHigherLimit", SqlDbType.Real) { Value = addVariable.TrendHigherLimit };
                SqlParameter prm21 = new SqlParameter("@TrendLowerLimit", SqlDbType.Real) { Value = addVariable.TrendLowerLimit };
                SqlParameter prm22 = new SqlParameter("@TrendListLength", SqlDbType.Int) { Value = addVariable.TrendListLength };

                cmd.Parameters.AddRange(new SqlParameter[]
                {
                    prm0, prm1, prm2, prm3, prm4, prm5, prm6, prm7, prm8, prm9, prm10, prm11, prm12, prm13, prm14, prm15,
                    prm16, prm17, prm18, prm19, prm20, prm21, prm22
                });
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
        /// 按照设备号删除变量
        /// </summary>
        /// <param name="deviceID">要删除的变量的设备号</param>
        /// <returns>True:成功/False:失败</returns>
        public bool DeleteVariableByDeviceId(int deviceID)
        {
            // 删除单个信息 SQL 命令
            string sqlTxt = string.Format("Delete From Variable Where DeviceID = {0}", deviceID);
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
            const string sqlTxt = "UPDATE Variable SET Code=@Code,Name=@Name,Address=@Address,Ratio=@Ratio,UpperLimit=@UpperLimit,LowerLimit=@LowerLimit,UltimateUpperLimit=@UltimateUpperLimit,UltimateLowerLimit=@UltimateLowerLimit,ControlPeriod=@ControlPeriod,OperateDelay=@OperateDelay,DeviceID=@DeviceID,IsRead=@IsRead,IsEnabled=@IsEnabled,IsOutput=@IsOutput,IsDisplayed=@IsDisplayed,IsFiltered=@IsFiltered,HistoryListLength=@HistoryListLength,TrendLength=@TrendLength,TrendInterval=@TrendInterval,TrendHigherLimit=@TrendHigherLimit,TrendLowerLimit=@TrendLowerLimit,TrendListLength=@TrendListLength WHERE Id=@Id";

            // 从配置文件读取连接字符串
            string connectionString = ConfigurationManager.ConnectionStrings["SQLSERVER"].ConnectionString;
            // 执行 SQL 命令
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(sqlTxt, conn);
                SqlParameter prm0 = new SqlParameter("@Code", SqlDbType.NVarChar, 16) { Value = currentVariable.Code };
                SqlParameter prm1 = new SqlParameter("@Name", SqlDbType.NVarChar, 50) { Value = currentVariable.Name };
                SqlParameter prm2 = new SqlParameter("@Address", SqlDbType.Int) { Value = currentVariable.Address };
                SqlParameter prm3 = new SqlParameter("@Ratio", SqlDbType.Real) { Value = currentVariable.Ratio };
                SqlParameter prm4 = new SqlParameter("@UltimateUpperLimit", SqlDbType.Real) { Value = IsParameterNull(currentVariable.Limit.UltimateHighLimit) };
                SqlParameter prm5 = new SqlParameter("@UpperLimit", SqlDbType.Real) { Value = IsParameterNull(currentVariable.Limit.HigherLimit) };
                SqlParameter prm6 = new SqlParameter("@LowerLimit", SqlDbType.Real) { Value = IsParameterNull(currentVariable.Limit.LowerLimit) };
                SqlParameter prm7 = new SqlParameter("@UltimateLowerLimit", SqlDbType.Real) { Value = IsParameterNull(currentVariable.Limit.UltimateLowLimit) };
                SqlParameter prm8 = new SqlParameter("@ControlPeriod", SqlDbType.Int) { Value = IsParameterNull(currentVariable.ControlPeriod) };
                SqlParameter prm9 = new SqlParameter("@OperateDelay", SqlDbType.Int) { Value = IsParameterNull(currentVariable.OperateDelay) };
                SqlParameter prm10 = new SqlParameter("@DeviceID", SqlDbType.Int) { Value = currentVariable.DeviceID };

                SqlParameter prm11 = new SqlParameter("@IsEnabled", SqlDbType.Bit) { Value = currentVariable.IsEnabled };
                SqlParameter prm12 = new SqlParameter("@IsRead", SqlDbType.Bit) { Value = currentVariable.IsRead };
                SqlParameter prm13 = new SqlParameter("@IsOutput", SqlDbType.Bit) { Value = currentVariable.IsOutput };
                SqlParameter prm14 = new SqlParameter("@IsDisplayed", SqlDbType.Bit) { Value = currentVariable.IsDisplayed };
                SqlParameter prm15 = new SqlParameter("@IsSaved", SqlDbType.Bit) { Value = currentVariable.IsSaved };

                SqlParameter prm16 = new SqlParameter("@IsFiltered", SqlDbType.Bit) { Value = currentVariable.IsFiltered };
                SqlParameter prm17 = new SqlParameter("@HistoryListLength", SqlDbType.Int) { Value = currentVariable.HistoryListLength };
                SqlParameter prm18 = new SqlParameter("@TrendLength", SqlDbType.Int) { Value = currentVariable.TrendLength };
                SqlParameter prm19 = new SqlParameter("@TrendInterval", SqlDbType.Int) { Value = currentVariable.TrendInterval };
                SqlParameter prm20 = new SqlParameter("@TrendHigherLimit", SqlDbType.Real) { Value = currentVariable.TrendHigherLimit };
                SqlParameter prm21 = new SqlParameter("@TrendLowerLimit", SqlDbType.Real) { Value = currentVariable.TrendLowerLimit };
                SqlParameter prm22 = new SqlParameter("@TrendListLength", SqlDbType.Int) { Value = currentVariable.TrendListLength };

                SqlParameter prm23 = new SqlParameter("@Id", SqlDbType.Int) { Value = currentVariable.Id };

                cmd.Parameters.AddRange(new SqlParameter[]
                {
                    prm0, prm1, prm2, prm3, prm4, prm5, prm6, prm7, prm8, prm9, prm10, prm11, prm12, prm13, prm14, prm15,
                    prm16, prm17, prm18, prm19, prm20, prm21, prm22, prm23
                });
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
            const string sqltxt = "SELECT * FROM Variable";
            //创建变量实体集合
            List<Variable> VariableCollection = new List<Variable>();

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
                        tmpVariable.Code = Convert.ToString(myReader["Code"]);
                        tmpVariable.Name = Convert.ToString(myReader["Name"]);
                        tmpVariable.Address = Convert.ToInt32(myReader["Address"]);
                        tmpVariable.Ratio = Math.Round(Convert.ToDouble(myReader["Ratio"]), 2);
                        tmpVariable.Limit = new Variable.VariableLimit()
                        {
                            HigherLimit = Convert.ToString(myReader["UpperLimit"]) != ""
                                ? Math.Round(Convert.ToDouble(myReader["UpperLimit"]), 2)
                                : -1,
                            LowerLimit = Convert.ToString(myReader["LowerLimit"]) != ""
                                ? Math.Round(Convert.ToDouble(myReader["LowerLimit"]), 2)
                                : -1,
                            UltimateHighLimit = Convert.ToString(myReader["UltimateUpperLimit"]) != ""
                                ? Math.Round(Convert.ToDouble(myReader["UltimateUpperLimit"]), 2)
                                : -1,
                            UltimateLowLimit = Convert.ToString(myReader["UltimateLowerLimit"]) != ""
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

                        tmpVariable.IsEnabled = Convert.ToBoolean(myReader["IsEnabled"]);
                        tmpVariable.IsRead = Convert.ToBoolean(myReader["IsRead"]);
                        tmpVariable.IsOutput = Convert.ToBoolean(myReader["IsOutput"]);
                        tmpVariable.IsDisplayed = Convert.ToBoolean(myReader["IsDisplayed"]);
                        tmpVariable.IsSaved = Convert.ToBoolean(myReader["IsSaved"]);

                        tmpVariable.IsFiltered = Convert.ToBoolean(myReader["IsFiltered"]);
                        tmpVariable.HistoryListLength = Convert.ToInt32(myReader["HistoryListLength"]);
                        tmpVariable.TrendLength = Convert.ToInt32(myReader["TrendLength"]);
                        tmpVariable.TrendInterval = Convert.ToInt32(myReader["TrendInterval"]);

                        tmpVariable.TrendHigherLimit = Convert.ToDouble(myReader["TrendHigherLimit"]);
                        tmpVariable.TrendLowerLimit = Convert.ToDouble(myReader["TrendLowerLimit"]);
                        tmpVariable.TrendListLength = Convert.ToInt32(myReader["TrendListLength"]);

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
                        tmpVariable.Code = Convert.ToString(myReader["Code"]);
                        tmpVariable.Name = Convert.ToString(myReader["Name"]);
                        tmpVariable.Address = Convert.ToInt32(myReader["Address"]);
                        tmpVariable.Ratio = Math.Round(Convert.ToDouble(myReader["Ratio"]), 2);
                        tmpVariable.Limit = new Variable.VariableLimit()
                        {
                            HigherLimit = Convert.ToString(myReader["UpperLimit"]) != ""
                                ? Math.Round(Convert.ToDouble(myReader["UpperLimit"]), 2)
                                : -1,
                            LowerLimit = Convert.ToString(myReader["LowerLimit"]) != ""
                                ? Math.Round(Convert.ToDouble(myReader["LowerLimit"]), 2)
                                : -1,
                            UltimateHighLimit = Convert.ToString(myReader["UltimateUpperLimit"]) != ""
                                ? Math.Round(Convert.ToDouble(myReader["UltimateUpperLimit"]), 2)
                                : -1,
                            UltimateLowLimit = Convert.ToString(myReader["UltimateLowerLimit"]) != ""
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

                        tmpVariable.IsEnabled = Convert.ToBoolean(myReader["IsEnabled"]);
                        tmpVariable.IsRead = Convert.ToBoolean(myReader["IsRead"]);
                        tmpVariable.IsOutput = Convert.ToBoolean(myReader["IsOutput"]);
                        tmpVariable.IsDisplayed = Convert.ToBoolean(myReader["IsDisplayed"]);
                        tmpVariable.IsSaved = Convert.ToBoolean(myReader["IsSaved"]);

                        tmpVariable.IsFiltered = Convert.ToBoolean(myReader["IsFiltered"]);
                        tmpVariable.HistoryListLength = Convert.ToInt32(myReader["HistoryListLength"]);
                        tmpVariable.TrendLength = Convert.ToInt32(myReader["TrendLength"]);
                        tmpVariable.TrendInterval = Convert.ToInt32(myReader["TrendInterval"]);

                        tmpVariable.TrendHigherLimit = Convert.ToDouble(myReader["TrendHigherLimit"]);
                        tmpVariable.TrendLowerLimit = Convert.ToDouble(myReader["TrendLowerLimit"]);
                        tmpVariable.TrendListLength = Convert.ToInt32(myReader["TrendListLength"]);

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
        public bool CheckVariableExist(string code)
        {
            //创建查询信息的 SQL
            string sqlTxt = string.Format(
                "Select Count(*) From Variable Where Code = '{0}'", code);
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

        #region 私有成员
        /// <summary>
        /// Determines whether the specified parameter is null.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns>result(null for -1)</returns>
        private object IsParameterNull(object parameter)
        {
            if (Convert.ToDouble(parameter).Equals(-1))
                return DBNull.Value;
            else return parameter;
        }
        #endregion
    }
}
