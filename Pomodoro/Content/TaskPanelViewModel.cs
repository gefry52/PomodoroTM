using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Pomodoro.Content
{


    class TaskPanelViewModel : INotifyPropertyChanged
    {
        
        public event PropertyChangedEventHandler PropertyChanged;

        private Model.ITaskModel _task;
        private Model.ITaskStyle _style;
        private bool _isEditable;

       
        public TaskPanelViewModel(Model.ITaskModel task, bool isEditable = false ) 
        {
            _task = task;
            _isEditable = isEditable;
        }

        public string State
        {
            get { return _task.State.ToString(); }
            set { _task.State = Model.State.open; }
        }
        public bool IsDone 
        {
            get
            {
                if (_task.State == Model.State.open) return false;
                else return true;
            }
            set 
            {
                if (value) _task.State = Model.State.done;
                else _task.State = Model.State.open;
            }
        }
        public string Title
        {
            get { return _task.Title;  }
            set { _task.Title = value; }
        }
        public string Description
        {
            get { return _task.Description; }
            set { _task.Description = value; }
        }
        public Model.Priority Priority
        {
            get { return _task.Priority; }
            private set { _task.Priority = value; }
        }
        public int PriorityIndex
        {
            get { return (int)_task.Priority; }
            set { _task.Priority = (Model.Priority)value; }
        
        }
        public int CountPomodoroUnit
        {
            get { return _task.CountPomodoroUnit; }
            private set { _task.CountPomodoroUnit = value; }
        }
        public DateTime OpeningDate
        {
            get { return _task.OpeningDate; }
            private set { _task.OpeningDate = value; }
        }
        public DateTime ClosingDate
        {
            get { return _task.ClosingDate; }
            private set { _task.ClosingDate = value; }
        }
        public Model.ITaskStyle Style
        {
            get { return _task.Style; }
            private set { _task.Style = value; }
        }
        
        public bool IsEditable 
        {
            get { return _isEditable;}
                
        }
        public string[] PriorityItem 
        {
            get { return new string[]{Model.Priority.high.ToString(),Model.Priority.low.ToString(),Model.Priority.normal.ToString(),Model.Priority.urgent.ToString()};}
        }

        public Model.ITaskModel Task 
        { 
            get { return _task; }
            set 
            { 
                _task = value;
                changeTaskProperty();
            }
        }

        private void changeTaskProperty()
        {
            string[] property = new string[] { "Task", "Title", "Description", "Priority", "CountPomodoroUnit", "OpeningDate", "ClosingDate", "Style", "PriorityIndex","IsDone" };
            foreach (string pr in property) { OnPropertyChanged(pr);}
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
