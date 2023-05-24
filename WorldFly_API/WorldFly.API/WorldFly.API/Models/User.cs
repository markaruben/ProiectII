using System.ComponentModel.DataAnnotations;

namespace WorldFly.API.Models
{
    public class User
    {
        [Key]
        public int UserID
        { get; set; }
        public string UserName { get; set; }
        [Required]
        public string UserEmail { get; set; }
        [Required]
        public string UserPassowrd { get; set; }

        public string Token { get; set; }


    }
}
