using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Pomodoro.PagesStrings
{
    class SettingsPageStr : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        Controllers.CultureUIController _cultureUIController = Controllers.CultureUIController.Instatnce;

        public string ShortBreakTimeStr { get { return Resources.UIStrings.ShortBreakTimeStr; } }

        public string LongBreakTimeStr { get { return Resources.UIStrings.LongBreakTimeStr; } }

        public string TurnSoundOnStr { get { return Resources.UIStrings.TurnSoundOnStr; } }

        public string TurnMonitorOffStr { get { return Resources.UIStrings.TurnMonitorOffStr; } }

        public string TimerSettingsStr { get { return Resources.UIStrings.TimerSettingStr; } }

        public string AppearanceStr { get { return Resources.UIStrings.AppearanceStr; } }

        public string AboutStr { get { return Resources.UIStrings.AboutStr; } }

        public string ThemeStr { get { return Resources.UIStrings.ThemeStr; } }

        public string LanguageStr { get { return Resources.UIStrings.LanguageStr; } }

        public SettingsPageStr() 
        {
            _cultureUIController.CultureChanged += OnCultureChanged;
        }

        public void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        private void OnCultureChanged(object sender, Controllers.CultureEventArgs e) 
        {
            foreach (var propperties in typeof(SettingsPageStr).GetProperties()) 
            {
                OnPropertyChanged(propperties.Name.ToString());
            }
        }
    }
}
