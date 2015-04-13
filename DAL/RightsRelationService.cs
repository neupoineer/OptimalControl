using System;
using System.Collections.Generic;
using System.Data;
using IDAL;
using Model;

namespace DAL
{
    /// <summary>
    /// 权限关系数据访问操作类
    /// </summary>
    public class RightsRelationService : IRightsRelationService
    {
        #region IRightsRelationService 成员

        /// <summary>
        /// 添加单个权限关系
        /// </summary>
        /// <param name="rightsRelation">权限关系实体</param>
        /// <returns>True:成功/False:失败</returns>
        public bool AddRightsRelation(RightsRelation rightsRelation)
        {
            // 拼接 SQL 命令
            string sqlTxt = string.Format(
                "Insert Into RightsRelation (OperatorId, RightsGroupId) " +
                "Values ({0}, {1})",
                rightsRelation.OperatorId, rightsRelation.RightsGroupId);

            // 创建 SQL 执行对象
            DBUtility.AbstractDBProvider dbProvider = DBUtility.AbstractDBProvider.Instance();
            // 执行 SQL
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
        /// 根据权限关系 ID 删除权限关系
        /// </summary>
        /// <param name="id">权限关系 ID</param>
        /// <returns>True:成功/False:失败</returns>
        public bool DeleteRightsRelationById(int id)
        {
            bool isOk = false;

            // 删除单个信息 SQL 命令
            string sqlTxt = string.Format("Delete From RightsRelation Where Id = {0}", id);
            // 创建 SQL 执行对象
            DBUtility.AbstractDBProvider dbProvider = DBUtility.AbstractDBProvider.Instance();
            // 执行 删除操作
            int rowsAffected;
            dbProvider.RunCommand(sqlTxt, out rowsAffected);

            if (rowsAffected >= 1)
            {
                isOk = true;
            }
            else
            {
                isOk = false;
            }

            return isOk;
        }

        /// <summary>
        /// 根据操作员 ID 删除对应的所有权限关系
        /// </summary>
        /// <param name="operatorId">操作员 ID</param>
        /// <returns>True:成功/False:失败</returns>
        public bool DeleteRightsRelationByOperatorId(int operatorId)
        {
            bool isOk = false;

            // 删除单个信息 SQL 命令
            string sqlTxt = string.Format("Delete From RightsRelation Where OperatorId = {0}", operatorId);
            // 创建 SQL 执行对象
            DBUtility.AbstractDBProvider dbProvider = DBUtility.AbstractDBProvider.Instance();
            // 执行 删除操作
            int rowsAffected;
            dbProvider.RunCommand(sqlTxt, out rowsAffected);

            if (rowsAffected >= 1)
            {
                isOk = true;
            }
            else
            {
                isOk = false;
            }

            return isOk;
        }

        /// <summary>
        /// 修改单个权限关系
        /// </summary>
        /// <param name="rightsRelation">权限关系实体</param>
        /// <returns>True:成功/False:失败</returns>
        public bool ModifyRightsRelation(RightsRelation rightsRelation)
        {
            // 拼接 SQL 命令
            string sqlTxt = string.Format(
                "Update RightsRelation Set OperatorId = {0}, RightsGroupId = {1} Where Id = {2}",
                rightsRelation.OperatorId, rightsRelation.RightsGroupId, rightsRelation.Id);

            // 创建 SQL 执行对象
            DBUtility.AbstractDBProvider dbProvider = DBUtility.AbstractDBProvider.Instance();
            // 执行 SQL
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
        /// 获取所有的权限关系集合
        /// </summary>
        /// <returns>权限关系集合</returns>
        public List<RightsRelation> GetAllRightsRelation()
        {
            //创建数据集
            DataSet dsRightsRelation = new DataSet("RightsRelation");
            //创建客户集合
            List<RightsRelation> rightsRelationList = new List<RightsRelation>();
            //创建查询客户信息的 SQL
            string sqlTxt = "Select R.Id, R.OperatorId, O.OperatorName, R.RightsGroupId, " +
                "G.GroupName As [RightsGroupName] From RightsRelation As R Join Operator As O " +
                "On R.OperatorId = O.Id Join RightsGroup As G On R.RightsGroupId = G.Id";
            //创建SQL执行对象
            DBUtility.AbstractDBProvider dbProvider = DBUtility.AbstractDBProvider.Instance();
            //执行查询操作
            dsRightsRelation = dbProvider.RunCommand(sqlTxt, "RightsRelation");
            //将数据集转换成实体集合
            foreach (DataRow row in dsRightsRelation.Tables["RightsRelation"].Rows)
            {
                RightsRelation tmpRightsRelation = new RightsRelation();
                tmpRightsRelation.Id = Convert.ToInt32(row["Id"]);
                tmpRightsRelation.OperatorId = Convert.ToInt32(row["OperatorId"]);
                tmpRightsRelation.OperatorName = Convert.ToString(row["OperatorName"]);
                tmpRightsRelation.RightsGroupId = Convert.ToInt32(row["RightsGroupId"]);
                tmpRightsRelation.RightsGroupName = Convert.ToString(row["RightsGroupName"]);

                rightsRelationList.Add(tmpRightsRelation);
            }

            //返回所有客户集合
            return rightsRelationList;
        }

        /// <summary>
        /// 根据操作员 ID 获取对应的所有权限关系
        /// </summary>
        /// <param name="id">操作员 ID</param>
        /// <returns>权限关系集合</returns>
        public List<RightsRelation> GetRightsRelationByOperatorId(int id)
        {
            //创建数据集
            DataSet dsRightsRelation = new DataSet("RightsRelation");
            //创建客户集合
            List<RightsRelation> rightsRelationList = new List<RightsRelation>();
            //创建查询客户信息的 SQL
            string sqlTxt = string.Format("Select R.Id, R.OperatorId, O.OperatorName, R.RightsGroupId, " +
                "G.GroupName As [RightsGroupName] From RightsRelation As R Join Operator As O " +
                "On R.OperatorId = O.Id Join RightsGroup As G On R.RightsGroupId = G.Id " +
                "Where OperatorId = {0}", id);
            //创建SQL执行对象
            DBUtility.AbstractDBProvider dbProvider = DBUtility.AbstractDBProvider.Instance();
            //执行查询操作
            dsRightsRelation = dbProvider.RunCommand(sqlTxt, "RightsRelation");
            //将数据集转换成实体集合
            foreach (DataRow row in dsRightsRelation.Tables["RightsRelation"].Rows)
            {
                RightsRelation tmpRightsRelation = new RightsRelation();
                tmpRightsRelation.Id = Convert.ToInt32(row["Id"]);
                tmpRightsRelation.OperatorId = Convert.ToInt32(row["OperatorId"]);
                tmpRightsRelation.OperatorName = Convert.ToString(row["OperatorName"]);
                tmpRightsRelation.RightsGroupId = Convert.ToInt32(row["RightsGroupId"]);
                tmpRightsRelation.RightsGroupName = Convert.ToString(row["RightsGroupName"]);

                rightsRelationList.Add(tmpRightsRelation);
            }

            //返回所有客户集合
            return rightsRelationList;
        }

        /// <summary>
        /// 根据权限组 ID 获取与此权限组相关的权限关系数量
        /// </summary>
        /// <param name="id">权限组 ID</param>
        /// <returns>权限关系数量</returns>
        public int GetRightsRelationCountByRightsGroupId(int id)
        {
            // SQL命令
            string sqlTxt = string.Format("Select Count(*) From RightsRelation Where RightsGroupId = {0}", id);

            // 创建SQL执行对象
            DBUtility.AbstractDBProvider dbProvider = DBUtility.AbstractDBProvider.Instance();
            // 执行查询操作
            int result = Convert.ToInt32(dbProvider.RunCommand(sqlTxt));

            // 返回结果
            return result;
        }

        #endregion
    }
}
