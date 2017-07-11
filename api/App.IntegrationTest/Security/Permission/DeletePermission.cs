namespace App.IntegrationTest.Security.Permission
{
    using App.Common.Http;
    using App.Service.Security.Permission;
    using Common.UnitTest;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Linq;

    [TestClass]
    public class DeletePermission : BaseIntegrationTest
    {
        private readonly CreatePermissionResponse createdPermissionResponse;
        public DeletePermission() : base(@"api/permissions/{0}")
        {
            this.createdPermissionResponse = this.CreateNewPermission();
        }

        private CreatePermissionResponse CreateNewPermission()
        {
            CreatePermissionRequest request = new CreatePermissionRequest("Delete permission name" + Guid.NewGuid(), "Delete permission key" + Guid.NewGuid(), "desc");
            IResponseData<CreatePermissionResponse> response = this.Connector.Post<CreatePermissionRequest, CreatePermissionResponse>(string.Format(this.BaseUrl, string.Empty), request);
            return response.Data;
        }

        [TestMethod()]
        public void Security_Permission_DeletePermission_ShouldBeSuccess_WithValidRequest()
        {
            IResponseData<string> response = this.Connector.Delete<string>(string.Format(this.BaseUrl, this.createdPermissionResponse.Id));
            Assert.IsTrue(response.Errors.Count == 0);
        }

        public void Security_Permission_DeletePermission_ShouldBeSuccess_WithNotExistedId()
        {
            IResponseData<string> response = this.Connector.Delete<string>(string.Format(this.BaseUrl, Guid.NewGuid()));
            Assert.IsTrue(response.Errors.Count > 0);
            Assert.IsTrue(response.Errors.Any(item => item.Key == "security.permissons.permissionIdIsInvalid"));
        }

        public void Security_Permission_DeletePermission_ShouldBeSuccess_WithEmptyId()
        {
            IResponseData<string> response = this.Connector.Delete<string>(string.Format(this.BaseUrl, Guid.Empty));
            Assert.IsTrue(response.Errors.Count > 0);
            Assert.IsTrue(response.Errors.Any(item => item.Key == "security.permissons.permissionIdIsInvalid"));
        }
    }
}
