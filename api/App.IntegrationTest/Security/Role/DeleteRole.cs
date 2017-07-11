namespace App.IntegrationTest.Security.Role
{
    using Common.Http;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Service.Security;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    [TestClass]
    public class DeleteRole : Common.UnitTest.BaseIntegrationTest
    {
        private readonly CreateRoleResponse createdRoleResponse;
        public DeleteRole() : base(@"api/roles/{0}")
        {
            this.createdRoleResponse = this.CreateNewRole();
        }

        private CreateRoleResponse CreateNewRole()
        {
            CreateRoleRequest createRoleRequest = new CreateRoleRequest()
            {
                Name = "name of role" + Guid.NewGuid(),
                Description = "description of role",
                Permissions = new List<Guid>()
            };

            IResponseData<CreateRoleResponse> createRoleResponse = this.Connector.Post<CreateRoleRequest, CreateRoleResponse>(string.Format(this.BaseUrl, string.Empty), createRoleRequest);
            return createRoleResponse.Data;
        }

        [TestMethod()]
        public void Security_Role_DeleteRole_ShouldBeSuccess_WithValidRequest()
        {
            IResponseData<string> deleteResponse = this.Connector.Delete<string>(string.Format(this.BaseUrl, this.createdRoleResponse.Id));
            IResponseData<GetRoleResponse> roleResponse = this.Connector.Get<GetRoleResponse>(string.Format(this.BaseUrl, this.createdRoleResponse.Id));
            Assert.IsNull(roleResponse.Data);
            Assert.IsTrue(deleteResponse.Errors.Count == 0);
        }

        [TestMethod]
        public void Security_Role_DeleteRole_ShouldGetException_WithEmptyId()
        {
            IResponseData<string> response = this.Connector.Delete<string>(string.Format(this.BaseUrl, Guid.Empty));
            Assert.IsTrue(response.Errors.Count > 0);
            Assert.IsTrue(response.Errors.Any(item => item.Key == "security.roles.validation.idIsInvalid"));
        }

        [TestMethod]
        public void Security_Role_DeleteRole_ShouldGetException_WithNotExistedId()
        {
            IResponseData<string> createRoleResponse = this.Connector.Delete<string>(string.Format(this.BaseUrl, Guid.NewGuid()));
            Assert.IsTrue(createRoleResponse.Errors.Count > 0);
            Assert.IsTrue(createRoleResponse.Errors.Any(item => item.Key == "security.roles.validation.roleNotExisted"));
        }
    }
}
