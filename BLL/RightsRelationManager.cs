using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 权限关系数据访问操作类
    /// </summary>
    public class RightsRelationManager : IBLL.IRightsRelationManager
    {
        #region IRightsRelationManager 成员

        /// <summary>
        /// 添加单个权限关系
        /// </summary>
        /// <param name="rightsRelation">权限关系实体</param>
        /// <returns>True:成功/False:失败</returns>
        public bool AddRightsRelation(Model.RightsRelation rightsRelation)
        {
            //定义并实例化抽象工厂类
            DALFactory.AbstractDALFactory absDALFactory = DALFactory.AbstractDALFactory.Instance();
            //调用工厂方法生成实例
            IDAL.IRightsRelationService rightsRelationService = absDALFactory.BuildRightsRelationService();
            //调用实例方法
            return rightsRelationService.AddRightsRelation(rightsRelation);
        }

        /// <summary>
        /// 根据权限关系 ID 删除权限关系
        /// </summary>
        /// <param name="id">权限关系 ID</param>
        /// <returns>True:成功/False:失败</returns>
        public bool DeleteRightsRelationById(int id)
        {
            //定义并实例化抽象工厂类
            DALFactory.AbstractDALFactory absDALFactory = DALFactory.AbstractDALFactory.Instance();
            //调用工厂方法生成实例
            IDAL.IRightsRelationService rightsRelationService = absDALFactory.BuildRightsRelationService();
            //调用实例方法
            return rightsRelationService.DeleteRightsRelationById(id);
        }

        /// <summary>
        /// 根据操作员 ID 删除对应的所有权限关系
        /// </summary>
        /// <param name="operatorId">操作员 ID</param>
        /// <returns>True:成功/False:失败</returns>
        public bool DeleteRightsRelationByOperatorId(int operatorId)
        {
            //定义并实例化抽象工厂类
            DALFactory.AbstractDALFactory absDALFactory = DALFactory.AbstractDALFactory.Instance();
            //调用工厂方法生成实例
            IDAL.IRightsRelationService rightsRelationService = absDALFactory.BuildRightsRelationService();
            //调用实例方法
            return rightsRelationService.DeleteRightsRelationByOperatorId(operatorId);
        }

        /// <summary>
        /// 修改单个权限关系
        /// </summary>
        /// <param name="rightsRelation">权限关系实体</param>
        /// <returns>True:成功/False:失败</returns>
        public bool ModifyRightsRelation(Model.RightsRelation rightsRelation)
        {
            //定义并实例化抽象工厂类
            DALFactory.AbstractDALFactory absDALFactory = DALFactory.AbstractDALFactory.Instance();
            //调用工厂方法生成实例
            IDAL.IRightsRelationService rightsRelationService = absDALFactory.BuildRightsRelationService();
            //调用实例方法
            return rightsRelationService.ModifyRightsRelation(rightsRelation);
        }

        /// <summary>
        /// 获取所有的权限关系集合
        /// </summary>
        /// <returns>权限关系集合</returns>
        public List<Model.RightsRelation> GetAllRightsRelation()
        {
            //定义并实例化抽象工厂类
            DALFactory.AbstractDALFactory absDALFactory = DALFactory.AbstractDALFactory.Instance();
            //调用工厂方法生成实例
            IDAL.IRightsRelationService rightsRelationService = absDALFactory.BuildRightsRelationService();
            //调用实例方法
            return rightsRelationService.GetAllRightsRelation();
        }

        /// <summary>
        /// 根据操作员 ID 获取对应的所有权限关系
        /// </summary>
        /// <param name="id">操作员 ID</param>
        /// <returns>权限关系集合</returns>
        public List<Model.RightsRelation> GetRightsRelationByOperatorId(int id)
        {
            //定义并实例化抽象工厂类
            DALFactory.AbstractDALFactory absDALFactory = DALFactory.AbstractDALFactory.Instance();
            //调用工厂方法生成实例
            IDAL.IRightsRelationService rightsRelationService = absDALFactory.BuildRightsRelationService();
            //调用实例方法
            return rightsRelationService.GetRightsRelationByOperatorId(id);
        }

        /// <summary>
        /// 根据权限组 ID 获取与此权限组相关的权限关系数量
        /// </summary>
        /// <param name="id">权限组 ID</param>
        /// <returns>权限关系数量</returns>
        public int GetRightsRelationCountByRightsGroupId(int id)
        {
            //定义并实例化抽象工厂类
            DALFactory.AbstractDALFactory absDALFactory = DALFactory.AbstractDALFactory.Instance();
            //调用工厂方法生成实例
            IDAL.IRightsRelationService rightsRelationService = absDALFactory.BuildRightsRelationService();
            //调用实例方法
            return rightsRelationService.GetRightsRelationCountByRightsGroupId(id);
        }

        #endregion
    }
}
