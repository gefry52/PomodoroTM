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
    /// Логика взаимодействия для TaskList.xaml
    /// </summary>
    public partial class TaskList : UserControl
    {

        private bool _unloadedFlag = false;

        public TaskList()
        {
            InitializeComponent();
            this.Loaded += OnPageLoaded;
        }


        private void OnPageLoaded(object sender, EventArgs e)
        {
            if (_unloadedFlag == true)
            {
                _unloadedFlag = false;
                return;
            }
            this.DataContext = new TaskListViewModel();
            _unloadedFlag = true;
        }

       

        private void DataGrid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.Taskslist.Columns[0].Width = (Double)((this.Taskslist.ActualWidth / 100) * 20);
            this.Taskslist.Columns[1].Width = (Double)((this.Taskslist.ActualWidth / 100) * 20);
            this.Taskslist.Columns[2].Width = (Double)((this.Taskslist.ActualWidth / 100) * 14);
            this.Taskslist.Columns[3].Width = (Double)((this.Taskslist.ActualWidth / 100) * 14);
            this.Taskslist.Columns[4].Width = (Double)((this.Taskslist.ActualWidth / 100) * 16);
            this.Taskslist.Columns[5].Width = (Double)((this.Taskslist.ActualWidth / 100) * 16);
        }
    }
}
