using ClinicWebApp.Models;
using ClinicWebApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace ClinicWebApp.Controllers
{
    // ClinicController class handles HTTP requests related to clinic operations
    // It interacts with the IClinicService to manage clinic data.
    [Route("api/clinics")]
    [ApiController] // Specifies that this class is an API controller, which automatically handles model validation and response formatting.
    public class ClinicController : ControllerBase
    {
        // IClinicService instance used to interact with the service layer for clinic operations
        private readonly IClinicService _clinicService;

        // Constructor that accepts an IClinicService and initializes the _clinicService field
        // This allows dependency injection of the clinic service into the controller
        public ClinicController(IClinicService clinicService)
        {
            _clinicService = clinicService;
        }

        // Endpoint to add a new clinic by accepting a Clinic object in the request body
        [HttpPost("add")]
        public IActionResult AddClinic(string Specialization, int NumOfSlots)
        {
            try
            {
                // Creates a new Clinic object using the provided specialization and number of slots
                var clinic = new Clinic
                {
                    Specialization = Specialization,
                    NumberOfSlots = NumOfSlots
                };

                // Calling the synchronous AddClinic method of the clinic service to add the clinic to the system
                _clinicService.AddClinic(clinic);

                // Returns a 201 Created response indicating that the clinic was successfully created
                // The URI of the newly created resource is provided in the response header
                return CreatedAtAction(nameof(GetClinic), new { clinicId = clinic.CID }, clinic);
            }
            catch (ArgumentException ex)
            {
                // If there are validation issues with the input data (e.g., invalid slots or specialization), 
                // a 400 Bad Request status is returned
                return BadRequest(new { message = "Invalid data provided for clinic creation.", details = ex.Message });
            }
            catch (Exception ex)
            {
                // Catch any unexpected errors (e.g., database or service layer failures) and return a 500 Internal Server Error
                return StatusCode(500, new { message = "An unexpected error occurred while creating the clinic.", details = ex.Message });
            }
        }

        // Endpoint to retrieve a clinic by its unique ID
        [HttpGet("{clinicId}")]
        public ActionResult<Clinic> GetClinic(int clinicId)
        {
            try
            {
                // Retrieves the clinic by ID from the clinic service
                var clinic = _clinicService.GetClinicById(clinicId);

                // If no clinic is found, return a 404 Not Found response with an appropriate message
                if (clinic == null)
                {
                    return NotFound(new { message = "Clinic not found." });
                }

                // If the clinic is found, return a 200 OK response with the clinic data
                return Ok(clinic);
            }
            catch (ArgumentException ex)
            {
                // If there is an issue with the provided clinic ID (e.g., invalid format), return a 400 Bad Request status
                return BadRequest(new { message = "Invalid clinic ID provided.", details = ex.Message });
            }
            catch (Exception ex)
            {
                // Catch any unexpected errors (e.g., service or database failures) and return a 500 Internal Server Error
                return StatusCode(500, new { message = "An unexpected error occurred while retrieving the clinic.", details = ex.Message });
            }
        }

        // Endpoint to retrieve all clinics
        [HttpGet]
        public ActionResult<IEnumerable<Clinic>> GetAllClinics()
        {
            try
            {
                // Retrieves all clinics from the clinic service
                var clinics = _clinicService.GetAllClinics();

                // If no clinics are found, return a 404 Not Found response with a message indicating the list is empty
                if (clinics == null || !clinics.Any())
                {
                    return NotFound(new { message = "No clinics found." });
                }

                // If clinics are found, return a 200 OK response with the list of clinics
                return Ok(clinics);
            }
            catch (Exception ex)
            {
                // Catch any unexpected errors (e.g., service or database failures) and return a 500 Internal Server Error
                return StatusCode(500, new { message = "An unexpected error occurred while retrieving the clinics.", details = ex.Message });
            }
        }

        // Endpoint to remove a clinic by its specialization (name)
        [HttpDelete("remove/{specialization}")]
        public IActionResult RemoveClinicByName(string specialization)
        {
            try
            {
                // Attempt to remove the clinic
                _clinicService.RemoveClinicByName(specialization);

                // Return a success response
                return Ok(new { message = "Clinic removed successfully." });
            }
            catch (Exception ex)
            {
                // Catch any unexpected errors and return a 500 Internal Server Error
                return StatusCode(500, new { message = "An unexpected error occurred while removing the clinic.", details = ex.Message });
            }
        }
    }
}
