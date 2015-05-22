using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MonitoringTool.Forms
{
    /// <summary>
    /// 团体开单界面
    /// </summary>
    public partial class frmGroupClientList : Form
    {
        //声明一个工厂类对象
        BLLFactory.BLLFactory _factory = null;
        // 声明一个帐单管理接口
        IBLL.ITabManager _tabManager;
        // 声明客户信息管理接口
        IBLL.IClientManager _clientManager;
        // 客户集合
        List<Model.Client> _clientList = null;
        //声明一个房间类型管理接口
        IBLL.IRoomTypeManager _roomTypeManager;
        //声明一个房间管理接口
        IBLL.IRoomManager _roomManager;
        IBLL.IRoomStateManager _roomStateManager;
        //声明一个List<RoomType>
        List<Model.RoomType> _roomTypeList = null;
        //声明一个List<Room>
        List<Model.Room> _roomList = null;
        // 记账人
        string _tallyMan = null;
        bool _isExit = false;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="tallyMan">记账人</param>
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
        #region 窗体加载事件 
        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmGroupClientList_Load(object sender, EventArgs e)
        {
            _roomList = new List<CodingMouse.CMHotelManager.Model.Room>();
            _roomTypeList = new List<CodingMouse.CMHotelManager.Model.RoomType>();
            // 调用方法
            ShowRoomTypeInfo();
            addCboIDCardNum();

            cboIDCardNum.SelectedIndex = 0;
            txtOperatorName.Text = _tallyMan;
        } 
        #endregion

        #region 显示可供房间信息
        /// <summary>
        /// 显示可供房间信息
        /// </summary>
        private void ShowRoomTypeInfo()
        {
            try
            {
                //创建工厂类
                _factory = new BLLFactory.BLLFactory();
                //调用工厂类生成的方法
                _roomTypeManager = _factory.BuildRoomTypeManager();                
                //调用实例方法
                _roomTypeList = _roomTypeManager.GetAllRoomType();

                //遍历 RoomType 集合
                foreach (Model.RoomType roomType in _roomTypeList)
                {

                    string _nodeText = string.Format("{0}    ￥ {1}", roomType.ModelName, roomType.AdvanceUnitPrice);
                    //创建 TreeView 节点
                    TreeNode _treeNodes = new TreeNode(_nodeText);
                    //添加到 tvInRoomInfo
                    this.tvUsableRoom.Nodes.Add(_treeNodes);

                    //调用工厂类生成的方法
                    _roomManager = _factory.BuildRoomManager();
                    //调用实例方法
                    _roomList = _roomManager.GetAllRoom();
                    //遍历 Room 集合
                    foreach (Model.Room tmpRoom in _roomList)
                    {
                        //如果 RoomType 等于 ModelName
                        if (tmpRoom.RoomType == roomType.ModelName)
                        {
                            if (tmpRoom.RoomState == Model.Room.RoomStateEnum.可供)
                            {
                                //添加到 TreeNodes 节点
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
                     "读取失败",
                     MessageBoxButtons.OK,
                     MessageBoxIcon.Error);
            }
        } 
        #endregion

        #region 双击节点(TreeView) 
        /// <summary>
        ///双击节点事件
        /// 把房间号从节点中移出，并把相对应的信息填充在ListView中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvInRoomInfo_NodeMouseDoubleClick_1(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                //遍历房间集合
                foreach (Model.Room room in _roomList)
                {
                    //如果实体的名称等于选中节点
                    if (room.ModelName == e.Node.Text && e.Node.Level != 0)
                    {
                        //显示在文本框中
                        this.txtRoomNumbern.Text = room.ModelName;
                        //遍历 房间类型集合 
                        foreach (Model.RoomType roomType in _roomTypeList)
                        {
                            //如果房间类型的名称等于房间类型
                            if (roomType.ModelName == room.RoomType)
                            {
                                // 创建工厂类
                                _factory = new CodingMouse.CMHotelManager.BLLFactory.BLLFactory();
                                // 调用生产方法
                                _roomStateManager = _factory.BuildRoomStateManager();

                                // 提取房态
                                Dictionary<string, Model.RoomState> roomStateList = _roomStateManager.GetAllRoomStateInfo();
                                // 打折比例
                                double rebateScale = roomStateList[room.ModelName].RebateScale;
                                // 折后价
                                decimal rebateMoney = roomStateList[room.ModelName].ReceivableMoney;
                                //创建一个listView项
                                ListViewItem item = new ListViewItem(room.RoomType);
                                //向当前项中添加子项
                                item.SubItems.AddRange(new string[] { room.ModelName, roomType.AdvanceUnitPrice.ToString(), rebateScale.ToString(), rebateMoney.ToString() });
                                //向listView中添加一个新项
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
                      "填充失败",
                      MessageBoxButtons.OK,
                      MessageBoxIcon.Error);
            }
        } 
        #endregion

        #region 鼠标双击(ListView)
        /// <summary>
        /// 鼠标双击ListView事件
        /// 从LisstsView 中移出房间信息，并把房间号添加到（TvInRoomInfo）相对应的房间类型中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lvRoomInfo_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (lvRoomInfo.SelectedItems.Count > 0)
                {
                    //添加到 tvRoomInfo 信息中
                    string roomType = lvRoomInfo.SelectedItems[0].SubItems[0].Text;
                    string roomNumber = lvRoomInfo.SelectedItems[0].SubItems[1].Text;
                    string AdvanceUnitPrice = lvRoomInfo.SelectedItems[0].SubItems[2].Text;
                    string TabRebateScale = lvRoomInfo.SelectedItems[0].SubItems[3].Text;
                    string RebatedUnitPrice = lvRoomInfo.SelectedItems[0].SubItems[4].Text;
                    // 遍历房间集合
                    foreach (Model.Room room in _roomList)
                    {
                        if (room.ModelName == roomNumber && room.RoomType == roomType)
                        {
                            // 遍历treeView节点集合
                            foreach (TreeNode treeNode in tvUsableRoom.Nodes)
                            {
                                string[] _str = treeNode.Text.Split(' ');

                                if (_str[0].Trim() == room.RoomType)
                                {
                                    //添加到节点
                                    treeNode.Nodes.Add(room.ModelName);
                                    //显示在主客房间文本框中
                                    this.txtRoomNumber.Text = room.ModelName;
                                    break;
                                }
                            }
                            break;
                        }

                    }
                    //从ListView中移除
                    lvRoomInfo.Items.Remove(lvRoomInfo.SelectedItems[0]);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(
                    ex.Message,
                    "填充失败",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        } 
        #endregion

        #region 键盘(Enter)
        /// <summary>
        /// 键盘获取相对应的信息，并填充在ListView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtRoomNumbern_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!string.IsNullOrEmpty(txtRoomNumbern.Text.Trim()))
                {
                    //遍历节点中的房间类型
                    foreach (TreeNode roomType in tvUsableRoom.Nodes)
                    {
                        //遍历父节点下的子节点的房间号
                        foreach (TreeNode room in roomType.Nodes)
                        {
                            //子节点的房间号等于文本框输入的房间号
                            if (room.Text == txtRoomNumbern.Text.Trim())
                            {
                                roomType.ExpandAll();
                                roomType.ForeColor = Color.Red;

                                //遍历房间集合
                                foreach (Model.Room modelRoom in _roomList)
                                {
                                    //如果房间类型的名称等于房间类型
                                    if (modelRoom.ModelName == room.Text)
                                    {
                                        //创建工厂类
                                        _factory = new CodingMouse.CMHotelManager.BLLFactory.BLLFactory();
                                        // 调用生产方法
                                        _roomStateManager = _factory.BuildRoomStateManager();

                                        // 提取房态
                                        Dictionary<string, Model.RoomState> roomStateList = _roomStateManager.GetAllRoomStateInfo();
                                        // 打折比例
                                        double rebateScale = roomStateList[modelRoom.ModelName].RebateScale;
                                        // 折后价
                                        decimal rebateMoney = Convert.ToDecimal(Convert.ToSingle(roomStateList[modelRoom.ModelName].AdvanceUnitPrice) * rebateScale);
                                        //创建一个listView项
                                        ListViewItem item = new ListViewItem(modelRoom.RoomType);
                                        //向当前项中添加子项
                                        item.SubItems.AddRange(new string[] { modelRoom.ModelName, roomStateList[modelRoom.ModelName].AdvanceUnitPrice.ToString(), rebateScale.ToString(), rebateMoney.ToString() });
                                        //向listView中添加一个新项
                                        this.lvRoomInfo.Items.Add(item);
                                        //如果节点的房间号等于文本的输入的房间号，则将房间号从节点中删除
                                        if (room.Text == txtRoomNumbern.Text.Trim())
                                        {
                                            //移除
                                            room.Remove();
                                            //清除文本框的值
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

        #region 取消事件
        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            //关闭当前窗体
            this.Close();
        } 
        #endregion
        
        /// <summary>
        /// 确定事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            #region 团体开单空值校验 

            if (string.IsNullOrEmpty(this.txtClientName.Text.Trim()))
            {
                MessageBox.Show("请输入宾客姓名！",
                                "开单提示",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                //获取焦点
                this.txtClientName.Focus();
                return;
            } //如果用户未选择顾客类型，提示用户选择
            else if (string.IsNullOrEmpty(cboClientType.Text.Trim()))
            {
                MessageBox.Show(
                    "请选择顾客类型！",
                    "开单提示",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                cboClientType.DroppedDown = true;
                return ;
            }//如果用户未输入顾客姓名，提示输入
            else if (string.IsNullOrEmpty(txtClientName.Text.Trim()))
            {
                MessageBox.Show(
                    "请输入顾客姓名！",
                    "开单提示",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                txtClientName.Focus();
                return ; ;
            }//如果用户未输入顾客的身份证号，提示输入
            else if (string.IsNullOrEmpty(cboIDCardNum.Text.Trim()))
            {
                MessageBox.Show(
                    "请输入或选择顾客的身份证号！",
                    "开单提示",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                cboIDCardNum.DroppedDown = true;
                return ;
            }//如果用户未选择顾客性别，提示用户选择
            else if (string.IsNullOrEmpty(cboClientSex.Text.Trim()))
            {
                MessageBox.Show(
                    "请选择顾客的性别！",
                    "开单提示",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                cboClientSex.DroppedDown = true;
                return ;
            }//如果用户未选择顾客的联系电话，提示用户选择
            else if (string.IsNullOrEmpty(txtContactPhone.Text.Trim()))
            {
                MessageBox.Show(
                    "请输入顾客的联系电话！",
                    "开单提示",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                txtContactPhone.Focus();
                return ;
            }//如果用户未选择顾客的状态，提示用户选择
            else if (string.IsNullOrEmpty(cboCurrentState.Text.Trim()))
            {
                MessageBox.Show(
                    "请选择顾客的当前状态！",
                    "开单提示",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                cboCurrentState.DroppedDown = true;
                return ;
            }//如果用户未输入顾客的联系地址，提示输入
            else if (string.IsNullOrEmpty(txtContactAddress.Text.Trim()))
            {
                MessageBox.Show(
                    "请输入顾客的联系地址！",
                    "开单提示",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                txtContactAddress.Focus();
                return ;
            }//如果用户未输入备注信息，则备注信息默认为空
            else if (string.IsNullOrEmpty(txtRemark.Text))
            {
                txtRemark.Text = "";
            }
            #endregion

            GroupClient();
        }

        /// <summary>
        /// 团体开单
        /// </summary>
        /// <returns></returns>
        private bool GroupClient()
        {
            bool isOk = false;
            try
            {
                // 创建工厂
                _factory = new CodingMouse.CMHotelManager.BLLFactory.BLLFactory();
                // 调用生产方法
                _clientManager = _factory.BuildClientManager();
                _tabManager = _factory.BuildTabManager();
                _roomManager = _factory.BuildRoomManager();

                // 账单集合
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
                                newTab.ConsumedProject = "房间费";
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
                                            room.RoomState = CodingMouse.CMHotelManager.Model.Room.RoomStateEnum.占用;
                                        }
                                    }
                                }
                                _roomManager.ModifyRoom(romList);
                                throw new Exception("添加账单信息成功");
                            }
                            else
                            {
                                throw new Exception("添加账单信息失败");
                            }
                        }
                    }
                }
                else
                {
                    //创建宾客实体类对象
                    Model.Client _client = new Model.Client();
                    //宾客姓名
                    _client.ModelName = this.txtClientName.Text;
                    //宾客性别
                    _client.Sex = char.Parse(this.cboClientSex.Text);
                    //宾客类型
                    _client.ClientType = (Model.Client.ClientTypeEnum)Enum.Parse(typeof(Model.Client.ClientTypeEnum), this.cboClientType.Text);
                    //宾客出生日期
                    _client.Birthday = this.dtpBirthday.Value;
                    //宾客地址
                    _client.ContactAddress = this.txtContactAddress.Text;
                    //宾客电话
                    _client.ContactPhone = this.txtContactPhone.Text;
                    //宾客状态
                    _client.CurrentState = (Model.Client.ClientStateEnum)Enum.Parse(typeof(Model.Client.ClientStateEnum), this.cboCurrentState.Text);
                    //储存金额
                    _client.SaveMoney = decimal.Parse(this.txtSaveMoney.Text);
                    //宾客备注
                    _client.Remark = this.txtRemark.Text;
                    //宾客身份证
                    _client.IdCardNo = this.cboIDCardNum.Text;

                    // 客户添加成功 则添加账单
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
                                    newTab.ConsumedProject = "房间费";
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
                                        room.RoomState = CodingMouse.CMHotelManager.Model.Room.RoomStateEnum.占用;
                                    }
                                }
                            }
                            _roomManager.ModifyRoom(romList);
                            throw new Exception("添加账单信息成功");
                        }
                        else
                        {
                            throw new Exception("添加账单信息失败");
                        }
                    }
                    else
                    {
                        throw new Exception("该客户信息正确，不能提供房间！");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                    "添加提示",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            return isOk;
        }

        /// <summary>
        /// 添加cboIDCardNum项
        /// </summary>
        private void addCboIDCardNum()
        {
            cboIDCardNum.Items.Clear();
            // 创建工厂
            _factory = new CodingMouse.CMHotelManager.BLLFactory.BLLFactory();
            // 调用生产方法
            _clientManager = _factory.BuildClientManager();

            _clientList = _clientManager.GetAllClient();

            BindingSource source = new BindingSource();
            source.DataSource = _clientList;
            cboIDCardNum.DataSource = source;
            cboIDCardNum.DisplayMember = "IdCardNo";
            cboIDCardNum.ValueMember = "Id";
        }

        /// <summary>
        /// cboIDCardNum项改变时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboIDCardNum_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cboIDCardNum.Text.Length <= 14)
            {
                this.cboIDCardNum.DroppedDown = true;
            }

            //遍历宾客集合
            foreach (Model.Client tmpClient in _clientList)
            {
                // 判读用户输入的身份证号码是否存在
                if (tmpClient.IdCardNo == this.cboIDCardNum.Text.Trim())
                {
                    //宾客姓名
                    this.txtClientName.Text = tmpClient.ModelName;
                    //宾客类型
                    this.cboClientType.Text = tmpClient.ClientType.ToString();
                    //宾客性别
                    this.cboClientSex.Text = tmpClient.Sex.ToString();
                    //宾客状态
                    this.cboCurrentState.Text = tmpClient.CurrentState.ToString();
                    //宾客地址
                    this.txtContactAddress.Text = tmpClient.ContactAddress;
                    //宾客电话
                    this.txtContactPhone.Text = tmpClient.ContactPhone;
                    //宾客出生日期
                    this.dtpBirthday.Text = tmpClient.Birthday.ToString();
                    //储存金额
                    this.txtSaveMoney.Text = tmpClient.SaveMoney.ToString();
                    //宾客备注
                    this.txtRemark.Text = tmpClient.Remark;

                    _isExit = true;
                }
            }
        }

        /// <summary>
        /// 房间详细信息
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