using FirstFloor.ModernUI.Presentation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Input;

namespace Pomodoro.Content
{   
    class AddTaskViewModel:INotifyPropertyChanged
    {
        
        public event PropertyChangedEventHandler PropertyChanged;
        Model.TaskModel _task;
        Repository.ITaskRepository _repository = Repository.TaskRepository.TaskRepositoryInstance;

        public DateTime ExDateTime 
        {
            get { return  _task.ExecutionDate; }
            set 
            {
                _task.ExecutionDate = value;
                OnPropertyChanged("ExDateTime");
            }
        }

        public string Title 
        {
            get { return _task.Title; } 
            set 
            { 
                _task.Title = value;
                OnPropertyChanged("Title");
            } 
        }

        public string Description 
        { 
            get { return _task.Description; } 
            set 
            {
                _task.Description = value;
                OnPropertyChanged("Description");
            } 
        }

        public int SelectedTaskPriorytiIndex
        {
            get
            { return (_task == null) ? 0 : (int)_task.Priority; }
            set
            {
                _task.Priority = (Model.Priority)value;
                OnPropertyChanged("SelectedTask");
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

        public AddTaskViewModel()
        {
            _task = new Model.TaskModel("title",DateTime.Now);
        }

        public void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));

            }
        }

        private ICommand _saveTask
        {
            get { return new RelayCommand(x => ExecuteSetCurrentTask()); }

        }

        public ICommand SaveTask
        {
            get { return _saveTask; }
            set { }
        }

        private void ExecuteSetCurrentTask()
        {
            _repository.AddTask(_task);
            if (_repository.Commit()) Controllers.NavigationController.GoTasklist();

                //ToDo make modern ui messageBox
            else MessageBox.Show("Error! Failed to save tasks!");
        }
    }
}
