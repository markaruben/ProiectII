using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorldFly.API.Models
{
    public class Booking
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FlightID
        { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserID
        { get; set; }
    }
}
