using System;
using System.Net.Mail;

namespace OptimalControl.Common
{
    /// <summary>
    /// Email����ͨ����
    /// </summary>
    internal static class EmailManager
    {
        #region Public Methods
        /// <summary>
        /// Email����ͨ�÷���
        /// </summary>
        /// <param name="from">������</param>
        /// <param name="to">�ռ���(����ռ����Զ��Ÿ���)</param>
        /// <param name="cc">����</param>
        /// <param name="subject">����</param>
        /// <param name="body">����</param>
        /// <param name="attch">����</param>
        /// <param name="priority">���ȼ�</param>
        /// <returns>���� "Succeed" ��ʾ���ͳɹ�</returns>
        public static string MailSend(string from, string to, string cc, string subject, string body, Attachment attch, string priority)
        {
            MailMessage message = new MailMessage(from, to);
            message.CC.Add(cc);
            message.Subject = subject;
            message.Body = body;

            //message.CC.Add(new MailAddress(from)); //���͸��Լ�
            //message.Bcc.Add(new MailAddress(""));

            if (attch != null)
            {
                Attachment data = attch;
                message.Attachments.Add(data);
            }

            message.BodyEncoding = System.Text.Encoding.UTF8;//���뷽ʽ
            switch (priority.ToUpper())
            {
                case "HIGH":
                    message.Priority = MailPriority.High;//���ȼ�
                    break;
                case "NORMAL":
                    message.Priority = MailPriority.Normal;//���ȼ�
                    break;
                case "LOW":
                    message.Priority = MailPriority.Low;//���ȼ�
                    break;
                default:
                    message.Priority = MailPriority.Normal;//���ȼ�
                    break;
            }

            message.IsBodyHtml = false;//�Ƿ���html��ʽ
            SmtpClient client = new SmtpClient();//��ͬ�������

            //client.Credentials = CredentialCache.DefaultNetworkCredentials;//������֤

            try
            {
                // ���Է����ʼ�
                client.Send(message);
                return "Succeed";
            }
            catch (Exception e)
            {
                // �����쳣��Ϣ
                return e.Message;
            }
        } 
        #endregion
    }
}
