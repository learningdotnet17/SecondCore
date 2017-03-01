using System.Collections.Generic;
using System.Threading.Tasks;

namespace SecondCore.Models
{
    public interface ICoreRepository
    {
        IEnumerable<Trip> GetAllTrips();
        void AddTrip(Trip trip);
        Task<bool> SaveChangesAsync();
        Trip GetTripByName(string tripName);
    }
}