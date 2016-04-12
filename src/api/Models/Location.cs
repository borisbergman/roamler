using System.Drawing;

namespace RoamlerGeoApi.Models
{
    public class Location
    {        
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Name { get; set; }        
    }

    public class LocationEx : Location
    {
        public double Distance { get; set; }        
    }
}
