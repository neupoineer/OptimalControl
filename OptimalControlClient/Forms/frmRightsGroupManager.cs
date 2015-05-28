using System;
using System.Collections.Generic;
using System.Windows.Forms;
using IBLL;
using Model;
using OptimalControl.Common;

namespace OptimalControl.Forms
{
    /// <summary>
    /// 权限组管理界面
    /// </summary>
    public partial class frmRightsGroupManager : Form
    {
        #region Private Members
        /// <summary>
        /// 当前所有操作员信息集合
        /// </summary>
        Dictionary<string, Operator> _operatorCollection = null;
        /// <summary>
        /// 当前所有权限分组信息集合
        /// </summary>
        Dictionary<string, RightsGroup> _rightsGroupCollection = null;
        /// <summary>
        /// 当前所有权限关系信息集合
        /// </summary>
        List<RightsRelation> _rightsRelationList = null;
        /// <summary>
        /// 权限菜单界面管理类对象
        /// </summary>
        RightsMenuUIManager _rmuManager = null;
        /// <summary>
        /// 权限菜单数据管理类对象
        /// </summary>
        RightsMenuDataManager _rmdManager = null;
        /// <summary>
        /// 激活当前权限组管理界面的权限管理界面对象
        /// </summary>
        frmRightsManager _frmRightsManager = null;
        /// <summary>
        /// 系统主界面
        /// </summary>
        FrmMain _frmMain = null;
        /// <summary>
        /// 当前编辑的权限组名称
        /// </summary>
        string _currentEditGroupName = null;
        /// <summary>
        /// 权限视图可勾选状态
        /// </summary>
        bool _rightsViewIsChecked = true;
        /// <summary>
        /// 保存上一次选中的权限组数据行
        /// </summary>
        DataGridViewRow _lastSelectedRightsGroupRow = null;
        #endregion

        #region Private Methods
        /// <summary>
        /// 根据权限组名称获取权限组 ID
        /// </summary>
        /// <param name="rightsGroupName">权限组名称</param>
        /// <returns>权限组 ID</returns>
        private int GetRightsGroupIdByRightsGroupName(string rightsGroupName)
        {
            int id = 0;
            foreach (RightsGroup tmpRightsGroup in _rightsGroupCollection.Values)
            {
                if (tmpRightsGroup.Name == rightsGroupName)
                {
                    id = tmpRightsGroup.Id;
                    break;
                }
            }
            return id;
        }
        /// <summary>
        /// 根据操作员名称获取操作员 ID
        /// </summary>
        /// <param name="operatorName">操作员名称</param>
        /// <returns>操作员 ID</returns>
        private int GetOperatorIdByOperatorName(string operatorName)
        {
            int id = 0;
            foreach (Operator tmpOperator in _operatorCollection.Values)
            {
                if (tmpOperator.Name == operatorName)
                {
                    id = tmpOperator.Id;
                    break;
                }
            }
            return id;
        }
        /// <summary>
        /// 选中/取消树节点及其子节点勾选状态
        /// </summary>
        /// <param name="currentTreeNode">当前操作的节点</param>
        private void CheckOrUnCheckTreeNode(TreeNode currentTreeNode)
        {
            // 如果有选中单元格
            if (dgvGroupList.SelectedCells.Count > 0)
            {
                // 获取当前选中的权限组名称
                string groupName = dgvGroupList.SelectedCells[0].OwningRow.Cells["Name"].Value.ToString();

                // 同步权限状态
                _rightsGroupCollection[groupName].GroupRightsCollection[currentTreeNode.Tag.ToString()].RightsState = currentTreeNode.Checked;

                // 同时选中/取消子节点的勾选
                foreach (TreeNode childTreeNode in currentTreeNode.Nodes)
                {
                    // 同步子节点勾选状态
                    childTreeNode.Checked = currentTreeNode.Checked;
                    // 递归勾选下层子节点
                    CheckOrUnCheckTreeNode(childTreeNode);
                }
            }
        }
        /// <summary>
        /// 读取并绑定权限组数据显示
        /// </summary>
        private void BindDataToRightsGroupList()
        {
            try
            {
                // 创建工厂类实例
                BLLFactory.BLLFactory bllFactory = new BLLFactory.BLLFactory();
                // 创建权限组管理类实例
                IRightsGroupManager rightsGroupManager = bllFactory.BuildRightsGroupManager();
                // 调用实例方法
                _rightsGroupCollection = rightsGroupManager.GetAllRightsGroupInfo();

                // 遍历取得的权限组集合
                foreach (RightsGroup tmpRightsGroup in _rightsGroupCollection.Values)
                {
                    // 如果当前权限组的权限集合为空则创建新的空白权限
                    if (!(tmpRightsGroup.GroupRightsCollection is Dictionary<string, Rights>))
                    {
                        tmpRightsGroup.GroupRightsCollection = new Dictionary<string, Rights>();
                        tmpRightsGroup.GroupRightsCollection =
                            _rmdManager.ReadMenuRightsItem(tmpRightsGroup.GroupRightsCollection);
                    }
                }

                // 如果包含权限组信息
                if (_rightsGroupCollection.Count > 0)
                {
                    // 绑定权限组数据显示
                    BindingSource source = new BindingSource();
                    source.DataSource = _rightsGroupCollection.Values;
                    dgvGroupList.DataSource = source;

                    // 设置中文列名
                    dgvGroupList.Columns["Id"].HeaderText = "分组编号";
                    dgvGroupList.Columns["Id"].ToolTipText = "[只读列]";
                    dgvGroupList.Columns["Id"].DisplayIndex = 0;
                    dgvGroupList.Columns["Id"].ReadOnly = true;
                    dgvGroupList.Columns["Id"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                    dgvGroupList.Columns["Name"].HeaderText = "分组名称";
                    dgvGroupList.Columns["Name"].DisplayIndex = 1;
                    dgvGroupList.Columns["GroupRightsCollection"].HeaderText = "分组权限";
                    dgvGroupList.Columns["GroupRightsCollection"].DisplayIndex = 2;
                    dgvGroupList.Columns["GroupRightsCollection"].ReadOnly = true;
                    dgvGroupList.Columns["GroupRightsCollection"].Visible = false;

                    // 设置工具提示
                    foreach (DataGridViewRow dgvRow in dgvGroupList.Rows)
                    {
                        foreach (DataGridViewCell dgvCell in dgvRow.Cells)
                        {
                            if (dgvCell.ReadOnly)
                                dgvCell.ToolTipText = "[只读格]";
                            else
                                dgvCell.ToolTipText = "[可写格]";
                        }
                    }
                }
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
        /// 读取并绑定权限关系数据显示
        /// </summary>
        private void BindDataToRightsRelationList()
        {
            try
            {
                // 创建工厂类实例
                BLLFactory.BLLFactory bllFactory = new BLLFactory.BLLFactory();
                // 创建权限关系管理类实例
                IRightsRelationManager rightsRelationManager = bllFactory.BuildRightsRelationManager();
                // 调用实例方法
                _rightsRelationList = rightsRelationManager.GetAllRightsRelation();
                // 创建操作管理类实例
                IOperatorManager operatorManager = bllFactory.BuildOperatorManager();
                // 调用实例方法
                _operatorCollection = operatorManager.GetAllOperatorInfo();

                // 清除原有的列
                dgvRightsRelationList.Columns.Clear();

                // 手动创建数据列
                //
                // dgvColId
                // 
                DataGridViewTextBoxColumn dgvColId = new DataGridViewTextBoxColumn();
                dgvColId.Name = "dgvColId";
                dgvColId.HeaderText = "关系编号";
                dgvColId.ToolTipText = "[只读列]";
                dgvColId.DisplayIndex = 0;
                dgvColId.ReadOnly = true;
                dgvColId.DataPropertyName = "Id";
                dgvColId.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                //
                // dgvColOperatorId
                // 
                DataGridViewTextBoxColumn dgvColOperatorId = new DataGridViewTextBoxColumn();
                dgvColOperatorId.Name = "dgvColOperatorId";
                dgvColOperatorId.HeaderText = "用户编号";
                dgvColOperatorId.ToolTipText = "[只读列]";
                dgvColOperatorId.DisplayIndex = 1;
                dgvColOperatorId.ReadOnly = true;
                dgvColOperatorId.DataPropertyName = "OperatorId";
                dgvColOperatorId.Visible = false;
                dgvColOperatorId.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                //
                // dgvColOperatorName
                // 
                DataGridViewComboBoxColumn dgvColOperatorName = new DataGridViewComboBoxColumn();
                dgvColOperatorName.Name = "dgvColOperatorName";
                dgvColOperatorName.HeaderText = "用户名称";
                dgvColOperatorName.DisplayIndex = 2;
                dgvColOperatorName.DataPropertyName = "OperatorName";
                BindingSource operatorNameSource = new BindingSource();
                operatorNameSource.DataSource = _operatorCollection.Values;
                dgvColOperatorName.DataSource = operatorNameSource;
                dgvColOperatorName.DisplayMember = "Name";
                dgvColOperatorName.ValueMember = "Name";
                //
                // dgvColRightsGroupId
                // 
                DataGridViewTextBoxColumn dgvColRightsGroupId = new DataGridViewTextBoxColumn();
                dgvColRightsGroupId.Name = "dgvColRightsGroupId";
                dgvColRightsGroupId.HeaderText = "分组编号";
                dgvColRightsGroupId.ToolTipText = "[只读列]";
                dgvColRightsGroupId.DisplayIndex = 3;
                dgvColRightsGroupId.ReadOnly = true;
                dgvColRightsGroupId.DataPropertyName = "RightsGroupId";
                dgvColRightsGroupId.Visible = false;
                dgvColRightsGroupId.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                //
                // dgvColRightsGroupName
                // 
                DataGridViewComboBoxColumn dgvColRightsGroupName = new DataGridViewComboBoxColumn();
                dgvColRightsGroupName.Name = "dgvColRightsGroupName";
                dgvColRightsGroupName.HeaderText = "分组名称";
                dgvColRightsGroupName.DisplayIndex = 4;
                dgvColRightsGroupName.DataPropertyName = "RightsGroupName";
                BindingSource rightsGroupSource = new BindingSource();
                rightsGroupSource.DataSource = _rightsGroupCollection.Values;
                dgvColRightsGroupName.DataSource = rightsGroupSource;
                dgvColRightsGroupName.DisplayMember = "Name";
                dgvColRightsGroupName.ValueMember = "Name";

                // 添加新建的列
                dgvRightsRelationList.Columns.AddRange(new DataGridViewColumn[] {
                dgvColId,
                dgvColOperatorId,
                dgvColOperatorName,
                dgvColRightsGroupId,
                dgvColRightsGroupName});

                //// 手动添加数据行(根据权限关系集合数量)
                //dgvRightsRelationList.Rows.Add(_rightsRelationList.Count);
                //// 手动为数据行赋值
                //foreach (Model.RightsRelation tmpRightsRelation in _rightsRelationList)
                //{
                //    dgvRightsRelationList.Rows[tmpRightsRelation.Id - 1].Cells["dgvColId"].Value = tmpRightsRelation.Id;
                //    dgvRightsRelationList.Rows[tmpRightsRelation.Id - 1].Cells["dgvColOperatorId"].Value = tmpRightsRelation.OperatorId;
                //    dgvRightsRelationList.Rows[tmpRightsRelation.Id - 1].Cells["dgvColOperatorName"].Value = tmpRightsRelation.OperatorId;
                //    dgvRightsRelationList.Rows[tmpRightsRelation.Id - 1].Cells["dgvColRightsGroupId"].Value = tmpRightsRelation.RightsGroupId;
                //    dgvRightsRelationList.Rows[tmpRightsRelation.Id - 1].Cells["dgvColRightsGroupName"].Value = tmpRightsRelation.RightsGroupId;
                //}

                // 使用绑定显示数据行
                BindingSource source = new BindingSource();
                source.DataSource = _rightsRelationList;
                dgvRightsRelationList.AutoGenerateColumns = false;
                dgvRightsRelationList.DataSource = source;

                // 设置工具提示
                foreach (DataGridViewRow dgvRow in dgvRightsRelationList.Rows)
                {
                    foreach (DataGridViewCell dgvCell in dgvRow.Cells)
                    {
                        if (dgvCell.ReadOnly)
                            dgvCell.ToolTipText = "[只读格]";
                        else
                            dgvCell.ToolTipText = "[可写格]";
                    }
                }
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
        /// 绑定权限组树形视图数据显示
        /// </summary>
        private void BindDataToGroupView()
        {
            // 禁用树视图重绘
            tvGroupView.BeginUpdate();

            // 清除所有树节点
            tvGroupView.Nodes.Clear();

            try
            {
                // 遍历当前权限关系列表
                foreach (RightsRelation rootRightsRelation in _rightsRelationList)
                {
                    // 创建根节点
                    TreeNode rootTreeNode = null;
                    // 是否存在此根节点
                    bool rootIsExist = false;
                    foreach (TreeNode tmpRootTreeNode in tvGroupView.Nodes)
                    {
                        if (Convert.ToInt32(tmpRootTreeNode.Tag) == rootRightsRelation.OperatorId)
                        {
                            rootTreeNode = tmpRootTreeNode;
                            rootIsExist = true;
                            break;
                        }
                    }

                    // 如果不存在此根节点
                    if (!rootIsExist)
                    {
                        rootTreeNode = new TreeNode(rootRightsRelation.OperatorName);
                        rootTreeNode.Tag = rootRightsRelation.OperatorId;

                        // 将根节点添加到树
                        tvGroupView.Nodes.Add(rootTreeNode);
                    }

                    // 遍历添加子节点
                    foreach (RightsRelation tmpRightsRelation in _rightsRelationList)
                    {
                        if (tmpRightsRelation.OperatorId == Convert.ToInt32(rootRightsRelation.OperatorId))
                        {
                            // 创建子节点
                            TreeNode childTreeNode = null;
                            // 是否存在此子节点
                            bool childIsExist = false;
                            foreach (TreeNode tmpChildTreeNode in rootTreeNode.Nodes)
                            {
                                if (Convert.ToInt32(tmpChildTreeNode.Tag) == tmpRightsRelation.RightsGroupId)
                                {
                                    childTreeNode = tmpChildTreeNode;
                                    childIsExist = true;
                                    break;
                                }
                            }

                            // 如果不存在此子节点
                            if (!childIsExist)
                            {
                                childTreeNode = new TreeNode(tmpRightsRelation.RightsGroupName);
                                childTreeNode.Tag = tmpRightsRelation.RightsGroupId;

                                // 将子节点添加到根节点
                                rootTreeNode.Nodes.Add(childTreeNode);
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "加载失败",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }

            // 展开所有树节点
            tvGroupView.ExpandAll();

            // 启用树视图重绘
            tvGroupView.EndUpdate();
        }

        /// <summary>
        /// 保存所有更改
        /// </summary>
        /// <param name="isShowTip">是否显示保存成功的提示</param>
        private void SaveAllChanged(bool isShowTip)
        {
            // 创建工厂类实例
            BLLFactory.BLLFactory bllFactory = new BLLFactory.BLLFactory();

            // 提交并结束数据视图的单元格编辑
            dgvGroupList.EndEdit();
            dgvRightsRelationList.EndEdit();

            // 保存权限关系是否成功
            bool SaveRightsRelationIsOk = true;

            // 保存权限关系信息
            try
            {
                IRightsRelationManager rightsRelationManager = bllFactory.BuildRightsRelationManager();
                // 调用实例方法
                foreach (RightsRelation tmpRightsRelation in _rightsRelationList)
                {
                    if (!rightsRelationManager.ModifyRightsRelation(tmpRightsRelation))
                    {
                        SaveRightsRelationIsOk = false;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                SaveRightsRelationIsOk = false;
                MessageBox.Show(
                    ex.Message,
                    "保存失败",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }

            if (!SaveRightsRelationIsOk)
                MessageBox.Show(
                    "保存权限关系信息失败！",
                    "保存失败",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

            // 保存权限组信息是否成功
            bool SaveRightsGroupIsOk = true;

            // 保存权限组信息
            try
            {
                IRightsGroupManager rightsGroupManager = bllFactory.BuildRightsGroupManager();
                // 调用实例方法
                foreach (RightsGroup tmpRightsGroup in _rightsGroupCollection.Values)
                {
                    // 如果当前权限组的权限集合为空则创建新的空白权限
                    if (!(tmpRightsGroup.GroupRightsCollection is Dictionary<string, Rights>))
                    {
                        tmpRightsGroup.GroupRightsCollection = new Dictionary<string, Rights>();
                        tmpRightsGroup.GroupRightsCollection =
                            _rmdManager.ReadMenuRightsItem(tmpRightsGroup.GroupRightsCollection);
                    }

                    if (!rightsGroupManager.ModifyRightsGroup(tmpRightsGroup))
                    {
                        SaveRightsGroupIsOk = false;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                SaveRightsGroupIsOk = false;
                MessageBox.Show(
                    ex.Message,
                    "保存失败",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }

            if (!SaveRightsGroupIsOk)
                MessageBox.Show(
                    "保存权限组信息失败！",
                    "保存失败",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

            // 保存操作员信息是否成功
            bool SaveOperatorIsOk = true;

            // 根据操作员所隶属的权限组刷新并保存操作员权限
            try
            {
                // 将所有操作员权限状态置为隐藏状态
                foreach (Operator tmpOperator in _operatorCollection.Values)
                    foreach (Rights tmpRights in tmpOperator.RightsCollection.Values)
                        tmpRights.RightsState = false;

                // 遍历权限关系实体列表
                foreach (RightsRelation tmpRightsRelation in _rightsRelationList)
                    // 遍历操作员实体集合
                    foreach (Operator tmpOperator in _operatorCollection.Values)
                        // 如果操作员名称与权限关系中相同
                        if (tmpOperator.Name == tmpRightsRelation.OperatorName)
                            // 遍历权限组实体权限集合
                            foreach (Rights tmpGroupRights in _rightsGroupCollection[tmpRightsRelation.RightsGroupName].GroupRightsCollection.Values)
                                // 遍历操作员权限集合
                                foreach (Rights tmpRights in tmpOperator.RightsCollection.Values)
                                    // 如果权限名称相同
                                    if (tmpRights.Name == tmpGroupRights.Name)
                                        // 如果组权限为显示状态则将组权限赋予给操作员权限
                                        if (tmpGroupRights.RightsState)
                                            tmpRights.RightsState = tmpGroupRights.RightsState;

                // 创建工厂类实例
                IOperatorManager operatorManager = bllFactory.BuildOperatorManager();

                // 遍历保存操作员权限
                foreach (Operator tmpOperator in _operatorCollection.Values)
                {
                    if (!operatorManager.ModifyOperator(tmpOperator))
                    {
                        SaveOperatorIsOk = false;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                SaveOperatorIsOk = false;
                MessageBox.Show(
                    ex.Message,
                    "保存失败",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }

            if (!SaveOperatorIsOk)
                MessageBox.Show(
                    "保存操作员信息失败！",
                    "保存失败",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

            if (isShowTip)
            {
                // 给出保存成功提示
                if (SaveRightsRelationIsOk && SaveRightsGroupIsOk && SaveOperatorIsOk)
                    MessageBox.Show(
                        "保存所有权限分组及其相关信息成功！",
                        "保存成功",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
            }
        }

        #endregion

        #region Internal Methods
        /// <summary>
        /// 刷新数据显示
        /// </summary>
        internal void RefreshDataDisplay()
        {
            // 绑定权限组数据显示
            BindDataToRightsGroupList();
            // 绑定权限关系数据显示
            BindDataToRightsRelationList();
            // 绑定树形视图数据显示
            BindDataToGroupView();

            // 加载权限树形视图数据显示(绑定当前选定权限组的权限数据)
            if (dgvGroupList.SelectedCells.Count > 0)
            {
                // 保存权限组名称
                string groupName = dgvGroupList.SelectedCells[0].OwningRow.Cells["Name"].Value.ToString();
                // 标识是否存在此权限组
                bool isExistGroup = false;
                // 校验指定权限组名称是否存在
                foreach (RightsGroup tmpRightsGroup in _rightsGroupCollection.Values)
                {
                    if (tmpRightsGroup.Name == groupName)
                    {
                        // 绑定树形视图显示
                        _rmuManager.BindDataToTreeView(
                        _rightsGroupCollection[groupName].GroupRightsCollection);
                        isExistGroup = true;
                        break;
                    }
                }
                // 如果不存在此权限组
                if (!isExistGroup)
                {
                    // 清空权限组列表数据行
                    dgvGroupList.Rows.Clear();
                    // 清空数据树形视图显示
                    tvRightsView.Nodes.Clear();
                }
            }
        }

        /// <summary>
        /// 选择最末项权限关系并保持可见
        /// </summary>
        internal void SelectLastRelation()
        {
            // 选中最末项并使其滚动保持可见
            if (dgvRightsRelationList.Rows.Count > 0)
            {
                dgvRightsRelationList.Rows[dgvRightsRelationList.Rows.Count - 1].Selected = true;
                dgvRightsRelationList.FirstDisplayedScrollingRowIndex = dgvRightsRelationList.Rows.Count - 1;
            }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// 无参构造
        /// </summary>
        /// <param name="frmRightsManager">权限管理界面</param>
        /// <param name="frmMain">系统主界面</param>
        public frmRightsGroupManager(frmRightsManager frmRightsManager, FrmMain frmMain)
        {
            InitializeComponent();

            // 保存权限管理界面对象
            _frmRightsManager = frmRightsManager;
            // 保存系统主界面
            _frmMain = frmMain;

            // 创建权限菜单界面管理类实例
            _rmuManager = new RightsMenuUIManager();
            // 赋予要操作的对象
            _rmuManager.TvRightsView = this.tvRightsView;
            _rmuManager.MsMain = _frmRightsManager.MsMain;

            // 创建权限菜单数据管理类实例
            _rmdManager = new RightsMenuDataManager();
            // 赋予要操作的对象
            _rmdManager.MsMain = _frmRightsManager.MsMain;
        } 
        #endregion

        #region Event Handlers
        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmRightsGroupManager_Load(object sender, EventArgs e)
        {
            // 刷新数据显示
            RefreshDataDisplay();
        }

        /// <summary>
        /// [权限组列表]单元格启动编辑模式事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvGroupList_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            // 保存当前选中的权限组名称
            _currentEditGroupName = Convert.ToString(dgvGroupList.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
        }

        /// <summary>
        /// [权限组列表]单元格停止编辑模式事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvGroupList_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            // 如果是权限组名列
            if (dgvGroupList.Columns[e.ColumnIndex].Name == "Name")
            {
                // 获取当前选中的权限组 ID
                int groupId = Convert.ToInt32(dgvGroupList.Rows[e.RowIndex].Cells["Id"].Value);
                // 获取当前选中的权限组名称
                string groupName = Convert.ToString(dgvGroupList.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                // 修改当前权限组项内容
                _rightsGroupCollection[_currentEditGroupName].Name = groupName;

                try
                {
                    // 创建权限组业务逻辑工厂类实例
                    BLLFactory.BLLFactory bllFactory = new BLLFactory.BLLFactory();
                    // 创建权限组业务逻辑管理类实例
                    IRightsGroupManager rightsGroupManager = bllFactory.BuildRightsGroupManager();
                    // 如果当前权限组权限集合为空则创建新的权限结构
                    if (!(_rightsGroupCollection[_currentEditGroupName].GroupRightsCollection is
                        Dictionary<string, Rights>))
                    {
                        // 创建新的权限结构
                        _rightsGroupCollection[_currentEditGroupName].GroupRightsCollection =
                            new Dictionary<string, Rights>();
                        // 读取空白权限
                        _rightsGroupCollection[_currentEditGroupName].GroupRightsCollection =
                            _rmdManager.ReadMenuRightsItem(_rightsGroupCollection[_currentEditGroupName].GroupRightsCollection);
                    }
                    // 调用实例方法
                    if (!rightsGroupManager.ModifyRightsGroup(_rightsGroupCollection[_currentEditGroupName]))
                    {
                        MessageBox.Show(
                            string.Format("权限组 [{0}] 的信息修改失败！", _currentEditGroupName),
                            "修改提示",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        return;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                    ex.Message,
                    "修改失败",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                }
                finally
                {
                    // 创建工厂类实例
                    BLLFactory.BLLFactory bllFactory = new BLLFactory.BLLFactory();
                    // 创建权限组管理类实例
                    IRightsGroupManager rightsGroupManager = bllFactory.BuildRightsGroupManager();
                    // 调用实例方法重新读取权限组信息
                    _rightsGroupCollection = rightsGroupManager.GetAllRightsGroupInfo();

                    // 绑定权限关系数据显示
                    BindDataToRightsRelationList();
                    // 绑定树形视图数据显示
                    BindDataToGroupView();
                }
            }
        }

        /// <summary>
        /// [权限组列表]选定内容更改事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvGroupList_SelectionChanged(object sender, EventArgs e)
        {
            // 启用权限视图可勾选状态
            _rightsViewIsChecked = true;

            // 如果有选中单元格
            if (dgvGroupList.SelectedCells.Count > 0)
            {
                // 如果不是上次选中的行
                if (_lastSelectedRightsGroupRow != dgvGroupList.SelectedCells[0].OwningRow)
                {
                    // 如果有选中单元格
                    if (dgvGroupList.SelectedCells.Count > 0)
                    {
                        // 获取当前选中的权限组名称
                        string groupName = dgvGroupList.SelectedCells[0].OwningRow.Cells["Name"].Value.ToString();

                        // 如果当前权限组的权限集合为空则创建新的空白权限
                        if (!(_rightsGroupCollection[groupName].GroupRightsCollection is Dictionary<string, Rights>))
                        {
                            _rightsGroupCollection[groupName].GroupRightsCollection = new Dictionary<string, Rights>();
                            _rightsGroupCollection[groupName].GroupRightsCollection =
                                _rmdManager.ReadMenuRightsItem(_rightsGroupCollection[groupName].GroupRightsCollection);
                        }

                        // 清除原有的单元格选择
                        dgvRightsRelationList.ClearSelection();
                        // 同步选中权限关系列表中的所有此操作员的此权限组单元格
                        foreach (DataGridViewRow dgvRow in dgvRightsRelationList.Rows)
                        {
                            if (dgvRow.Cells["dgvColRightsGroupName"].Value.ToString() == groupName)
                                dgvRow.Cells["dgvColRightsGroupName"].Selected = true;
                        }

                        // 加载权限树形视图数据显示(绑定当前选择的权限组权限数据)
                        _rmuManager.BindDataToTreeView(_rightsGroupCollection[groupName].GroupRightsCollection);
                    }

                    // 保存当前选中的数据行
                    _lastSelectedRightsGroupRow = dgvGroupList.SelectedCells[0].OwningRow;
                }
            }
        }

        /// <summary>
        /// [权限关系列表]单元格停止编辑模式事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvRightsRelationList_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            switch (dgvRightsRelationList.Columns[e.ColumnIndex].Name)
            {
                case "dgvColOperatorName":
                    _rightsRelationList[e.RowIndex].OperatorName = 
                        Convert.ToString(dgvRightsRelationList.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                    _rightsRelationList[e.RowIndex].OperatorId =
                        GetOperatorIdByOperatorName(
                        Convert.ToString(dgvRightsRelationList.Rows[e.RowIndex].Cells[e.ColumnIndex].Value));
                    break;
                case "dgvColRightsGroupName":
                    _rightsRelationList[e.RowIndex].RightsGroupName =
                        Convert.ToString(dgvRightsRelationList.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                    _rightsRelationList[e.RowIndex].RightsGroupId =
                        GetRightsGroupIdByRightsGroupName(
                        Convert.ToString(dgvRightsRelationList.Rows[e.RowIndex].Cells[e.ColumnIndex].Value));
                    break;
            }

            // 更改显示 
            dgvRightsRelationList.CurrentRow.Cells["dgvColOperatorId"].Value = 
                _rightsRelationList[e.RowIndex].OperatorId;
            dgvRightsRelationList.CurrentRow.Cells["dgvColRightsGroupId"].Value =
                _rightsRelationList[e.RowIndex].RightsGroupId;

            // 绑定权限组视图数据显示
            BindDataToGroupView();
        }

        /// <summary>
        /// [保存更改]工具栏按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnSaveAllChanged_Click(object sender, EventArgs e)
        {
            // 保存所有更改并要求显示保存成功提示
            SaveAllChanged(true);

            // 刷新数据显示
            RefreshDataDisplay();
        }

        /// <summary>
        /// [关闭窗口]工具栏按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnCloseWindow_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// [权限组视图]更改选定内容事件 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvGroupView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                // 在选择操作员名称时
                if (e.Node.Level == 0)
                {
                    // 禁用权限视图可勾选状态
                    _rightsViewIsChecked = false;

                    if (!(_operatorCollection[e.Node.Text].RightsCollection is 
                        Dictionary<string, Rights>))
                    {
                        // 为当前选择的操作员创建新的空权限数据
                        _operatorCollection[e.Node.Text].RightsCollection =
                            new Dictionary<string, Rights>();
                        _operatorCollection[e.Node.Text].RightsCollection =
                            _rmdManager.ReadMenuRightsItem(_operatorCollection[e.Node.Text].RightsCollection);
                    }

                    // 加载权限树形视图数据显示(绑定当前选择的操作员权限数据)
                    _rmuManager.BindDataToTreeView(_operatorCollection[e.Node.Text].RightsCollection);
                    // 清除原有的单元格选择
                    dgvRightsRelationList.ClearSelection();
                    // 同步选中权限关系列表中的所有此操作员单元格
                    foreach (DataGridViewRow dgvRow in dgvRightsRelationList.Rows)
                    {
                        if (dgvRow.Cells["dgvColOperatorName"].Value.ToString() == e.Node.Text)
                            dgvRow.Cells["dgvColOperatorName"].Selected = true;
                    }
                }
                // 在选择操作员所属的权限组名时
                else if (e.Node.Level == 1)
                {
                    // 启用权限视图可勾选状态
                    _rightsViewIsChecked = true;

                    if (!(_rightsGroupCollection[e.Node.Text].GroupRightsCollection is 
                        Dictionary<string, Rights>))
                    {
                        // 为当前选择的权限组创建新的空权限数据
                        _rightsGroupCollection[e.Node.Text].GroupRightsCollection =
                            new Dictionary<string, Rights>();
                        _rightsGroupCollection[e.Node.Text].GroupRightsCollection =
                            _rmdManager.ReadMenuRightsItem(_rightsGroupCollection[e.Node.Text].GroupRightsCollection);
                    }

                    // 选中权限组列表中的对应组名
                    foreach (DataGridViewRow dgvRow in dgvGroupList.Rows)
                    {
                        if (dgvRow.Cells["Name"].Value.ToString() == e.Node.Text)
                            dgvRow.Selected = true;
                    }

                    // 加载权限树形视图数据显示(绑定当前选择的权限组权限数据)
                    _rmuManager.BindDataToTreeView(_rightsGroupCollection[e.Node.Text].GroupRightsCollection);
                    // 清除原有的单元格选择
                    dgvRightsRelationList.ClearSelection();
                    // 同步选中权限关系列表中的所有此操作员的此权限组单元格
                    foreach (DataGridViewRow dgvRow in dgvRightsRelationList.Rows)
                    {
                        if (dgvRow.Cells["dgvColRightsGroupName"].Value.ToString() == e.Node.Text
                            && dgvRow.Cells["dgvColOperatorName"].Value.ToString() == e.Node.Parent.Text)
                            dgvRow.Cells["dgvColRightsGroupName"].Selected = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    string.Format("加载权限视图失败！\r\n\r\n{0}", ex.Message), 
                    "加载失败", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// [权限组视图]单元格验证事件　
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvGroupList_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (dgvGroupList.Columns[e.ColumnIndex].Name == "Name")
            {
                if (e.FormattedValue.ToString().Trim() == String.Empty)
                {
                    dgvGroupList.Rows[e.RowIndex].ErrorText =
                        "权限组名称不允许为空字符(空格)！";
                    MessageBox.Show(
                        dgvGroupList.Rows[e.RowIndex].ErrorText, 
                        "编辑提示", 
                        MessageBoxButtons.OK, 
                        MessageBoxIcon.Error);
                    e.Cancel = true;
                }
            }
        }

        /// <summary>
        /// [权限视图]的节点复选前事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvRightsView_BeforeCheck(object sender, TreeViewCancelEventArgs e)
        {
            // 如果权限视图不可勾选则取消勾选事件
            if (!_rightsViewIsChecked)
                e.Cancel = true;
        }

        /// <summary>
        /// [权限视图]的节点复选事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvRightsView_AfterCheck(object sender, TreeViewEventArgs e)
        {
            // 递归勾选节点及其子节点
            CheckOrUnCheckTreeNode(e.Node);
        }

        /// <summary>
        /// [添加分组]工具栏按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnAddGroup_Click(object sender, EventArgs e)
        {
            try
            {
                BLLFactory.BLLFactory bllFactory = new BLLFactory.BLLFactory();
                IRightsGroupManager rightsGroupManager = bllFactory.BuildRightsGroupManager();
                RightsGroup addRightsGroup = new RightsGroup();
                addRightsGroup.Name = "<- 未命名 ->";
                addRightsGroup.GroupRightsCollection = new Dictionary<string, Rights>();
                addRightsGroup.GroupRightsCollection = _rmdManager.ReadMenuRightsItem(addRightsGroup.GroupRightsCollection);

                if (rightsGroupManager.CheckRightsGroupExist(addRightsGroup.Name))
                {
                    MessageBox.Show(
                        string.Format("默认新权限组名 [{0}] 已被占用，请先删除或重命名该权限组！", addRightsGroup.Name),
                        "添加警告",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                // 执行添加
                if (!(rightsGroupManager.AddRightsGroup(addRightsGroup)))
                {
                    MessageBox.Show(
                        "添加权限组信息失败！",
                        "添加失败",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
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
            finally
            {
                // 刷新数据显示
                RefreshDataDisplay();
                // 选中最末项并使其滚动保持可见
                if (dgvGroupList.Rows.Count > 0)
                {
                    dgvGroupList.Rows[dgvGroupList.Rows.Count - 1].Selected = true;
                    dgvGroupList.FirstDisplayedScrollingRowIndex = dgvGroupList.Rows.Count - 1;
                }
            }
        }

        /// <summary>
        /// [删除分组]工具栏按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnDeleteGroup_Click(object sender, EventArgs e)
        {
            try
            {
                BLLFactory.BLLFactory bllFactory = new BLLFactory.BLLFactory();
                IRightsGroupManager rightsGroupManager = bllFactory.BuildRightsGroupManager();

                // 如果有选中单元格
                if (dgvGroupList.SelectedCells.Count > 0)
                {
                    // 保存权限组 ID
                    int rightsGroupId = Convert.ToInt32(dgvGroupList.SelectedCells[0].OwningRow.Cells["Id"].Value);
                    // 保存权限组 名称
                    string rightsGroupName = Convert.ToString(dgvGroupList.SelectedCells[0].OwningRow.Cells["Name"].Value);

                    DialogResult result;
                    result = MessageBox.Show(
                        string.Format("确实要删除权限组 [{0}] 吗？", rightsGroupName),
                        "删除提示",
                        MessageBoxButtons.OKCancel,
                        MessageBoxIcon.Question,
                        MessageBoxDefaultButton.Button2);

                    if (result == DialogResult.OK)
                    {
                        // 校验此权限组是否包含操作员
                        IRightsRelationManager rightsRelationManager = bllFactory.BuildRightsRelationManager();
                        int relationCount = rightsRelationManager.GetRightsRelationCountByRightsGroupId(rightsGroupId);
                        if (relationCount > 0)
                        {
                            MessageBox.Show(
                                string.Format("权限组 [{0}] 中包含 {1} 个权限关系，不能删除该组！", rightsGroupName, relationCount),
                                "删除警告",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                            return;
                        }

                        // 执行删除
                        if (!(rightsGroupManager.DeleteRightsGroupByID(rightsGroupId)))
                        {
                            MessageBox.Show(
                                "删除权限组信息失败！",
                                "删除失败",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show(
                        "请在 [权限组列表] 中选择要删除的权限组！",
                        "删除提示",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "删除失败",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                // 刷新数据显示
                RefreshDataDisplay();
            }
        }

        /// <summary>
        /// [刷新分组]工具栏按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnRefreshGroup_Click(object sender, EventArgs e)
        {
            // 刷新数据显示
            RefreshDataDisplay();
        }

        /// <summary>
        /// [添加关系]工具栏按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnAddRelation_Click(object sender, EventArgs e)
        {
            // 显示添加权限关系界面
            frmAddRightsRelation frmAddRightsRelation = new frmAddRightsRelation(this);
            frmAddRightsRelation.ShowInTaskbar = false;
            frmAddRightsRelation.ShowDialog();

            // 保存所有更改但不要求显示保存成功提示
            SaveAllChanged(false);

            // 刷新数据显示
            RefreshDataDisplay();
        }

        /// <summary>
        /// [删除关系]工具栏按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnDeleteRelation_Click(object sender, EventArgs e)
        {
            try
            {
                BLLFactory.BLLFactory bllFactory = new BLLFactory.BLLFactory();
                IRightsRelationManager rightsRelationManager = bllFactory.BuildRightsRelationManager();

                // 如果有选中单元格
                if (dgvRightsRelationList.SelectedCells.Count > 0)
                {
                    // 保存权限关系 ID、操作员名称及权限组名称
                    int rightsRelationId = Convert.ToInt32(
                        dgvRightsRelationList.SelectedCells[0].OwningRow.Cells["dgvColId"].Value);
                    string operatorName = Convert.ToString(
                        dgvRightsRelationList.SelectedCells[0].OwningRow.Cells["dgvColOperatorName"].Value);
                    string rightsGroupName = Convert.ToString(
                        dgvRightsRelationList.SelectedCells[0].OwningRow.Cells["dgvColRightsGroupName"].Value);

                    DialogResult result;
                    result = MessageBox.Show(
                        string.Format(
                        "确实要删除权限关系 [{0} - {1} - {2}] 吗？",
                        rightsRelationId,
                        operatorName,
                        rightsGroupName),
                        "删除提示",
                        MessageBoxButtons.OKCancel,
                        MessageBoxIcon.Question,
                        MessageBoxDefaultButton.Button2);

                    if (result == DialogResult.OK)
                    {
                        // 执行删除
                        if (!(rightsRelationManager.DeleteRightsRelationById(rightsRelationId)))
                        {
                            MessageBox.Show(
                                "删除权限组信息失败！",
                                "删除失败",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show(
                        "请在 [权限关系列表] 中选择要删除的权限关系！",
                        "删除提示",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "删除失败",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                // 刷新数据显示
                RefreshDataDisplay();
            }
        }

        /// <summary>
        /// 窗体关闭前且未指定关闭原因事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmRightsGroupManager_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 保存所有更改但不要求显示保存成功提示
            SaveAllChanged(false);
        }

        #endregion
    }
}