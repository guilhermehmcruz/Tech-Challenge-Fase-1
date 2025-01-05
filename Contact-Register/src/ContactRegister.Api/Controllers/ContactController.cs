using ContactRegister.Application.DTOs;
using ContactRegister.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace ContactRegister.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ContactController : ControllerBase
{
    private readonly ILogger<ContactController> _logger;
    private readonly IContactService _contactService;

    public ContactController(ILogger<ContactController> logger, IContactService contactService)
    {
        _logger = logger;
        _contactService = contactService;
    }

    [HttpGet("[action]/{id:int}")]
    public async Task<IActionResult> GetContact(int id)
    {
        var contact = await _contactService.GetContactByIdAsync(id);
        if(contact.Value == null)
        {
            return NotFound();
        }
        return Ok(contact.Value);
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> GetContacts(
        [FromQuery] string? firstName,
        [FromQuery] string? lastName,
        [FromQuery] string? email,
        [FromQuery] string? city,
        [FromQuery] string? state,
        [FromQuery] string? postalCode,
        [FromQuery] string? addressLine1,
        [FromQuery] string? addressLine2,
        [FromQuery] string? homeNumber,
        [FromQuery] string? mobileNumber,
        [FromQuery] int dddCode = 0,
        [FromQuery] int skip = 0,
        [FromQuery] int take = 50)
    {
        var contact = await _contactService.GetContactsAsync(firstName, lastName, email, dddCode, city, state, postalCode, addressLine1, addressLine2, homeNumber, mobileNumber);
        if (contact.Value == null)
        {
            return NotFound();
        }
        return Ok(contact.Value.Skip(skip).Take(take));
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> CreateContact([FromBody] ContactDto contact)
    {
        var result = await _contactService.AddContactAsync(contact);

        if (result.IsError)
            return BadRequest(result.Errors);

        return Ok();
    }

	[HttpPut("[action]/{id:int}")]
	public async Task<IActionResult> UpdateContact([FromRoute] int id, [FromBody] ContactDto contact)
	{
		var result = await _contactService.UpdateContactAsync(id, contact);

		if (result.IsError)
			return BadRequest(result.Errors);

		return Ok();
	}
}