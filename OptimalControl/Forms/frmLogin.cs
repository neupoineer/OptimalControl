using System;
using System.Drawing;
using System.Windows.Forms;
using IBLL;
using Model;

namespace OptimalControl.Forms
{
    /// <summary>
    /// �û���¼����
    /// </summary>
    public partial class frmLogin : Form
    {
        #region Private Members
        /// <summary>
        /// ���洰��������X��ֵ��Y��ֵ
        /// </summary>
        int _x, _y;
        /// <summary>
        /// ���洰���Ƿ���ƶ���ʶ
        /// </summary>
        bool isMove = false;
        #endregion

        // �����¼����Ƿ�Ϸ���֤���
        internal bool isPass = false;
        internal Operator currentOperator;

        #region Private Methods
        /// <summary>
        /// �û�������֤
        /// </summary>
        /// <returns></returns>
        private bool UserInputCheck()
        {
            // �����¼����
            string loginName = this.txtLoginName.Text.Trim();
            // �����û�����
            string userPwd = this.txtUserPwd.Text.Trim();

            // ��ʼ��֤
            if (string.IsNullOrEmpty(loginName))
            {
                this.toolTip.ToolTipIcon = ToolTipIcon.Info;
                this.toolTip.ToolTipTitle = "��¼��ʾ";
                Point showLocation = new Point(
                    this.txtLoginName.Location.X + this.txtLoginName.Width, 
                    this.txtLoginName.Location.Y);
                this.toolTip.Show("���������¼���ƣ�", this, showLocation, 1000);
                this.txtLoginName.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(userPwd))
            {
                this.toolTip.ToolTipIcon = ToolTipIcon.Info;
                this.toolTip.ToolTipTitle = "��¼��ʾ";
                Point showLocation = new Point(
                    this.txtUserPwd.Location.X + this.txtUserPwd.Width,
                    this.txtUserPwd.Location.Y);
                this.toolTip.Show("���������û����룡", this, showLocation, 1000);
                this.txtUserPwd.Focus();
                return false;
            }
            else if (userPwd.Length < 6)
            {
                this.toolTip.ToolTipIcon = ToolTipIcon.Warning;
                this.toolTip.ToolTipTitle = "��¼����";
                Point showLocation = new Point(
                    this.txtUserPwd.Location.X + this.txtUserPwd.Width,
                    this.txtUserPwd.Location.Y);
                this.toolTip.Show("�û����볤�Ȳ���С����λ��", this, showLocation, 1000);
                this.txtUserPwd.Focus();
                return false;
            }

            // �����ͨ������������֤�򷵻���
            return true;
        }

        /// <summary>
        /// ��ʾ��¼ʧ�ܹ�����ʾ
        /// </summary>
        /// <param name="ex">������¼ʧ�ܵ��쳣</param>
        private void ShowLoginLostToolTip(Exception ex)
        {
            // �����¼����δ���ͷ�
            if (!this.IsDisposed)
            {
                this.toolTip.ToolTipIcon = ToolTipIcon.Error;
                this.toolTip.ToolTipTitle = "��¼ʧ��";
                Point showLocation = new Point(
                    this.txtLoginName.Location.X + this.txtLoginName.Width,
                    this.txtLoginName.Location.Y);
                this.toolTip.Show(ex.Message, this, showLocation, 1000);
                this.txtLoginName.SelectAll();
                this.txtLoginName.Focus();
            }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// ���ι���
        /// </summary>
        public frmLogin()
        {
            // ���������
            InitializeComponent();
        } 
        #endregion

        #region Event Handlers
        /// <summary>
        /// ��¼��ť����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLogin_Click(object sender, EventArgs e)
        {
            // ���ͨ��������֤
            if (UserInputCheck())
            {
                try
                {

                    // ����ҵ���߼��㹤����ʵ��
                    BLLFactory.BLLFactory bllFactory = new BLLFactory.BLLFactory();
                    // ���ù�����ʵ����������ҵ���߼�������ʵ��
                    IOperatorManager operatorManager = bllFactory.BuildOperatorManager();
                    // ��ȡ��ǰ��¼����Աʵ��
                    currentOperator = operatorManager.GetOperatorInfoByName(
                        this.txtLoginName.Text.Trim(),
                        this.txtUserPwd.Text.Trim());

                    // ��Ȩ��֤
                    if (currentOperator.RightsCollection == null)
                        throw new Exception(string.Format("����Ա [{0}] ����ЧȨ�ޣ���ֹ��¼��", currentOperator.Name));

                    if (currentOperator != null)
                        if (this.txtUserPwd.Text.Trim() == currentOperator.Password)
                        {
                            // �������
                            this.txtLoginName.Text = string.Empty;
                            this.txtUserPwd.Text = string.Empty;
                            this.txtLoginName.Focus();

                            // ��ʶ��֤ͨ��
                            isPass = true;
                            this.DialogResult = DialogResult.OK;
                            this.Dispose();
                        }

                    // ���δͨ����֤
                    if (!isPass)
                    {
                        throw new Exception("��¼���ƻ��û����벻��ȷ��");
                    }
                }
                catch (Exception ex)
                {
                    ShowLoginLostToolTip(ex);
                }
            }
        }

        /// <summary>
        /// �˳���ť�����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// ���ָ���ڴ����Ϸ������°����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmLogin_MouseDown(object sender, MouseEventArgs e)
        {
            // ����Ӧ����������¼�
            if (e.Button == MouseButtons.Left)
            {
                // ���������
                this._x = e.X;
                this._y = e.Y;

                // ��ʶ������ƶ�
                this.isMove = true;
            }
        }

        /// <summary>
        /// ���ָ���ڴ����Ϸ����ƶ����ָ���¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmLogin_MouseMove(object sender, MouseEventArgs e)
        {
            // ������ƶ�
            if (this.isMove)
            {
                // ���ݾ���������ƫ��λ���ƶ�����
                // ����һ��
                // this.Left += e.X - this._x;
                // this.Top += e.Y - this._y;
                // ��������
                this.SetDesktopLocation(this.Left + e.X - this._x, this.Top + e.Y - this._y);

                // �ڱ�������ʾ��ǰ����
                string xPoint = this.Left.ToString().Trim();
                string yPoint = this.Top.ToString().Trim();
                this.Text = string.Format(
                    "��{0},{1}��",
                    xPoint.Length < 5 ? (xPoint.PadLeft(5)) : xPoint,
                    yPoint.Length < 5 ? (yPoint.PadRight(5)) : yPoint);
            }
        }

        /// <summary>
        /// ���ָ���ڴ����Ϸ����ͷŰ����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmLogin_MouseUp(object sender, MouseEventArgs e)
        {
            // ��ʶ���岻���ƶ�
            this.isMove = false;
        }

        #endregion
    }
}