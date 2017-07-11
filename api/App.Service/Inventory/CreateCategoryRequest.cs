namespace App.Service.Inventory
{
    using App.Common;
    using App.Common.Validation.Attribute;

    public class CreateCategoryRequest
    {
        [ValueInRange("inventory.addOrUpdateCategory.validation.nameTooLong", 0, FormValidationRules.MaxNameLength)]
        [Required("inventory.addOrUpdateCategory.validation.nameRequired")]
        public string Name { get; set; }

        [ValueInRange("inventory.addOrUpdateCategory.validation.descriptionTooLong", 0, FormValidationRules.MaxDescriptionLength)]
        public string Description { get; set; }

        public CreateCategoryRequest(string name, string description)
        {
            this.Name = name;
            this.Description = description;
        }
    }
}
