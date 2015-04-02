using System;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace OptimalControl.Common
{
    public class NumbericTextbox : TextBox
    {
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            //e.Handled = !(Char.IsNumber(e.KeyChar) || e.KeyChar == (char)8 || e.KeyChar == '.');
            e.Handled = !(Char.IsNumber(e.KeyChar) || e.KeyChar == (char)8);
            if (!e.Handled) this.Tag = this.Text;//记录最后一次正确输入 
            base.OnKeyPress(e);
        }
        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            if (!System.Text.RegularExpressions.Regex.IsMatch((this).Text, @"^(?!0/d)/d+(/./d*)?$|"))
            {
                int index = (this).SelectionStart;
                (this).Text = (this).Tag as string;
                (this).SelectionStart = index;
            }
        }
        public const int WM_PASTE = 0x302;
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_PASTE)//粘贴
            {
                if (!Regex.IsMatch(Clipboard.GetText(), @"^(?!0/d)/d+(/./d*)?$|")) return;
            }
            base.WndProc(ref m);
        }
    }
}
