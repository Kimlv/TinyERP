namespace App.Service.Impl.Inventory
{
    using System.Collections.Generic;
    using App.Service.Inventory;
    using App.Common.Validation;
    using App.Repository.Inventory;
    using App.Common.DI;
    using App.Common;
    using System;
    using App.Common.Data;
    using Entity.Inventory;
    using App.Common.Helpers;

    internal class CategoryService : ICategoryService
    {
        public void CreateIfNotExist(List<CreateCategoryRequest> createCategoryRequests)
        {
            using (App.Common.Data.IUnitOfWork uow = new App.Common.Data.UnitOfWork(RepositoryType.MSSQL))
            {
                ICategoryRepository categoryRepository = IoC.Container.Resolve<ICategoryRepository>(uow);
                foreach (CreateCategoryRequest request in createCategoryRequests)
                {
                    try
                    {
                        this.ValiateCreateCategoryRequest(request);
                        Category category = new Category(request.Name, request.Description);
                        categoryRepository.Add(category);
                    }
                    catch (ValidationException exception)
                    {
                        if (exception.HasExceptionKey("inventory.addOrUpdateCategory.validation.nameAlreadyExisted")) { continue; }
                    }
                }

                uow.Commit();
            }
        }

        public IList<CategoryListItem> GetCategories()
        {
            ICategoryRepository categoryRepository = IoC.Container.Resolve<ICategoryRepository>();
            return categoryRepository.GetItems<CategoryListItem>();
        }

        public GetCategoryResponse GetCategory(Guid id)
        {
            ICategoryRepository categoryRepository = IoC.Container.Resolve<ICategoryRepository>();
            return categoryRepository.GetById<GetCategoryResponse>(id.ToString());
        }

        public CreateCategoryResponse Create(CreateCategoryRequest createCategoryRequest)
        {
            this.ValiateCreateCategoryRequest(createCategoryRequest);
            using (IUnitOfWork uow = new UnitOfWork(RepositoryType.MSSQL))
            {
                ICategoryRepository categoryRepository = IoC.Container.Resolve<ICategoryRepository>(uow);
                Category category = new Category(createCategoryRequest.Name, createCategoryRequest.Description);
                categoryRepository.Add(category);
                uow.Commit();
                return ObjectHelper.Convert<CreateCategoryResponse>(category);
            }
        }

        private void ValiateCreateCategoryRequest(CreateCategoryRequest createCategoryRequest)
        {
            IValidationException validationException = ValidationHelper.Validate(createCategoryRequest);
            ICategoryRepository categoryRepository = IoC.Container.Resolve<ICategoryRepository>();

            if (categoryRepository.GetByName(createCategoryRequest.Name) != null)
            {
                validationException.Add(new App.Common.Validation.ValidationError("inventory.addOrUpdateCategory.validation.nameAlreadyExisted"));
            }

            validationException.ThrowIfError();
        }

        public void Update(UpdateCategoryRequest updateCategoryRequest)
        {
            this.ValiateUpdateCategoryRequest(updateCategoryRequest);
            using (IUnitOfWork uow = new UnitOfWork(RepositoryType.MSSQL))
            {
                ICategoryRepository categoryRepository = IoC.Container.Resolve<ICategoryRepository>(uow);
                Category category = categoryRepository.GetById(updateCategoryRequest.Id.ToString());
                category.Name = updateCategoryRequest.Name;
                category.Description = updateCategoryRequest.Description;
                categoryRepository.Update(category);
                uow.Commit();
            }
        }

        private void ValiateUpdateCategoryRequest(UpdateCategoryRequest updateCategoryRequest)
        {
            IValidationException validationException = ValidationHelper.Validate(updateCategoryRequest);
            ICategoryRepository categoryRepository = IoC.Container.Resolve<ICategoryRepository>();
            Category oldCategory = categoryRepository.GetById(updateCategoryRequest.Id.ToString());
            if (oldCategory == null)
            {
                validationException.Add(new App.Common.Validation.ValidationError("inventory.addOrUpdateCategory.validation.categoryNotExisted"));
            }

            if (oldCategory != null && oldCategory.Name != updateCategoryRequest.Name && categoryRepository.GetByName(updateCategoryRequest.Name) != null)
            {
                validationException.Add(new App.Common.Validation.ValidationError("inventory.addOrUpdateCategory.validation.nameAlreadyExisted"));
            }

            validationException.ThrowIfError();
        }

        public void Delete(Guid id)
        {
            this.ValidateDeleteRequest(id);
            using (IUnitOfWork uow = new UnitOfWork(RepositoryType.MSSQL))
            {
                ICategoryRepository categoryRepository = IoC.Container.Resolve<ICategoryRepository>(uow);
                categoryRepository.Delete(id);
                uow.Commit();
            }
        }

        private void ValidateDeleteRequest(Guid id)
        {
            ICategoryRepository categoryRepository = IoC.Container.Resolve<ICategoryRepository>();
            if (categoryRepository.GetById(id.ToString()) == null)
            {
                throw new ValidationException("inventory.categories.validation.categoryIsInvalid");
            }
        }
    }
}
