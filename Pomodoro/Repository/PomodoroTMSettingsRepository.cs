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

        public Model.IPomodoroTMSettingsModel GetPomodoroTMSettings()
        {
            return _settings;
        }

        public void SetPomodoroTMSettings(Model.IPomodoroTMSettingsModel settings) 
        {
            _settings = settings;
        }

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
