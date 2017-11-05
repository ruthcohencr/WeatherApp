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
        private DayOfWeek _dayOfWeek;
        private string _cityNameText;
        //private MainViewModel _mainViewModel;

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


        private List<Datum2> _weaterForWeek;

        public List<Datum2> WeatherForWeek
        {
            get { return _weaterForWeek; }
            set
            {
                _weaterForWeek = value;
                OnPropertyChanged("WeatherForWeek");
            }
        }
        //private string _cityNameText;
        //private string _temperatureText;
        //private double _temperatureMax;
        //private double _temperatureMin;
        private ICommand _goBackCommand;
        public DayOfWeek DayOfWeek
        {
            get { return _dayOfWeek; }
            set
            {
                _dayOfWeek = value;
                OnPropertyChanged("DayOfWeek");
            }
        }
        public ICommand GoBackCommand
        {
            get { return _goBackCommand ?? (_goBackCommand = new Command(GoBackCommandAction)); }
        }

        private async void GoBackCommandAction(object obj)
        {
            await navigation.PopAsync();
        }

        public WeekViewModel(string cityName,string jsonResult, List<Datum2> dailyDetails, INavigation navigation)
            :base(navigation)
        {
           
            _cityNameText = cityName;
            _weaterForWeek = dailyDetails;
            DayOfWeek dayOfWeek = DateTime.Now.DayOfWeek;
            DayOfWeek = dayOfWeek;
            _dateTime = DateTime.Now;
          
        }

        
    }
}
