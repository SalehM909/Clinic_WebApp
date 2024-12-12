using ClinicWebApp.Models;
using ClinicWebApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace ClinicWebApp.Controllers
{
    // PatientController class handles HTTP requests related to patient operations
    // It interacts with the IPatientService to manage patient data.
    [Route("api/patients")]
    [ApiController] // Specifies that this class is an API controller, which automatically handles model validation and response formatting.
    public class PatientController : ControllerBase
    {
        // IPatientService instance used to interact with the service layer for patient operations
        private readonly IPatientService _patientService;

        // Constructor that accepts an IPatientService and initializes the _patientService field
        // This allows dependency injection of the patient service into the controller
        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        // Endpoint to add a new patient by accepting a Patient object in the request body
        [HttpPost("add")]
        public IActionResult AddPatient(string name, int age, string gender)
        {
            try
            {
                // Creates a new Patient object using the provided input values
                var patient = new Patient
                {
                    Name = name,
                    Age = age,
                    Gender = gender
                };

                // Calls the AddPatient method of the patient service to add the patient to the system
                _patientService.AddPatient(patient);

                // Returns a 201 Created response, indicating the patient was successfully created
                return CreatedAtAction(nameof(GetPatient), new { patientId = patient.PID }, patient);
            }
            catch (ArgumentException ex)
            {
                // If there are validation errors (e.g., invalid age, gender), return a 400 Bad Request status
                return BadRequest(new { message = "Invalid data provided for patient creation.", details = ex.Message });
            }
            catch (Exception ex)
            {
                // Catch any unexpected errors that may occur during patient creation
                // Return a 500 Internal Server Error status with details of the exception
                return StatusCode(500, new { message = "An unexpected error occurred while creating the patient.", details = ex.Message });
            }
        }

        // Endpoint to retrieve a patient by their unique ID
        [HttpGet("{patientId}")]
        public ActionResult<Patient> GetPatient(int patientId)
        {
            try
            {
                // Retrieves the patient by ID from the patient service
                var patient = _patientService.GetPatientById(patientId);

                // If no patient is found, return a 404 Not Found response
                if (patient == null)
                {
                    return NotFound(new { message = "Patient not found." });
                }

                // If the patient is found, return a 200 OK response with the patient data
                return Ok(patient);
            }
            catch (ArgumentException ex)
            {
                // If there is an issue with the provided patient ID (e.g., invalid format), return a 400 Bad Request status
                return BadRequest(new { message = "Invalid patient ID.", details = ex.Message });
            }
            catch (Exception ex)
            {
                // Catch any unexpected errors (e.g., database or network issues)
                // Return a 500 Internal Server Error status code
                return StatusCode(500, new { message = "An unexpected error occurred while retrieving the patient.", details = ex.Message });
            }
        }

        // Endpoint to retrieve all patients
        [HttpGet]
        public ActionResult<IEnumerable<Patient>> GetAllPatients()
        {
            try
            {
                // Retrieves all patients from the patient service
                var patients = _patientService.GetAllPatients();

                // If no patients are found, return a 404 Not Found response with a message
                if (patients == null || !patients.Any())
                {
                    return NotFound(new { message = "No patients found." });
                }

                // If patients are found, return a 200 OK response with the list of patients
                return Ok(patients);
            }
            catch (Exception ex)
            {
                // Catch any unexpected errors (e.g., database or service issues)
                // Return a 500 Internal Server Error status code with the exception details for debugging
                return StatusCode(500, new { message = "An unexpected error occurred while retrieving the patients.", details = ex.Message });
            }
        }

        // Endpoint to remove a clinic by its specialization (name)
        [HttpDelete("remove/{name}")]
        public IActionResult RemoveClinicByName(string name)
        {
            try
            {
                // Attempt to remove the patient
                _patientService.RemovePatientByName(name);

                // Return a success response
                return Ok(new { message = "Patient removed successfully." });
            }
            catch (Exception ex)
            {
                // Catch any unexpected errors and return a 500 Internal Server Error
                return StatusCode(500, new { message = "An unexpected error occurred while removing the patient.", details = ex.Message });
            }
        }
    }

}
