using System;
using System.Collections.Generic;
using System.Text;

namespace CodingMouse.CMHotelManager.IBLL
{
    /// <summary>
    /// 客户业务逻辑管理接口
    /// </summary>
    public interface IClientManager
    {
        /// <summary>
        /// 添加单个客户
        /// </summary>
        /// <param name="Client">客户实体</param>
        /// <returns>True:成功 / False:失败</returns>
        bool AddVIP(Model.Client Client);

        /// <summary>
        /// 根据客户ID删除单个客户
        /// </summary>
        /// <param name="id">客户ID</param>
        /// <returns>True:成功 / False:失败</returns>
        bool DeleteVIP(int id);

        /// <summary>
        /// 修改单个/批量客户
        /// </summary>
        /// <param name="ClientList">客户集合</param>
        /// <returns>True:成功 / False:失败</returns>
        bool Modify(List<Model.Client> ClientList);

        /// <summary>
        /// 根据选择列名和查询内容查询客户
        /// </summary>
        /// <param name="columnName">列名</param>
        /// <param name="content">查询内容</param>
        /// <returns>客户集合</returns>
        List<Model.Client> GetVIPByColumnAndContent(string columnName, string content);

        /// <summary>
        /// 获取所有客户
        /// </summary>
        /// <returns>客户集合</returns>
        List<Model.Client> GetAllVIP();
    }
}
