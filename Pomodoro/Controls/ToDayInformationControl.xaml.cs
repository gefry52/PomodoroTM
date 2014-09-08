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
        private DayChangedNotifier _dayChangeNotifer = new DayChangedNotifier();

        #region //routed event
        public static readonly RoutedEvent DateChangedEvent = EventManager.RegisterRoutedEvent(
                "DateChanged",
                RoutingStrategy.Bubble,
                typeof(RoutedEventHandler),
                typeof(CurrentTaskControl)
            );
        #endregion

        public event RoutedEventHandler ClickDone
        {
            add { base.AddHandler(DateChangedEvent,value); }
            remove { base.RemoveHandler(DateChangedEvent,value); }
        }


        public ToDayInformationControl()
        {
            InitializeComponent();
            _dayChangeNotifer.DayChanged += OnDateChanged;
            this.WeekDayName.Content = DateTime.Now.DayOfWeek.ToString();
            this.DateLabel.Content = DateTime.Now.ToString("dd  MMMM  yyyy");
        }

        private void OnDateChanged(object sender, DayChangedEventArgs args) 
        {
            WeekDayName.Dispatcher.Invoke(DispatcherPriority.Background, 
                new Action(() => WeekDayName.Content = DateTime.Now.DayOfWeek.ToString())
                );
            this.DateLabel.Dispatcher.Invoke(DispatcherPriority.Background,
                new Action(() => DateLabel.Content = DateTime.Now.ToString("dd  MMMM  yyyy"))
                );
        } 
    }


    // ToDo Refactor this class 
    #region //DayChangedNotifier
    public  class DayChangedNotifier
    {
        private  Timer _timer;

        public DayChangedNotifier()
        {
            _timer = new Timer(GetSleepTime());
            _timer.Elapsed += (o, e) =>
            {
                OnDayChanged(DateTime.Now.DayOfWeek);
                _timer.Interval = GetSleepTime();
            };
            _timer.Start();
            SystemEvents.TimeChanged += new EventHandler(SystemEvents_TimeChanged);
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

    public class DayChangedEventArgs : EventArgs
    {
        public DayChangedEventArgs(DayOfWeek day)
        {
            this.DayOfWeek = day;
        }

        public DayOfWeek DayOfWeek { get; private set; }
    }
    #endregion
}
