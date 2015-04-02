using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using OptimalControl.Forms;

namespace OptimalControl.Common
{
    /// <summary>
    /// Ȩ�޲˵����������
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
        /// Ҫ�����Ĳ���Ա�б� DataGridView ����
        /// </summary>
        internal DataGridView DgvOperatorList
        {
            get { return _dgvOperatorList; }
            set { _dgvOperatorList = value; }
        }
        /// <summary>
        /// Ҫ������Ȩ���б� DataGridView ����
        /// </summary>
        internal DataGridView DgvRightsList
        {
            get { return _dgvRightsList; }
            set { _dgvRightsList = value; }
        }
        /// <summary>
        /// Ҫ������Ȩ����ͼ TreeView ����
        /// </summary>
        internal TreeView TvRightsView
        {
            get { return _tvRightsView; }
            set { _tvRightsView = value; }
        }
        /// <summary>
        /// ������Ĳ˵�����
        /// </summary>
        internal MenuStrip MsMain
        {
            get { return _msMain; }
            set { _msMain = value; }
        }
        /// <summary>
        /// ��ǰ������Ĳ�������
        /// </summary>
        internal frmRightsManager RightsManagerUI
        {
            get { return _rightsManagerUI; }
            set { _rightsManagerUI = value; }
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// �����������ӽڵ�
        /// </summary>
        /// <param name="currentChildTreeNode">��ǰ�ӽڵ�</param>
        /// <param name="rightsCollection">����Ȩ�޼���</param>
        /// <returns>���������дμ��ӽڵ�ĵ�ǰ�ӽڵ�</returns>
        private TreeNode LoadAllChildTreeNode(TreeNode currentChildTreeNode, Dictionary<string, Model.Rights> rightsCollection)
        {
            // ����ǲ˵��ָ�������ͻ����ʾ
            if (currentChildTreeNode.Text == "��������")
            {
                currentChildTreeNode.ForeColor = Color.Red;
                currentChildTreeNode.ToolTipText = "<-- �˵��ָ� -->";
            }
            // ����ͬ��Ȩ�����
            foreach (Model.Rights tmpRights in rightsCollection.Values)
            {
                // �����ǰ����Ȩ�����Ȩ�������뵱ǰ�ڵ���ͬ
                if (tmpRights.ParentLevelRightsName == currentChildTreeNode.Tag.ToString())
                {
                    // Ϊ��ǰ�ڵ㴴���µ��ӽڵ�
                    TreeNode newChildTreeNode = new TreeNode(tmpRights.RightsCaption);
                    newChildTreeNode.Tag = tmpRights.ModelName;
                    newChildTreeNode.Checked = tmpRights.RightsState;
                    // ����ͬ��Ȩ�����
                    List<Model.Rights> sameNessParentRightsList = new List<Model.Rights>();
                    // ��ȡ�����뵱ǰȨ���������ͬ��Ȩ�����Ȩ����
                    foreach (Model.Rights sameNessParentRights in rightsCollection.Values)
                    {
                        if (sameNessParentRights.ParentLevelRightsName == tmpRights.ParentLevelRightsName)
                            sameNessParentRightsList.Add(sameNessParentRights);
                    }
                    // �ݹ���ӵ���ǰ�ڵ㼰�������ӽڵ�
                    currentChildTreeNode.Nodes.Add(LoadAllChildTreeNode(newChildTreeNode, rightsCollection));
                }
            }

            // ���ص�ǰ����Ľڵ�
            return currentChildTreeNode;
        } 
        #endregion

        #region Internal Methods
        /// <summary>
        /// ������Ա�������ݰ���ʾ��������ͼ
        /// </summary>
        /// <param name="operatorCollection">����Ա����</param>
        internal void BindOperatorInfoToDataGridView(Dictionary<string, Model.Operator> operatorCollection)
        {
            try
            {
                // �����������Ա��Ϣ
                if (operatorCollection.Count > 0)
                {
                    // ��Ȩ�޼��ϰ���ʾ��������ͼ��
                    BindingSource source = new BindingSource();
                    source.DataSource = operatorCollection.Values;
                    this._dgvOperatorList.DataSource = source;
                    // ����������������д״̬
                    this._dgvOperatorList.Columns["Id"].HeaderText = "���";
                    this._dgvOperatorList.Columns["Id"].ToolTipText = "[ֻ����]";
                    this._dgvOperatorList.Columns["Id"].DisplayIndex = 0;
                    this._dgvOperatorList.Columns["Id"].ReadOnly = true;
                    this._dgvOperatorList.Columns["ModelName"].HeaderText = "����Ա����";
                    this._dgvOperatorList.Columns["ModelName"].ToolTipText = "[ֻ����]";
                    this._dgvOperatorList.Columns["ModelName"].DisplayIndex = 1;
                    this._dgvOperatorList.Columns["ModelName"].ReadOnly = true;
                    this._dgvOperatorList.Columns["Password"].HeaderText = "����";
                    this._dgvOperatorList.Columns["Password"].DisplayIndex = 2;
                    this._dgvOperatorList.Columns["State"].HeaderText = "״̬";
                    this._dgvOperatorList.Columns["State"].DisplayIndex = 3;
                    this._dgvOperatorList.Columns["RightsCollection"].HeaderText = "Ȩ���б�";
                    this._dgvOperatorList.Columns["RightsCollection"].DisplayIndex = 4;
                    this._dgvOperatorList.Columns["RightsCollection"].ReadOnly = true;
                    this._dgvOperatorList.Columns["RightsCollection"].Visible = false;

                    // ���ò˵��ָ����Ȩ�ޱ�����Ϊֻ��
                    foreach (DataGridViewRow dgvRow in this._dgvOperatorList.Rows)
                    {
                        // ���õ�Ԫ�񹤾�����ʾ
                        foreach (DataGridViewCell dgvCell in dgvRow.Cells)
                        {
                            if (dgvCell.ReadOnly)
                                dgvCell.ToolTipText = "[ֻ����]";
                            else
                                dgvCell.ToolTipText = "[��д��]";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "����ʧ��",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// ��Ȩ�޼������ݰ���ʾ��������ͼ
        /// </summary>
        /// <param name="rightsCollection">Ȩ�޼���</param>
        internal void BindDataToDataGridView(Dictionary<string, Model.Rights> rightsCollection)
        {
            try
            {
                // ��������ѡ��Ԫ��
                List<string> selectedCellValueList = new List<string>();
                foreach (DataGridViewCell dgvCell in _dgvRightsList.SelectedCells)
                {
                    selectedCellValueList.Add(dgvCell.Value.ToString().Trim());
                }

                // ��Ȩ�޼��ϰ���ʾ��������ͼ��
                BindingSource source = new BindingSource();
                source.DataSource = rightsCollection.Values;
                this._dgvRightsList.DataSource = source;
                // ����������������д״̬
                this._dgvRightsList.Columns["Id"].HeaderText = "���";
                this._dgvRightsList.Columns["Id"].ToolTipText = "[ֻ����]";
                this._dgvRightsList.Columns["Id"].ReadOnly = true;
                this._dgvRightsList.Columns["Id"].DisplayIndex = 0;
                this._dgvRightsList.Columns["RightsCaption"].HeaderText = "Ȩ�ޱ���";
                this._dgvRightsList.Columns["RightsCaption"].DisplayIndex = 1;
                this._dgvRightsList.Columns["ModelName"].HeaderText = "�ڲ�����";
                this._dgvRightsList.Columns["ModelName"].ToolTipText = "[ֻ����]";
                this._dgvRightsList.Columns["ModelName"].ReadOnly = true;
                this._dgvRightsList.Columns["ModelName"].Visible = false;
                this._dgvRightsList.Columns["ModelName"].DisplayIndex = 2;
                this._dgvRightsList.Columns["RightsState"].HeaderText = "Ȩ��״̬";
                this._dgvRightsList.Columns["RightsState"].DisplayIndex = 3;
                this._dgvRightsList.Columns["ParentLevelRightsName"].HeaderText = "����Ȩ��";
                this._dgvRightsList.Columns["ParentLevelRightsName"].ToolTipText = "[ֻ����]";
                this._dgvRightsList.Columns["ParentLevelRightsName"].ReadOnly = true;
                this._dgvRightsList.Columns["ParentLevelRightsName"].DisplayIndex = 4;
                this._dgvRightsList.Columns["ParentLevelRightsName"].Visible = false;

                // ���ò˵��ָ����Ȩ�ޱ�����Ϊֻ��
                foreach (DataGridViewRow dgvRow in this._dgvRightsList.Rows)
                {
                    // ����ǲ˵��ָ���������Ϊֻ��
                    if (dgvRow.Cells["RightsCaption"].Value.ToString().Trim() == "��������")
                        dgvRow.Cells["RightsCaption"].ReadOnly = true;
                    // ���õ�Ԫ�񹤾�����ʾ
                    foreach (DataGridViewCell dgvCell in dgvRow.Cells)
                    {
                        if (dgvCell.ReadOnly)
                        {
                            dgvCell.ToolTipText = "[ֻ����]";
                            if (dgvCell.Value.ToString().Trim() == "��������")
                                dgvCell.ToolTipText += " | <-- �˵��ָ� -->";
                        }
                        else
                            dgvCell.ToolTipText = "[��д��]";
                    }
                }

                // ���ù���ѡ��
                RightsManagerUI.IsRelatingChooseCells = false;
                // ������е�Ԫ��ѡ��
                _dgvRightsList.ClearSelection();
                // ��ԭԭ��ѡ��
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
                // ���ù���ѡ��
                RightsManagerUI.IsRelatingChooseCells = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "����ʧ��",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// �����ݰ󶨼��ص�������ͼ
        /// </summary>
        /// <param name="rightsCollection">Ȩ�޼���</param>
        internal void BindDataToTreeView(Dictionary<string, Model.Rights> rightsCollection)
        {
            // ��������ͼ���ػ�
            _tvRightsView.BeginUpdate();
            // ���ԭ�нڵ�
            _tvRightsView.Nodes.Clear();
            // ����Ȩ�޼����Լ�������
            foreach (Model.Rights tmpRights in rightsCollection.Values)
            {
                // ����Ȩ�޸���
                TreeNode rootTreeNode = null;
                // �����Ȩ�޸���
                if (tmpRights.ParentLevelRightsName == _msMain.Name)
                {
                    rootTreeNode = new TreeNode(tmpRights.RightsCaption);
                    rootTreeNode.Tag = tmpRights.ModelName;
                    rootTreeNode.Checked = tmpRights.RightsState;
                    _tvRightsView.Nodes.Add(rootTreeNode);
                }
                // �����Ȩ������
                else
                {
                    // ����Ȩ������
                    TreeNode childTreeNode = new TreeNode(tmpRights.RightsCaption);
                    childTreeNode.Tag = tmpRights.ModelName;
                    childTreeNode.Checked = tmpRights.RightsState;
                    // ��������ӵ���Ӧ�ĸ�����
                    foreach (TreeNode tmpTreeNode in _tvRightsView.Nodes)
                    {
                        // ������ִ�Ľڵ㸸��Ȩ����ͬ
                        if (tmpTreeNode.Tag.ToString() == tmpRights.ParentLevelRightsName)
                        {
                            // �ݹ�������в㼶�ӽڵ�
                            tmpTreeNode.Nodes.Add(LoadAllChildTreeNode(childTreeNode, rightsCollection));
                        }
                    }
                }
            }

            // չ���������ڵ�
            _tvRightsView.ExpandAll();

            // ��������ͼ���ػ�
            _tvRightsView.EndUpdate();
        }

        /// <summary>
        /// ѡ��/ȡ�����ڵ㼰���ӽڵ㹴ѡ״̬
        /// </summary>
        /// <param name="currentTreeNode">��ǰ�����Ľڵ�</param>
        internal void CheckOrUnCheckTreeNode(TreeNode currentTreeNode)
        {
            // �����ѡ�е�Ԫ��
            if (_dgvOperatorList.SelectedCells.Count > 0)
            {
                // ���浱ǰѡ���еĲ���Ա����
                string operatorName = _dgvOperatorList.Rows[_dgvOperatorList.SelectedCells[0].RowIndex].Cells["ModelName"].Value.ToString().Trim();
                // ͬ��Ȩ��״̬
                RightsManagerUI.OperatorCollection[operatorName].RightsCollection[currentTreeNode.Tag.ToString()].RightsState = currentTreeNode.Checked;

                // ͬʱѡ��/ȡ���ӽڵ�Ĺ�ѡ
                foreach (TreeNode childTreeNode in currentTreeNode.Nodes)
                {
                    // ͬ���ӽڵ㹴ѡ״̬
                    childTreeNode.Checked = currentTreeNode.Checked;
                    // �ݹ鹴ѡ�²��ӽڵ�
                    CheckOrUnCheckTreeNode(childTreeNode);
                }
            }
        }

        /// <summary>
        /// ���ز���Ա�б�
        /// </summary>
        internal void LoadOperatorList()
        {
            BLLFactory.BLLFactory bllFactory = new BLLFactory.BLLFactory();
            IBLL.IOperatorManager operatorManager = bllFactory.BuildOperatorManager();

            // ���ز���Ա�б�
            try
            {
                RightsManagerUI.OperatorCollection = operatorManager.GetAllOperatorInfo();
                // ������в���Ա��Ȩ���б�
                foreach (Model.Operator tmpOperator in RightsManagerUI.OperatorCollection.Values)
                {
                    // ���Ȩ��Ϊ�վʹ���һ���µĿ�Ȩ�޼���
                    if (!(tmpOperator.RightsCollection is Dictionary<string, Model.Rights>))
                    {
                        tmpOperator.RightsCollection = new Dictionary<string, Model.Rights>();
                        // ����Ȩ�޹�����ʵ��
                        RightsMenuDataManager rmdManager = new RightsMenuDataManager();
                        // ����Ȩ�޼��Ͽսṹ
                        tmpOperator.RightsCollection = rmdManager.ReadMenuRightsItem(_msMain, tmpOperator.RightsCollection);
                    }
                }

                // �����������Ա��Ϣ
                if (RightsManagerUI.OperatorCollection.Count > 0)
                {
                    // ������Ա�������ݰ���ʾ��������ͼ
                    BindOperatorInfoToDataGridView(RightsManagerUI.OperatorCollection);
                    // ����ָ����ǰ��¼����Ա����
                    foreach (Model.Operator tmpOperator in RightsManagerUI.OperatorCollection.Values)
                    {
                        if (tmpOperator.ModelName == RightsManagerUI.CurrentOperator.ModelName)
                        {
                            RightsManagerUI.CurrentOperator = RightsManagerUI.OperatorCollection[RightsManagerUI.CurrentOperator.ModelName];
                            // �����ݰ���ʾ��������ͼ
                            BindDataToDataGridView(RightsManagerUI.CurrentOperator.RightsCollection);
                            // �����ݰ󶨼��ص�������ͼ
                            BindDataToTreeView(RightsManagerUI.CurrentOperator.RightsCollection);

                            // �ڲ���Ա�б���ѡ�е�ǰ����Ա
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
                    // ��ղ���Ա�б��������
                    DgvOperatorList.Rows.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "����ʧ��",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
        #endregion
    }
}
