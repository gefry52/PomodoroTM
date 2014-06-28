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

namespace Pomodoro.Content
{
    /// <summary>
    /// Логика взаимодействия для TaskPanelView.xaml
    /// </summary>
    public partial class TaskPanelView : UserControl
    {
        
        static TaskPanelViewModel _dataContext = new TaskPanelViewModel(new Model.TaskModel("sdf"),false);

        public static readonly DependencyProperty TaskProperty = DependencyProperty.Register(
                    "Taskys",
                    typeof(Model.ITaskModel),
                    typeof(TaskPanelView),
                    new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsParentArrange,  new PropertyChangedCallback(OnTaskChange))
                    );

      

        private static void OnTaskChange(DependencyObject sender, DependencyPropertyChangedEventArgs e) 
        {
            
        }

        public  Model.ITaskModel Taskys
        {
            get
            {
                return GetValue(TaskProperty) as Model.ITaskModel; 
            }
            set { this.SetValue(TaskProperty, value); }
        }


        public TaskPanelView()
        {
            this.DataContext = _dataContext;
            Taskys = new Model.TaskModel("taskys");
   
            _dataContext.Task = Taskys;
            
            InitializeComponent();
        }
   
    }
}