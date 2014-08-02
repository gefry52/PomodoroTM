using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace Pomodoro.Controllers
{
   public static class NavigationController
    {
       public static void GoBack() 
       {
           NavigationCommands.PreviousPage.Execute("#",null);
       }

       public static void GoHome()
       {
           NavigationCommands.GoToPage.Execute("./Pages/Home.xaml", null);
       }

       public static void GoTasklist()
       {
           NavigationCommands.GoToPage.Execute("./Content/TaskList.xaml", null);
       }

       public static void GoAddTask()
       {
           NavigationCommands.GoToPage.Execute("./Content/AddTask.xaml", null);
       }
    }
}
