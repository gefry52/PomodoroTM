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
        void SetPomodoroTMSettings(Model.IPomodoroTMSettingsModel settings);
        bool Commit();
    }
}
