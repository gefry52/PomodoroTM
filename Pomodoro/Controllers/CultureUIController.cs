using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;

namespace Pomodoro.Controllers
{
    class CultureUIController
    {
    
       private static readonly CultureUIController GetInstatnce = new CultureUIController();

       public event EventHandler<CultureEventArgs> CultureChanged;

       public static CultureUIController Instatnce 
       {
           get { return GetInstatnce; }
           private set { }
       }

       protected CultureUIController() 
       {
       }

       public void ChangeCulture(CultureInfo culture) 
       {
          
           Thread.CurrentThread.CurrentCulture = culture;
           Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
           OnCultureChanged(culture);
       }

       private void OnCultureChanged(CultureInfo culture)
       {
           var handler = CultureChanged;
           if (handler != null)
           {
               handler(null, new CultureEventArgs(culture.DisplayName));
           }
       }


    }

    class CultureEventArgs : EventArgs
    {
        public string CultureDisplayName { get; private set; }

        public CultureEventArgs(string cultureDisplayName) 
        {
            this.CultureDisplayName = cultureDisplayName;
        }

    }
}
