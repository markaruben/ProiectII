using Microsoft.EntityFrameworkCore;
using WorldFly.API.Data;
using WorldFly.API.Models;

namespace WorldFly.API.Helpers
{
    public class BookingRepository
    {
        private readonly WorldFlyDbContext _dbContext;

        public BookingRepository(WorldFlyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public  List<Flight> GetFlightsByUserID(int userId)
        {
            List<Flight> flights = _dbContext.Booking
                .Where(b => b.UserID == userId)
                .Join(_dbContext.Flight,
                    booking => booking.FlightID,
                    flight => flight.FlightID,
                    (booking, flight) => flight)
                .ToList();

            return flights;
        }
    }
}
