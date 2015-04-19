using System;
using System.Collections.Generic;
using System.Text;
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

        public ICurveManager BuildCurveManager()
        {
            return new CurveManager();
        }
        public IDeviceManager BuildDeviceManager()
        {
            return new DeviceManager();
        }
        public IRuleManager BuildRuleManager()
        {
            return new RuleManager();
        }
        public IVariableManager BuildIVariableManager()
        {
            return new VariableManager();
        }
        public ILogManager BuildLogManager()
        {
            return new LogManager();
        }
    }
}
