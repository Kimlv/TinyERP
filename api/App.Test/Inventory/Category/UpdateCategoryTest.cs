namespace App.Service.Test.Inventory.Category
{
    using App.Common;
    using App.Common.DI;
    using App.Common.UnitTest;
    using App.Common.Validation;
    using App.Service.Inventory;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;

    [TestClass]
    public class UpdateCategoryTest : BaseUnitTest
    {
        private CreateCategoryResponse category;
        [TestInitialize]
        protected override void OnInit()
        {
            base.OnInit();
            string name = "Name" + Guid.NewGuid();
            string desc = "Desc" + Guid.NewGuid();
            CreateCategoryRequest request = new CreateCategoryRequest(name, desc);
            ICategoryService service = IoC.Container.Resolve<ICategoryService>();
            this.category = service.Create(request);
        }

        [TestMethod]
        public void Inventory_Category_UpdateCategory_ShouldBeSuccess_WithValidRequest()
        {
            UpdateCategoryRequest updateCategoryRequest = new UpdateCategoryRequest()
            {
                Id = this.category.Id,
                Name = "Name" + Guid.NewGuid(),
                Description = "Desc of category updated"
            };

            this.UpdateCategory(updateCategoryRequest);
            ICategoryService service = IoC.Container.Resolve<ICategoryService>();
            var categoryAfterUpdate = service.GetCategory(this.category.Id);
            Assert.IsTrue(this.category.Name != categoryAfterUpdate.Name);
        }

        [TestMethod]
        public void Inventory_Category_UpdateCateroy_ShouldGetException_WithEmptyName()
        {
            try
            {
                UpdateCategoryRequest updateCategoryRequest = new UpdateCategoryRequest()
                {
                    Id = this.category.Id,
                    Name = string.Empty,
                    Description = "Des of category updated"
                };

                this.UpdateCategory(updateCategoryRequest);
                Assert.IsTrue(false);
            }
            catch (ValidationException exception)
            {
                Assert.IsTrue(exception.HasExceptionKey("inventory.addOrUpdateCategory.validation.nameRequired"));
            }
        }

        [TestMethod]
        public void Inventory_Category_UpdateCategory_ShouldGetException_WithNameTooLong()
        {
            try
            {
                UpdateCategoryRequest updateCategoryRequest = new UpdateCategoryRequest()
                {
                    Id = this.category.Id,
                    Name = "Name of category" + new string('g', FormValidationRules.MaxNameLength),
                    Description = "Des of category updated"
                };

                this.UpdateCategory(updateCategoryRequest);
                Assert.IsTrue(false);
            }
            catch (ValidationException exception)
            {
                Assert.IsTrue(exception.HasExceptionKey("inventory.addOrUpdateCategory.validation.nameTooLong"));
            }
        }

        [TestMethod]
        public void Inventory_Category_UpdateCategory_ShouldGetException_WithDuplicateName()
        {
            try
            {
                string otherName = "Name" + Guid.NewGuid();
                string otherDescription = "Other description";
                CreateCategoryRequest createCategoryRequest = new CreateCategoryRequest(otherName, otherDescription);
                ICategoryService service = IoC.Container.Resolve<ICategoryService>();
                CreateCategoryResponse otherCategory = service.Create(createCategoryRequest);

                UpdateCategoryRequest updateCategoryRequest = new UpdateCategoryRequest()
                {
                    Id = otherCategory.Id,
                    Name = this.category.Name,
                    Description = "Description of category updated"
                };

                this.UpdateCategory(updateCategoryRequest);
                Assert.IsTrue(false);
            }
            catch (ValidationException exception)
            {
                Assert.IsTrue(exception.HasExceptionKey("inventory.addOrUpdateCategory.validation.nameAlreadyExisted"));
            }
        }

        [TestMethod]
        public void Inventory_Category_UpdateCategory_ShouldGetException_WithDescriptionTooLong()
        {
            try
            {
                UpdateCategoryRequest updateCategoryRequest = new UpdateCategoryRequest()
                {
                    Id = this.category.Id,
                    Name = "Name" + Guid.NewGuid(),
                    Description = "Description too" + new string('g', FormValidationRules.MaxDescriptionLength)
                };

                this.UpdateCategory(updateCategoryRequest);
                Assert.IsTrue(false);
            }
            catch (ValidationException exception)
            {
                Assert.IsTrue(exception.HasExceptionKey("inventory.addOrUpdateCategory.validation.descriptionTooLong"));
            }
        }

        [TestMethod]
        public void Inventory_Category_UpdateCategory_ShouldGetException_WithEmptyCategoryId()
        {
            try
            {
                UpdateCategoryRequest updateCategoryRequest = new UpdateCategoryRequest()
                {
                    Id = Guid.Empty,
                    Name = "Name" + Guid.NewGuid(),
                    Description = "Description of category updated"
                };
                ICategoryService service = IoC.Container.Resolve<ICategoryService>();
                service.Update(updateCategoryRequest);
                Assert.IsTrue(false);
            }
            catch (ValidationException exception)
            {
                Assert.IsTrue(exception.HasExceptionKey("inventory.addOrUpdateCategory.validation.idRequired"));
            }
        }

        private void UpdateCategory(UpdateCategoryRequest request)
        {
            ICategoryService service = IoC.Container.Resolve<ICategoryService>();
            service.Update(request);
        }
    }
}