using System.Collections.Generic;
using Model.Control;

namespace IBLL.Control
{
    /// <summary>
    /// 曲线数据访问操作接口
    /// </summary>
    public interface ICurveManager
    {
        /// <summary>
        /// 根据曲线ID获取曲线实体
        /// </summary>
        /// <param name="id">曲线ID</param>
        /// <returns>
        /// 曲线实体
        /// </returns>
        Curve GetCurveInfoById(int id);

        /// <summary>
        /// 获取所有曲线信息
        /// </summary>
        /// <returns>曲线实体集合</returns>
        List<Curve> GetAllCurveInfo();

        /// <summary>
        /// 添加曲线
        /// </summary>
        /// <param name="addCurve">要添加的曲线实体</param>
        /// <returns>True:成功/False:失败</returns>
        bool AddCurve(Curve addCurve);

        /// <summary>
        /// 删除曲线
        /// </summary>
        /// <param name="id">要删除的曲线 ID</param>
        /// <returns>True:成功/False:失败</returns>
        bool DeleteCurveById(int id);

        /// <summary>
        /// 修改曲线
        /// </summary>
        /// <param name="currentCurve">要修改的曲线实体</param>
        /// <returns>True:成功/False:失败</returns>
        bool ModifyCurve(Curve currentCurve);

        /// <summary>
        /// 根据曲线名称校验曲线是否存在
        /// </summary>
        /// <param name="curveName">曲线名称</param>
        /// <returns>True:存在/Flase:不存在</returns>
        bool CheckCurveExist(string curveName);
    }
}
