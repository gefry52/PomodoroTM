using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Pomodoro.Content
{
    class TimerSettingsViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private Repository.IPomodoroTMSettingsRepository _repository = Repository.PomodoroTMSettingsRepository.SettingInstance;
        private Model.IPomodoroTMSettingsModel _setting;

        public double WorkTime 
        { 
            get { return _setting.WorkTime; }
            set
            { 
                _setting.WorkTime = value;
                OnPropertyChanged("WorkTime");
            }
        }

        public double ShortBreakTime 
        {
            get { return _setting.ShortBreakTime; }
            set 
            { 
                _setting.ShortBreakTime = value;
                _repository.SetPomodoroTMSettings(_setting);
                OnPropertyChanged("ShortBreakTime");
            }
        }

        public double LongBreakTime 
        {
            get { return _setting.LongBreakTime; }
            set 
            { 
                _setting.LongBreakTime = value;
                OnPropertyChanged("LongBreakTime");
            }

        }

        public bool IsTurnOnSound
        {
            get { return _setting.IsTurnOnSound; }
            set 
            { 
                _setting.IsTurnOnSound = value;
                OnPropertyChanged("IsTurnOnSound");
            }
        }

        public bool IsTurnOffMonitor 
        {
            get { return _setting.IsTurnOffMonitor; }
            set 
            { 
                _setting.IsTurnOffMonitor = value;
                OnPropertyChanged("IsTurnOffMonitor");
            }
        }

        public TimerSettingsViewModel() 
        {
            _setting = _repository.GetPomodoroTMSettings();

        }

        public void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));

            }
        }

    }
}
