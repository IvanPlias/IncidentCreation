using System.Security.Principal;

namespace TestWork.Models
{
    public class Contact
    {
        public Contact()
        {
            Accounts = new HashSet<Account>();
        }
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public virtual ICollection<Account> Accounts { get; set; }

    }
}
