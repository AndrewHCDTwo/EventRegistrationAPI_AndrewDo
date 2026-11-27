namespace EventRegistrationAPI_AndrewDo.DTOs
{
    // Input DTO: the fields a client sends to create an event
    public class CreateEventDto
    {
        public string Title { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public DateTime EventDate { get; set; }
        public int Capacity { get; set; }
    }
}