using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WeatherApp.Models;
using Xamarin.Forms;

namespace WeatherApp.ViewModels
{
    public class WeekViewModel:ViewModelBase
    {
        private string _cityNameText;

        public string CityNameText
        {
            get
            {
                return _cityNameText;
            }
            set
            {
                _cityNameText = value;
                OnPropertyChanged("CityNameText");
            }
        }
        //private string _cityNameText;
        //private string _temperatureText;
        //private double _temperatureMax;
        //private double _temperatureMin;
        private ICommand _goBackCommand;

        public ICommand GoBackCommand
        {
            get { return _goBackCommand ?? (_goBackCommand = new Command(GoBackCommandAction)); }
        }

        private async void GoBackCommandAction(object obj)
        {
            await navigation.PopAsync();
        }

        public WeekViewModel(string cityName,string jsonResult,INavigation navigation)
            :base(navigation)
        {
            _cityNameText = cityName;
        }

        
    }
}
