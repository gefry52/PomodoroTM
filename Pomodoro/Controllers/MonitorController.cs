using System;
using System.Runtime.InteropServices;

namespace Pomodoro.Controllers
{
    
    public class MonitorController
     {
         private static int _WM_SYSCOMMAND = 0x0112;
         private static int _SC_MONITORPOWER = 0xF170;
         private static int _HWND_BROADCAST = 0xFFFF;
 
         [DllImport("user32.dll")]
         private static extern int SendMessage(int hWnd, int hMsg, int wParam, int lParam);
 
         public static void OffMonitor()
         {
             SendMessage(_HWND_BROADCAST, _WM_SYSCOMMAND, _SC_MONITORPOWER, 2);
         }
 
         public static void OnMonitor()
         {
             SendMessage(_HWND_BROADCAST, _WM_SYSCOMMAND, _SC_MONITORPOWER, -1);
         }
     
    }
}
