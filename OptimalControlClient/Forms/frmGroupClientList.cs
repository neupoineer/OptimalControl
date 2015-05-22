using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MonitoringTool.Forms
{
    /// <summary>
    /// ���忪������
    /// </summary>
    public partial class frmGroupClientList : Form
    {
        //����һ�����������
        BLLFactory.BLLFactory _factory = null;
        // ����һ���ʵ�����ӿ�
        IBLL.ITabManager _tabManager;
        // �����ͻ���Ϣ����ӿ�
        IBLL.IClientManager _clientManager;
        // �ͻ�����
        List<Model.Client> _clientList = null;
        //����һ���������͹���ӿ�
        IBLL.IRoomTypeManager _roomTypeManager;
        //����һ���������ӿ�
        IBLL.IRoomManager _roomManager;
        IBLL.IRoomStateManager _roomStateManager;
        //����һ��List<RoomType>
        List<Model.RoomType> _roomTypeList = null;
        //����һ��List<Room>
        List<Model.Room> _roomList = null;
        // ������
        string _tallyMan = null;
        bool _isExit = false;
        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="tallyMan">������</param>
        public frmGroupClientList(string tallyMan)
        {
            InitializeComponent();

            txtRabatePrice.SetTextChangedEventHandler(true);
            txtConsumedUnitPrice.SetTextChangedEventHandler(true);
            txtRebateScale.SetTextChangedEventHandler(true);
            txtSaveMoney.SetTextChangedEventHandler(true);
            txtContactPhone.SetTextChangedEventHandler(true);

            _tallyMan = tallyMan;
        }
        #region ��������¼� 
        /// <summary>
        /// ��������¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmGroupClientList_Load(object sender, EventArgs e)
        {
            _roomList = new List<CodingMouse.CMHotelManager.Model.Room>();
            _roomTypeList = new List<CodingMouse.CMHotelManager.Model.RoomType>();
            // ���÷���
            ShowRoomTypeInfo();
            addCboIDCardNum();

            cboIDCardNum.SelectedIndex = 0;
            txtOperatorName.Text = _tallyMan;
        } 
        #endregion

        #region ��ʾ�ɹ�������Ϣ
        /// <summary>
        /// ��ʾ�ɹ�������Ϣ
        /// </summary>
        private void ShowRoomTypeInfo()
        {
            try
            {
                //����������
                _factory = new BLLFactory.BLLFactory();
                //���ù��������ɵķ���
                _roomTypeManager = _factory.BuildRoomTypeManager();                
                //����ʵ������
                _roomTypeList = _roomTypeManager.GetAllRoomType();

                //���� RoomType ����
                foreach (Model.RoomType roomType in _roomTypeList)
                {

                    string _nodeText = string.Format("{0}    �� {1}", roomType.ModelName, roomType.AdvanceUnitPrice);
                    //���� TreeView �ڵ�
                    TreeNode _treeNodes = new TreeNode(_nodeText);
                    //��ӵ� tvInRoomInfo
                    this.tvUsableRoom.Nodes.Add(_treeNodes);

                    //���ù��������ɵķ���
                    _roomManager = _factory.BuildRoomManager();
                    //����ʵ������
                    _roomList = _roomManager.GetAllRoom();
                    //���� Room ����
                    foreach (Model.Room tmpRoom in _roomList)
                    {
                        //��� RoomType ���� ModelName
                        if (tmpRoom.RoomType == roomType.ModelName)
                        {
                            if (tmpRoom.RoomState == Model.Room.RoomStateEnum.�ɹ�)
                            {
                                //��ӵ� TreeNodes �ڵ�
                                _treeNodes.Nodes.Add(tmpRoom.ModelName);

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(
                     ex.Message,
                     "��ȡʧ��",
                     MessageBoxButtons.OK,
                     MessageBoxIcon.Error);
            }
        } 
        #endregion

        #region ˫���ڵ�(TreeView) 
        /// <summary>
        ///˫���ڵ��¼�
        /// �ѷ���Ŵӽڵ����Ƴ����������Ӧ����Ϣ�����ListView��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvInRoomInfo_NodeMouseDoubleClick_1(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                //�������伯��
                foreach (Model.Room room in _roomList)
                {
                    //���ʵ������Ƶ���ѡ�нڵ�
                    if (room.ModelName == e.Node.Text && e.Node.Level != 0)
                    {
                        //��ʾ���ı�����
                        this.txtRoomNumbern.Text = room.ModelName;
                        //���� �������ͼ��� 
                        foreach (Model.RoomType roomType in _roomTypeList)
                        {
                            //����������͵����Ƶ��ڷ�������
                            if (roomType.ModelName == room.RoomType)
                            {
                                // ����������
                                _factory = new CodingMouse.CMHotelManager.BLLFactory.BLLFactory();
                                // ������������
                                _roomStateManager = _factory.BuildRoomStateManager();

                                // ��ȡ��̬
                                Dictionary<string, Model.RoomState> roomStateList = _roomStateManager.GetAllRoomStateInfo();
                                // ���۱���
                                double rebateScale = roomStateList[room.ModelName].RebateScale;
                                // �ۺ��
                                decimal rebateMoney = roomStateList[room.ModelName].ReceivableMoney;
                                //����һ��listView��
                                ListViewItem item = new ListViewItem(room.RoomType);
                                //��ǰ�����������
                                item.SubItems.AddRange(new string[] { room.ModelName, roomType.AdvanceUnitPrice.ToString(), rebateScale.ToString(), rebateMoney.ToString() });
                                //��listView�����һ������
                                this.lvRoomInfo.Items.Add(item);
                                if (e.Node.Parent is TreeNode)
                                    e.Node.Parent.Nodes.Remove(e.Node);
                                break;
                            }
                        }
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
        #endregion

        #region ���˫��(ListView)
        /// <summary>
        /// ���˫��ListView�¼�
        /// ��LisstsView ���Ƴ�������Ϣ�����ѷ������ӵ���TvInRoomInfo�����Ӧ�ķ���������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lvRoomInfo_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (lvRoomInfo.SelectedItems.Count > 0)
                {
                    //��ӵ� tvRoomInfo ��Ϣ��
                    string roomType = lvRoomInfo.SelectedItems[0].SubItems[0].Text;
                    string roomNumber = lvRoomInfo.SelectedItems[0].SubItems[1].Text;
                    string AdvanceUnitPrice = lvRoomInfo.SelectedItems[0].SubItems[2].Text;
                    string TabRebateScale = lvRoomInfo.SelectedItems[0].SubItems[3].Text;
                    string RebatedUnitPrice = lvRoomInfo.SelectedItems[0].SubItems[4].Text;
                    // �������伯��
                    foreach (Model.Room room in _roomList)
                    {
                        if (room.ModelName == roomNumber && room.RoomType == roomType)
                        {
                            // ����treeView�ڵ㼯��
                            foreach (TreeNode treeNode in tvUsableRoom.Nodes)
                            {
                                string[] _str = treeNode.Text.Split(' ');

                                if (_str[0].Trim() == room.RoomType)
                                {
                                    //��ӵ��ڵ�
                                    treeNode.Nodes.Add(room.ModelName);
                                    //��ʾ�����ͷ����ı�����
                                    this.txtRoomNumber.Text = room.ModelName;
                                    break;
                                }
                            }
                            break;
                        }

                    }
                    //��ListView���Ƴ�
                    lvRoomInfo.Items.Remove(lvRoomInfo.SelectedItems[0]);
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
        #endregion

        #region ����(Enter)
        /// <summary>
        /// ���̻�ȡ���Ӧ����Ϣ���������ListView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtRoomNumbern_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!string.IsNullOrEmpty(txtRoomNumbern.Text.Trim()))
                {
                    //�����ڵ��еķ�������
                    foreach (TreeNode roomType in tvUsableRoom.Nodes)
                    {
                        //�������ڵ��µ��ӽڵ�ķ����
                        foreach (TreeNode room in roomType.Nodes)
                        {
                            //�ӽڵ�ķ���ŵ����ı�������ķ����
                            if (room.Text == txtRoomNumbern.Text.Trim())
                            {
                                roomType.ExpandAll();
                                roomType.ForeColor = Color.Red;

                                //�������伯��
                                foreach (Model.Room modelRoom in _roomList)
                                {
                                    //����������͵����Ƶ��ڷ�������
                                    if (modelRoom.ModelName == room.Text)
                                    {
                                        //����������
                                        _factory = new CodingMouse.CMHotelManager.BLLFactory.BLLFactory();
                                        // ������������
                                        _roomStateManager = _factory.BuildRoomStateManager();

                                        // ��ȡ��̬
                                        Dictionary<string, Model.RoomState> roomStateList = _roomStateManager.GetAllRoomStateInfo();
                                        // ���۱���
                                        double rebateScale = roomStateList[modelRoom.ModelName].RebateScale;
                                        // �ۺ��
                                        decimal rebateMoney = Convert.ToDecimal(Convert.ToSingle(roomStateList[modelRoom.ModelName].AdvanceUnitPrice) * rebateScale);
                                        //����һ��listView��
                                        ListViewItem item = new ListViewItem(modelRoom.RoomType);
                                        //��ǰ�����������
                                        item.SubItems.AddRange(new string[] { modelRoom.ModelName, roomStateList[modelRoom.ModelName].AdvanceUnitPrice.ToString(), rebateScale.ToString(), rebateMoney.ToString() });
                                        //��listView�����һ������
                                        this.lvRoomInfo.Items.Add(item);
                                        //����ڵ�ķ���ŵ����ı�������ķ���ţ��򽫷���Ŵӽڵ���ɾ��
                                        if (room.Text == txtRoomNumbern.Text.Trim())
                                        {
                                            //�Ƴ�
                                            room.Remove();
                                            //����ı����ֵ
                                            this.txtRoomNumbern.Clear();
                                        }
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        } 
        #endregion

        #region ȡ���¼�
        /// <summary>
        /// ȡ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            //�رյ�ǰ����
            this.Close();
        } 
        #endregion
        
        /// <summary>
        /// ȷ���¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            #region ���忪����ֵУ�� 

            if (string.IsNullOrEmpty(this.txtClientName.Text.Trim()))
            {
                MessageBox.Show("���������������",
                                "������ʾ",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                //��ȡ����
                this.txtClientName.Focus();
                return;
            } //����û�δѡ��˿����ͣ���ʾ�û�ѡ��
            else if (string.IsNullOrEmpty(cboClientType.Text.Trim()))
            {
                MessageBox.Show(
                    "��ѡ��˿����ͣ�",
                    "������ʾ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                cboClientType.DroppedDown = true;
                return ;
            }//����û�δ����˿���������ʾ����
            else if (string.IsNullOrEmpty(txtClientName.Text.Trim()))
            {
                MessageBox.Show(
                    "������˿�������",
                    "������ʾ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                txtClientName.Focus();
                return ; ;
            }//����û�δ����˿͵����֤�ţ���ʾ����
            else if (string.IsNullOrEmpty(cboIDCardNum.Text.Trim()))
            {
                MessageBox.Show(
                    "�������ѡ��˿͵����֤�ţ�",
                    "������ʾ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                cboIDCardNum.DroppedDown = true;
                return ;
            }//����û�δѡ��˿��Ա���ʾ�û�ѡ��
            else if (string.IsNullOrEmpty(cboClientSex.Text.Trim()))
            {
                MessageBox.Show(
                    "��ѡ��˿͵��Ա�",
                    "������ʾ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                cboClientSex.DroppedDown = true;
                return ;
            }//����û�δѡ��˿͵���ϵ�绰����ʾ�û�ѡ��
            else if (string.IsNullOrEmpty(txtContactPhone.Text.Trim()))
            {
                MessageBox.Show(
                    "������˿͵���ϵ�绰��",
                    "������ʾ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                txtContactPhone.Focus();
                return ;
            }//����û�δѡ��˿͵�״̬����ʾ�û�ѡ��
            else if (string.IsNullOrEmpty(cboCurrentState.Text.Trim()))
            {
                MessageBox.Show(
                    "��ѡ��˿͵ĵ�ǰ״̬��",
                    "������ʾ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                cboCurrentState.DroppedDown = true;
                return ;
            }//����û�δ����˿͵���ϵ��ַ����ʾ����
            else if (string.IsNullOrEmpty(txtContactAddress.Text.Trim()))
            {
                MessageBox.Show(
                    "������˿͵���ϵ��ַ��",
                    "������ʾ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                txtContactAddress.Focus();
                return ;
            }//����û�δ���뱸ע��Ϣ����ע��ϢĬ��Ϊ��
            else if (string.IsNullOrEmpty(txtRemark.Text))
            {
                txtRemark.Text = "";
            }
            #endregion

            GroupClient();
        }

        /// <summary>
        /// ���忪��
        /// </summary>
        /// <returns></returns>
        private bool GroupClient()
        {
            bool isOk = false;
            try
            {
                // ��������
                _factory = new CodingMouse.CMHotelManager.BLLFactory.BLLFactory();
                // ������������
                _clientManager = _factory.BuildClientManager();
                _tabManager = _factory.BuildTabManager();
                _roomManager = _factory.BuildRoomManager();

                // �˵�����
                List<Model.Tab> _tabList  = new List<CodingMouse.CMHotelManager.Model.Tab>();
                if (_isExit)
                {
                    foreach (Model.Client cli in _clientList)
                    {
                        if (cli.IdCardNo == cboIDCardNum.Text.Trim())
                        {
                            foreach (ListViewItem list in lvRoomInfo.Items)
                            {

                                Model.Tab newTab = new CodingMouse.CMHotelManager.Model.Tab();

                                newTab.ClientId = cli.Id;
                                newTab.ClientName = cli.ModelName;
                                newTab.ConsumableNumber = 1;
                                newTab.ConsumedProject = "�����";
                                newTab.ConsumedTime = DateTime.Now;
                                newTab.IsSettleAccounts = false;
                                newTab.ModelName = list.SubItems[1].Text;
                                newTab.OperatorName = _tallyMan;
                                newTab.RebateScale = Convert.ToDouble(list.SubItems[3].Text);
                                newTab.Remark = "";
                                newTab.UnitPrice = Convert.ToDecimal(list.SubItems[2].Text);

                                _tabList.Add(newTab);

                            }
                            if (_tabManager.AddTab(_tabList))
                            {
                                isOk = true;

                                List<Model.Room> romList = _roomManager.GetAllRoom();
                                foreach (ListViewItem list in lvRoomInfo.Items)
                                {
                                    foreach (Model.Room room in romList)
                                    {
                                        if (list.SubItems[1].Text == room.ModelName)
                                        {
                                            room.RoomState = CodingMouse.CMHotelManager.Model.Room.RoomStateEnum.ռ��;
                                        }
                                    }
                                }
                                _roomManager.ModifyRoom(romList);
                                throw new Exception("����˵���Ϣ�ɹ�");
                            }
                            else
                            {
                                throw new Exception("����˵���Ϣʧ��");
                            }
                        }
                    }
                }
                else
                {
                    //��������ʵ�������
                    Model.Client _client = new Model.Client();
                    //��������
                    _client.ModelName = this.txtClientName.Text;
                    //�����Ա�
                    _client.Sex = char.Parse(this.cboClientSex.Text);
                    //��������
                    _client.ClientType = (Model.Client.ClientTypeEnum)Enum.Parse(typeof(Model.Client.ClientTypeEnum), this.cboClientType.Text);
                    //���ͳ�������
                    _client.Birthday = this.dtpBirthday.Value;
                    //���͵�ַ
                    _client.ContactAddress = this.txtContactAddress.Text;
                    //���͵绰
                    _client.ContactPhone = this.txtContactPhone.Text;
                    //����״̬
                    _client.CurrentState = (Model.Client.ClientStateEnum)Enum.Parse(typeof(Model.Client.ClientStateEnum), this.cboCurrentState.Text);
                    //������
                    _client.SaveMoney = decimal.Parse(this.txtSaveMoney.Text);
                    //���ͱ�ע
                    _client.Remark = this.txtRemark.Text;
                    //�������֤
                    _client.IdCardNo = this.cboIDCardNum.Text;

                    // �ͻ���ӳɹ� ������˵�
                    if (_clientManager.AddClient(_client))
                    {
                        foreach (ListViewItem list in lvRoomInfo.Items)
                        {
                            addCboIDCardNum();
                            cboIDCardNum.SelectedText = string.Format("{0}", _client.IdCardNo);

                            foreach (Model.Client client in _clientList)
                            {
                                if (client.IdCardNo == _client.IdCardNo)
                                {
                                    Model.Tab newTab = new CodingMouse.CMHotelManager.Model.Tab();

                                    newTab.ClientId = client.Id;
                                    newTab.ClientName = client.ModelName;
                                    newTab.ConsumableNumber = 1;
                                    newTab.ConsumedProject = "�����";
                                    newTab.ConsumedTime = DateTime.Now;
                                    newTab.IsSettleAccounts = false;
                                    newTab.ModelName = list.SubItems[1].Text;
                                    newTab.OperatorName = _tallyMan;
                                    newTab.RebateScale = Convert.ToDouble(list.SubItems[3].Text);
                                    newTab.UnitPrice = Convert.ToDecimal(list.SubItems[2].Text);

                                    _tabList.Add(newTab);
                                }
                            }
                        }
                        if (_tabManager.AddTab(_tabList))
                        {
                            isOk = true;

                            List<Model.Room> romList = _roomManager.GetAllRoom();
                            foreach (ListViewItem list in lvRoomInfo.Items)
                            {
                                foreach (Model.Room room in romList)
                                {
                                    if (list.SubItems[1].Text == room.ModelName)
                                    {
                                        room.RoomState = CodingMouse.CMHotelManager.Model.Room.RoomStateEnum.ռ��;
                                    }
                                }
                            }
                            _roomManager.ModifyRoom(romList);
                            throw new Exception("����˵���Ϣ�ɹ�");
                        }
                        else
                        {
                            throw new Exception("����˵���Ϣʧ��");
                        }
                    }
                    else
                    {
                        throw new Exception("�ÿͻ���Ϣ��ȷ�������ṩ���䣡");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                    "�����ʾ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            return isOk;
        }

        /// <summary>
        /// ���cboIDCardNum��
        /// </summary>
        private void addCboIDCardNum()
        {
            cboIDCardNum.Items.Clear();
            // ��������
            _factory = new CodingMouse.CMHotelManager.BLLFactory.BLLFactory();
            // ������������
            _clientManager = _factory.BuildClientManager();

            _clientList = _clientManager.GetAllClient();

            BindingSource source = new BindingSource();
            source.DataSource = _clientList;
            cboIDCardNum.DataSource = source;
            cboIDCardNum.DisplayMember = "IdCardNo";
            cboIDCardNum.ValueMember = "Id";
        }

        /// <summary>
        /// cboIDCardNum��ı�ʱ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboIDCardNum_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cboIDCardNum.Text.Length <= 14)
            {
                this.cboIDCardNum.DroppedDown = true;
            }

            //�������ͼ���
            foreach (Model.Client tmpClient in _clientList)
            {
                // �ж��û���������֤�����Ƿ����
                if (tmpClient.IdCardNo == this.cboIDCardNum.Text.Trim())
                {
                    //��������
                    this.txtClientName.Text = tmpClient.ModelName;
                    //��������
                    this.cboClientType.Text = tmpClient.ClientType.ToString();
                    //�����Ա�
                    this.cboClientSex.Text = tmpClient.Sex.ToString();
                    //����״̬
                    this.cboCurrentState.Text = tmpClient.CurrentState.ToString();
                    //���͵�ַ
                    this.txtContactAddress.Text = tmpClient.ContactAddress;
                    //���͵绰
                    this.txtContactPhone.Text = tmpClient.ContactPhone;
                    //���ͳ�������
                    this.dtpBirthday.Text = tmpClient.Birthday.ToString();
                    //������
                    this.txtSaveMoney.Text = tmpClient.SaveMoney.ToString();
                    //���ͱ�ע
                    this.txtRemark.Text = tmpClient.Remark;

                    _isExit = true;
                }
            }
        }

        /// <summary>
        /// ������ϸ��Ϣ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lvRoomInfo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(lvRoomInfo.Items.Count > 0)
            {
                txtRoomNumber.Text = lvRoomInfo.Items[0].SubItems[1].Text;
                if (lvRoomInfo.SelectedItems.Count > 0)
                {
                    txtConsumedUnitPrice.Text = lvRoomInfo.SelectedItems[0].SubItems[2].Text;
                    txtRebateScale.Text = lvRoomInfo.SelectedItems[0].SubItems[3].Text;
                    float rabatePrice = Convert.ToSingle(txtConsumedUnitPrice.Text) * (1 - Convert.ToSingle(txtRebateScale.Text));

                    txtRabatePrice.Text = rabatePrice.ToString();
                }
                else if (lvRoomInfo.SelectedItems.Count == 0)
                {
                    txtConsumedUnitPrice.Text = lvRoomInfo.Items[0].SubItems[2].Text;
                    txtRebateScale.Text = lvRoomInfo.Items[0].SubItems[3].Text;
                    float rabatePrice = Convert.ToSingle(txtConsumedUnitPrice.Text) * (1 - Convert.ToSingle(txtRebateScale.Text));

                    txtRabatePrice.Text = rabatePrice.ToString();
                }
            }
        }
    }
}