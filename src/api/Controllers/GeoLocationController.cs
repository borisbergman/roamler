using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using RoamlerGeoApi.Dapper;
using RoamlerGeoApi.Models;

namespace RoamlerGeoApi.Controllers
{
    public class GeoLocationController : ApiController
    {
        public GeoLocationController()
        {

        }

        readonly ILocationRepository _locationRepository = new LocationRepository();
        
        [Route("{distance}/{limit}")]
        public IEnumerable<LocationEx> PostAround([FromBody] Location location, double distance, int limit)
        {
            return _locationRepository.GetAround(location, distance, limit);
        }

        [Route("GetAll/{offset}")]
        public IEnumerable<Location> GetAll(int offset)
        {
            return _locationRepository.GetAll(offset);
        }

    }
}

