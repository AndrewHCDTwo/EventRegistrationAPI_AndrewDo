using EventRegistrationAPI_AndrewDo.Data;
using EventRegistrationAPI_AndrewDo.DTOs;
using EventRegistrationAPI_AndrewDo.Models;
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

        
        // Creates a new event from the input DTO and returns it as an EventDto
        public async Task<EventDto> CreateAsync(CreateEventDto dto)
        {
            // map the event
            var newEvent = new Event
            {
                Title = dto.Title,
                Location = dto.Location,
                EventDate = dto.EventDate,
                Capacity = dto.Capacity
            };

            _context.Events.Add(newEvent);
            await _context.SaveChangesAsync();

            // map entity
            return new EventDto
            {
                EventId = newEvent.EventId,
                Title = newEvent.Title,
                Location = newEvent.Location,
                EventDate = newEvent.EventDate,
                Capacity = newEvent.Capacity,
                RegisteredCount = 0,
                SpotsLeft = newEvent.Capacity
            };
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