using System;
using System.Collections.Generic;

using Newtonsoft.Json;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WeatherApp.Models;
using WeatherApp.Views;
using Xamarin.Forms;
using System.Net.Http;

namespace WeatherApp.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private const string BaseAddress = "https://api.darksky.net/forecast/cbcd2c87dcbf63941d36d326cc40b1f1/{0},{1}?units=si";
    //    private const string BaseAddress = "https://api.darksky.net/forecast/cbcd2c87dcbf63941d36d326cc40b1f1/{0},{1},{2}?units=si";

        private IEnumerable<CityModel> _citiesList;
        public string Title { get; set; }
        private ICommand _showTempCommand;
        private int UnixTime { get; set; } = 255589200;
        private string _loadingText;
        public List<Datum2> DailyDetails;
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
                try
                {
                    var url = string.Format(BaseAddress, model.Latitude, model.Longitude, UnixTime);
                    string result = await client.GetStringAsync(url);
                    var jsonedModel = JsonConvert.DeserializeObject<WeatherModel>(result.ToString());

                    LoadingText = string.Empty;
                    DailyDetails = jsonedModel.daily.data;
                    var today = DailyDetails.First();
                    TemperatureMin = today.TemperatureMin;
                   
                    //CityNameText = SelectedCityModel.Name;
                    //TemperatureText = model.currently.temperature.ToString();

                    //Device.BeginInvokeOnMainThread(() =>
                    //{

                    //});

                    //await navigation.PushAsync(new WeekView(model.CityName, jsonedModel.currently.temperature.ToString()));
                }
                catch (Exception ex)
                {
                    //_onError("נכשל ניסיון עדכון מזג אוויר. אנא נסו שנית מאוחר יותר.");
                    return;
                }
            }
        }

        public MainViewModel()
        {

            Title = "תחזית מזג האויר";
            _citiesList = new List<CityModel>
            {
                new CityModel
                {
                     CityName = "ירושלים",
                     Latitude = "35.216667",
                     Longitude ="31.783333"
                },
                new CityModel
                {
                     CityName = "תל אביב",
                       Latitude = "32.0808800",
                    Longitude = "34.7805700"
                },
                 new CityModel
                 {
                     CityName = "בית שמש",
                     Latitude = "31.747041",
                     Longitude ="34.988099"
                 },
                 new CityModel
                 {
                    CityName  = "ניו-יורק",
                    Latitude = "40.730610",
                    Longitude = "-73.935242"
                 },
                  new CityModel
                 {
                     CityName = "צפת",
                     Latitude = "32.964648",
                     Longitude ="35.495997"
                 },
                   new CityModel
                {
                    CityName = "פריז",
                    Latitude = "48.864716",
                    Longitude = "2.349014"
                }
            };
        }
    }
}
