namespace App.Service.Impl.Security
{
    using System.Collections.Generic;
    using App.Service.Security;
    using App.Repository.Secutiry;
    using App.Common.DI;
    using App.Entity.Security;
    using App.Common;
    using App.Common.Data;
    using System;
    using Context;
    using App.Common.Validation;
    using Service.Security.Permission;
    using App.Common.Helpers;
    using App.Common.Configurations;

    internal class PermissionService : IPermissionService
    {
        public void CreateIfNotExist(IList<CreatePermissionRequest> pers)
        {
            using (App.Common.Data.IUnitOfWork uow = UnitOfWorkFactory.Create(Configuration.Current.Repository.DefaultConnectionStringName))
            {
                IPermissionRepository perRepository = IoC.Container.Resolve<IPermissionRepository>(uow);
                foreach (CreatePermissionRequest perRequest in pers)
                {
                    try
                    {
                        this.ValidateCreatePermissionRequest(perRequest);
                        Permission permission = new Permission(perRequest.Name, perRequest.Key, perRequest.Description);
                        perRepository.Add(permission);
                    }
                    catch (ValidationException ex)
                    {
                        if (ex.HasExceptionKey("security.addPermission.validation.nameAlreadyExist")) { continue; }
                        if (ex.HasExceptionKey("security.addPermission.validation.keyAlreadyExist")) { continue; }
                    }
                }

                uow.Commit();
            }
        }

        public CreatePermissionResponse Create(CreatePermissionRequest permission)
        {
            this.ValidateCreatePermissionRequest(permission);
            using (App.Common.Data.IUnitOfWork uow = new App.Common.Data.UnitOfWork(RepositoryType.MSSQL))
            {
                IPermissionRepository perRepository = IoC.Container.Resolve<IPermissionRepository>(uow);
                Permission per = new Permission(permission.Name, permission.Key, permission.Description);
                perRepository.Add(per);
                uow.Commit();
                return ObjectHelper.Convert<CreatePermissionResponse>(per);
            }
        }

        private void ValidateCreatePermissionRequest(CreatePermissionRequest permission)
        {
            IValidationException validationException = ValidationHelper.Validate(permission);

            IPermissionRepository perRepo = IoC.Container.Resolve<IPermissionRepository>();
            if (perRepo.GetByName(permission.Name) != null)
            {
                validationException.Add(new App.Common.Validation.ValidationError("security.addOrUpdatePermission.validation.nameAlreadyExist"));
            }

            if (perRepo.GetByKey(permission.Key) != null)
            {
                validationException.Add(new App.Common.Validation.ValidationError("security.addOrUpdatePermission.validation.keyAlreadyExist"));
            }

            validationException.ThrowIfError();
        }

        public IList<PermissionAsKeyNamePair> GetPermissions()
        {
            IPermissionRepository perRepo = IoC.Container.Resolve<IPermissionRepository>();
            return perRepo.GetPermissions<PermissionAsKeyNamePair>();
        }

        public void Delete(Guid id)
        {
            this.ValidateDeleteRequest(id);
            using (IUnitOfWork uow = new UnitOfWork(RepositoryType.MSSQL))
            {
                IPermissionRepository perRepo = IoC.Container.Resolve<IPermissionRepository>(uow);
                perRepo.Delete(id);
                uow.Commit();
            }
        }

        private void ValidateDeleteRequest(Guid id)
        {
            if (id == null || id == Guid.Empty)
            {
                throw new ValidationException("security.permissons.permissionIdIsInvalid");
            }

            IPermissionRepository perRepo = IoC.Container.Resolve<IPermissionRepository>();
            if (perRepo.GetById(id.ToString()) == null)
            {
                throw new ValidationException("security.permissons.permissionIdIsInvalid");
            }
        }

        public GetPermissionResponse GetPermission(Guid id)
        {
            IPermissionRepository perRepo = IoC.Container.Resolve<IPermissionRepository>();
            return perRepo.GetById<GetPermissionResponse>(id.ToString());
        }

        public void Update(UpdatePermissionRequest request)
        {
            this.ValidateUpdateRequest(request);
            using (IUnitOfWork uow = new UnitOfWork(RepositoryType.MSSQL))
            {
                IPermissionRepository perRepo = IoC.Container.Resolve<IPermissionRepository>(uow);
                Permission existedPermission = perRepo.GetById(request.Id.ToString());
                existedPermission.Name = request.Name;
                existedPermission.Key = request.Key;
                existedPermission.Description = request.Description;
                perRepo.Update(existedPermission);
                uow.Commit();
            }
        }

        private void ValidateUpdateRequest(UpdatePermissionRequest request)
        {
            IPermissionRepository perRepo = IoC.Container.Resolve<IPermissionRepository>();
            IValidationException validationException = ValidationHelper.Validate(request);
            Permission per = perRepo.GetById(request.Id.ToString());
            if (per == null) {
                validationException.Add(new App.Common.Validation.ValidationError("security.addPermission.validation.invalidId"));
            }

            per = perRepo.GetByName(request.Name);
            if (per != null && per.Id != request.Id)
            {
                validationException.Add(new App.Common.Validation.ValidationError("security.addPermission.validation.nameAlreadyExist"));
            }

            per = perRepo.GetByKey(request.Key);
            if (per != null && per.Id != request.Id)
            {
                validationException.Add(new App.Common.Validation.ValidationError("security.addPermission.validation.keyAlreadyExist"));
            }

            validationException.ThrowIfError();
        }
    }
}
