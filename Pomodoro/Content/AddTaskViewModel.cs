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

        Model.ITaskModel _task;
        Repository.ITaskRepository _repository = Repository.TaskRepository.TaskRepositoryInstance;
        Controllers.INavigationController _navigationController = Controllers.NavigationController.Instatnce;

        /// <summary>
        /// Get or set date when must execute the task 
        /// </summary>
        public DateTime ExDateTime 
        {
            get { return  _task.ExecutionDate; }
            set 
            {
                _task.ExecutionDate = value;
                OnPropertyChanged("ExDateTime");
            }
        }

        /// <summary>
        /// Get or set task title 
        /// </summary>
        public string Title 
        {
            get { return _task.Title; } 
            set 
            { 
                _task.Title = value;
                OnPropertyChanged("Title");
            } 
        }

        /// <summary>
        /// Get or set task description 
        /// </summary>
        public string Description 
        { 
            get { return _task.Description; } 
            set 
            {
                _task.Description = value;
                OnPropertyChanged("Description");
            } 
        }

        /// <summary>
        /// Get or set index of selected priority
        /// </summary>
        public int SelectedTaskPriorityIndex
        {
            get
            { return (_task == null) ? 1 : (int)_task.Priority; }
            set
            {
                if (_task == null) return;
                _task.Priority = (Model.Priority)value;
                OnPropertyChanged("SelectedTaskPriorityIndex");  
            }
        }

        /// <summary>
        /// Get collection priorities for task
        /// </summary>
        public ObservableCollection<string> TaskPriorityItem 
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
            private set {}
        }

        //
        public AddTaskViewModel()
        {
           _task=(_navigationController.Parameter != null)?_repository.GetTaskById(_navigationController.Parameter.ToString()):new Model.TaskModel("title",DateTime.Now);
           _navigationController.Parameter = null;
        }

       
        public void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        #region // Save Task and go to the taskList page
        private ICommand _saveTask
        {
            get { return new RelayCommand(x => ExecuteSaveTask()); }
        }

        /// <summary>
        /// Get save task command
        /// </summary>
        public ICommand SaveTask
        {
            get { return _saveTask; }
            set { }
        }

        private void ExecuteSaveTask()
        {
            _repository.AddTask(_task);
            if (_repository.Commit())
            {
                _task = null;
                _navigationController.GoTasklist();
            }
            //ToDo make modern ui messageBox
            else MessageBox.Show("Error! Failed to save tasks!");
        }
        #endregion
    }
}
