using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pomodoro.Repository
{
    interface ITaskRepository
    {
        List<Model.ITaskModel> GetTasks();
        Model.ITaskModel GetTaskById(string id);
        IQueryable<Model.ITaskModel> GetTaskByDate(DateTime date);
        void AddTask(Model.ITaskModel task);
        void DeleteTask(Model.ITaskModel task);
        bool Commit();

    }
}
