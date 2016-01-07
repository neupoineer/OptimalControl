using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using IBLL;
using Model;
using OptimalControl.Common;

namespace OptimalControl.Forms
{
    /// <summary>
    /// 添加新用户界面
    /// </summary>
    public partial class frmOperatorManager : Form
    {
        #region Private Members
        /// <summary>
        /// 当前登录操作员实体
        /// </summary>
        Operator _currentOperator = null;
        /// <summary>
        /// 主界面被管理的菜单对象
        /// </summary>
        MenuStrip _msMain = null;
        /// <summary>
        /// 是否显示为修改密码界面
        /// </summary>
        bool _isModify = false;
        #endregion

        #region Private Methods
        /// <summary>
        /// 用户输入校验
        /// </summary>
        /// <returns>True:通过 / False:未通过</returns>
        private bool UserInputCheck()
        {
            string operatorName = txtOperatorName.Text.Trim();
            string operatorPwd = txtOperatorPwd.Text.Trim();
            string validatePwd = txtValidatePwd.Text.Trim();

            if (string.IsNullOrEmpty(operatorName))
            {
                this.toolTip.ToolTipIcon = ToolTipIcon.Info;
                this.toolTip.ToolTipTitle = !_isModify ? "添加提示" : "修改提示";
                Point showLocation = new Point(
                    this.txtOperatorName.Location.X + this.txtOperatorName.Width,
                    this.txtOperatorName.Location.Y);
                this.toolTip.Show(!_isModify ? "请输入登录名称！" : "请输入原始密码！", this, showLocation, 5000);
                this.txtOperatorName.Focus();
                return false;
            }

            // 如果为修改密码界面且原始密码不正确
            if (_isModify && operatorName != _currentOperator.Password.Trim())
            {
                this.toolTip.ToolTipIcon = ToolTipIcon.Warning;
                this.toolTip.ToolTipTitle = "修改警告";
                Point showLocation = new Point(
                    this.txtOperatorName.Location.X + this.txtOperatorName.Width,
                    this.txtOperatorName.Location.Y);
                this.toolTip.Show("输入的原始密码不正确！", this, showLocation, 5000);
                this.txtOperatorName.Focus();
                return false;
            }

            if (operatorPwd.Length < 6)
            {
                this.toolTip.ToolTipIcon = ToolTipIcon.Warning;
                this.toolTip.ToolTipTitle = !_isModify ? "添加警告" : "修改警告";
                Point showLocation = new Point(
                    this.txtOperatorPwd.Location.X + this.txtOperatorPwd.Width,
                    this.txtOperatorPwd.Location.Y);
                this.toolTip.Show("用户密码长度不能小于六位！", this, showLocation, 5000);
                this.txtOperatorPwd.Focus();
                return false;
            }

            if (validatePwd.Length < 6)
            {
                this.toolTip.ToolTipIcon = ToolTipIcon.Warning;
                this.toolTip.ToolTipTitle = !_isModify ? "添加警告" : "修改警告";
                Point showLocation = new Point(
                    this.txtValidatePwd.Location.X + this.txtValidatePwd.Width,
                    this.txtValidatePwd.Location.Y);
                this.toolTip.Show("确认密码长度不能小于六位！", this, showLocation, 5000);
                this.txtValidatePwd.Focus();
                return false;
            }

            if (operatorPwd != validatePwd)
            {
                this.toolTip.ToolTipIcon = ToolTipIcon.Warning;
                this.toolTip.ToolTipTitle = !_isModify ? "添加警告" : "修改警告";
                Point showLocation = new Point(
                    this.txtValidatePwd.Location.X + this.txtValidatePwd.Width,
                    this.txtValidatePwd.Location.Y);
                this.toolTip.Show("两次输入的密码必须一致！", this, showLocation, 5000);
                this.txtValidatePwd.Focus();
                return false;
            }

            return true;
        }

        /// <summary>
        /// 校验操作员是否已存在
        /// </summary>
        /// <returns>True:存在 / False:不存在</returns>
        private bool CheckOperatorExist()
        {
            try
            {
                // 创建工厂类实例
                BLLFactory.BLLFactory bllFactory = new BLLFactory.BLLFactory();
                // 创建业务逻辑管理类实例
                IOperatorManager operatorManager = bllFactory.BuildOperatorManager();

                // 校验操作员是否已存在
                if (operatorManager.CheckOperatorExist(this.txtOperatorName.Text.Trim()))
                {
                    this.toolTip.ToolTipIcon = ToolTipIcon.Warning;
                    this.toolTip.ToolTipTitle = "添加警告";
                    Point showLocation = new Point(
                        this.txtOperatorName.Location.X + this.txtOperatorName.Width,
                        this.txtOperatorName.Location.Y);
                    this.toolTip.Show(
                        string.Format("用户 [{0}] 已经存在，请另行指定！", 
                        this.txtOperatorName.Text.Trim()), 
                        this, showLocation, 5000);
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                        ex.Message,
                        "校验失败",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
            }

            return true;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// 无参构造
        /// </summary>
        /// <param name="currentOperator">当前登录操作员实体</param>
        /// <param name="msMain">被管理的主菜单对象</param>
        /// <param name="isModify">是否显示为修改密码界面</param>
        public frmOperatorManager(Operator currentOperator, MenuStrip msMain, bool isModify)
        {
            // 构建设计器控件
            InitializeComponent();
            // 保存当前登录操作员实体
            _currentOperator = currentOperator;
            // 保存传入的主界面被管理的菜单对象
            _msMain = msMain;
            // 保存界面显示要求
            _isModify = isModify;

            // 如果要求显示为修改密码界面
            if (_isModify)
            {
                this.Text = string.Format("修改操作员 [{0}] 的密码", _currentOperator.Name);
                this.chkOperatorState.Visible = false;
                this.btnSubmit.Text = "保存";

                this.lblOperatorName.Text = "原始密码(&O):";
                this.txtOperatorName.PasswordChar = '*';
            }
        } 
        #endregion

        #region Event Handlers
        /// <summary>
        /// [添加]/[保存]按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnASubmit_Click(object sender, EventArgs e)
        {
            // 创建工厂类实例
            BLLFactory.BLLFactory bllFactory = new BLLFactory.BLLFactory();
            // 创建业务逻辑管理类实例
            IOperatorManager operatorManager = bllFactory.BuildOperatorManager();

            // 如果不要求显示为修改密码界面
            if (!_isModify)
            {
                // 如果通过输入校验
                if (UserInputCheck())
                {
                    try
                    {
                        // 校验操作员是否已存在
                        if (!CheckOperatorExist())
                            return;

                        // 创建新的操作员实体
                        Operator addOperator = new Operator();
                        addOperator.Name = this.txtOperatorName.Text.Trim();
                        addOperator.Password = this.txtOperatorPwd.Text.Trim();
                        addOperator.RightsCollection = new Dictionary<string, Rights>();
                        // 创建权限菜单管理类实例
                        RightsMenuDataManager rmManager = new RightsMenuDataManager();
                        // 创建权限列表结构
                        addOperator.RightsCollection = rmManager.ReadMenuRightsItem(_msMain, addOperator.RightsCollection);
                        addOperator.State = this.chkOperatorState.Checked;

                        // 执行添加
                        if (!operatorManager.AddOperator(addOperator))
                        {
                            this.toolTip.ToolTipIcon = ToolTipIcon.Error;
                            this.toolTip.ToolTipTitle = "添加失败";
                            Point showLocation = new Point(
                                this.txtOperatorName.Location.X + this.txtOperatorName.Width,
                                this.txtOperatorName.Location.Y);
                            this.toolTip.Show(string.Format("未能将用户 [{0}] 添加到本系统！", addOperator.Name), this, showLocation, 5000);
                        }
                        else
                        {
                            if (!addOperator.State)
                                MessageBox.Show(
                                    string.Format(
                                    "用户 [{0}] 已经添加但未激活。\r\n\r\n您可以手动激或为其赋予权限。",
                                    addOperator.Name),
                                    "添加成功",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                            else
                                MessageBox.Show(
                                    string.Format(
                                    "用户 [{0}] 已经添加并已激活。\r\n\r\n请为其赋予适当的系统权限。",
                                    addOperator.Name),
                                    "添加成功",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);

                            this.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(
                            ex.Message,
                            "添加失败",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }
            }
            // 如果要求显示为修改密码界面
            else
            {
                // 如果通过输入校验
                if (UserInputCheck())
                {
                    // 保存旧密码
                    string oldPwd = _currentOperator.Password;
                    // 修改当前登录操作员实体的密码信息
                    _currentOperator.Password = this.txtOperatorPwd.Text.Trim();

                    try
                    {
                        // 执行修改
                        if (operatorManager.ModifyOperator(_currentOperator))
                        {
                            MessageBox.Show(
                                    string.Format(
                                    "用户 [{0}] 的密码修改成功！",
                                    _currentOperator.Name),
                                    "修改成功",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show(
                                    string.Format(
                                    "未能修改用户 [{0}] 的密码！",
                                    _currentOperator.Name),
                                    "修改失败",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);

                            // 修改失败则还原旧密码
                            _currentOperator.Password = oldPwd;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(
                            ex.Message,
                            "修改失败",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);

                        // 修改失败则还原旧密码
                        _currentOperator.Password = oldPwd;
                    }
                }
            }
        }

        /// <summary>
        /// [取消]按钮点击事件
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