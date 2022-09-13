using Microsoft.AspNetCore.Mvc;

namespace Contacts.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<Contact>>>> GetContacts()
        {
            var result = await _contactService.GetContacts();

            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ServiceResponse<List<Contact>>>> GetContact(int id)
        {
            var result = await _contactService.GetContact(id);

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<Contact>>> CreateContact(Contact contact)
        {
            var result = await _contactService.CreateContact(contact);

            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<Contact>>> UpdateContact(Contact contact)
        {
            var result = await _contactService.UpdateContact(contact);

            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ServiceResponse<bool>>> DeleteContact(int id)
        {
            var result = await _contactService.DeleteContact(id);

            return Ok(result);
        }
    }
}
