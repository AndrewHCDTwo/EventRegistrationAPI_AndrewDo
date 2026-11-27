using EventRegistrationAPI_AndrewDo.DTOs;
using EventRegistrationAPI_AndrewDo.Services;
using Microsoft.AspNetCore.Mvc;

namespace EventRegistrationAPI_AndrewDo.Controllers
{
    // Handles HTTP requests for events All logic is in IEventService
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventsController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet] // GET: api/Events
        public async Task<ActionResult<IEnumerable<EventDto>>> GetEvents()
        {
            var events = await _eventService.GetAllAsync();
            return Ok(events); // 200 OK
        }    
        
        [HttpPost] // POST: api/Events
        public async Task<ActionResult<EventDto>> CreateEvent(CreateEventDto dto)
        {
            // [ApiController] returns 400 automatically if the DTO fails validation
            var createdEvent = await _eventService.CreateAsync(dto);

            return CreatedAtAction(nameof(GetEvent), new { id = createdEvent.EventId }, createdEvent); // 201 Created
        }

        [HttpGet("{id}")] // GET: api/Events/5
        public async Task<ActionResult<EventDto>> GetEvent(int id)
        {
            var eventDto = await _eventService.GetByIdAsync(id);

            if (eventDto == null)
            {
                return NotFound(); // 404 Not Found
            }

            return Ok(eventDto); // 200 ok good
        }
    }
}