using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using IBLL.Control;
using Utility;
using Model.Control;

namespace OptimalControl.Forms
{
    public partial class frmConfig : Form
    {
        #region 构造函数
        private BLLFactory.BLLFactory _bllFactory = new BLLFactory.BLLFactory();
        private IVariableManager _variableManager;
        private List<Variable> _variableCollection;
        private List<string> _variableCodes = new List<string>();

        public frmConfig()
        {
            InitializeComponent();
            LoadSetting();
        }

        #endregion

        #region 私有函数

        private delegate void UpdateCurveGridDelegate();

        private void LoadSetting()
        {
            _variableManager = _bllFactory.BuildIVariableManager();
            _variableCollection = _variableManager.GetAllVariableInfo();

            string[] tempStrings = System.IO.Ports.SerialPort.GetPortNames();
            string tmpCode = ConfigExeSettings.GetSettingString("OptimalControlEnabledVariable", "").Trim();
            int tmpindex = _variableCodes.IndexOf(tmpCode);
        }

        private void SaveSetting()
        {
            if (tb_UpdateVariableTime.Text.Equals("") || tb_UpdateVariableTime.Text.Equals("0") || Convert.ToInt32(tb_UpdateVariableTime.Text) < 500)
            {
                MessageBox.Show("数据更新间隔格式错误！未保存该配置", "参数错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                ConfigExeSettings.SetValue("RealTime", tb_UpdateVariableTime.Text.Trim());
            }
        }

        #endregion

        #region 控件响应

        private void btn_OK_Click(object sender, EventArgs e)
        {
            SaveSetting();
            this.DialogResult = DialogResult.OK;
            this.Dispose();
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        #endregion
    }
}