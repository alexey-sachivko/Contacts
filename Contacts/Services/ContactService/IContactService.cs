namespace Contacts.Services.ContactService
{
    public interface IContactService
    {
        Task<ServiceResponse<List<Contact>>> GetContacts();
        Task<ServiceResponse<Contact>> GetContact(int contactId);
        Task<ServiceResponse<Contact>> CreateContact(Contact contact);
        Task<ServiceResponse<Contact>> UpdateContact(Contact contact);
        Task<ServiceResponse<bool>> DeleteContact(int contactId);
    }
}
