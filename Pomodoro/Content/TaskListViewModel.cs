using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Pomodoro.Content
{


    class TaskListViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;


        private IQueryable<Model.ITaskModel> _tasks;
        
        public IQueryable<Model.ITaskModel> Tasks
        {
            get { return _tasks; }
            set
            {
               _tasks = value;
                OnPropertyChanged("Tasks");
            }
        }

        public TaskListViewModel() 
        {
            Repository.ITaskRepository rep = new Repository.TaskRepository();
            Tasks = rep.GetTasks().AsQueryable<Model.ITaskModel>();
        }

        public void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));

            }
        }

    }
}
