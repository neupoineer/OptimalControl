using System;
using System.Collections.Generic;
using System.Text;

namespace IDAL
{
    /// <summary>
    /// Ȩ�޹�ϵ���ݷ��ʲ����ӿ�
    /// </summary>
    public interface IRightsRelationService
    {
        /// <summary>
        /// ��ӵ���Ȩ�޹�ϵ
        /// </summary>
        /// <param name="rightsRelation">Ȩ�޹�ϵʵ��</param>
        /// <returns>True:�ɹ�/False:ʧ��</returns>
        bool AddRightsRelation(Model.RightsRelation rightsRelation);

        /// <summary>
        /// ����Ȩ�޹�ϵ ID ɾ��Ȩ�޹�ϵ
        /// </summary>
        /// <param name="id">Ȩ�޹�ϵ ID</param>
        /// <returns>True:�ɹ�/False:ʧ��</returns>
        bool DeleteRightsRelationById(int id);

        /// <summary>
        /// ���ݲ���Ա ID ɾ����Ӧ������Ȩ�޹�ϵ
        /// </summary>
        /// <param name="operatorId">����Ա ID</param>
        /// <returns>True:�ɹ�/False:ʧ��</returns>
        bool DeleteRightsRelationByOperatorId(int operatorId);

        /// <summary>
        /// �޸ĵ���Ȩ�޹�ϵ
        /// </summary>
        /// <param name="rightsRelation">Ȩ�޹�ϵʵ��</param>
        /// <returns>True:�ɹ�/False:ʧ��</returns>
        bool ModifyRightsRelation(Model.RightsRelation rightsRelation);

        /// <summary>
        /// ��ȡ���е�Ȩ�޹�ϵ����
        /// </summary>
        /// <returns>Ȩ�޹�ϵ����</returns>
        List<Model.RightsRelation> GetAllRightsRelation();

        /// <summary>
        /// ���ݲ���Ա ID ��ȡ��Ӧ������Ȩ�޹�ϵ
        /// </summary>
        /// <param name="id">����Ա ID</param>
        /// <returns>Ȩ�޹�ϵ����</returns>
        List<Model.RightsRelation> GetRightsRelationByOperatorId(int id);

        /// <summary>
        /// ����Ȩ���� ID ��ȡ���Ȩ������ص�Ȩ�޹�ϵ����
        /// </summary>
        /// <param name="id">Ȩ���� ID</param>
        /// <returns>Ȩ�޹�ϵ����</returns>
        int GetRightsRelationCountByRightsGroupId(int id);
    }
}
