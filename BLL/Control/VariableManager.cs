using System.Collections.Generic;
using IBLL.Control;
using IDAL.Control;
using Model.Control;

namespace BLL.Control
{

    /// <summary>
    /// 变量数据访问操作接口
    /// </summary>
    public class VariableManager : IVariableManager
    {
        /// <summary>
        /// 根据变量ID获取变量实体
        /// </summary>
        /// <param name="id">变量ID</param>
        /// <returns>
        /// 变量实体
        /// </returns>
        public Variable GetVariableInfoById(int id)
        {
            DALFactory.AbstractDALFactory absDALFactory = DALFactory.AbstractDALFactory.Instance();
            //调用工厂方法生成实例
            IVariableService variableService = absDALFactory.BuildVariableService();
            //调用实例方法
            return variableService.GetVariableInfoById(id);
        }

        /// <summary>
        /// 根据变量名获取变量实体
        /// </summary>
        /// <param name="name">变量名</param>
        /// <returns>变量实体</returns>
        public Variable GetVariableInfoByName(string name)
        {
            DALFactory.AbstractDALFactory absDALFactory = DALFactory.AbstractDALFactory.Instance();
            //调用工厂方法生成实例
            IVariableService variableService = absDALFactory.BuildVariableService();
            //调用实例方法
            return variableService.GetVariableInfoByName(name);

        }

        /// <summary>
        /// 获取所有变量信息
        /// </summary>
        /// <returns>变量实体集合</returns>
        public List<Variable> GetAllVariableInfo()
        {
            DALFactory.AbstractDALFactory absDALFactory = DALFactory.AbstractDALFactory.Instance();
            //调用工厂方法生成实例
            IVariableService variableService = absDALFactory.BuildVariableService();
            //调用实例方法
            return variableService.GetAllVariableInfo();
        }

        /// <summary>
        /// 获取Device的所有变量信息
        /// </summary>
        /// <returns>变量实体集合</returns>
        public List<Variable> GetVariableByDeviceId(int deviceId)
        {
            DALFactory.AbstractDALFactory absDALFactory = DALFactory.AbstractDALFactory.Instance();
            //调用工厂方法生成实例
            IVariableService variableService = absDALFactory.BuildVariableService();
            //调用实例方法
            return variableService.GetVariableByDeviceId(deviceId);
        }

        /// <summary>
        /// 添加变量
        /// </summary>
        /// <param name="addVariable">要添加的变量实体</param>
        /// <returns>True:成功/False:失败</returns>
        public bool AddVariable(Variable addVariable)
        {
            DALFactory.AbstractDALFactory absDALFactory = DALFactory.AbstractDALFactory.Instance();
            //调用工厂方法生成实例
            IVariableService variableService = absDALFactory.BuildVariableService();
            //调用实例方法
            return variableService.AddVariable(addVariable);
        }

        /// <summary>
        /// 删除变量
        /// </summary>
        /// <param name="id">要删除的变量 ID</param>
        /// <returns>True:成功/False:失败</returns>
        public bool DeleteVariableById(int id)
        {
            DALFactory.AbstractDALFactory absDALFactory = DALFactory.AbstractDALFactory.Instance();
            //调用工厂方法生成实例
            IVariableService variableService = absDALFactory.BuildVariableService();
            //调用实例方法
            return variableService.DeleteVariableById(id);
        }

        /// <summary>
        /// 按照设备号删除变量
        /// </summary>
        /// <param name="deviceID">要删除的变量的设备号</param>
        /// <returns>True:成功/False:失败</returns>
        public bool DeleteVariableByDeviceId(int deviceID)
        {
            DALFactory.AbstractDALFactory absDALFactory = DALFactory.AbstractDALFactory.Instance();
            //调用工厂方法生成实例
            IVariableService variableService = absDALFactory.BuildVariableService();
            //调用实例方法
            return variableService.DeleteVariableByDeviceId(deviceID);
        }

        /// <summary>
        /// 修改变量
        /// </summary>
        /// <param name="currentVariable">要修改的变量实体</param>
        /// <returns>True:成功/False:失败</returns>
        public bool ModifyVariable(Variable currentVariable)
        {
            DALFactory.AbstractDALFactory absDALFactory = DALFactory.AbstractDALFactory.Instance();
            //调用工厂方法生成实例
            IVariableService variableService = absDALFactory.BuildVariableService();
            //调用实例方法
            return variableService.ModifyVariable(currentVariable);
        }

        /// <summary>
        /// 根据变量名称校验变量是否存在
        /// </summary>
        /// <param name="variableName">变量名称</param>
        /// <returns>True:存在/Flase:不存在</returns>
        public bool CheckVariableExist(string variableName)
        {
            DALFactory.AbstractDALFactory absDALFactory = DALFactory.AbstractDALFactory.Instance();
            //调用工厂方法生成实例
            IVariableService variableService = absDALFactory.BuildVariableService();
            //调用实例方法
            return variableService.CheckVariableExist(variableName);

        }
    }
}
