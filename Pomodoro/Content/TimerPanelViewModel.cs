using FirstFloor.ModernUI.Presentation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CountdownTimer;

namespace Pomodoro.Content
{
    class TimerPanelViewModel:  INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        
        private CountdownTimer.CountdownTimer _cTimer = new CountdownTimer.CountdownTimer();

        private string _remainingTime { get; set; }

        private int _remainingProgressBarrValue { get; set; }

        private int _remainingProgressBarrMaximum { get; set; }

        private bool _progressRingState { get; set; }

        private Model.IPomodoroTMSettingsModel _pomodoroSetting;

        Repository.IPomodoroTMSettingsRepository _pomodoroSettingsRepository;
        
        //ToDo how does it work?
        private ICommand _runTimer{
            get{return new RelayCommand(x => ExecuteRunTimer());}
    
        }



        public ICommand RunTimer
        {
            get {return _runTimer;}
            set {}
        }
       
        
        public TimerPanelViewModel() 
        {
            _cTimer.CountdownTimerTick += onTimerTick;
            _cTimer.CountdownTimeOver += onTimerOver;
            _pomodoroSettingsRepository = new Repository.PomodoroTMSettingsRepository();
            _pomodoroSetting = _pomodoroSettingsRepository.GetPomodoroTMSettings();
            _progressRingState = false;
        }


        private void onTimerOver(object sender, CountdownTimerEventArgs e)
        {
            ProgressRingState = false;
           // Pomodoro.Controllers.MonitorController.OffMonitor();
        }
        private void onTimerTick(object sender, CountdownTimerEventArgs e)
        {
            RemainingTime = ((int)(e.RemainingTime / 1000) / 60).ToString("D2") + ":" + ((int)(e.RemainingTime / 1000) % 60).ToString("D2");
            RemainingProgressBarrValue = (int)e.RemainingTime;
        }
        
        public void ExecuteRunTimer() 
        { 

            _cTimer.CountTime = _pomodoroSetting.WorkTime;
            RemainingProgressBarrMaximum = (int)_cTimer.CountTime;
            _cTimer.Start();
            ProgressRingState = true;
        }

     

        public string RemainingTime
        {
            get
            {
                return _remainingTime;
            }       
  
           private set
            {
                if (value != null)
                    _remainingTime = value;
                OnPropertyChanged("RemainingTime");
            }
        }

        public int RemainingProgressBarrMaximum {
            get { return _remainingProgressBarrMaximum; }
            private set {
                _remainingProgressBarrMaximum = value;
                OnPropertyChanged("RemainingProgressBarrMaximum");
                }
        }

        public int RemainingProgressBarrValue
        {
            get { return _remainingProgressBarrValue; }
            private set
            {
                _remainingProgressBarrValue = value;
                OnPropertyChanged("RemainingProgressBarrValue");
            }
        }

        public bool ProgressRingState 
        {
            get { return _progressRingState; }
            private set { 
                _progressRingState = value;
                OnPropertyChanged("ProgressRingState");
            }
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
      

    }
    
    
}
