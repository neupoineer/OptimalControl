using System;
using System.Collections.Generic;
using System.Windows.Forms;
using IBLL;
using Model;
using OptimalControl.Common;

namespace OptimalControl.Forms
{
    /// <summary>
    /// Ȩ�޹������
    /// </summary>
    public partial class frmRightsManager : Form
    {
        #region Private Members
        MenuStrip _msMain = null;
        /// <summary>
        /// �����汻����Ĳ˵�����
        /// </summary>
        internal MenuStrip MsMain
        {
            get { return _msMain; }
            set { _msMain = value; }
        }
        frmMain _frmMain = null;
        /// <summary>
        /// ���������
        /// </summary>
        public frmMain FrmMain
        {
            get { return _frmMain; }
            set { _frmMain = value; }
        }
        /// <summary>
        /// Ȩ�޲˵�������������
        /// </summary>
        RightsMenuUIManager _rmuManager = null;
        /// <summary>
        /// Ȩ�޲˵����ݹ��������
        /// </summary>
        RightsMenuDataManager _rmdManager = null;
        Operator _currentOperator = null;
        /// <summary>
        /// ���浱ǰ��¼�Ĳ���Ա����
        /// </summary>
        internal Operator CurrentOperator
        {
            get { return _currentOperator; }
            set { _currentOperator = value; }
        }
        Dictionary<string, Operator> _operatorCollection = null;
        /// <summary>
        /// ��ǰ���ص����в���Աʵ�弯��
        /// </summary>
        internal Dictionary<string, Operator> OperatorCollection
        {
            get { return _operatorCollection; }
            set { _operatorCollection = value; }
        }
        bool _isRelatingChooseCells = true;
        /// <summary>
        /// �Ƿ�����������ͼ����ѡ��
        /// </summary>
        internal bool IsRelatingChooseCells
        {
            get { return _isRelatingChooseCells; }
            set { _isRelatingChooseCells = value; }
        }
        /// <summary>
        /// �Ƿ���������ͼ�ӽڵ����ѡ��
        /// </summary>
        bool _isCheckedChildTreeNode = true;
        /// <summary>
        /// ������һ�α�ѡ�еĲ���Ա�б���
        /// </summary>
        DataGridViewRow _dgvOldSelectedRow = null;
        /// <summary>
        /// Ȩ����ͼ�ɹ�ѡ״̬
        /// </summary>
        bool _rightsViewIsChecked = false;
        #endregion

        #region Public Methods
        /// <summary>
        /// ���ι���
        /// </summary>
        /// <param name="msMain">�����������Ĳ˵�����</param>
        /// <param name="frmMain">���������</param>
        /// <param name="currentOperator">��ǰ��¼����Ա����</param>
        public frmRightsManager(MenuStrip msMain, frmMain frmMain, Operator currentOperator)
        {
            // ����������ؼ�
            InitializeComponent();
            // ����˵�����
            MsMain = msMain;
            // �������������
            FrmMain = frmMain;
            // ���浱ǰ��¼����Ա����
            CurrentOperator = currentOperator;

            // ����Ȩ�޲˵����������ʵ��
            _rmuManager = new RightsMenuUIManager();
            // ����Ҫ�����Ķ���
            _rmuManager.DgvOperatorList = this.dgvOperatorList;
            _rmuManager.DgvRightsList = this.dgvRightsList;
            _rmuManager.TvRightsView = this.tvRightsView;
            _rmuManager.MsMain = _msMain;
            _rmuManager.RightsManagerUI = this;

            // ����Ȩ�޲˵����ݹ�����ʵ��
            _rmdManager = new RightsMenuDataManager();
        } 
        #endregion

        #region Event Handlers
        /// <summary>
        /// �����ʼ���¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmRightsManager_Load(object sender, EventArgs e)
        {
            // ���ز���Ա�б�
            _rmuManager.LoadOperatorList();

            // Ĭ��Ϊ����ģʽ
            tsbtnRightsMode.Text = tsmiGroupMode.Text;
        }

        /// <summary>
        /// ������ͼ�Ҽ��˵���������¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiTreeViewContentMenuItem_Click(object sender, EventArgs e)
        {
            // ���浱ǰ�˵������
            ToolStripMenuItem menuItem = sender as ToolStripMenuItem;
            // ��ѡ���н���ϵĸ�ѡ��
            if (menuItem == tsmiSelectAll)
            {
                foreach (TreeNode treeNode in this.tvRightsView.Nodes)
                {
                    treeNode.Checked = true;
                    // ��������ͼ�ػ�
                    tvRightsView.BeginUpdate();
                    // ѡ��/ȡ�����ڵ㼰���ӽڵ㹴ѡ״̬
                    _rmuManager.CheckOrUnCheckTreeNode(treeNode);
                    // ��������ͼ�ػ�
                    tvRightsView.EndUpdate();
                }
            }
            // ȡ�����н���ϵĸ�ѡ
            if (menuItem == tsmiCancelAll)
            {
                foreach (TreeNode treeNode in this.tvRightsView.Nodes)
                {
                    treeNode.Checked = false;
                    // ��������ͼ�ػ�
                    tvRightsView.BeginUpdate();
                    // ѡ��/ȡ�����ڵ㼰���ӽڵ㹴ѡ״̬
                    _rmuManager.CheckOrUnCheckTreeNode(treeNode);
                    // ��������ͼ�ػ�
                    tvRightsView.EndUpdate();
                }
            }
            // ���¼����˻�Ȩ�޽ṹ��
            if (menuItem == tsmiReload)
            {
                // �����ѡ����
                if (dgvOperatorList.SelectedCells.Count > 0)
                {
                    // ���浱ǰѡ���еĲ���Ա����
                    string operatorName = _dgvOldSelectedRow.Cells["Name"].Value.ToString().Trim();
                    // ָ����ǰ�༭�Ĳ���Ա����
                    Operator editOperator = _operatorCollection[operatorName];

                    // �����ݰ󶨼��ص�������ͼ
                    _rmuManager.BindDataToTreeView(editOperator.RightsCollection);
                }
            }
            // �� TreeView �����ȫ��չ��
            if (menuItem == tsmiExpandAll)
            {
                this.tvRightsView.ExpandAll();
            }
            // �� TreeView �����ȫ���۵�
            if (menuItem == tsmiCollapseAll)
            {
                this.tvRightsView.CollapseAll();
            }
        }

        /// <summary>
        /// [�������]��������ť����¼� 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnSaveAllChanged_Click(object sender, EventArgs e)
        {
            // �ύ������������ͼ�ĵ�Ԫ��༭
            dgvRightsList.EndEdit();
            dgvOperatorList.EndEdit();
            
            // �����ѡ�е�Ԫ��
            if (dgvOperatorList.SelectedCells.Count > 0)
            {
                try
                {
                    // ����ҵ���߼��㹤����ʵ��
                    BLLFactory.BLLFactory bllFactory = new BLLFactory.BLLFactory();
                    // ���ù�����ʵ��������������Ա������ʵ��
                    IOperatorManager operatorManager = bllFactory.BuildOperatorManager();

                    // �������в���ԱȨ����Ϣ
                    foreach (Operator tmpOperator in _operatorCollection.Values)
                    {
                        // ���浱ǰ����Ա�������޸�
                        operatorManager.ModifyOperator(tmpOperator);
                    }
                    
                    // �����û�������ʾ
                    MessageBox.Show(
                        "Ȩ�����ñ���ɹ���",
                        "������ʾ",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
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
        }

        /// <summary>
        /// [ˢ��Ȩ��]��������ť����¼� 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnRefreshRightsList_Click(object sender, EventArgs e)
        {
            // ����Ƿǳ�������Ա
            if (_currentOperator.Id != 0)
            {
                // �����ݰ���ʾ��������ͼ
                _rmuManager.BindDataToDataGridView(_currentOperator.RightsCollection);
                // �����ݰ󶨼��ص�������ͼ
                _rmuManager.BindDataToTreeView(_currentOperator.RightsCollection);
            }
        }

        /// <summary>
        /// [ˢ���û�]��������ť����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnRefreshOperator_Click(object sender, EventArgs e)
        {
            // �����ݰ���ʾ��������ͼ
            _rmuManager.BindOperatorInfoToDataGridView(_operatorCollection);
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
        /// Ȩ���б�������ͼ��Ԫ�������༭ģʽ�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvRightsList_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            // �����Ȩ��״̬���ҽڵ㲻��ѡ��ȡ����ѡ�¼�
            if (dgvRightsList.Columns[e.ColumnIndex].Name == "RightsState" && !_rightsViewIsChecked)
                e.Cancel = true;
        }

        /// <summary>
        /// Ȩ���б�������ͼ��Ԫ��ֹͣ�༭ģʽ�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvRightsList_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            // �����������Ա��Ϣ
            if (dgvOperatorList.SelectedCells.Count > 0)
            {
                // ���浱ǰѡ���еĲ���Ա����
                string operatorName = dgvOperatorList.Rows[dgvOperatorList.SelectedCells[0].RowIndex].Cells["Name"].Value.ToString().Trim();
                // ָ����ǰ�༭�Ĳ���Ա����
                Operator editOperator = _operatorCollection[operatorName];

                // �ҵ���ǰ�༭��Ȩ����
                Rights currentEditRights = editOperator.RightsCollection[dgvRightsList.Rows[e.RowIndex].Cells["Name"].Value.ToString().Trim()];
                // ���浱ǰ�޸ĵ�Ȩ�ޱ����Ȩ��״̬��Ȩ�޼���
                currentEditRights.RightsCaption = Convert.ToString(dgvRightsList.Rows[e.RowIndex].Cells["RightsCaption"].Value);
                currentEditRights.RightsState = Convert.ToBoolean(dgvRightsList.Rows[e.RowIndex].Cells["RightsState"].Value);

                // �����ݰ󶨼��ص�������ͼ
                _rmuManager.BindDataToTreeView(editOperator.RightsCollection);
            }
        }

        /// <summary>
        /// ����Ա�б�������ͼ��Ԫ��ֹͣ�༭ģʽ�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvOperatorList_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            // �����������Ա��Ϣ
            if (dgvOperatorList.SelectedCells.Count > 0)
            {
                // �ҵ���ǰ�༭�Ĳ���Ա��
                Operator currentEidtOperator = _operatorCollection[dgvOperatorList.Rows[e.RowIndex].Cells["Name"].Value.ToString().Trim()];
                // ���浱ǰ�޸ĵĲ���Ա�����״̬������Ա����
                currentEidtOperator.Password = Convert.ToString(dgvOperatorList.Rows[e.RowIndex].Cells["Password"].Value);
                currentEidtOperator.State = Convert.ToBoolean(dgvOperatorList.Rows[e.RowIndex].Cells["State"].Value);
            }
        }

        /// <summary>
        /// Ȩ���б�������ͼ��ѡ���ݸ����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvRightsList_SelectionChanged(object sender, EventArgs e)
        {
            // ������ѡ�е�Ԫ�������ù���ѡ��ʱ��ִ�в���
            if (dgvRightsList.SelectedCells.Count > 0 && IsRelatingChooseCells)
            {
                // ��������ѡ�е�Ԫ��ѡ������ͬ��Ȩ�������
                for (int i = 0; i < dgvRightsList.SelectedCells.Count; i++)
                {
                    // �ҽ���ѡ��[�ڲ�����]��[����Ȩ��]��Ԫ��ʱ��ִ�в���
                    if (dgvRightsList.SelectedCells[i].OwningColumn.Name == "Name"
                        || dgvRightsList.SelectedCells[i].OwningColumn.Name == "ParentLevelRightsName")
                    {
                        // ͬʱѡ����ѡ�е�Ԫ��������ͬ������[�ڲ�����]��[����Ȩ��]��Ԫ��
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
        /// ����Ա�б�������ͼ��ѡ���ݸ����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvOperatorList_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvOperatorList.SelectedCells.Count > 0
                && dgvOperatorList.Rows[dgvOperatorList.SelectedCells[0].RowIndex] != _dgvOldSelectedRow)
            {
                // ���浱ǰѡ�е���
                _dgvOldSelectedRow = dgvOperatorList.Rows[dgvOperatorList.SelectedCells[0].RowIndex];
                // ���浱ǰѡ���еĲ���Ա����
                string operatorName = _dgvOldSelectedRow.Cells["Name"].Value.ToString().Trim();
                // ָ����ǰ�༭�Ĳ���Ա����
                Operator editOperator = _operatorCollection[operatorName];

                // �����ݰ���ʾ��������ͼ
                _rmuManager.BindDataToDataGridView(editOperator.RightsCollection);
                // �����ݰ󶨼��ص�������ͼ
                _rmuManager.BindDataToTreeView(editOperator.RightsCollection);
            }
        }

        /// <summary>
        /// [Ȩ����ͼ]�ڵ㸴ѡǰ�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvRightsView_BeforeCheck(object sender, TreeViewCancelEventArgs e)
        {
            // ����ڵ㲻��ѡ��ȡ����ѡ�¼�
            if (!_rightsViewIsChecked)
                e.Cancel = true;
        }

        /// <summary>
        /// Ȩ��������ͼ��ѡ��״̬�����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvRightsView_AfterCheck(object sender, TreeViewEventArgs e)
        {
            // ����Ѿ������ӽڵ����ѡ��
            if (_isCheckedChildTreeNode)
            {
                // ��������ͼ�ػ�
                tvRightsView.BeginUpdate();
                // �����ӽڵ����ѡ��
                _isCheckedChildTreeNode = false;
                // ѡ��/ȡ�����ڵ㼰���ӽڵ㹴ѡ״̬
                _rmuManager.CheckOrUnCheckTreeNode(e.Node);
                // �����ӽڵ����ѡ��
                _isCheckedChildTreeNode = true;
                // ��������ͼ�ػ�
                tvRightsView.EndUpdate();

                if (dgvOperatorList.SelectedCells.Count > 0)
                {
                    // ���浱ǰѡ���еĲ���Ա����
                    string operatorName = dgvOperatorList.Rows[dgvOperatorList.SelectedCells[0].RowIndex].Cells["Name"].Value.ToString().Trim();
                    // �����ݰ���ʾ��������ͼ
                    _rmuManager.BindDataToDataGridView(_operatorCollection[operatorName].RightsCollection);
                }
            }
        }

        /// <summary>
        /// Ȩ��������ͼѡ�����ݸ����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvRightsView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            // ���ù���ѡ��
            _isRelatingChooseCells = false;
            // ������е�Ԫ��ѡ��
            dgvRightsList.ClearSelection();
            // ���ù���ѡ��
            _isRelatingChooseCells = true;

            // ͬ��������ͼ��Ӧ��ѡ��
            foreach (DataGridViewRow dgvRow in this.dgvRightsList.Rows)
            {
                if (dgvRow.Cells["Name"].Value.ToString().Trim() == e.Node.Tag.ToString().Trim())
                    dgvRow.Cells["Name"].Selected = true;
            }
        }

        /// <summary>
        /// �û��༭Ȩ��������ͼ�ڵ��ı����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvRightsView_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            // ���浱ǰѡ���еĲ���Ա����
            string operatorName = dgvOperatorList.Rows[dgvOperatorList.SelectedCells[0].RowIndex].Cells["Name"].Value.ToString().Trim();
            // ͬ���޸�Ȩ�޼���
            _operatorCollection[operatorName].RightsCollection[e.Node.Tag.ToString()].RightsCaption = e.Label;
            // �����ݰ���ʾ��������ͼ
            _rmuManager.BindDataToDataGridView(_operatorCollection[operatorName].RightsCollection);
        }

        /// <summary>
        /// ����ָ���ر�ԭ������ѹر��¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmRightsManager_FormClosed(object sender, FormClosedEventArgs e)
        {
            // ���¼���Ȩ�޲˵�
            _rmdManager.LoadMenuRightsItem(_msMain, _currentOperator.RightsCollection);
        }

        /// <summary>
        /// [����û�]��������ť����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnAddOperator_Click(object sender, EventArgs e)
        {
            frmOperatorManager frmAddOperator = new frmOperatorManager(_currentOperator, _msMain, false);
            frmAddOperator.ShowInTaskbar = false;
            frmAddOperator.ShowDialog();

            // ���¼��ز���Ա�б�
            _rmuManager.LoadOperatorList();

            // ѡ����ĩ�ʹ��������ֿɼ�
            if (dgvOperatorList.Rows.Count > 0)
            {
                dgvOperatorList.Rows[dgvOperatorList.Rows.Count - 1].Selected = true;
                dgvOperatorList.FirstDisplayedScrollingRowIndex = dgvOperatorList.Rows.Count - 1;
            }
        }

        /// <summary>
        /// [ɾ���û�]��������ť����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnDeleteOperator_Click(object sender, EventArgs e)
        {
            // �����ѡ�е�Ԫ��
            if (dgvOperatorList.SelectedCells.Count > 0)
            {
                try
                {
                    // ��ȡ��ǰҪɾ���Ĳ���Ա ID
                    int operatorId = Convert.ToInt32(dgvOperatorList.SelectedCells[0].OwningRow.Cells["Id"].Value);
                    // ��ȡ��ǰҪɾ���Ĳ���Ա����
                    string operatorName = Convert.ToString(dgvOperatorList.SelectedCells[0].OwningRow.Cells["Name"].Value);

                    DialogResult result;
                    result = MessageBox.Show(
                        string.Format("ȷʵҪɾ������Ա [{0}] ������(����Ȩ�޹�ϵ)��Ϣ��", operatorName),
                        "ɾ����ʾ",
                        MessageBoxButtons.OKCancel,
                        MessageBoxIcon.Question,
                        MessageBoxDefaultButton.Button2);

                    if (result == DialogResult.Cancel)
                        return;

                    // ����������
                    BLLFactory.BLLFactory bllFactory = new BLLFactory.BLLFactory();

                    // ɾ��Ȩ�޹�ϵ��Ϣ
                    IRightsRelationManager rightsRelationManager = bllFactory.BuildRightsRelationManager();
                    if (rightsRelationManager.GetRightsRelationByOperatorId(operatorId).Count > 0)
                    {
                        if (!rightsRelationManager.DeleteRightsRelationByOperatorId(operatorId))
                        {
                            MessageBox.Show(
                            string.Format("δ��ɾ������Ա [{0}] ��Ȩ�޹�ϵ��Ϣ��", operatorName),
                            "ɾ��ʧ��",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                            return;
                        }
                    }

                    // ɾ������Ա��Ϣ
                    IOperatorManager operatorManager = bllFactory.BuildOperatorManager();
                    if (!operatorManager.DeleteOperatorByID(operatorId))
                    {
                        MessageBox.Show(
                        string.Format("δ��ɾ������Ա [{0}] �Ļ�����Ϣ��", operatorName),
                        "ɾ��ʧ��",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                        return;
                    }

                    // ���¼��ز���Ա�б�
                    _rmuManager.LoadOperatorList();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        ex.Message,
                        "ɾ��ʧ��",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }

            // �������Ա��Ϣ����Ϊ��
            if (_operatorCollection.Count == 0)
            {
                // ���Ȩ���б�����Ա�б��Ȩ����ͼ
                dgvOperatorList.Rows.Clear();
                dgvRightsList.Rows.Clear();
                tvRightsView.Nodes.Clear();
            }
        }

        /// <summary>
        /// [�������]��������ť����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnRightsGroupManager_Click(object sender, EventArgs e)
        {
            frmRightsGroupManager frmRightsGroupManager = new frmRightsGroupManager(this, this.FrmMain);
            frmRightsGroupManager.ShowInTaskbar = false;
            frmRightsGroupManager.ShowDialog();

            // ���ز���Ա�б�
            _rmuManager.LoadOperatorList();
            // ˢ��������ʾ��������ͼ
            _rmuManager.BindOperatorInfoToDataGridView(_operatorCollection);
        }

        /// <summary>
        /// [Ȩ���б�]��Ԫ����֤�¼�
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
                        "Ȩ�ޱ��ⲻ����Ϊ���ַ�(�ո�)��";
                    MessageBox.Show(
                        dgvRightsList.Rows[e.RowIndex].ErrorText,
                        "�༭��ʾ",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    e.Cancel = true;
                }
            }
        }

        /// <summary>
        /// [����Ա�б�]��Ԫ����֤�¼���
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
                        "�û����볤�Ȳ���С����λ��";
                    MessageBox.Show(
                        dgvOperatorList.Rows[e.RowIndex].ErrorText,
                        "�༭��ʾ",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    e.Cancel = true;
                }
            }
        }

        /// <summary>
        /// [����ģʽ]/[�û�ģʽ]�����������˵������¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiRightsMode_Click(object sender, EventArgs e)
        {
            // ��ѡ����˵���
            (sender as ToolStripMenuItem).Checked = true;
            // �ύ�������Ե�ǰ��Ԫ��ı༭
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
        /// [Ȩ��ģʽ]��������ť����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnRightsMode_ButtonClick(object sender, EventArgs e)
        {
            // ���������˵�ѡ��
            tsbtnRightsMode.ShowDropDown();
        }

        #endregion
    }
}