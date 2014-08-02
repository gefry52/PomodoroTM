using FirstFloor.ModernUI.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CountdownTimer;
using System.Windows.Input;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace Pomodoro.Pages
{
    class HomeViewModel: INotifyPropertyChanged
    
    {    
        

        public event PropertyChangedEventHandler PropertyChanged;

        private Repository.ITaskRepository _taskRepository = Repository.TaskRepository.TaskRepositoryInstance;
        private Repository.IPomodoroTMSettingsRepository _settingRepository = Repository.PomodoroTMSettingsRepository.SettingInstance;
        private Model.IPomodoroTMSettingsModel _setting;


        private Model.ITaskModel _selectedTask;
        private Model.ITaskModel _currentTask;
        private IQueryable<Model.ITaskModel> _toDayTasks;
        private bool _runTimer = false;
        private int _selectedTaskIndex;

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
                OnPropertyChanged("SelectedTaskPomodorosSpent");
                OnPropertyChanged("SelectedTaskState");
            }
        }

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

        public String SelectedTaskDescription
        {
            get {  return (_selectedTask == null) ? " " : _selectedTask.Description; }
            set
            {
                _selectedTask.Description = value;
                OnPropertyChanged("SelectedTask");
            }
        }

        public IQueryable<Model.ITaskModel> ToDayTasks 
        {
            get { return _toDayTasks; }
            set 
            { 
                _toDayTasks = value;
                OnPropertyChanged("ToDayTasks");
            }
        }

        public Model.ITaskModel CurrentTask 
        {
            get { return _currentTask; }
            set 
            { 
                _currentTask = value;
                OnPropertyChanged("CurrentTask");
            }
        }

        public bool RunTimer 
        {
            get { return _runTimer; }
            set 
            {
                _runTimer = value;
                OnPropertyChanged("RunTimer");
            }
        }

        public double WorkTime 
        {
            get { return (_setting != null) ? _setting.WorkTime : 160000; }
            
        }

        public double ShortBreakTime 
        {
            get { return (_setting != null) ? _setting.ShortBreakTime : 160000; }
        }

        public double LongBreakTime
        {
            get { return (_setting != null) ? _setting.LongBreakTime : 160000; }
           
        }

      
        //
        public ObservableCollection<string> TaskPriorytiItem 
        {
            get 
            {
                ObservableCollection<string> _collection = new ObservableCollection<string>();
               foreach( var str in Enum.GetValues(typeof(Model.Priority)))
                {
                    _collection.Add(str.ToString());
                }
               return _collection; 
            }
            set {}
        }
        // 


        public int SelectedTaskPriorytiIndex 
        {
            get
            { return (_selectedTask == null) ? 0 : (int)_selectedTask.Priority;  }
            set 
            { 
                _selectedTask.Priority = (Model.Priority)value;
                OnPropertyChanged("SelectedTask"); 
            }
        }

        public int SelectedTaskPomodorosSpent 
        {
            get { return (_selectedTask == null) ? 0 : (int)_selectedTask.CountPomodoroUnit; }
            set 
            {
                _selectedTask.CountPomodoroUnit = value;
                OnPropertyChanged("SelectedTask");
            }
        }

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

        public int SelectedTaskIndex 
        {
            get { return (_selectedTask == null) ? 0 : _selectedTaskIndex; }
            set 
            {
                _selectedTaskIndex = value;
                OnPropertyChanged("SelectedTaskIndex");
            }
        }

        public HomeViewModel() 
        {
            ToDayTasks = _taskRepository.GetTaskByDate(DateTime.Now);
            _setting = _settingRepository.GetPomodoroTMSettings();
            SelectedTask = ToDayTasks.FirstOrDefault<Model.ITaskModel>();
        }

        public void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
                
            }
        }


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
        
        // <summary>
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
            SelectedTaskIndex++;
        }
        
        // <asd>
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
            SelectedTaskIndex--;
        }

    }
}
