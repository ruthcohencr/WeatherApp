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
    // clear-day = string "http://icons.iconarchive.com/icons/oxygen-icons.org/oxygen/128/Status-weather-clear-icon.png";
    //enum Weather
    //{
    //    clear-day,
    //    clear-night,
    //    rain,
    //    snow,
    //    partly-cloudy-night,
    //    partly-cloudy-day,
    //    cloudy
    //}
    
    public class WeekViewModel : ViewModelBase
    {
        private Constants.Constants constants;
        private DayOfWeek _dayOfWeek;
        private string _cityNameText;

        private List<Datum2> _dailyDetails;

        private List<string> _weatherDay;
        public List<string> WeatherDay
        {
            get { return _weatherDay; }
            set
            {
                _weatherDay = value;
            }
        }
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

        private Dictionary<string, string> _iconsToUrl;


        private List<MinDatum2> _weatherForWeek;
        public List<MinDatum2> WeatherForWeek
        {
            get { return _weatherForWeek; }
            set
            {
                _weatherForWeek = value;
                OnPropertyChanged("WeatherForWeek");
            }
        }

        private ICommand _goBackCommand;
      
        public ICommand GoBackCommand
        {
            get { return _goBackCommand ?? (_goBackCommand = new Command(GoBackCommandAction)); }
        }

        private async void GoBackCommandAction(object obj)
        {
            await navigation.PopAsync();
        }

        public WeekViewModel(string cityName, string jsonResult, List<Datum2> dailyDetails, INavigation navigation)
            : base(navigation)
        {
            constants = new Constants.Constants();
            _cityNameText = cityName;
            _dailyDetails = dailyDetails;
            //init for each day the name of the day
            WeatherDay = GetListWeatherDay();
            //combine the name of days to list of view model
            WeatherForWeek = GetListIncludingNameOfDays();          
            AddImageAccordingToIcon();
        }

        private void AddImageAccordingToIcon()
        {
            foreach (var day in WeatherForWeek)
            {
                var icon = day.Icon;
                string urlImageIcon;
              
                if (constants.IconToUrl.ContainsKey(icon))
                {
                    urlImageIcon = constants.IconToUrl[icon];
                    day.ImageUrlIcon = urlImageIcon;
                }
            }
        }

        private List<MinDatum2> GetListIncludingNameOfDays()
        {
            List<MinDatum2> listForModel = new List<MinDatum2>();
            var wrappertList = _dailyDetails.Select(
                day => new MinDatum2
                {
                    Humidity = day.Humidity*100,
                    Icon = day.Icon,
                    Summary = day.Summary,
                    TemperatureHigh = day.TemperatureHigh,
                    TemperatureLow = day.TemperatureLow,
                    WindSpeed = day.WindSpeed
                }).ToList();

            var nameOfDay = DateTime.Now;
            foreach (var day in wrappertList)
            {
                day.NameOfDay = nameOfDay.DayOfWeek.ToString();
                //setting the date for each day.
                day.Date = nameOfDay.Date;
                nameOfDay = nameOfDay.AddDays(1);
            }
            //var list = firstList.Select(x => { x.NameOfDay = nameOfDay.DayOfWeek.ToString(); nameOfDay.AddDays(1); });

            return wrappertList;
        }

        public List<string> GetListWeatherDay()
        {
            List<string> weatherDays = new List<string>();
            var today = DateTime.Now;
            //set list[0] to be the day of today
            weatherDays.Add(today.DayOfWeek.ToString());

            // how many days do i have ahead
            var length = _dailyDetails.Count;

            for (int i = 1; i < length; i++)
            {
                today = today.AddDays(1);
                string nameOfDay = today.DayOfWeek.ToString();
                weatherDays.Add(nameOfDay);
            }
            
            return weatherDays;
        }

    }
}

