namespace EventRegistrationAPI_AndrewDo.DTOs
{
    // Input DTO: the fields a client sends to register for an event
    // Registeredon is not here because the service sets it
    public class CreateRegistrationDto
    {
        public string StudentName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}