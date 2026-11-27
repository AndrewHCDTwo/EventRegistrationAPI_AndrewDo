using EventRegistrationAPI_AndrewDo.DTOs;

namespace EventRegistrationAPI_AndrewDo.Services
{
    // Contract for event operations thhe controller depends on this interface
    public interface IEventService
    {
        Task<List<EventDto>> GetAllAsync();
        Task<EventDto?> GetByIdAsync(int id);
    }
}