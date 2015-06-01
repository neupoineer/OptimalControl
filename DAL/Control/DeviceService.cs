using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using IDAL.Control;
using Model.Modbus;

namespace DAL.Control
{
    /// <summary>
    /// 设备数据访问操作类
    /// </summary>
    public class DeviceService : IDeviceService
    {
        #region IDeviceService 成员

        /// <summary>
        /// 根据设备ID获取设备实体
        /// </summary>
        /// <param name="id">设备ID</param>
        /// <returns>设备实体</returns>
        public Device GetDeviceInfoById(int id)
        {
            //SQL命令
            string sqltxt = string.Format("Select * From Device Where Id = '{0}'", id);

            //创建设备实体
            Device tmpDevice = new Device();

            // 转换数据库存储的 二进制数据为 Byte[] 数组 以便进而转换为设备权限集合
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
                        tmpDevice.Id = Convert.ToInt32(myReader["Id"]);
                        tmpDevice.Name = Convert.ToString(myReader["Name"]);
                        tmpDevice.State = Convert.ToBoolean(myReader["State"]);
                        tmpDevice.SyncState = Convert.ToBoolean(myReader["SyncState"]);
                        tmpDevice.ModbusTcpDevice = new ModbusTcpDevice()
                        {
                            IP = Convert.ToString(myReader["Ip"]),
                            Port = Convert.ToInt32(myReader["Port"]),
                            UnitID = Convert.ToByte(myReader["UnitID"]),
                        };
                    }
                    else
                        //如果没有读取到内容则抛出异常
                        throw new Exception("设备ID错误！");
                }
            }
            // 返回结果
            return tmpDevice;
        }

        /// <summary>
        /// 添加设备
        /// </summary>
        /// <param name="addDevice">要添加的设备实体</param>
        /// <returns>True:成功/False:失败</returns>
        public bool AddDevice(Device addDevice)
        {
            // 拼接 SQL 命令
            const string sqlTxt = "INSERT INTO Device (Name,State,SyncState,IP,Port,UnitID) VALUES "+
                                  "(@Name,@State,@SyncState,@IP,@Port,@UnitID)";
            // 从配置文件读取连接字符串
            string connectionString = ConfigurationManager.ConnectionStrings["SQLSERVER"].ConnectionString;
            // 执行 SQL 命令
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(sqlTxt, conn);
                SqlParameter prm1 = new SqlParameter("@Name", SqlDbType.NVarChar, 50) { Value = addDevice.Name };
                SqlParameter prm2 = new SqlParameter("@State", SqlDbType.Bit) { Value = addDevice.State };
                SqlParameter prm3 = new SqlParameter("@SyncState", SqlDbType.Bit) { Value = addDevice.SyncState };
                SqlParameter prm4 = new SqlParameter("@IP", SqlDbType.NVarChar, 15) { Value = addDevice.ModbusTcpDevice.IP };
                SqlParameter prm5 = new SqlParameter("@Port", SqlDbType.Int) { Value = addDevice.ModbusTcpDevice.Port };
                SqlParameter prm6 = new SqlParameter("@UnitID", SqlDbType.TinyInt) { Value = addDevice.ModbusTcpDevice.UnitID };

                cmd.Parameters.AddRange(new SqlParameter[] { prm1, prm2, prm3, prm4, prm5, prm6 });
                conn.Open();

                if (cmd.ExecuteNonQuery() >= 1)
                    return true;
                else
                    return false;
            }
        }

        /// <summary>
        /// 删除设备
        /// </summary>
        /// <param name="id">要删除的设备 ID</param>
        /// <returns>True:成功/False:失败</returns>
        public bool DeleteDeviceById(int id)
        {
            // 删除单个信息 SQL 命令
            string sqlTxt = string.Format("Delete From Device Where Id = {0}", id);
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
        /// 修改设备
        /// </summary>
        /// <param name="currentDevice">要修改的设备实体</param>
        /// <returns>True:成功/False:失败</returns>
        public bool ModifyDevice(Device currentDevice)
        {
            // 拼接 SQL 命令
            const string sqlTxt = "UPDATE Device SET Name=@Name,State=@State,SyncState=@SyncState,IP=@IP,Port=@Port,UnitID=@UnitID WHERE Id=@Id";

            // 从配置文件读取连接字符串
            string connectionString = ConfigurationManager.ConnectionStrings["SQLSERVER"].ConnectionString;
            // 执行 SQL 命令
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(sqlTxt, conn);
                SqlParameter prm1 = new SqlParameter("@Name", SqlDbType.NVarChar, 50) { Value = currentDevice.Name };
                SqlParameter prm2 = new SqlParameter("@State", SqlDbType.Bit) { Value = currentDevice.State };
                SqlParameter prm3 = new SqlParameter("@SyncState", SqlDbType.Bit) { Value = currentDevice.SyncState };
                SqlParameter prm4 = new SqlParameter("@IP", SqlDbType.NVarChar, 15) { Value = currentDevice.ModbusTcpDevice.IP };
                SqlParameter prm5 = new SqlParameter("@Port", SqlDbType.Int) { Value = currentDevice.ModbusTcpDevice.Port };
                SqlParameter prm6 = new SqlParameter("@UnitID", SqlDbType.TinyInt) { Value = currentDevice.ModbusTcpDevice.UnitID };
                SqlParameter prm7 = new SqlParameter("@Id", SqlDbType.Int) { Value = currentDevice.Id };

                cmd.Parameters.AddRange(new SqlParameter[] {prm1, prm2, prm3, prm4, prm5, prm6, prm7});
                conn.Open();

                if (cmd.ExecuteNonQuery() >= 1)
                    return true;
                else
                    return false;
            }
        }

        /// <summary>
        /// 获取所有设备信息
        /// </summary>
        /// <returns>设备实体集合</returns>
        public List<Device> GetAllDeviceInfo()
        {
            //SQL命令
            const string sqltxt = "SELECT * FROM Device";
            //创建设备实体集合
            List<Device> deviceCollection = new List<Device>();
            //定义设备实体

            // 转换数据库存储的 二进制数据为 Byte[] 数组 以便进而转换为设备权限集合
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
                        // 创建设备实体
                        Device tmpDevice = new Device();
                        //将数据集转换成实体集合
                        tmpDevice.Id = Convert.ToInt32(myReader["Id"]);
                        tmpDevice.Name = Convert.ToString(myReader["Name"]);
                        tmpDevice.State = Convert.ToBoolean(myReader["State"]);
                        tmpDevice.SyncState = Convert.ToBoolean(myReader["SyncState"]);
                        tmpDevice.ModbusTcpDevice = new ModbusTcpDevice()
                        {
                            IP = Convert.ToString(myReader["Ip"]),
                            Port = Convert.ToInt32(myReader["Port"]),
                            UnitID = Convert.ToByte(myReader["UnitID"]),
                        };

                        // 添加到设备实体集合
                        deviceCollection.Add(tmpDevice);
                    }
                }
            }

            // 返回结果
            return deviceCollection;
        }

        /// <summary>
        /// 根据设备名称校验设备是否存在
        /// </summary>
        /// <param name="deviceName">设备名称</param>
        /// <returns>True:存在/Flase:不存在</returns>
        public bool CheckDeviceExist(string deviceName)
        {
            //创建查询信息的 SQL
            string sqlTxt = string.Format(
                "Select Count(*) From Device Where Name = '{0}'", deviceName);
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
