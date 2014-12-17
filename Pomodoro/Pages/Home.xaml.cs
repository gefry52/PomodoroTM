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



namespace Pomodoro.Pages
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : UserControl
    {
        private bool _unloadedFlag = false;

        public Home()
        {
            InitializeComponent();
            this.HomePage.Loaded += OnPageLoad;
        }
        
        private void  OnPageLoad(object sender,EventArgs e) 
        {
            if (_unloadedFlag == true)
            {
                _unloadedFlag =false;
                return;
            }    
            this.DataContext = new HomeViewModel();
            _unloadedFlag = true;
        }

     

        private void ToDayTasksList_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.ToDayTasksList.Columns[0].Width = (Double)((this.ToDayTasksList.ActualWidth/100) * 50);
            this.ToDayTasksList.Columns[1].Width = (Double)((this.ToDayTasksList.ActualWidth / 100) * 25);
            this.ToDayTasksList.Columns[2].Width = (Double)((this.ToDayTasksList.ActualWidth / 100) * 25);
        }

      

    }
}