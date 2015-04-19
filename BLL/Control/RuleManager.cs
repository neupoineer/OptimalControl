using System.Collections.Generic;
using IBLL.Control;
using IDAL.Control;
using Model.Control;

namespace BLL.Control
{

    /// <summary>
    /// 规则数据访问操作接口
    /// </summary>
    public class RuleManager : IRuleManager
    {
        /// <summary>
        /// 根据规则ID获取规则实体
        /// </summary>
        /// <param name="id">规则ID</param>
        /// <returns>
        /// 规则实体
        /// </returns>
        public Rule GetRuleInfoById(int id)
        {
            DALFactory.AbstractDALFactory absDALFactory = DALFactory.AbstractDALFactory.Instance();
            //调用工厂方法生成实例
            IRuleService ruleService = absDALFactory.BuildRuleService();
            //调用实例方法
            return ruleService.GetRuleInfoById(id);
        }

        /// <summary>
        /// 获取所有规则信息
        /// </summary>
        /// <returns>规则实体集合</returns>
        public List<Rule> GetAllRuleInfo()
        {
            DALFactory.AbstractDALFactory absDALFactory = DALFactory.AbstractDALFactory.Instance();
            //调用工厂方法生成实例
            IRuleService ruleService = absDALFactory.BuildRuleService();
            //调用实例方法
            return ruleService.GetAllRuleInfo();
        }

        /// <summary>
        /// 获取有效的规则实体
        /// </summary>
        /// <returns>规则实体</returns>
        public List<Rule> GetRuleInfoEnabled()
        {
            DALFactory.AbstractDALFactory absDALFactory = DALFactory.AbstractDALFactory.Instance();
            //调用工厂方法生成实例
            IRuleService ruleService = absDALFactory.BuildRuleService();
            //调用实例方法
            return ruleService.GetRuleInfoEnabled();
        }

        /// <summary>
        /// 添加规则
        /// </summary>
        /// <param name="addRule">要添加的规则实体</param>
        /// <returns>True:成功/False:失败</returns>
        public bool AddRule(Rule addRule)
        {
            DALFactory.AbstractDALFactory absDALFactory = DALFactory.AbstractDALFactory.Instance();
            //调用工厂方法生成实例
            IRuleService ruleService = absDALFactory.BuildRuleService();
            //调用实例方法
            return ruleService.AddRule(addRule);
        }

        /// <summary>
        /// 删除规则
        /// </summary>
        /// <param name="id">要删除的规则 ID</param>
        /// <returns>True:成功/False:失败</returns>
        public bool DeleteRuleById(int id)
        {
            DALFactory.AbstractDALFactory absDALFactory = DALFactory.AbstractDALFactory.Instance();
            //调用工厂方法生成实例
            IRuleService ruleService = absDALFactory.BuildRuleService();
            //调用实例方法
            return ruleService.DeleteRuleById(id);
        }

        /// <summary>
        /// 修改规则
        /// </summary>
        /// <param name="currentRule">要修改的规则实体</param>
        /// <returns>True:成功/False:失败</returns>
        public bool ModifyRule(Rule currentRule)
        {
            DALFactory.AbstractDALFactory absDALFactory = DALFactory.AbstractDALFactory.Instance();
            //调用工厂方法生成实例
            IRuleService ruleService = absDALFactory.BuildRuleService();
            //调用实例方法
            return ruleService.ModifyRule(currentRule);
        }

        /// <summary>
        /// 根据规则名称校验规则是否存在
        /// </summary>
        /// <param name="ruleName">规则名称</param>
        /// <returns>True:存在/Flase:不存在</returns>
        public bool CheckRuleExist(string ruleName)
        {
            DALFactory.AbstractDALFactory absDALFactory = DALFactory.AbstractDALFactory.Instance();
            //调用工厂方法生成实例
            IRuleService ruleService = absDALFactory.BuildRuleService();
            //调用实例方法
            return ruleService.CheckRuleExist(ruleName);

        }
    }
}
