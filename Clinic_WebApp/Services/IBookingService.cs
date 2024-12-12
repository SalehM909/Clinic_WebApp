using Clinic_WebApp.Models;

namespace Clinic_WebApp.Services
{
    public interface IBookingService
    {
        void BookAppointment(Booking booking);
        List<Booking> GetAppointmentsByClinic(int clinicId);
        IEnumerable<Booking> GetAppointmentsByPatientName(string patientName);
        List<Booking> GetAppointmentsByPatient(int patientId);
    }
}