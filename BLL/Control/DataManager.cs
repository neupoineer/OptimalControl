using System;
using System.Collections.Generic;
using System.Data;
using IBLL.Control;
using IDAL.Control;
using Model.Control;

namespace BLL.Control
{
    /// <summary>
    /// 数据访问操作接口
    /// </summary>
    public class DataManager : IDataManager
    {
        /// <summary>
        /// 根据变量名和设备ID获取数据
        /// </summary>
        /// <param name="variableCode">变量名</param>
        /// <param name="deviceID">设备ID</param>
        /// <param name="starTime">起始时间</param>
        /// <param name="endTime">截止时间</param>
        /// <returns>
        /// 数据
        /// </returns>
        public List<Data> GetDataByVariableCode(string variableCode, int deviceID, DateTime starTime, DateTime endTime)
        {
            DALFactory.AbstractDALFactory absDALFactory = DALFactory.AbstractDALFactory.Instance();
            //调用工厂方法生成实例
            IDataService dataService = absDALFactory.BuildDataService();
            //调用实例方法
            return dataService.GetDataByVariableCode(variableCode, deviceID, starTime, endTime);
        }

        /// <summary>
        /// 根据变量编码和设备ID获取最后的数据
        /// </summary>
        /// <param name="variableCode">变量编码</param>
        /// <param name="deviceID">设备ID</param>
        /// <returns>数据</returns>
        public Data GetLastDataByVariableCode(string variableCode, int deviceID)
        {
            DALFactory.AbstractDALFactory absDALFactory = DALFactory.AbstractDALFactory.Instance();
            //调用工厂方法生成实例
            IDataService dataService = absDALFactory.BuildDataService();
            //调用实例方法
            return dataService.GetLastDataByVariableCode(variableCode, deviceID);
        }

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="dataCollection">要添加的数据实体</param>
        /// <returns>True:成功/False:失败</returns>
        public bool AddData(List<Data> dataCollection)
        {
            DALFactory.AbstractDALFactory absDALFactory = DALFactory.AbstractDALFactory.Instance();
            //调用工厂方法生成实例
            IDataService dataService = absDALFactory.BuildDataService();
            //调用实例方法
            return dataService.AddData(dataCollection);
        }

        /// <summary>
        /// 获得所选时间内的所有数据.
        /// </summary>
        /// <param name="starTime">起始时间</param>
        /// <param name="endTime">截止时间</param>
        /// <returns>数据表DataTable</returns>
        public DataTable GetAllDataInfoByTime(DateTime starTime, DateTime endTime)
        {
            DALFactory.AbstractDALFactory absDALFactory = DALFactory.AbstractDALFactory.Instance();
            //调用工厂方法生成实例
            IDataService dataService = absDALFactory.BuildDataService();
            //调用实例方法
            return dataService.GetAllDataInfoByTime(starTime, endTime);
        }

        /// <summary>
        /// 统计投用率
        /// </summary>
        /// <param name="variableCode">使能变量名</param>
        /// <param name="startTime">起始时间</param>
        /// <param name="endTime">截止时间</param>
        /// <returns></returns>
        public double GetUseRateByTime(string variableCode, DateTime startTime, DateTime endTime)
        {
            DALFactory.AbstractDALFactory absDALFactory = DALFactory.AbstractDALFactory.Instance();
            //调用工厂方法生成实例
            IDataService dataService = absDALFactory.BuildDataService();
            //调用实例方法
            return dataService.GetUseRateByTime(variableCode, startTime, endTime);
        }
    }
}
