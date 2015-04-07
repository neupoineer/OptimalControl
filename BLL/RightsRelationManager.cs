using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    /// <summary>
    /// Ȩ�޹�ϵ���ݷ��ʲ�����
    /// </summary>
    public class RightsRelationManager : IBLL.IRightsRelationManager
    {
        #region IRightsRelationManager ��Ա

        /// <summary>
        /// ��ӵ���Ȩ�޹�ϵ
        /// </summary>
        /// <param name="rightsRelation">Ȩ�޹�ϵʵ��</param>
        /// <returns>True:�ɹ�/False:ʧ��</returns>
        public bool AddRightsRelation(Model.RightsRelation rightsRelation)
        {
            //���岢ʵ�������󹤳���
            DALFactory.AbstractDALFactory absDALFactory = DALFactory.AbstractDALFactory.Instance();
            //���ù�����������ʵ��
            IDAL.IRightsRelationService rightsRelationService = absDALFactory.BuildRightsRelationService();
            //����ʵ������
            return rightsRelationService.AddRightsRelation(rightsRelation);
        }

        /// <summary>
        /// ����Ȩ�޹�ϵ ID ɾ��Ȩ�޹�ϵ
        /// </summary>
        /// <param name="id">Ȩ�޹�ϵ ID</param>
        /// <returns>True:�ɹ�/False:ʧ��</returns>
        public bool DeleteRightsRelationById(int id)
        {
            //���岢ʵ�������󹤳���
            DALFactory.AbstractDALFactory absDALFactory = DALFactory.AbstractDALFactory.Instance();
            //���ù�����������ʵ��
            IDAL.IRightsRelationService rightsRelationService = absDALFactory.BuildRightsRelationService();
            //����ʵ������
            return rightsRelationService.DeleteRightsRelationById(id);
        }

        /// <summary>
        /// ���ݲ���Ա ID ɾ����Ӧ������Ȩ�޹�ϵ
        /// </summary>
        /// <param name="operatorId">����Ա ID</param>
        /// <returns>True:�ɹ�/False:ʧ��</returns>
        public bool DeleteRightsRelationByOperatorId(int operatorId)
        {
            //���岢ʵ�������󹤳���
            DALFactory.AbstractDALFactory absDALFactory = DALFactory.AbstractDALFactory.Instance();
            //���ù�����������ʵ��
            IDAL.IRightsRelationService rightsRelationService = absDALFactory.BuildRightsRelationService();
            //����ʵ������
            return rightsRelationService.DeleteRightsRelationByOperatorId(operatorId);
        }

        /// <summary>
        /// �޸ĵ���Ȩ�޹�ϵ
        /// </summary>
        /// <param name="rightsRelation">Ȩ�޹�ϵʵ��</param>
        /// <returns>True:�ɹ�/False:ʧ��</returns>
        public bool ModifyRightsRelation(Model.RightsRelation rightsRelation)
        {
            //���岢ʵ�������󹤳���
            DALFactory.AbstractDALFactory absDALFactory = DALFactory.AbstractDALFactory.Instance();
            //���ù�����������ʵ��
            IDAL.IRightsRelationService rightsRelationService = absDALFactory.BuildRightsRelationService();
            //����ʵ������
            return rightsRelationService.ModifyRightsRelation(rightsRelation);
        }

        /// <summary>
        /// ��ȡ���е�Ȩ�޹�ϵ����
        /// </summary>
        /// <returns>Ȩ�޹�ϵ����</returns>
        public List<Model.RightsRelation> GetAllRightsRelation()
        {
            //���岢ʵ�������󹤳���
            DALFactory.AbstractDALFactory absDALFactory = DALFactory.AbstractDALFactory.Instance();
            //���ù�����������ʵ��
            IDAL.IRightsRelationService rightsRelationService = absDALFactory.BuildRightsRelationService();
            //����ʵ������
            return rightsRelationService.GetAllRightsRelation();
        }

        /// <summary>
        /// ���ݲ���Ա ID ��ȡ��Ӧ������Ȩ�޹�ϵ
        /// </summary>
        /// <param name="id">����Ա ID</param>
        /// <returns>Ȩ�޹�ϵ����</returns>
        public List<Model.RightsRelation> GetRightsRelationByOperatorId(int id)
        {
            //���岢ʵ�������󹤳���
            DALFactory.AbstractDALFactory absDALFactory = DALFactory.AbstractDALFactory.Instance();
            //���ù�����������ʵ��
            IDAL.IRightsRelationService rightsRelationService = absDALFactory.BuildRightsRelationService();
            //����ʵ������
            return rightsRelationService.GetRightsRelationByOperatorId(id);
        }

        /// <summary>
        /// ����Ȩ���� ID ��ȡ���Ȩ������ص�Ȩ�޹�ϵ����
        /// </summary>
        /// <param name="id">Ȩ���� ID</param>
        /// <returns>Ȩ�޹�ϵ����</returns>
        public int GetRightsRelationCountByRightsGroupId(int id)
        {
            //���岢ʵ�������󹤳���
            DALFactory.AbstractDALFactory absDALFactory = DALFactory.AbstractDALFactory.Instance();
            //���ù�����������ʵ��
            IDAL.IRightsRelationService rightsRelationService = absDALFactory.BuildRightsRelationService();
            //����ʵ������
            return rightsRelationService.GetRightsRelationCountByRightsGroupId(id);
        }

        #endregion
    }
}
