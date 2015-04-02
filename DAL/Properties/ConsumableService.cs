using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DBUtility = CodingMouse.CMHotelManager.DBUtility;
using IDAL = CodingMouse.CMHotelManager.IDAL;
using Model = CodingMouse.CMHotelManager.Model;

namespace CodingMouse.CMHotelManager.DAL
{
    /// <summary>
    /// ����Ʒ���ݷ��ʲ�����
    /// </summary>
    public class ConsumableService : IDAL.IConsumableService
    {

        #region IConsumableService ��Ա

        /// <summary>
        /// ��ӵ�������Ʒ��Ϣ
        /// </summary>
        /// <param name="consumable">����Ʒʵ��</param>
        /// <returns>True:�ɹ� / False:ʧ��</returns>
        public bool AddConsumable(Model.Consumable consumable)
        {
            // ������ʱ���ݼ�
            DataSet dsconsumableType = new DataSet("ConsumableType");
            // ����ƷԤ�赥��
            decimal advanceUnitPrice = consumable.ConsumableAdvanceUnitPrice;
            // ����Ʒ����
            int consumableNumber = consumable.ConsumableNumber;
            // ����Ʒ����
            string consumableType = consumable.ConsumableType;
            // ����Ʒ����
            string modelName = consumable.ModelName;
            // ���� SQL ִ�ж���
            DBUtility.AbstractDBProvider dbProvider = DBUtility.AbstractDBProvider.Instance();
            // ��ȡ�� Id
            int consumableTypeId = consumable.ConsumableTypeId;
            // ��������Ʒ��Ϣ �� SQL ����
            string sqlTxt = string.Format("insert into Consumable Values ('{0}', {1}, {2}, {3})", modelName, advanceUnitPrice, consumableTypeId, consumableNumber);
            // ִ�� ��������Ʒ��Ϣ SQL
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
        /// ��������Ʒ ID ɾ������/��������Ʒ��Ϣ
        /// </summary>
        /// <param name="idList">����Ʒ ID ����</param>
        /// <returns>True:�ɹ� / False:ʧ��</returns>
        public bool DeleteConsumableByID(List<int> idList)
        {
            bool isOk = false;
            int consumableId = 0;
            // ���� SQL ִ�ж���
            DBUtility.AbstractDBProvider dbProvider = DBUtility.AbstractDBProvider.Instance();
            // �����ݿ�����
            dbProvider.Open();
            // ��ʼ����
            dbProvider.BeginTrans();
            //ѭ��ִ�� ɾ������
            foreach(int id in idList)
            {
                //ִ��ɾ����������
                consumableId = id;
                // ɾ������Ʒ ������Ϣ SQL ����
                string sqlTxt = string.Format("Delete From Consumable Where Id = {0}", consumableId);

                //���ܷ��ص���Ӱ������
                int rowsAffected;
                dbProvider.RunCommand(sqlTxt, out rowsAffected);

                if (rowsAffected != 1)
                {
                    isOk = false;
                    break;
                }
                else
                {
                    isOk = true;
                }
            }

            if (isOk)
                // �ύ����
                dbProvider.CommitTrans();
            else
                // �ع�����
                dbProvider.RollBackTrans();

            // �ر���������
            dbProvider.Close();

            return isOk;
        }

        /// <summary>
        /// ��ȡ��������Ʒ��Ϣ
        /// </summary>
        /// <returns>����Ʒ����</returns>
        public List<Model.Consumable> GetAllConsumable()
        {
            //��������Ʒ����
            List<Model.Consumable> consumableList = new List<CodingMouse.CMHotelManager.Model.Consumable>();
            //������ʱ���ݼ�
            DataSet dsConsumable = new DataSet("Consumable");
            //���� ��ѯ����Ʒ��Ϣ SQL����
            string sqlTxt = string.Format("select C.Id, C.ConsumableName, C.ConsumableAdvanceUnitPrice, C.ConsumableNumber, T.Id as [TypeId]," +
                "T.ConsumableTypeName from Consumable as C join ConsumableType as T on C.ConsumableTypeId = T.Id");
            //����SQLִ�ж���
            DBUtility.AbstractDBProvider dbProvider = DBUtility.AbstractDBProvider.Instance();
            //ִ������Ʒ��ѯ����
            dsConsumable = dbProvider.RunCommand(sqlTxt, "Consumable");
            //�����ݼ�ת����ʵ�弯��
            foreach (DataRow row in dsConsumable.Tables["Consumable"].Rows)
            {
                Model.Consumable tmConsumable = new CodingMouse.CMHotelManager.Model.Consumable();
                tmConsumable.ConsumableAdvanceUnitPrice = Convert.ToDecimal(row["ConsumableAdvanceUnitPrice"]);
                tmConsumable.ConsumableNumber = Convert.ToInt32(row["ConsumableNumber"]);
                tmConsumable.ConsumableTypeId = Convert.ToInt32(row["TypeId"]);
                tmConsumable.ConsumableType = Convert.ToString(row["ConsumableTypeName"]);
                tmConsumable.Id = Convert.ToInt32(row["Id"]);
                tmConsumable.ModelName = Convert.ToString(row["ConsumableName"]);

                consumableList.Add(tmConsumable);
            }
            
            //������������Ʒ����
            return consumableList;
        }

        /// <summary>
        /// ����ѡ�������Ͳ�ѯ���ݲ�ѯ����Ʒ��Ϣ
        /// </summary>
        /// <param name="columnName">����</param>
        /// <param name="content">��ѯ����</param>
        /// <returns>����Ʒ����</returns>
        public List<Model.Consumable> GetConsumableByColumnAndContent(string columnName, string content)
        {
            //��������Ʒ����
            List<Model.Consumable> consumableList = new List<CodingMouse.CMHotelManager.Model.Consumable>();
            //������ʱ���ݼ�
            DataSet dsConsumable = new DataSet("Consumable");
            //������ѯ��Ϣ�� SQL
            string sqlTxt = null;
            //����ת�������Ͳ�ѯ����
            int numberContent = 0;
            if (int.TryParse(content, out numberContent))
            {
                sqlTxt = string.Format(
                    "select C.Id, C.ConsumableName, C.ConsumableAdvanceUnitPrice, C.ConsumableNumber, T.Id as [TypeId]," +
                    "T.ConsumableTypeName from Consumable as C join ConsumableType as T on C.ConsumableTypeId = T.Id where {0} = {1}", columnName, numberContent);
            }
            else
            {
                sqlTxt = string.Format(
                    "select C.Id, C.ConsumableName, C.ConsumableAdvanceUnitPrice, C.ConsumableNumber, T.Id as [TypeId]," +
                    "T.ConsumableTypeName from Consumable as C join ConsumableType as T on C.ConsumableTypeId = T.Id where {0} like '%{1}%'", columnName, content);
            }
            //����SQLִ�ж���
            DBUtility.AbstractDBProvider dbProvider = DBUtility.AbstractDBProvider.Instance();
            //ִ������Ʒ��ѯ����
            dsConsumable = dbProvider.RunCommand(sqlTxt, "Consumable");
            //�����ݼ�ת����ʵ�弯��
            foreach (DataRow row in dsConsumable.Tables["Consumable"].Rows)
            {
                Model.Consumable tmConsumable = new CodingMouse.CMHotelManager.Model.Consumable();
                tmConsumable.ConsumableAdvanceUnitPrice = Convert.ToDecimal(row["ConsumableAdvanceUnitPrice"]);
                tmConsumable.ConsumableNumber = Convert.ToInt32(row["ConsumableNumber"]);
                tmConsumable.ConsumableTypeId = Convert.ToInt32(row["TypeId"]);
                tmConsumable.ConsumableType = Convert.ToString(row["ConsumableTypeName"]);
                tmConsumable.Id = Convert.ToInt32(row["Id"]);
                tmConsumable.ModelName = Convert.ToString(row["ConsumableName"]);

                consumableList.Add(tmConsumable);
            }

            //������������Ʒ����
            return consumableList;
        }

        /// <summary>
        /// �޸ĵ���/��������Ʒ��Ϣ
        /// </summary>
        /// <param name="consumableList">����Ʒ����</param>
        /// <returns>True:�ɹ� / False:ʧ��</returns>
        public bool ModifyConsumable(List<Model.Consumable> consumableList)
        {
            bool isOk = false;
            // ������ʱ���ݼ�
            DataSet dsconsumableType = new DataSet("ConsumableType");
            // ���� SQL ִ�ж���
            DBUtility.AbstractDBProvider dbProvider = DBUtility.AbstractDBProvider.Instance();
            // �����ݿ�����
            dbProvider.Open();
            // ��ʼ����
            dbProvider.BeginTrans();

            //ѭ���޸� list �д����ÿһ��
            foreach (Model.Consumable con in consumableList)
            {
                // ����ƷԤ�赥��
                decimal advanceUnitPrice = con.ConsumableAdvanceUnitPrice;
                // ����Ʒ����
                int consumableNumber = con.ConsumableNumber;
                // ����Ʒ����
                string consumableType = con.ConsumableType;
                // ����Ʒ����
                string modelName = con.ModelName;
                // ����Ʒ���
                int id = con.Id;

                // ��ȡ�� Id
                int consumableTypeId = con.ConsumableTypeId;

                //�޸�����Ʒ��Ϣ SQL ����
                string sqlTxt = string.Format("update Consumable set ConsumableName = '{0}', ConsumableAdvanceUnitPrice = '{1}', ConsumableTypeId = '{2}', ConsumableNumber = '{3}' where Id = {4}", modelName, advanceUnitPrice, consumableTypeId, consumableNumber, id);
                // ִ��SQL����
                // ���շ��ص� ��Ӱ������
                int rowsAffected = 0;
                dbProvider.RunCommand(sqlTxt, out rowsAffected);

                if (rowsAffected != 1)
                {
                    isOk = false;
                    break;
                }
                else
                {
                    isOk = true;
                }
            }

            if (isOk)
                // �ύ����
                dbProvider.CommitTrans();
            else
                // �ع�����
                dbProvider.RollBackTrans();

            // �ر���������
            dbProvider.Close();

            return isOk;
        }

        /// <summary>
        /// ��������Ʒ�������ƻ�ȡ�������µ���������Ʒ����
        /// </summary>
        /// <param name="typeName">����Ʒ��������</param>
        /// <returns>����Ʒ����</returns>
        public List<Model.Consumable> GetConsumableByTypeName(string typeName)
        {
            //��������Ʒ����
            List<Model.Consumable> consumableList = new List<CodingMouse.CMHotelManager.Model.Consumable>();
            //������ʱ���ݼ�
            DataSet dsConsumable = new DataSet("Consumable");
            //���� ��ѯ����Ʒ��Ϣ SQL����
            string sqlTxt = string.Format("select C.Id, C.ConsumableName, C.ConsumableAdvanceUnitPrice, C.ConsumableNumber, T.Id as [TypeId]," +
                "T.ConsumableTypeName from Consumable as C join ConsumableType as T on C.ConsumableTypeId = T.Id Where ConsumableTypeName = '{0}'", typeName);
            //����SQLִ�ж���
            DBUtility.AbstractDBProvider dbProvider = DBUtility.AbstractDBProvider.Instance();
            //ִ������Ʒ��ѯ����
            dsConsumable = dbProvider.RunCommand(sqlTxt, "Consumable");
            //�����ݼ�ת����ʵ�弯��
            foreach (DataRow row in dsConsumable.Tables["Consumable"].Rows)
            {
                Model.Consumable tmConsumable = new CodingMouse.CMHotelManager.Model.Consumable();
                tmConsumable.ConsumableAdvanceUnitPrice = Convert.ToDecimal(row["ConsumableAdvanceUnitPrice"]);
                tmConsumable.ConsumableNumber = Convert.ToInt32(row["ConsumableNumber"]);
                tmConsumable.ConsumableTypeId = Convert.ToInt32(row["TypeId"]);
                tmConsumable.ConsumableType = Convert.ToString(row["ConsumableTypeName"]);
                tmConsumable.Id = Convert.ToInt32(row["Id"]);
                tmConsumable.ModelName = Convert.ToString(row["ConsumableName"]);

                consumableList.Add(tmConsumable);
            }

            //������������Ʒ����
            return consumableList;
        }

        #endregion
    }
}
