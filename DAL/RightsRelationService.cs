using System;
using System.Collections.Generic;
using System.Data;
using IDAL;
using Model;

namespace DAL
{
    /// <summary>
    /// Ȩ�޹�ϵ���ݷ��ʲ�����
    /// </summary>
    public class RightsRelationService : IRightsRelationService
    {
        #region IRightsRelationService ��Ա

        /// <summary>
        /// ��ӵ���Ȩ�޹�ϵ
        /// </summary>
        /// <param name="rightsRelation">Ȩ�޹�ϵʵ��</param>
        /// <returns>True:�ɹ�/False:ʧ��</returns>
        public bool AddRightsRelation(RightsRelation rightsRelation)
        {
            // ƴ�� SQL ����
            string sqlTxt = string.Format(
                "Insert Into RightsRelation (OperatorId, RightsGroupId) " +
                "Values ({0}, {1})",
                rightsRelation.OperatorId, rightsRelation.RightsGroupId);

            // ���� SQL ִ�ж���
            DBUtility.AbstractDBProvider dbProvider = DBUtility.AbstractDBProvider.Instance();
            // ִ�� SQL
            int rowsAffected;
            dbProvider.RunCommand(sqlTxt, out rowsAffected);

            if (rowsAffected == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// ����Ȩ�޹�ϵ ID ɾ��Ȩ�޹�ϵ
        /// </summary>
        /// <param name="id">Ȩ�޹�ϵ ID</param>
        /// <returns>True:�ɹ�/False:ʧ��</returns>
        public bool DeleteRightsRelationById(int id)
        {
            bool isOk = false;

            // ɾ��������Ϣ SQL ����
            string sqlTxt = string.Format("Delete From RightsRelation Where Id = {0}", id);
            // ���� SQL ִ�ж���
            DBUtility.AbstractDBProvider dbProvider = DBUtility.AbstractDBProvider.Instance();
            // ִ�� ɾ������
            int rowsAffected;
            dbProvider.RunCommand(sqlTxt, out rowsAffected);

            if (rowsAffected >= 1)
            {
                isOk = true;
            }
            else
            {
                isOk = false;
            }

            return isOk;
        }

        /// <summary>
        /// ���ݲ���Ա ID ɾ����Ӧ������Ȩ�޹�ϵ
        /// </summary>
        /// <param name="operatorId">����Ա ID</param>
        /// <returns>True:�ɹ�/False:ʧ��</returns>
        public bool DeleteRightsRelationByOperatorId(int operatorId)
        {
            bool isOk = false;

            // ɾ��������Ϣ SQL ����
            string sqlTxt = string.Format("Delete From RightsRelation Where OperatorId = {0}", operatorId);
            // ���� SQL ִ�ж���
            DBUtility.AbstractDBProvider dbProvider = DBUtility.AbstractDBProvider.Instance();
            // ִ�� ɾ������
            int rowsAffected;
            dbProvider.RunCommand(sqlTxt, out rowsAffected);

            if (rowsAffected >= 1)
            {
                isOk = true;
            }
            else
            {
                isOk = false;
            }

            return isOk;
        }

        /// <summary>
        /// �޸ĵ���Ȩ�޹�ϵ
        /// </summary>
        /// <param name="rightsRelation">Ȩ�޹�ϵʵ��</param>
        /// <returns>True:�ɹ�/False:ʧ��</returns>
        public bool ModifyRightsRelation(RightsRelation rightsRelation)
        {
            // ƴ�� SQL ����
            string sqlTxt = string.Format(
                "Update RightsRelation Set OperatorId = {0}, RightsGroupId = {1} Where Id = {2}",
                rightsRelation.OperatorId, rightsRelation.RightsGroupId, rightsRelation.Id);

            // ���� SQL ִ�ж���
            DBUtility.AbstractDBProvider dbProvider = DBUtility.AbstractDBProvider.Instance();
            // ִ�� SQL
            int rowsAffected;
            dbProvider.RunCommand(sqlTxt, out rowsAffected);

            if (rowsAffected == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// ��ȡ���е�Ȩ�޹�ϵ����
        /// </summary>
        /// <returns>Ȩ�޹�ϵ����</returns>
        public List<RightsRelation> GetAllRightsRelation()
        {
            //�������ݼ�
            DataSet dsRightsRelation = new DataSet("RightsRelation");
            //�����ͻ�����
            List<RightsRelation> rightsRelationList = new List<RightsRelation>();
            //������ѯ�ͻ���Ϣ�� SQL
            string sqlTxt = "Select R.Id, R.OperatorId, O.OperatorName, R.RightsGroupId, " +
                "G.GroupName As [RightsGroupName] From RightsRelation As R Join Operator As O " +
                "On R.OperatorId = O.Id Join RightsGroup As G On R.RightsGroupId = G.Id";
            //����SQLִ�ж���
            DBUtility.AbstractDBProvider dbProvider = DBUtility.AbstractDBProvider.Instance();
            //ִ�в�ѯ����
            dsRightsRelation = dbProvider.RunCommand(sqlTxt, "RightsRelation");
            //�����ݼ�ת����ʵ�弯��
            foreach (DataRow row in dsRightsRelation.Tables["RightsRelation"].Rows)
            {
                RightsRelation tmpRightsRelation = new RightsRelation();
                tmpRightsRelation.Id = Convert.ToInt32(row["Id"]);
                tmpRightsRelation.OperatorId = Convert.ToInt32(row["OperatorId"]);
                tmpRightsRelation.OperatorName = Convert.ToString(row["OperatorName"]);
                tmpRightsRelation.RightsGroupId = Convert.ToInt32(row["RightsGroupId"]);
                tmpRightsRelation.RightsGroupName = Convert.ToString(row["RightsGroupName"]);

                rightsRelationList.Add(tmpRightsRelation);
            }

            //�������пͻ�����
            return rightsRelationList;
        }

        /// <summary>
        /// ���ݲ���Ա ID ��ȡ��Ӧ������Ȩ�޹�ϵ
        /// </summary>
        /// <param name="id">����Ա ID</param>
        /// <returns>Ȩ�޹�ϵ����</returns>
        public List<RightsRelation> GetRightsRelationByOperatorId(int id)
        {
            //�������ݼ�
            DataSet dsRightsRelation = new DataSet("RightsRelation");
            //�����ͻ�����
            List<RightsRelation> rightsRelationList = new List<RightsRelation>();
            //������ѯ�ͻ���Ϣ�� SQL
            string sqlTxt = string.Format("Select R.Id, R.OperatorId, O.OperatorName, R.RightsGroupId, " +
                "G.GroupName As [RightsGroupName] From RightsRelation As R Join Operator As O " +
                "On R.OperatorId = O.Id Join RightsGroup As G On R.RightsGroupId = G.Id " +
                "Where OperatorId = {0}", id);
            //����SQLִ�ж���
            DBUtility.AbstractDBProvider dbProvider = DBUtility.AbstractDBProvider.Instance();
            //ִ�в�ѯ����
            dsRightsRelation = dbProvider.RunCommand(sqlTxt, "RightsRelation");
            //�����ݼ�ת����ʵ�弯��
            foreach (DataRow row in dsRightsRelation.Tables["RightsRelation"].Rows)
            {
                RightsRelation tmpRightsRelation = new RightsRelation();
                tmpRightsRelation.Id = Convert.ToInt32(row["Id"]);
                tmpRightsRelation.OperatorId = Convert.ToInt32(row["OperatorId"]);
                tmpRightsRelation.OperatorName = Convert.ToString(row["OperatorName"]);
                tmpRightsRelation.RightsGroupId = Convert.ToInt32(row["RightsGroupId"]);
                tmpRightsRelation.RightsGroupName = Convert.ToString(row["RightsGroupName"]);

                rightsRelationList.Add(tmpRightsRelation);
            }

            //�������пͻ�����
            return rightsRelationList;
        }

        /// <summary>
        /// ����Ȩ���� ID ��ȡ���Ȩ������ص�Ȩ�޹�ϵ����
        /// </summary>
        /// <param name="id">Ȩ���� ID</param>
        /// <returns>Ȩ�޹�ϵ����</returns>
        public int GetRightsRelationCountByRightsGroupId(int id)
        {
            // SQL����
            string sqlTxt = string.Format("Select Count(*) From RightsRelation Where RightsGroupId = {0}", id);

            // ����SQLִ�ж���
            DBUtility.AbstractDBProvider dbProvider = DBUtility.AbstractDBProvider.Instance();
            // ִ�в�ѯ����
            int result = Convert.ToInt32(dbProvider.RunCommand(sqlTxt));

            // ���ؽ��
            return result;
        }

        #endregion
    }
}
