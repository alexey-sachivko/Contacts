namespace Contacts.Services.ContactService
{
    public class ContactService : IContactService
    {
        private readonly DataContext _context;

        public ContactService(DataContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<Contact>> CreateContact(Contact contact)
        {
            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync();

            return new ServiceResponse<Contact>()
            {
                Data = contact
            };
        }

        public async Task<ServiceResponse<bool>> DeleteContact(int contactId)
        {
            var dbContact = await _context.Contacts.FindAsync(contactId);
            if (dbContact == null)
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Data = false,
                    Message = "Контакт не найден."
                };
            }

            _context.Contacts.Remove(dbContact);
            await _context.SaveChangesAsync();

            return new ServiceResponse<bool> { Data = true };
        }

        public async Task<ServiceResponse<Contact>> GetContact(int contactId)
        {
            var response = new ServiceResponse<Contact>();
            var contact = await _context.Contacts
                .FirstOrDefaultAsync(c => c.Id == contactId);

            if (contact == null)
            {
                response.Success = false;
                response.Message = "Контакт не найден.";
            }
            else
            {
                response.Data = contact;
            }

            return response;
        }

        public async Task<ServiceResponse<List<Contact>>> GetContacts()
        {
            var response = new ServiceResponse<List<Contact>>()
            {
                Data = await _context.Contacts.ToListAsync()
            };

            return response;
        }

        public async Task<ServiceResponse<Contact>> UpdateContact(Contact contact)
        {
            var dbContact = await _context.Contacts.FindAsync(contact.Id);
            if (dbContact == null)
            {
                return new ServiceResponse<Contact>()
                {
                    Success = false,
                    Message = "Контакт не найден."
                };
            }

            dbContact.Name = contact.Name;
            dbContact.MobilePhone = contact.MobilePhone;
            dbContact.JobTitle = contact.JobTitle;
            dbContact.BirthDate = contact.BirthDate;

            await _context.SaveChangesAsync();

            return new ServiceResponse<Contact>()
            {
                Data = contact
            };
        }
    }
}
