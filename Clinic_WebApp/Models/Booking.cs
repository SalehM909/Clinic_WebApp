using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicWebApp.Models
{
    // Specifies that the combination of BookingID, PatientID, and ClinicID together form a unique primary key for the booking.
    [PrimaryKey(nameof(BookingID), nameof(PatientID), nameof(ClinicID))]
    public class Booking
    {
        public int BookingID { get; set; }

        [ForeignKey("Patient")]
        public int PatientID { get; set; }
        public Patient Patient { get; set; } // This allows access to the full Patient details for this booking

        [ForeignKey("Clinic")]
        public int ClinicID { get; set; }
        public Clinic Clinic { get; set; } // This allows access to the full Clinic details for this booking

        public DateTime Date { get; set; }
        public int SlotNumber { get; set; }
    }

}
