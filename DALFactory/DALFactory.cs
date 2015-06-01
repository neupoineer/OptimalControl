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

        /// <summary>
        /// �������ݷ��ʲ��������������
        /// </summary>
        /// <returns>�������ݷ��ʲ�����ʵ��</returns>
        public override ICurveService BuildCurveService()
        {
            return new CurveService();
        }

        /// <summary>
        /// �豸���ݷ��ʲ��������������
        /// </summary>
        /// <returns>�豸���ݷ��ʲ�����ʵ��</returns>
        public override IDeviceService BuildDeviceService()
        {
            return new DeviceService();
        }

        /// <summary>
        /// �������ݷ��ʲ��������������
        /// </summary>
        /// <returns>�������ݷ��ʲ�����ʵ��</returns>
        public override IRuleService BuildRuleService()
        {
            return new RuleService();
        }

        /// <summary>
        /// �������ݷ��ʲ��������������
        /// </summary>
        /// <returns>�������ݷ��ʲ�����ʵ��</returns>
        public override IVariableService BuildVariableService()
        {
            return new VariableService();
        }

        /// <summary>
        /// ��־���ݷ��ʲ��������������
        /// </summary>
        /// <returns>��־���ݷ��ʲ�����ʵ��</returns>
        public override ILogService BuildLogService()
        {
            return new LogService();
        }

        /// <summary>
        /// ���ݷ��ʲ��������������
        /// </summary>
        /// <returns>���ݷ��ʲ�����ʵ��</returns>
        public override IDataService BuildDataService()
        {
            return new DataService();
        }
    }
}
