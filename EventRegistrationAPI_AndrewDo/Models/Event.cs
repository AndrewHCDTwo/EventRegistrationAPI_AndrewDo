using System.ComponentModel.DataAnnotations;

namespace EventRegistrationAPI_AndrewDo.Models
{
    // Represents a campus event that students and staff can register for
    // One Event has many registrations
    public class Event
    {
        public int EventId { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Location { get; set; } = string.Empty;

        public DateTime EventDate { get; set; }

        // Maximum number of registratins allowed for this event
        public int Capacity { get; set; }

        // Navigation property: all registrations for this event
        public ICollection<Registration> Registrations { get; set; } = new List<Registration>();
    }
}