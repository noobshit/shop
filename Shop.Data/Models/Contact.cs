namespace Shop.Data.Models
{
    public class Contact
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }

        public Contact Clone()
        {
            var contact = (Contact) this.MemberwiseClone();
            contact.Id = 0;
            return contact;
        }
    }
}
