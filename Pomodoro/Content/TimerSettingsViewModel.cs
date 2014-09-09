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

        /// <summary>
        ///  Get set work time interval
        /// </summary>
        public double WorkTime 
        { 
            get { return _setting.WorkTime; }
            set
            { 
                _setting.WorkTime = value;
                OnPropertyChanged("WorkTime");
            }
        }

        /// <summary>
        /// Get set short break time interval 
        /// </summary>
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

        /// <summary>
        /// Get set long break time interval
        /// </summary>
        public double LongBreakTime 
        {
            get { return _setting.LongBreakTime; }
            set 
            { 
                _setting.LongBreakTime = value;
                _repository.SetPomodoroTMSettings(_setting);
                OnPropertyChanged("LongBreakTime");
            }

        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsTurnOnSound
        {
            get { return _setting.IsTurnOnSound; }
            set 
            { 
                _setting.IsTurnOnSound = (bool)value;
                _repository.SetPomodoroTMSettings(_setting);
                OnPropertyChanged("IsTurnOnSound");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool IsTurnOffMonitor 
        {
            get { return _setting.IsTurnOffMonitor; }
            set 
            { 
                _setting.IsTurnOffMonitor = (bool)value;
                _repository.SetPomodoroTMSettings(_setting);
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
