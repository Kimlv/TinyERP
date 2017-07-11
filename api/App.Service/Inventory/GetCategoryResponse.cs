namespace App.Service.Inventory
{
    using App.Common.Data;
    using App.Common.Mapping;
    using App.Entity.Inventory;

    public class GetCategoryResponse : BaseContent, IMappedFrom<Category>
    {
    }
}