using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;




namespace Pomodoro.Repository
{
    sealed class  TaskRepository : ITaskRepository
    {
        /// <summary>
        /// 
        /// </summary>
        const string TaskFileName = "./tasks.bin";

        //

        private static readonly ITaskRepository GetInstatce = new TaskRepository();

        private List<Model.ITaskModel> _taskList;

        public static  ITaskRepository TaskRepositoryInstance  
        {
            get { return GetInstatce; }         
        }
        
        //
        protected TaskRepository() 
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
        }
        //
        
        public List<Model.ITaskModel> GetTasks()
        {
            return _taskList;
        }

        public IQueryable<Model.ITaskModel> GetTaskByDate(DateTime date) 
        {  
            IQueryable<Model.ITaskModel> tasksByDate = from t in _taskList.AsQueryable<Model.ITaskModel>() 
                                                where t.ExecutionDate.Date == date.Date
                                                select t;
            return tasksByDate;
        }

        public Model.ITaskModel GetTaskById(string id) 
        {

            IEnumerable<Model.ITaskModel> task = from t in _taskList.AsQueryable<Model.ITaskModel>()
                                            where t.Id == id                           
                                            select  t;
            return task.FirstOrDefault();
        }

        public void AddTask(Model.ITaskModel task) 
        {
            _taskList.Remove(_taskList.Find(x => x.Id == task.Id));
            _taskList.Add(task);
           
        }

        public void DeleteTask(Model.ITaskModel task) 
        {        
            _taskList.Remove(_taskList.Find(x => x.Id == task.Id));
        }

        public bool Commit() 
        {
            try
                {
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
