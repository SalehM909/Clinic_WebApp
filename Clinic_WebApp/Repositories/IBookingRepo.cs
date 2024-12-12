using Clinic_WebApp.Models;

namespace Clinic_WebApp.Repositories
{
    public interface IBookingRepo
    {
        void BookAppointment(Booking booking);
        List<Booking> GetAppointmentsByClinic(int clinicId);
        List<Booking> GetAppointmentsByPatient(int patientId);
        IEnumerable<Booking> GetAppointmentsByPatientName(string patientName);
        bool IsClinicDuplicate(int clinicID);
        bool IsPatientDuplicate(int patientID);
        bool IsSlotTaken(int clinicID, int slotNumber);
    }
}