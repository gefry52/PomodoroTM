using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pomodoro.Model
{
    public enum Priority { urgent, high, normal, low }
    public enum State {open,done}

    public interface ITaskModel
    {
         String Id { get; }
         State State { get; set; }
         String Title { get; set; }
         String Description { get; set; }
         Priority Priority { get; set; }
         Int32 CountPomodoroUnit { get; set; }
         DateTime OpeningDate { get; set; }
         DateTime ClosingDate { get; set; }
         ITaskStyle Style { get; set; }
    }
}
