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
    public class UpdatePermissionTest : BaseUnitTest
    {
        private CreatePermissionResponse permission;
        private CreatePermissionResponse permission1;
        protected override void OnInit()
        {
            base.OnInit();
            string name = "Name of Permission" + Guid.NewGuid();
            string key = "Key of Permission" + Guid.NewGuid();
            string desc = "Desc of Permission";
            this.permission = this.CreatePermissionItem(name, key, desc);

            name = "Duplicated Name" + Guid.NewGuid();
            key = "Duplicated key" + Guid.NewGuid();
            desc = "Desc of Permission";
            this.permission1 = this.CreatePermissionItem(name, key, desc);
        }

        private CreatePermissionResponse CreatePermissionItem(string name, string key, string desc)
        {
            CreatePermissionRequest request = new CreatePermissionRequest(name, key, desc);
            IPermissionService service = IoC.Container.Resolve<IPermissionService>();
            return service.Create(request);
        }

        [TestMethod]
        public void Security_Permission_UpdatePermission_ShouldBeSuccess_WithValidRequest()
        {
            string name = "New Name of Permission" + Guid.NewGuid();
            string key = "New Key of Permission" + Guid.NewGuid();
            string desc = "New Desc of Permission";
            this.UpdatePermissionItem(this.permission.Id, name, key, desc);

            IPermissionService service = IoC.Container.Resolve<IPermissionService>();
            App.Service.Security.Permission.GetPermissionResponse updatedPermisison = service.GetPermission(this.permission.Id);
            Assert.AreEqual(updatedPermisison.Name, name);
        }

        [TestMethod]
        public void Security_Permission_UpdatePermission_ShouldGetException_WithEmptyName()
        {
            try
            {
                string name = string.Empty;
                string key = "Key of Permission" + Guid.NewGuid();
                string desc = "Desc of Permission";
                this.UpdatePermissionItem(this.permission.Id, name, key, desc);
                Assert.IsTrue(false);
            }
            catch (ValidationException ex)
            {
                Assert.IsTrue(ex.HasExceptionKey("security.addPermission.validation.nameIsRequire"));
            }
        }

        [TestMethod]
        public void Security_Permission_UpdatePermission_ShouldGetException_WithEmptyId()
        {
            try
            {
                string name = this.permission1.Name;
                string key = "Key of Permission" + Guid.NewGuid();
                string desc = "Desc of Permission";
                this.UpdatePermissionItem(Guid.Empty, name, key.ToString(), desc);
                Assert.IsTrue(false);
            }
            catch (ValidationException ex)
            {
                Assert.IsTrue(ex.HasExceptionKey("common.validation.invalidRequest"));
            }
        }

        [TestMethod]
        public void Security_Permission_UpdatePermission_ShouldGetException_WithNotExistedId()
        {
            try
            {
                string name = this.permission1.Name;
                string key = "Key of Permission" + Guid.NewGuid();
                string desc = "Desc of Permission";
                this.UpdatePermissionItem(Guid.NewGuid(), name, key.ToString(), desc);
                Assert.IsTrue(false);
            }
            catch (ValidationException ex)
            {
                Assert.IsTrue(ex.HasExceptionKey("security.addPermission.validation.invalidId"));
            }
        }

        [TestMethod]
        public void Security_Permission_UpdatePermission_ShouldGetException_WithDuplicatedName()
        {
            try
            {
                string name = this.permission1.Name;
                string key = "Key of Permission" + Guid.NewGuid();
                string desc = "Desc of Permission";
                this.UpdatePermissionItem(this.permission.Id, name, key.ToString(), desc);
                Assert.IsTrue(false);
            }
            catch (ValidationException ex)
            {
                Assert.IsTrue(ex.HasExceptionKey("security.addPermission.validation.nameAlreadyExist"));
            }
        }

        [TestMethod]
        public void Security_Permission_UpdatePermission_ShouldGetException_WithEmptyKey()
        {
            try
            {
                string name = "Name Of Permission" + Guid.NewGuid();
                string key = string.Empty;
                string desc = "Desc of Permission";
                this.UpdatePermissionItem(this.permission.Id, name, key, desc);
                Assert.IsTrue(false);
            }
            catch (ValidationException ex)
            {
                Assert.IsTrue(ex.HasExceptionKey("security.addPermission.validation.keyIsRequire"));
            }
        }

        [TestMethod]
        public void Security_Permission_UpdatePermission_ShouldGetException_WithDuplicatedKey()
        {
            try
            {
                string name = "Name of Pemrission" + Guid.NewGuid();
                string key = this.permission1.Key;
                string desc = "Desc of Permission";
                this.UpdatePermissionItem(this.permission.Id, name, key, desc);
                Assert.IsTrue(false);
            }
            catch (ValidationException ex)
            {
                Assert.IsTrue(ex.HasExceptionKey("security.addPermission.validation.keyAlreadyExist"));
            }
        }

        private void UpdatePermissionItem(Guid id, string name, string key, string desc)
        {
            UpdatePermissionRequest request = new UpdatePermissionRequest(id, name, key, desc);
            IPermissionService service = IoC.Container.Resolve<IPermissionService>();
            service.Update(request);
        }
    }
}
