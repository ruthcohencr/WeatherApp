using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Constants
{
    public class Constants
    {
 
        public Dictionary<string, string> IconToUrl = new Dictionary<string, string>()
        {
            { "clear-day","http://icons.iconarchive.com/icons/oxygen-icons.org/oxygen/128/Status-weather-clear-icon.png" },
            {"rain","http://icons.iconarchive.com/icons/oxygen-icons.org/oxygen/128/Status-weather-showers-icon.png" },
            {"snow", "http://icons.iconarchive.com/icons/oxygen-icons.org/oxygen/128/Status-weather-hail-icon.png" },
            {"partly-cloudy-day", "http://icons.iconarchive.com/icons/oxygen-icons.org/oxygen/128/Status-weather-clouds-icon.png" },
            {"partly-cloudy-night", "http://icons.iconarchive.com/icons/oxygen-icons.org/oxygen/128/Status-weather-clouds-night-icon.png" },
            {"cloudy", "http://icons.iconarchive.com/icons/oxygen-icons.org/oxygen/128/Status-weather-many-clouds-icon.png" },
            {"clear-night", "http://icons.iconarchive.com/icons/oxygen-icons.org/oxygen/128/Status-weather-few-clouds-night-icon.png" }
        };
    };
}
