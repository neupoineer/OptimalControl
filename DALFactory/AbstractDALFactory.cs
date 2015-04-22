using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using IDAL;
using IDAL.Control;

namespace DALFactory
{
    /// <summary>
    /// ���ݷ��ʲ���󹤳���
    /// </summary>
    public abstract class AbstractDALFactory
    {
        #region Abstract Methods
        /// <summary>
        /// ����Ա���ݷ��ʲ��������������
        /// </summary>
        /// <returns>����Ա���ݷ��ʲ�����ʵ��</returns>
        public abstract IOperatorService BuildOperatorService();
        /// <summary>
        /// Ȩ�������ݷ��ʲ��������������
        /// </summary>
        /// <returns>Ȩ�������ݷ��ʲ�����ʵ��</returns>
        public abstract IRightsGroupService BuildRightsGroupService();
        /// <summary>
        /// Ȩ�޹�ϵ���ݷ��ʲ��������������
        /// </summary>
        /// <returns>Ȩ�޹�ϵ���ݷ��ʲ�����ʵ��</returns>
        public abstract IRightsRelationService BuildRightsRelationService();

        public abstract ICurveService BuildCurveService();
        public abstract IDeviceService BuildDeviceService();
        public abstract IRuleService BuildRuleService();
        public abstract IVariableService BuildVariableService();
        public abstract ILogService BuildLogService();
        public abstract IDataService BuildDataService();
        #endregion

        #region Static Methods
        /// <summary>
        /// �������ݷ��ʲ���󹤳���ʵ��
        /// </summary>
        /// <returns>���ݷ��ʲ���󹤳���ʵ��</returns>
        public static AbstractDALFactory Instance()
        {
            // ���汾ϵͳ��ʹ�õ���������
            string dbType = string.Empty;
            // ��ȡ�����ļ��б������������
            dbType = ConfigurationManager.AppSettings["ConnectionType"].ToString();
            // ���ݱ������������ȷ��ʹ�þ�������ݷ��ʲ㹤����
            if (dbType.ToUpper().Trim() == "SQLSERVER")
                return new DALFactory();
            else if (dbType.ToUpper().Trim() == "SQLITE")
                return new DALFactory();
            else
                return null;
        } 
        #endregion
    }
}
