using EventRegistrationAPI_AndrewDo.DTOs;

namespace EventRegistrationAPI_AndrewDo.Services
{
    // contract for registration operations. The controller depends on this interface
    public interface IRegistrationService
    {
        // Returns null if the event does not exist
        Task<List<RegistrationDto>?> GetByEventAsync(int eventId);

        // Returns null if the event does not exist anbd throws if a business rule is broken
        Task<RegistrationDto?> CreateAsync(int eventId, CreateRegistrationDto dto);

        // Returns false if the registration does not exist
        Task<bool> DeleteAsync(int id);
    }
}