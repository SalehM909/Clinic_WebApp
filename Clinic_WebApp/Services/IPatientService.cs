using Clinic_WebApp.Models;

namespace Clinic_WebApp.Services
{
    public interface IPatientService
    {
        void AddPatient(Patient patient);
        IEnumerable<Patient> GetAllPatients();
        Patient GetPatientById(int patientId);
        void RemovePatientByName(string name);
    }
}