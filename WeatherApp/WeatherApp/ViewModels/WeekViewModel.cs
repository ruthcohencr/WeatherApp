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
    public class WeekViewModel : ViewModelBase
    {
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
        //public DateTime Date { get; set; }
        //public DayOfWeek DayOfWeek
        //{
        //    get { return _dayOfWeek; }
        //    set
        //    {
        //        _dayOfWeek = value;
        //        OnPropertyChanged("DayOfWeek");
        //    }
        //}
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

            _cityNameText = cityName;
            //_weatherForWeek = dailyDetails;
            _dailyDetails = dailyDetails;
            WeatherDay = GetListWeatherDay();
            WeatherForWeek = GetListIncludingNameOfDays();
        }

        private List<MinDatum2> GetListIncludingNameOfDays()
        {
            List<MinDatum2> listForModel = new List<MinDatum2>();
            var wrappertList = _dailyDetails.Select(
                day => new MinDatum2
                {
                    Humidity = day.Humidity,
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
