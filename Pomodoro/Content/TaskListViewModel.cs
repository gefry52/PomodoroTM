using FirstFloor.ModernUI.Presentation;
using FirstFloor.ModernUI.Windows.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace Pomodoro.Content
{


    class TaskListViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private Repository.ITaskRepository _repository = Repository.TaskRepository.TaskRepositoryInstance;
        private Controllers.INavigationController _navigationController = Controllers.NavigationController.Instatnce;
        private IList<Model.ITaskModel> _tasks;
        private Model.ITaskModel _selectedTask;

        /// <summary>
        /// Get or set selected task
        /// </summary>
        public Model.ITaskModel SelectedTask 
        {
            get { return _selectedTask; }
            set 
            {
                _selectedTask = value;
                OnPropertyChanged("SelectedTask");
            }

        }
        /// <summary>
        /// Get or set tasks list
        /// </summary>
        public IList<Model.ITaskModel> Tasks
        {
            get { return _tasks; }
            set
            {
               _tasks = value.ToList<Model.ITaskModel>();
                OnPropertyChanged("Tasks");
            }
        }

        public TaskListViewModel() 
        {  
            Tasks = _repository.GetTasks();
        }
        //
        public void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        #region // delete selected task 
        private ICommand _deleteTask
        {
            get { return new RelayCommand(x => ExecuteDeleteTask()); }
        }

        public ICommand DeleteTask 
        {
            get { return _deleteTask; }
            private set { }
        }

        private void ExecuteDeleteTask() 
        {
           if (_selectedTask == null) return;
           _repository.DeleteTask(_selectedTask);
           Tasks = _repository.GetTasks();
           if (Tasks.Count >= 1) SelectedTask = Tasks[0];
        }
#endregion

        #region // got to edit selected task
        private ICommand _editTask
        {
            get { return new RelayCommand(x => ExecuteEditTask()); }
        }

        public ICommand EditTask
        {
            get { return _editTask; }
            private set { }
        }

        private void ExecuteEditTask()
        {
            if (_selectedTask == null) return;
            _navigationController.GoEditTask(_selectedTask.Id);
        }
#endregion
    }
}
