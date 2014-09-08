using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pomodoro.Repository
{
    interface ITaskRepository
    {
        IList<Model.ITaskModel> GetTasks();
        Model.ITaskModel GetTaskById(string id);
        IList<Model.ITaskModel> GetTasksByDate(DateTime date);
        void AddTask(Model.ITaskModel task);
        void DeleteTask(Model.ITaskModel task);
        bool Commit();

    }
}
