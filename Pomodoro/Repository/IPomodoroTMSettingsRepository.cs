using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pomodoro.Repository
{
    interface IPomodoroTMSettingsRepository
    {
        Model.IPomodoroTMSettingsModel GetPomodoroTMSettings();
        bool SavePomodoroTMSettings(Model.IPomodoroTMSettingsModel settings);

    }
}
