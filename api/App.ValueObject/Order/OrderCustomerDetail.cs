namespace App.ValueObject.Order
{
    using App.Common.Data;
    public class OrderCustomerDetail : BaseEntity
    {
        public OrderCustomerDetail(){}
        public OrderCustomerDetail(string name)
        {
            this.Name = name;
        }

        public string Name { get; set; }
    }
}
