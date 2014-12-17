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
    /// Interaction logic for Tasks.xaml
    /// </summary>
    public partial class Tasks : UserControl
    {
        private Controllers.CultureUIController _cultureController = Controllers.CultureUIController.Instatnce;

        public Tasks()
        {
            InitializeComponent();

             // Based on the fact that  "Binding" can only be set in 
            // parameter DependencyProperty object DependencyObject.
            // And property "DisplayName" type of "Link" is not DependencyObject.
            // The properties will be assigned in the code. And change the 
            // will occur with a event CultureChanged type of CultureUIController
            SetUIStrings();
            _cultureController.CultureChanged += OnCultureChange;
            
        }

        private void SetUIStrings() 
        {
             this.Tab.Links[0].DisplayName = Pomodoro.Resources.UIStrings.TasksStr;
             this.Tab.Links[1].DisplayName = Pomodoro.Resources.UIStrings.NewTaskStr;
        }

        private void OnCultureChange(object o, Controllers.CultureEventArgs e) 
        {
            SetUIStrings();
        } 
       

        
    }
}
