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
        const string TaskFileName = "../tasks.bin";

        public IQueryable<Model.ITaskModel> GetTasks()
        {

            if (File.Exists(TaskFileName))
            {
                Stream TestFileStream = File.OpenRead(TaskFileName);
                BinaryFormatter deserializer = new BinaryFormatter();
                _taskList = (List<Model.ITaskModel>)deserializer.Deserialize(TestFileStream);
                TestFileStream.Close();
            }
            else 
            {
                _taskList = null;
            }
            return _taskList.AsQueryable();
        }

        public IQueryable<Model.ITaskModel> GetTaskByDate(DateTime date) 
        {  
            IQueryable<Model.ITaskModel> tasks = GetTasks();
            IQueryable<Model.ITaskModel> tasksByDate = from  t in tasks 
                                                where t.OpeningDate == date
                                                select t;
            return tasksByDate;
        }

        public Model.ITaskModel GetTaskById(string id) 
        {
            IQueryable<Model.ITaskModel> tasks = GetTasks();
            IEnumerable<Model.ITaskModel> task = from t in tasks
                                            where t.Id == id                           
                                            select  t;
            return task.FirstOrDefault();
        }

        public bool SaveTask(Model.ITaskModel task) 
        {
            List<Model.ITaskModel> taskList = GetTasks().ToList<Model.ITaskModel>();
            taskList.Remove(GetTaskById(task.Id));
            taskList.Add(task);
            try
            {  
                Stream FileStream = File.Create(TaskFileName);
                BinaryFormatter serializer = new BinaryFormatter();
                serializer.Serialize(FileStream, taskList.AsQueryable<Model.ITaskModel>());
                FileStream.Close();
                return true;
            }
            catch { return false; }
        }


    }
}
