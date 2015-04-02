using System;
using System.Collections.Generic;
using System.Text;

namespace CodingMouse.CMHotelManager.IBLL
{
    /// <summary>
    /// �ͻ�ҵ���߼�����ӿ�
    /// </summary>
    public interface IClientManager
    {
        /// <summary>
        /// ��ӵ����ͻ�
        /// </summary>
        /// <param name="Client">�ͻ�ʵ��</param>
        /// <returns>True:�ɹ� / False:ʧ��</returns>
        bool AddVIP(Model.Client Client);

        /// <summary>
        /// ���ݿͻ�IDɾ�������ͻ�
        /// </summary>
        /// <param name="id">�ͻ�ID</param>
        /// <returns>True:�ɹ� / False:ʧ��</returns>
        bool DeleteVIP(int id);

        /// <summary>
        /// �޸ĵ���/�����ͻ�
        /// </summary>
        /// <param name="ClientList">�ͻ�����</param>
        /// <returns>True:�ɹ� / False:ʧ��</returns>
        bool Modify(List<Model.Client> ClientList);

        /// <summary>
        /// ����ѡ�������Ͳ�ѯ���ݲ�ѯ�ͻ�
        /// </summary>
        /// <param name="columnName">����</param>
        /// <param name="content">��ѯ����</param>
        /// <returns>�ͻ�����</returns>
        List<Model.Client> GetVIPByColumnAndContent(string columnName, string content);

        /// <summary>
        /// ��ȡ���пͻ�
        /// </summary>
        /// <returns>�ͻ�����</returns>
        List<Model.Client> GetAllVIP();
    }
}
