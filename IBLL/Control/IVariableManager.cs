using System.Collections.Generic;
using Model.Control;

namespace IBLL.Control
{
    /// <summary>
    /// 变量数据访问操作接口
    /// </summary>
    public interface IVariableManager
    {
        /// <summary>
        /// 根据变量ID获取变量实体
        /// </summary>
        /// <param name="id">变量ID</param>
        /// <returns>
        /// 变量实体
        /// </returns>
        Variable GetVariableInfoById(int id);

        /// <summary>
        /// 根据变量ID获取变量实体
        /// </summary>
        /// <param name="name">变量名</param>
        /// <returns>
        /// 变量实体
        /// </returns>
        Variable GetVariableInfoByName(string name);

        /// <summary>
        /// 获取所有变量信息
        /// </summary>
        /// <returns>变量实体集合</returns>
        List<Variable> GetAllVariableInfo();

        /// <summary>
        /// 获取Device的所有变量信息
        /// </summary>
        /// <returns>变量实体集合</returns>
        List<Variable> GetVariableByDeviceId(int deviceId);

        /// <summary>
        /// 添加变量
        /// </summary>
        /// <param name="addVariable">要添加的变量实体</param>
        /// <returns>True:成功/False:失败</returns>
        bool AddVariable(Variable addVariable);

        /// <summary>
        /// 删除变量
        /// </summary>
        /// <param name="id">要删除的变量 ID</param>
        /// <returns>True:成功/False:失败</returns>
        bool DeleteVariableById(int id);

        /// <summary>
        /// 修改变量
        /// </summary>
        /// <param name="currentVariable">要修改的变量实体</param>
        /// <returns>True:成功/False:失败</returns>
        bool ModifyVariable(Variable currentVariable);

        /// <summary>
        /// 根据变量名称校验变量是否存在
        /// </summary>
        /// <param name="variableName">变量名称</param>
        /// <returns>True:存在/Flase:不存在</returns>
        bool CheckVariableExist(string variableName);
    }
}
