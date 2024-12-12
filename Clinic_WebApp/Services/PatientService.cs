using Clinic_WebApp.Models;
using Clinic_WebApp.Repositories;

namespace Clinic_WebApp.Services
{
    // PatientService class implements the IPatientService interface
    // It acts as a service layer, interacting with the IPatientRepo (repository) to manage patient-related operations.
    public class PatientService : IPatientService
    {
        // IPatientRepo instance used to interact with the repository layer for patient operations
        private readonly IPatientRepo _patientRepo;

        // Constructor that accepts an IPatientRepo and initializes the _patientRepo field
        // This allows dependency injection of the patient repository into the service
        public PatientService(IPatientRepo patientRepo)
        {
            _patientRepo = patientRepo;
        }

        // Method to add a new patient by delegating the operation to the repository
        public void AddPatient(Patient patient)
        {
            // Calls the AddPatient method of the patient repository to add the patient
            _patientRepo.AddPatient(patient);
        }

        // Method to retrieve a patient by their unique ID by delegating the operation to the repository
        public Patient GetPatientById(int patientId)
        {
            // Calls the GetPatientById method of the patient repository to get the patient by ID
            return _patientRepo.GetPatientById(patientId);
        }

        // Method to retrieve all patients by delegating the operation to the repository
        public IEnumerable<Patient> GetAllPatients()
        {
            // Calls the GetAllPatients method of the patient repository to get a list of all patients
            return _patientRepo.GetAllPatients();
        }

        // Method to remove a clinic by its specialization
        public void RemovePatientByName(string specialization)
        {
            _patientRepo.RemovePatientByName(specialization);
        }
    }
}
