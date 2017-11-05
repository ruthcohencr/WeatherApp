using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Models;
using WeatherApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WeatherApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class WeekView : ContentPage
	{
		public WeekView (string cityName,string jsonResult,List<Datum2> dailyDetails)
		{
			InitializeComponent ();
            BindingContext = new WeekViewModel(cityName, jsonResult, dailyDetails, Navigation);
		}
	}
}