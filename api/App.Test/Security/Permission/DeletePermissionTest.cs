namespace App.Service.Test.Security.Permission
{
    using App.Common.DI;
    using App.Common.UnitTest;
    using App.Common.Validation;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Service.Security;
    using Service.Security.Permission;
    using System;

    [TestClass]
    public class DeletePermissionTest : BaseUnitTest
    {
        [TestMethod]
        public void Security_Permission_DeletePermission_ShouldBeSuccess_WithValidRequest()
        {
            string name = "Name of Permission" + Guid.NewGuid();
            string key = "Key of Permission" + Guid.NewGuid();
            string desc = "Desc of Permission";
            CreatePermissionResponse permission = this.CreatePermissionItem(name, key, desc);
            this.DeletePermission(permission.Id);

            IPermissionService service = IoC.Container.Resolve<IPermissionService>();
            App.Service.Security.Permission.GetPermissionResponse deletedPermisison = service.GetPermission(permission.Id);
            Assert.IsNull(deletedPermisison);
        }

        private CreatePermissionResponse CreatePermissionItem(string name, string key, string desc)
        {
            CreatePermissionRequest request = new CreatePermissionRequest(name, key, desc);
            IPermissionService service = IoC.Container.Resolve<IPermissionService>();
            return service.Create(request);
        }

        [TestMethod]
        public void Security_Permission_DeletePermission_ShouldGetException_WithInvalidId()
        {
            try
            {
                Guid itemId = Guid.NewGuid();
                this.DeletePermission(itemId);
                Assert.IsTrue(false);
            }
            catch (ValidationException ex)
            {
                Assert.IsTrue(ex.HasExceptionKey("security.permissons.permissionIdIsInvalid"));
            }
        }

        [TestMethod]
        public void Security_Permission_DeletePermission_ShouldGetException_WithEmptyId()
        {
            try
            {
                this.DeletePermission(Guid.Empty);
                Assert.IsTrue(false);
            }
            catch (ValidationException ex)
            {
                Assert.IsTrue(ex.HasExceptionKey("security.permissons.permissionIdIsInvalid"));
            }
        }

        private void DeletePermission(Guid id)
        {
            IPermissionService service = IoC.Container.Resolve<IPermissionService>();
            service.Delete(id);
        }
    }
}
