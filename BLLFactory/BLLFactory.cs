using BLL;
using BLL.Control;
using IBLL;
using IBLL.Control;

namespace BLLFactory
{
    /// <summary>
    /// ҵ���߼��㹤����
    /// </summary>
    public class BLLFactory
    {
        /// <summary>
        /// ����Աҵ���߼����������������
        /// </summary>
        /// <returns>����Աҵ���߼�������ʵ��</returns>
        public IOperatorManager BuildOperatorManager()
        {
            return new OperatorManager();
        }

        /// <summary>
        /// Ȩ����ҵ���߼����������������
        /// </summary>
        /// <returns>Ȩ����ҵ���߼�������ʵ��</returns>
        public IRightsGroupManager BuildRightsGroupManager()
        {
            return new RightsGroupManager();
        }

        /// <summary>
        /// Ȩ�޹�ϵҵ���߼����������������
        /// </summary>
        /// <returns>Ȩ�޹�ϵҵ���߼�������ʵ��</returns>
        public IRightsRelationManager BuildRightsRelationManager()
        {
            return new RightsRelationManager();
        }

        /// <summary>
        /// ����ҵ���߼����������������
        /// </summary>
        /// <returns>����ҵ���߼�������ʵ��</returns>
        public ICurveManager BuildCurveManager()
        {
            return new CurveManager();
        }

        /// <summary>
        /// �豸ҵ���߼����������������
        /// </summary>
        /// <returns>�豸ҵ���߼�������ʵ��</returns>
        public IDeviceManager BuildDeviceManager()
        {
            return new DeviceManager();
        }

        /// <summary>
        /// ����ҵ���߼����������������
        /// </summary>
        /// <returns>����ҵ���߼�������ʵ��</returns>
        public IRuleManager BuildRuleManager()
        {
            return new RuleManager();
        }

        /// <summary>
        /// ����ҵ���߼����������������
        /// </summary>
        /// <returns>����ҵ���߼�������ʵ��</returns>
        public IVariableManager BuildIVariableManager()
        {
            return new VariableManager();
        }

        /// <summary>
        /// ��־ҵ���߼����������������
        /// </summary>
        /// <returns>��־ҵ���߼�������ʵ��</returns>
        public ILogManager BuildLogManager()
        {
            return new LogManager();
        }

        /// <summary>
        /// ����ҵ���߼����������������
        /// </summary>
        /// <returns>����ҵ���߼�������ʵ��</returns>
        public IDataManager BuildDataManager()
        {
            return new DataManager();
        }
    }
}
