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
        //private ICommand _goBackCommand;

        public WeekViewModel(string cityName,List<Datum2> weatherForWeek,INavigation navigation)
            :base(navigation)
        {
            _cityNameText = cityName;
        }

        
    }
}
