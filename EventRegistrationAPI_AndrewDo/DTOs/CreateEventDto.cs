using System.ComponentModel.DataAnnotations;
using EventRegistrationAPI_AndrewDo.Validation;

namespace EventRegistrationAPI_AndrewDo.DTOs
{
    // Input DTO: the fields a client sends to create an event
    public class CreateEventDto
    {
        [Required(ErrorMessage = "Title is required.")]
        [StringLength(100, ErrorMessage = "Title can't exceed 100 characters.")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Location is required.")]
        public string Location { get; set; } = string.Empty;

        [FutureDate(ErrorMessage = "Event date must be in the future.")]
        public DateTime EventDate { get; set; }

        [Range(1, 500, ErrorMessage = "Capacity must be between 1 and 500.")]
        public int Capacity { get; set; }
    }
}