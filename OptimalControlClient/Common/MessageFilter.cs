using System.Windows.Forms;

namespace OptimalControl.Common
{
    /// <summary>
    /// MessageFilter
    /// </summary>
    public class MessageFilter : IMessageFilter
    {
        internal bool isActive = false;
        /// <summary>
        /// Pres the filter message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        public bool PreFilterMessage(ref Message message)
        {
            //如果检测到有鼠标或则键盘被按下的消息
            if (message.Msg == 0x0201 ||
                message.Msg == 0x0100 ||
                message.Msg == 0x0204 ||
                message.Msg == 0x0207 ||
                message.Msg == 0x0216)
            {
                isActive = true;
            }
            return false;
        }
    }
}
