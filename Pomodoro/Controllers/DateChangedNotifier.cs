using System;
using System.Collections.Generic;


namespace Pomodoro
{
   class DateChangedNotifier
    {
        private  System.Timers.Timer _timer;

        public DateChangedNotifier()
        {
            _timer = new System.Timers.Timer(GetSleepTime());
            _timer.Elapsed += (o, e) =>
            {
                OnDayChanged(DateTime.Now.DayOfWeek);
                _timer.Interval = GetSleepTime();
            };
            _timer.Start();
            Microsoft.Win32.SystemEvents.TimeChanged += new EventHandler(SystemEvents_TimeChanged);
        }



        private  void SystemEvents_TimeChanged(object sender, EventArgs e)
        {
            _timer.Interval = GetSleepTime();
            OnDayChanged(DateTime.Now.DayOfWeek);
        }

        private  double GetSleepTime()
        {
            var midnightTonight = DateTime.Today.AddDays(1);
            var differenceInMilliseconds = (midnightTonight - DateTime.Now).TotalMilliseconds;
            return differenceInMilliseconds;
        }

        private  void OnDayChanged(DayOfWeek day)
        {
            var handler = DayChanged;
            if (handler != null)
            {
                handler(null, new DayChangedEventArgs(day));
            }
        }

        public  event EventHandler<DayChangedEventArgs> DayChanged;
    }

     class DayChangedEventArgs : EventArgs
    {
        public DayChangedEventArgs(DayOfWeek day)
        {
            this.DayOfWeek = day;
        }

        public DayOfWeek DayOfWeek { get; private set; }
    }

}
