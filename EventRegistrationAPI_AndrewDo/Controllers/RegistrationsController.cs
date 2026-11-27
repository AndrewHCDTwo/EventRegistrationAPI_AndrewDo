using EventRegistrationAPI_AndrewDo.DTOs;
using EventRegistrationAPI_AndrewDo.Services;
using Microsoft.AspNetCore.Mvc;

namespace EventRegistrationAPI_AndrewDo.Controllers
{
    // Handles HTTP requests for registrations 
    // The routes dont all start with the controller name, so the class route is just "api"
    [Route("api")]
    [ApiController]
    public class RegistrationsController : ControllerBase
    {
        private readonly IRegistrationService _registrationService;

        public RegistrationsController(IRegistrationService registrationService)
        {
            _registrationService = registrationService;
        }

        [HttpGet("events/{id}/registrations")] // GET: api/events/2/registrations
        public async Task<ActionResult<IEnumerable<RegistrationDto>>> GetRegistrations(int id)
        {
            var registrations = await _registrationService.GetByEventAsync(id);

            if (registrations == null)
            {
                return NotFound(); // 404 Not Found
            }

            return Ok(registrations); // 200 OK
        }

        [HttpPost("events/{id}/registrations")] // POST: api/events/2/registrations
        public async Task<ActionResult<RegistrationDto>> CreateRegistration(int id, CreateRegistrationDto dto)
        {
            try
            {
                var created = await _registrationService.CreateAsync(id, dto);

                if (created == null)
                {
                    return NotFound(); // 404 Not Found (event does not exist)
                }

                // No GET-one-registration endpoint exists, so CreatedAtAction cant be used here
                return StatusCode(StatusCodes.Status201Created, created); // 201 Created
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message); // 400 Bad Request (past event)
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message); // 409 Conflict (duplicate)
            }
        }

        [HttpDelete("registrations/{id}")] // DELETE: api/registrations/1
        public async Task<IActionResult> DeleteRegistration(int id)
        {
            var result = await _registrationService.DeleteAsync(id);

            if (!result)
            {
                return NotFound(); // 404 Not Found
            }

            return NoContent(); // 204 No Content
        }
    }
}