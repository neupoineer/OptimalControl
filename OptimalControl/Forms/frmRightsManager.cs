using System;
using System.Collections.Generic;
using System.Windows.Forms;
using IBLL;
using Model;
using OptimalControl.Common;

namespace OptimalControl.Forms
{
    /// <summary>
    /// 权限管理界面
    /// </summary>
    public partial class frmRightsManager : Form
    {
        #region Private Members
        MenuStrip _msMain = null;
        /// <summary>
        /// 主界面被管理的菜单对象
        /// </summary>
        internal MenuStrip MsMain
        {
            get { return _msMain; }
            set { _msMain = value; }
        }
        frmMain _frmMain = null;
        /// <summary>
        /// 主界面对象
        /// </summary>
        public frmMain FrmMain
        {
            get { return _frmMain; }
            set { _frmMain = value; }
        }
        /// <summary>
        /// 权限菜单界面管理类对象
        /// </summary>
        RightsMenuUIManager _rmuManager = null;
        /// <summary>
        /// 权限菜单数据管理类对象
        /// </summary>
        RightsMenuDataManager _rmdManager = null;
        Operator _currentOperator = null;
        /// <summary>
        /// 保存当前登录的操作员对象
        /// </summary>
        internal Operator CurrentOperator
        {
            get { return _currentOperator; }
            set { _currentOperator = value; }
        }
        Dictionary<string, Operator> _operatorCollection = null;
        /// <summary>
        /// 当前加载的所有操作员实体集合
        /// </summary>
        internal Dictionary<string, Operator> OperatorCollection
        {
            get { return _operatorCollection; }
            set { _operatorCollection = value; }
        }
        bool _isRelatingChooseCells = true;
        /// <summary>
        /// 是否启用数据视图关联选择
        /// </summary>
        internal bool IsRelatingChooseCells
        {
            get { return _isRelatingChooseCells; }
            set { _isRelatingChooseCells = value; }
        }
        /// <summary>
        /// 是否启用树视图子节点关联选择
        /// </summary>
        bool _isCheckedChildTreeNode = true;
        /// <summary>
        /// 保存上一次被选中的操作员列表行
        /// </summary>
        DataGridViewRow _dgvOldSelectedRow = null;
        /// <summary>
        /// 权限视图可勾选状态
        /// </summary>
        bool _rightsViewIsChecked = false;
        #endregion

        #region Public Methods
        /// <summary>
        /// 带参构造
        /// </summary>
        /// <param name="msMain">主界面待管理的菜单对象</param>
        /// <param name="frmMain">主界面对象</param>
        /// <param name="currentOperator">当前登录操作员对象</param>
        public frmRightsManager(MenuStrip msMain, frmMain frmMain, Operator currentOperator)
        {
            // 构建设计器控件
            InitializeComponent();
            // 保存菜单对象
            MsMain = msMain;
            // 保存主界面对象
            FrmMain = frmMain;
            // 保存当前登录操作员对象
            CurrentOperator = currentOperator;

            // 创建权限菜单界面管理类实例
            _rmuManager = new RightsMenuUIManager();
            // 赋予要操作的对象
            _rmuManager.DgvOperatorList = this.dgvOperatorList;
            _rmuManager.DgvRightsList = this.dgvRightsList;
            _rmuManager.TvRightsView = this.tvRightsView;
            _rmuManager.MsMain = _msMain;
            _rmuManager.RightsManagerUI = this;

            // 创建权限菜单数据管理类实例
            _rmdManager = new RightsMenuDataManager();
        } 
        #endregion

        #region Event Handlers
        /// <summary>
        /// 窗体初始化事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmRightsManager_Load(object sender, EventArgs e)
        {
            // 加载操作员列表
            _rmuManager.LoadOperatorList();

            // 默认为分组模式
            tsbtnRightsMode.Text = tsmiGroupMode.Text;
        }

        /// <summary>
        /// 树形视图右键菜单公共点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiTreeViewContentMenuItem_Click(object sender, EventArgs e)
        {
            // 保存当前菜单项激发者
            ToolStripMenuItem menuItem = sender as ToolStripMenuItem;
            // 勾选所有结点上的复选框
            if (menuItem == tsmiSelectAll)
            {
                foreach (TreeNode treeNode in this.tvRightsView.Nodes)
                {
                    treeNode.Checked = true;
                    // 禁用树视图重绘
                    tvRightsView.BeginUpdate();
                    // 选中/取消树节点及其子节点勾选状态
                    _rmuManager.CheckOrUnCheckTreeNode(treeNode);
                    // 启用树视图重绘
                    tvRightsView.EndUpdate();
                }
            }
            // 取消所有结点上的复选
            if (menuItem == tsmiCancelAll)
            {
                foreach (TreeNode treeNode in this.tvRightsView.Nodes)
                {
                    treeNode.Checked = false;
                    // 禁用树视图重绘
                    tvRightsView.BeginUpdate();
                    // 选中/取消树节点及其子节点勾选状态
                    _rmuManager.CheckOrUnCheckTreeNode(treeNode);
                    // 启用树视图重绘
                    tvRightsView.EndUpdate();
                }
            }
            // 重新加载账户权限结构树
            if (menuItem == tsmiReload)
            {
                // 如果有选中行
                if (dgvOperatorList.SelectedCells.Count > 0)
                {
                    // 保存当前选中行的操作员名称
                    string operatorName = _dgvOldSelectedRow.Cells["Name"].Value.ToString().Trim();
                    // 指定当前编辑的操作员对象
                    Operator editOperator = _operatorCollection[operatorName];

                    // 将数据绑定加载到树形视图
                    _rmuManager.BindDataToTreeView(editOperator.RightsCollection);
                }
            }
            // 将 TreeView 树结点全部展开
            if (menuItem == tsmiExpandAll)
            {
                this.tvRightsView.ExpandAll();
            }
            // 将 TreeView 树结点全部折叠
            if (menuItem == tsmiCollapseAll)
            {
                this.tvRightsView.CollapseAll();
            }
        }

        /// <summary>
        /// [保存更改]工具栏按钮点击事件 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnSaveAllChanged_Click(object sender, EventArgs e)
        {
            // 提交并结束数据视图的单元格编辑
            dgvRightsList.EndEdit();
            dgvOperatorList.EndEdit();
            
            // 如果有选中单元格
            if (dgvOperatorList.SelectedCells.Count > 0)
            {
                try
                {
                    // 创建业务逻辑层工厂类实例
                    BLLFactory.BLLFactory bllFactory = new BLLFactory.BLLFactory();
                    // 调用工厂类实例方法创建操作员管理类实例
                    IOperatorManager operatorManager = bllFactory.BuildOperatorManager();

                    // 保存所有操作员权限信息
                    foreach (Operator tmpOperator in _operatorCollection.Values)
                    {
                        // 保存当前操作员所做的修改
                        operatorManager.ModifyOperator(tmpOperator);
                    }
                    
                    // 给出用户操作提示
                    MessageBox.Show(
                        "权限设置保存成功！",
                        "保存提示",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        ex.Message,
                        "保存失败",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// [刷新权限]工具栏按钮点击事件 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnRefreshRightsList_Click(object sender, EventArgs e)
        {
            // 如果是非超级管理员
            if (_currentOperator.Id != 0)
            {
                // 将数据绑定显示到数据视图
                _rmuManager.BindDataToDataGridView(_currentOperator.RightsCollection);
                // 将数据绑定加载到树形视图
                _rmuManager.BindDataToTreeView(_currentOperator.RightsCollection);
            }
        }

        /// <summary>
        /// [刷新用户]工具栏按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnRefreshOperator_Click(object sender, EventArgs e)
        {
            // 将数据绑定显示到数据视图
            _rmuManager.BindOperatorInfoToDataGridView(_operatorCollection);
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
        /// 权限列表数据视图单元格启动编辑模式事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvRightsList_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            // 如果是权限状态列且节点不可选则取消勾选事件
            if (dgvRightsList.Columns[e.ColumnIndex].Name == "RightsState" && !_rightsViewIsChecked)
                e.Cancel = true;
        }

        /// <summary>
        /// 权限列表数据视图单元格停止编辑模式事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvRightsList_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            // 如果包含操作员信息
            if (dgvOperatorList.SelectedCells.Count > 0)
            {
                // 保存当前选中行的操作员名称
                string operatorName = dgvOperatorList.Rows[dgvOperatorList.SelectedCells[0].RowIndex].Cells["Name"].Value.ToString().Trim();
                // 指定当前编辑的操作员对象
                Operator editOperator = _operatorCollection[operatorName];

                // 找到当前编辑的权限项
                Rights currentEditRights = editOperator.RightsCollection[dgvRightsList.Rows[e.RowIndex].Cells["Name"].Value.ToString().Trim()];
                // 保存当前修改的权限标题和权限状态到权限集合
                currentEditRights.RightsCaption = Convert.ToString(dgvRightsList.Rows[e.RowIndex].Cells["RightsCaption"].Value);
                currentEditRights.RightsState = Convert.ToBoolean(dgvRightsList.Rows[e.RowIndex].Cells["RightsState"].Value);

                // 将数据绑定加载到树形视图
                _rmuManager.BindDataToTreeView(editOperator.RightsCollection);
            }
        }

        /// <summary>
        /// 操作员列表数据视图单元格停止编辑模式事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvOperatorList_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            // 如果包含操作员信息
            if (dgvOperatorList.SelectedCells.Count > 0)
            {
                // 找到当前编辑的操作员项
                Operator currentEidtOperator = _operatorCollection[dgvOperatorList.Rows[e.RowIndex].Cells["Name"].Value.ToString().Trim()];
                // 保存当前修改的操作员密码和状态到操作员集合
                currentEidtOperator.Password = Convert.ToString(dgvOperatorList.Rows[e.RowIndex].Cells["Password"].Value);
                currentEidtOperator.State = Convert.ToBoolean(dgvOperatorList.Rows[e.RowIndex].Cells["State"].Value);
            }
        }

        /// <summary>
        /// 权限列表数据视图所选内容更改事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvRightsList_SelectionChanged(object sender, EventArgs e)
        {
            // 仅在有选中单元格且启用关联选择时才执行操作
            if (dgvRightsList.SelectedCells.Count > 0 && IsRelatingChooseCells)
            {
                // 遍历所有选中单元格选中所有同级权限项及父项
                for (int i = 0; i < dgvRightsList.SelectedCells.Count; i++)
                {
                    // 且仅在选中[内部名称]和[父级权限]单元格时才执行操作
                    if (dgvRightsList.SelectedCells[i].OwningColumn.Name == "Name"
                        || dgvRightsList.SelectedCells[i].OwningColumn.Name == "ParentLevelRightsName")
                    {
                        // 同时选中与选中单元格内容相同的所有[内部名称]和[父级权限]单元格
                        foreach (DataGridViewRow dgvRow in this.dgvRightsList.Rows)
                        {
                            if (dgvRow.Cells["Name"].Value == dgvRightsList.SelectedCells[i].Value)
                                dgvRow.Cells["Name"].Selected = true;
                            else if (dgvRow.Cells["ParentLevelRightsName"].Value == dgvRightsList.SelectedCells[i].Value)
                                dgvRow.Cells["ParentLevelRightsName"].Selected = true;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 操作员列表数据视图所选内容更改事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvOperatorList_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvOperatorList.SelectedCells.Count > 0
                && dgvOperatorList.Rows[dgvOperatorList.SelectedCells[0].RowIndex] != _dgvOldSelectedRow)
            {
                // 保存当前选中的行
                _dgvOldSelectedRow = dgvOperatorList.Rows[dgvOperatorList.SelectedCells[0].RowIndex];
                // 保存当前选中行的操作员名称
                string operatorName = _dgvOldSelectedRow.Cells["Name"].Value.ToString().Trim();
                // 指定当前编辑的操作员对象
                Operator editOperator = _operatorCollection[operatorName];

                // 将数据绑定显示到数据视图
                _rmuManager.BindDataToDataGridView(editOperator.RightsCollection);
                // 将数据绑定加载到树形视图
                _rmuManager.BindDataToTreeView(editOperator.RightsCollection);
            }
        }

        /// <summary>
        /// [权限视图]节点复选前事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvRightsView_BeforeCheck(object sender, TreeViewCancelEventArgs e)
        {
            // 如果节点不可选则取消勾选事件
            if (!_rightsViewIsChecked)
                e.Cancel = true;
        }

        /// <summary>
        /// 权限树形视图复选框状态更改事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvRightsView_AfterCheck(object sender, TreeViewEventArgs e)
        {
            // 如果已经启用子节点关联选择
            if (_isCheckedChildTreeNode)
            {
                // 禁用树视图重绘
                tvRightsView.BeginUpdate();
                // 禁用子节点关联选择
                _isCheckedChildTreeNode = false;
                // 选中/取消树节点及其子节点勾选状态
                _rmuManager.CheckOrUnCheckTreeNode(e.Node);
                // 启用子节点关联选择
                _isCheckedChildTreeNode = true;
                // 启用树视图重绘
                tvRightsView.EndUpdate();

                if (dgvOperatorList.SelectedCells.Count > 0)
                {
                    // 保存当前选中行的操作员名称
                    string operatorName = dgvOperatorList.Rows[dgvOperatorList.SelectedCells[0].RowIndex].Cells["Name"].Value.ToString().Trim();
                    // 将数据绑定显示到数据视图
                    _rmuManager.BindDataToDataGridView(_operatorCollection[operatorName].RightsCollection);
                }
            }
        }

        /// <summary>
        /// 权限树形视图选定内容更改事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvRightsView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            // 禁用关联选择
            _isRelatingChooseCells = false;
            // 清除所有单元格选择
            dgvRightsList.ClearSelection();
            // 启用关联选择
            _isRelatingChooseCells = true;

            // 同步数据视图对应项选择
            foreach (DataGridViewRow dgvRow in this.dgvRightsList.Rows)
            {
                if (dgvRow.Cells["Name"].Value.ToString().Trim() == e.Node.Tag.ToString().Trim())
                    dgvRow.Cells["Name"].Selected = true;
            }
        }

        /// <summary>
        /// 用户编辑权限树形视图节点文本后事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvRightsView_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            // 保存当前选中行的操作员名称
            string operatorName = dgvOperatorList.Rows[dgvOperatorList.SelectedCells[0].RowIndex].Cells["Name"].Value.ToString().Trim();
            // 同步修改权限集合
            _operatorCollection[operatorName].RightsCollection[e.Node.Tag.ToString()].RightsCaption = e.Label;
            // 将数据绑定显示到数据视图
            _rmuManager.BindDataToDataGridView(_operatorCollection[operatorName].RightsCollection);
        }

        /// <summary>
        /// 窗体指定关闭原因后且已关闭事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmRightsManager_FormClosed(object sender, FormClosedEventArgs e)
        {
            // 重新加载权限菜单
            _rmdManager.LoadMenuRightsItem(_msMain, _currentOperator.RightsCollection);
        }

        /// <summary>
        /// [添加用户]工具栏按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnAddOperator_Click(object sender, EventArgs e)
        {
            frmOperatorManager frmAddOperator = new frmOperatorManager(_currentOperator, _msMain, false);
            frmAddOperator.ShowInTaskbar = false;
            frmAddOperator.ShowDialog();

            // 重新加载操作员列表
            _rmuManager.LoadOperatorList();

            // 选中最末项并使其滚动保持可见
            if (dgvOperatorList.Rows.Count > 0)
            {
                dgvOperatorList.Rows[dgvOperatorList.Rows.Count - 1].Selected = true;
                dgvOperatorList.FirstDisplayedScrollingRowIndex = dgvOperatorList.Rows.Count - 1;
            }
        }

        /// <summary>
        /// [删除用户]工具栏按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnDeleteOperator_Click(object sender, EventArgs e)
        {
            // 如果有选中单元格
            if (dgvOperatorList.SelectedCells.Count > 0)
            {
                try
                {
                    // 获取当前要删除的操作员 ID
                    int operatorId = Convert.ToInt32(dgvOperatorList.SelectedCells[0].OwningRow.Cells["Id"].Value);
                    // 获取当前要删除的操作员名称
                    string operatorName = Convert.ToString(dgvOperatorList.SelectedCells[0].OwningRow.Cells["Name"].Value);

                    DialogResult result;
                    result = MessageBox.Show(
                        string.Format("确实要删除操作员 [{0}] 的所有(包括权限关系)信息吗？", operatorName),
                        "删除提示",
                        MessageBoxButtons.OKCancel,
                        MessageBoxIcon.Question,
                        MessageBoxDefaultButton.Button2);

                    if (result == DialogResult.Cancel)
                        return;

                    // 创建工厂类
                    BLLFactory.BLLFactory bllFactory = new BLLFactory.BLLFactory();

                    // 删除权限关系信息
                    IRightsRelationManager rightsRelationManager = bllFactory.BuildRightsRelationManager();
                    if (rightsRelationManager.GetRightsRelationByOperatorId(operatorId).Count > 0)
                    {
                        if (!rightsRelationManager.DeleteRightsRelationByOperatorId(operatorId))
                        {
                            MessageBox.Show(
                            string.Format("未能删除操作员 [{0}] 的权限关系信息！", operatorName),
                            "删除失败",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                            return;
                        }
                    }

                    // 删除操作员信息
                    IOperatorManager operatorManager = bllFactory.BuildOperatorManager();
                    if (!operatorManager.DeleteOperatorByID(operatorId))
                    {
                        MessageBox.Show(
                        string.Format("未能删除操作员 [{0}] 的基本信息！", operatorName),
                        "删除失败",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                        return;
                    }

                    // 重新加载操作员列表
                    _rmuManager.LoadOperatorList();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        ex.Message,
                        "删除失败",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }

            // 如果操作员信息数量为零
            if (_operatorCollection.Count == 0)
            {
                // 清空权限列表、操作员列表和权限视图
                dgvOperatorList.Rows.Clear();
                dgvRightsList.Rows.Clear();
                tvRightsView.Nodes.Clear();
            }
        }

        /// <summary>
        /// [分组管理]工具栏按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnRightsGroupManager_Click(object sender, EventArgs e)
        {
            frmRightsGroupManager frmRightsGroupManager = new frmRightsGroupManager(this, this.FrmMain);
            frmRightsGroupManager.ShowInTaskbar = false;
            frmRightsGroupManager.ShowDialog();

            // 加载操作员列表
            _rmuManager.LoadOperatorList();
            // 刷新数据显示到数据视图
            _rmuManager.BindOperatorInfoToDataGridView(_operatorCollection);
        }

        /// <summary>
        /// [权限列表]单元格验证事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvRightsList_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (dgvRightsList.Columns[e.ColumnIndex].Name == "RightsCaption")
            {
                if (e.FormattedValue.ToString().Trim() == String.Empty)
                {
                    dgvRightsList.Rows[e.RowIndex].ErrorText =
                        "权限标题不允许为空字符(空格)！";
                    MessageBox.Show(
                        dgvRightsList.Rows[e.RowIndex].ErrorText,
                        "编辑提示",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    e.Cancel = true;
                }
            }
        }

        /// <summary>
        /// [操作员列表]单元格验证事件　
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvOperatorList_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (dgvOperatorList.Columns[e.ColumnIndex].Name == "Password")
            {
                if (e.FormattedValue.ToString().Trim().Length < 6)
                {
                    dgvOperatorList.Rows[e.RowIndex].ErrorText =
                        "用户密码长度不能小于六位！";
                    MessageBox.Show(
                        dgvOperatorList.Rows[e.RowIndex].ErrorText,
                        "编辑提示",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    e.Cancel = true;
                }
            }
        }

        /// <summary>
        /// [分组模式]/[用户模式]工具栏下拉菜单项点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiRightsMode_Click(object sender, EventArgs e)
        {
            // 勾选点击菜单项
            (sender as ToolStripMenuItem).Checked = true;
            // 提交并结束对当前单元格的编辑
            dgvRightsList.EndEdit();

            if ((sender as ToolStripMenuItem).Name == tsmiGroupMode.Name)
            {
                tsmiOperatorMode.Checked = false;
                _rightsViewIsChecked = false;
                tsbtnRightsGroupManager.Visible = true;
                tsbtnRightsMode.Text = tsmiGroupMode.Text;
                tvRightsView.ContextMenuStrip = null;
            }
            if ((sender as ToolStripMenuItem).Name == tsmiOperatorMode.Name)
            {
                tsmiGroupMode.Checked = false;
                _rightsViewIsChecked = true;
                tsbtnRightsGroupManager.Visible = false;
                tsbtnRightsMode.Text = tsmiOperatorMode.Text;
                tvRightsView.ContextMenuStrip = this.cmsRightsTreeView;
            }
        }

        /// <summary>
        /// [权限模式]工具栏按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnRightsMode_ButtonClick(object sender, EventArgs e)
        {
            // 下拉弹出菜单选项
            tsbtnRightsMode.ShowDropDown();
        }

        #endregion
    }
}