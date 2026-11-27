using System.ComponentModel.DataAnnotations;

namespace EventRegistrationAPI_AndrewDo.Models
{
    // Represents one persons registration for one Event.
    // Many Registrations belong to one Event.
    public class Registration
    {
        public int RegistrationId { get; set; }

        // Foreign key to the Event this registration belongs to
        public int EventId { get; set; }

        [Required]
        public string StudentName { get; set; } = string.Empty;

        [Required]
        public string Email { get; set; } = string.Empty;

        // Set automatically by the service when the registration is created
        public DateTime RegisteredOn { get; set; }

        // Navigation property: the Event this registration is for
        public Event Event { get; set; } = null!;
    }
}