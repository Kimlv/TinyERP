namespace App.IntegrationTest.Security.Permission
{
    using App.Common.Http;
    using App.Service.Security.Permission;
    using Common.UnitTest;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Linq;

    [TestClass]
    public class CreatePermission : BaseIntegrationTest
    {
        public CreatePermission() : base(@"api/permissions")
        {
        }

        [TestMethod()]
        public void Security_Permission_CreatePermission_ShouldBeSuccess_WithValidRequest()
        {
            CreatePermissionRequest request = new CreatePermissionRequest("Name " + Guid.NewGuid(), "Key " + Guid.NewGuid(), "desc");
            IResponseData<CreatePermissionResponse> response = this.Connector.Post<CreatePermissionRequest, CreatePermissionResponse>(this.BaseUrl, request);
            /* Assert.IsTrue(response.Status == HttpStatusCode.OK); */
            Assert.IsTrue(response.Errors.Count == 0);
            Assert.IsTrue(response.Data != null);
            Assert.IsTrue(response.Data.Id != null && response.Data.Id != Guid.Empty);
        }

        [TestMethod()]
        public void Security_Permission_CreatePermission_ShouldThroException_WithInValidRequest()
        {
            CreatePermissionRequest request = new CreatePermissionRequest(string.Empty, string.Empty, "desc");
            IResponseData<CreatePermissionResponse> response = this.Connector.Post<CreatePermissionRequest, CreatePermissionResponse>(this.BaseUrl, request);
            Assert.IsTrue(response.Errors.Count > 0);
            Assert.IsTrue(response.Errors.Any(item => item.Key == "security.addPermission.validation.nameIsRequire"));
            Assert.IsTrue(response.Errors.Any(item => item.Key == "security.addPermission.validation.keyIsRequire"));
        }

        [TestMethod()]
        public void Security_Permission_CreatePermission_ShouldThroException_WithDuplicatedNameAndKey()
        {
            CreatePermissionRequest request = new CreatePermissionRequest("Name " + Guid.NewGuid(), "Key " + Guid.NewGuid(), "desc");
            this.Connector.Post<CreatePermissionRequest, CreatePermissionResponse>(this.BaseUrl, request);

            IResponseData<CreatePermissionResponse> response = this.Connector.Post<CreatePermissionRequest, CreatePermissionResponse>(this.BaseUrl, request);
            Assert.IsTrue(response.Errors.Count > 0);
            Assert.IsTrue(response.Errors.Any(item => item.Key == "security.addPermission.validation.nameAlreadyExist"));
            Assert.IsTrue(response.Errors.Any(item => item.Key == "security.addPermission.validation.keyAlreadyExist"));
        }
    }
}
