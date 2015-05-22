using System;
using System.Drawing;
using System.Windows.Forms;
using IBLL;
using Model;

namespace OptimalControl.Forms
{
    /// <summary>
    /// 用户登录界面
    /// </summary>
    public partial class frmLogin : Form
    {
        #region Private Members
        /// <summary>
        /// 保存窗体旧坐标的X轴值和Y轴值
        /// </summary>
        int _x, _y;
        /// <summary>
        /// 保存窗体是否可移动标识
        /// </summary>
        bool isMove = false;
        #endregion

        // 保存登录身份是否合法验证结果
        internal bool isPass = false;
        internal Operator currentOperator;

        #region Private Methods
        /// <summary>
        /// 用户输入验证
        /// </summary>
        /// <returns></returns>
        private bool UserInputCheck()
        {
            // 保存登录名称
            string loginName = this.txtLoginName.Text.Trim();
            // 保存用户密码
            string userPwd = this.txtUserPwd.Text.Trim();

            // 开始验证
            if (string.IsNullOrEmpty(loginName))
            {
                this.toolTip.ToolTipIcon = ToolTipIcon.Info;
                this.toolTip.ToolTipTitle = "登录提示";
                Point showLocation = new Point(
                    this.txtLoginName.Location.X + this.txtLoginName.Width, 
                    this.txtLoginName.Location.Y);
                this.toolTip.Show("请您输入登录名称！", this, showLocation, 1000);
                this.txtLoginName.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(userPwd))
            {
                this.toolTip.ToolTipIcon = ToolTipIcon.Info;
                this.toolTip.ToolTipTitle = "登录提示";
                Point showLocation = new Point(
                    this.txtUserPwd.Location.X + this.txtUserPwd.Width,
                    this.txtUserPwd.Location.Y);
                this.toolTip.Show("请您输入用户密码！", this, showLocation, 1000);
                this.txtUserPwd.Focus();
                return false;
            }
            else if (userPwd.Length < 6)
            {
                this.toolTip.ToolTipIcon = ToolTipIcon.Warning;
                this.toolTip.ToolTipTitle = "登录警告";
                Point showLocation = new Point(
                    this.txtUserPwd.Location.X + this.txtUserPwd.Width,
                    this.txtUserPwd.Location.Y);
                this.toolTip.Show("用户密码长度不能小于六位！", this, showLocation, 1000);
                this.txtUserPwd.Focus();
                return false;
            }

            // 如果已通过以上所有验证则返回真
            return true;
        }

        /// <summary>
        /// 显示登录失败工具提示
        /// </summary>
        /// <param name="ex">引发登录失败的异常</param>
        private void ShowLoginLostToolTip(Exception ex)
        {
            // 如果登录界面未被释放
            if (!this.IsDisposed)
            {
                this.toolTip.ToolTipIcon = ToolTipIcon.Error;
                this.toolTip.ToolTipTitle = "登录失败";
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
        /// 带参构造
        /// </summary>
        public frmLogin()
        {
            // 构建设计器
            InitializeComponent();
        } 
        #endregion

        #region Event Handlers
        /// <summary>
        /// 登录按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLogin_Click(object sender, EventArgs e)
        {
            // 如果通过输入验证
            if (UserInputCheck())
            {
                try
                {

                    // 创建业务逻辑层工厂类实例
                    BLLFactory.BLLFactory bllFactory = new BLLFactory.BLLFactory();
                    // 调用工厂类实例方法创建业务逻辑管理类实例
                    IOperatorManager operatorManager = bllFactory.BuildOperatorManager();
                    // 获取当前登录操作员实体
                    currentOperator = operatorManager.GetOperatorInfoByName(
                        this.txtLoginName.Text.Trim(),
                        this.txtUserPwd.Text.Trim());

                    // 授权验证
                    if (currentOperator.RightsCollection == null)
                        throw new Exception(string.Format("操作员 [{0}] 无有效权限，禁止登录！", currentOperator.Name));

                    if (currentOperator != null)
                        if (this.txtUserPwd.Text.Trim() == currentOperator.Password)
                        {
                            // 清空输入
                            this.txtLoginName.Text = string.Empty;
                            this.txtUserPwd.Text = string.Empty;
                            this.txtLoginName.Focus();

                            // 标识验证通过
                            isPass = true;
                            this.DialogResult = DialogResult.OK;
                            this.Dispose();
                        }

                    // 如果未通过验证
                    if (!isPass)
                    {
                        throw new Exception("登录名称或用户密码不正确！");
                    }
                }
                catch (Exception ex)
                {
                    ShowLoginLostToolTip(ex);
                }
            }
        }

        /// <summary>
        /// 退出按钮单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 鼠标指针在窗体上方并按下按键事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmLogin_MouseDown(object sender, MouseEventArgs e)
        {
            // 仅响应鼠标左键点击事件
            if (e.Button == MouseButtons.Left)
            {
                // 保存旧坐标
                this._x = e.X;
                this._y = e.Y;

                // 标识窗体可移动
                this.isMove = true;
            }
        }

        /// <summary>
        /// 鼠标指针在窗体上方并移动鼠标指针事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmLogin_MouseMove(object sender, MouseEventArgs e)
        {
            // 如果可移动
            if (this.isMove)
            {
                // 根据旧坐标的相对偏移位置移动窗体
                // 方法一：
                // this.Left += e.X - this._x;
                // this.Top += e.Y - this._y;
                // 方法二：
                this.SetDesktopLocation(this.Left + e.X - this._x, this.Top + e.Y - this._y);

                // 在标题栏显示当前坐标
                string xPoint = this.Left.ToString().Trim();
                string yPoint = this.Top.ToString().Trim();
                this.Text = string.Format(
                    "「{0},{1}」",
                    xPoint.Length < 5 ? (xPoint.PadLeft(5)) : xPoint,
                    yPoint.Length < 5 ? (yPoint.PadRight(5)) : yPoint);
            }
        }

        /// <summary>
        /// 鼠标指针在窗体上方并释放按键事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmLogin_MouseUp(object sender, MouseEventArgs e)
        {
            // 标识窗体不可移动
            this.isMove = false;
        }

        #endregion
    }
}