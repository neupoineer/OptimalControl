using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using IDAL.Control;
using Rule = Model.Control.Rule;

namespace DAL.Control
{
    /// <summary>
    /// 规则数据访问操作类
    /// </summary>
    public class RuleService : IRuleService
    {

        #region IRuleService 成员

        /// <summary>
        /// 根据规则ID获取规则实体
        /// </summary>
        /// <param name="id">规则ID</param>
        /// <returns>规则实体</returns>
        public Rule GetRuleInfoById(int id)
        {
            //SQL命令
            string sqltxt = string.Format("Select * From Rules Where Id = '{0}'", id);

            //创建规则实体
            Rule tmpRule = new Rule();

            // 转换数据库存储的 二进制数据为 Byte[] 数组 以便进而转换为规则权限集合
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
                        tmpRule.Id = Convert.ToInt32(myReader["Id"]);
                        tmpRule.Name = Convert.ToString(myReader["Name"]);
                        tmpRule.Expression = Convert.ToString(myReader["Expression"]);
                        tmpRule.Operation = Convert.ToString(myReader["Operation"]);
                        tmpRule.Period = Convert.ToInt32(myReader["Period"]);
                        tmpRule.State = Convert.ToBoolean(myReader["State"]);
                        tmpRule.Priority = Convert.ToInt32(myReader["Priority"]);
                    }
                    else
                        //如果没有读取到内容则抛出异常
                        throw new Exception("规则ID错误！");
                }
            }
            // 返回结果
            return tmpRule;
        }

        /// <summary>
        /// 添加规则
        /// </summary>
        /// <param name="addRule">要添加的规则实体</param>
        /// <returns>True:成功/False:失败</returns>
        public bool AddRule(Rule addRule)
        {
            // 拼接 SQL 命令
            string sqlTxt =
                "INSERT INTO Rules (Name,Expression,Operation,Period,State,Priority) VALUES " +
                "(@Name,@Expression,@Operation,@Period,@Enabled,@Priority)";
            // 从配置文件读取连接字符串
            string connectionString = ConfigurationManager.ConnectionStrings["SQLSERVER"].ConnectionString;
            // 执行 SQL 命令
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(sqlTxt, conn);
                SqlParameter prm1 = new SqlParameter("@Name", SqlDbType.NVarChar, 50) { Value = addRule.Name };
                SqlParameter prm2 = new SqlParameter("@Expression", SqlDbType.NVarChar, 1000) { Value = addRule.Expression };
                SqlParameter prm3 = new SqlParameter("@Operation", SqlDbType.NVarChar, 1000) { Value = addRule.Operation };
                SqlParameter prm4 = new SqlParameter("@Period", SqlDbType.Int) { Value = addRule.Period };
                SqlParameter prm5 = new SqlParameter("@State", SqlDbType.Bit) { Value = addRule.State };
                SqlParameter prm6 = new SqlParameter("@Priority", SqlDbType.Int) { Value = addRule.Priority };

                cmd.Parameters.AddRange(new SqlParameter[] { prm1, prm2, prm3, prm4, prm5, prm6 });
                conn.Open();

                if (cmd.ExecuteNonQuery() >= 1)
                    return true;
                else
                    return false;
            }
        }

        /// <summary>
        /// 删除规则
        /// </summary>
        /// <param name="id">要删除的规则 ID</param>
        /// <returns>True:成功/False:失败</returns>
        public bool DeleteRuleById(int id)
        {
            // 删除单个信息 SQL 命令
            string sqlTxt = string.Format("Delete From Rules Where Id = {0}", id);
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
        /// 修改规则
        /// </summary>
        /// <param name="currentRule">要修改的规则实体</param>
        /// <returns>True:成功/False:失败</returns>
        public bool ModifyRule(Rule currentRule)
        {
            // 拼接 SQL 命令
            string sqlTxt =
                "UPDATE Rules SET Name=@Name,State=@State,SyncState=@SyncState,IP=@IP,Port=@Port,UnitID=@UnitID WHERE Id=@Id";

            // 从配置文件读取连接字符串
            string connectionString = ConfigurationManager.ConnectionStrings["SQLSERVER"].ConnectionString;
            // 执行 SQL 命令
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(sqlTxt, conn);
                SqlParameter prm1 = new SqlParameter("@Name", SqlDbType.NVarChar, 50) { Value = currentRule.Name };
                SqlParameter prm2 = new SqlParameter("@Expression", SqlDbType.NVarChar, 1000) { Value = currentRule.Expression };
                SqlParameter prm3 = new SqlParameter("@Operation", SqlDbType.NVarChar, 1000) { Value = currentRule.Operation };
                SqlParameter prm4 = new SqlParameter("@Period", SqlDbType.Int) { Value = currentRule.Period };
                SqlParameter prm5 = new SqlParameter("@State", SqlDbType.Bit) { Value = currentRule.State };
                SqlParameter prm6 = new SqlParameter("@Priority", SqlDbType.Int) { Value = currentRule.Priority };
                SqlParameter prm7 = new SqlParameter("@Id", SqlDbType.Int) { Value = currentRule.Id };

                cmd.Parameters.AddRange(new SqlParameter[] {prm1, prm2, prm3, prm4, prm5, prm6, prm7});
                conn.Open();

                if (cmd.ExecuteNonQuery() >= 1)
                    return true;
                else
                    return false;
            }
        }

        /// <summary>
        /// 获取有效的规则实体
        /// </summary>
        /// <returns>规则实体</returns>
        public List<Rule> GetRuleInfoEnabled()
        {
            //SQL命令
            string sqltxt = string.Format("Select * From Rules Where State = 'True'");

            //创建规则实体集合
            List<Rule> ruleCollection = new List<Rule>();
            //定义规则实体

            // 转换数据库存储的 二进制数据为 Byte[] 数组 以便进而转换为规则权限集合
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
                        // 创建规则实体
                        Rule tmpRule = new Rule();
                        //将数据集转换成实体集合
                        tmpRule.Id = Convert.ToInt32(myReader["Id"]);
                        tmpRule.Name = Convert.ToString(myReader["Name"]);
                        tmpRule.Expression = Convert.ToString(myReader["Expression"]);
                        tmpRule.Operation = Convert.ToString(myReader["Operation"]);
                        tmpRule.Period = Convert.ToInt32(myReader["Period"]);
                        tmpRule.State = Convert.ToBoolean(myReader["State"]);
                        tmpRule.Priority = Convert.ToInt32(myReader["Priority"]);

                        // 添加到规则实体集合
                        ruleCollection.Add(tmpRule);
                    }
                }
            }

            // 返回结果
            return ruleCollection;
        }

        /// <summary>
        /// 获取所有规则信息
        /// </summary>
        /// <returns>规则实体集合</returns>
        public List<Rule> GetAllRuleInfo()
        {
            //SQL命令
            string sqltxt = "SELECT * FROM Rules";
            //创建规则实体集合
            List<Rule> ruleCollection = new List<Rule>();
            //定义规则实体

            // 转换数据库存储的 二进制数据为 Byte[] 数组 以便进而转换为规则权限集合
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
                        // 创建规则实体
                        Rule tmpRule = new Rule();
                        //将数据集转换成实体集合
                        tmpRule.Id = Convert.ToInt32(myReader["Id"]);
                        tmpRule.Name = Convert.ToString(myReader["Name"]);
                        tmpRule.Expression = Convert.ToString(myReader["Expression"]);
                        tmpRule.Operation = Convert.ToString(myReader["Operation"]);
                        tmpRule.Period = Convert.ToInt32(myReader["Period"]);
                        tmpRule.State = Convert.ToBoolean(myReader["State"]);
                        tmpRule.Priority = Convert.ToInt32(myReader["Priority"]);

                        // 添加到规则实体集合
                        ruleCollection.Add(tmpRule);
                    }
                }
            }

            // 返回结果
            return ruleCollection;
        }

        /// <summary>
        /// 根据规则名称校验规则是否存在
        /// </summary>
        /// <param name="ruleName">规则名称</param>
        /// <returns>True:存在/Flase:不存在</returns>
        public bool CheckRuleExist(string ruleName)
        {
            //创建查询信息的 SQL
            string sqlTxt = string.Format(
                "Select Count(*) From Rules Where Name = '{0}'", ruleName);
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
