namespace App.Test.Inventory.Category
{
    using App.Common.UnitTest;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using App.Common.DI;
    using App.Common.Validation;
    using App.Service.Inventory;
    using System;

    [TestClass]
    public class DeleteCategoryTest : BaseUnitTest
    {
        [TestMethod]
        public void Inventory_Category_DeleteCategory_ShouldGetException_WithEmptyId()
        {
            try
            {
                Guid id = Guid.Empty;
                this.DeleteCategory(id);
                Assert.IsTrue(false);
            }
            catch (ValidationException ex)
            {
                Assert.IsTrue(ex.HasExceptionKey("inventory.categories.validation.categoryIsInvalid"));
            }
        }

        [TestMethod]
        public void Inventory_Category_DeleteCategory_ShouldGetException_WithInvalidId()
        {
            try
            {
                Guid categoryId = Guid.NewGuid();
                this.DeleteCategory(categoryId);
                Assert.IsTrue(false);
            }
            catch (ValidationException ex)
            {
                Assert.IsTrue(ex.HasExceptionKey("inventory.categories.validation.categoryIsInvalid"));
            }
        }

        [TestMethod]
        public void Inventory_Category_DeleteCategory_ShouldBeSuccess_WithValidRequest()
        {
            string name = "Name of category";
            string description = "Description of category";
            ICategoryService categoryService = IoC.Container.Resolve<ICategoryService>();
            CreateCategoryResponse category = this.CreateCategoryItem(name, description);
            this.DeleteCategory(category.Id);
            GetCategoryResponse deletedCategory = categoryService.GetCategory(category.Id);
            Assert.IsNull(deletedCategory);
        }

        private CreateCategoryResponse CreateCategoryItem(string name, string desc)
        {
            CreateCategoryRequest request = new CreateCategoryRequest(name, desc);
            ICategoryService service = IoC.Container.Resolve<ICategoryService>();
            CreateCategoryResponse category = service.Create(request);
            return category;
        }

        private void DeleteCategory(Guid id)
        {
            ICategoryService service = IoC.Container.Resolve<ICategoryService>();
            service.Delete(id);
        }
    }
}
