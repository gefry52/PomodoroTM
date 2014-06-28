using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pomodoro.Repository
{
    interface ITaskRepository
    {
        IQueryable<Model.ITaskModel> GetTasks();
        Model.ITaskModel GetTaskById(string id);
        IQueryable<Model.ITaskModel> GetTaskByDate(DateTime date);
        bool SaveTask(Model.ITaskModel task);
    }
}
