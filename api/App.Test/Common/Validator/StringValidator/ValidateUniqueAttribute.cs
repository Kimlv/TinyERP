namespace App.Service.Test.Common
{
    using App.Common;
    using App.Common.DI;
    using App.Common.Helpers;
    using App.Common.UnitTest;
    using App.Common.Validation;
    using App.Common.Validation.Attribute;
    using Entity.Security;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Service.Security.Permission;

    public class ValidateUniqueAttribute : BaseUnitTest
    {
        private const string NameExceptionKey = "Name.Unique.Key";
        private class CustomAttributeObject
        {
            public string Name { get; set; }
            public CustomAttributeObject(string name = "")
            {
                this.Name = name;
            }
        }

        [TestMethod]
        public void Common_Validator_ValidateUniqueAttribute_ShouldBeSuccess_WithValidName()
        {
            CustomAttributeObject obj = new CustomAttributeObject("132");
            IValidationException ex = ValidationHelper.Validate(obj);
            Assert.IsTrue(ex.Errors.Count == 0);
        }

        [TestMethod]
        public void Common_Validator_ValidateUniqueAttribute_ShouldThrowException_WithDuplicatedName()
        {
            CreatePermissionRequest request = new CreatePermissionRequest("abc", "dsfsdfs", "sdfdfs");
            IPermissionService service = IoC.Container.Resolve<IPermissionService>();
            service.Create(request);

            CustomAttributeObject obj = new CustomAttributeObject("abc");
            IValidationException ex = ValidationHelper.Validate(obj);
            Assert.IsTrue(ex.HasExceptionKey(ValidateUniqueAttribute.NameExceptionKey));
        }
    }
}