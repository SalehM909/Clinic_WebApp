using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ClinicWebApp.Models
{
    public class Patient
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PID { get; set; }

        [Required]
        public string Name { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }

        [JsonIgnore]
        public ICollection<Booking> Bookings { get; set; }
    }

}
