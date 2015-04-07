using System;
using System.Drawing;
using System.Windows.Forms;

namespace OptimalControl.Forms
{
    /// <summary>
    /// ��Ļ��������
    /// </summary>
    public partial class frmLockScreen : Form
    {
        #region Private Members
        frmMain _frmMain = null;
        Model.Operator _currentOperator = null;
        bool isPass = false;
        // ����һ����������
        Form frmBackground = new Form();
        #endregion

        #region Event Handlers
        /// <summary>
        /// ���ι���
        /// </summary>
        /// <param name="frmMain">������</param>
        /// <param name="currentOperator">��ǰ��¼����Աʵ��</param>
        public frmLockScreen(frmMain frmMain, Model.Operator currentOperator)
        {
            // ���ô���ͬ��
            frmBackground.TopLevel = false;
            frmBackground.Parent = frmMain;

            // ��ʾ��������
            frmBackground.FormBorderStyle = FormBorderStyle.None;
            frmBackground.ShowInTaskbar = false;
            frmBackground.WindowState = FormWindowState.Maximized;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLockScreen));
            frmBackground.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("login")));
            frmBackground.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            frmBackground.Show();

            this.TopLevel = false;
            this.Parent = frmBackground;
            Point tempPoint = new Point(Parent.Location.X + (int) (Parent.Size.Width/2.4),
                                        Parent.Location.Y + (int) (Parent.Size.Width/2.6));
            this.Location = tempPoint;
            // ����������ؼ�
            InitializeComponent();
            // ����������
            _frmMain = frmMain;
            // ���浱ǰ��¼����Ա
            _currentOperator = currentOperator;
        }

        /// <summary>
        /// ������ť����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUnLock_Click(object sender, EventArgs e)
        {
            string pwd = this.txtPassword.Text.Trim();
            if (string.IsNullOrEmpty(pwd))
            {
                this.txtPassword.Focus();
                return;
            }
            if (pwd == _currentOperator.Password)
            {
                isPass = true;
                this.Close();
            }
            else
            {
                this.toolTip.ToolTipIcon = ToolTipIcon.Error;
                this.toolTip.ToolTipTitle = "������ʾ";
                Point showLocation = new Point(
                    this.txtPassword.Location.X,
                    this.txtPassword.Location.Y + this.txtPassword.Height);
                this.toolTip.Show("����������벻��ȷ��", this, showLocation, 5000);
                this.txtPassword.SelectAll();
                this.txtPassword.Focus();
            }
        }

        /// <summary>
        /// �û��رմ���ʱ��δ�رմ��岢ָ���ر�ԭ��ǰ�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmLockScreen_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!isPass)
                e.Cancel = true;

            // �رձ�������
            frmBackground.Close();
            // ����������
            _frmMain.Activate();
        }
        #endregion
    }
}