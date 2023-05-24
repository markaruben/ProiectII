using System.ComponentModel.DataAnnotations;

namespace WorldFly.API.Models
{
    public class Flight
    {
        [Key]
        public int FlightID
        { get; set; }
        public string departureCity { get; set; }
        public string arrivalCity { get; set; }
        public Guid arrivalCityID { get; set; }
        public string departureTime { get; set; }

        public string arrivalTime { get; set; }

    }
}
