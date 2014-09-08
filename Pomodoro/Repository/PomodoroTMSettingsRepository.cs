using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;


namespace Pomodoro.Repository
{
    class PomodoroTMSettingsRepository : IPomodoroTMSettingsRepository
    { 
        const string FileName = "../settings.bin";
        
        private static readonly IPomodoroTMSettingsRepository GetInsatance = new PomodoroTMSettingsRepository();

        /// <summary>
        /// Get repositiry instatnce
        /// </summary>
        public static IPomodoroTMSettingsRepository SettingInstance 
        {
            get { return GetInsatance; }
        }

        private Model.IPomodoroTMSettingsModel _settings;

        protected PomodoroTMSettingsRepository() 
        {
            if (File.Exists(FileName))
            {
                Stream TestFileStream = File.OpenRead(FileName);
                BinaryFormatter deserializer = new BinaryFormatter();
                _settings = (Model.IPomodoroTMSettingsModel)deserializer.Deserialize(TestFileStream);
                TestFileStream.Close();
            }
            else
            {
                _settings = new Model.PomodoroTMSettingsModel();
            }
           
        }
        
        /// <summary>
        /// Get settings for pomodoro timer 
        /// </summary>
        /// <returns>Model.IPomodoroTMSettingsModel</returns>
        public Model.IPomodoroTMSettingsModel GetPomodoroTMSettings()
        {
            return _settings;
        }

        /// <summary>
        /// Set settings for pomodoro timers
        /// </summary>
        /// <param name="settings">Model.IPomodoroTMSettingsModel</param>
        public void SetPomodoroTMSettings(Model.IPomodoroTMSettingsModel settings) 
        {
            _settings = settings;
        }

        /// <summary>
        /// Save changes on repository 
        /// </summary>
        /// <returns>bool</returns>
        public bool Commit() 
        {
            try
            {
                Stream FileStream = File.Create(FileName);
                BinaryFormatter serializer = new BinaryFormatter();
                serializer.Serialize(FileStream, _settings);
                FileStream.Close();
                 return true;
            }
            catch { return false; }
        }
    }
}
