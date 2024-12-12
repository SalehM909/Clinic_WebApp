using Clinic_WebApp.Models;
using Clinic_WebApp.Repositories;

namespace Clinic_WebApp.Services
{
    // BookingService class implements the IBookingService interface
    // It acts as a service layer, interacting with the IBookingRepo (repository) to manage booking-related operations.
    public class BookingService : IBookingService
    {
        // IBookingRepo instance used to interact with the repository layer for booking operations
        private readonly IBookingRepo _bookingRepo;

        // Constructor that accepts an IBookingRepo and initializes the _bookingRepo field
        // This allows dependency injection of the booking repository into the service
        public BookingService(IBookingRepo bookingRepo)
        {
            _bookingRepo = bookingRepo;
        }

        // Method to book an appointment by delegating the operation to the repository
        public void BookAppointment(Booking booking)
        {
            // 1. Check if the patient's ID is duplicated
            if (_bookingRepo.IsPatientDuplicate(booking.PatientID))
            {
                throw new InvalidOperationException("This patient has already booked an appointment.");
            }

            // 2. Check if the clinic's ID is duplicated
            if (_bookingRepo.IsClinicDuplicate(booking.ClinicID))
            {
                throw new InvalidOperationException("This clinic is already fully booked for this appointment.");
            }

            // 3. Ensure the appointment date is in the future
            if (booking.Date < DateTime.Now)
            {
                throw new ArgumentException("Appointment date cannot be in the past.");
            }

            // 4. Check if the slot number is already booked or all slots are taken
            if (_bookingRepo.IsSlotTaken(booking.ClinicID, booking.SlotNumber))
            {
                throw new InvalidOperationException($"The slot number {booking.SlotNumber} is already taken.");
            }

            // If all checks pass, book the appointment
            _bookingRepo.BookAppointment(booking);
        }

        // Method to retrieve all appointments for a specific clinic by delegating the operation to the repository
        public List<Booking> GetAppointmentsByClinic(int clinicId)
        {
            // Calls the synchronous GetAppointmentsByClinic method of the booking repository
            return _bookingRepo.GetAppointmentsByClinic(clinicId);
        }

        public IEnumerable<Booking> GetAppointmentsByPatientName(string patientName)
        {
            return _bookingRepo.GetAppointmentsByPatientName(patientName);
        }

        // Method to retrieve all appointments for a specific patient by delegating the operation to the repository
        public List<Booking> GetAppointmentsByPatient(int patientId)
        {
            // Calls the synchronous GetAppointmentsByPatient method of the booking repository
            return _bookingRepo.GetAppointmentsByPatient(patientId);
        }
    }
}
