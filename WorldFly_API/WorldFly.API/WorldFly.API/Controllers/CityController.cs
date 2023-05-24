using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorldFly.API.Data;
using WorldFly.API.Models;

namespace WorldFly.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CityController : Controller
    {
        private readonly WorldFlyDbContext _db;
        public CityController(WorldFlyDbContext worldFlyDbContext)
        {
            _db = worldFlyDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCities()
        {
         var cities = await _db.City.ToListAsync();
            return Ok(cities);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<City>> GetCity(Guid id)
        {
            return null;

        }
        

        [HttpPost]
        public async Task<IActionResult> AddCity([FromBody] City cityRequest)
        {
            cityRequest.CityID = Guid.NewGuid();
            await _db.City.AddAsync(cityRequest);
            await _db.SaveChangesAsync();

            return Ok(cityRequest);
        }
    }
}
