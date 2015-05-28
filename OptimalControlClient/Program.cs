using System;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Reflection;
using ExpertSystem;
using OptimalControl.Forms;

namespace OptimalControl
{
    internal static class Program
    {
        /// <summary> 
        /// 该函数设置由不同线程产生的窗口的显示状态。 
        /// </summary> 
        /// <param name="hWnd">窗口句柄</param> 
        /// <param name="cmdShow">指定窗口如何显示。查看允许值列表，请查阅ShowWlndow函数的说明部分。</param> 
        /// <returns>如果函数原来可见，返回值为非零；如果函数原来被隐藏，返回值为零。</returns> 
        [DllImport("User32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int cmdShow);

        /// <summary> 
        /// 该函数将创建指定窗口的线程设置到前台，并且激活该窗口。键盘输入转向该窗口，并为用户改各种可视的记号。系统给创建前台窗口的线程分配的权限稍高于其他线程。 
        /// </summary> 
        /// <param name="hWnd">将被激活并被调入前台的窗口句柄。</param> 
        /// <returns>如果窗口设入了前台，返回值为非零；如果窗口未被设入前台，返回值为零。</returns> 
        [DllImport("User32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        /// <summary>
        /// 当前应用程序
        /// </summary>
        private static string applicationName = "OptimalControl";

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        private static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Process instance = RunningInstance();
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
            if (instance == null)
            {
                frmLogin frmLogin = new frmLogin();
                frmLogin.ShowInTaskbar = false;
                frmLogin.StartPosition = FormStartPosition.CenterScreen;
                DialogResult dialogResult = frmLogin.ShowDialog();
                if (dialogResult.Equals(DialogResult.OK))
                {
                    if (frmLogin.isPass)
                    {
                        Application.Run(new FrmMain(args, frmLogin.isPass, frmLogin.currentOperator));
                    }
                }
                else if (dialogResult.Equals(DialogResult.Cancel))
                {
                    //Application.Run(new frmMain(args, frmLogin.isPass, frmLogin.currentOperator));
                }
            }
            else
            {
                HandleRunningInstance(instance);
            }
        }

        /// <summary>
        /// 全局线程异常处理事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            // 显示友好的自定义全局异常提示窗体
            Common.GlobalExceptionManager.ShowGlobalExceptionInfo(e.Exception, applicationName, "BGRIMM");
        }

        /// <summary> 
        /// 获取正在运行的实例，没有运行的实例返回null; 
        /// </summary> 
        private static Process RunningInstance()
        {
            Process current = Process.GetCurrentProcess();
            Process[] processes = Process.GetProcessesByName(current.ProcessName);
            foreach (Process process in processes)
            {
                if (process.Id != current.Id)
                {
                    if (Assembly.GetExecutingAssembly().Location.Replace("/", "\\") == current.MainModule.FileName)
                    {
                        return process;
                    }
                }
            }
            return null;
        }

        /// <summary> 
        /// 显示已运行的程序。 
        /// </summary> 
        private static void HandleRunningInstance(Process instance)
        {
            ShowWindowAsync(instance.MainWindowHandle, 3); //显示
            SetForegroundWindow(instance.MainWindowHandle); //放到前端 
        }
    }
}
