using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
namespace Pomodoro.Model
{
    public class TaskStyle :ITaskStyle
    {
        private Color _backgroundColor;
        private Color _textColor;

        public Color BackgroundColor 
        {
            get { return _backgroundColor; }
            set { _backgroundColor = value; }
        }
        public Color TextColor 
        {
            get { return _textColor; }
            set { _textColor = value; }
        }

        public TaskStyle(Color backgroundColor, Color textColor) 
        {
            _backgroundColor = backgroundColor;
            _textColor = textColor;

        }
    }
}
