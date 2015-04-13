using System.Collections.Generic;
using Model;

namespace IDAL
{
    /// <summary>
    /// 操作员数据访问操作接口
    /// </summary>
    public interface IOperatorService
    {
        /// <summary>
        /// 根据操作员名称和密码获取操作员实体
        /// </summary>
        /// <param name="name">操作员名称</param>
        /// <param name="pwd">操作员密码</param>
        /// <returns>操作员实体</returns>
        Operator GetOperatorInfoByName(string name, string pwd);

        /// <summary>
        /// 获取所有操作员信息
        /// </summary>
        /// <returns>操作员实体集合</returns>
        Dictionary<string, Operator> GetAllOperatorInfo();

        /// <summary>
        /// 添加操作员
        /// </summary>
        /// <param name="addOperator">要添加的操作员实体</param>
        /// <returns>True:成功/False:失败</returns>
        bool AddOperator(Operator addOperator);

        /// <summary>
        /// 删除操作员
        /// </summary>
        /// <param name="id">要删除的操作员 ID</param>
        /// <returns>True:成功/False:失败</returns>
        bool DeleteOperatorByID(int id);

        /// <summary>
        /// 修改操作员
        /// </summary>
        /// <param name="currentOperator">要修改的操作员实体</param>
        /// <returns>True:成功/False:失败</returns>
        bool ModifyOperator(Operator currentOperator);

        /// <summary>
        /// 根据操作员名称校验操作员是否存在
        /// </summary>
        /// <param name="operatorName">操作员名称</param>
        /// <returns>True:存在/Flase:不存在</returns>
        bool CheckOperatorExist(string operatorName);
    }
}
