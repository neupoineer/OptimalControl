using System.Collections.Generic;
using Model;

namespace IDAL
{
    /// <summary>
    /// ����Ա���ݷ��ʲ����ӿ�
    /// </summary>
    public interface IOperatorService
    {
        /// <summary>
        /// ���ݲ���Ա���ƺ������ȡ����Աʵ��
        /// </summary>
        /// <param name="name">����Ա����</param>
        /// <param name="pwd">����Ա����</param>
        /// <returns>����Աʵ��</returns>
        Operator GetOperatorInfoByName(string name, string pwd);

        /// <summary>
        /// ��ȡ���в���Ա��Ϣ
        /// </summary>
        /// <returns>����Աʵ�弯��</returns>
        Dictionary<string, Operator> GetAllOperatorInfo();

        /// <summary>
        /// ��Ӳ���Ա
        /// </summary>
        /// <param name="addOperator">Ҫ��ӵĲ���Աʵ��</param>
        /// <returns>True:�ɹ�/False:ʧ��</returns>
        bool AddOperator(Operator addOperator);

        /// <summary>
        /// ɾ������Ա
        /// </summary>
        /// <param name="id">Ҫɾ���Ĳ���Ա ID</param>
        /// <returns>True:�ɹ�/False:ʧ��</returns>
        bool DeleteOperatorByID(int id);

        /// <summary>
        /// �޸Ĳ���Ա
        /// </summary>
        /// <param name="currentOperator">Ҫ�޸ĵĲ���Աʵ��</param>
        /// <returns>True:�ɹ�/False:ʧ��</returns>
        bool ModifyOperator(Operator currentOperator);

        /// <summary>
        /// ���ݲ���Ա����У�����Ա�Ƿ����
        /// </summary>
        /// <param name="operatorName">����Ա����</param>
        /// <returns>True:����/Flase:������</returns>
        bool CheckOperatorExist(string operatorName);
    }
}
