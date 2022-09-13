using System.ComponentModel.DataAnnotations.Schema;

namespace Contacts.Models
{
    public class Contact
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public string MobilePhone { get; set; } = String.Empty;
        public string JobTitle { get; set; } = String.Empty;
        [Column(TypeName = "date")]
        public DateTime BirthDate { get; set; }
    }
}
