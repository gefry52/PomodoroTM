using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Pomodoro.Model
{
    public   interface ITaskStyle
    {
        Color BackgroundColor{get;set;}
        Color TextColor { get; set; }
    }
}
