using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// Логика взаимодействия для PomodoroTimerControl.xaml
    /// </summary>
    public partial class PomodoroTimerControl : UserControl
    {

        private static CountdownTimer.CountdownTimer _cTimer = new CountdownTimer.CountdownTimer();
        
        private string _remainingTimeStr { get; set; }

        private TimerState _timerState = TimerState.Off;

        #region // Dependency property
        public static readonly DependencyProperty WorkTimeProperty = DependencyProperty.Register(
                   "WorkTime",
                   typeof(double),
                   typeof(PomodoroTimerControl),
                   new PropertyMetadata((double)1500000, new PropertyChangedCallback(OnChangeWorkTime))
                   );

        public static readonly DependencyProperty ShortBreakTimeProperty = DependencyProperty.Register(
                   "ShortBreakTime",
                   typeof(double),
                   typeof(PomodoroTimerControl),
                   new PropertyMetadata((double)300000, new PropertyChangedCallback(OnChangeShortBreakTime))
                   );

        public static readonly DependencyProperty LongBreakTimeProperty = DependencyProperty.Register(
                   "LongBreakTime",
                   typeof(double),
                   typeof(PomodoroTimerControl),
                   new PropertyMetadata((double)1800000, new PropertyChangedCallback(OnChangeLongBreakTime))
                   );

        public static readonly DependencyProperty RunProperty = DependencyProperty.Register(
                   "Run",
                   typeof(bool),
                   typeof(PomodoroTimerControl),
                   new PropertyMetadata(false, new PropertyChangedCallback(OnChangeRun))
                   );
        #endregion

        #region //Routed event
        public static readonly RoutedEvent WorkTimeOverEvent = EventManager.RegisterRoutedEvent(
            "WorkTimeOver",
            RoutingStrategy.Bubble,
            typeof(RoutedEventHandler),
            typeof(PomodoroTimerControl)
            );

        public static readonly RoutedEvent ShortBreakTimeOverEvent = EventManager.RegisterRoutedEvent(
            "ShortBreakTimeOver",
            RoutingStrategy.Bubble,
            typeof(RoutedEventHandler),
            typeof(PomodoroTimerControl)
            );

        public static readonly RoutedEvent LongBreakTimeOverEvent = EventManager.RegisterRoutedEvent(
            "LongBreakTimeOver",
            RoutingStrategy.Bubble,
            typeof(RoutedEventHandler),
            typeof(PomodoroTimerControl)
            );
        #endregion

        #region // Shell for dependency property
        /// <summary>
        /// Get or set work time interval 
        /// </summary>
        public double WorkTime
        {
            get { return (double)GetValue(WorkTimeProperty); }
            set { this.SetValue(WorkTimeProperty, value); }
        }
        /// <summary>
        /// Get or set short break time interval
        /// </summary>
        public double ShortBreakTime 
        {
            get { return (double)GetValue(ShortBreakTimeProperty); }
            set { this.SetValue(ShortBreakTimeProperty, value); }
        }

        /// <summary>
        /// Get or set long break time interval
        /// </summary>
        public double LongBreakTime
        {
            get { return (double)GetValue(LongBreakTimeProperty); }
            set { this.SetValue(LongBreakTimeProperty, value); }
        }

        /// <summary>
        /// Get or set state timer 
        /// </summary>
        public bool Run 
        {
            get { return (bool)GetValue(RunProperty); }
            set { this.SetValue(RunProperty, value); }
        }
        #endregion

        #region // Shell for routed event
        public event RoutedEventHandler WorkTimeOver
        {
            add
            {
                base.AddHandler(WorkTimeOverEvent, value);
            }
            remove
            {
                base.RemoveHandler(WorkTimeOverEvent, value);
            }
        }

        public event RoutedEventHandler ShortBreakTimeOver
        {
            add
            {
                base.AddHandler(ShortBreakTimeOverEvent, value);
            }
            remove
            {
                base.RemoveHandler(ShortBreakTimeOverEvent, value);
            }
        }

        public event RoutedEventHandler LongBreakTimeOver
        {
            add
            {
                base.AddHandler(LongBreakTimeOverEvent, value);
            }
            remove
            {
                base.RemoveHandler(LongBreakTimeOverEvent, value);
            }
        } 

        #endregion

        #region //Dependency property change callback
        private static void OnChangeWorkTime(DependencyObject sender, DependencyPropertyChangedEventArgs args) 
        {

        }

        private static void OnChangeShortBreakTime(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {

        }

        private static void OnChangeLongBreakTime(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {

        }

        private static void OnChangeRun(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            _cTimer.Enabled = (bool)args.NewValue;
            PomodoroTimerControl _control = (PomodoroTimerControl)sender;
            _control.TimeIndication.Content = "00:00";
        }
        #endregion


        private void onTimerTick(object sender, CountdownTimer.CountdownTimerEventArgs args) 
        {
            _remainingTimeStr = ((int)(args.RemainingTime / 1000) / 60).ToString("D2") + ":" + ((int)(args.RemainingTime / 1000) % 60).ToString("D2");
            this.TimeIndication.Dispatcher.Invoke(DispatcherPriority.Background,new Action(()=> this.TimeIndication.Content = _remainingTimeStr));
            
        }

        private void onTimerOver(object sender, CountdownTimer.CountdownTimerEventArgs args)
        {
            switch (_timerState)
            {
                case TimerState.WorkTime: base.Dispatcher.Invoke(DispatcherPriority.Background, new Action(() => ExecuteWorkOver()));
                    break;
                case TimerState.ShortBreakTime: base.Dispatcher.Invoke(DispatcherPriority.Background, new Action(() => ExecuteShotrBreakOver()));
                    break;
                case TimerState.LongBreakTime: base.Dispatcher.Invoke(DispatcherPriority.Background, new Action(() => ExecuteLongBreakOver()));
                    break;
            } 
            this.Dispatcher.Invoke(DispatcherPriority.Background, new Action(() => this._timerState = TimerState.Off));
        }

        #region // ToDo 
        private void ExecuteWorkOver() 
        {
            base.RaiseEvent(new RoutedEventArgs(WorkTimeOverEvent, this));
        }
        private void ExecuteShotrBreakOver() 
        {
            base.RaiseEvent(new RoutedEventArgs(ShortBreakTimeOverEvent, this));
        }

        private void ExecuteLongBreakOver() 
        {
            base.RaiseEvent(new RoutedEventArgs(LongBreakTimeOverEvent, this));
        }
        #endregion

        public PomodoroTimerControl()
        {
            InitializeComponent();
            _cTimer.CountdownTimerTick += onTimerTick;
            _cTimer.CountdownTimeOver += onTimerOver;
         
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _cTimer.Enabled = false;
            _cTimer.CountTime = WorkTime;
            _cTimer.Start();
            _timerState = TimerState.WorkTime;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            _cTimer.Enabled = false;
            _cTimer.CountTime = ShortBreakTime;
            _cTimer.Start();
            _timerState = TimerState.ShortBreakTime;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            _cTimer.Enabled = false;
            _cTimer.CountTime = LongBreakTime;
            _cTimer.Start();

            _timerState = TimerState.LongBreakTime;
        }
    }
  
    enum TimerState {Off,WorkTime,ShortBreakTime,LongBreakTime}
}
