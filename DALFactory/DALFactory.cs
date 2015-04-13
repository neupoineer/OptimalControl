using System;
using System.Collections.Generic;
using System.Text;
using DAL;
using IDAL;

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
    }
}
