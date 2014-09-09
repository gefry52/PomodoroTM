using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pomodoro.Model
{
    class PomodoroTMSettingsModel:IPomodoroTMSettingsModel
    {
        private double _workTime;
        private double _shortBreakTime;
        private double _longBreakTime;
        private bool _isTurnOffMonitor;
        private bool _isTurnOnSound;
        private bool _isActiveProgressRing;

        //double worktime = 1500000, double shortBreakTime = 300000, double longBreakTime = 1800000,bool isActiveProgressRing = true, bool isTurnOffMonitor = false, bool isTurnOnSound = false
        public PomodoroTMSettingsModel(double worktime = 1500000, double shortBreakTime = 300000, double longBreakTime = 1800000, bool isActiveProgressRing = true, bool isTurnOffMonitor = false, bool isTurnOnSound = false) 
        {
            _workTime = worktime;
            _shortBreakTime = shortBreakTime;
            _longBreakTime = longBreakTime;
            _isActiveProgressRing = isActiveProgressRing;
            _isTurnOffMonitor = isTurnOffMonitor;
            _isTurnOnSound = isTurnOnSound;

        }


        public double WorkTime 
        {
            get { return _workTime; }
            set { _workTime = value; }
        }
        public double ShortBreakTime 
        {
            get { return _shortBreakTime; }
            set { _shortBreakTime = value; }
        }

        public double LongBreakTime 
        {
            get { return _longBreakTime; }
            set { _longBreakTime = value; }
        }
        public bool IsTurnOffMonitor
        {
            get { return _isTurnOffMonitor; }
            set { _isTurnOffMonitor = value; }
        }
        public bool IsTurnOnSound 
        {
            get { return _isTurnOnSound; }
            set {  _isTurnOnSound = value; }
        }

        public bool IsActiveProgressRing
        {
            get { return _isActiveProgressRing; }
            set { _isActiveProgressRing = value; }
        }
    }
}
