using ClinicWebApp.Models;
using ClinicWebApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace ClinicWebApp.Controllers
{
    // BookingController class handles HTTP requests related to booking appointments
    // It interacts with the IBookingService to manage booking operations.
    [Route("api/bookings")]
    [ApiController] // Specifies that this class is an API controller, which automatically handles model validation and response formatting.
    public class BookingController : ControllerBase
    {
        // IBookingService instance used to interact with the service layer for booking operations
        private readonly IBookingService _bookingService;

        // Constructor that accepts an IBookingService and initializes the _bookingService field
        // This allows dependency injection of the booking service into the controller
        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        // Endpoint to book an appointment by accepting a Booking object in the request body
        // It accepts patientID, clinicID, date, and slotNumber to create a booking
        [HttpPost("book")]
        public IActionResult BookAppointment(int patientID, int clinicID, DateTime date, int slotNumber)
        {
            try
            {
                // Creating a new Booking object based on the parameters received
                var booking = new Booking
                {
                    PatientID = patientID,
                    ClinicID = clinicID,
                    Date = date,
                    SlotNumber = slotNumber
                };

                // Calling the service method synchronously
                _bookingService.BookAppointment(booking);

                // Returns a 200 OK response when the appointment is successfully booked
                return Ok();
            }

            catch (ArgumentException ex)
            {
                // Return a 400 Bad Request if there are validation issues (e.g., invalid patient or clinic)
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                // Return a 500 Internal Server Error for any unexpected issues
                return StatusCode(500, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }

        // Endpoint to retrieve all appointments for a specific clinic by clinic ID
        [HttpGet("appointmentsByClinic/{clinicId}")]
        public ActionResult<IEnumerable<Booking>> GetAppointmentsByClinic(int clinicId)
        {

            try
            {
                // Retrieves appointments for the given clinic ID from the booking service
                var appointments = _bookingService.GetAppointmentsByClinic(clinicId); // Calling synchronously

                if (appointments == null || !appointments.Any())
                {
                    // Return 404 Not Found if no appointments are found for the given clinic
                    return NotFound(new { message = "No appointments found for the specified clinic." });
                }

                // Returns the appointments as a 200 OK response
                return Ok(appointments);
            }
            catch (Exception ex)
            {
                // Catch any unexpected exceptions and return a 500 Internal Server Error response
                return StatusCode(500, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }

        // Endpoint to retrieve all appointments for a specific patient by patient name
        [HttpGet("appointmentsByPatientName/{patientName}")]
        public ActionResult<IEnumerable<Booking>> GetAppointmentsByPatientName(string patientName)
        {
            try
            {
                // Retrieves appointments for the given patient name from the booking service
                var appointments = _bookingService.GetAppointmentsByPatientName(patientName);

                if (appointments == null || !appointments.Any()) // If no appointments are found, return 404 Not Found
                {
                    return NotFound(new { message = "No appointments found for the specified patient." });
                }
                return Ok(appointments); // If appointments are found, return them with a 200 OK response
            }
            catch (Exception ex)
            {
                // If an unexpected error occurs, return a 500 Internal Server Error response
                return StatusCode(500, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }

        // Endpoint to retrieve all appointments for a specific patient by patient ID
        [HttpGet("appointmentsByPatient/{patientId}")]
        public ActionResult<IEnumerable<Booking>> GetAppointmentsByPatient(int patientId)
        {
            try
            {
                // Retrieves appointments for the given patient ID from the booking service
                var appointments = _bookingService.GetAppointmentsByPatient(patientId); // Calling synchronously

                // If no appointments are found for the given patientId, return 404 Not Found
                if (appointments == null || !appointments.Any())
                {
                    return NotFound(new { message = "No appointments found for the specified patient." });
                }

                // Returns the appointments as a 200 OK response
                return Ok(appointments);
            }
            catch (Exception ex)
            {
                // Catch any unexpected exceptions and return a 500 Internal Server Error response
                return StatusCode(500, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }
    }
}
