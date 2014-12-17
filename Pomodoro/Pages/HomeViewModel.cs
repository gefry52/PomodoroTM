using FirstFloor.ModernUI.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;

using CountdownTimer;
using System.Windows.Input;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Media;
using System.Globalization;
using System.Threading;

namespace Pomodoro.Pages
{
    class HomeViewModel: INotifyPropertyChanged
    
    {    
        public event PropertyChangedEventHandler PropertyChanged;

        private DateChangedNotifier _dateChangeNotifer = new DateChangedNotifier();

        private Repository.ITaskRepository _taskRepository = Repository.TaskRepository.TaskRepositoryInstance;
        private Repository.IPomodoroTMSettingsRepository _settingRepository = Repository.PomodoroTMSettingsRepository.SettingInstance;
        private Model.IPomodoroTMSettingsModel _setting;
        private Model.ITaskModel _selectedTask;
        private Model.ITaskModel _currentTask;
        private IList<Model.ITaskModel> _toDayTasks;
        private bool _runTimer = false;
        private int _selectedTaskIndex;
        //
        private CultureInfo _culture = Thread.CurrentThread.CurrentCulture;
       //
        /// <summary>
        /// Get or set list of tasks who mast been execute today
        /// </summary>
        public IList<Model.ITaskModel> ToDayTasks 
        {
            get { return _toDayTasks; }
            set 
            { 
                _toDayTasks = value;
                OnPropertyChanged("ToDayTasks");
            }
        }

        #region // Timer properties
        /// <summary>
        /// Get or set state timer 
        /// </summary>
        public bool RunTimer 
        {
            get { return _runTimer; }
            set 
            {
                _runTimer = value;
                OnPropertyChanged("RunTimer");
            }
        }
        /// <summary>
        /// Get  workTime in pomodoro timer
        /// </summary>
        public double WorkTime 
        {
            get { return (_setting != null) ? _setting.WorkTime : 160000; }
            
        }
        /// <summary>
        /// Get shortBreakTime in pomodoro timer
        /// </summary>
        public double ShortBreakTime 
        {
            get { return (_setting != null) ? _setting.ShortBreakTime : 160000; }
        }
        /// <summary>
        /// Get longBreakTime in pomodoro timer
        /// </summary>
        public double LongBreakTime
        {
            get { return (_setting != null) ? _setting.LongBreakTime : 160000; }
           
        }
        #endregion

        /// <summary>
        /// Get or set selected task
        /// </summary>
        public Model.ITaskModel SelectedTask
        {
            get{return _selectedTask;}
            set
            {
                _selectedTask=value;
                OnPropertyChanged("SelectedTask");
                OnPropertyChanged("SelectedTaskTitle");
                OnPropertyChanged("SelectedTaskDescription");
                OnPropertyChanged("SelectedTaskPriorytiIndex");
                OnPropertyChanged("SelectedTaskPrioryti");
                OnPropertyChanged("SelectedTaskPomodorosSpent");
                OnPropertyChanged("SelectedTaskState");
            }
        }

        #region // Selected task properties
        /// <summary>
        /// Get or set selected task title
        /// </summary>
        public String SelectedTaskTitle 
        {
            get
            {
                return (_selectedTask == null) ? " " : _selectedTask.Title;
            }
            set {
                    _selectedTask.Title = value;
                    OnPropertyChanged("SelectedTask");
                }
        }

        /// <summary>
        ///  Get or set selected task description
        /// </summary>
        public String SelectedTaskDescription
        {
            get {  return (_selectedTask == null) ? " " : _selectedTask.Description; }
            set
            {
                _selectedTask.Description = value;
                OnPropertyChanged("SelectedTaskDescription");
            }
        }

        /// <summary>
        ///  Get selected task priority
        /// </summary>
        public string SelectedTaskPriority 
        {
            get
            { return (_selectedTask == null) ? "" : _selectedTask.Priority.ToString(); }
             private set
            {
                
            }
        }

        /// <summary>
        /// Get or set  time interval spent for selected task
        /// </summary>
        public int SelectedTaskPomodorosSpent 
        {
            get { return (_selectedTask == null) ? 0 : (int)_selectedTask.CountPomodoroUnit; }
            set 
            {
                _selectedTask.CountPomodoroUnit = value;
                OnPropertyChanged("SelectedTask");
            }
        }

        /// <summary>
        /// Get or set selected task state
        /// </summary>
        public bool SelectedTaskState 
        {
            get
            { return (_selectedTask == null) ? false : (_selectedTask.State == Model.State.done) ? true : false;}
            set
            {
                if (value) _selectedTask.State = Model.State.done;
                else _selectedTask.State = Model.State.open;
                OnPropertyChanged("SelectedTask");
            }
        }

        // <summary>
        /// Get or set index of selected task
        /// </summary>
        public int SelectedTaskIndex 
        {
            get { return (_selectedTask == null) ? 0 : _selectedTaskIndex; }
            set 
            {
                _selectedTaskIndex = value;
                OnPropertyChanged("SelectedTaskIndex");
            }
        }
        #endregion

        /// <summary>
        /// Get or set current task
        /// </summary>
        public Model.ITaskModel CurrentTask
        {
            get { return _currentTask; }
            set
            {
                _currentTask = value;
                OnPropertyChanged("CurrentTask");
                OnPropertyChanged("CurrentTaskTitle");
                OnPropertyChanged("CurrentTaskTaskDescription");
                OnPropertyChanged("CurrentTaskPomodorosSpent");
            }
        }

        #region //Current task properties
        /// <summary>
        /// Get or set current task title
        /// </summary>
        public String CurrentTaskTitle
        {
            get
            {
                return (_currentTask == null) ? " " : _currentTask.Title;
            }
            set
            {
                _currentTask.Title = value;
                OnPropertyChanged("CurrentTask");
                OnPropertyChanged("CurrentTaskTaskDescription");
                OnPropertyChanged("CurrentTaskPomodorosSpent");
            }
        }
        /// <summary>
        /// Get or set current task description
        /// </summary>
        public String CurrentTaskTaskDescription
        {
            get { return (_currentTask == null) ? " " : _currentTask.Description; }
            set
            {
                _currentTask.Description = value;
                OnPropertyChanged("CurrentTaskTaskDescription");
            }
        }
        /// <summary>
        /// Get or set timer interval spent for current task
        /// </summary>
        public string CurrentTaskPomodorosSpent
        {
            get { return (_currentTask == null) ? "" : _currentTask.CountPomodoroUnit.ToString(); }
            set
            {
                _currentTask.CountPomodoroUnit = Convert.ToInt32(value);
                OnPropertyChanged("CurrentTaskPomodorosSpent");
            }
        }
        /// <summary>
        /// Get or set current task state
        /// </summary>
        public bool CurrentTaskState 
        {
            get { return (_currentTask == null) ? false : (_currentTask.State==Model.State.done)?true:false; }
            set 
            { 
                if(_currentTask!=null) _currentTask.State = (value == true) ? Model.State.done : Model.State.open;
                OnPropertyChanged("CurrentTaskState");
            }
        }

        #endregion

        

        public HomeViewModel() 
        {
            ToDayTasks = _taskRepository.GetTasksByDate(DateTime.Now);
            _setting = _settingRepository.GetPomodoroTMSettings();
            SelectedTask = ToDayTasks.FirstOrDefault<Model.ITaskModel>();
            _dateChangeNotifer.DayChanged += onDateChange;
        }

        public void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
                
            }
        }

        #region//Set current task 
        private ICommand _setCurrentTask
        {
            get { return new RelayCommand(x => ExecuteSetCurrentTask()); }

        }

        public ICommand SetCurrentTask
        {
            get { return _setCurrentTask; }
            set { }
        }

        private void ExecuteSetCurrentTask()
        {
            CurrentTask = SelectedTask;
            RunTimer = false;
        }
        #endregion

        #region//Move next task
        private ICommand _moveNextTask
        {
            get { return new RelayCommand(x => ExecuteMoveNextTask()); }
        }

        public ICommand MoveNextTask
        {
            get { return _moveNextTask; }
            set { }
        }

        private void ExecuteMoveNextTask()
        {
            if ((_toDayTasks.Count<Model.ITaskModel>()-1) > SelectedTaskIndex) SelectedTaskIndex++;
        }
        #endregion

        #region //Move previos task
        private ICommand _movePreviosTask
        {
            get { return new RelayCommand(x => ExecuteMovePreviosTask()); }

        }

        public ICommand MovePreviosTask
        {
            get { return _movePreviosTask; }
            set { }
        }

        private void ExecuteMovePreviosTask()
        {
            if (SelectedTaskIndex != 0) SelectedTaskIndex--;
        }
        #endregion

        #region//Click on curerent control button
        private ICommand _clickCurrentTaskDone
        {
            get { return new RelayCommand(x => ExecuteClickCurrentTaskDone()); }
        }

        public ICommand ClickCurrentTaskDone
        {
            get { return _clickCurrentTaskDone; }
            set { }
        }

        private void ExecuteClickCurrentTaskDone()
        {
            if(_currentTask==null)return;
            _currentTask.State=Model.State.done;
            // reload tasks list from repository after change current task state 
            ToDayTasks = _taskRepository.GetTasksByDate(DateTime.Now);
            OnPropertyChanged("SelectedTaskState");
        }
        #endregion

        #region//End of time work 
        private ICommand _workTimeOverCommand
        {
            get { return new RelayCommand(x => ExecuteWorkTimeOver()); }
        }

        public ICommand WorkTimeOverCommand
        {
            get { return _workTimeOverCommand; }
            set { }
        }

        private void ExecuteWorkTimeOver()
        {
            if (_currentTask == null) return;
            _currentTask.CountPomodoroUnit += 1;
            OnPropertyChanged("CurrentTaskPomodorosSpent");
            OnPropertyChanged("SelectedTaskPomodorosSpent");
            if (_setting.IsTurnOffMonitor) Controllers.MonitorController.OffMonitor();
        }
        #endregion

        #region //End of time short break 
        private ICommand _shortBreakTimeOverCommand
        {
            get { return new RelayCommand(x => ExecuteShortBreakTimeOver()); }
        }

        public ICommand ShortBreakTimeOverCommand
        {
            get { return _shortBreakTimeOverCommand; }
            set { }
        }

        private void ExecuteShortBreakTimeOver()
        {
            if (_setting.IsTurnOffMonitor) Controllers.MonitorController.OnMonitor();
        }
        #endregion

        #region //End of time long break
        private ICommand _longBreakTimeOverCommand
        {
            get { return new RelayCommand(x => ExecuteLongBreakTimeOver()); }
        }

        public ICommand LongBreakTimeOverCommand
        {
            get { return _longBreakTimeOverCommand; }
            set { }
        }

        private void ExecuteLongBreakTimeOver()
        {
            
        }
        #endregion

        private void onDateChange(object sender, DayChangedEventArgs arg)
        {
            OnPropertyChanged("TodayDayName");
            OnPropertyChanged("TodayStr");
        }

    }
}
