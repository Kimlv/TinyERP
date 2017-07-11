namespace App.Service.Test.Common
{
    using App.Common.Helpers;
    using App.Common.UnitTest;
    using App.Common.Validation;
    using App.Common.Validation.Attribute;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ValidateValueInCollectionAttribute : BaseUnitTest
    {
        private const string NameExceptionKey = "Name.ValueInCollection.Key";
        private class CustomAttributeObject
        {
            [ValueInCollection(ValidateValueInCollectionAttribute.NameExceptionKey, new object[] { "one", "two", "three" })]
            public string Name { get; set; }
            public CustomAttributeObject(string name = "")
            {
                this.Name = name;
            }
        }

        [TestMethod]
        public void Common_StringValidator_ValidateValueInCollectionAttribute_ShouldBeSuccess_WithValidValue()
        {
            CustomAttributeObject obj = new CustomAttributeObject("one");
            IValidationException ex = ValidationHelper.Validate(obj);
            Assert.IsTrue(ex.Errors.Count == 0);
        }

        [TestMethod]
        public void Common_StringValidator_ValidateValueInCollectionAttribute_ShouldThrowException_WithInvalidNumber()
        {
            CustomAttributeObject obj = new CustomAttributeObject("abc");
            IValidationException ex = ValidationHelper.Validate(obj);
            Assert.IsTrue(ex.HasExceptionKey(ValidateValueInCollectionAttribute.NameExceptionKey));
        }
    }
}