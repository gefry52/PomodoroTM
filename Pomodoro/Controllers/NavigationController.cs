using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace Pomodoro.Controllers
{
    class NavigationController: INavigationController
    {
       private Object _parameter;

       public Object Parameter
       {
           get{return _parameter;}
           set{_parameter = value;}
       }

       private static readonly INavigationController GetInstatnce = new NavigationController();

       public static INavigationController Instatnce 
       {
           get { return GetInstatnce; }
           private set { }
       }

       protected NavigationController() 
       {
       }

       public void GoBack() 
       {
           NavigationCommands.PreviousPage.Execute("#",null);
       }

       public void GoHome()
       {
           NavigationCommands.GoToPage.Execute("./Pages/Home.xaml", null);
       }

       public void GoTasklist()
       {
           NavigationCommands.GoToPage.Execute("./Content/TaskList.xaml", null);
       }

       public void GoAddTask()
       {
           NavigationCommands.GoToPage.Execute("./Content/AddTask.xaml", null);
       }

       public void GoEditTask(Object parameter)
       {
           _parameter = parameter;
           NavigationCommands.GoToPage.Execute("./Content/AddTask.xaml", null);
           //_parameter = null;
       }
    }
}
