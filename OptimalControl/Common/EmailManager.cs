using System;
using System.Net.Mail;

namespace OptimalControl.Common
{
    /// <summary>
    /// Email发送通用类
    /// </summary>
    internal static class EmailManager
    {
        #region Public Methods
        /// <summary>
        /// Email发送通用方法
        /// </summary>
        /// <param name="from">发件人</param>
        /// <param name="to">收件人(多个收件人以逗号隔开)</param>
        /// <param name="cc">抄送</param>
        /// <param name="subject">主题</param>
        /// <param name="body">正文</param>
        /// <param name="attch">附件</param>
        /// <param name="priority">优先级</param>
        /// <returns>返回 "Succeed" 表示发送成功</returns>
        public static string MailSend(string from, string to, string cc, string subject, string body, Attachment attch, string priority)
        {
            MailMessage message = new MailMessage(from, to);
            message.CC.Add(cc);
            message.Subject = subject;
            message.Body = body;

            //message.CC.Add(new MailAddress(from)); //抄送给自己
            //message.Bcc.Add(new MailAddress(""));

            if (attch != null)
            {
                Attachment data = attch;
                message.Attachments.Add(data);
            }

            message.BodyEncoding = System.Text.Encoding.UTF8;//编码方式
            switch (priority.ToUpper())
            {
                case "HIGH":
                    message.Priority = MailPriority.High;//优先级
                    break;
                case "NORMAL":
                    message.Priority = MailPriority.Normal;//优先级
                    break;
                case "LOW":
                    message.Priority = MailPriority.Low;//优先级
                    break;
                default:
                    message.Priority = MailPriority.Normal;//优先级
                    break;
            }

            message.IsBodyHtml = false;//是否是html格式
            SmtpClient client = new SmtpClient();//不同情况更改

            //client.Credentials = CredentialCache.DefaultNetworkCredentials;//匿名认证

            try
            {
                // 尝试发送邮件
                client.Send(message);
                return "Succeed";
            }
            catch (Exception e)
            {
                // 返回异常信息
                return e.Message;
            }
        } 
        #endregion
    }
}
