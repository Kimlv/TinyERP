namespace App.IntegrationTest.Security.Permission
{
    using App.Common.Http;
    using App.Service.Security.Permission;
    using Common.UnitTest;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Linq;

    [TestClass]
    public class UpdatePermission : BaseIntegrationTest
    {
        private readonly CreatePermissionResponse createdPermissionResponse;
        public UpdatePermission() : base(@"api/permissions/{0}")
        {
            this.createdPermissionResponse = this.CreateNewPermission();
        }

        [TestMethod()]
        public void Security_Permission_UpdatePermission_ShouldBeSuccess_WithValidRequest()
        {
            UpdatePermissionRequest request = new UpdatePermissionRequest(this.createdPermissionResponse.Id, "new updated name" + Guid.NewGuid(), "New updated Key " + Guid.NewGuid(), "desc");
            IResponseData<string> response = this.Connector.Put<UpdatePermissionRequest, string>(string.Format(this.BaseUrl, request.Id), request);
            Assert.IsTrue(response.Errors.Count == 0);
        }

        private CreatePermissionResponse CreateNewPermission()
        {
            CreatePermissionRequest request = new CreatePermissionRequest("update permission name" + Guid.NewGuid(), "update permission key" + Guid.NewGuid(), "desc");
            IResponseData<CreatePermissionResponse> response = this.Connector.Post<CreatePermissionRequest, CreatePermissionResponse>(string.Format(this.BaseUrl, string.Empty), request);
            return response.Data;
        }

        [TestMethod()]
        public void Security_Permission_UpdatePermission_ShouldThrowException_WithEmptyNameAndKey()
        {
            UpdatePermissionRequest request = new UpdatePermissionRequest(this.createdPermissionResponse.Id, string.Empty, string.Empty, "desc");
            IResponseData<string> response = this.Connector.Put<UpdatePermissionRequest, string>(string.Format(this.BaseUrl, request.Id), request);
            Assert.IsTrue(response.Errors.Count > 0);
            Assert.IsTrue(response.Errors.Any(item => item.Key == "security.addPermission.validation.nameIsRequire"));
            Assert.IsTrue(response.Errors.Any(item => item.Key == "security.addPermission.validation.keyIsRequire"));
        }

        [TestMethod()]
        public void Security_Permission_CreatePermission_ShouldThrowException_WithNotExistedId()
        {
            UpdatePermissionRequest request = new UpdatePermissionRequest(Guid.NewGuid(), "new updated name" + Guid.NewGuid(), "New updated Key " + Guid.NewGuid(), "desc");
            IResponseData<string> response = this.Connector.Put<UpdatePermissionRequest, string>(string.Format(this.BaseUrl, request.Id), request);
            Assert.IsTrue(response.Errors.Count > 0);
            Assert.IsTrue(response.Errors.Any(item => item.Key == "security.addPermission.validation.invalidId"));
        }

        [TestMethod()]
        public void Security_Permission_CreatePermission_ShouldThrowException_WithEmptyId()
        {
            UpdatePermissionRequest request = new UpdatePermissionRequest(Guid.Empty, "new updated name" + Guid.NewGuid(), "New updated Key " + Guid.NewGuid(), "desc");
            IResponseData<object> response = this.Connector.Put<UpdatePermissionRequest, object>(string.Format(this.BaseUrl, request.Id), request);
            Assert.IsTrue(response.Errors.Count > 0);
            Assert.IsTrue(response.Errors.Any(item => item.Key == "common.validation.invalidRequest"));
        }
    }
}
