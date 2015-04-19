using System.Collections.Generic;
using IBLL.Control;
using IDAL.Control;
using Model.Control;

namespace BLL.Control
{

    /// <summary>
    /// 曲线数据访问操作接口
    /// </summary>
    public class CurveManager:ICurveManager
    {
        /// <summary>
        /// 根据曲线ID获取曲线实体
        /// </summary>
        /// <param name="id">曲线ID</param>
        /// <returns>
        /// 曲线实体
        /// </returns>
        public Curve GetCurveInfoById(int id)
        {
            DALFactory.AbstractDALFactory absDALFactory = DALFactory.AbstractDALFactory.Instance();
            //调用工厂方法生成实例
            ICurveService curveService = absDALFactory.BuildCurveService();
            //调用实例方法
            return curveService.GetCurveInfoById(id);
        }

        /// <summary>
        /// 获取所有曲线信息
        /// </summary>
        /// <returns>曲线实体集合</returns>
        public List<Curve> GetAllCurveInfo()
        {
            DALFactory.AbstractDALFactory absDALFactory = DALFactory.AbstractDALFactory.Instance();
            //调用工厂方法生成实例
            ICurveService curveService = absDALFactory.BuildCurveService();
            //调用实例方法
            return curveService.GetAllCurveInfo();
        }

        /// <summary>
        /// 添加曲线
        /// </summary>
        /// <param name="addCurve">要添加的曲线实体</param>
        /// <returns>True:成功/False:失败</returns>
        public bool AddCurve(Curve addCurve)
        {
            DALFactory.AbstractDALFactory absDALFactory = DALFactory.AbstractDALFactory.Instance();
            //调用工厂方法生成实例
            ICurveService curveService = absDALFactory.BuildCurveService();
            //调用实例方法
            return curveService.AddCurve(addCurve);
        }

        /// <summary>
        /// 删除曲线
        /// </summary>
        /// <param name="id">要删除的曲线 ID</param>
        /// <returns>True:成功/False:失败</returns>
        public bool DeleteCurveById(int id)
        {
            DALFactory.AbstractDALFactory absDALFactory = DALFactory.AbstractDALFactory.Instance();
            //调用工厂方法生成实例
            ICurveService curveService = absDALFactory.BuildCurveService();
            //调用实例方法
            return curveService.DeleteCurveById(id);
        }

        /// <summary>
        /// 修改曲线
        /// </summary>
        /// <param name="currentCurve">要修改的曲线实体</param>
        /// <returns>True:成功/False:失败</returns>
        public bool ModifyCurve(Curve currentCurve)
        {
            DALFactory.AbstractDALFactory absDALFactory = DALFactory.AbstractDALFactory.Instance();
            //调用工厂方法生成实例
            ICurveService curveService = absDALFactory.BuildCurveService();
            //调用实例方法
            return curveService.ModifyCurve(currentCurve);
        }

        /// <summary>
        /// 根据曲线名称校验曲线是否存在
        /// </summary>
        /// <param name="curveName">曲线名称</param>
        /// <returns>True:存在/Flase:不存在</returns>
        public bool CheckCurveExist(string curveName)
        {
            DALFactory.AbstractDALFactory absDALFactory = DALFactory.AbstractDALFactory.Instance();
            //调用工厂方法生成实例
            ICurveService curveService = absDALFactory.BuildCurveService();
            //调用实例方法
            return curveService.CheckCurveExist(curveName);

        }
    }
}
