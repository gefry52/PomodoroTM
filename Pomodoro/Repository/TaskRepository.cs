using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;




namespace Pomodoro.Repository
{
    class TaskRepository : ITaskRepository
    {
        private List<Model.ITaskModel> _taskList;
        const string TaskFileName = "./tasks.bin";

        public List<Model.ITaskModel> GetTasks()
        {

            if (File.Exists(TaskFileName))
            {
                using (Stream stream = File.Open(TaskFileName, FileMode.Open))
                {
                    BinaryFormatter bin = new BinaryFormatter();
                    _taskList = (List<Model.ITaskModel>)bin.Deserialize(stream);
                }       
            }
            else 
            {

                _taskList = new List<Model.ITaskModel>();

            }
            return _taskList;
        }

        public IQueryable<Model.ITaskModel> GetTaskByDate(DateTime date) 
        {  
            IQueryable<Model.ITaskModel> tasks = GetTasks().AsQueryable<Model.ITaskModel>();
            IQueryable<Model.ITaskModel> tasksByDate = from  t in tasks 
                                                where t.OpeningDate.Date == date.Date
                                                select t;
            return tasksByDate;
        }

        public Model.ITaskModel GetTaskById(string id) 
        {
            IQueryable<Model.ITaskModel> tasks = GetTasks().AsQueryable<Model.ITaskModel>();
            IEnumerable<Model.ITaskModel> task = from t in tasks
                                            where t.Id == id                           
                                            select  t;
            return task.FirstOrDefault();
        }

        public bool SaveTask(Model.ITaskModel task) 
        {
            List<Model.ITaskModel> _taskList = GetTasks().ToList<Model.ITaskModel>();
            _taskList.Remove(GetTaskById(task.Id));
            _taskList.Add(task);
            try {
                using (Stream stream = File.Open(TaskFileName, FileMode.OpenOrCreate))
                    {
                        var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                        bformatter.Serialize(stream, _taskList);
                        return true;
                    }  
                }
            catch { return false; }
        }


    }
}
