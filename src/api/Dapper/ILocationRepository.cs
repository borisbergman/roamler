using System.Collections.Generic;
using RoamlerGeoApi.Models;

namespace RoamlerGeoApi.Dapper
{
    public interface ILocationRepository
    {
        IEnumerable<Location> GetAll(int offset);        
        IEnumerable<LocationEx> GetAround(Location location, double distance, int limit);        
    }
}