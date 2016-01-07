using System;
using System.Windows.Forms;

namespace OptimalControl.Forms
{
    public partial class frmLimitEditor : Form
    {
        private double[] _limitDoubles = new double[6];

        public double[] LimitDoubles
        {
            get { return _limitDoubles; }
            set { _limitDoubles = value; }
        }


        public frmLimitEditor(double [] limitDoubles)
        {
            _limitDoubles = limitDoubles;

            InitializeComponent();
            LoadUI();
        }
        private void LoadUI()
        {
            if (_limitDoubles.Length >= 6)
            {
                tb_oc_104.Text = _limitDoubles[0].ToString();
                tb_oc_105.Text = _limitDoubles[1].ToString();
                tb_oc_114.Text = _limitDoubles[2].ToString();
                tb_oc_115.Text = _limitDoubles[3].ToString();
                tb_oc_124.Text = _limitDoubles[4].ToString();
                tb_oc_125.Text = _limitDoubles[5].ToString();
            }
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            string[] limitStrings = new[]
            {
                tb_oc_104.Text.Trim(), tb_oc_105.Text.Trim(), 
                tb_oc_114.Text.Trim(), tb_oc_115.Text.Trim(),
                tb_oc_124.Text.Trim(), tb_oc_125.Text.Trim()
            };
            for (int index = 0; index < _limitDoubles.Length; index++)
            {
                double tmpDouble = 0;
                if (double.TryParse(limitStrings[index], out tmpDouble))
                {
                    _limitDoubles[index] = tmpDouble;
                }
            }
            this.DialogResult = DialogResult.OK;
            this.Dispose();

        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Dispose();
        }

    }
}
