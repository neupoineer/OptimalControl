using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using OptimalControl.Forms;

namespace OptimalControl.Common
{
    /// <summary>
    /// 权限菜单界面管理类
    /// </summary>
    internal class RightsMenuUIManager
    {
        #region Private Members
        DataGridView _dgvOperatorList = null;
        DataGridView _dgvRightsList = null;
        TreeView _tvRightsView = null;
        MenuStrip _msMain = null;
        frmRightsManager _rightsManagerUI = null;
        #endregion

        #region Internal Properties
        /// <summary>
        /// 要操作的操作员列表 DataGridView 对象
        /// </summary>
        internal DataGridView DgvOperatorList
        {
            get { return _dgvOperatorList; }
            set { _dgvOperatorList = value; }
        }
        /// <summary>
        /// 要操作的权限列表 DataGridView 对象
        /// </summary>
        internal DataGridView DgvRightsList
        {
            get { return _dgvRightsList; }
            set { _dgvRightsList = value; }
        }
        /// <summary>
        /// 要操作的权限视图 TreeView 对象
        /// </summary>
        internal TreeView TvRightsView
        {
            get { return _tvRightsView; }
            set { _tvRightsView = value; }
        }
        /// <summary>
        /// 被管理的菜单对象
        /// </summary>
        internal MenuStrip MsMain
        {
            get { return _msMain; }
            set { _msMain = value; }
        }
        /// <summary>
        /// 当前被管理的操作界面
        /// </summary>
        internal frmRightsManager RightsManagerUI
        {
            get { return _rightsManagerUI; }
            set { _rightsManagerUI = value; }
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// 加载所有树子节点
        /// </summary>
        /// <param name="currentChildTreeNode">当前子节点</param>
        /// <param name="rightsCollection">所有权限集合</param>
        /// <returns>加载了所有次级子节点的当前子节点</returns>
        private TreeNode LoadAllChildTreeNode(TreeNode currentChildTreeNode, Dictionary<string, Model.Rights> rightsCollection)
        {
            // 如果是菜单分隔则设置突出显示
            if (currentChildTreeNode.Text == "━━━━")
            {
                currentChildTreeNode.ForeColor = Color.Red;
                currentChildTreeNode.ToolTipText = "<-- 菜单分隔 -->";
            }
            // 遍历同父权限项集合
            foreach (Model.Rights tmpRights in rightsCollection.Values)
            {
                // 如果当前父级权限项的权限名称与当前节点相同
                if (tmpRights.ParentLevelRightsName == currentChildTreeNode.Tag.ToString())
                {
                    // 为当前节点创建新的子节点
                    TreeNode newChildTreeNode = new TreeNode(tmpRights.RightsCaption);
                    newChildTreeNode.Tag = tmpRights.ModelName;
                    newChildTreeNode.Checked = tmpRights.RightsState;
                    // 创建同父权限项集合
                    List<Model.Rights> sameNessParentRightsList = new List<Model.Rights>();
                    // 获取所有与当前权限项具有相同父权限项的权限项
                    foreach (Model.Rights sameNessParentRights in rightsCollection.Values)
                    {
                        if (sameNessParentRights.ParentLevelRightsName == tmpRights.ParentLevelRightsName)
                            sameNessParentRightsList.Add(sameNessParentRights);
                    }
                    // 递归添加到当前节点及其所有子节点
                    currentChildTreeNode.Nodes.Add(LoadAllChildTreeNode(newChildTreeNode, rightsCollection));
                }
            }

            // 返回当前处理的节点
            return currentChildTreeNode;
        } 
        #endregion

        #region Internal Methods
        /// <summary>
        /// 将操作员集合数据绑定显示到数据视图
        /// </summary>
        /// <param name="operatorCollection">操作员集合</param>
        internal void BindOperatorInfoToDataGridView(Dictionary<string, Model.Operator> operatorCollection)
        {
            try
            {
                // 如果包含操作员信息
                if (operatorCollection.Count > 0)
                {
                    // 将权限集合绑定显示在数据视图中
                    BindingSource source = new BindingSource();
                    source.DataSource = operatorCollection.Values;
                    this._dgvOperatorList.DataSource = source;
                    // 设置中文列名及可写状态
                    this._dgvOperatorList.Columns["Id"].HeaderText = "编号";
                    this._dgvOperatorList.Columns["Id"].ToolTipText = "[只读列]";
                    this._dgvOperatorList.Columns["Id"].DisplayIndex = 0;
                    this._dgvOperatorList.Columns["Id"].ReadOnly = true;
                    this._dgvOperatorList.Columns["ModelName"].HeaderText = "操作员名称";
                    this._dgvOperatorList.Columns["ModelName"].ToolTipText = "[只读列]";
                    this._dgvOperatorList.Columns["ModelName"].DisplayIndex = 1;
                    this._dgvOperatorList.Columns["ModelName"].ReadOnly = true;
                    this._dgvOperatorList.Columns["Password"].HeaderText = "密码";
                    this._dgvOperatorList.Columns["Password"].DisplayIndex = 2;
                    this._dgvOperatorList.Columns["State"].HeaderText = "状态";
                    this._dgvOperatorList.Columns["State"].DisplayIndex = 3;
                    this._dgvOperatorList.Columns["RightsCollection"].HeaderText = "权限列表";
                    this._dgvOperatorList.Columns["RightsCollection"].DisplayIndex = 4;
                    this._dgvOperatorList.Columns["RightsCollection"].ReadOnly = true;
                    this._dgvOperatorList.Columns["RightsCollection"].Visible = false;

                    // 设置菜单分隔项的权限标题列为只读
                    foreach (DataGridViewRow dgvRow in this._dgvOperatorList.Rows)
                    {
                        // 设置单元格工具栏提示
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
        /// 将权限集合数据绑定显示到数据视图
        /// </summary>
        /// <param name="rightsCollection">权限集合</param>
        internal void BindDataToDataGridView(Dictionary<string, Model.Rights> rightsCollection)
        {
            try
            {
                // 保存所有选择单元格
                List<string> selectedCellValueList = new List<string>();
                foreach (DataGridViewCell dgvCell in _dgvRightsList.SelectedCells)
                {
                    selectedCellValueList.Add(dgvCell.Value.ToString().Trim());
                }

                // 将权限集合绑定显示在数据视图中
                BindingSource source = new BindingSource();
                source.DataSource = rightsCollection.Values;
                this._dgvRightsList.DataSource = source;
                // 设置中文列名及可写状态
                this._dgvRightsList.Columns["Id"].HeaderText = "编号";
                this._dgvRightsList.Columns["Id"].ToolTipText = "[只读列]";
                this._dgvRightsList.Columns["Id"].ReadOnly = true;
                this._dgvRightsList.Columns["Id"].DisplayIndex = 0;
                this._dgvRightsList.Columns["RightsCaption"].HeaderText = "权限标题";
                this._dgvRightsList.Columns["RightsCaption"].DisplayIndex = 1;
                this._dgvRightsList.Columns["ModelName"].HeaderText = "内部名称";
                this._dgvRightsList.Columns["ModelName"].ToolTipText = "[只读列]";
                this._dgvRightsList.Columns["ModelName"].ReadOnly = true;
                this._dgvRightsList.Columns["ModelName"].Visible = false;
                this._dgvRightsList.Columns["ModelName"].DisplayIndex = 2;
                this._dgvRightsList.Columns["RightsState"].HeaderText = "权限状态";
                this._dgvRightsList.Columns["RightsState"].DisplayIndex = 3;
                this._dgvRightsList.Columns["ParentLevelRightsName"].HeaderText = "父级权限";
                this._dgvRightsList.Columns["ParentLevelRightsName"].ToolTipText = "[只读列]";
                this._dgvRightsList.Columns["ParentLevelRightsName"].ReadOnly = true;
                this._dgvRightsList.Columns["ParentLevelRightsName"].DisplayIndex = 4;
                this._dgvRightsList.Columns["ParentLevelRightsName"].Visible = false;

                // 设置菜单分隔项的权限标题列为只读
                foreach (DataGridViewRow dgvRow in this._dgvRightsList.Rows)
                {
                    // 如果是菜单分隔项则设置为只读
                    if (dgvRow.Cells["RightsCaption"].Value.ToString().Trim() == "━━━━")
                        dgvRow.Cells["RightsCaption"].ReadOnly = true;
                    // 设置单元格工具栏提示
                    foreach (DataGridViewCell dgvCell in dgvRow.Cells)
                    {
                        if (dgvCell.ReadOnly)
                        {
                            dgvCell.ToolTipText = "[只读格]";
                            if (dgvCell.Value.ToString().Trim() == "━━━━")
                                dgvCell.ToolTipText += " | <-- 菜单分隔 -->";
                        }
                        else
                            dgvCell.ToolTipText = "[可写格]";
                    }
                }

                // 禁用关联选择
                RightsManagerUI.IsRelatingChooseCells = false;
                // 清除所有单元格选择
                _dgvRightsList.ClearSelection();
                // 还原原有选择
                foreach (string dgvSelectedCellValue in selectedCellValueList)
                {
                    foreach (DataGridViewRow dgvRow in _dgvRightsList.Rows)
                    {
                        foreach (DataGridViewCell dgvCell in dgvRow.Cells)
                        {
                            if (dgvCell.Value.ToString().Trim() == dgvSelectedCellValue)
                                dgvCell.Selected = true;
                        }
                    }
                }
                // 启用关联选择
                RightsManagerUI.IsRelatingChooseCells = true;
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
        /// 将数据绑定加载到树形视图
        /// </summary>
        /// <param name="rightsCollection">权限集合</param>
        internal void BindDataToTreeView(Dictionary<string, Model.Rights> rightsCollection)
        {
            // 禁用树视图的重绘
            _tvRightsView.BeginUpdate();
            // 清除原有节点
            _tvRightsView.Nodes.Clear();
            // 遍历权限集合以加载数据
            foreach (Model.Rights tmpRights in rightsCollection.Values)
            {
                // 定义权限根项
                TreeNode rootTreeNode = null;
                // 如果是权限根项
                if (tmpRights.ParentLevelRightsName == _msMain.Name)
                {
                    rootTreeNode = new TreeNode(tmpRights.RightsCaption);
                    rootTreeNode.Tag = tmpRights.ModelName;
                    rootTreeNode.Checked = tmpRights.RightsState;
                    _tvRightsView.Nodes.Add(rootTreeNode);
                }
                // 如果是权限子项
                else
                {
                    // 创建权限子项
                    TreeNode childTreeNode = new TreeNode(tmpRights.RightsCaption);
                    childTreeNode.Tag = tmpRights.ModelName;
                    childTreeNode.Checked = tmpRights.RightsState;
                    // 将子项添加到对应的父项中
                    foreach (TreeNode tmpTreeNode in _tvRightsView.Nodes)
                    {
                        // 如果与现存的节点父级权限相同
                        if (tmpTreeNode.Tag.ToString() == tmpRights.ParentLevelRightsName)
                        {
                            // 递归添加所有层级子节点
                            tmpTreeNode.Nodes.Add(LoadAllChildTreeNode(childTreeNode, rightsCollection));
                        }
                    }
                }
            }

            // 展开所有树节点
            _tvRightsView.ExpandAll();

            // 启用树视图的重绘
            _tvRightsView.EndUpdate();
        }

        /// <summary>
        /// 选中/取消树节点及其子节点勾选状态
        /// </summary>
        /// <param name="currentTreeNode">当前操作的节点</param>
        internal void CheckOrUnCheckTreeNode(TreeNode currentTreeNode)
        {
            // 如果有选中单元格
            if (_dgvOperatorList.SelectedCells.Count > 0)
            {
                // 保存当前选中行的操作员名称
                string operatorName = _dgvOperatorList.Rows[_dgvOperatorList.SelectedCells[0].RowIndex].Cells["ModelName"].Value.ToString().Trim();
                // 同步权限状态
                RightsManagerUI.OperatorCollection[operatorName].RightsCollection[currentTreeNode.Tag.ToString()].RightsState = currentTreeNode.Checked;

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
        /// 加载操作员列表
        /// </summary>
        internal void LoadOperatorList()
        {
            BLLFactory.BLLFactory bllFactory = new BLLFactory.BLLFactory();
            IBLL.IOperatorManager operatorManager = bllFactory.BuildOperatorManager();

            // 加载操作员列表
            try
            {
                RightsManagerUI.OperatorCollection = operatorManager.GetAllOperatorInfo();
                // 检查所有操作员的权限列表
                foreach (Model.Operator tmpOperator in RightsManagerUI.OperatorCollection.Values)
                {
                    // 如果权限为空就创建一个新的空权限集合
                    if (!(tmpOperator.RightsCollection is Dictionary<string, Model.Rights>))
                    {
                        tmpOperator.RightsCollection = new Dictionary<string, Model.Rights>();
                        // 创建权限管理类实例
                        RightsMenuDataManager rmdManager = new RightsMenuDataManager();
                        // 创建权限集合空结构
                        tmpOperator.RightsCollection = rmdManager.ReadMenuRightsItem(_msMain, tmpOperator.RightsCollection);
                    }
                }

                // 如果包含操作员信息
                if (RightsManagerUI.OperatorCollection.Count > 0)
                {
                    // 将操作员集合数据绑定显示到数据视图
                    BindOperatorInfoToDataGridView(RightsManagerUI.OperatorCollection);
                    // 重新指定当前登录操作员对象
                    foreach (Model.Operator tmpOperator in RightsManagerUI.OperatorCollection.Values)
                    {
                        if (tmpOperator.ModelName == RightsManagerUI.CurrentOperator.ModelName)
                        {
                            RightsManagerUI.CurrentOperator = RightsManagerUI.OperatorCollection[RightsManagerUI.CurrentOperator.ModelName];
                            // 将数据绑定显示到数据视图
                            BindDataToDataGridView(RightsManagerUI.CurrentOperator.RightsCollection);
                            // 将数据绑定加载到树形视图
                            BindDataToTreeView(RightsManagerUI.CurrentOperator.RightsCollection);

                            // 在操作员列表中选中当前操作员
                            foreach (DataGridViewRow dgvRow in _dgvOperatorList.Rows)
                            {
                                if (dgvRow.Cells["ModelName"].Value.ToString().Trim() == RightsManagerUI.CurrentOperator.ModelName)
                                {
                                    dgvRow.Selected = true;
                                    break;
                                }
                            }

                            break;
                        }
                    }
                }
                else
                {
                    // 清空操作员列表的数据行
                    DgvOperatorList.Rows.Clear();
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
        #endregion
    }
}
