using System;
using System.Collections.Generic;
using System.Text;
using DAL;
using IDAL;
using DAL.Control;
using IDAL.Control;

namespace DALFactory
{
    /// <summary>
    /// ���ݷ��ʲ㹤����
    /// </summary>
    public class DALFactory : AbstractDALFactory
    {
        /// <summary>
        /// ����Ա���ݷ��ʲ��������������
        /// </summary>
        /// <returns>����Ա���ݷ��ʲ�����ʵ��</returns>
        public override IOperatorService BuildOperatorService()
        {
            return new OperatorService();
        }

        /// <summary>
        /// Ȩ�������ݷ��ʲ��������������
        /// </summary>
        /// <returns>Ȩ�������ݷ��ʲ�����ʵ��</returns>
        public override IRightsGroupService BuildRightsGroupService()
        {
            return new RightsGroupService();
        }

        /// <summary>
        /// Ȩ�޹�ϵ���ݷ��ʲ��������������
        /// </summary>
        /// <returns>Ȩ�޹�ϵ���ݷ��ʲ�����ʵ��</returns>
        public override IRightsRelationService BuildRightsRelationService()
        {
            return new RightsRelationService();
        }
        public override ICurveService BuildCurveService()
        {
            return new CurveService();
        }
        public override IDeviceService BuildDeviceService()
        {
            return new DeviceService();
        }
        public override IRuleService BuildRuleService()
        {
            return new RuleService();
        }
        public override IVariableService BuildVariableService()
        {
            return new VariableService();
        }
        public override ILogService BuildLogService()
        {
            return new LogService();
        }
        public override IDataService BuildDataService()
        {
            return new DataService();
        }
    }
}
