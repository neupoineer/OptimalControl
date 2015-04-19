using System;
using System.Collections.Generic;
using System.Text;
using Model.Modbus;

namespace IDAL.Control
{
    /// <summary>
    /// 设备数据访问操作接口
    /// </summary>
    public interface IDeviceService
    {
        /// <summary>
        /// 根据设备ID获取设备实体
        /// </summary>
        /// <param name="id">设备ID</param>
        /// <returns>
        /// 设备实体
        /// </returns>
        Device GetDeviceInfoById(int id);

        /// <summary>
        /// 获取所有设备信息
        /// </summary>
        /// <returns>设备实体集合</returns>
        List<Device> GetAllDeviceInfo();

        /// <summary>
        /// 添加设备
        /// </summary>
        /// <param name="addDevice">要添加的设备实体</param>
        /// <returns>True:成功/False:失败</returns>
        bool AddDevice(Device addDevice);

        /// <summary>
        /// 删除设备
        /// </summary>
        /// <param name="id">要删除的设备 ID</param>
        /// <returns>True:成功/False:失败</returns>
        bool DeleteDeviceById(int id);

        /// <summary>
        /// 修改设备
        /// </summary>
        /// <param name="currentDevice">要修改的设备实体</param>
        /// <returns>True:成功/False:失败</returns>
        bool ModifyDevice(Device currentDevice);

        /// <summary>
        /// 根据设备名称校验设备是否存在
        /// </summary>
        /// <param name="deviceName">设备名称</param>
        /// <returns>True:存在/Flase:不存在</returns>
        bool CheckDeviceExist(string deviceName);
    }
}
