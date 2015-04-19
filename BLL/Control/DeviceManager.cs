using System.Collections.Generic;
using IBLL.Control;
using IDAL.Control;
using Model.Modbus;

namespace BLL.Control
{

    /// <summary>
    /// 设备数据访问操作接口
    /// </summary>
    public class DeviceManager : IDeviceManager
    {
        /// <summary>
        /// 根据设备ID获取设备实体
        /// </summary>
        /// <param name="id">设备ID</param>
        /// <returns>
        /// 设备实体
        /// </returns>
        public Device GetDeviceInfoById(int id)
        {
            DALFactory.AbstractDALFactory absDALFactory = DALFactory.AbstractDALFactory.Instance();
            //调用工厂方法生成实例
            IDeviceService deviceService = absDALFactory.BuildDeviceService();
            //调用实例方法
            return deviceService.GetDeviceInfoById(id);
        }

        /// <summary>
        /// 获取所有设备信息
        /// </summary>
        /// <returns>设备实体集合</returns>
        public List<Device> GetAllDeviceInfo()
        {
            DALFactory.AbstractDALFactory absDALFactory = DALFactory.AbstractDALFactory.Instance();
            //调用工厂方法生成实例
            IDeviceService deviceService = absDALFactory.BuildDeviceService();
            //调用实例方法
            return deviceService.GetAllDeviceInfo();
        }

        /// <summary>
        /// 添加设备
        /// </summary>
        /// <param name="addDevice">要添加的设备实体</param>
        /// <returns>True:成功/False:失败</returns>
        public bool AddDevice(Device addDevice)
        {
            DALFactory.AbstractDALFactory absDALFactory = DALFactory.AbstractDALFactory.Instance();
            //调用工厂方法生成实例
            IDeviceService deviceService = absDALFactory.BuildDeviceService();
            //调用实例方法
            return deviceService.AddDevice(addDevice);
        }

        /// <summary>
        /// 删除设备
        /// </summary>
        /// <param name="id">要删除的设备 ID</param>
        /// <returns>True:成功/False:失败</returns>
        public bool DeleteDeviceById(int id)
        {
            DALFactory.AbstractDALFactory absDALFactory = DALFactory.AbstractDALFactory.Instance();
            //调用工厂方法生成实例
            IDeviceService deviceService = absDALFactory.BuildDeviceService();
            //调用实例方法
            return deviceService.DeleteDeviceById(id);
        }

        /// <summary>
        /// 修改设备
        /// </summary>
        /// <param name="currentDevice">要修改的设备实体</param>
        /// <returns>True:成功/False:失败</returns>
        public bool ModifyDevice(Device currentDevice)
        {
            DALFactory.AbstractDALFactory absDALFactory = DALFactory.AbstractDALFactory.Instance();
            //调用工厂方法生成实例
            IDeviceService deviceService = absDALFactory.BuildDeviceService();
            //调用实例方法
            return deviceService.ModifyDevice(currentDevice);
        }

        /// <summary>
        /// 根据设备名称校验设备是否存在
        /// </summary>
        /// <param name="deviceName">设备名称</param>
        /// <returns>True:存在/Flase:不存在</returns>
        public bool CheckDeviceExist(string deviceName)
        {
            DALFactory.AbstractDALFactory absDALFactory = DALFactory.AbstractDALFactory.Instance();
            //调用工厂方法生成实例
            IDeviceService deviceService = absDALFactory.BuildDeviceService();
            //调用实例方法
            return deviceService.CheckDeviceExist(deviceName);

        }
    }
}
