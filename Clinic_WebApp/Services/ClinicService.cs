using Clinic_WebApp.Models;
using Clinic_WebApp.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Clinic_WebApp.Services
{
    // ClinicService class implements the IClinicService interface
    // It acts as a service layer, interacting with the IClinicRepo (repository) to manage clinic-related operations.
    public class ClinicService : IClinicService
    {
        // IClinicRepo instance used to interact with the repository layer for clinic operations
        private readonly IClinicRepo _clinicRepo;

        // Constructor that accepts an IClinicRepo and initializes the _clinicRepo field
        // This allows dependency injection of the clinic repository into the service
        public ClinicService(IClinicRepo clinicRepo)
        {
            _clinicRepo = clinicRepo;
        }

        // Method to add a new clinic by delegating the operation to the repository
        public void AddClinic(Clinic clinic)
        {
            // Calls the AddClinic method of the clinic repository to add the clinic
            _clinicRepo.AddClinic(clinic);
        }

        // Method to retrieve a clinic by its unique ID by delegating the operation to the repository
        public Clinic GetClinicById(int clinicId)
        {
            // Calls the GetClinicById method of the clinic repository to get the clinic by ID
            return _clinicRepo.GetClinicById(clinicId);
        }

        // Method to retrieve all clinics by delegating the operation to the repository
        public IEnumerable<Clinic> GetAllClinics()
        {
            // Calls the GetAllClinics method of the clinic repository to get a list of all clinics
            return _clinicRepo.GetAllClinics();
        }

        // Method to remove a clinic by its specialization
        public void RemoveClinicByName(string specialization)
        {
            _clinicRepo.RemoveClinicByName(specialization);
        }
    }
}
