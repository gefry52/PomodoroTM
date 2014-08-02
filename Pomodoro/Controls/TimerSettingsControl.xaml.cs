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

namespace Pomodoro.Controls
{
    /// <summary>
    /// Логика взаимодействия для TimerSettingsControl.xaml
    /// </summary>
    public partial class TimerSettingsControl : UserControl
    {
        
            public static readonly DependencyProperty ShortBreakTimeProperty = DependencyProperty.Register(
                       "ShortBreakTime",
                       typeof(double),
                       typeof(TimerSettingsControl),
                       new PropertyMetadata((double)300000, new PropertyChangedCallback(OnChangeShortBreakTime))
                       );

            public static readonly DependencyProperty LongBreakTimeProperty = DependencyProperty.Register(
                        "LongBreakTime",
                        typeof(double),
                        typeof(TimerSettingsControl),
                        new PropertyMetadata((double)1800000, new PropertyChangedCallback(OnLongBreakTime))
                        );


            public static readonly DependencyProperty ShortBreakTimeMaximumProperty = DependencyProperty.Register(
                         "ShortBreakTimeMaximum",
                         typeof(double),
                         typeof(TimerSettingsControl),
                         new PropertyMetadata((double)300000, new PropertyChangedCallback(OnChangeShortBreakTimeMaximum))
                         );

            public static readonly DependencyProperty ShortBreakTimeMinimumProperty = DependencyProperty.Register(
                      "ShortBreakTimeMinimum",
                      typeof(double),
                      typeof(TimerSettingsControl),
                      new PropertyMetadata((double)0, new PropertyChangedCallback(OnChangeShortBreakTimeMinimum))
                      );

            public static readonly DependencyProperty LongBreakTimeMaximumProperty = DependencyProperty.Register(
                            "LongBreakTimeMaximum",
                            typeof(double),
                            typeof(TimerSettingsControl),
                            new PropertyMetadata((double)1800000, new PropertyChangedCallback(OnChangeLongBreakTimeMaximum))
                            );

            public static readonly DependencyProperty LongBreakTimeMinimumProperty = DependencyProperty.Register(
                               "LongBreakTimeMinimum",
                               typeof(double),
                               typeof(TimerSettingsControl),
                               new PropertyMetadata((double)0, new PropertyChangedCallback(OnChangeLongBreakTimeMinimum))
                               );

            public static readonly DependencyProperty IsTurnOnSoundProperty = DependencyProperty.Register(
                        "IsTurnOnSound",
                        typeof(bool),
                        typeof(TimerSettingsControl),
                        new PropertyMetadata(false, new PropertyChangedCallback(OnIsTurnOnSound))
                        );

            public static readonly DependencyProperty IsTurnOffMonitorProperty = DependencyProperty.Register(
                        "IsTurnOffMonitor",
                        typeof(bool),
                        typeof(TaskViewControl),
                        new PropertyMetadata(false, new PropertyChangedCallback(OnIsTurnOffMonitor))
                        );

           
            /// 
            public double ShortBreakTime
            {
                get { return (double)GetValue(ShortBreakTimeProperty) ; }
                set { this.SetValue(ShortBreakTimeProperty, value); }
            }

            public double ShortBreakTimeMaximum
            {
                get { return (double)GetValue(ShortBreakTimeMaximumProperty); }
                set { this.SetValue(ShortBreakTimeMaximumProperty, value); }

            }

            public double ShortBreakTimeMinimum
            {
                get { return (double)GetValue(ShortBreakTimeMinimumProperty); }
                set { this.SetValue(ShortBreakTimeMinimumProperty, value); }

            }

            public double LongBreakTime
            {
                get { return (double)GetValue(LongBreakTimeProperty); }
                set { this.SetValue(LongBreakTimeProperty, value); }
            }
            
            public double LongBreakTimeMaximum 
            {
                get { return (double)GetValue(LongBreakTimeMaximumProperty); }
                set { this.SetValue(LongBreakTimeMaximumProperty, value); }

            }

            public double LongBreakTimeMinimum
            {
                get { return (double)GetValue(LongBreakTimeMinimumProperty); }
                set { this.SetValue(LongBreakTimeMinimumProperty, value); }

            }

            public bool IsTurnOnSound
            {
                get { return (bool)GetValue(IsTurnOnSoundProperty); }
                set { this.SetValue(IsTurnOnSoundProperty, value); }
            }

            public bool IsTurnOffMonitor
            {
                get { return (bool)GetValue(IsTurnOffMonitorProperty); }
                set { this.SetValue(IsTurnOffMonitorProperty, value); }
            }
            ///
            public TimerSettingsControl() 
            {
                InitializeComponent();
                this.ShortBreakTimeSlider.Maximum = ShortBreakTimeMaximum;
                this.ShortBreakTimeSlider.Minimum = ShortBreakTimeMinimum;
                this.ShortBreakTimeSlider.Value = ShortBreakTime;
                this.LongBreakTimeSlider.Maximum = LongBreakTimeMaximum;
                this.LongBreakTimeSlider.Minimum = LongBreakTimeMinimum;
                this.LongBreakTimeSlider.Value = LongBreakTime;
                this.TurnMonitorCheckbox.IsChecked = IsTurnOffMonitor;
                this.TurnSoundCheckBox.IsChecked = IsTurnOnSound;
            }
    
            /// 
            private static void OnChangeShortBreakTime(DependencyObject sender, DependencyPropertyChangedEventArgs args)
            {
                TimerSettingsControl _control = (TimerSettingsControl)sender;
                _control.ShortBreakTimeSlider.Value = (double)args.NewValue;
            }

            private static void OnLongBreakTime(DependencyObject sender, DependencyPropertyChangedEventArgs args)
            {
                TimerSettingsControl _control = (TimerSettingsControl)sender;
                _control.LongBreakTimeSlider.Value = (double)args.NewValue;
            }

            private static void OnIsTurnOnSound(DependencyObject sender, DependencyPropertyChangedEventArgs args)
            {
                TimerSettingsControl _control = (TimerSettingsControl)sender;
                _control.TurnSoundCheckBox.IsChecked = (bool)args.NewValue;
            }

            private static void OnIsTurnOffMonitor(DependencyObject sender, DependencyPropertyChangedEventArgs args)
            {
                TimerSettingsControl _control = (TimerSettingsControl)sender;
                _control.TurnMonitorCheckbox.IsChecked = (bool)args.NewValue;
            }

            private static void OnChangeShortBreakTimeMaximum(DependencyObject sender, DependencyPropertyChangedEventArgs args)
            {
                TimerSettingsControl _control = (TimerSettingsControl)sender;
                _control.ShortBreakTimeSlider.Maximum = (double)args.NewValue;
            }

            private static void OnChangeShortBreakTimeMinimum(DependencyObject sender, DependencyPropertyChangedEventArgs args)
            {
                TimerSettingsControl _control = (TimerSettingsControl)sender;
                _control.ShortBreakTimeSlider.Minimum = (double)args.NewValue;
            }

            private static void OnChangeLongBreakTimeMaximum(DependencyObject sender, DependencyPropertyChangedEventArgs args)
            {
                TimerSettingsControl _control = (TimerSettingsControl)sender;
                _control.LongBreakTimeSlider.Maximum = (double)args.NewValue;
            }

            private static void OnChangeLongBreakTimeMinimum(DependencyObject sender, DependencyPropertyChangedEventArgs args)
            {
                TimerSettingsControl _control = (TimerSettingsControl)sender;
                _control.LongBreakTimeSlider.Minimum = (double)args.NewValue;
            }

            private void ShortBreakTimeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
            {
                ShortBreakTime = this.ShortBreakTimeSlider.Value;
                this.ShortBreakTimeLabel.Content = ((int)(ShortBreakTime / 1000) / 60).ToString("D2") + ":" + ((int)(ShortBreakTime / 1000) % 60).ToString("D2")+" min";
            }

            private void LongBreakTimeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
            {
                LongBreakTime = this.LongBreakTimeSlider.Value;
                this.LongBreakTimeLabel.Content = ((int)(LongBreakTime / 1000) / 60).ToString("D2") + ":" + ((int)(LongBreakTime / 1000) % 60).ToString("D2")+" min";
            }

            ///
            




        private void TurnSoundCheckBox_Checked(object sender, RoutedEventArgs e)
        {
           if ((bool)this.TurnSoundCheckBox.IsChecked) IsTurnOnSound = true;
            else IsTurnOnSound = false;
        }

       private void TurnMonitorCheckbox_Checked(object sender, RoutedEventArgs e)
       {
           if ((bool)this.TurnMonitorCheckbox.IsChecked) IsTurnOffMonitor = true;
           else IsTurnOffMonitor = false;
       }
        


       
    }
}
