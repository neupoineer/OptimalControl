using System;
using System.Collections.Generic;
using System.Text;
using Model.Rights;

namespace BLL
{
    /// <summary>
    /// Ȩ�������ݷ��ʲ�����
    /// </summary>
    public class RightsGroupManager : IBLL.IRightsGroupManager
    {
        #region IRightsGroupManager ��Ա

        /// <summary>
        /// ��ȡ����Ȩ������Ϣ
        /// </summary>
        /// <returns>Ȩ����ʵ�弯��</returns>
        public Dictionary<string, RightsGroup> GetAllRightsGroupInfo()
        {
            //���岢ʵ�������󹤳���
            DALFactory.AbstractDALFactory absDALFactory = DALFactory.AbstractDALFactory.Instance();
            //���ù�����������ʵ��
            IDAL.IRightsGroupService rightsGroupService = absDALFactory.BuildRightsGroupService();
            //����ʵ������
            return rightsGroupService.GetAllRightsGroupInfo();
        }

        /// <summary>
        /// ���Ȩ����
        /// </summary>
        /// <param name="addRightsGroup">Ҫ��ӵ�Ȩ����ʵ��</param>
        /// <returns>True:�ɹ�/False:ʧ��</returns>
        public bool AddRightsGroup(RightsGroup addRightsGroup)
        {
            //���岢ʵ�������󹤳���
            DALFactory.AbstractDALFactory absDALFactory = DALFactory.AbstractDALFactory.Instance();
            //���ù�����������ʵ��
            IDAL.IRightsGroupService rightsGroupService = absDALFactory.BuildRightsGroupService();
            //����ʵ������
            return rightsGroupService.AddRightsGroup(addRightsGroup);
        }

        /// <summary>
        /// ɾ��Ȩ����
        /// </summary>
        /// <param name="id">Ҫɾ����Ȩ���� ID</param>
        /// <returns>True:�ɹ�/False:ʧ��</returns>
        public bool DeleteRightsGroupByID(int id)
        {
            //���岢ʵ�������󹤳���
            DALFactory.AbstractDALFactory absDALFactory = DALFactory.AbstractDALFactory.Instance();
            //���ù�����������ʵ��
            IDAL.IRightsGroupService rightsGroupService = absDALFactory.BuildRightsGroupService();
            //����ʵ������
            return rightsGroupService.DeleteRightsGroupByID(id);
        }

        /// <summary>
        /// �޸�Ȩ����
        /// </summary>
        /// <param name="currentRightsGroup">Ҫ�޸ĵ�Ȩ����ʵ��</param>
        /// <returns>True:�ɹ�/False:ʧ��</returns>
        public bool ModifyRightsGroup(RightsGroup currentRightsGroup)
        {
            //���岢ʵ�������󹤳���
            DALFactory.AbstractDALFactory absDALFactory = DALFactory.AbstractDALFactory.Instance();
            //���ù�����������ʵ��
            IDAL.IRightsGroupService rightsGroupService = absDALFactory.BuildRightsGroupService();
            //����ʵ������
            return rightsGroupService.ModifyRightsGroup(currentRightsGroup);
        }

        /// <summary>
        /// ����Ȩ��������У��Ȩ�����Ƿ��Ѿ�����
        /// </summary>
        /// <param name="rightsGroupName">Ȩ��������</param>
        /// <returns>True:����/False:������</returns>
        public bool CheckRightsGroupExist(string rightsGroupName)
        {
            //���岢ʵ�������󹤳���
            DALFactory.AbstractDALFactory absDALFactory = DALFactory.AbstractDALFactory.Instance();
            //���ù�����������ʵ��
            IDAL.IRightsGroupService rightsGroupService = absDALFactory.BuildRightsGroupService();
            //����ʵ������
            return rightsGroupService.CheckRightsGroupExist(rightsGroupName);
        }

        #endregion
    }
}
