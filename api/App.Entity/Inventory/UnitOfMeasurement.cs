namespace App.Entity.Inventory
{
    using App.Common.Data;
    public class UnitOfMeasurement : BaseContent
    {
        public UnitOfMeasurement() : base()
        {
        }

        public UnitOfMeasurement(string name, string description)
        {
            this.Name = name;
            this.Description = description;
        }
    }
}