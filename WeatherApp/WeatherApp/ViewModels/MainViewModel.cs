using WeatherApp.Models;
using WeatherApp.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using System.Linq;

namespace WeatherApp.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private const string BaseAddress = "https://api.darksky.net/forecast/cbcd2c87dcbf63941d36d326cc40b1f1/{0},{1}?units=si";
        //    private const string BaseAddress = "https://api.darksky.net/forecast/cbcd2c87dcbf63941d36d326cc40b1f1/{0},{1},{2}?units=si";

        private IEnumerable<CityModel> _citiesList;
        private Action<string> _onError;

        public string Title { get; set; }
        private ICommand _showTempCommand;
        //private int UnixTime { get; set; } = 255589200;
        private string _loadingText;
        public List<Datum2> DailyDetails { get; set; }
        public double TemperatureMin;

        public string LoadingText
        {
            get { return _loadingText; }
            set
            {
                _loadingText = value;
                OnPropertyChanged("LoadingText");
            }
        }

        public IEnumerable<CityModel> GetCitiesList
        {
            get { return _citiesList; }
        }

        public ICommand ShowTempCommand
        {
            get { return _showTempCommand ?? (_showTempCommand = new Command(ShowTempCommandAction)); }
        }

        private void ShowTempCommandAction(object obj)
        {
            StartHttpClientCall(obj as CityModel);
        }

        private async void StartHttpClientCall(CityModel model)
        {
            if (model == null)
                return;

            using (var client = new HttpClient())
            {
                LoadingText = "Connecting to Dark sky...";
                try
                {
                    var url = string.Format(BaseAddress, model.Latitude, model.Longitude);
                    string jsonResult = await client.GetStringAsync(url);
                    var jsonedModel = JsonConvert.DeserializeObject<WeatherModel>(jsonResult.ToString());



                    DailyDetails = jsonedModel.daily.data;

                    //Device.BeginInvokeOnMainThread(() =>
                    //{

                    //});
                    LoadingText = string.Empty;
                    await navigation.PushAsync(new WeekView(model.CityName, jsonResult, DailyDetails));
                }
                catch (Exception ex)
                {
                    _onError("נכשל ניסיון טעינת מזג האוויר לשבוע הקרוב. אנא נסו שוב מאוחר יותר.");
                    return;
                }
            }
        }

        public MainViewModel(Action<string> onError, INavigation navigation)
            : base(navigation)
        {
            _onError = onError;

            Title = "The Weather Forecast";
            _citiesList = new List<CityModel>
            {
                new CityModel
                {
                     CityName = "Jerusalem",
                     Latitude = "35.216667",
                     Longitude ="31.783333"
                },
                new CityModel
                {
                     CityName = "Tel-Aviv",
                       Latitude = "32.0808800",
                    Longitude = "34.7805700"
                },
                 new CityModel
                 {
                     CityName = "Beit-Shemesh",
                     Latitude = "31.747041",
                     Longitude ="34.988099"
                 },
                 new CityModel
                 {
                    CityName  = "Tiberias",
                    Latitude = "32.795859",
                    Longitude = "35.530973"
                 },
                  new CityModel
                 {
                    CityName  = "Eilat",
                    Latitude = "29.557669",
                    Longitude = "34.951925"
                 },
                  new CityModel
                 {
                     CityName = "Tzfat",
                     Latitude = "32.964648",
                     Longitude ="35.495997"
                 },
                   new CityModel
                {
                    CityName = "Paris",
                    Latitude = "48.864716",
                    Longitude = "2.349014"
                },
                   new CityModel
                 {
                    CityName  = "New-York",
                    Latitude = "40.730610",
                    Longitude = "-73.935242"
                 }
            };
        }
    }
}
