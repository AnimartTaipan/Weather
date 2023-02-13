using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather_Report.Models
{
    public class WeatherFuture
    {
        public string cod { get; set; }
        public int message { get; set; }
        public int cnt { get; set; }
        public List<List> list { get; set; }
        public City city { get; set; }
    }
  
    public class City
    {
        public int id { get; set; }
        public string name { get; set; }
        public CoordArr coord { get; set; }
        public string country { get; set; }
        public int population { get; set; }
        public int timezone { get; set; }
        public int sunrise { get; set; }
        public int sunset { get; set; }
    } 

    public class List
    {
        public int dt { get; set; }
        public MainArr main { get; set; }
        public List<WeatherArr> weather { get; set; }
        public CloudsArr clouds { get; set; }
        public WindArr wind { get; set; }
        public int visibility { get; set; }
        public double pop { get; set; }
        public SysArr sys { get; set; }
        public string dt_txt { get; set; }
    }

    public class MainArr
    {
        public double temp { get; set; }
        public double feels_like { get; set; }
        public double temp_min { get; set; }
        public double temp_max { get; set; }
        public int pressure { get; set; }
        public int sea_level { get; set; }
        public int grnd_level { get; set; }
        public int humidity { get; set; }
        public double temp_kf { get; set; }
    }

    public class CloudsArr
    {
        public int all { get; set; }
    }

    public class CoordArr
    {
        public double lat { get; set; }
        public double lon { get; set; }
    }

    public class SysArr
    {
        public string pod { get; set; }
    }
    public class WeatherArr
    {
        public int id { get; set; }
        public string main { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
    }
    public class WindArr
    {
        public double speed { get; set; }
        public int deg { get; set; }
        public double gust { get; set; }
    }
}
