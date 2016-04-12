using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using Dapper;
using MySql.Data.MySqlClient;
using RoamlerGeoApi.Helpers;
using RoamlerGeoApi.Models;

namespace RoamlerGeoApi.Dapper
{
    public class LocationRepository : ILocationRepository
    {
        private readonly IDbConnection _db;

        public LocationRepository()
        {
            try
            {
                _db = new MySqlConnection(ConfigurationManager.ConnectionStrings["default"].ConnectionString);
            }
            catch (Exception)
            {
                Debug.WriteLine("Error connecting db");
            }
        }

        public IEnumerable<Location> GetAll(int offset)
        {
            return _db?.Query<Location>("SELECT name, y(mappoint) as latitude, x(mappoint) as longitude FROM Location limit 100 offset @offset", new {offset}).ToList();
        }

        /// <summary>
        /// Let the database do the hard work. Spatial search is fully implemented in MySql, Postgres, MongoDB. 
        /// Azure offers MySQL, so i'll be using it. Because of discribed limitations a square instead of a circle is measured.
        /// https://www.percona.com/blog/2013/10/21/using-the-new-mysql-spatial-functions-5-6-for-geo-enabled-applications/
        /// 
        /// 1 degree of latitude ~= 111 km
        /// 1 degree of longitude ~= cos(latitude)*111 km
        /// </summary>
        /// <param name="location"></param>
        /// <param name="distance">x^2 of square around point</param>
        /// <param name="limit">result limit</param>
        /// <returns></returns>
        public IEnumerable<LocationEx> GetAround(Location location, double distance, int limit)
        {            
            var rlon1 = location.Longitude - distance / Math.Abs(Math.Cos(location.Latitude.ToRadians()) * 111);
            var rlon2 = location.Longitude + distance / Math.Abs(Math.Cos(location.Latitude.ToRadians()) * 111);
            var rlat1 = location.Latitude - (distance / 111);
            var rlat2 = location.Latitude + (distance / 111);

            List<LocationEx> results = null;
            
                results =
                    _db?.Query<LocationEx>(@"select harvesine(y(mappoint), x(mappoint), @lat, @lon ) as distance, 
                                        y(mappoint) as latitude, x(mappoint) as longitude, mappoint,  
                                        name 
                                        from location where st_within(mappoint, envelope(linestring(point(@rlon1, @rlat1), point(@rlon2, @rlat2))))
                                        order by distance limit @limit",
                                        new { rlon1, rlon2, rlat1, rlat2, limit, lon = location.Longitude, lat = location.Latitude }).ToList();
            
            return results;
        }
    }
}