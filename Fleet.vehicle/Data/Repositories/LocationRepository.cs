
using Fleet.Transport.Data.Entities;
using System.Collections.Generic;
namespace Fleet.Transport.Data.Repositories
{
    public class LocationRepository
    {

        private Random _random= new Random();
        public LocationRepository()
        {
        }

   
        public Location FindLocationById(int id) => new Location
        {
            Lantitude = _random.NextDouble() * (90 - (-90)) - 90,
            Longitude = _random.NextDouble() * (180 - (-180)) - 180
            //double latitude = random.NextDouble() * (90 - (-90)) - 90; // Latitude varia entre -90 e 90
            //double longitude = random.NextDouble() * (180 - (-180)) - 180; // Longitude varia entre -180 e 180
        };

        public Location FindLocationByRegistration(string registration) => new Location
        {
            Lantitude = _random.NextDouble() * (90 - (-90)) - 90,
            Longitude = _random.NextDouble() * (180 - (-180)) - 180

        };

    }
}
