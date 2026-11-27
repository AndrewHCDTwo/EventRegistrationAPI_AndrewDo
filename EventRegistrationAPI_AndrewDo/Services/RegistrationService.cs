using EventRegistrationAPI_AndrewDo.Data;
using EventRegistrationAPI_AndrewDo.DTOs;
using EventRegistrationAPI_AndrewDo.Models;
using Microsoft.EntityFrameworkCore;

namespace EventRegistrationAPI_AndrewDo.Services
{
    // this does all database work, DTO mapping and business rules for registrations
    
    public class RegistrationService : IRegistrationService
    {
        private readonly AppDbContext _context;

        public RegistrationService(AppDbContext context)
        {
            _context = context;
        }

        // Returns all registrations for one event, or null if the event does not exist
        public async Task<List<RegistrationDto>?> GetByEventAsync(int eventId)
        {
            var ev = await _context.Events.FindAsync(eventId);
            if (ev == null)
            {
                return null;
            }

            return await _context.Registrations
                .Where(r => r.EventId == eventId)
                .OrderBy(r => r.RegisteredOn)
                .Select(r => new RegistrationDto
                {
                    RegistrationId = r.RegistrationId,
                    EventTitle = r.Event.Title,
                    StudentName = r.StudentName,
                    Email = r.Email,
                    RegisteredOn = r.RegisteredOn
                })
                .ToListAsync();
        }

        // registers someone for an event. The rules are checked in the required order
        public async Task<RegistrationDto?> CreateAsync(int eventId, CreateRegistrationDto dto)
        {
            // Rule 1: the event must exist (null means 404 in the controller)
            var ev = await _context.Events.FindAsync(eventId);
            if (ev == null)
            {
                return null;
            }

            // Rule 2: no registration for events that already happened (should still be400 series)
            if (ev.EventDate <= DateTime.Now)
            {
                throw new ArgumentException("Registration is closed for past events.");
            }

            // Rule 3: no duplicate email for this event, case-insensitive (400 sereies)
            bool duplicate = await _context.Registrations.AnyAsync(r =>
                r.EventId == eventId && r.Email.ToLower() == dto.Email.ToLower());
            if (duplicate)
            {
                throw new InvalidOperationException("This email is already registered for this event.");
            }

            // Rule 4: the event must still have a free spot (400 series)
            int registeredCount = await _context.Registrations.CountAsync(r => r.EventId == eventId);
            if (registeredCount >= ev.Capacity)
            {
                throw new InvalidOperationException("This event is full.");
            }

            // Map DTO > entity; RegisteredOn is set by the service, not the client
            var registration = new Registration
            {
                EventId = eventId,
                StudentName = dto.StudentName,
                Email = dto.Email,
                RegisteredOn = DateTime.Now
            };

            _context.Registrations.Add(registration);
            await _context.SaveChangesAsync();

            // Map entity > DTO
            return new RegistrationDto
            {
                RegistrationId = registration.RegistrationId,
                EventTitle = ev.Title,
                StudentName = registration.StudentName,
                Email = registration.Email,
                RegisteredOn = registration.RegisteredOn
            };
        }

        // Cancels a registration. This frees a spot, so SpotsLeft goes up by one
        public async Task<bool> DeleteAsync(int id)
        {
            var registration = await _context.Registrations.FindAsync(id);
            if (registration == null)
            {
                return false;
            }

            _context.Registrations.Remove(registration);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}