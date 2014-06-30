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
    
        private Model.ITaskModel _currentTask {get;set;}
        
        

        public Model.ITaskModel CurrentTask
        {
            get{return _currentTask;}
            set
            {
                _currentTask=value;
                OnPropertyChanged("CurrentTask");
                OnPropertyChanged("CurrentTaskTitle");
                OnPropertyChanged("CurrentTaskDescription");
                OnPropertyChanged("CurrentTaskPriorytiIndex");
                OnPropertyChanged("CurrentTaskPomodorosSpent");
                OnPropertyChanged("CurrentTaskState");
            }
        }

        public String CurrentTaskTitle 
        {
            get { return _currentTask.Title; }
            set {
                    _currentTask.Title = value;
                    OnPropertyChanged("CurrentTask");
                }
        }

        public String CurrentTaskDescription
        {
            get { return _currentTask.Description; }
            set
            {
                _currentTask.Description = value;
                OnPropertyChanged("CurrentTask");
            }
        }

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

        public int CurrentTaskPriorytiIndex 
        {
            get { return (int)_currentTask.Priority; }
            set 
            { 
                _currentTask.Priority = (Model.Priority)value; 
                OnPropertyChanged("CurrentTask"); 
            }
        }

        public int CurrentTaskPomodorosSpent 
        {
            get { return _currentTask.CountPomodoroUnit; }
            set 
            {
                _currentTask.CountPomodoroUnit = value;
                OnPropertyChanged("CurrentTask");
            }
        }

        public bool CurrentTaskState 
        {
            get
            {
                if (_currentTask.State == Model.State.done) return true;
                else return false;
            }
            set
            {
                if (value) _currentTask.State = Model.State.done;
                else _currentTask.State = Model.State.open;
                OnPropertyChanged("CurrentTask");
            }
        }

        public HomeViewModel() 
        {
            
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
            int i = 1;
            CurrentTask = new Model.TaskModel("huyna", Model.Priority.low, "asdasd" + i) { CountPomodoroUnit=3,State = Model.State.done};
            i++;
        }

    }
}
