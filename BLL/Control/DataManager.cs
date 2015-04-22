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
        /// <param name="variableName">变量名</param>
        /// <param name="deviceID">设备ID</param>
        /// <param name="starTime">起始时间</param>
        /// <param name="endTime">截止时间</param>
        /// <returns>
        /// 数据
        /// </returns>
        public List<Data> GetDataByVariableName(string variableName, int deviceID, DateTime starTime, DateTime endTime)
        {
            DALFactory.AbstractDALFactory absDALFactory = DALFactory.AbstractDALFactory.Instance();
            //调用工厂方法生成实例
            IDataService dataService = absDALFactory.BuildDataService();
            //调用实例方法
            return dataService.GetDataByVariableName(variableName, deviceID, starTime, endTime);
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
    }
}
