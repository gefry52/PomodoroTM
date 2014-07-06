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
                   "LongBreakTimeTime",
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


        public double WorkTime
        {
            get { return (double)GetValue(WorkTimeProperty); }
            set { this.SetValue(WorkTimeProperty, value); }
        }
        public double ShortBreakTime 
        {
            get { return (double)GetValue(ShortBreakTimeProperty); }
            set { this.SetValue(ShortBreakTimeProperty, value); }
        }
        
        public double LongBreakTime
        {
            get { return (double)GetValue(LongBreakTimeProperty); }
            set { this.SetValue(LongBreakTimeProperty, value); }
        }
        
        public bool Run 
        {
            get { return (bool)GetValue(RunProperty); }
            set { this.SetValue(RunProperty, value); }
        }


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
        }

        private void onTimerTick(object sender, CountdownTimer.CountdownTimerEventArgs args) 
        {
            _remainingTimeStr = ((int)(args.RemainingTime / 1000) / 60).ToString("D2") + ":" + ((int)(args.RemainingTime / 1000) % 60).ToString("D2");
            TimeIndication.Dispatcher.Invoke(DispatcherPriority.Background,new Action(()=> TimeIndication.Content = _remainingTimeStr));
            
        }

        private void onTimerOver(object sender, CountdownTimer.CountdownTimerEventArgs args)
        {
            Run = false;
       }


        public PomodoroTimerControl()
        {
            InitializeComponent();
            
            
            _cTimer.CountdownTimerTick += onTimerTick;
            _cTimer.CountdownTimeOver += onTimerOver;
            Run = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _cTimer.Enabled = false;
            _cTimer.CountTime = WorkTime;
            _cTimer.Start();
            Run = true;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            _cTimer.CountTime = ShortBreakTime;
            _cTimer.Start();
            Run = true;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            _cTimer.CountTime = LongBreakTime;
            _cTimer.Start();
            Run = true;
        }
    }
}
