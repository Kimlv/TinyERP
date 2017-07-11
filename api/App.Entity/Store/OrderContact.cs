namespace App.Entity.Store
{
    using App.Common.Data;

    public class OrderContact : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public OrderContact() : base()
        {
        }

        public OrderContact(string name, string email, string phone) : this()
        {
            this.Name = name;
            this.Email = email;
            this.Phone = phone;
        }
    }
}
