namespace App.Service.Test.Common
{
    using App.Common.Helpers;
    using App.Common.UnitTest;
    using App.Common.Validation;
    using App.Common.Validation.Attribute;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ValidateMatchAttribute : BaseUnitTest
    {
        private const string NameExceptionKey = "Name.Match.Key";
        private class CustomAttributeObject
        {
            [Match(ValidateMatchAttribute.NameExceptionKey, @"(\d)")]
            public string Name { get; set; }
            public CustomAttributeObject(string name = "")
            {
                this.Name = name;
            }
        }

        [TestMethod]
        public void Common_StringValidator_ValidateMatchAttribute_ShouldBeSuccess_WithValidValue()
        {
            CustomAttributeObject obj = new CustomAttributeObject("132");
            IValidationException ex = ValidationHelper.Validate(obj);
            Assert.IsTrue(ex.Errors.Count == 0);
        }

        [TestMethod]
        public void Common_StringValidator_ValidateMatchAttribute_ShouldThrowException_WithInvalidNumber()
        {
            CustomAttributeObject obj = new CustomAttributeObject("abc");
            IValidationException ex = ValidationHelper.Validate(obj);
            Assert.IsTrue(ex.HasExceptionKey(ValidateMatchAttribute.NameExceptionKey));
        }
    }
}