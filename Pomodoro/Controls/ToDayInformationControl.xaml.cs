using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Pomodoro.Controls
{
    /// <summary>
    /// Логика взаимодействия для ToDayInformationControl.xaml
    /// </summary>
    public partial class ToDayInformationControl : UserControl
    {
        

        public ToDayInformationControl()
        {
            InitializeComponent();
            DayChangedNotifier.DayChanged += OnDateChanged;
            this.WeekDayName.Content = DateTime.Now.DayOfWeek.ToString();
            this.DateLabel.Content = DateTime.Now.ToString("dd  MMMM  yyyy");
            
        }

        private void OnDateChanged(object sender, DayChangedEventArgs args) 
        {
            WeekDayName.Dispatcher.Invoke(DispatcherPriority.Background, new Action(() => WeekDayName.Content = DateTime.Now.DayOfWeek.ToString()));
            this.DateLabel.Dispatcher.Invoke(DispatcherPriority.Background, new Action(() => DateLabel.Content = DateTime.Now.ToString("dd  MMMM  yyyy")));
            
        } 
    }
    // ToDo Refactor this class 
    public static class DayChangedNotifier
    {
        private static Timer timer;

        static DayChangedNotifier()
        {
            timer = new Timer(GetSleepTime());
            timer.Elapsed += (o, e) =>
            {
                OnDayChanged(DateTime.Now.DayOfWeek);
                timer.Interval = GetSleepTime();
            };
            timer.Start();

            SystemEvents.TimeChanged += new EventHandler(SystemEvents_TimeChanged);
        }

        private static void SystemEvents_TimeChanged(object sender, EventArgs e)
        {
            timer.Interval = GetSleepTime();
            OnDayChanged(DateTime.Now.DayOfWeek);
        }

        private static double GetSleepTime()
        {
            var midnightTonight = DateTime.Today.AddDays(1);
            var differenceInMilliseconds = (midnightTonight - DateTime.Now).TotalMilliseconds;
            return differenceInMilliseconds;
        }

        private static void OnDayChanged(DayOfWeek day)
        {
            var handler = DayChanged;
            if (handler != null)
            {
                handler(null, new DayChangedEventArgs(day));
            }
        }

        public static event EventHandler<DayChangedEventArgs> DayChanged;
    }

    public class DayChangedEventArgs : EventArgs
    {
        public DayChangedEventArgs(DayOfWeek day)
        {
            this.DayOfWeek = day;
        }

        public DayOfWeek DayOfWeek { get; private set; }
    }
}
