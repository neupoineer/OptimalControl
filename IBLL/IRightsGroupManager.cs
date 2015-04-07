using System;
using System.Collections.Generic;
using System.Text;

namespace IBLL
{
    /// <summary>
    /// 权限组业务逻辑管理接口
    /// </summary>
    public interface IRightsGroupManager
    {
        /// <summary>
        /// 获取所有权限组信息
        /// </summary>
        /// <returns>权限组实体集合</returns>
        Dictionary<string, Model.RightsGroup> GetAllRightsGroupInfo();

        /// <summary>
        /// 根据权限组名称校验权限组是否已经存在
        /// </summary>
        /// <param name="rightsGroupName">权限组名称</param>
        /// <returns>True:存在/False:不存在</returns>
        bool CheckRightsGroupExist(string rightsGroupName);

        /// <summary>
        /// 添加权限组
        /// </summary>
        /// <param name="addRightsGroup">要添加的权限组实体</param>
        /// <returns>True:成功/False:失败</returns>
        bool AddRightsGroup(Model.RightsGroup addRightsGroup);

        /// <summary>
        /// 删除权限组
        /// </summary>
        /// <param name="id">要删除的权限组 ID</param>
        /// <returns>True:成功/False:失败</returns>
        bool DeleteRightsGroupByID(int id);

        /// <summary>
        /// 修改权限组
        /// </summary>
        /// <param name="currentRightsGroup">要修改的权限组实体</param>
        /// <returns>True:成功/False:失败</returns>
        bool ModifyRightsGroup(Model.RightsGroup currentRightsGroup);
    }
}
