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
    /// 数据访问操作类
    /// </summary>
    public class DataService : IDataService
    {
        #region IDataService 成员

        /// <summary>
        /// 根据变量编码和设备ID获取数据
        /// </summary>
        /// <param name="variableCode">变量编码</param>
        /// <param name="deviceID">设备ID</param>
        /// <param name="starTime">起始时间</param>
        /// <param name="endTime">截止时间</param>
        /// <returns>数据</returns>
        public List<Data> GetDataByVariableCode(string variableCode, int deviceID, DateTime starTime, DateTime endTime)
        {
            //SQL命令
            const string sqltxt = "SELECT TimeValue, Value FROM Data WHERE " +
                                  "VariableCode=@VariableCode AND DeviceID=@DeviceID AND TimeValue >= @StartTime AND TimeValue < @EndTime";
            //创建数据实体集合
            List<Data> dataCollection = new List<Data>();
            //定义数据实体

            // 从配置文件读取连接字符串
            string connectionString = ConfigurationManager.ConnectionStrings["SQLSERVER"].ConnectionString;
            // 执行 SQL 命令
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(sqltxt, conn);
                SqlParameter prm1 = new SqlParameter("@VariableCode", SqlDbType.NVarChar, 16) { Value = variableCode };
                SqlParameter prm2 = new SqlParameter("@DeviceID", SqlDbType.Int) {Value = deviceID};
                SqlParameter prm3 = new SqlParameter("@StartTime", SqlDbType.DateTime) {Value = starTime};
                SqlParameter prm4 = new SqlParameter("@EndTime", SqlDbType.DateTime) {Value = endTime};
                cmd.Parameters.AddRange(new SqlParameter[] {prm1, prm2, prm3, prm4});
                conn.Open();

                using (SqlDataReader myReader = cmd.ExecuteReader(
                    CommandBehavior.CloseConnection))
                {
                    while (myReader.Read())
                    {
                        // 创建数据实体
                        Data tmpData = new Data();
                        //将数据集转换成实体集合
                        tmpData.TimeValue = Convert.ToDateTime(myReader["TimeValue"]);
                        tmpData.Value = Convert.ToDouble(myReader["Value"]);
                        tmpData.State = (Variable.VariableState)(Convert.ToInt32(myReader["State"]));
                        tmpData.Trend = (Variable.VariableTrend)(Convert.ToInt32(myReader["Trend"]));
                        tmpData.TrendValue = Convert.ToDouble(myReader["TrendValue"]);
                        // 添加到数据实体集合
                        dataCollection.Add(tmpData);
                    }
                }
            }
            // 返回结果
            return dataCollection;
        }

        /// <summary>
        /// 根据变量编码和设备ID获取最后的数据
        /// </summary>
        /// <param name="variableCode">变量编码</param>
        /// <param name="deviceID">设备ID</param>
        /// <returns>数据</returns>
        public Data GetLastDataByVariableCode(string variableCode, int deviceID)
        {
            //SQL命令
            const string sqltxt = "SELECT TOP 1 TimeValue, Value FROM Data WHERE " +
                                  "VariableCode=@VariableCode AND DeviceID=@DeviceID ORDER BY id DESC";
            //创建数据实体集合
            Data data = new Data();
            //定义数据实体

            // 从配置文件读取连接字符串
            string connectionString = ConfigurationManager.ConnectionStrings["SQLSERVER"].ConnectionString;
            // 执行 SQL 命令
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(sqltxt, conn);
                SqlParameter prm1 = new SqlParameter("@VariableCode", SqlDbType.NVarChar, 16) { Value = variableCode };
                SqlParameter prm2 = new SqlParameter("@DeviceID", SqlDbType.Int) { Value = deviceID };
                cmd.Parameters.AddRange(new SqlParameter[] {prm1, prm2});
                conn.Open();

                using (SqlDataReader myReader = cmd.ExecuteReader(
                    CommandBehavior.CloseConnection))
                {
                    if (myReader.Read())
                    {
                        // 创建实体
                        data = new Data
                        {
                            TimeValue = Convert.ToDateTime(myReader["TimeValue"]),
                            Value = Convert.ToDouble(myReader["Value"]),
                            State = (Variable.VariableState) (Convert.ToInt32(myReader["State"])),
                            Trend = (Variable.VariableTrend) (Convert.ToInt32(myReader["Trend"])),
                            TrendValue = Convert.ToDouble(myReader["TrendValue"]),
                        };
                    }
                }
            }
            // 返回结果
            return data;
        }

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="dataCollection">要添加的数据实体</param>
        /// <returns>True:成功/False:失败</returns>
        public bool AddData(List<Data> dataCollection)
        {
            // 从配置文件读取连接字符串
            string connectionString = ConfigurationManager.ConnectionStrings["SQLSERVER"].ConnectionString;

            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            SqlTransaction tran = conn.BeginTransaction();

            try
            {
                foreach (Data data in dataCollection)
                {
                    const string sql = "INSERT INTO Data (VariableCode, TimeValue, Value, DeviceID, State, Trend, TrendValue) VALUES " +
                                       "(@VariableCode, @TimeValue, @Value, @DeviceID, @State, @Trend, @TrendValue)";

                    SqlCommand cmd = new SqlCommand(sql, conn, tran);

                    SqlParameter[] parameters = new SqlParameter[]
                    {
                        new SqlParameter("@VariableCode", data.VariableCode),
                        new SqlParameter("@TimeValue", data.TimeValue),
                        new SqlParameter("@Value", data.Value),
                        new SqlParameter("@DeviceID", data.DeviceID),
                        new SqlParameter("@State", data.State),
                        new SqlParameter("@Trend", data.Trend),
                        new SqlParameter("@TrendValue", data.TrendValue),
                    };

                    cmd.Parameters.AddRange(parameters);
                    cmd.ExecuteNonQuery();
                }
                tran.Commit();
                return true;
            }
            catch
            {
                tran.Rollback();
                return false;
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// 获得所选时间内的所有数据.
        /// </summary>
        /// <param name="starTime">起始时间</param>
        /// <param name="endTime">截止时间</param>
        /// <returns>数据表DataTable</returns>
        public DataTable GetAllDataInfoByTime(DateTime starTime, DateTime endTime)
        {
            //SQL命令
            string sqltxt =
                "DECLARE @sql1 varchar(8000);" +
                "SELECT @sql1 = ISNULL(@sql1 + '],[' , '') + [VariableCode] FROM [Curve] GROUP BY [VariableCode]; SET @sql1 = '[' + @sql1 + ']';" +
                "DECLARE @sql2 varchar(8000);" +
                "SELECT @sql2 = ISNULL(@sql2 + ''',MAX([' , '') + [VariableCode] +']) AS ''' + [VariableCode]  FROM [Curve] GROUP BY [VariableCode];" +
                "SET @sql2 = 'MAX([' + @sql2 + '''';" +
                "EXEC ('SELECT [TimeValue] AS ''时间'',' + @sql2 + ' FROM (SELECT * FROM [Data] WHERE [TimeValue] >= ''@StartTime'' AND [TimeValue] < ''@EndTime'') " +
                "AS a PIVOT (MAX([Value]) FOR [VariableCode] IN (' + @sql1 + ')) b GROUP BY [TimeValue] ORDER BY [TimeValue]');";
            sqltxt = sqltxt.Replace("@StartTime", starTime.ToString("yyyy-MM-dd HH:mm:ss.fff"))
                .Replace("@EndTime", endTime.ToString("yyyy-MM-dd HH:mm:ss.fff"));

            DataSet dataset = new DataSet();

            // 从配置文件读取连接字符串
            string connectionString = ConfigurationManager.ConnectionStrings["SQLSERVER"].ConnectionString;

            try
            {
                SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sqltxt;
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
        
        #endregion
    }
}
