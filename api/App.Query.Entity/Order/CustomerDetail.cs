namespace App.Query.Entity.Order
{
    public class CustomerDetail
    {
        public string Name { get; set; }
        public CustomerDetail(string name)
        {
            this.Name= name;
        }
    }
}
