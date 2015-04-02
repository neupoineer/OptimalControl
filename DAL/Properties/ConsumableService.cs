using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DBUtility = CodingMouse.CMHotelManager.DBUtility;
using IDAL = CodingMouse.CMHotelManager.IDAL;
using Model = CodingMouse.CMHotelManager.Model;

namespace CodingMouse.CMHotelManager.DAL
{
    /// <summary>
    /// 消费品数据访问操作类
    /// </summary>
    public class ConsumableService : IDAL.IConsumableService
    {

        #region IConsumableService 成员

        /// <summary>
        /// 添加单个消费品信息
        /// </summary>
        /// <param name="consumable">消费品实体</param>
        /// <returns>True:成功 / False:失败</returns>
        public bool AddConsumable(Model.Consumable consumable)
        {
            // 创建临时数据集
            DataSet dsconsumableType = new DataSet("ConsumableType");
            // 消费品预设单价
            decimal advanceUnitPrice = consumable.ConsumableAdvanceUnitPrice;
            // 消费品数量
            int consumableNumber = consumable.ConsumableNumber;
            // 消费品类型
            string consumableType = consumable.ConsumableType;
            // 消费品名称
            string modelName = consumable.ModelName;
            // 创建 SQL 执行对象
            DBUtility.AbstractDBProvider dbProvider = DBUtility.AbstractDBProvider.Instance();
            // 提取出 Id
            int consumableTypeId = consumable.ConsumableTypeId;
            // 插入消费品信息 的 SQL 命令
            string sqlTxt = string.Format("insert into Consumable Values ('{0}', {1}, {2}, {3})", modelName, advanceUnitPrice, consumableTypeId, consumableNumber);
            // 执行 插入消费品信息 SQL
            int rowsAffected;
            dbProvider.RunCommand(sqlTxt, out rowsAffected);

            if (rowsAffected == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 根据消费品 ID 删除单个/批量消费品信息
        /// </summary>
        /// <param name="idList">消费品 ID 集合</param>
        /// <returns>True:成功 / False:失败</returns>
        public bool DeleteConsumableByID(List<int> idList)
        {
            bool isOk = false;
            int consumableId = 0;
            // 创建 SQL 执行对象
            DBUtility.AbstractDBProvider dbProvider = DBUtility.AbstractDBProvider.Instance();
            // 打开数据库连接
            dbProvider.Open();
            // 开始事务
            dbProvider.BeginTrans();
            //循环执行 删除操作
            foreach(int id in idList)
            {
                //执行删除单个操作
                consumableId = id;
                // 删除消费品 单个信息 SQL 命令
                string sqlTxt = string.Format("Delete From Consumable Where Id = {0}", consumableId);

                //接受返回的受影响行数
                int rowsAffected;
                dbProvider.RunCommand(sqlTxt, out rowsAffected);

                if (rowsAffected != 1)
                {
                    isOk = false;
                    break;
                }
                else
                {
                    isOk = true;
                }
            }

            if (isOk)
                // 提交事务
                dbProvider.CommitTrans();
            else
                // 回滚事务
                dbProvider.RollBackTrans();

            // 关闭数据连接
            dbProvider.Close();

            return isOk;
        }

        /// <summary>
        /// 获取所有消费品信息
        /// </summary>
        /// <returns>消费品集合</returns>
        public List<Model.Consumable> GetAllConsumable()
        {
            //创建消费品集合
            List<Model.Consumable> consumableList = new List<CodingMouse.CMHotelManager.Model.Consumable>();
            //创建临时数据集
            DataSet dsConsumable = new DataSet("Consumable");
            //创建 查询消费品信息 SQL命令
            string sqlTxt = string.Format("select C.Id, C.ConsumableName, C.ConsumableAdvanceUnitPrice, C.ConsumableNumber, T.Id as [TypeId]," +
                "T.ConsumableTypeName from Consumable as C join ConsumableType as T on C.ConsumableTypeId = T.Id");
            //创建SQL执行对象
            DBUtility.AbstractDBProvider dbProvider = DBUtility.AbstractDBProvider.Instance();
            //执行消费品查询操作
            dsConsumable = dbProvider.RunCommand(sqlTxt, "Consumable");
            //将数据集转换成实体集合
            foreach (DataRow row in dsConsumable.Tables["Consumable"].Rows)
            {
                Model.Consumable tmConsumable = new CodingMouse.CMHotelManager.Model.Consumable();
                tmConsumable.ConsumableAdvanceUnitPrice = Convert.ToDecimal(row["ConsumableAdvanceUnitPrice"]);
                tmConsumable.ConsumableNumber = Convert.ToInt32(row["ConsumableNumber"]);
                tmConsumable.ConsumableTypeId = Convert.ToInt32(row["TypeId"]);
                tmConsumable.ConsumableType = Convert.ToString(row["ConsumableTypeName"]);
                tmConsumable.Id = Convert.ToInt32(row["Id"]);
                tmConsumable.ModelName = Convert.ToString(row["ConsumableName"]);

                consumableList.Add(tmConsumable);
            }
            
            //返回所有消费品集合
            return consumableList;
        }

        /// <summary>
        /// 根据选择列名和查询内容查询消费品信息
        /// </summary>
        /// <param name="columnName">列名</param>
        /// <param name="content">查询内容</param>
        /// <returns>消费品集合</returns>
        public List<Model.Consumable> GetConsumableByColumnAndContent(string columnName, string content)
        {
            //创建消费品集合
            List<Model.Consumable> consumableList = new List<CodingMouse.CMHotelManager.Model.Consumable>();
            //创建临时数据集
            DataSet dsConsumable = new DataSet("Consumable");
            //创建查询信息的 SQL
            string sqlTxt = null;
            //尝试转换整数型查询内容
            int numberContent = 0;
            if (int.TryParse(content, out numberContent))
            {
                sqlTxt = string.Format(
                    "select C.Id, C.ConsumableName, C.ConsumableAdvanceUnitPrice, C.ConsumableNumber, T.Id as [TypeId]," +
                    "T.ConsumableTypeName from Consumable as C join ConsumableType as T on C.ConsumableTypeId = T.Id where {0} = {1}", columnName, numberContent);
            }
            else
            {
                sqlTxt = string.Format(
                    "select C.Id, C.ConsumableName, C.ConsumableAdvanceUnitPrice, C.ConsumableNumber, T.Id as [TypeId]," +
                    "T.ConsumableTypeName from Consumable as C join ConsumableType as T on C.ConsumableTypeId = T.Id where {0} like '%{1}%'", columnName, content);
            }
            //创建SQL执行对象
            DBUtility.AbstractDBProvider dbProvider = DBUtility.AbstractDBProvider.Instance();
            //执行消费品查询操作
            dsConsumable = dbProvider.RunCommand(sqlTxt, "Consumable");
            //将数据集转换成实体集合
            foreach (DataRow row in dsConsumable.Tables["Consumable"].Rows)
            {
                Model.Consumable tmConsumable = new CodingMouse.CMHotelManager.Model.Consumable();
                tmConsumable.ConsumableAdvanceUnitPrice = Convert.ToDecimal(row["ConsumableAdvanceUnitPrice"]);
                tmConsumable.ConsumableNumber = Convert.ToInt32(row["ConsumableNumber"]);
                tmConsumable.ConsumableTypeId = Convert.ToInt32(row["TypeId"]);
                tmConsumable.ConsumableType = Convert.ToString(row["ConsumableTypeName"]);
                tmConsumable.Id = Convert.ToInt32(row["Id"]);
                tmConsumable.ModelName = Convert.ToString(row["ConsumableName"]);

                consumableList.Add(tmConsumable);
            }

            //返回所有消费品集合
            return consumableList;
        }

        /// <summary>
        /// 修改单个/批量消费品信息
        /// </summary>
        /// <param name="consumableList">消费品集合</param>
        /// <returns>True:成功 / False:失败</returns>
        public bool ModifyConsumable(List<Model.Consumable> consumableList)
        {
            bool isOk = false;
            // 创建临时数据集
            DataSet dsconsumableType = new DataSet("ConsumableType");
            // 创建 SQL 执行对象
            DBUtility.AbstractDBProvider dbProvider = DBUtility.AbstractDBProvider.Instance();
            // 打开数据库连接
            dbProvider.Open();
            // 开始事务
            dbProvider.BeginTrans();

            //循环修改 list 中传入的每一项
            foreach (Model.Consumable con in consumableList)
            {
                // 消费品预设单价
                decimal advanceUnitPrice = con.ConsumableAdvanceUnitPrice;
                // 消费品数量
                int consumableNumber = con.ConsumableNumber;
                // 消费品类型
                string consumableType = con.ConsumableType;
                // 消费品名称
                string modelName = con.ModelName;
                // 消费品编号
                int id = con.Id;

                // 提取出 Id
                int consumableTypeId = con.ConsumableTypeId;

                //修改消费品信息 SQL 命令
                string sqlTxt = string.Format("update Consumable set ConsumableName = '{0}', ConsumableAdvanceUnitPrice = '{1}', ConsumableTypeId = '{2}', ConsumableNumber = '{3}' where Id = {4}", modelName, advanceUnitPrice, consumableTypeId, consumableNumber, id);
                // 执行SQL命令
                // 接收返回的 受影响行数
                int rowsAffected = 0;
                dbProvider.RunCommand(sqlTxt, out rowsAffected);

                if (rowsAffected != 1)
                {
                    isOk = false;
                    break;
                }
                else
                {
                    isOk = true;
                }
            }

            if (isOk)
                // 提交事务
                dbProvider.CommitTrans();
            else
                // 回滚事务
                dbProvider.RollBackTrans();

            // 关闭数据连接
            dbProvider.Close();

            return isOk;
        }

        /// <summary>
        /// 根据消费品类型名称获取该类型下的所有消费品集合
        /// </summary>
        /// <param name="typeName">消费品类型名称</param>
        /// <returns>消费品集合</returns>
        public List<Model.Consumable> GetConsumableByTypeName(string typeName)
        {
            //创建消费品集合
            List<Model.Consumable> consumableList = new List<CodingMouse.CMHotelManager.Model.Consumable>();
            //创建临时数据集
            DataSet dsConsumable = new DataSet("Consumable");
            //创建 查询消费品信息 SQL命令
            string sqlTxt = string.Format("select C.Id, C.ConsumableName, C.ConsumableAdvanceUnitPrice, C.ConsumableNumber, T.Id as [TypeId]," +
                "T.ConsumableTypeName from Consumable as C join ConsumableType as T on C.ConsumableTypeId = T.Id Where ConsumableTypeName = '{0}'", typeName);
            //创建SQL执行对象
            DBUtility.AbstractDBProvider dbProvider = DBUtility.AbstractDBProvider.Instance();
            //执行消费品查询操作
            dsConsumable = dbProvider.RunCommand(sqlTxt, "Consumable");
            //将数据集转换成实体集合
            foreach (DataRow row in dsConsumable.Tables["Consumable"].Rows)
            {
                Model.Consumable tmConsumable = new CodingMouse.CMHotelManager.Model.Consumable();
                tmConsumable.ConsumableAdvanceUnitPrice = Convert.ToDecimal(row["ConsumableAdvanceUnitPrice"]);
                tmConsumable.ConsumableNumber = Convert.ToInt32(row["ConsumableNumber"]);
                tmConsumable.ConsumableTypeId = Convert.ToInt32(row["TypeId"]);
                tmConsumable.ConsumableType = Convert.ToString(row["ConsumableTypeName"]);
                tmConsumable.Id = Convert.ToInt32(row["Id"]);
                tmConsumable.ModelName = Convert.ToString(row["ConsumableName"]);

                consumableList.Add(tmConsumable);
            }

            //返回所有消费品集合
            return consumableList;
        }

        #endregion
    }
}
