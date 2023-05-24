using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorldFly.API.Data;
using WorldFly.API.Helpers;
using WorldFly.API.Models;

namespace WorldFly.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FlightController : Controller
    {
        private readonly WorldFlyDbContext _db;
        private readonly BookingRepository _bookingRepository;
        public FlightController(WorldFlyDbContext worldFlyDbContext, BookingRepository _bookingRepo)
        {
            _db = worldFlyDbContext;
            _bookingRepository = _bookingRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFlights()
        {
            var flights = await _db.Flight.ToListAsync();
            return Ok(flights);
        }

        [HttpPost]
        public async Task<IActionResult> AddFlight([FromBody] Flight flightRequest)
        {

            await _db.Flight.AddAsync(flightRequest);
            await _db.SaveChangesAsync();

            return Ok(flightRequest);
        }
        [HttpGet("byUserId/{userId}")]
        public IActionResult GetFlightsByUserId(int userId)
        {
            var flights = _bookingRepository.GetFlightsByUserID(userId);
            return Ok(flights);
        }

    }
}
