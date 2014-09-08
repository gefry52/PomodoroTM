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

        private static readonly ITaskRepository GetInstatce = new TaskRepository();
        private List<Model.ITaskModel> _taskList;

        /// <summary>
        /// Get repository instance 
        /// </summary>
        public static  ITaskRepository TaskRepositoryInstance  
        {
            get { return GetInstatce; }         
        }
        
        
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
        
        /// <summary>
        /// return tasks list
        /// </summary>
        /// <returns>IList<ITaskmodel></returns>
        public IList<Model.ITaskModel> GetTasks()
        {
            return _taskList;
        }

        /// <summary>
        /// return tasks list by date
        /// </summary>
        /// <param name="date"></param>
        /// <returns>IList<Model.ITaskModel></returns>
        public IList<Model.ITaskModel> GetTasksByDate(DateTime date) 
        {  
            IQueryable<Model.ITaskModel> tasksByDate = from t in _taskList.AsQueryable<Model.ITaskModel>() 
                                                where t.ExecutionDate.Date == date.Date
                                                select t;
            return tasksByDate.ToList<Model.ITaskModel>();
        }

        /// <summary>
        /// return task by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>ITaskModel</returns>
        public Model.ITaskModel GetTaskById(string id) 
        {
            IQueryable<Model.ITaskModel> task = from t in _taskList.AsQueryable<Model.ITaskModel>()
                                            where t.Id == id                           
                                            select  t;
            return task.FirstOrDefault();
        }
        
        /// <summary>
        /// Insert task to task list 
        /// </summary>
        /// <param name="task">Model.ITaskModel</param>
        public void AddTask(Model.ITaskModel task) 
        {
            _taskList.Remove(task);
            _taskList.Add(task); 
        }

        /// <summary>
        /// Delete task from task list 
        /// </summary>
        /// <param name="task">Model.ITaskModel</param>
        public void DeleteTask(Model.ITaskModel task) 
        {
            _taskList.Remove(task);
        }

        /// <summary>
        /// Save changes on repositories
        /// </summary>
        /// <returns>bool</returns>
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
