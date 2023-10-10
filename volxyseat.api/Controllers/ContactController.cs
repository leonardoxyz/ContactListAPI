using Microsoft.AspNetCore.Mvc;
using volxyseat.Domain.Models;
using volxyseat.Domain.Services;

namespace volxyseat.api.Controllers;

[ApiController]
[Route("api/controller")]
public class ContactController : Controller
{
    private readonly ContactService _contactService;

    public ContactController(ContactService contactService)
    {
        _contactService = contactService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateContact(Contact contact)
    {
        var createdContact = await _contactService.CreateContactAsync(contact);
        return CreatedAtAction(nameof(GetContact), new {id = createdContact.Id}, createdContact);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllContacts()
    {
        var contacts = await _contactService.GetContactsAsync();
        return Ok(contacts);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetContact(Guid id)
    {
        var contact = await _contactService.GetContactByIdAsync(id);
        if (contact == null)
        {
            return NotFound();
        }
        return Ok(contact);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateContact(Guid id, Contact contact)
    {
        if (contact == null || id != contact.Id)
        {
            return BadRequest(); // Solicitação inválida
        }

        var updatedContact = await _contactService.UpdateContactAsync(id, contact);
        if (updatedContact == null)
        {
            return NotFound(); // Contato não encontrado
        }

        return Ok(updatedContact);
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteContact(Guid id)
    {
        var result = await _contactService.DeleteContactAsync(id);
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }
}
