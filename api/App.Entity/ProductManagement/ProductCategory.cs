namespace App.Entity.ProductManagement
{
    using System;
    using App.Common;
    using App.Common.Data;

    public class ProductCategory : BaseContent
    {
        public Guid ParentId { get; set; }
        public ProductCategory()
        {
        }

        public ProductCategory(string name, ItemStatus status, string description, Guid parentId)
        {
            this.Name = name;
            this.Status = status;
            this.Description = description;
            this.ParentId = parentId;
        }
    }
}
