using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pomodoro.Controllers
{
    interface INavigationController
    {
        Object Parameter{get;set;}
        void GoBack();
        void GoHome();
        void GoTasklist();
        void GoAddTask();
        void GoEditTask(Object obj);

    }
}
