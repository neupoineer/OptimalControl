using System;
using System.Collections.Generic;
using System.Text;
using BLL;
using IBLL;

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
    }
}
