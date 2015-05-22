using System;
using OptimalControl.Forms;

namespace OptimalControl.Common
{
    /// <summary>
    /// ȫ���쳣������
    /// </summary>
    internal static class GlobalExceptionManager
    {
        #region Public Methods
        /// <summary>
        /// ��ʾȫ���쳣��ʾ��Ϣ
        /// </summary>
        /// <param name="globalException">���񵽵�ȫ���쳣</param>
        /// <param name="applicationName">��ǰӦ�ó�������</param>
        /// <param name="developerName">���򿪷�������</param>
        public static void ShowGlobalExceptionInfo(Exception globalException, string applicationName, string developerName)
        {
            // ����ȫ���쳣��ʾ����ʵ������
            frmGlobalException frmGlobalException = new frmGlobalException(globalException, applicationName, developerName);
            // �ԶԻ���ģʽ��ʾ
            frmGlobalException.ShowDialog();
        } 
        #endregion
    }
}
