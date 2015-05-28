using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using IDAL.Control;
using Model.Control;

namespace DAL.Control
{
    public class DataService : IDataService
    {
        /// <summary>
        /// 根据变量名和设备ID获取数据
        /// </summary>
        /// <param name="variableCode">变量名</param>
        /// <param name="deviceID">设备ID</param>
        /// <param name="starTime">起始时间</param>
        /// <param name="endTime">截止时间</param>
        /// <returns>
        /// 数据
        /// </returns>
        public List<Data> GetDataByVariableCode(string variableCode, int deviceID, DateTime starTime, DateTime endTime)
        {
            //SQL命令
            string sqltxt =
                "SELECT TimeValue, Value FROM Data WHERE " +
                "ParameterCode=@ParameterCode AND DeviceID=@DeviceID AND TimeValue >= @StartTime AND TimeValue < @EndTime";
            //创建曲线实体集合
            List<Data> dataCollection = new List<Data>();
            //定义曲线实体

            // 从配置文件读取连接字符串
            string connectionString = ConfigurationManager.ConnectionStrings["SQLSERVER"].ConnectionString;
            // 执行 SQL 命令
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(sqltxt, conn);
                SqlParameter prm1 = new SqlParameter("@ParameterName", SqlDbType.NVarChar, 50) {Value = variableCode};
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
                        // 创建曲线实体
                        Data tmpData = new Data();
                        //将数据集转换成实体集合
                        tmpData.TimeValue = Convert.ToDateTime(myReader["TimeValue"]);
                        tmpData.Value = Convert.ToDouble(myReader["Value"]);

                        // 添加到曲线实体集合
                        dataCollection.Add(tmpData);
                    }
                }
            }
            // 返回结果
            return dataCollection;
        }

        /// <summary>
        /// Gets the last data by variable name.
        /// </summary>
        /// <param name="variableName">Name of the variable.</param>
        /// <param name="deviceID">The device identifier.</param>
        /// <returns></returns>
        public Data GetLastDataByVariableCode(string variableName, int deviceID)
        {
            //SQL命令
            string sqltxt =
                "SELECT TOP 1 TimeValue, Value FROM Data WHERE " +
                "ParameterName=@ParameterName AND DeviceID=@DeviceID ORDER BY id DESC";
            //创建曲线实体集合
            Data data = new Data();
            //定义曲线实体

            // 从配置文件读取连接字符串
            string connectionString = ConfigurationManager.ConnectionStrings["SQLSERVER"].ConnectionString;
            // 执行 SQL 命令
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(sqltxt, conn);
                SqlParameter prm1 = new SqlParameter("@ParameterName", SqlDbType.NVarChar, 50) { Value = variableName };
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
                            Value = Convert.ToDouble(myReader["Value"])
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
                    string sql = "INSERT INTO Data (ParameterName, TimeValue, Value, DeviceID) VALUES " +
                                 "(@ParameterName, @TimeValue, @Value, @DeviceID)";

                    SqlCommand cmd = new SqlCommand(sql, conn, tran);

                    SqlParameter[] parameters = new SqlParameter[]
                    {
                        new SqlParameter("@ParameterName", data.ParameterCode),
                        new SqlParameter("@TimeValue", data.TimeValue),
                        new SqlParameter("@Value", data.Value),
                        new SqlParameter("@DeviceID", data.DeviceID),
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
                "SELECT @sql1 = ISNULL(@sql1 + '],[' , '') + [Name] FROM [Curve] GROUP BY [Name]; SET @sql1 = '[' + @sql1 + ']';" +
                "DECLARE @sql2 varchar(8000);" +
                "SELECT @sql2 = ISNULL(@sql2 + ''',MAX([' , '') + [Name] +']) AS ''' + [Name]  FROM [Curve] GROUP BY [Name];" +
                "SET @sql2 = 'MAX([' + @sql2 + '''';" +
                "EXEC ('SELECT [TimeValue] AS ''时间'',' + @sql2 + ' FROM (SELECT * FROM [Data] WHERE [TimeValue] >= ''@StartTime'' AND [TimeValue] < ''@EndTime'') " +
                "AS a PIVOT (MAX([Value]) FOR [ParameterName] IN (' + @sql1 + ')) b GROUP BY [TimeValue] ORDER BY [TimeValue]');";
            sqltxt = sqltxt.Replace("@StartTime", starTime.ToString("yyyy-MM-dd HH:mm:ss.fff"))
                .Replace("@EndTime", endTime.ToString("yyyy-MM-dd HH:mm:ss.fff"));
            //创建曲线实体集合
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


    }
}
