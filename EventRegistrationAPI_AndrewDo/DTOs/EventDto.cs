namespace EventRegistrationAPI_AndrewDo.DTOs
{
    // Output DTO: what the API sends back for an event
    // RegisteredCount and SpotsLeft are calculated, not stored in the database
    public class EventDto
    {
        public int EventId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public DateTime EventDate { get; set; }
        public int Capacity { get; set; }
        public int RegisteredCount { get; set; }
        public int SpotsLeft { get; set; }
    }
}