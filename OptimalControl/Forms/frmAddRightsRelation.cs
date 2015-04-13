using System;
using System.Collections.Generic;
using System.Windows.Forms;
using IBLL;
using Model;

namespace OptimalControl.Forms
{
    /// <summary>
    /// ���Ȩ�޹�ϵ����
    /// </summary>
    public partial class frmAddRightsRelation : Form
    {
        #region Private Members
        /// <summary>
        /// ����Ȩ����������
        /// </summary>
        frmRightsGroupManager _frmRightsGroupManager = null; 
        #endregion

        #region Public Methods
        /// <summary>
        /// �޲ι���
        /// </summary>
        public frmAddRightsRelation(frmRightsGroupManager frmRightsGroupManager)
        {
            // ����������ؼ�
            InitializeComponent();

            // ���洫���Ȩ����������
            _frmRightsGroupManager = frmRightsGroupManager;
        } 
        #endregion

        #region Event Handlers
        /// <summary>
        /// ��������¼���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmAddRightsRelation_Load(object sender, EventArgs e)
        {
            // ����������ʵ��
            BLLFactory.BLLFactory bllFactory = new BLLFactory.BLLFactory();

            try
            {
                // ��������Ա������ʵ��
                IOperatorManager operatorManager = bllFactory.BuildOperatorManager();
                // ����ʵ��������
                Dictionary<string, Operator> operatorCollection = operatorManager.GetAllOperatorInfo();

                // �����������Ա��Ϣ
                if (operatorCollection.Count > 0)
                {
                    // ��������ʾ
                    BindingSource operatorSource = new BindingSource();
                    operatorSource.DataSource = operatorCollection.Values;
                    cboOperatorName.DataSource = operatorSource;
                    cboOperatorName.DisplayMember = "Name";
                    cboOperatorName.ValueMember = "Id";
                }
                else
                    MessageBox.Show(
                        "δ�ҵ��κβ���Ա��Ϣ������ [Ȩ�޹���] ������µĲ���Ա��",
                        "���ؾ���",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                // ����Ȩ�޹�����ʵ��
                IRightsGroupManager rightsGroupManager = bllFactory.BuildRightsGroupManager();
                // ����ʵ������
                Dictionary<string, RightsGroup> rightsGroupCollection = rightsGroupManager.GetAllRightsGroupInfo();

                // �������Ȩ������Ϣ
                if (rightsGroupCollection.Count > 0)
                {
                    // ��������ʾ
                    BindingSource rightsGroupSource = new BindingSource();
                    rightsGroupSource.DataSource = rightsGroupCollection.Values;
                    cboGroupName.DataSource = rightsGroupSource;
                    cboGroupName.DisplayMember = "Name";
                    cboGroupName.ValueMember = "Id";
                }
                else
                    MessageBox.Show(
                    "δ�ҵ��κβ���Ա��Ϣ������ [Ȩ�������] ������µ�Ȩ���飡",
                    "���ؾ���",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
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
        /// [���]��ť����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cboOperatorName.Text) ||
                string.IsNullOrEmpty(cboGroupName.Text))
            {
                MessageBox.Show(
                    "[����Ա] �� [Ȩ����] ��Ϊ�������",
                    "�����ʾ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                cboOperatorName.Focus();

                return;
            }

            // ����������ʵ��
            BLLFactory.BLLFactory bllFactory = new BLLFactory.BLLFactory();

            try
            {
                // ����Ȩ�޹�ϵ������ʵ��
                IRightsRelationManager rightsRelationManager = bllFactory.BuildRightsRelationManager();
                // ����ʵ������
                List<RightsRelation> rightsRelationList =
                    rightsRelationManager.GetRightsRelationByOperatorId(
                    Convert.ToInt32(cboOperatorName.SelectedValue));

                // У�鵱ǰѡ��Ĺ�ϵ�Ƿ��Ѵ���
                foreach (RightsRelation tmpRightsRelation in rightsRelationList)
                {
                    if (tmpRightsRelation.RightsGroupId ==
                        Convert.ToInt32(cboGroupName.SelectedValue))
                    {
                        MessageBox.Show(
                            string.Format("�û� [{0}] �Ѿ�������Ȩ���� [{1}]��",
                            cboOperatorName.Text,
                            cboGroupName.Text),
                            "��Ӿ���",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        return;
                    }
                }

                // ִ����Ӳ���
                RightsRelation rightsRelation = new RightsRelation();
                rightsRelation.OperatorId = Convert.ToInt32(cboOperatorName.SelectedValue);
                rightsRelation.RightsGroupId = Convert.ToInt32(cboGroupName.SelectedValue);
                if (!rightsRelationManager.AddRightsRelation(rightsRelation))
                {
                    MessageBox.Show(
                        "���Ȩ�޹�ϵ��Ϣʧ�ܣ�",
                        "���ʧ��",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show(
                        "���Ȩ�޹�ϵ��Ϣ�ɹ���",
                        "��ӳɹ�",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    // ���Ȩ����������δ���ͷ�
                    if (!_frmRightsGroupManager.IsDisposed)
                    {
                        // ����Ȩ���������������ˢ��
                        _frmRightsGroupManager.RefreshDataDisplay();
                        // ѡ����ĩ����ֿɼ�
                        _frmRightsGroupManager.SelectLastRelation();
                    }
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
        }

        /// <summary>
        /// [ȡ��]��ť����¼�
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