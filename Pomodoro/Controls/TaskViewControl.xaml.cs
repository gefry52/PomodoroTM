using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Логика взаимодействия для TaskViewControl.xaml
    /// </summary>
    public partial class TaskViewControl : UserControl
    {

        public static readonly DependencyProperty TaskTitleProperty = DependencyProperty.Register(
                   "TaskTitle",
                   typeof(string),
                   typeof(TaskViewControl),
                   new PropertyMetadata("Title is empty", new PropertyChangedCallback(OnChangeTitle))
                   );

        public static readonly DependencyProperty TaskDescriptionProperty = DependencyProperty.Register(
                    "TaskDescription",
                    typeof(string),
                    typeof(TaskViewControl),
                    new PropertyMetadata("Title is empty", new PropertyChangedCallback(OnChangeDescription))
                    );

        public static readonly DependencyProperty IsEditableProperty = DependencyProperty.Register(
                    "IsEditable",
                    typeof(bool),
                    typeof(TaskViewControl),
                    new PropertyMetadata(false, new PropertyChangedCallback(OnChangeIsEditable))
                    );

        public static readonly DependencyProperty TaskPomodorosSpentProperty = DependencyProperty.Register(
                    "TaskPomodorosSpent",
                    typeof(string),
                    typeof(TaskViewControl),
                    new PropertyMetadata("0",new PropertyChangedCallback(OnChangePomodorosSpent))
                    );

        public static readonly DependencyProperty TaskPriorytiItemsProperty = DependencyProperty.Register(
                    "TaskPriorytiItems",
                    typeof(ObservableCollection<string>),
                    typeof(TaskViewControl),
                    new PropertyMetadata(null, new PropertyChangedCallback(OnChangePriorytiItems))
                    );

        public static readonly DependencyProperty SelectedPriorytiItemIndexProperty = DependencyProperty.Register(
                    "SelectedPriorytiIndex", 
                    typeof(Int32), 
                    typeof(TaskViewControl),
                    new PropertyMetadata(0, new PropertyChangedCallback(OnChangeSelectedIndex)) 
                    );

        public static readonly DependencyProperty TaskStateProperty = DependencyProperty.Register(
                    "TaskState",
                    typeof(bool),
                    typeof(TaskViewControl),
                    new PropertyMetadata(false, new PropertyChangedCallback(OnChangeState))
                    );

        public String TaskTitle
        {
            get { return GetValue(TaskTitleProperty) as String;}
            set { this.SetValue(TaskTitleProperty, value); }
        }

        public String TaskDescription
        {
            get { return GetValue(TaskDescriptionProperty) as String; }
            set { this.SetValue(TaskDescriptionProperty, value); }
        }

        public bool IsEditable 
        {
            get{ return (bool) GetValue(IsEditableProperty);}
            set { this.SetValue(IsEditableProperty, value); }
        }

        public String TaskPomodorosSpent 
        {
            get { return GetValue(TaskPomodorosSpentProperty) as String; }
            set { this.SetValue(TaskPomodorosSpentProperty, value); }
        }

        public ObservableCollection<string> TaskPriorytiItems 
        {
            get { return GetValue(TaskPriorytiItemsProperty) as ObservableCollection<string>; }
            set { this.SetValue(TaskPriorytiItemsProperty, (ObservableCollection<string>)value); }
        }

        public Int32 SelectedPriorytiIndex 
        {
            get { return (Int32) GetValue(SelectedPriorytiItemIndexProperty); }
            set { this.SetValue(SelectedPriorytiItemIndexProperty, value); }
        }

        public bool TaskState 
        {

            get { return (bool)GetValue(TaskStateProperty); }
            set { this.SetValue(TaskStateProperty, value); }
        } 



        private static void OnChangeTitle(DependencyObject sender, DependencyPropertyChangedEventArgs args) 
        {
            TaskViewControl _control = (TaskViewControl)sender;
            _control.TitleTextBox.Text = (string)args.NewValue;
        }


        private static void OnChangeDescription(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            TaskViewControl _control = (TaskViewControl)sender;
            _control.DescriptionTextBox.Text = (string)args.NewValue;
        }

        private static void OnChangePriorytiItems(DependencyObject sender, DependencyPropertyChangedEventArgs args) 
        {
            TaskViewControl _control = (TaskViewControl)sender;
            foreach (string item in (ObservableCollection<string>)args.NewValue) 
            {
                _control.PriorityComboBox.Items.Add(item);
            }
        }
        private static void OnChangeSelectedIndex(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            TaskViewControl _control = (TaskViewControl)sender;
            _control.PriorityComboBox.SelectedIndex = (int)args.NewValue;

        }

        private static void OnChangePomodorosSpent(DependencyObject sender, DependencyPropertyChangedEventArgs args) 
        {
            TaskViewControl _control = (TaskViewControl)sender;
            _control.PomodoroSpentLabel.Content = (string)args.NewValue;
        }

        private static void OnChangeIsEditable(DependencyObject sender, DependencyPropertyChangedEventArgs args) 
        {
            TaskViewControl _control = (TaskViewControl)sender;
            bool arg = (bool)args.NewValue;
            _control.DescriptionTextBox.IsEnabled = arg;
            _control.TitleTextBox.IsEnabled = arg;
            _control.PriorityComboBox.IsEnabled = arg;
            _control.StateCheckBox.IsEnabled = arg;
        }

        private static void OnChangeState(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            TaskViewControl _control = (TaskViewControl)sender;
            _control.StateCheckBox.IsChecked = (bool) args.NewValue;
        }

        

        public TaskViewControl()
        {
            InitializeComponent();
            TitleTextBox.IsEnabled = IsEditable;
            DescriptionTextBox.IsEnabled = IsEditable;
            PriorityComboBox.IsEnabled = IsEnabled;
            StateCheckBox.IsEnabled = IsEditable;

            
        }
    }
}
