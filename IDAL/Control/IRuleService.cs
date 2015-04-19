using System;
using System.Collections.Generic;
using System.Text;
using Model.Control;

namespace IDAL.Control
{
    /// <summary>
    /// 规则数据访问操作接口
    /// </summary>
    public interface IRuleService
    {
        /// <summary>
        /// 根据规则ID获取规则实体
        /// </summary>
        /// <param name="id">规则ID</param>
        /// <returns>
        /// 规则实体
        /// </returns>
        Rule GetRuleInfoById(int id);

        /// <summary>
        /// 获取有效的规则实体
        /// </summary>
        /// <returns>规则实体</returns>
        List<Rule> GetRuleInfoEnabled();

        /// <summary>
        /// 获取所有规则信息
        /// </summary>
        /// <returns>规则实体集合</returns>
        List<Rule> GetAllRuleInfo();

        /// <summary>
        /// 添加规则
        /// </summary>
        /// <param name="addRule">要添加的规则实体</param>
        /// <returns>True:成功/False:失败</returns>
        bool AddRule(Rule addRule);

        /// <summary>
        /// 删除规则
        /// </summary>
        /// <param name="id">要删除的规则 ID</param>
        /// <returns>True:成功/False:失败</returns>
        bool DeleteRuleById(int id);

        /// <summary>
        /// 修改规则
        /// </summary>
        /// <param name="currentRule">要修改的规则实体</param>
        /// <returns>True:成功/False:失败</returns>
        bool ModifyRule(Rule currentRule);

        /// <summary>
        /// 根据规则名称校验规则是否存在
        /// </summary>
        /// <param name="ruleName">规则名称</param>
        /// <returns>True:存在/Flase:不存在</returns>
        bool CheckRuleExist(string ruleName);
    }
}