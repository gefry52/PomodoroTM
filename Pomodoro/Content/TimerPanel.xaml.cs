using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    /// Логика взаимодействия для TimerPanel.xaml
    /// </summary>
    public partial class TimerPanel : UserControl
    {
        public TimerPanel()
        {
            this.DataContext = new TimerPanelViewModel();
            InitializeComponent();
        }

        
    }
}
