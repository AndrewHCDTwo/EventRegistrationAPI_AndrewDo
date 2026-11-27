using System.ComponentModel.DataAnnotations;

namespace EventRegistrationAPI_AndrewDo.DTOs

{
    // Input DTO: the fields a client sends to register for an event
    // Registeredon is not here because the service sets it
    public class CreateRegistrationDto
    {
        [Required(ErrorMessage = "Student name is required.")]
        [StringLength(100, ErrorMessage = "Student name can't exceed 100 characters.")]
        public string StudentName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; } = string.Empty;
    }
}