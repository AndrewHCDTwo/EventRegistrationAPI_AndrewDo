using System.ComponentModel.DataAnnotations;

namespace EventRegistrationAPI_AndrewDo.Validation
{
    //uses data and time to tell past and future events
    public class FutureDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            return value is DateTime date && date > DateTime.Now;
        }
    }
}