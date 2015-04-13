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
    /// ������û�����
    /// </summary>
    public partial class frmOperatorManager : Form
    {
        #region Private Members
        /// <summary>
        /// ��ǰ��¼����Աʵ��
        /// </summary>
        Operator _currentOperator = null;
        /// <summary>
        /// �����汻����Ĳ˵�����
        /// </summary>
        MenuStrip _msMain = null;
        /// <summary>
        /// �Ƿ���ʾΪ�޸��������
        /// </summary>
        bool _isModify = false;
        #endregion

        #region Private Methods
        /// <summary>
        /// �û�����У��
        /// </summary>
        /// <returns>True:ͨ�� / False:δͨ��</returns>
        private bool UserInputCheck()
        {
            string operatorName = txtOperatorName.Text.Trim();
            string operatorPwd = txtOperatorPwd.Text.Trim();
            string validatePwd = txtValidatePwd.Text.Trim();

            if (string.IsNullOrEmpty(operatorName))
            {
                this.toolTip.ToolTipIcon = ToolTipIcon.Info;
                this.toolTip.ToolTipTitle = !_isModify ? "�����ʾ" : "�޸���ʾ";
                Point showLocation = new Point(
                    this.txtOperatorName.Location.X + this.txtOperatorName.Width,
                    this.txtOperatorName.Location.Y);
                this.toolTip.Show(!_isModify ? "�������¼���ƣ�" : "������ԭʼ���룡", this, showLocation, 5000);
                this.txtOperatorName.Focus();
                return false;
            }

            // ���Ϊ�޸����������ԭʼ���벻��ȷ
            if (_isModify && operatorName != _currentOperator.Password.Trim())
            {
                this.toolTip.ToolTipIcon = ToolTipIcon.Warning;
                this.toolTip.ToolTipTitle = "�޸ľ���";
                Point showLocation = new Point(
                    this.txtOperatorName.Location.X + this.txtOperatorName.Width,
                    this.txtOperatorName.Location.Y);
                this.toolTip.Show("�����ԭʼ���벻��ȷ��", this, showLocation, 5000);
                this.txtOperatorName.Focus();
                return false;
            }

            if (operatorPwd.Length < 6)
            {
                this.toolTip.ToolTipIcon = ToolTipIcon.Warning;
                this.toolTip.ToolTipTitle = !_isModify ? "��Ӿ���" : "�޸ľ���";
                Point showLocation = new Point(
                    this.txtOperatorPwd.Location.X + this.txtOperatorPwd.Width,
                    this.txtOperatorPwd.Location.Y);
                this.toolTip.Show("�û����볤�Ȳ���С����λ��", this, showLocation, 5000);
                this.txtOperatorPwd.Focus();
                return false;
            }

            if (validatePwd.Length < 6)
            {
                this.toolTip.ToolTipIcon = ToolTipIcon.Warning;
                this.toolTip.ToolTipTitle = !_isModify ? "��Ӿ���" : "�޸ľ���";
                Point showLocation = new Point(
                    this.txtValidatePwd.Location.X + this.txtValidatePwd.Width,
                    this.txtValidatePwd.Location.Y);
                this.toolTip.Show("ȷ�����볤�Ȳ���С����λ��", this, showLocation, 5000);
                this.txtValidatePwd.Focus();
                return false;
            }

            if (operatorPwd != validatePwd)
            {
                this.toolTip.ToolTipIcon = ToolTipIcon.Warning;
                this.toolTip.ToolTipTitle = !_isModify ? "��Ӿ���" : "�޸ľ���";
                Point showLocation = new Point(
                    this.txtValidatePwd.Location.X + this.txtValidatePwd.Width,
                    this.txtValidatePwd.Location.Y);
                this.toolTip.Show("����������������һ�£�", this, showLocation, 5000);
                this.txtValidatePwd.Focus();
                return false;
            }

            return true;
        }

        /// <summary>
        /// У�����Ա�Ƿ��Ѵ���
        /// </summary>
        /// <returns>True:���� / False:������</returns>
        private bool CheckOperatorExist()
        {
            try
            {
                // ����������ʵ��
                BLLFactory.BLLFactory bllFactory = new BLLFactory.BLLFactory();
                // ����ҵ���߼�������ʵ��
                IOperatorManager operatorManager = bllFactory.BuildOperatorManager();

                // У�����Ա�Ƿ��Ѵ���
                if (operatorManager.CheckOperatorExist(this.txtOperatorName.Text.Trim()))
                {
                    this.toolTip.ToolTipIcon = ToolTipIcon.Warning;
                    this.toolTip.ToolTipTitle = "��Ӿ���";
                    Point showLocation = new Point(
                        this.txtOperatorName.Location.X + this.txtOperatorName.Width,
                        this.txtOperatorName.Location.Y);
                    this.toolTip.Show(
                        string.Format("�û� [{0}] �Ѿ����ڣ�������ָ����", 
                        this.txtOperatorName.Text.Trim()), 
                        this, showLocation, 5000);
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                        ex.Message,
                        "У��ʧ��",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
            }

            return true;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// �޲ι���
        /// </summary>
        /// <param name="currentOperator">��ǰ��¼����Աʵ��</param>
        /// <param name="msMain">����������˵�����</param>
        /// <param name="isModify">�Ƿ���ʾΪ�޸��������</param>
        public frmOperatorManager(Operator currentOperator, MenuStrip msMain, bool isModify)
        {
            // ����������ؼ�
            InitializeComponent();
            // ���浱ǰ��¼����Աʵ��
            _currentOperator = currentOperator;
            // ���洫��������汻����Ĳ˵�����
            _msMain = msMain;
            // ���������ʾҪ��
            _isModify = isModify;

            // ���Ҫ����ʾΪ�޸��������
            if (_isModify)
            {
                this.Text = string.Format("�޸Ĳ���Ա [{0}] ������", _currentOperator.Name);
                this.chkOperatorState.Visible = false;
                this.btnSubmit.Text = "����";

                this.lblOperatorName.Text = "ԭʼ����(&O):";
                this.txtOperatorName.PasswordChar = '*';
            }
        } 
        #endregion

        #region Event Handlers
        /// <summary>
        /// [���]/[����]��ť����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnASubmit_Click(object sender, EventArgs e)
        {
            // ����������ʵ��
            BLLFactory.BLLFactory bllFactory = new BLLFactory.BLLFactory();
            // ����ҵ���߼�������ʵ��
            IOperatorManager operatorManager = bllFactory.BuildOperatorManager();

            // �����Ҫ����ʾΪ�޸��������
            if (!_isModify)
            {
                // ���ͨ������У��
                if (UserInputCheck())
                {
                    try
                    {
                        // У�����Ա�Ƿ��Ѵ���
                        if (!CheckOperatorExist())
                            return;

                        // �����µĲ���Աʵ��
                        Operator addOperator = new Operator();
                        addOperator.Name = this.txtOperatorName.Text.Trim();
                        addOperator.Password = this.txtOperatorPwd.Text.Trim();
                        addOperator.RightsCollection = new Dictionary<string, Rights>();
                        // ����Ȩ�޲˵�������ʵ��
                        RightsMenuDataManager rmManager = new RightsMenuDataManager();
                        // ����Ȩ���б�ṹ
                        addOperator.RightsCollection = rmManager.ReadMenuRightsItem(_msMain, addOperator.RightsCollection);
                        addOperator.State = this.chkOperatorState.Checked;

                        // ִ�����
                        if (!operatorManager.AddOperator(addOperator))
                        {
                            this.toolTip.ToolTipIcon = ToolTipIcon.Error;
                            this.toolTip.ToolTipTitle = "���ʧ��";
                            Point showLocation = new Point(
                                this.txtOperatorName.Location.X + this.txtOperatorName.Width,
                                this.txtOperatorName.Location.Y);
                            this.toolTip.Show(string.Format("δ�ܽ��û� [{0}] ��ӵ���ϵͳ��", addOperator.Name), this, showLocation, 5000);
                        }
                        else
                        {
                            if (!addOperator.State)
                                MessageBox.Show(
                                    string.Format(
                                    "�û� [{0}] �Ѿ���ӵ�δ���\r\n\r\n�������ֶ�����Ϊ�丳��Ȩ�ޡ�",
                                    addOperator.Name),
                                    "��ӳɹ�",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                            else
                                MessageBox.Show(
                                    string.Format(
                                    "�û� [{0}] �Ѿ���Ӳ��Ѽ��\r\n\r\n��Ϊ�丳���ʵ���ϵͳȨ�ޡ�",
                                    addOperator.Name),
                                    "��ӳɹ�",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);

                            this.Close();
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
            }
            // ���Ҫ����ʾΪ�޸��������
            else
            {
                // ���ͨ������У��
                if (UserInputCheck())
                {
                    // ���������
                    string oldPwd = _currentOperator.Password;
                    // �޸ĵ�ǰ��¼����Աʵ���������Ϣ
                    _currentOperator.Password = this.txtOperatorPwd.Text.Trim();

                    try
                    {
                        // ִ���޸�
                        if (operatorManager.ModifyOperator(_currentOperator))
                        {
                            MessageBox.Show(
                                    string.Format(
                                    "�û� [{0}] �������޸ĳɹ���",
                                    _currentOperator.Name),
                                    "�޸ĳɹ�",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show(
                                    string.Format(
                                    "δ���޸��û� [{0}] �����룡",
                                    _currentOperator.Name),
                                    "�޸�ʧ��",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);

                            // �޸�ʧ����ԭ������
                            _currentOperator.Password = oldPwd;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(
                            ex.Message,
                            "�޸�ʧ��",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);

                        // �޸�ʧ����ԭ������
                        _currentOperator.Password = oldPwd;
                    }
                }
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