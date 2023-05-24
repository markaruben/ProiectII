using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorldFly.API.Data;
using WorldFly.API.Models;

namespace WorldFly.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]


    public class BookingController : ControllerBase
    {
        private readonly WorldFlyDbContext _dbContext;
        public BookingController(WorldFlyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public async Task<IActionResult> AddBooking([FromBody] Booking bookingRequest)
        {

            if(bookingRequest == null) return BadRequest();

            if (await BookingExistsAsync(bookingRequest.FlightID, bookingRequest.UserID))
                return BadRequest(new { Message = "Booking already exists!" });

            await _dbContext.Booking.AddAsync(bookingRequest);
            await _dbContext.SaveChangesAsync();


            return Ok(new
            {
                message = "Booking added!"
            });
        }

    
        private Task<bool> BookingExistsAsync(int flightId, int userId)
        => _dbContext.Booking.AnyAsync(b => b.FlightID == flightId && b.UserID == userId);
        
    }

}
