using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using IDAL.Control;
using Model.Control;
using ZedGraph;


namespace DAL.Control
{
    /// <summary>
    /// 曲线数据访问操作类
    /// </summary>
    public class CurveService : ICurveService
    {

        #region ICurveService 成员

        /// <summary>
        /// 根据曲线ID获取曲线实体
        /// </summary>
        /// <param name="id">曲线ID</param>
        /// <returns>曲线实体</returns>
        public Curve GetCurveInfoById(int id)
        {
            //SQL命令
            string sqltxt = string.Format("Select * From Curve Where Id = '{0}'", id);

            //创建曲线实体
            Curve tmpCurve = new Curve();

            ColorSymbolRotator rotator = new ColorSymbolRotator();
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
                        tmpCurve.Id = Convert.ToInt32(myReader["Id"]);
                        tmpCurve.Name = Convert.ToString(myReader["Name"]);
                        tmpCurve.VariableCode = Convert.ToString(myReader["VariableCode"]);
                        tmpCurve.DeviceID = Convert.ToInt32(myReader["DeviceID"]);
                        tmpCurve.Address = Convert.ToUInt16(myReader["Address"]);
                        tmpCurve.LineColor = string.IsNullOrEmpty(Convert.ToString(myReader["LineColor"]))
                            ? rotator.NextColor
                            : Color.FromName(Convert.ToString(myReader["LineColor"]));
                        tmpCurve.LineType = string.IsNullOrEmpty(Convert.ToString(myReader["LineType"])) ||
                                            Convert.ToBoolean(myReader["LineType"]);
                        tmpCurve.LineWidth = string.IsNullOrEmpty(Convert.ToString(myReader["LineWidth"]))
                            ? 2
                            : Convert.ToSingle(myReader["LineWidth"]);
                        tmpCurve.SymbolSize = string.IsNullOrEmpty(Convert.ToString(myReader["SymbolSize"]))
                            ? 4
                            : Convert.ToSingle(myReader["SymbolSize"]);
                        if (!(string.IsNullOrEmpty(Convert.ToString(myReader["SymbolType"]))))
                        {
                            switch (Convert.ToString(myReader["SymbolType"]))
                            {

                                case "Diamond":
                                    tmpCurve.SymbolType = SymbolType.Diamond;
                                    break;
                                case "Circle":
                                    tmpCurve.SymbolType = SymbolType.Circle;
                                    break;
                                case "Square":
                                    tmpCurve.SymbolType = SymbolType.Square;
                                    break;
                                case "Star":
                                    tmpCurve.SymbolType = SymbolType.Star;
                                    break;
                                case "Triangle":
                                    tmpCurve.SymbolType = SymbolType.Triangle;
                                    break;
                                case "Plus":
                                    tmpCurve.SymbolType = SymbolType.Plus;
                                    break;
                                case "None":
                                    tmpCurve.SymbolType = SymbolType.None;
                                    break;
                            }
                        }
                        else
                        {
                            tmpCurve.SymbolType = SymbolType.Default;
                        }

                        tmpCurve.XTitle = Convert.ToString(myReader["XTitle"]);
                        tmpCurve.YTitle = Convert.ToString(myReader["YTitle"]);
                        tmpCurve.YMax = Convert.ToSingle(myReader["YMax"]);
                        tmpCurve.YMin = Convert.ToSingle(myReader["YMin"]);
                    }
                    else
                        //如果没有读取到内容则抛出异常
                        throw new Exception("曲线ID错误！");
                }
            }
            // 返回结果
            return tmpCurve;
        }

        /// <summary>
        /// 添加曲线
        /// </summary>
        /// <param name="addCurve">要添加的曲线实体</param>
        /// <returns>True:成功/False:失败</returns>
        public bool AddCurve(Curve addCurve)
        {
            // 拼接 SQL 命令
            const string sqlTxt = "INSERT INTO Curve (VariableCode,Name,DeviceID,Address,LineColor,LineType,LineWidth,SymbolType,SymbolSize,XTitle,YTitle,YMax,YMin) VALUES " +
                                  "(@VariableCode,@Name,@DeviceID,@Address,@LineColor,@LineType,@LineWidth,@SymbolType,@SymbolSize,@XTitle,@YTitle,@YMax,@YMin)";
            // 从配置文件读取连接字符串
            string connectionString = ConfigurationManager.ConnectionStrings["SQLSERVER"].ConnectionString;
            // 执行 SQL 命令
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(sqlTxt, conn);
                SqlParameter prm0 = new SqlParameter("@VariableCode", SqlDbType.NVarChar, 16) { Value = addCurve.VariableCode };
                SqlParameter prm1 = new SqlParameter("@Name", SqlDbType.NVarChar, 50) { Value = addCurve.Name };
                SqlParameter prm2 = new SqlParameter("@DeviceID", SqlDbType.Int) { Value = addCurve.DeviceID };
                SqlParameter prm3 = new SqlParameter("@Address", SqlDbType.Int) { Value = addCurve.Address };
                SqlParameter prm4 = new SqlParameter("@LineColor", SqlDbType.NVarChar, 50) { Value = IsColorNull(addCurve.LineColor) };
                SqlParameter prm5 = new SqlParameter("@LineType", SqlDbType.Bit) { Value = IsStringNull(addCurve.LineType) };
                SqlParameter prm6 = new SqlParameter("@LineWidth", SqlDbType.Real) { Value = IsDoubleNull(addCurve.LineWidth) };
                SqlParameter prm7 = new SqlParameter("@SymbolType", SqlDbType.NVarChar, 30) { Value = IsSymbolNull(addCurve.SymbolType) };
                SqlParameter prm8 = new SqlParameter("@SymbolSize", SqlDbType.Real) { Value = IsDoubleNull(addCurve.SymbolSize) };
                SqlParameter prm9 = new SqlParameter("@XTitle", SqlDbType.NVarChar, 50) { Value = addCurve.XTitle };
                SqlParameter prm10 = new SqlParameter("@YTitle", SqlDbType.NVarChar, 50) { Value = addCurve.YTitle };
                SqlParameter prm11 = new SqlParameter("@YMax", SqlDbType.Real) { Value = addCurve.YMax };
                SqlParameter prm12 = new SqlParameter("@YMin", SqlDbType.Real) { Value = addCurve.YMin };

                cmd.Parameters.AddRange(new SqlParameter[]
                {prm0, prm1, prm2, prm3, prm4, prm5, prm6, prm7, prm8, prm9, prm10, prm11, prm12});
                conn.Open();

                if (cmd.ExecuteNonQuery() >= 1)
                    return true;
                else
                    return false;
            }
        }

        /// <summary>
        /// 删除曲线
        /// </summary>
        /// <param name="id">要删除的曲线 ID</param>
        /// <returns>True:成功/False:失败</returns>
        public bool DeleteCurveById(int id)
        {
            // 删除单个信息 SQL 命令
            string sqlTxt = string.Format("Delete From Curve Where Id = {0}", id);
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
        /// 修改曲线
        /// </summary>
        /// <param name="currentCurve">要修改的曲线实体</param>
        /// <returns>True:成功/False:失败</returns>
        public bool ModifyCurve(Curve currentCurve)
        {
            // 拼接 SQL 命令
            const string sqlTxt = "UPDATE Curve SET VariableCode=@VariableCode,Name=@Name,DeviceID=@DeviceID,Address=@Address,LineColor=@LineColor,LineType=@LineType,LineWidth=@LineWidth,SymbolType=@SymbolType,SymbolSize=@SymbolSize,XTitle=@XTitle,YTitle=@YTitle,YMax=@YMax,YMin=@YMin WHERE Id=@Id";

            // 从配置文件读取连接字符串
            string connectionString = ConfigurationManager.ConnectionStrings["SQLSERVER"].ConnectionString;
            // 执行 SQL 命令
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(sqlTxt, conn);
                SqlParameter prm0 = new SqlParameter("@VariableCode", SqlDbType.NVarChar, 16) { Value = currentCurve.VariableCode };
                SqlParameter prm1 = new SqlParameter("@Name", SqlDbType.NVarChar, 50) { Value = currentCurve.Name };
                SqlParameter prm2 = new SqlParameter("@DeviceID", SqlDbType.Int) { Value = currentCurve.DeviceID };
                SqlParameter prm3 = new SqlParameter("@Address", SqlDbType.Int) { Value = currentCurve.Address };
                SqlParameter prm4 = new SqlParameter("@LineColor", SqlDbType.NVarChar, 50) { Value = IsColorNull(currentCurve.LineColor) };
                SqlParameter prm5 = new SqlParameter("@LineType", SqlDbType.Bit) { Value = IsStringNull(currentCurve.LineType) };
                SqlParameter prm6 = new SqlParameter("@LineWidth", SqlDbType.Real) { Value = IsDoubleNull(currentCurve.LineWidth) };
                SqlParameter prm7 = new SqlParameter("@SymbolType", SqlDbType.NVarChar, 30) { Value = IsSymbolNull(currentCurve.SymbolType) };
                SqlParameter prm8 = new SqlParameter("@SymbolSize", SqlDbType.Real) { Value = IsDoubleNull(currentCurve.SymbolSize) };
                SqlParameter prm9 = new SqlParameter("@XTitle", SqlDbType.NVarChar, 50) { Value = currentCurve.XTitle };
                SqlParameter prm10 = new SqlParameter("@YTitle", SqlDbType.NVarChar, 50) { Value = currentCurve.YTitle };
                SqlParameter prm11 = new SqlParameter("@YMax", SqlDbType.Real) { Value = currentCurve.YMax };
                SqlParameter prm12 = new SqlParameter("@YMin", SqlDbType.Real) { Value = currentCurve.YMin };
                SqlParameter prm13 = new SqlParameter("@Id", SqlDbType.Int) { Value = currentCurve.Id };

                cmd.Parameters.AddRange(new SqlParameter[]
                {prm0, prm1, prm2, prm3, prm4, prm5, prm6, prm7, prm8, prm9, prm10, prm11, prm12, prm13});
                conn.Open();

                if (cmd.ExecuteNonQuery() >= 1)
                    return true;
                else
                    return false;
            }
        }

        /// <summary>
        /// 获取所有曲线信息
        /// </summary>
        /// <returns>曲线实体集合</returns>
        public List<Curve> GetAllCurveInfo()
        {
            //SQL命令
            const string sqltxt = "SELECT * FROM Curve";
            //创建曲线实体集合
            List<Curve> curveCollection = new List<Curve>();
            //定义曲线实体

            ColorSymbolRotator rotator = new ColorSymbolRotator();
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
                        // 创建曲线实体
                        Curve tmpCurve = new Curve();
                        //将数据集转换成实体集合
                        tmpCurve.Id = Convert.ToInt32(myReader["Id"]);
                        tmpCurve.VariableCode = Convert.ToString(myReader["VariableCode"]);
                        tmpCurve.Name = Convert.ToString(myReader["Name"]);
                        tmpCurve.DeviceID = Convert.ToInt32(myReader["DeviceID"]);
                        tmpCurve.Address = Convert.ToUInt16(myReader["Address"]);
                        tmpCurve.LineColor = string.IsNullOrEmpty(Convert.ToString(myReader["LineColor"]))
                            ? rotator.NextColor
                            : Color.FromName(Convert.ToString(myReader["LineColor"]));
                        tmpCurve.LineType = string.IsNullOrEmpty(Convert.ToString(myReader["LineType"])) ||
                                            Convert.ToBoolean(myReader["LineType"]);
                        tmpCurve.LineWidth = string.IsNullOrEmpty(Convert.ToString(myReader["LineWidth"]))
                            ? 2
                            : Convert.ToSingle(myReader["LineWidth"]);

                        tmpCurve.SymbolSize = string.IsNullOrEmpty(Convert.ToString(myReader["SymbolSize"]))
                            ? 4
                            : Convert.ToSingle(myReader["SymbolSize"]);
                        if (!(string.IsNullOrEmpty(Convert.ToString(myReader["SymbolType"]))))
                        {
                            switch (Convert.ToString(myReader["SymbolType"]))
                            {
                                case "Diamond":
                                    tmpCurve.SymbolType = SymbolType.Diamond;
                                    break;
                                case "Circle":
                                    tmpCurve.SymbolType = SymbolType.Circle;
                                    break;
                                case "Square":
                                    tmpCurve.SymbolType = SymbolType.Square;
                                    break;
                                case "Star":
                                    tmpCurve.SymbolType = SymbolType.Star;
                                    break;
                                case "Triangle":
                                    tmpCurve.SymbolType = SymbolType.Triangle;
                                    break;
                                case "Plus":
                                    tmpCurve.SymbolType = SymbolType.Plus;
                                    break;
                                case "None":
                                    tmpCurve.SymbolType = SymbolType.None;
                                    break;
                            }
                        }
                        else
                        {
                            tmpCurve.SymbolType = SymbolType. UserDefined;
                        }

                        tmpCurve.XTitle = Convert.ToString(myReader["XTitle"]);
                        tmpCurve.YTitle = Convert.ToString(myReader["YTitle"]);
                        tmpCurve.YMax = Convert.ToSingle(myReader["YMax"]);
                        tmpCurve.YMin = Convert.ToSingle(myReader["YMin"]);

                        // 添加到曲线实体集合
                        curveCollection.Add(tmpCurve);
                    }
                }
            }

            // 返回结果
            return curveCollection;
        }

        /// <summary>
        /// 根据曲线名称校验曲线是否存在
        /// </summary>
        /// <param name="curveName">曲线名称</param>
        /// <returns>True:存在/Flase:不存在</returns>
        public bool CheckCurveExist(string curveName)
        {
            //创建查询信息的 SQL
            string sqlTxt = string.Format(
                "Select Count(*) From Curve Where Name = '{0}'", curveName);
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
        private object IsDoubleNull(object parameter)
        {
            if (Convert.ToDouble(parameter).Equals(-1))
                return DBNull.Value;
            else return parameter;
        }

        /// <summary>
        /// Determines whether [the specified parameter] [is string null].
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns>result(null for "")</returns>
        private object IsStringNull(object parameter)
        {
            if (Convert.ToString(parameter).Equals(""))
                return DBNull.Value;
            else return parameter;
        }

        /// <summary>
        /// Determines whether [the specified symbol] [is symbol null].
        /// </summary>
        /// <param name="symbol">The symbol.</param>
        /// <returns>result(DBNull.Value for SymbolType.UserDefined)</returns>
        private object IsSymbolNull(SymbolType symbol)
        {
            if (symbol.Equals(SymbolType.UserDefined))
                return DBNull.Value;
            else return symbol;
        }

        /// <summary>
        /// Determines whether [the specified color] [is color null].
        /// </summary>
        /// <param name="color">The color.</param>
        /// <returns>result(DBNull.Value for Color.FromArgb(0))</returns>
        private object IsColorNull(Color color)
        {
            if (color.Equals(Color.FromArgb(0)))
                return DBNull.Value;
            else return color.Name;
        }
        #endregion
    }
}
