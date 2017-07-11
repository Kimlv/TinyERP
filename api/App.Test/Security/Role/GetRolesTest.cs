namespace App.Test.Security.Role
{
    using App.Common.UnitTest;
    using Common.DI;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Service.Security;
    using System;
    using System.Collections.Generic;

    [TestClass]
    public class GetRolesTest : BaseUnitTest
    {
        [TestMethod]
        public void Sercurity_Role_GetRoles_ShouldBeSusscess_WithExistedRoles()
        {
            string name = "name of role" + Guid.NewGuid();
            string description = "description of role";
            IList<Guid> permissions = new List<Guid>();
            CreateRoleRequest createRoleRequest = new CreateRoleRequest()
            {
                Name = name,
                Description = description,
                Permissions = permissions
            };

            IRoleService roleService = IoC.Container.Resolve<IRoleService>();
            roleService.Create(createRoleRequest);
            Assert.IsTrue(roleService.GetRoles().Count > 0);
        }
    }
}
