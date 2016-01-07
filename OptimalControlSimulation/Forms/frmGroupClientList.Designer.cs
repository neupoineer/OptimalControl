namespace MonitoringTool.Forms
{
    partial class frmGroupClientList
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.lblRoomNo = new System.Windows.Forms.Label();
            this.tvUsableRoom = new System.Windows.Forms.TreeView();
            this.lblLeaseholdRoomInfo = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cboIDCardNum = new System.Windows.Forms.ComboBox();
            this.txtClientName = new System.Windows.Forms.TextBox();
            this.txtSaveMoney = new CodingMouse.CMCSharpSDK.CMControls.CMTextBox.CMTextBoxFloat();
            this.txtContactPhone = new CodingMouse.CMCSharpSDK.CMControls.CMTextBox.CMTextBoxInteger();
            this.txtConsumedUnitPrice = new CodingMouse.CMCSharpSDK.CMControls.CMTextBox.CMTextBoxFloat();
            this.txtRebateScale = new CodingMouse.CMCSharpSDK.CMControls.CMTextBox.CMTextBoxFloat();
            this.txtRabatePrice = new CodingMouse.CMCSharpSDK.CMControls.CMTextBox.CMTextBoxFloat();
            this.cboClientSex = new System.Windows.Forms.ComboBox();
            this.cboCurrentState = new System.Windows.Forms.ComboBox();
            this.cboClientType = new System.Windows.Forms.ComboBox();
            this.dtpBirthday = new System.Windows.Forms.DateTimePicker();
            this.txtContactAddress = new System.Windows.Forms.TextBox();
            this.lblSaveMoney = new System.Windows.Forms.Label();
            this.lblContactAddress = new System.Windows.Forms.Label();
            this.lblClientSex = new System.Windows.Forms.Label();
            this.lblIDCardNum = new System.Windows.Forms.Label();
            this.lblBirthday = new System.Windows.Forms.Label();
            this.lblContactPhone = new System.Windows.Forms.Label();
            this.lblCurrentState = new System.Windows.Forms.Label();
            this.lblClientType = new System.Windows.Forms.Label();
            this.lblRebateScale = new System.Windows.Forms.Label();
            this.lblRabatePrice = new System.Windows.Forms.Label();
            this.dtpConsumedTime = new System.Windows.Forms.DateTimePicker();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.lblRemark = new System.Windows.Forms.Label();
            this.lblOperatorName = new System.Windows.Forms.Label();
            this.txtOperatorName = new System.Windows.Forms.TextBox();
            this.lblConsumedTime = new System.Windows.Forms.Label();
            this.lblConsumedUnitPrice = new System.Windows.Forms.Label();
            this.lblClientName = new System.Windows.Forms.Label();
            this.txtRoomNumber = new System.Windows.Forms.TextBox();
            this.lblRoom = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lvRoomInfo = new System.Windows.Forms.ListView();
            this.colRoomType = new System.Windows.Forms.ColumnHeader();
            this.colRoomNumber = new System.Windows.Forms.ColumnHeader();
            this.colAdvanceUnitPrice = new System.Windows.Forms.ColumnHeader();
            this.colTabRebateScale = new System.Windows.Forms.ColumnHeader();
            this.colRebatedUnitPrice = new System.Windows.Forms.ColumnHeader();
            this.txtRoomNumbern = new System.Windows.Forms.TextBox();
            this.gpUsableRoom = new System.Windows.Forms.GroupBox();
            this.lblUsableRoom = new System.Windows.Forms.Label();
            this.gpRoomInfo = new System.Windows.Forms.GroupBox();
            this.helpProvider = new System.Windows.Forms.HelpProvider();
            this.groupBox1.SuspendLayout();
            this.gpUsableRoom.SuspendLayout();
            this.gpRoomInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblRoomNo
            // 
            this.lblRoomNo.AutoSize = true;
            this.lblRoomNo.Location = new System.Drawing.Point(6, 36);
            this.lblRoomNo.Name = "lblRoomNo";
            this.lblRoomNo.Size = new System.Drawing.Size(53, 12);
            this.lblRoomNo.TabIndex = 0;
            this.lblRoomNo.Text = "房间号：";
            // 
            // tvUsableRoom
            // 
            this.tvUsableRoom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.tvUsableRoom.Location = new System.Drawing.Point(0, 60);
            this.tvUsableRoom.Name = "tvUsableRoom";
            this.tvUsableRoom.Size = new System.Drawing.Size(198, 205);
            this.tvUsableRoom.TabIndex = 1;
            this.tvUsableRoom.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvInRoomInfo_NodeMouseDoubleClick_1);
            // 
            // lblLeaseholdRoomInfo
            // 
            this.lblLeaseholdRoomInfo.BackColor = System.Drawing.Color.DarkGreen;
            this.lblLeaseholdRoomInfo.ForeColor = System.Drawing.Color.White;
            this.lblLeaseholdRoomInfo.Location = new System.Drawing.Point(0, 6);
            this.lblLeaseholdRoomInfo.Name = "lblLeaseholdRoomInfo";
            this.lblLeaseholdRoomInfo.Size = new System.Drawing.Size(414, 19);
            this.lblLeaseholdRoomInfo.TabIndex = 3;
            this.lblLeaseholdRoomInfo.Text = "开单房间信息";
            this.lblLeaseholdRoomInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cboIDCardNum);
            this.groupBox1.Controls.Add(this.txtClientName);
            this.groupBox1.Controls.Add(this.txtSaveMoney);
            this.groupBox1.Controls.Add(this.txtContactPhone);
            this.groupBox1.Controls.Add(this.txtConsumedUnitPrice);
            this.groupBox1.Controls.Add(this.txtRebateScale);
            this.groupBox1.Controls.Add(this.txtRabatePrice);
            this.groupBox1.Controls.Add(this.cboClientSex);
            this.groupBox1.Controls.Add(this.cboCurrentState);
            this.groupBox1.Controls.Add(this.cboClientType);
            this.groupBox1.Controls.Add(this.dtpBirthday);
            this.groupBox1.Controls.Add(this.txtContactAddress);
            this.groupBox1.Controls.Add(this.lblSaveMoney);
            this.groupBox1.Controls.Add(this.lblContactAddress);
            this.groupBox1.Controls.Add(this.lblClientSex);
            this.groupBox1.Controls.Add(this.lblIDCardNum);
            this.groupBox1.Controls.Add(this.lblBirthday);
            this.groupBox1.Controls.Add(this.lblContactPhone);
            this.groupBox1.Controls.Add(this.lblCurrentState);
            this.groupBox1.Controls.Add(this.lblClientType);
            this.groupBox1.Controls.Add(this.lblRebateScale);
            this.groupBox1.Controls.Add(this.lblRabatePrice);
            this.groupBox1.Controls.Add(this.dtpConsumedTime);
            this.groupBox1.Controls.Add(this.txtRemark);
            this.groupBox1.Controls.Add(this.lblRemark);
            this.groupBox1.Controls.Add(this.lblOperatorName);
            this.groupBox1.Controls.Add(this.txtOperatorName);
            this.groupBox1.Controls.Add(this.lblConsumedTime);
            this.groupBox1.Controls.Add(this.lblConsumedUnitPrice);
            this.groupBox1.Controls.Add(this.lblClientName);
            this.groupBox1.Controls.Add(this.txtRoomNumber);
            this.groupBox1.Controls.Add(this.lblRoom);
            this.groupBox1.Location = new System.Drawing.Point(12, 271);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(635, 270);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            // 
            // cboIDCardNum
            // 
            this.cboIDCardNum.FormattingEnabled = true;
            this.cboIDCardNum.Location = new System.Drawing.Point(500, 93);
            this.cboIDCardNum.Name = "cboIDCardNum";
            this.cboIDCardNum.Size = new System.Drawing.Size(121, 20);
            this.cboIDCardNum.TabIndex = 85;
            this.cboIDCardNum.SelectedValueChanged += new System.EventHandler(this.cboIDCardNum_SelectedValueChanged);
            // 
            // txtClientName
            // 
            this.txtClientName.Location = new System.Drawing.Point(80, 93);
            this.txtClientName.Name = "txtClientName";
            this.txtClientName.Size = new System.Drawing.Size(118, 21);
            this.txtClientName.TabIndex = 84;
            // 
            // txtSaveMoney
            // 
            this.txtSaveMoney.DecimalDigits = 2;
            this.txtSaveMoney.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtSaveMoney.ForeColor = System.Drawing.Color.Blue;
            this.txtSaveMoney.Location = new System.Drawing.Point(501, 129);
            this.txtSaveMoney.Name = "txtSaveMoney";
            this.txtSaveMoney.Size = new System.Drawing.Size(120, 21);
            this.txtSaveMoney.TabIndex = 83;
            this.txtSaveMoney.Text = "0.00";
            // 
            // txtContactPhone
            // 
            this.txtContactPhone.Location = new System.Drawing.Point(296, 129);
            this.txtContactPhone.Name = "txtContactPhone";
            this.txtContactPhone.Size = new System.Drawing.Size(120, 21);
            this.txtContactPhone.TabIndex = 82;
            // 
            // txtConsumedUnitPrice
            // 
            this.txtConsumedUnitPrice.BackColor = System.Drawing.SystemColors.Info;
            this.txtConsumedUnitPrice.DecimalDigits = 2;
            this.txtConsumedUnitPrice.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtConsumedUnitPrice.ForeColor = System.Drawing.Color.Blue;
            this.txtConsumedUnitPrice.Location = new System.Drawing.Point(296, 18);
            this.txtConsumedUnitPrice.Name = "txtConsumedUnitPrice";
            this.txtConsumedUnitPrice.ReadOnly = true;
            this.txtConsumedUnitPrice.Size = new System.Drawing.Size(120, 21);
            this.txtConsumedUnitPrice.TabIndex = 81;
            this.txtConsumedUnitPrice.Text = "0.00";
            // 
            // txtRebateScale
            // 
            this.txtRebateScale.BackColor = System.Drawing.SystemColors.Info;
            this.txtRebateScale.DecimalDigits = 2;
            this.txtRebateScale.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtRebateScale.ForeColor = System.Drawing.Color.Blue;
            this.txtRebateScale.Location = new System.Drawing.Point(501, 18);
            this.txtRebateScale.Name = "txtRebateScale";
            this.txtRebateScale.ReadOnly = true;
            this.txtRebateScale.Size = new System.Drawing.Size(120, 21);
            this.txtRebateScale.TabIndex = 79;
            this.txtRebateScale.Text = "1.0";
            // 
            // txtRabatePrice
            // 
            this.txtRabatePrice.BackColor = System.Drawing.SystemColors.Info;
            this.txtRabatePrice.DecimalDigits = 2;
            this.txtRabatePrice.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtRabatePrice.ForeColor = System.Drawing.Color.Blue;
            this.txtRabatePrice.Location = new System.Drawing.Point(80, 57);
            this.txtRabatePrice.Name = "txtRabatePrice";
            this.txtRabatePrice.ReadOnly = true;
            this.txtRabatePrice.Size = new System.Drawing.Size(120, 21);
            this.txtRabatePrice.TabIndex = 78;
            this.txtRabatePrice.Text = "0.00";
            // 
            // cboClientSex
            // 
            this.cboClientSex.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboClientSex.FormattingEnabled = true;
            this.cboClientSex.Items.AddRange(new object[] {
            "男",
            "女"});
            this.cboClientSex.Location = new System.Drawing.Point(296, 93);
            this.cboClientSex.Name = "cboClientSex";
            this.cboClientSex.Size = new System.Drawing.Size(120, 20);
            this.cboClientSex.TabIndex = 67;
            // 
            // cboCurrentState
            // 
            this.cboCurrentState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCurrentState.FormattingEnabled = true;
            this.cboCurrentState.Items.AddRange(new object[] {
            "可用",
            "停用",
            "挂失"});
            this.cboCurrentState.Location = new System.Drawing.Point(80, 165);
            this.cboCurrentState.Name = "cboCurrentState";
            this.cboCurrentState.Size = new System.Drawing.Size(119, 20);
            this.cboCurrentState.TabIndex = 73;
            // 
            // cboClientType
            // 
            this.cboClientType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboClientType.FormattingEnabled = true;
            this.cboClientType.Items.AddRange(new object[] {
            "普通宾客",
            "VIP客户"});
            this.cboClientType.Location = new System.Drawing.Point(80, 129);
            this.cboClientType.Name = "cboClientType";
            this.cboClientType.Size = new System.Drawing.Size(119, 20);
            this.cboClientType.TabIndex = 63;
            // 
            // dtpBirthday
            // 
            this.dtpBirthday.Location = new System.Drawing.Point(296, 164);
            this.dtpBirthday.Name = "dtpBirthday";
            this.dtpBirthday.Size = new System.Drawing.Size(120, 21);
            this.dtpBirthday.TabIndex = 75;
            // 
            // txtContactAddress
            // 
            this.txtContactAddress.Location = new System.Drawing.Point(80, 201);
            this.txtContactAddress.Name = "txtContactAddress";
            this.txtContactAddress.Size = new System.Drawing.Size(540, 21);
            this.txtContactAddress.TabIndex = 77;
            // 
            // lblSaveMoney
            // 
            this.lblSaveMoney.AutoSize = true;
            this.lblSaveMoney.Location = new System.Drawing.Point(430, 132);
            this.lblSaveMoney.Name = "lblSaveMoney";
            this.lblSaveMoney.Size = new System.Drawing.Size(65, 12);
            this.lblSaveMoney.TabIndex = 70;
            this.lblSaveMoney.Text = "储值金额：";
            // 
            // lblContactAddress
            // 
            this.lblContactAddress.AutoSize = true;
            this.lblContactAddress.Location = new System.Drawing.Point(16, 204);
            this.lblContactAddress.Name = "lblContactAddress";
            this.lblContactAddress.Size = new System.Drawing.Size(65, 12);
            this.lblContactAddress.TabIndex = 76;
            this.lblContactAddress.Text = "联系地址：";
            // 
            // lblClientSex
            // 
            this.lblClientSex.AutoSize = true;
            this.lblClientSex.Location = new System.Drawing.Point(225, 96);
            this.lblClientSex.Name = "lblClientSex";
            this.lblClientSex.Size = new System.Drawing.Size(65, 12);
            this.lblClientSex.TabIndex = 66;
            this.lblClientSex.Text = "性    别：";
            // 
            // lblIDCardNum
            // 
            this.lblIDCardNum.AutoSize = true;
            this.lblIDCardNum.Location = new System.Drawing.Point(430, 96);
            this.lblIDCardNum.Name = "lblIDCardNum";
            this.lblIDCardNum.Size = new System.Drawing.Size(65, 12);
            this.lblIDCardNum.TabIndex = 64;
            this.lblIDCardNum.Text = "身份证号：";
            // 
            // lblBirthday
            // 
            this.lblBirthday.AutoSize = true;
            this.lblBirthday.Location = new System.Drawing.Point(225, 168);
            this.lblBirthday.Name = "lblBirthday";
            this.lblBirthday.Size = new System.Drawing.Size(65, 12);
            this.lblBirthday.TabIndex = 74;
            this.lblBirthday.Text = "出生日期：";
            // 
            // lblContactPhone
            // 
            this.lblContactPhone.AutoSize = true;
            this.lblContactPhone.Location = new System.Drawing.Point(225, 132);
            this.lblContactPhone.Name = "lblContactPhone";
            this.lblContactPhone.Size = new System.Drawing.Size(71, 12);
            this.lblContactPhone.TabIndex = 68;
            this.lblContactPhone.Text = "联系电话： ";
            // 
            // lblCurrentState
            // 
            this.lblCurrentState.AutoSize = true;
            this.lblCurrentState.Location = new System.Drawing.Point(16, 168);
            this.lblCurrentState.Name = "lblCurrentState";
            this.lblCurrentState.Size = new System.Drawing.Size(65, 12);
            this.lblCurrentState.TabIndex = 72;
            this.lblCurrentState.Text = "当前状态：";
            // 
            // lblClientType
            // 
            this.lblClientType.AutoSize = true;
            this.lblClientType.Location = new System.Drawing.Point(16, 132);
            this.lblClientType.Name = "lblClientType";
            this.lblClientType.Size = new System.Drawing.Size(65, 12);
            this.lblClientType.TabIndex = 62;
            this.lblClientType.Text = "宾客类型：";
            // 
            // lblRebateScale
            // 
            this.lblRebateScale.AutoSize = true;
            this.lblRebateScale.Location = new System.Drawing.Point(430, 21);
            this.lblRebateScale.Name = "lblRebateScale";
            this.lblRebateScale.Size = new System.Drawing.Size(65, 12);
            this.lblRebateScale.TabIndex = 28;
            this.lblRebateScale.Text = "打折比例：";
            // 
            // lblRabatePrice
            // 
            this.lblRabatePrice.AutoSize = true;
            this.lblRabatePrice.Location = new System.Drawing.Point(16, 60);
            this.lblRabatePrice.Name = "lblRabatePrice";
            this.lblRabatePrice.Size = new System.Drawing.Size(65, 12);
            this.lblRabatePrice.TabIndex = 26;
            this.lblRabatePrice.Text = "优惠金额：";
            // 
            // dtpConsumedTime
            // 
            this.dtpConsumedTime.Location = new System.Drawing.Point(296, 56);
            this.dtpConsumedTime.Name = "dtpConsumedTime";
            this.dtpConsumedTime.Size = new System.Drawing.Size(120, 21);
            this.dtpConsumedTime.TabIndex = 5;
            // 
            // txtRemark
            // 
            this.txtRemark.Location = new System.Drawing.Point(80, 237);
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(541, 21);
            this.txtRemark.TabIndex = 7;
            // 
            // lblRemark
            // 
            this.lblRemark.AutoSize = true;
            this.lblRemark.Location = new System.Drawing.Point(4, 240);
            this.lblRemark.Name = "lblRemark";
            this.lblRemark.Size = new System.Drawing.Size(77, 12);
            this.lblRemark.TabIndex = 23;
            this.lblRemark.Text = "(宾客)备注：";
            // 
            // lblOperatorName
            // 
            this.lblOperatorName.AutoSize = true;
            this.lblOperatorName.Location = new System.Drawing.Point(442, 60);
            this.lblOperatorName.Name = "lblOperatorName";
            this.lblOperatorName.Size = new System.Drawing.Size(53, 12);
            this.lblOperatorName.TabIndex = 22;
            this.lblOperatorName.Text = "记帐人：";
            // 
            // txtOperatorName
            // 
            this.txtOperatorName.BackColor = System.Drawing.SystemColors.Info;
            this.txtOperatorName.Location = new System.Drawing.Point(501, 56);
            this.txtOperatorName.Name = "txtOperatorName";
            this.txtOperatorName.ReadOnly = true;
            this.txtOperatorName.Size = new System.Drawing.Size(120, 21);
            this.txtOperatorName.TabIndex = 3;
            // 
            // lblConsumedTime
            // 
            this.lblConsumedTime.AutoSize = true;
            this.lblConsumedTime.Location = new System.Drawing.Point(225, 60);
            this.lblConsumedTime.Name = "lblConsumedTime";
            this.lblConsumedTime.Size = new System.Drawing.Size(65, 12);
            this.lblConsumedTime.TabIndex = 19;
            this.lblConsumedTime.Text = "时    间：";
            // 
            // lblConsumedUnitPrice
            // 
            this.lblConsumedUnitPrice.AutoSize = true;
            this.lblConsumedUnitPrice.Location = new System.Drawing.Point(225, 21);
            this.lblConsumedUnitPrice.Name = "lblConsumedUnitPrice";
            this.lblConsumedUnitPrice.Size = new System.Drawing.Size(65, 12);
            this.lblConsumedUnitPrice.TabIndex = 14;
            this.lblConsumedUnitPrice.Text = "消费单价：";
            // 
            // lblClientName
            // 
            this.lblClientName.AutoSize = true;
            this.lblClientName.Location = new System.Drawing.Point(16, 96);
            this.lblClientName.Name = "lblClientName";
            this.lblClientName.Size = new System.Drawing.Size(65, 12);
            this.lblClientName.TabIndex = 9;
            this.lblClientName.Text = "宾客姓名：";
            // 
            // txtRoomNumber
            // 
            this.txtRoomNumber.BackColor = System.Drawing.SystemColors.Info;
            this.txtRoomNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRoomNumber.Location = new System.Drawing.Point(80, 19);
            this.txtRoomNumber.Name = "txtRoomNumber";
            this.txtRoomNumber.ReadOnly = true;
            this.txtRoomNumber.Size = new System.Drawing.Size(119, 21);
            this.txtRoomNumber.TabIndex = 1;
            // 
            // lblRoom
            // 
            this.lblRoom.AutoSize = true;
            this.lblRoom.Location = new System.Drawing.Point(16, 21);
            this.lblRoom.Name = "lblRoom";
            this.lblRoom.Size = new System.Drawing.Size(65, 12);
            this.lblRoom.TabIndex = 8;
            this.lblRoom.Text = "主客房间：";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(491, 549);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 6;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(572, 549);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lvRoomInfo
            // 
            this.lvRoomInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lvRoomInfo.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colRoomType,
            this.colRoomNumber,
            this.colAdvanceUnitPrice,
            this.colTabRebateScale,
            this.colRebatedUnitPrice});
            this.lvRoomInfo.FullRowSelect = true;
            this.lvRoomInfo.GridLines = true;
            this.lvRoomInfo.Location = new System.Drawing.Point(0, 25);
            this.lvRoomInfo.MultiSelect = false;
            this.lvRoomInfo.Name = "lvRoomInfo";
            this.lvRoomInfo.Size = new System.Drawing.Size(414, 240);
            this.lvRoomInfo.TabIndex = 18;
            this.lvRoomInfo.UseCompatibleStateImageBehavior = false;
            this.lvRoomInfo.View = System.Windows.Forms.View.Details;
            this.lvRoomInfo.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvRoomInfo_MouseDoubleClick);
            this.lvRoomInfo.SelectedIndexChanged += new System.EventHandler(this.lvRoomInfo_SelectedIndexChanged);
            // 
            // colRoomType
            // 
            this.colRoomType.Text = "房间类型";
            this.colRoomType.Width = 100;
            // 
            // colRoomNumber
            // 
            this.colRoomNumber.Text = "房间号";
            this.colRoomNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.colRoomNumber.Width = 85;
            // 
            // colAdvanceUnitPrice
            // 
            this.colAdvanceUnitPrice.Text = "原房价";
            this.colAdvanceUnitPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.colAdvanceUnitPrice.Width = 75;
            // 
            // colTabRebateScale
            // 
            this.colTabRebateScale.Text = "打折比例";
            this.colTabRebateScale.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.colTabRebateScale.Width = 70;
            // 
            // colRebatedUnitPrice
            // 
            this.colRebatedUnitPrice.Text = "折后房价";
            this.colRebatedUnitPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.colRebatedUnitPrice.Width = 80;
            // 
            // txtRoomNumbern
            // 
            this.txtRoomNumbern.Location = new System.Drawing.Point(65, 33);
            this.txtRoomNumbern.Name = "txtRoomNumbern";
            this.txtRoomNumbern.Size = new System.Drawing.Size(127, 21);
            this.txtRoomNumbern.TabIndex = 0;
            this.txtRoomNumbern.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtRoomNumbern_KeyDown);
            // 
            // gpUsableRoom
            // 
            this.gpUsableRoom.Controls.Add(this.lblUsableRoom);
            this.gpUsableRoom.Controls.Add(this.tvUsableRoom);
            this.gpUsableRoom.Controls.Add(this.txtRoomNumbern);
            this.gpUsableRoom.Controls.Add(this.lblRoomNo);
            this.gpUsableRoom.ForeColor = System.Drawing.Color.Black;
            this.gpUsableRoom.Location = new System.Drawing.Point(14, 3);
            this.gpUsableRoom.Name = "gpUsableRoom";
            this.gpUsableRoom.Size = new System.Drawing.Size(198, 265);
            this.gpUsableRoom.TabIndex = 19;
            this.gpUsableRoom.TabStop = false;
            // 
            // lblUsableRoom
            // 
            this.lblUsableRoom.BackColor = System.Drawing.Color.DarkGreen;
            this.lblUsableRoom.ForeColor = System.Drawing.Color.White;
            this.lblUsableRoom.Location = new System.Drawing.Point(1, 6);
            this.lblUsableRoom.Name = "lblUsableRoom";
            this.lblUsableRoom.Size = new System.Drawing.Size(195, 19);
            this.lblUsableRoom.TabIndex = 2;
            this.lblUsableRoom.Text = "可供房 ";
            this.lblUsableRoom.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gpRoomInfo
            // 
            this.gpRoomInfo.Controls.Add(this.lblLeaseholdRoomInfo);
            this.gpRoomInfo.Controls.Add(this.lvRoomInfo);
            this.gpRoomInfo.Location = new System.Drawing.Point(229, 3);
            this.gpRoomInfo.Name = "gpRoomInfo";
            this.gpRoomInfo.Size = new System.Drawing.Size(416, 265);
            this.gpRoomInfo.TabIndex = 20;
            this.gpRoomInfo.TabStop = false;
            // 
            // helpProvider
            // 
            this.helpProvider.HelpNamespace = "CMHotelManager.chm";
            // 
            // frmGroupClientList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(659, 584);
            this.Controls.Add(this.gpRoomInfo);
            this.Controls.Add(this.gpUsableRoom);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.helpProvider.SetHelpKeyword(this, "来宾登记");
            this.helpProvider.SetHelpNavigator(this, System.Windows.Forms.HelpNavigator.KeywordIndex);
            this.helpProvider.SetHelpString(this, "来宾登记");
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(665, 470);
            this.Name = "frmGroupClientList";
            this.helpProvider.SetShowHelp(this, true);
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "团体开单";
            this.Load += new System.EventHandler(this.frmGroupClientList_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gpUsableRoom.ResumeLayout(false);
            this.gpUsableRoom.PerformLayout();
            this.gpRoomInfo.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblRoomNo;
        private System.Windows.Forms.TreeView tvUsableRoom;
        private System.Windows.Forms.Label lblLeaseholdRoomInfo;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ListView lvRoomInfo;
        private System.Windows.Forms.ColumnHeader colRoomType;
        private System.Windows.Forms.ColumnHeader colRoomNumber;
        private System.Windows.Forms.ColumnHeader colAdvanceUnitPrice;
        private System.Windows.Forms.ColumnHeader colTabRebateScale;
        private System.Windows.Forms.ColumnHeader colRebatedUnitPrice;
        private System.Windows.Forms.TextBox txtRoomNumber;
        private System.Windows.Forms.Label lblRoom;
        private System.Windows.Forms.Label lblClientName;
        private System.Windows.Forms.Label lblConsumedUnitPrice;
        private System.Windows.Forms.TextBox txtOperatorName;
        private System.Windows.Forms.Label lblConsumedTime;
        private System.Windows.Forms.Label lblOperatorName;
        private System.Windows.Forms.TextBox txtRemark;
        private System.Windows.Forms.Label lblRemark;
        private System.Windows.Forms.DateTimePicker dtpConsumedTime;
        private System.Windows.Forms.TextBox txtRoomNumbern;
        private System.Windows.Forms.GroupBox gpUsableRoom;
        private System.Windows.Forms.Label lblUsableRoom;
        private System.Windows.Forms.GroupBox gpRoomInfo;
        private System.Windows.Forms.Label lblRabatePrice;
        private System.Windows.Forms.Label lblRebateScale;
        private System.Windows.Forms.ComboBox cboClientSex;
        private System.Windows.Forms.ComboBox cboCurrentState;
        private System.Windows.Forms.ComboBox cboClientType;
        private System.Windows.Forms.DateTimePicker dtpBirthday;
        private System.Windows.Forms.TextBox txtContactAddress;
        private System.Windows.Forms.Label lblSaveMoney;
        private System.Windows.Forms.Label lblContactAddress;
        private System.Windows.Forms.Label lblClientSex;
        private System.Windows.Forms.Label lblIDCardNum;
        private System.Windows.Forms.Label lblBirthday;
        private System.Windows.Forms.Label lblContactPhone;
        private System.Windows.Forms.Label lblCurrentState;
        private System.Windows.Forms.Label lblClientType;
        private CodingMouse.CMCSharpSDK.CMControls.CMTextBox.CMTextBoxFloat txtRebateScale;
        private CodingMouse.CMCSharpSDK.CMControls.CMTextBox.CMTextBoxFloat txtRabatePrice;
        private CodingMouse.CMCSharpSDK.CMControls.CMTextBox.CMTextBoxFloat txtConsumedUnitPrice;
        private CodingMouse.CMCSharpSDK.CMControls.CMTextBox.CMTextBoxInteger txtContactPhone;
        private CodingMouse.CMCSharpSDK.CMControls.CMTextBox.CMTextBoxFloat txtSaveMoney;
        private System.Windows.Forms.TextBox txtClientName;
        private System.Windows.Forms.ComboBox cboIDCardNum;
        private System.Windows.Forms.HelpProvider helpProvider;
    }
}