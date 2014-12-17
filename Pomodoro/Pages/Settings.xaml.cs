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
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : UserControl
    {
        Controllers.CultureUIController _cultureController= Controllers.CultureUIController.Instatnce;

        public Settings()
        {
            InitializeComponent();
            // Based on the fact that  "Binding" can only be set in 
            // parameter DependencyProperty object DependencyObject.
            // And property "DisplayName" type of "Link" is not DependencyObject.
            // The properties will be assigned in the code. And change the 
            // will occur with a event CultureChanged type of CultureUIController.
            List.Links[0].DisplayName = Pomodoro.Resources.UIStrings.TimerSettingStr;
            List.Links[1].DisplayName = Pomodoro.Resources.UIStrings.AppearanceStr;
            List.Links[2].DisplayName = Pomodoro.Resources.UIStrings.AboutStr;

            _cultureController.CultureChanged += OnChangeCulture;
        }

        private void OnChangeCulture (object o,Controllers.CultureEventArgs e) 
        {
            List.Links[0].DisplayName = Pomodoro.Resources.UIStrings.TimerSettingStr;
            List.Links[1].DisplayName = Pomodoro.Resources.UIStrings.AppearanceStr;
            List.Links[2].DisplayName = Pomodoro.Resources.UIStrings.AboutStr;
        }
    }
}
