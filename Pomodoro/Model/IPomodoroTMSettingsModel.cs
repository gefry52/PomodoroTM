using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pomodoro.Model
{

  
    interface IPomodoroTMSettingsModel
    {
         double WorkTime { get; set; }
         double ShortBreakTime { get; set; }
         double LongBreakTime { get; set; }
         bool IsActiveProgressRing { get; set; }
         bool IsTurnOffMonitor { get; set; }
         bool IsTurnOnSound { get; set; }
    }
}
