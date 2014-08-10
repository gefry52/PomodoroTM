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
        //
        public event PropertyChangedEventHandler PropertyChanged;

        private Repository.ITaskRepository _repository = Repository.TaskRepository.TaskRepositoryInstance;

        private Controllers.INavigationController _navigationController = Controllers.NavigationController.Instatnce;

        private IQueryable<Model.ITaskModel> _tasks;

        private Model.ITaskModel _selectedTask;

        //
        public Model.ITaskModel SelectedTask 
        {
            get { return _selectedTask; }
            set 
            {
                _selectedTask = value;
                OnPropertyChanged("SelectedTask");
            }

        }

        public ObservableCollection<Model.ITaskModel> Tasks
        {
            get { return ToObservableCollection(_tasks); }
            set
            {
               _tasks = value.AsQueryable<Model.ITaskModel>();
                OnPropertyChanged("Tasks");
            }
        }

        public TaskListViewModel() 
        {
            
            Tasks = ToObservableCollection(_repository.GetTasks().AsQueryable<Model.ITaskModel>());
        }

        private ObservableCollection<Model.ITaskModel> ToObservableCollection(IQueryable<Model.ITaskModel> collection) 
        {
            return new ObservableCollection<Model.ITaskModel>(collection);
        }

        public void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));

            }
        }

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
            Tasks = new ObservableCollection<Model.ITaskModel>(_repository.GetTasks());
        }

        private ICommand _editeTask
        {
            get { return new RelayCommand(x => ExecuteEditeTask()); }
        }

        public ICommand EditeTask
        {
            get { return _editeTask; }
            private set { }
        }

        private void ExecuteEditeTask()
        {
            if (_selectedTask == null) return;
            _navigationController.GoEditTask(_selectedTask.Id);
        }
        //
    }
}
