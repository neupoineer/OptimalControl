using System.Collections.Generic;
using Model;

namespace IBLL
{
    /// <summary>
    /// Ȩ����ҵ���߼�����ӿ�
    /// </summary>
    public interface IRightsGroupManager
    {
        /// <summary>
        /// ��ȡ����Ȩ������Ϣ
        /// </summary>
        /// <returns>Ȩ����ʵ�弯��</returns>
        Dictionary<string, RightsGroup> GetAllRightsGroupInfo();

        /// <summary>
        /// ����Ȩ��������У��Ȩ�����Ƿ��Ѿ�����
        /// </summary>
        /// <param name="rightsGroupName">Ȩ��������</param>
        /// <returns>True:����/False:������</returns>
        bool CheckRightsGroupExist(string rightsGroupName);

        /// <summary>
        /// ���Ȩ����
        /// </summary>
        /// <param name="addRightsGroup">Ҫ��ӵ�Ȩ����ʵ��</param>
        /// <returns>True:�ɹ�/False:ʧ��</returns>
        bool AddRightsGroup(RightsGroup addRightsGroup);

        /// <summary>
        /// ɾ��Ȩ����
        /// </summary>
        /// <param name="id">Ҫɾ����Ȩ���� ID</param>
        /// <returns>True:�ɹ�/False:ʧ��</returns>
        bool DeleteRightsGroupByID(int id);

        /// <summary>
        /// �޸�Ȩ����
        /// </summary>
        /// <param name="currentRightsGroup">Ҫ�޸ĵ�Ȩ����ʵ��</param>
        /// <returns>True:�ɹ�/False:ʧ��</returns>
        bool ModifyRightsGroup(RightsGroup currentRightsGroup);
    }
}
