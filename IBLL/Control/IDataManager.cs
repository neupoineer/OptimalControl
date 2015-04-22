using System;
using System.Collections.Generic;
using System.Data;
using Model.Control;

namespace IBLL.Control
{
    /// <summary>
    /// 数据访问操作接口
    /// </summary>
    public interface IDataManager
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
        List<Data> GetDataByVariableName(string variableName, int deviceID, DateTime starTime, DateTime endTime);

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="dataCollection">要添加的数据实体</param>
        /// <returns>True:成功/False:失败</returns>
        bool AddData(List<Data> dataCollection);

        /// <summary>
        /// 获得所选时间内的所有数据.
        /// </summary>
        /// <param name="starTime">起始时间</param>
        /// <param name="endTime">截止时间</param>
        /// <returns>数据表DataTable</returns>
        DataTable GetAllDataInfoByTime(DateTime starTime, DateTime endTime);

    }
}
