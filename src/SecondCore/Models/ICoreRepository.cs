using System.Collections.Generic;
using System.Threading.Tasks;

namespace SecondCore.Models
{
    public interface ICoreRepository
    {
        IEnumerable<Trip> GetAllTrips();
        void AddTrip(Trip trip);
        void AddStop(string tripName, Stop newStop);
        Task<bool> SaveChangesAsync();
        Trip GetTripByName(string tripName);
    }
}