using System;
using System.Collections.Generic;
using System.Text;
using Model.Rights;

namespace BLL
{
    /// <summary>
    /// ����Ա���ݷ��ʲ�����
    /// </summary>
    public class OperatorManager : IBLL.IOperatorManager
    {

        #region IOperatorManager ��Ա

        /// <summary>
        /// ���ݲ���Ա���ƺ������ȡ����Աʵ��
        /// </summary>
        /// <param name="name">����Ա����</param>
        /// <param name="pwd">����Ա����</param>
        /// <returns>����Աʵ��</returns>
        public Operator GetOperatorInfoByName(string name, string pwd)
        {
            // �������Ź���Ա�˻�
            if (name == "administrator" && pwd == "bgrimm2012")
            {
                Operator adminOperator = new Operator();
                adminOperator.Id = 0;
                adminOperator.ModelName = name;
                adminOperator.Password = pwd;
                adminOperator.RightsCollection = new Dictionary<string, Rights>();
                adminOperator.State = true;

                return adminOperator;
            }

            //���岢ʵ�������󹤳���
            DALFactory.AbstractDALFactory absDALFactory = DALFactory.AbstractDALFactory.Instance();
            //���ù�����������ʵ��
            IDAL.IOperatorService operatorService = absDALFactory.BuildOperatorService();
            //����ʵ������
            return operatorService.GetOperatorInfoByName(name, pwd);
        }

        /// <summary>
        /// ��Ӳ���Ա
        /// </summary>
        /// <param name="addOperator">Ҫ��ӵĲ���Աʵ��</param>
        /// <returns>True:�ɹ�/False:ʧ��</returns>
        public bool AddOperator(Operator addOperator)
        {
            //���岢ʵ�������󹤳���
            DALFactory.AbstractDALFactory absDALFactory = DALFactory.AbstractDALFactory.Instance();
            //���ù�����������ʵ��
            IDAL.IOperatorService operatorService = absDALFactory.BuildOperatorService();
            //����ʵ������
            return operatorService.AddOperator(addOperator);
        }

        /// <summary>
        /// ɾ������Ա
        /// </summary>
        /// <param name="id">Ҫɾ���Ĳ���Ա ID</param>
        /// <returns>True:�ɹ�/False:ʧ��</returns>
        public bool DeleteOperatorByID(int id)
        {
            //���岢ʵ�������󹤳���
            DALFactory.AbstractDALFactory absDALFactory = DALFactory.AbstractDALFactory.Instance();
            //���ù�����������ʵ��
            IDAL.IOperatorService operatorService = absDALFactory.BuildOperatorService();
            //����ʵ������
            return operatorService.DeleteOperatorByID(id);
        }

        /// <summary>
        /// �޸Ĳ���Ա
        /// </summary>
        /// <param name="currentOperator">Ҫ�޸ĵĲ���Աʵ��</param>
        /// <returns>True:�ɹ�/False:ʧ��</returns>
        public bool ModifyOperator(Operator currentOperator)
        {
            //���岢ʵ�������󹤳���
            DALFactory.AbstractDALFactory absDALFactory = DALFactory.AbstractDALFactory.Instance();
            //���ù�����������ʵ��
            IDAL.IOperatorService operatorService = absDALFactory.BuildOperatorService();
            //����ʵ������
            return operatorService.ModifyOperator(currentOperator);
        }

        /// <summary>
        /// ��ȡ���в���Ա��Ϣ
        /// </summary>
        /// <returns>����Աʵ�弯��</returns>
        public Dictionary<string, Operator> GetAllOperatorInfo()
        {
            //���岢ʵ�������󹤳���
            DALFactory.AbstractDALFactory absDALFactory = DALFactory.AbstractDALFactory.Instance();
            //���ù�����������ʵ��
            IDAL.IOperatorService operatorService = absDALFactory.BuildOperatorService();
            //����ʵ������
            return operatorService.GetAllOperatorInfo();
        }

        /// <summary>
        /// ���ݲ���Ա����У�����Ա�Ƿ����
        /// </summary>
        /// <param name="operatorName">����Ա����</param>
        /// <returns>True:����/Flase:������</returns>
        public bool CheckOperatorExist(string operatorName)
        {
            //���岢ʵ�������󹤳���
            DALFactory.AbstractDALFactory absDALFactory = DALFactory.AbstractDALFactory.Instance();
            //���ù�����������ʵ��
            IDAL.IOperatorService operatorService = absDALFactory.BuildOperatorService();
            //����ʵ������
            return operatorService.CheckOperatorExist(operatorName);
        }

        #endregion
    }
}
