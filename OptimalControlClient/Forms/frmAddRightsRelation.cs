using System;
using System.Collections.Generic;
using System.Windows.Forms;
using IBLL;
using Model;

namespace OptimalControl.Forms
{
    /// <summary>
    /// 添加权限关系界面
    /// </summary>
    public partial class frmAddRightsRelation : Form
    {
        #region Private Members
        /// <summary>
        /// 保存权限组管理界面
        /// </summary>
        frmRightsGroupManager _frmRightsGroupManager = null; 
        #endregion

        #region Public Methods
        /// <summary>
        /// 无参构造
        /// </summary>
        public frmAddRightsRelation(frmRightsGroupManager frmRightsGroupManager)
        {
            // 构造设计器控件
            InitializeComponent();

            // 保存传入的权限组管理界面
            _frmRightsGroupManager = frmRightsGroupManager;
        } 
        #endregion

        #region Event Handlers
        /// <summary>
        /// 窗体加载事件　
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmAddRightsRelation_Load(object sender, EventArgs e)
        {
            // 创建工厂类实例
            BLLFactory.BLLFactory bllFactory = new BLLFactory.BLLFactory();

            try
            {
                // 创建操作员管理类实例
                IOperatorManager operatorManager = bllFactory.BuildOperatorManager();
                // 调用实例方法　
                Dictionary<string, Operator> operatorCollection = operatorManager.GetAllOperatorInfo();

                // 如果包含操作员信息
                if (operatorCollection.Count > 0)
                {
                    // 绑定数据显示
                    BindingSource operatorSource = new BindingSource();
                    operatorSource.DataSource = operatorCollection.Values;
                    cboOperatorName.DataSource = operatorSource;
                    cboOperatorName.DisplayMember = "Name";
                    cboOperatorName.ValueMember = "Id";
                }
                else
                    MessageBox.Show(
                        "未找到任何操作员信息，请在 [权限管理] 中添加新的操作员！",
                        "加载警告",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                // 创建权限管理类实例
                IRightsGroupManager rightsGroupManager = bllFactory.BuildRightsGroupManager();
                // 调用实例方法
                Dictionary<string, RightsGroup> rightsGroupCollection = rightsGroupManager.GetAllRightsGroupInfo();

                // 如果包含权限组信息
                if (rightsGroupCollection.Count > 0)
                {
                    // 绑定数据显示
                    BindingSource rightsGroupSource = new BindingSource();
                    rightsGroupSource.DataSource = rightsGroupCollection.Values;
                    cboGroupName.DataSource = rightsGroupSource;
                    cboGroupName.DisplayMember = "Name";
                    cboGroupName.ValueMember = "Id";
                }
                else
                    MessageBox.Show(
                    "未找到任何操作员信息，请在 [权限组管理] 中添加新的权限组！",
                    "加载警告",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "加载失败",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// [添加]按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cboOperatorName.Text) ||
                string.IsNullOrEmpty(cboGroupName.Text))
            {
                MessageBox.Show(
                    "[操作员] 和 [权限组] 均为必须填项！",
                    "添加提示",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                cboOperatorName.Focus();

                return;
            }

            // 创建工厂类实例
            BLLFactory.BLLFactory bllFactory = new BLLFactory.BLLFactory();

            try
            {
                // 创建权限关系管理类实例
                IRightsRelationManager rightsRelationManager = bllFactory.BuildRightsRelationManager();
                // 调用实例方法
                List<RightsRelation> rightsRelationList =
                    rightsRelationManager.GetRightsRelationByOperatorId(
                    Convert.ToInt32(cboOperatorName.SelectedValue));

                // 校验当前选择的关系是否已存在
                foreach (RightsRelation tmpRightsRelation in rightsRelationList)
                {
                    if (tmpRightsRelation.RightsGroupId ==
                        Convert.ToInt32(cboGroupName.SelectedValue))
                    {
                        MessageBox.Show(
                            string.Format("用户 [{0}] 已经隶属于权限组 [{1}]！",
                            cboOperatorName.Text,
                            cboGroupName.Text),
                            "添加警告",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        return;
                    }
                }

                // 执行添加操作
                RightsRelation rightsRelation = new RightsRelation();
                rightsRelation.OperatorId = Convert.ToInt32(cboOperatorName.SelectedValue);
                rightsRelation.RightsGroupId = Convert.ToInt32(cboGroupName.SelectedValue);
                if (!rightsRelationManager.AddRightsRelation(rightsRelation))
                {
                    MessageBox.Show(
                        "添加权限关系信息失败！",
                        "添加失败",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show(
                        "添加权限关系信息成功！",
                        "添加成功",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    // 如果权限组管理界面未被释放
                    if (!_frmRightsGroupManager.IsDisposed)
                    {
                        // 调用权限组管理界面的数据刷新
                        _frmRightsGroupManager.RefreshDataDisplay();
                        // 选择最末项并保持可见
                        _frmRightsGroupManager.SelectLastRelation();
                    }
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