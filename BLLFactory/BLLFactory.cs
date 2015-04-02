using System;
using System.Collections.Generic;
using System.Text;

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
        public IBLL.IOperatorManager BuildOperatorManager()
        {
            return new BLL.OperatorManager();
        }

        /// <summary>
        /// Ȩ����ҵ���߼����������������
        /// </summary>
        /// <returns>Ȩ����ҵ���߼�������ʵ��</returns>
        public IBLL.IRightsGroupManager BuildRightsGroupManager()
        {
            return new BLL.RightsGroupManager();
        }

        /// <summary>
        /// Ȩ�޹�ϵҵ���߼����������������
        /// </summary>
        /// <returns>Ȩ�޹�ϵҵ���߼�������ʵ��</returns>
        public IBLL.IRightsRelationManager BuildRightsRelationManager()
        {
            return new BLL.RightsRelationManager();
        }
    }
}
