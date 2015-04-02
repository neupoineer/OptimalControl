using System;
using System.Collections.Generic;
using System.Text;
using IDAL;
using DAL;
using Model;

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
        public override IDAL.IOperatorService BuildOperatorService()
        {
            return new DAL.OperatorService();
        }

        /// <summary>
        /// Ȩ�������ݷ��ʲ��������������
        /// </summary>
        /// <returns>Ȩ�������ݷ��ʲ�����ʵ��</returns>
        public override IDAL.IRightsGroupService BuildRightsGroupService()
        {
            return new DAL.RightsGroupService();
        }

        /// <summary>
        /// Ȩ�޹�ϵ���ݷ��ʲ��������������
        /// </summary>
        /// <returns>Ȩ�޹�ϵ���ݷ��ʲ�����ʵ��</returns>
        public override IDAL.IRightsRelationService BuildRightsRelationService()
        {
            return new DAL.RightsRelationService();
        }
    }
}
