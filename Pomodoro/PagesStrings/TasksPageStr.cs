using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Pomodoro.PagesStrings
{
    class TasksPageStr: INotifyPropertyChanged
    {
        private ObservableCollection<string> _priorityColl = new ObservableCollection<string>(); 

        public event PropertyChangedEventHandler PropertyChanged;

        Controllers.CultureUIController _cultureUIController = Controllers.CultureUIController.Instatnce;

        public string RegistrationDateStr { get { return Resources.UIStrings.RegistrationDateStr; } }

        public string TitleStr { get { return Resources.UIStrings.TaskTitleStr; } }

        public string DescriptionStr { get { return Resources.UIStrings.TaskDescriptionStr; } }

        public string Priority { get { return Resources.UIStrings.TaskPriorityStr; } }

        public string StatusStr { get { return Resources.UIStrings.TaskStatusStr; } }

        public string ExecutionDateStr { get { return Resources.UIStrings.ExecutionDateStr; } }

        public string PomodoroSpentStr { get { return Resources.UIStrings.TaskSpentPomodorosStr; } }

        public string DeleteStr { get { return Resources.UIStrings.DeleteStr; } }

        public string EditStr { get { return Resources.UIStrings.EditStr; } }

        public string SaveStr { get { return Resources.UIStrings.SaveStr; } }

        public string CancelStr { get { return Resources.UIStrings.CancelStr; } }

        public string IsDone { get { return Resources.UIStrings.IsDoneStr; } }

        public string ExDateNewTaskStr { get { return Resources.UIStrings.ExNewDateStr; } }

        public string[] PriorityesCollectionStr 
        {
            get 
            { 
                _priorityColl.Clear();
                _priorityColl.Add(Resources.UIStrings.PriorityUrgentStr);
                _priorityColl.Add(Resources.UIStrings.PriorityHighStr);
                _priorityColl.Add(Resources.UIStrings.PriorityNormalStr);
                _priorityColl.Add(Resources.UIStrings.PriorityLowStr);
                return _priorityColl.ToArray(); 
            } 
        }

        public TasksPageStr() 
        {
            _cultureUIController.CultureChanged += OnCultureChanged;
        }

        public void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        private void OnCultureChanged(object sender, Controllers.CultureEventArgs e) 
        {
            foreach (var propperties in typeof(TasksPageStr).GetProperties()) 
            {
                OnPropertyChanged(propperties.Name.ToString());
            }
        }
    }
}
