namespace App.Service.Test.Common
{
    using App.Common.Helpers;
    using App.Common.UnitTest;
    using App.Common.Validation;
    using App.Common.Validation.Attribute;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ValidateValueInRangeAttribute : BaseUnitTest
    {
        private const string NameExceptionKey = "Name.Max.Key";
        private class CustomAttributeObject
        {
            [ValueInRange(ValidateValueInRangeAttribute.NameExceptionKey, 5, 10)]
            public string Name { get; set; }

            public CustomAttributeObject(string name = "")
            {
                this.Name = name;
            }
        }

        [TestMethod]
        public void Common_StringValidator_ValidateValueInRangeAttribute_ShouldBeSuccess_WithValidValueInRange()
        {
            CustomAttributeObject obj = new CustomAttributeObject("123456798");
            IValidationException ex = ValidationHelper.Validate(obj);
            Assert.IsTrue(ex.Errors.Count == 0);
        }

        [TestMethod]
        public void Common_StringValidator_ValidateValueInRangeAttribute_ShouldThrowException_WithEmptyValue()
        {
            CustomAttributeObject obj = new CustomAttributeObject(string.Empty);
            IValidationException ex = ValidationHelper.Validate(obj);
            Assert.IsTrue(ex.HasExceptionKey(ValidateValueInRangeAttribute.NameExceptionKey));
        }

        [TestMethod]
        public void Common_StringValidator_ValidateValueInRangeAttribute_ShouldThrowException_WithValueLessThanLowerBound()
        {
            CustomAttributeObject obj = new CustomAttributeObject("123");
            IValidationException ex = ValidationHelper.Validate(obj);
            Assert.IsTrue(ex.Errors.Count == 1 && ex.HasExceptionKey(ValidateValueInRangeAttribute.NameExceptionKey));
        }

        [TestMethod]
        public void Common_StringValidator_ValidateValueInRangeAttribute_ShouldThrowException_WithValueGreaterThanUpperBound()
        {
            CustomAttributeObject obj = new CustomAttributeObject("123456789123456798");
            IValidationException ex = ValidationHelper.Validate(obj);
            Assert.IsTrue(ex.Errors.Count == 1 && ex.HasExceptionKey(ValidateValueInRangeAttribute.NameExceptionKey));
        }
    }
}