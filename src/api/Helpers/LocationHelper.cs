using System;
using RoamlerGeoApi.Models;

namespace RoamlerGeoApi.Helpers
{
    public static class LocationHelper
    {
        /// <summary>
        /// Calculates the distance between this location and another one, in meters        
        /// uses the haversine implementation, https://en.wikipedia.org/wiki/Haversine_formula, disregarding biaxial ellipsoid properties (0.3%)
        /// </summary>               
        public static double CalculateDistance(Location location1, Location location2)
        {
            var R = 6371000; // metres
            var φ1 = location1.Latitude.ToRadians();
            var φ2 = location2.Latitude.ToRadians();
            var Δφ = (location2.Latitude - location1.Latitude).ToRadians();
            var Δλ = (location2.Longitude - location1.Longitude).ToRadians();

            var a = Math.Sin(Δφ / 2) * Math.Sin(Δφ / 2) +
                    Math.Cos(φ1) * Math.Cos(φ2) *
                    Math.Sin(Δλ / 2) * Math.Sin(Δλ / 2);
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            return R * c;
        }        

    }
    public static class ExtensionMethods
    {
        public static double ToRadians(this double angle)
        {
            return (Math.PI / 180) * angle;
        }
    }
}