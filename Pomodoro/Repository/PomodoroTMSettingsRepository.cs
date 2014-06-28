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
        private Model.IPomodoroTMSettingsModel _settings;
        const string FileName = "../settings.bin";

        public Model.IPomodoroTMSettingsModel GetPomodoroTMSettings()
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
            return _settings;
 
        }

        public bool SavePomodoroTMSettings(Model.IPomodoroTMSettingsModel settings) 
        {
            try
            {
                Stream FileStream = File.Create(FileName);
                BinaryFormatter serializer = new BinaryFormatter();
                serializer.Serialize(FileStream, settings);
                FileStream.Close();
                return true;
            }
            catch { return false; }
        }
    }
}
