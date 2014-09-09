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
    /// Логика взаимодействия для CurrentTaskControl.xaml
    /// </summary>
    public partial class CurrentTaskControl : UserControl
    {
        #region//dependency property
        public static readonly DependencyProperty TaskTitleProperty = DependencyProperty.Register(
                   "TaskTitle",
                   typeof(string),
                   typeof(CurrentTaskControl),
                   new PropertyMetadata(null, new PropertyChangedCallback(OnChangeTaskTitle))
                   );

        public static readonly DependencyProperty PomodoroSpentProperty = DependencyProperty.Register(
                   "PomodoroSpent",
                   typeof(string),
                   typeof(CurrentTaskControl),
                   new PropertyMetadata("", new PropertyChangedCallback(OnChangePomodoroSpent))
                   );

        public static readonly DependencyProperty StateProperty = DependencyProperty.Register(
                   "State",
                   typeof(bool),
                   typeof(CurrentTaskControl),
                   new PropertyMetadata(false, new PropertyChangedCallback(OnChangeState))
                   );
         
        //
        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register(
                "Command",
                typeof(ICommand),
                typeof(CurrentTaskControl),
                new PropertyMetadata(null, new PropertyChangedCallback(OnChangeCommand))
                );
#endregion

        #region //routed event
        public static readonly RoutedEvent ClickDoneEvent = EventManager.RegisterRoutedEvent(
                "ClickDone",
                RoutingStrategy.Bubble,
                typeof(RoutedEventHandler),
                typeof(CurrentTaskControl)
            );
        #endregion
        
        //
        public string TaskTitle
        {
            get { return (string)GetValue(TaskTitleProperty); }
            set { this.SetValue(TaskTitleProperty, (string)value); }
        }

        //Get or set count pomodoro spent
        public string PomodoroSpent
        {
            get { return (string)GetValue(PomodoroSpentProperty); }
            set { this.SetValue(PomodoroSpentProperty,(string)value); }
        }

        //Get or set State property for task
        public bool State
        {
            get { return (bool)GetValue(StateProperty); }
            set { this.SetValue(StateProperty, value); }
        }

        //Command property 
        public ICommand Command 
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { this.SetValue(CommandProperty, value); }
        }

        //routed event property     
        public event RoutedEventHandler ClickDone 
        {
            add { this.ButtonDone.AddHandler(ClickDoneEvent, value); }
            remove { this.ButtonDone.RemoveHandler(ClickDoneEvent, value); }
        }

        public CurrentTaskControl()
        {
            InitializeComponent();
        }

        private static void OnChangeTaskTitle(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            CurrentTaskControl _control = (CurrentTaskControl)sender;
            _control.Title.Content = _control.TaskTitle; ;
        }

        private static void OnChangePomodoroSpent(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            CurrentTaskControl _control = (CurrentTaskControl)sender;
            _control.PomodorosSpent.Content = _control.PomodoroSpent;
        }

        private static void OnChangeState(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {

        }

        private static void OnChangeCommand(DependencyObject sender, DependencyPropertyChangedEventArgs args) 
        {
            CurrentTaskControl _control = (CurrentTaskControl)sender;
            _control.ButtonDone.Command = _control.Command;
        }
    }
}
