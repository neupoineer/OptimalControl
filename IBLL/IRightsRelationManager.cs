using System.Collections.Generic;
using Model;

namespace IBLL
{
    /// <summary>
    /// 权限关系业务逻辑管理接口
    /// </summary>
    public interface IRightsRelationManager
    {
        /// <summary>
        /// 添加单个权限关系
        /// </summary>
        /// <param name="rightsRelation">权限关系实体</param>
        /// <returns>True:成功/False:失败</returns>
        bool AddRightsRelation(RightsRelation rightsRelation);

        /// <summary>
        /// 根据权限关系 ID 删除权限关系
        /// </summary>
        /// <param name="id">权限关系 ID</param>
        /// <returns>True:成功/False:失败</returns>
        bool DeleteRightsRelationById(int id);

        /// <summary>
        /// 根据操作员 ID 删除对应的所有权限关系
        /// </summary>
        /// <param name="operatorId">操作员 ID</param>
        /// <returns>True:成功/False:失败</returns>
        bool DeleteRightsRelationByOperatorId(int operatorId);

        /// <summary>
        /// 修改单个权限关系
        /// </summary>
        /// <param name="rightsRelation">权限关系实体</param>
        /// <returns>True:成功/False:失败</returns>
        bool ModifyRightsRelation(RightsRelation rightsRelation);

        /// <summary>
        /// 获取所有的权限关系集合
        /// </summary>
        /// <returns>权限关系集合</returns>
        List<RightsRelation> GetAllRightsRelation();

        /// <summary>
        /// 根据操作员 ID 获取对应的所有权限关系
        /// </summary>
        /// <param name="id">操作员 ID</param>
        /// <returns>权限关系集合</returns>
        List<RightsRelation> GetRightsRelationByOperatorId(int id);

        /// <summary>
        /// 根据权限组 ID 获取与此权限组相关的权限关系数量
        /// </summary>
        /// <param name="id">权限组 ID</param>
        /// <returns>权限关系数量</returns>
        int GetRightsRelationCountByRightsGroupId(int id);
    }
}
