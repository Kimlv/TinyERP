namespace App.Service.Test.Common
{
    using App.Common.Helpers;
    using App.Common.UnitTest;
    using App.Common.Validation;
    using App.Common.Validation.Attribute;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ValidateRequireAttribute : BaseUnitTest
    {
        private const string NameExceptionKey = "Test.Name.Validation.Key";
        private const string SubjectExceptionKey = "Test.Subject.Validation.Key";
        private class CustomAttributeObject
        {
            [Required(ValidateRequireAttribute.SubjectExceptionKey)]
            public string Subject { get; set; }

            [Required(ValidateRequireAttribute.NameExceptionKey)]
            public string Name { get; set; }

            public CustomAttributeObject(string name = "", string subject = "")
            {
                this.Name = name;
                this.Subject = subject;
            }
        }

        [TestMethod]
        public void Common_StringValidator_ValidateRequireAttribute_ShouldBeSuccess_WithValidObject()
        {
            CustomAttributeObject obj = new CustomAttributeObject("Test value", "Test Subject");
            IValidationException ex = ValidationHelper.Validate(obj);
            Assert.IsTrue(ex.Errors.Count == 0);
        }

        [TestMethod]
        public void Common_StringValidator_ValidateRequireAttribute_ShouldThrowException_WithEmptyName()
        {
            CustomAttributeObject obj = new CustomAttributeObject(string.Empty, "Test Subject");
            IValidationException ex = ValidationHelper.Validate(obj);
            Assert.IsTrue(ex.HasExceptionKey(ValidateRequireAttribute.NameExceptionKey));
        }

        [TestMethod]
        public void Common_StringValidator_ValidateRequireAttribute_ShouldThrowOnlyExceptionForName_WithEmptyName()
        {
            CustomAttributeObject obj = new CustomAttributeObject(string.Empty, "Test Subject");
            IValidationException ex = ValidationHelper.Validate(obj);
            Assert.IsTrue(ex.Errors.Count == 1 && ex.HasExceptionKey(ValidateRequireAttribute.NameExceptionKey));
        }

        [TestMethod]
        public void Common_StringValidator_ValidateRequireAttribute_ShouldThrowExceptionForNameAndSubject_WithEmptyNameAndSubject()
        {
            CustomAttributeObject obj = new CustomAttributeObject(string.Empty, string.Empty);
            IValidationException ex = ValidationHelper.Validate(obj);
            Assert.IsTrue(ex.Errors.Count == 2 && ex.HasExceptionKey(ValidateRequireAttribute.NameExceptionKey));
            Assert.IsTrue(ex.Errors.Count == 2 && ex.HasExceptionKey(ValidateRequireAttribute.SubjectExceptionKey));
        }
    }
}