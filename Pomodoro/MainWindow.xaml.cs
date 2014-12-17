using FirstFloor.ModernUI.Presentation;
using FirstFloor.ModernUI.Windows.Controls;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
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


namespace Pomodoro
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ModernWindow
    {
        private Controllers.CultureUIController _cultureController = Controllers.CultureUIController.Instatnce;

        public MainWindow()
        {
           // Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
           // this.Language = System.Windows.Markup.XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag);
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
            this.TitleLinks[0].DisplayName = Pomodoro.Resources.UIStrings.TimerStr;
            this.TitleLinks[1].DisplayName = Pomodoro.Resources.UIStrings.TasksStr;
            this.TitleLinks[2].DisplayName = Pomodoro.Resources.UIStrings.SettingsStr;
            this.MenuLinkGroups[0].DisplayName = Pomodoro.Resources.UIStrings.PomodoroTimer;
        }

        private void OnCultureChange(object o, Controllers.CultureEventArgs e) 
        {
            SetUIStrings();
        } 
       

    }

}
