using System;
using System.Drawing;
using System.Windows.Forms;

namespace OptimalControl.Forms
{
    /// <summary>
    /// 屏幕锁定界面
    /// </summary>
    public partial class frmLockScreen : Form
    {
        #region Private Members
        frmMain _frmMain = null;
        Model.Operator _currentOperator = null;
        bool isPass = false;
        // 创建一个背景窗体
        Form frmBackground = new Form();
        #endregion

        #region Event Handlers
        /// <summary>
        /// 带参构造
        /// </summary>
        /// <param name="frmMain">主窗体</param>
        /// <param name="currentOperator">当前登录操作员实体</param>
        public frmLockScreen(frmMain frmMain, Model.Operator currentOperator)
        {
            // 设置窗体同步
            frmBackground.TopLevel = false;
            frmBackground.Parent = frmMain;

            // 显示背景窗体
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
            // 构建设计器控件
            InitializeComponent();
            // 保存主窗体
            _frmMain = frmMain;
            // 保存当前登录操作员
            _currentOperator = currentOperator;
        }

        /// <summary>
        /// 解锁按钮点击事件
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
                this.toolTip.ToolTipTitle = "解锁提示";
                Point showLocation = new Point(
                    this.txtPassword.Location.X,
                    this.txtPassword.Location.Y + this.txtPassword.Height);
                this.toolTip.Show("您输入的密码不正确！", this, showLocation, 5000);
                this.txtPassword.SelectAll();
                this.txtPassword.Focus();
            }
        }

        /// <summary>
        /// 用户关闭窗体时且未关闭窗体并指定关闭原因前事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmLockScreen_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!isPass)
                e.Cancel = true;

            // 关闭背景窗体
            frmBackground.Close();
            // 激活主窗体
            _frmMain.Activate();
        }
        #endregion
    }
}