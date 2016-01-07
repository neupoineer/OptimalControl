using System;
using System.Text;
using System.Windows.Forms;
using System.Net.Mail;
using OptimalControl.Common;

namespace OptimalControl.Forms
{
    /// <summary>
    /// 异常信息反馈邮件发送用户界面
    /// </summary>
    internal partial class frmSendFeedbackEMail : Form
    {
        #region Public Methods
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public frmSendFeedbackEMail()
        {
            InitializeComponent();
        } 
        #endregion

        #region Event Handlers
        /// <summary>
        /// 发送
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSend_Click(object sender, EventArgs e)
        {
            if (txtAddressPrefix.Text.Trim() == "" || cboAddressSuffix.Text.Trim() == "")
            {
                MessageBox.Show(
                    "请将邮件地址填写完整！",
                    "信息反馈",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                // 设置焦点
                if (txtAddressPrefix.Text.Trim() == "")
                {
                    txtAddressPrefix.Focus();
                }
                else
                {
                    cboAddressSuffix.Focus();
                    cboAddressSuffix.DroppedDown = true;
                }

                return;
            }
            else if (cboAddressSuffix.Text.Trim().IndexOf(".") < 0)
            {
                MessageBox.Show(
                    "邮件地址后缀填写不正确！",
                    "信息反馈",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                // 设置焦点
                cboAddressSuffix.Focus();
                cboAddressSuffix.DroppedDown = true;

                return;
            }

            // 保存发件人邮件地址
            string from = string.Format(
                "{0}@{1}",
                txtAddressPrefix.Text.Trim(),
                cboAddressSuffix.Text.Trim());

            // 声明Email附件对象
            Attachment attachment = null;
            // 附件名称
            string today = string.Format(
                "{0}-{1}-{2}",
                DateTime.Today.Year.ToString(),
                DateTime.Today.Month.ToString(),
                DateTime.Today.Day.ToString());
            string fileName = string.Format(@".\cache\ErrorLog\{0}.log", today);
            // 邮件正文
            StringBuilder mailBody = new StringBuilder();
            mailBody.Append("TO：BGRIMM%0A%0B"
                + "    在使用您的产品时遇到问题，现将程序自动生成的即时%0A"
                + "错误报告通过电邮反馈给您（详情请参见附件内容）。%0A"
                + "请即时解决此问题！%0A%0B"
                + "                                <用户姓名>%0A"
                + "                                 <用户ID>%0A");
            // 增加附件
            attachment = new Attachment(fileName);

            // 使用Email发送通用类对象发送邮件
            string state = Common.EmailManager.MailSend(from, "yangjiawei@bgrimm.com", "yangjiawei@bgrimm.com", "软件自动错误报告反馈", mailBody.ToString(), attachment, "HIGH");

            // 显示结果
            if (state.ToUpper() == "SUCCEED")
            {
                MessageBox.Show(
                    "错误报告反馈邮件已发送完毕！",
                    "信息反馈",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(
                    string.Format("错误报告反馈邮件发送失败！\n\n原因:{0}", state),
                    "信息反馈",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        } 
        #endregion
    }
}