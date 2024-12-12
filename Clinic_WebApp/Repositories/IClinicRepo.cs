using Clinic_WebApp.Models;

namespace Clinic_WebApp.Repositories
{
    public interface IClinicRepo
    {
        void AddClinic(Clinic clinic);
        IEnumerable<Clinic> GetAllClinics();
        Clinic GetClinicById(int clinicId);
        void RemoveClinicByName(string specialization);
    }
}