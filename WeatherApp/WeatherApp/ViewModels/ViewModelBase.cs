using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace WeatherApp.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        // protected INavigation navigation;

        //public ViewModelBase(INavigation navigation)
        //{
        //    this.navigation = navigation;
        //}

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
            
         
    }
}
