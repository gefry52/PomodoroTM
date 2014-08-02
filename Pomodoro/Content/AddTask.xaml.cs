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
    /// Логика взаимодействия для AddTask.xaml
    /// </summary>
    public partial class AddTask : UserControl
    {
        private Model.ITaskModel _task;

    
        public AddTask()
        {
            InitializeComponent();
            this.Loaded += OnPageLoaded;
        }

        private void OnPageLoaded(object sender,EventArgs e)
        {
            this.DataContext = new AddTaskViewModel();
        }
    }
}
