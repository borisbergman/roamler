using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RoamlerGeoApi.Helpers;
using RoamlerGeoApi.Models;

namespace Testing
{
    [TestClass]
    public class LocationTest
    {
        /// <summary>
        /// Reference to http://www.movable-type.co.uk/scripts/latlong.html for geo distances, do a few trials to see if distance is correct
        /// </summary>
        [TestMethod]
        public void TestDistance()
        {
            var locA = new Location {Latitude = 52.216542, Longitude = 5.4778534};
            var locB = new Location {Latitude = 50.91414, Longitude = 5.95549};
            var locC = new Location {Latitude = -20.34334, Longitude = -1.3540565};

            Assert.AreEqual(LocationHelper.CalculateDistance(locA, locB), 148500, 1000);
            Assert.AreEqual(LocationHelper.CalculateDistance(locA, locC), 8095500, 1000);

        }
    }
}
