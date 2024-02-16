using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Common
{
    /// <summary>
    /// 사용방법 : AutoClosingMessageBox.Show(내용, 타이틀, 종료시간);
    /// </summary>
    public class AutoClosingMessageBox
    {
        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)] 
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName); 
        [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)] 
        static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam); 
        System.Threading.Timer _timeoutTimer; //쓰레드 타이머 
        string _caption; const int WM_CLOSE = 0x0010; //close 명령 
        AutoClosingMessageBox(string text, string caption, int timeout) 
        { _caption = caption; _timeoutTimer = new System.Threading.Timer(OnTimerElapsed, null, timeout, System.Threading.Timeout.Infinite); 
            MessageBox.Show(text, caption); } //생성자 함수 
        public static void Show(string text, string caption, int timeout) 
        { 
            new AutoClosingMessageBox(text, caption, timeout); 
        } //시간이 다되면 close 메세지를 보냄 
        void OnTimerElapsed(object state) 
        { 
            IntPtr mbWnd = FindWindow(null, _caption); 
            if (mbWnd != IntPtr.Zero) SendMessage(mbWnd, WM_CLOSE, IntPtr.Zero, IntPtr.Zero); _timeoutTimer.Dispose();
        }
    }
}
