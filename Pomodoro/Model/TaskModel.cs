using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pomodoro.Model
{
    public class TaskModel:ITaskModel
    {
        private String _id;
        private State _state;
        private String _title;
        private String _description;
        private Priority _priority;
        private Int32 _countPomodoroUnit;
        private DateTime _openingDate;
        private DateTime _closingDate;
        private ITaskStyle _style;
         

        
        
        public TaskModel(string title,Priority priority = Priority.normal, string description="You forgot to add the description")
        {
            _id = Guid.NewGuid().ToString();
            _title = title;
            _state = State.done;
            _countPomodoroUnit = 4;
            _openingDate = DateTime.Now;
            _description = description;
            _priority = priority;
        }

        public String Id 
        {
            get { return _id; }
        }

        public State State 
        {
            get { return _state; }
            set { _state = value; }
        }
        public string Title 
        { 
            get { return _title; }
            set { _title = value;}
        }
        public string Description 
        {
            get { return _description; }
            set { _description = value; }
        }
        public Priority Priority 
        {
            get { return _priority; } 
            set { _priority = value; } 
        }
        public int CountPomodoroUnit 
        {
            get { return _countPomodoroUnit; }
            set { _countPomodoroUnit = value; }
        }
        public DateTime OpeningDate 
        { 
            get { return _openingDate; }
            set { _openingDate = value; }
        }
        public DateTime ClosingDate
        {
            get{return _closingDate;}
            set { _closingDate = value; }
        }
        public ITaskStyle Style 
        { 
            get { return _style; }
            set { _style = value; }
        }
        
    }
}
