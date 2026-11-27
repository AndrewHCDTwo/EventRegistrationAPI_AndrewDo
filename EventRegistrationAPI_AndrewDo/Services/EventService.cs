using EventRegistrationAPI_AndrewDo.Data;
using EventRegistrationAPI_AndrewDo.DTOs;
using Microsoft.EntityFrameworkCore;

namespace EventRegistrationAPI_AndrewDo.Services
{
    // Does all database work and DTO mapping for events
    // Controllers call this class and never use AppDbContext directly
    public class EventService : IEventService
    {
        private readonly AppDbContext _context;

        public EventService(AppDbContext context)
        {
            _context = context;
        }

        // Returns all events sorted by date
        public async Task<List<EventDto>> GetAllAsync()
        {
            // Select projects straight into the DTO 
            return await _context.Events
                .OrderBy(e => e.EventDate)
                .Select(e => new EventDto
                {
                    EventId = e.EventId,
                    Title = e.Title,
                    Location = e.Location,
                    EventDate = e.EventDate,
                    Capacity = e.Capacity,
                    RegisteredCount = e.Registrations.Count,
                    SpotsLeft = e.Capacity - e.Registrations.Count
                })
                .ToListAsync();
        }

        // Returns one event or null if it does not exist 
        public async Task<EventDto?> GetByIdAsync(int id)
        {
            return await _context.Events
                .Where(e => e.EventId == id)
                .Select(e => new EventDto
                {
                    EventId = e.EventId,
                    Title = e.Title,
                    Location = e.Location,
                    EventDate = e.EventDate,
                    Capacity = e.Capacity,
                    RegisteredCount = e.Registrations.Count,
                    SpotsLeft = e.Capacity - e.Registrations.Count
                })
                .FirstOrDefaultAsync();
        }
    }
}