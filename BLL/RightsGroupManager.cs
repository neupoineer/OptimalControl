using System;
using System.Collections.Generic;
using System.Text;
using Model.Rights;

namespace BLL
{
    /// <summary>
    /// 权限组数据访问操作类
    /// </summary>
    public class RightsGroupManager : IBLL.IRightsGroupManager
    {
        #region IRightsGroupManager 成员

        /// <summary>
        /// 获取所有权限组信息
        /// </summary>
        /// <returns>权限组实体集合</returns>
        public Dictionary<string, RightsGroup> GetAllRightsGroupInfo()
        {
            //定义并实例化抽象工厂类
            DALFactory.AbstractDALFactory absDALFactory = DALFactory.AbstractDALFactory.Instance();
            //调用工厂方法生成实例
            IDAL.IRightsGroupService rightsGroupService = absDALFactory.BuildRightsGroupService();
            //调用实例方法
            return rightsGroupService.GetAllRightsGroupInfo();
        }

        /// <summary>
        /// 添加权限组
        /// </summary>
        /// <param name="addRightsGroup">要添加的权限组实体</param>
        /// <returns>True:成功/False:失败</returns>
        public bool AddRightsGroup(RightsGroup addRightsGroup)
        {
            //定义并实例化抽象工厂类
            DALFactory.AbstractDALFactory absDALFactory = DALFactory.AbstractDALFactory.Instance();
            //调用工厂方法生成实例
            IDAL.IRightsGroupService rightsGroupService = absDALFactory.BuildRightsGroupService();
            //调用实例方法
            return rightsGroupService.AddRightsGroup(addRightsGroup);
        }

        /// <summary>
        /// 删除权限组
        /// </summary>
        /// <param name="id">要删除的权限组 ID</param>
        /// <returns>True:成功/False:失败</returns>
        public bool DeleteRightsGroupByID(int id)
        {
            //定义并实例化抽象工厂类
            DALFactory.AbstractDALFactory absDALFactory = DALFactory.AbstractDALFactory.Instance();
            //调用工厂方法生成实例
            IDAL.IRightsGroupService rightsGroupService = absDALFactory.BuildRightsGroupService();
            //调用实例方法
            return rightsGroupService.DeleteRightsGroupByID(id);
        }

        /// <summary>
        /// 修改权限组
        /// </summary>
        /// <param name="currentRightsGroup">要修改的权限组实体</param>
        /// <returns>True:成功/False:失败</returns>
        public bool ModifyRightsGroup(RightsGroup currentRightsGroup)
        {
            //定义并实例化抽象工厂类
            DALFactory.AbstractDALFactory absDALFactory = DALFactory.AbstractDALFactory.Instance();
            //调用工厂方法生成实例
            IDAL.IRightsGroupService rightsGroupService = absDALFactory.BuildRightsGroupService();
            //调用实例方法
            return rightsGroupService.ModifyRightsGroup(currentRightsGroup);
        }

        /// <summary>
        /// 根据权限组名称校验权限组是否已经存在
        /// </summary>
        /// <param name="rightsGroupName">权限组名称</param>
        /// <returns>True:存在/False:不存在</returns>
        public bool CheckRightsGroupExist(string rightsGroupName)
        {
            //定义并实例化抽象工厂类
            DALFactory.AbstractDALFactory absDALFactory = DALFactory.AbstractDALFactory.Instance();
            //调用工厂方法生成实例
            IDAL.IRightsGroupService rightsGroupService = absDALFactory.BuildRightsGroupService();
            //调用实例方法
            return rightsGroupService.CheckRightsGroupExist(rightsGroupName);
        }

        #endregion
    }
}
