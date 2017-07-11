namespace App.Service.Impl.Security
{
    using System.Collections.Generic;
    using App.Service.Security;
    using App.Common.DI;
    using System;
    using App.Common;
    using App.Repository.Secutiry;
    using App.Entity.Security;
    using App.Common.Data;
    using App.Common.Validation;
    using App.Service.Security.UserGroup;
    using App.Common.Helpers;
    using System.Linq;

    internal class UserGroupService : IUserGroupService
    {
        public CreateUserGroupResponse Create(CreateUserGroupRequest request)
        {
            this.ValidateCreateRequest(request);
            using (App.Common.Data.IUnitOfWork uow = new App.Common.Data.UnitOfWork(RepositoryType.MSSQL))
            {
                IUserGroupRepository userGroupRepository = IoC.Container.Resolve<IUserGroupRepository>(uow);
                IPermissionRepository permissionRepo = IoC.Container.Resolve<IPermissionRepository>(uow);
                IList<Permission> permissions = permissionRepo.GetPermissions(request.PermissionIds);
                UserGroup userGroup = new UserGroup(request.Name, request.Description, permissions);
                userGroupRepository.Add(userGroup);
                uow.Commit();
                return new CreateUserGroupResponse(userGroup);
            }
        }

        private void ValidateCreateRequest(CreateUserGroupRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Name))    
            {
                throw new App.Common.Validation.ValidationException("security.addOrUpdateUserGroup.validation.nameIsRequire");
            }

            IUserGroupRepository repo = IoC.Container.Resolve<IUserGroupRepository>();
            if (repo.GetByName(request.Name) != null)
            {
                throw new App.Common.Validation.ValidationException("security.addOrUpdateUserGroup.validation.nameAlreadyExist");
            }
        }

        public IList<UserGroupListItemSummary> GetUserGroups()
        {
            IUserGroupRepository repository = IoC.Container.Resolve<IUserGroupRepository>();
            return repository.GetItems<UserGroupListItemSummary>();
        }

        public void Delete(Guid id)
        {
            this.ValidateDeleteRequest(id);
            using (IUnitOfWork uow = new App.Common.Data.UnitOfWork(RepositoryType.MSSQL))
            {
                IUserGroupRepository repository = IoC.Container.Resolve<IUserGroupRepository>(uow);
                repository.Delete(id);
                uow.Commit();
            }
        }

        private void ValidateDeleteRequest(Guid id)
        {
            if (id == null || id == Guid.Empty)
            {
                throw new ValidationException("security.addOrUpdateUserGroup.validation.idIsInvalid");
            }

            IUserGroupRepository repository = IoC.Container.Resolve<IUserGroupRepository>();
            if (repository.GetById(id.ToString()) == null)
            {
                throw new ValidationException("security.addOrUpdateUserGroup.validation.userGroupNotExist");
            }
        }

        public GetUserGroupResponse Get(Guid id)
        {
            IUserGroupRepository repository = IoC.Container.Resolve<IUserGroupRepository>();
            IPermissionRepository perRepo = IoC.Container.Resolve<IPermissionRepository>();
            UserGroup userGroup = repository.GetById(id.ToString(), "Permissions");
            GetUserGroupResponse response = ObjectHelper.Convert<GetUserGroupResponse>(userGroup);
            response.PermissionIds = new List<Guid>();
            foreach (Permission per in userGroup.Permissions)
            {
                response.PermissionIds.Add(per.Id);
            }

            return response;
        }

        public void Update(UpdateUserGroupRequest request)
        {
            this.ValidateUpdateRequest(request);
            using (IUnitOfWork uow = new UnitOfWork(RepositoryType.MSSQL))
            {
                IUserGroupRepository repository = IoC.Container.Resolve<IUserGroupRepository>(uow);
                UserGroup existedItem = repository.GetById(request.Id.ToString(), "Permissions");
                existedItem.Name = request.Name;
                existedItem.Key = App.Common.Helpers.UtilHelper.ToKey(request.Name);
                existedItem.Description = request.Description;

                this.RemoveRemovedPermissions(existedItem, request, uow);
                this.AddAddedPermission(existedItem, request, uow);
                repository.Update(existedItem);
                uow.Commit();
            }
        }

        private void AddAddedPermission(UserGroup existedItem, UpdateUserGroupRequest request, IUnitOfWork uow)
        {
            if (request.PermissionIds.Count == 0) { return; }
            IList<Guid> existPers = existedItem.Permissions.Select(item => item.Id).ToList();
            IEnumerable<Guid> addedItems = request.PermissionIds.Except(existPers);
            IPermissionRepository perRepo = IoC.Container.Resolve<IPermissionRepository>(uow);
            foreach (Guid item in addedItems)
            {
                Permission per = perRepo.GetById(item.ToString());
                existedItem.Permissions.Add(per);
            }
        }

        private void RemoveRemovedPermissions(UserGroup existedItem, UpdateUserGroupRequest request, IUnitOfWork uow)
        {
            if (existedItem.Permissions.Count == 0) { return; }
            IList<Guid> existPers = existedItem.Permissions.Select(item => item.Id).ToList();
            IEnumerable<Guid> removedItems = existPers.Except(request.PermissionIds);
            foreach (Guid item in removedItems)
            {
                Permission per = existedItem.Permissions.FirstOrDefault(perItem => perItem.Id == item);
                existedItem.Permissions.Remove(per);
            }
        }

        private void ValidateUpdateRequest(UpdateUserGroupRequest request)
        {
            if (request.Id == null || request.Id == Guid.Empty)
            {
                throw new ValidationException("security.addOrUpdateUserGroup.validation.idIsInvalid");
            }

            IUserGroupRepository repository = IoC.Container.Resolve<IUserGroupRepository>();
            if (repository.GetById(request.Id.ToString()) == null)
            {
                throw new ValidationException("security.addOrUpdateUserGroup.validation.itemNotExist");
            }

            if (string.IsNullOrWhiteSpace(request.Name))
            {
                throw new ValidationException("security.addOrUpdateUserGroup.validation.nameIsRequire");
            }

            UserGroup itemByName = repository.GetByName(request.Name);
            if (itemByName != null && itemByName.Id != request.Id)
            {
                throw new ValidationException("security.addOrUpdateUserGroup.validation.nameAlreadyExist");
            }

            string key = App.Common.Helpers.UtilHelper.ToKey(request.Name);
            UserGroup itemByKey = repository.GetByKey(key);
            if (itemByKey != null && itemByKey.Id != request.Id)
            {
                throw new ValidationException("security.addOrUpdateUserGroup.validation.keyAlreadyExisted");
            }
        }
    }
}
