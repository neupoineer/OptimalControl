using System.Collections.Generic;
using Model;

namespace IBLL
{
    /// <summary>
    /// Ȩ�޹�ϵҵ���߼�����ӿ�
    /// </summary>
    public interface IRightsRelationManager
    {
        /// <summary>
        /// ��ӵ���Ȩ�޹�ϵ
        /// </summary>
        /// <param name="rightsRelation">Ȩ�޹�ϵʵ��</param>
        /// <returns>True:�ɹ�/False:ʧ��</returns>
        bool AddRightsRelation(RightsRelation rightsRelation);

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
        bool ModifyRightsRelation(RightsRelation rightsRelation);

        /// <summary>
        /// ��ȡ���е�Ȩ�޹�ϵ����
        /// </summary>
        /// <returns>Ȩ�޹�ϵ����</returns>
        List<RightsRelation> GetAllRightsRelation();

        /// <summary>
        /// ���ݲ���Ա ID ��ȡ��Ӧ������Ȩ�޹�ϵ
        /// </summary>
        /// <param name="id">����Ա ID</param>
        /// <returns>Ȩ�޹�ϵ����</returns>
        List<RightsRelation> GetRightsRelationByOperatorId(int id);

        /// <summary>
        /// ����Ȩ���� ID ��ȡ���Ȩ������ص�Ȩ�޹�ϵ����
        /// </summary>
        /// <param name="id">Ȩ���� ID</param>
        /// <returns>Ȩ�޹�ϵ����</returns>
        int GetRightsRelationCountByRightsGroupId(int id);
    }
}
