using FirstFloor.ModernUI.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CountdownTimer;
using System.Windows.Input;
using System.ComponentModel;

namespace Pomodoro.Pages
{
    class HomeViewModel: INotifyPropertyChanged
    
    {    
        public event PropertyChangedEventHandler PropertyChanged;
    
        private Model.ITaskModel _currentTask {get;set;}
        
        

        public Model.ITaskModel CurrentTask
        {
            get{return _currentTask;}
            set
            {
                _currentTask=value;
                OnPropertyChanged("CurrentTask");
            }
        }

        public HomeViewModel() 
        {
            _currentTask = new Model.TaskModel("bbbb");
        }

        public void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
                
            }
        }

        
        private ICommand _setCurrentTask
        {
            get { return new RelayCommand(x => ExecuteSetCurrentTask()); }

        }

        public ICommand SetCurrentTask
        {
            get { return _setCurrentTask; }
            set { }
        }

        private void ExecuteSetCurrentTask()
        {
            
            this.CurrentTask = new  Model.TaskModel( "huyna"); 
        }

    }
}
