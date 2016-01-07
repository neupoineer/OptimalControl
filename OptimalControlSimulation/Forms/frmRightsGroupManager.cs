using System;
using System.Collections.Generic;
using System.Windows.Forms;
using IBLL;
using Model;
using OptimalControl.Common;

namespace OptimalControl.Forms
{
    /// <summary>
    /// Ȩ����������
    /// </summary>
    public partial class frmRightsGroupManager : Form
    {
        #region Private Members
        /// <summary>
        /// ��ǰ���в���Ա��Ϣ����
        /// </summary>
        Dictionary<string, Operator> _operatorCollection = null;
        /// <summary>
        /// ��ǰ����Ȩ�޷�����Ϣ����
        /// </summary>
        Dictionary<string, RightsGroup> _rightsGroupCollection = null;
        /// <summary>
        /// ��ǰ����Ȩ�޹�ϵ��Ϣ����
        /// </summary>
        List<RightsRelation> _rightsRelationList = null;
        /// <summary>
        /// Ȩ�޲˵�������������
        /// </summary>
        RightsMenuUIManager _rmuManager = null;
        /// <summary>
        /// Ȩ�޲˵����ݹ��������
        /// </summary>
        RightsMenuDataManager _rmdManager = null;
        /// <summary>
        /// ���ǰȨ�����������Ȩ�޹���������
        /// </summary>
        frmRightsManager _frmRightsManager = null;
        /// <summary>
        /// ϵͳ������
        /// </summary>
        FrmMain _frmMain = null;
        /// <summary>
        /// ��ǰ�༭��Ȩ��������
        /// </summary>
        string _currentEditGroupName = null;
        /// <summary>
        /// Ȩ����ͼ�ɹ�ѡ״̬
        /// </summary>
        bool _rightsViewIsChecked = true;
        /// <summary>
        /// ������һ��ѡ�е�Ȩ����������
        /// </summary>
        DataGridViewRow _lastSelectedRightsGroupRow = null;
        #endregion

        #region Private Methods
        /// <summary>
        /// ����Ȩ�������ƻ�ȡȨ���� ID
        /// </summary>
        /// <param name="rightsGroupName">Ȩ��������</param>
        /// <returns>Ȩ���� ID</returns>
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
        /// ���ݲ���Ա���ƻ�ȡ����Ա ID
        /// </summary>
        /// <param name="operatorName">����Ա����</param>
        /// <returns>����Ա ID</returns>
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
        /// ѡ��/ȡ�����ڵ㼰���ӽڵ㹴ѡ״̬
        /// </summary>
        /// <param name="currentTreeNode">��ǰ�����Ľڵ�</param>
        private void CheckOrUnCheckTreeNode(TreeNode currentTreeNode)
        {
            // �����ѡ�е�Ԫ��
            if (dgvGroupList.SelectedCells.Count > 0)
            {
                // ��ȡ��ǰѡ�е�Ȩ��������
                string groupName = dgvGroupList.SelectedCells[0].OwningRow.Cells["Name"].Value.ToString();

                // ͬ��Ȩ��״̬
                _rightsGroupCollection[groupName].GroupRightsCollection[currentTreeNode.Tag.ToString()].RightsState = currentTreeNode.Checked;

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
        /// ��ȡ����Ȩ����������ʾ
        /// </summary>
        private void BindDataToRightsGroupList()
        {
            try
            {
                // ����������ʵ��
                BLLFactory.BLLFactory bllFactory = new BLLFactory.BLLFactory();
                // ����Ȩ���������ʵ��
                IRightsGroupManager rightsGroupManager = bllFactory.BuildRightsGroupManager();
                // ����ʵ������
                _rightsGroupCollection = rightsGroupManager.GetAllRightsGroupInfo();

                // ����ȡ�õ�Ȩ���鼯��
                foreach (RightsGroup tmpRightsGroup in _rightsGroupCollection.Values)
                {
                    // �����ǰȨ�����Ȩ�޼���Ϊ���򴴽��µĿհ�Ȩ��
                    if (!(tmpRightsGroup.GroupRightsCollection is Dictionary<string, Rights>))
                    {
                        tmpRightsGroup.GroupRightsCollection = new Dictionary<string, Rights>();
                        tmpRightsGroup.GroupRightsCollection =
                            _rmdManager.ReadMenuRightsItem(tmpRightsGroup.GroupRightsCollection);
                    }
                }

                // �������Ȩ������Ϣ
                if (_rightsGroupCollection.Count > 0)
                {
                    // ��Ȩ����������ʾ
                    BindingSource source = new BindingSource();
                    source.DataSource = _rightsGroupCollection.Values;
                    dgvGroupList.DataSource = source;

                    // ������������
                    dgvGroupList.Columns["Id"].HeaderText = "������";
                    dgvGroupList.Columns["Id"].ToolTipText = "[ֻ����]";
                    dgvGroupList.Columns["Id"].DisplayIndex = 0;
                    dgvGroupList.Columns["Id"].ReadOnly = true;
                    dgvGroupList.Columns["Id"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                    dgvGroupList.Columns["Name"].HeaderText = "��������";
                    dgvGroupList.Columns["Name"].DisplayIndex = 1;
                    dgvGroupList.Columns["GroupRightsCollection"].HeaderText = "����Ȩ��";
                    dgvGroupList.Columns["GroupRightsCollection"].DisplayIndex = 2;
                    dgvGroupList.Columns["GroupRightsCollection"].ReadOnly = true;
                    dgvGroupList.Columns["GroupRightsCollection"].Visible = false;

                    // ���ù�����ʾ
                    foreach (DataGridViewRow dgvRow in dgvGroupList.Rows)
                    {
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
        /// ��ȡ����Ȩ�޹�ϵ������ʾ
        /// </summary>
        private void BindDataToRightsRelationList()
        {
            try
            {
                // ����������ʵ��
                BLLFactory.BLLFactory bllFactory = new BLLFactory.BLLFactory();
                // ����Ȩ�޹�ϵ������ʵ��
                IRightsRelationManager rightsRelationManager = bllFactory.BuildRightsRelationManager();
                // ����ʵ������
                _rightsRelationList = rightsRelationManager.GetAllRightsRelation();
                // ��������������ʵ��
                IOperatorManager operatorManager = bllFactory.BuildOperatorManager();
                // ����ʵ������
                _operatorCollection = operatorManager.GetAllOperatorInfo();

                // ���ԭ�е���
                dgvRightsRelationList.Columns.Clear();

                // �ֶ�����������
                //
                // dgvColId
                // 
                DataGridViewTextBoxColumn dgvColId = new DataGridViewTextBoxColumn();
                dgvColId.Name = "dgvColId";
                dgvColId.HeaderText = "��ϵ���";
                dgvColId.ToolTipText = "[ֻ����]";
                dgvColId.DisplayIndex = 0;
                dgvColId.ReadOnly = true;
                dgvColId.DataPropertyName = "Id";
                dgvColId.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                //
                // dgvColOperatorId
                // 
                DataGridViewTextBoxColumn dgvColOperatorId = new DataGridViewTextBoxColumn();
                dgvColOperatorId.Name = "dgvColOperatorId";
                dgvColOperatorId.HeaderText = "�û����";
                dgvColOperatorId.ToolTipText = "[ֻ����]";
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
                dgvColOperatorName.HeaderText = "�û�����";
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
                dgvColRightsGroupId.HeaderText = "������";
                dgvColRightsGroupId.ToolTipText = "[ֻ����]";
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
                dgvColRightsGroupName.HeaderText = "��������";
                dgvColRightsGroupName.DisplayIndex = 4;
                dgvColRightsGroupName.DataPropertyName = "RightsGroupName";
                BindingSource rightsGroupSource = new BindingSource();
                rightsGroupSource.DataSource = _rightsGroupCollection.Values;
                dgvColRightsGroupName.DataSource = rightsGroupSource;
                dgvColRightsGroupName.DisplayMember = "Name";
                dgvColRightsGroupName.ValueMember = "Name";

                // ����½�����
                dgvRightsRelationList.Columns.AddRange(new DataGridViewColumn[] {
                dgvColId,
                dgvColOperatorId,
                dgvColOperatorName,
                dgvColRightsGroupId,
                dgvColRightsGroupName});

                //// �ֶ����������(����Ȩ�޹�ϵ��������)
                //dgvRightsRelationList.Rows.Add(_rightsRelationList.Count);
                //// �ֶ�Ϊ�����и�ֵ
                //foreach (Model.RightsRelation tmpRightsRelation in _rightsRelationList)
                //{
                //    dgvRightsRelationList.Rows[tmpRightsRelation.Id - 1].Cells["dgvColId"].Value = tmpRightsRelation.Id;
                //    dgvRightsRelationList.Rows[tmpRightsRelation.Id - 1].Cells["dgvColOperatorId"].Value = tmpRightsRelation.OperatorId;
                //    dgvRightsRelationList.Rows[tmpRightsRelation.Id - 1].Cells["dgvColOperatorName"].Value = tmpRightsRelation.OperatorId;
                //    dgvRightsRelationList.Rows[tmpRightsRelation.Id - 1].Cells["dgvColRightsGroupId"].Value = tmpRightsRelation.RightsGroupId;
                //    dgvRightsRelationList.Rows[tmpRightsRelation.Id - 1].Cells["dgvColRightsGroupName"].Value = tmpRightsRelation.RightsGroupId;
                //}

                // ʹ�ð���ʾ������
                BindingSource source = new BindingSource();
                source.DataSource = _rightsRelationList;
                dgvRightsRelationList.AutoGenerateColumns = false;
                dgvRightsRelationList.DataSource = source;

                // ���ù�����ʾ
                foreach (DataGridViewRow dgvRow in dgvRightsRelationList.Rows)
                {
                    foreach (DataGridViewCell dgvCell in dgvRow.Cells)
                    {
                        if (dgvCell.ReadOnly)
                            dgvCell.ToolTipText = "[ֻ����]";
                        else
                            dgvCell.ToolTipText = "[��д��]";
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
        /// ��Ȩ����������ͼ������ʾ
        /// </summary>
        private void BindDataToGroupView()
        {
            // ��������ͼ�ػ�
            tvGroupView.BeginUpdate();

            // ����������ڵ�
            tvGroupView.Nodes.Clear();

            try
            {
                // ������ǰȨ�޹�ϵ�б�
                foreach (RightsRelation rootRightsRelation in _rightsRelationList)
                {
                    // �������ڵ�
                    TreeNode rootTreeNode = null;
                    // �Ƿ���ڴ˸��ڵ�
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

                    // ��������ڴ˸��ڵ�
                    if (!rootIsExist)
                    {
                        rootTreeNode = new TreeNode(rootRightsRelation.OperatorName);
                        rootTreeNode.Tag = rootRightsRelation.OperatorId;

                        // �����ڵ���ӵ���
                        tvGroupView.Nodes.Add(rootTreeNode);
                    }

                    // ��������ӽڵ�
                    foreach (RightsRelation tmpRightsRelation in _rightsRelationList)
                    {
                        if (tmpRightsRelation.OperatorId == Convert.ToInt32(rootRightsRelation.OperatorId))
                        {
                            // �����ӽڵ�
                            TreeNode childTreeNode = null;
                            // �Ƿ���ڴ��ӽڵ�
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

                            // ��������ڴ��ӽڵ�
                            if (!childIsExist)
                            {
                                childTreeNode = new TreeNode(tmpRightsRelation.RightsGroupName);
                                childTreeNode.Tag = tmpRightsRelation.RightsGroupId;

                                // ���ӽڵ���ӵ����ڵ�
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
                    "����ʧ��",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }

            // չ���������ڵ�
            tvGroupView.ExpandAll();

            // ��������ͼ�ػ�
            tvGroupView.EndUpdate();
        }

        /// <summary>
        /// �������и���
        /// </summary>
        /// <param name="isShowTip">�Ƿ���ʾ����ɹ�����ʾ</param>
        private void SaveAllChanged(bool isShowTip)
        {
            // ����������ʵ��
            BLLFactory.BLLFactory bllFactory = new BLLFactory.BLLFactory();

            // �ύ������������ͼ�ĵ�Ԫ��༭
            dgvGroupList.EndEdit();
            dgvRightsRelationList.EndEdit();

            // ����Ȩ�޹�ϵ�Ƿ�ɹ�
            bool SaveRightsRelationIsOk = true;

            // ����Ȩ�޹�ϵ��Ϣ
            try
            {
                IRightsRelationManager rightsRelationManager = bllFactory.BuildRightsRelationManager();
                // ����ʵ������
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
                    "����ʧ��",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }

            if (!SaveRightsRelationIsOk)
                MessageBox.Show(
                    "����Ȩ�޹�ϵ��Ϣʧ�ܣ�",
                    "����ʧ��",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

            // ����Ȩ������Ϣ�Ƿ�ɹ�
            bool SaveRightsGroupIsOk = true;

            // ����Ȩ������Ϣ
            try
            {
                IRightsGroupManager rightsGroupManager = bllFactory.BuildRightsGroupManager();
                // ����ʵ������
                foreach (RightsGroup tmpRightsGroup in _rightsGroupCollection.Values)
                {
                    // �����ǰȨ�����Ȩ�޼���Ϊ���򴴽��µĿհ�Ȩ��
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
                    "����ʧ��",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }

            if (!SaveRightsGroupIsOk)
                MessageBox.Show(
                    "����Ȩ������Ϣʧ�ܣ�",
                    "����ʧ��",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

            // �������Ա��Ϣ�Ƿ�ɹ�
            bool SaveOperatorIsOk = true;

            // ���ݲ���Ա��������Ȩ����ˢ�²��������ԱȨ��
            try
            {
                // �����в���ԱȨ��״̬��Ϊ����״̬
                foreach (Operator tmpOperator in _operatorCollection.Values)
                    foreach (Rights tmpRights in tmpOperator.RightsCollection.Values)
                        tmpRights.RightsState = false;

                // ����Ȩ�޹�ϵʵ���б�
                foreach (RightsRelation tmpRightsRelation in _rightsRelationList)
                    // ��������Աʵ�弯��
                    foreach (Operator tmpOperator in _operatorCollection.Values)
                        // �������Ա������Ȩ�޹�ϵ����ͬ
                        if (tmpOperator.Name == tmpRightsRelation.OperatorName)
                            // ����Ȩ����ʵ��Ȩ�޼���
                            foreach (Rights tmpGroupRights in _rightsGroupCollection[tmpRightsRelation.RightsGroupName].GroupRightsCollection.Values)
                                // ��������ԱȨ�޼���
                                foreach (Rights tmpRights in tmpOperator.RightsCollection.Values)
                                    // ���Ȩ��������ͬ
                                    if (tmpRights.Name == tmpGroupRights.Name)
                                        // �����Ȩ��Ϊ��ʾ״̬����Ȩ�޸��������ԱȨ��
                                        if (tmpGroupRights.RightsState)
                                            tmpRights.RightsState = tmpGroupRights.RightsState;

                // ����������ʵ��
                IOperatorManager operatorManager = bllFactory.BuildOperatorManager();

                // �����������ԱȨ��
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
                    "����ʧ��",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }

            if (!SaveOperatorIsOk)
                MessageBox.Show(
                    "�������Ա��Ϣʧ�ܣ�",
                    "����ʧ��",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

            if (isShowTip)
            {
                // ��������ɹ���ʾ
                if (SaveRightsRelationIsOk && SaveRightsGroupIsOk && SaveOperatorIsOk)
                    MessageBox.Show(
                        "��������Ȩ�޷��鼰�������Ϣ�ɹ���",
                        "����ɹ�",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
            }
        }

        #endregion

        #region Internal Methods
        /// <summary>
        /// ˢ��������ʾ
        /// </summary>
        internal void RefreshDataDisplay()
        {
            // ��Ȩ����������ʾ
            BindDataToRightsGroupList();
            // ��Ȩ�޹�ϵ������ʾ
            BindDataToRightsRelationList();
            // ��������ͼ������ʾ
            BindDataToGroupView();

            // ����Ȩ��������ͼ������ʾ(�󶨵�ǰѡ��Ȩ�����Ȩ������)
            if (dgvGroupList.SelectedCells.Count > 0)
            {
                // ����Ȩ��������
                string groupName = dgvGroupList.SelectedCells[0].OwningRow.Cells["Name"].Value.ToString();
                // ��ʶ�Ƿ���ڴ�Ȩ����
                bool isExistGroup = false;
                // У��ָ��Ȩ���������Ƿ����
                foreach (RightsGroup tmpRightsGroup in _rightsGroupCollection.Values)
                {
                    if (tmpRightsGroup.Name == groupName)
                    {
                        // ��������ͼ��ʾ
                        _rmuManager.BindDataToTreeView(
                        _rightsGroupCollection[groupName].GroupRightsCollection);
                        isExistGroup = true;
                        break;
                    }
                }
                // ��������ڴ�Ȩ����
                if (!isExistGroup)
                {
                    // ���Ȩ�����б�������
                    dgvGroupList.Rows.Clear();
                    // �������������ͼ��ʾ
                    tvRightsView.Nodes.Clear();
                }
            }
        }

        /// <summary>
        /// ѡ����ĩ��Ȩ�޹�ϵ�����ֿɼ�
        /// </summary>
        internal void SelectLastRelation()
        {
            // ѡ����ĩ�ʹ��������ֿɼ�
            if (dgvRightsRelationList.Rows.Count > 0)
            {
                dgvRightsRelationList.Rows[dgvRightsRelationList.Rows.Count - 1].Selected = true;
                dgvRightsRelationList.FirstDisplayedScrollingRowIndex = dgvRightsRelationList.Rows.Count - 1;
            }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// �޲ι���
        /// </summary>
        /// <param name="frmRightsManager">Ȩ�޹������</param>
        /// <param name="frmMain">ϵͳ������</param>
        public frmRightsGroupManager(frmRightsManager frmRightsManager, FrmMain frmMain)
        {
            InitializeComponent();

            // ����Ȩ�޹���������
            _frmRightsManager = frmRightsManager;
            // ����ϵͳ������
            _frmMain = frmMain;

            // ����Ȩ�޲˵����������ʵ��
            _rmuManager = new RightsMenuUIManager();
            // ����Ҫ�����Ķ���
            _rmuManager.TvRightsView = this.tvRightsView;
            _rmuManager.MsMain = _frmRightsManager.MsMain;

            // ����Ȩ�޲˵����ݹ�����ʵ��
            _rmdManager = new RightsMenuDataManager();
            // ����Ҫ�����Ķ���
            _rmdManager.MsMain = _frmRightsManager.MsMain;
        } 
        #endregion

        #region Event Handlers
        /// <summary>
        /// ��������¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmRightsGroupManager_Load(object sender, EventArgs e)
        {
            // ˢ��������ʾ
            RefreshDataDisplay();
        }

        /// <summary>
        /// [Ȩ�����б�]��Ԫ�������༭ģʽ�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvGroupList_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            // ���浱ǰѡ�е�Ȩ��������
            _currentEditGroupName = Convert.ToString(dgvGroupList.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
        }

        /// <summary>
        /// [Ȩ�����б�]��Ԫ��ֹͣ�༭ģʽ�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvGroupList_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            // �����Ȩ��������
            if (dgvGroupList.Columns[e.ColumnIndex].Name == "Name")
            {
                // ��ȡ��ǰѡ�е�Ȩ���� ID
                int groupId = Convert.ToInt32(dgvGroupList.Rows[e.RowIndex].Cells["Id"].Value);
                // ��ȡ��ǰѡ�е�Ȩ��������
                string groupName = Convert.ToString(dgvGroupList.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                // �޸ĵ�ǰȨ����������
                _rightsGroupCollection[_currentEditGroupName].Name = groupName;

                try
                {
                    // ����Ȩ����ҵ���߼�������ʵ��
                    BLLFactory.BLLFactory bllFactory = new BLLFactory.BLLFactory();
                    // ����Ȩ����ҵ���߼�������ʵ��
                    IRightsGroupManager rightsGroupManager = bllFactory.BuildRightsGroupManager();
                    // �����ǰȨ����Ȩ�޼���Ϊ���򴴽��µ�Ȩ�޽ṹ
                    if (!(_rightsGroupCollection[_currentEditGroupName].GroupRightsCollection is
                        Dictionary<string, Rights>))
                    {
                        // �����µ�Ȩ�޽ṹ
                        _rightsGroupCollection[_currentEditGroupName].GroupRightsCollection =
                            new Dictionary<string, Rights>();
                        // ��ȡ�հ�Ȩ��
                        _rightsGroupCollection[_currentEditGroupName].GroupRightsCollection =
                            _rmdManager.ReadMenuRightsItem(_rightsGroupCollection[_currentEditGroupName].GroupRightsCollection);
                    }
                    // ����ʵ������
                    if (!rightsGroupManager.ModifyRightsGroup(_rightsGroupCollection[_currentEditGroupName]))
                    {
                        MessageBox.Show(
                            string.Format("Ȩ���� [{0}] ����Ϣ�޸�ʧ�ܣ�", _currentEditGroupName),
                            "�޸���ʾ",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        return;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                    ex.Message,
                    "�޸�ʧ��",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                }
                finally
                {
                    // ����������ʵ��
                    BLLFactory.BLLFactory bllFactory = new BLLFactory.BLLFactory();
                    // ����Ȩ���������ʵ��
                    IRightsGroupManager rightsGroupManager = bllFactory.BuildRightsGroupManager();
                    // ����ʵ���������¶�ȡȨ������Ϣ
                    _rightsGroupCollection = rightsGroupManager.GetAllRightsGroupInfo();

                    // ��Ȩ�޹�ϵ������ʾ
                    BindDataToRightsRelationList();
                    // ��������ͼ������ʾ
                    BindDataToGroupView();
                }
            }
        }

        /// <summary>
        /// [Ȩ�����б�]ѡ�����ݸ����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvGroupList_SelectionChanged(object sender, EventArgs e)
        {
            // ����Ȩ����ͼ�ɹ�ѡ״̬
            _rightsViewIsChecked = true;

            // �����ѡ�е�Ԫ��
            if (dgvGroupList.SelectedCells.Count > 0)
            {
                // ��������ϴ�ѡ�е���
                if (_lastSelectedRightsGroupRow != dgvGroupList.SelectedCells[0].OwningRow)
                {
                    // �����ѡ�е�Ԫ��
                    if (dgvGroupList.SelectedCells.Count > 0)
                    {
                        // ��ȡ��ǰѡ�е�Ȩ��������
                        string groupName = dgvGroupList.SelectedCells[0].OwningRow.Cells["Name"].Value.ToString();

                        // �����ǰȨ�����Ȩ�޼���Ϊ���򴴽��µĿհ�Ȩ��
                        if (!(_rightsGroupCollection[groupName].GroupRightsCollection is Dictionary<string, Rights>))
                        {
                            _rightsGroupCollection[groupName].GroupRightsCollection = new Dictionary<string, Rights>();
                            _rightsGroupCollection[groupName].GroupRightsCollection =
                                _rmdManager.ReadMenuRightsItem(_rightsGroupCollection[groupName].GroupRightsCollection);
                        }

                        // ���ԭ�еĵ�Ԫ��ѡ��
                        dgvRightsRelationList.ClearSelection();
                        // ͬ��ѡ��Ȩ�޹�ϵ�б��е����д˲���Ա�Ĵ�Ȩ���鵥Ԫ��
                        foreach (DataGridViewRow dgvRow in dgvRightsRelationList.Rows)
                        {
                            if (dgvRow.Cells["dgvColRightsGroupName"].Value.ToString() == groupName)
                                dgvRow.Cells["dgvColRightsGroupName"].Selected = true;
                        }

                        // ����Ȩ��������ͼ������ʾ(�󶨵�ǰѡ���Ȩ����Ȩ������)
                        _rmuManager.BindDataToTreeView(_rightsGroupCollection[groupName].GroupRightsCollection);
                    }

                    // ���浱ǰѡ�е�������
                    _lastSelectedRightsGroupRow = dgvGroupList.SelectedCells[0].OwningRow;
                }
            }
        }

        /// <summary>
        /// [Ȩ�޹�ϵ�б�]��Ԫ��ֹͣ�༭ģʽ�¼�
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

            // ������ʾ 
            dgvRightsRelationList.CurrentRow.Cells["dgvColOperatorId"].Value = 
                _rightsRelationList[e.RowIndex].OperatorId;
            dgvRightsRelationList.CurrentRow.Cells["dgvColRightsGroupId"].Value =
                _rightsRelationList[e.RowIndex].RightsGroupId;

            // ��Ȩ������ͼ������ʾ
            BindDataToGroupView();
        }

        /// <summary>
        /// [�������]��������ť����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnSaveAllChanged_Click(object sender, EventArgs e)
        {
            // �������и��Ĳ�Ҫ����ʾ����ɹ���ʾ
            SaveAllChanged(true);

            // ˢ��������ʾ
            RefreshDataDisplay();
        }

        /// <summary>
        /// [�رմ���]��������ť����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnCloseWindow_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// [Ȩ������ͼ]����ѡ�������¼� 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvGroupView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                // ��ѡ�����Ա����ʱ
                if (e.Node.Level == 0)
                {
                    // ����Ȩ����ͼ�ɹ�ѡ״̬
                    _rightsViewIsChecked = false;

                    if (!(_operatorCollection[e.Node.Text].RightsCollection is 
                        Dictionary<string, Rights>))
                    {
                        // Ϊ��ǰѡ��Ĳ���Ա�����µĿ�Ȩ������
                        _operatorCollection[e.Node.Text].RightsCollection =
                            new Dictionary<string, Rights>();
                        _operatorCollection[e.Node.Text].RightsCollection =
                            _rmdManager.ReadMenuRightsItem(_operatorCollection[e.Node.Text].RightsCollection);
                    }

                    // ����Ȩ��������ͼ������ʾ(�󶨵�ǰѡ��Ĳ���ԱȨ������)
                    _rmuManager.BindDataToTreeView(_operatorCollection[e.Node.Text].RightsCollection);
                    // ���ԭ�еĵ�Ԫ��ѡ��
                    dgvRightsRelationList.ClearSelection();
                    // ͬ��ѡ��Ȩ�޹�ϵ�б��е����д˲���Ա��Ԫ��
                    foreach (DataGridViewRow dgvRow in dgvRightsRelationList.Rows)
                    {
                        if (dgvRow.Cells["dgvColOperatorName"].Value.ToString() == e.Node.Text)
                            dgvRow.Cells["dgvColOperatorName"].Selected = true;
                    }
                }
                // ��ѡ�����Ա������Ȩ������ʱ
                else if (e.Node.Level == 1)
                {
                    // ����Ȩ����ͼ�ɹ�ѡ״̬
                    _rightsViewIsChecked = true;

                    if (!(_rightsGroupCollection[e.Node.Text].GroupRightsCollection is 
                        Dictionary<string, Rights>))
                    {
                        // Ϊ��ǰѡ���Ȩ���鴴���µĿ�Ȩ������
                        _rightsGroupCollection[e.Node.Text].GroupRightsCollection =
                            new Dictionary<string, Rights>();
                        _rightsGroupCollection[e.Node.Text].GroupRightsCollection =
                            _rmdManager.ReadMenuRightsItem(_rightsGroupCollection[e.Node.Text].GroupRightsCollection);
                    }

                    // ѡ��Ȩ�����б��еĶ�Ӧ����
                    foreach (DataGridViewRow dgvRow in dgvGroupList.Rows)
                    {
                        if (dgvRow.Cells["Name"].Value.ToString() == e.Node.Text)
                            dgvRow.Selected = true;
                    }

                    // ����Ȩ��������ͼ������ʾ(�󶨵�ǰѡ���Ȩ����Ȩ������)
                    _rmuManager.BindDataToTreeView(_rightsGroupCollection[e.Node.Text].GroupRightsCollection);
                    // ���ԭ�еĵ�Ԫ��ѡ��
                    dgvRightsRelationList.ClearSelection();
                    // ͬ��ѡ��Ȩ�޹�ϵ�б��е����д˲���Ա�Ĵ�Ȩ���鵥Ԫ��
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
                    string.Format("����Ȩ����ͼʧ�ܣ�\r\n\r\n{0}", ex.Message), 
                    "����ʧ��", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// [Ȩ������ͼ]��Ԫ����֤�¼���
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
                        "Ȩ�������Ʋ�����Ϊ���ַ�(�ո�)��";
                    MessageBox.Show(
                        dgvGroupList.Rows[e.RowIndex].ErrorText, 
                        "�༭��ʾ", 
                        MessageBoxButtons.OK, 
                        MessageBoxIcon.Error);
                    e.Cancel = true;
                }
            }
        }

        /// <summary>
        /// [Ȩ����ͼ]�Ľڵ㸴ѡǰ�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvRightsView_BeforeCheck(object sender, TreeViewCancelEventArgs e)
        {
            // ���Ȩ����ͼ���ɹ�ѡ��ȡ����ѡ�¼�
            if (!_rightsViewIsChecked)
                e.Cancel = true;
        }

        /// <summary>
        /// [Ȩ����ͼ]�Ľڵ㸴ѡ�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvRightsView_AfterCheck(object sender, TreeViewEventArgs e)
        {
            // �ݹ鹴ѡ�ڵ㼰���ӽڵ�
            CheckOrUnCheckTreeNode(e.Node);
        }

        /// <summary>
        /// [��ӷ���]��������ť����¼�
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
                addRightsGroup.Name = "<- δ���� ->";
                addRightsGroup.GroupRightsCollection = new Dictionary<string, Rights>();
                addRightsGroup.GroupRightsCollection = _rmdManager.ReadMenuRightsItem(addRightsGroup.GroupRightsCollection);

                if (rightsGroupManager.CheckRightsGroupExist(addRightsGroup.Name))
                {
                    MessageBox.Show(
                        string.Format("Ĭ����Ȩ������ [{0}] �ѱ�ռ�ã�����ɾ������������Ȩ���飡", addRightsGroup.Name),
                        "��Ӿ���",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                // ִ�����
                if (!(rightsGroupManager.AddRightsGroup(addRightsGroup)))
                {
                    MessageBox.Show(
                        "���Ȩ������Ϣʧ�ܣ�",
                        "���ʧ��",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "���ʧ��",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                // ˢ��������ʾ
                RefreshDataDisplay();
                // ѡ����ĩ�ʹ��������ֿɼ�
                if (dgvGroupList.Rows.Count > 0)
                {
                    dgvGroupList.Rows[dgvGroupList.Rows.Count - 1].Selected = true;
                    dgvGroupList.FirstDisplayedScrollingRowIndex = dgvGroupList.Rows.Count - 1;
                }
            }
        }

        /// <summary>
        /// [ɾ������]��������ť����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnDeleteGroup_Click(object sender, EventArgs e)
        {
            try
            {
                BLLFactory.BLLFactory bllFactory = new BLLFactory.BLLFactory();
                IRightsGroupManager rightsGroupManager = bllFactory.BuildRightsGroupManager();

                // �����ѡ�е�Ԫ��
                if (dgvGroupList.SelectedCells.Count > 0)
                {
                    // ����Ȩ���� ID
                    int rightsGroupId = Convert.ToInt32(dgvGroupList.SelectedCells[0].OwningRow.Cells["Id"].Value);
                    // ����Ȩ���� ����
                    string rightsGroupName = Convert.ToString(dgvGroupList.SelectedCells[0].OwningRow.Cells["Name"].Value);

                    DialogResult result;
                    result = MessageBox.Show(
                        string.Format("ȷʵҪɾ��Ȩ���� [{0}] ��", rightsGroupName),
                        "ɾ����ʾ",
                        MessageBoxButtons.OKCancel,
                        MessageBoxIcon.Question,
                        MessageBoxDefaultButton.Button2);

                    if (result == DialogResult.OK)
                    {
                        // У���Ȩ�����Ƿ��������Ա
                        IRightsRelationManager rightsRelationManager = bllFactory.BuildRightsRelationManager();
                        int relationCount = rightsRelationManager.GetRightsRelationCountByRightsGroupId(rightsGroupId);
                        if (relationCount > 0)
                        {
                            MessageBox.Show(
                                string.Format("Ȩ���� [{0}] �а��� {1} ��Ȩ�޹�ϵ������ɾ�����飡", rightsGroupName, relationCount),
                                "ɾ������",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                            return;
                        }

                        // ִ��ɾ��
                        if (!(rightsGroupManager.DeleteRightsGroupByID(rightsGroupId)))
                        {
                            MessageBox.Show(
                                "ɾ��Ȩ������Ϣʧ�ܣ�",
                                "ɾ��ʧ��",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show(
                        "���� [Ȩ�����б�] ��ѡ��Ҫɾ����Ȩ���飡",
                        "ɾ����ʾ",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "ɾ��ʧ��",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                // ˢ��������ʾ
                RefreshDataDisplay();
            }
        }

        /// <summary>
        /// [ˢ�·���]��������ť����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnRefreshGroup_Click(object sender, EventArgs e)
        {
            // ˢ��������ʾ
            RefreshDataDisplay();
        }

        /// <summary>
        /// [��ӹ�ϵ]��������ť����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnAddRelation_Click(object sender, EventArgs e)
        {
            // ��ʾ���Ȩ�޹�ϵ����
            frmAddRightsRelation frmAddRightsRelation = new frmAddRightsRelation(this);
            frmAddRightsRelation.ShowInTaskbar = false;
            frmAddRightsRelation.ShowDialog();

            // �������и��ĵ���Ҫ����ʾ����ɹ���ʾ
            SaveAllChanged(false);

            // ˢ��������ʾ
            RefreshDataDisplay();
        }

        /// <summary>
        /// [ɾ����ϵ]��������ť����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnDeleteRelation_Click(object sender, EventArgs e)
        {
            try
            {
                BLLFactory.BLLFactory bllFactory = new BLLFactory.BLLFactory();
                IRightsRelationManager rightsRelationManager = bllFactory.BuildRightsRelationManager();

                // �����ѡ�е�Ԫ��
                if (dgvRightsRelationList.SelectedCells.Count > 0)
                {
                    // ����Ȩ�޹�ϵ ID������Ա���Ƽ�Ȩ��������
                    int rightsRelationId = Convert.ToInt32(
                        dgvRightsRelationList.SelectedCells[0].OwningRow.Cells["dgvColId"].Value);
                    string operatorName = Convert.ToString(
                        dgvRightsRelationList.SelectedCells[0].OwningRow.Cells["dgvColOperatorName"].Value);
                    string rightsGroupName = Convert.ToString(
                        dgvRightsRelationList.SelectedCells[0].OwningRow.Cells["dgvColRightsGroupName"].Value);

                    DialogResult result;
                    result = MessageBox.Show(
                        string.Format(
                        "ȷʵҪɾ��Ȩ�޹�ϵ [{0} - {1} - {2}] ��",
                        rightsRelationId,
                        operatorName,
                        rightsGroupName),
                        "ɾ����ʾ",
                        MessageBoxButtons.OKCancel,
                        MessageBoxIcon.Question,
                        MessageBoxDefaultButton.Button2);

                    if (result == DialogResult.OK)
                    {
                        // ִ��ɾ��
                        if (!(rightsRelationManager.DeleteRightsRelationById(rightsRelationId)))
                        {
                            MessageBox.Show(
                                "ɾ��Ȩ������Ϣʧ�ܣ�",
                                "ɾ��ʧ��",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show(
                        "���� [Ȩ�޹�ϵ�б�] ��ѡ��Ҫɾ����Ȩ�޹�ϵ��",
                        "ɾ����ʾ",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "ɾ��ʧ��",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                // ˢ��������ʾ
                RefreshDataDisplay();
            }
        }

        /// <summary>
        /// ����ر�ǰ��δָ���ر�ԭ���¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmRightsGroupManager_FormClosing(object sender, FormClosingEventArgs e)
        {
            // �������и��ĵ���Ҫ����ʾ����ɹ���ʾ
            SaveAllChanged(false);
        }

        #endregion
    }
}