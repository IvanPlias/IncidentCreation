namespace TestWork.Models
{
    public class Account
    {
        public Account()
        {
            Incidents = new HashSet<Incident>();
        }
        public int Id { get; set; }
        public int ContactId { get; set; }
        public string? AccountName { get; set; }
        public Contact? Contact { get; set; }
        public ICollection<Incident> Incidents { get; set; }
    }
}
