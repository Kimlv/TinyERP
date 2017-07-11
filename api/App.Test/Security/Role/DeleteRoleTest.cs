namespace App.Test.Security.Role
{
    using App.Common.UnitTest;
    using Common.DI;
    using Common.Validation;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Service.Security;
    using System;
    using System.Collections.Generic;

    [TestClass]
    public class DeleteRoleTest : BaseUnitTest
    {
        [TestMethod]
        public void Security_Role_DeleteRole_ShouldBeSuccess_WithValidRequest()
        {
            string name = "name of role" + Guid.NewGuid();
            string description = "desc of role";
            IList<Guid> permissions = new List<Guid>();
            CreateRoleResponse createRoleResponse = this.CreateRole(name, description, permissions);
            IRoleService roleService = IoC.Container.Resolve<IRoleService>();
            roleService.Delete(createRoleResponse.Id);
            GetRoleResponse roleRespone = roleService.Get(createRoleResponse.Id);
            Assert.IsNull(roleRespone);
        }

        [TestMethod]
        public void Security_Role_DeleteRole_ShouldGetException_WithNotExistedId()
        {
            try
            {
                IRoleService roleService = IoC.Container.Resolve<IRoleService>();
                roleService.Delete(Guid.NewGuid());
                Assert.IsTrue(false);
            }
            catch (ValidationException exception)
            {
                Assert.IsTrue(exception.HasExceptionKey("security.roles.validation.roleNotExisted"));
            }
        }

        [TestMethod]
        public void Security_Role_DeleteRole_ShouldGetException_WithEmptyId()
        {
            try
            {
                IRoleService roleService = IoC.Container.Resolve<IRoleService>();
                roleService.Delete(Guid.Empty);
                Assert.IsTrue(false);
            }
            catch (ValidationException exception)
            {
                Assert.IsTrue(exception.HasExceptionKey("security.roles.validation.idIsInvalid"));
            }
        }

        private CreateRoleResponse CreateRole(string name, string description, IList<Guid> permissions)
        {
            CreateRoleRequest createRoleRequest = new CreateRoleRequest()
            {
                Name = name,
                Description = description,
                Permissions = permissions
            };

            IRoleService roleService = IoC.Container.Resolve<IRoleService>();
            return roleService.Create(createRoleRequest);
        }
    }
}
