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

        private Model.ITaskModel _selectedTask;
        private Model.ITaskModel _currentTask;
        private IQueryable<Model.ITaskModel> _toDayTasks;
        private bool _runTimer = false;
        

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
            get { return _selectedTask.Title; }
            set {
                    _selectedTask.Title = value;
                    OnPropertyChanged("SelectedTask");
                }
        }

        public String SelectedTaskDescription
        {
            get { return _selectedTask.Description; }
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

        /// <summary>
        /// 
        /// </summary>
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
            get { return (int)_selectedTask.Priority; }
            set 
            { 
                _selectedTask.Priority = (Model.Priority)value;
                OnPropertyChanged("SelectedTask"); 
            }
        }

        public int SelectedTaskPomodorosSpent 
        {
            get { return _selectedTask.CountPomodoroUnit; }
            set 
            {
                _selectedTask.CountPomodoroUnit = value;
                OnPropertyChanged("SelectedTask");
            }
        }

        public bool SelectedTaskState 
        {
            get
            {
                if (_selectedTask.State == Model.State.done) return true;
                else return false;
            }
            set
            {
                if (value) _selectedTask.State = Model.State.done;
                else _selectedTask.State = Model.State.open;
                OnPropertyChanged("SelectedTask");
            }
        }



        public HomeViewModel() 
        {
            //+++++++++++++++++
            Repository.ITaskRepository rep = new Repository.TaskRepository();
            ToDayTasks = rep.GetTaskByDate(DateTime.Now);
            //++++++++++++++++++
            SelectedTask = ToDayTasks.First<Model.ITaskModel>();
        }

        public void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
                
            }
        }

        
        private ICommand _setSelectedTask
        {
            get { return new RelayCommand(x => ExecuteSetSelectedTask()); }

        }

        public ICommand SetCurrentTask
        {
            get { return _setSelectedTask; }
            set { }
        }

        private void ExecuteSetSelectedTask()
        {
            CurrentTask = SelectedTask;
            RunTimer = false;
        }

    }
}
