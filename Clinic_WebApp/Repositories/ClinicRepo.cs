using Clinic_WebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace Clinic_WebApp.Repositories
{
    // ClinicRepo class implements the IClinicRepo interface
    // It is responsible for interacting with the ApplicationDbContext to perform CRUD operations on the Clinic data.
    public class ClinicRepo : IClinicRepo
    {
        // ApplicationDbContext instance used to interact with the database
        private readonly ApplicationDbContext _context;

        // Constructor that accepts an ApplicationDbContext and initializes the _context field
        // This allows dependency injection of the database context into the repository
        public ClinicRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        // Method to add a new Clinic to the database
        public void AddClinic(Clinic clinic)
        {
            // Adds the given clinic entity to the Clinics DbSet in the context
            _context.Clinics.Add(clinic);

            // Saves the changes to the database
            _context.SaveChanges();
        }

        // Method to retrieve a Clinic by its unique ID
        public Clinic GetClinicById(int clinicId)
        {
            // Finds a clinic by its ID in the Clinics DbSet
            // If no clinic is found, it returns null
            return _context.Clinics.Find(clinicId);
        }

        // Method to retrieve all Clinics from the database
        public IEnumerable<Clinic> GetAllClinics()
        {
            // Retrieves all clinics from the Clinics DbSet synchronously
            return _context.Clinics.ToList();
        }

        // Method to remove a clinic by its name (specialization)
        public void RemoveClinicByName(string specialization)
        {
            var clinic = _context.Clinics.FirstOrDefault(c => c.Specialization == specialization);
            if (clinic != null)
            {
                _context.Clinics.Remove(clinic);
                _context.SaveChanges();
            }
        }
    }
}
