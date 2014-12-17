using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;

namespace Pomodoro.PagesStrings
{
    class HomePageStr: INotifyPropertyChanged
    {
        private Controllers.CultureUIController _cultureUIController = Controllers.CultureUIController.Instatnce;

        public event PropertyChangedEventHandler PropertyChanged;

        #region //UI strings
        //
        public string TodayDayName
        {
            get
            {
                return Thread.CurrentThread.CurrentCulture.DateTimeFormat.GetDayName(DateTime.Today.DayOfWeek);
            }
            set { }
        }

        public string TodayStr
        {
            get { return String.Concat(DateTime.Today.Date.Day.ToString(), " ", Thread.CurrentThread.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Today.Month), " ", DateTime.Today.Year.ToString()); }
            set { }
        }

        public string TaskTitleStr
        {
            get { return Resources.UIStrings.TaskTitleStr; }
        }

        public string TaskDescriptionStr
        {
            get { return Resources.UIStrings.TaskDescriptionStr; }
        }

        public string TaskStatusStr
        {
            get { return Resources.UIStrings.TaskStatusStr; }
        }

        public string TaskStateStr
        {
            get { return Resources.UIStrings.TaskStateStr; }
        }

        public string TaskPriorityStr
        {
            get { return Resources.UIStrings.TaskPriorityStr; }
        }

        public string TaskSpentPomodorosStr
        {
            get { return Resources.UIStrings.TaskSpentPomodorosStr; }
        }

        public string PreviosStr
        {
            get { return Resources.UIStrings.PreviosStr; }
        }

        public string CurrentSetStr
        {
            get { return Resources.UIStrings.CurrentSetStr; }
        }

        public string NextStr
        {
            get { return Resources.UIStrings.NextStr; }
        }

        public string StartStr
        {
            get { return Resources.UIStrings.StartStr; }
        }

        public string ShortBreakStr
        {
            get { return Resources.UIStrings.ShortBreakStr; }
        }

        public string LongBreakStr
        {
            get { return Resources.UIStrings.LongBreakStr; }
        }

        public string CurrentTaskStr
        {
            get { return Resources.UIStrings.CurrentTaskStr; }
        }

        public string DoneStr
        {
            get { return Resources.UIStrings.DoneStr; }
        }
        //

        #endregion


        public HomePageStr() 
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

        private void OnCultureChanged (object sender, Controllers.CultureEventArgs e)
        {
            foreach (var propperties in typeof(HomePageStr).GetProperties())
            {
                OnPropertyChanged(propperties.Name.ToString());
            }
        }
    }
}
