namespace App.Entity.Inventory
{
    using App.Common.Data;

    public class Category : BaseContent
    {
        public Category() : base()
        {
        }

        public Category(string name, string description)
        {
            this.Name = name;
            this.Description = description;
        }
    }
}
