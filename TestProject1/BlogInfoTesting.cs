using DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject1
{
    [TestFixture]
    public class BlogTests
    {
        [Test]
        public void BlogInfo_ValidProperties_PassesValidation()
        {
            // Arrange
            var blogInfo = new BlogInfo
            {
                BlogId = 10,
                Title = "Azure",
                Subject = "Azure Connect to MVC",
                BlogUrl = "https://github.com/deepakk2000/Deploy-an-ASP.NET-WebForms-Application-on-Azure.git",
                Email = "jkesh123@gmail.com",
                EmpInfo = new EmpInfo()
            };

            // Act
            var validationResults = ValidateModel(blogInfo);

            // Assert
            Assert.IsEmpty(validationResults); // If validationResults is empty, validation passed
        }

        [Test]
        public void BlogInfo_MissingRequiredProperties_FailsValidation()
        {
            // Arrange
            var blogInfo = new BlogInfo();

            // Act
            var validationResults = ValidateModel(blogInfo);

            // Assert
            Assert.IsNotEmpty(validationResults); // If validationResults is not empty, validation failed
            Assert.AreEqual(4, validationResults.Count); // Adjust the count based on the number of required properties
        }

        private System.Collections.Generic.List<ValidationResult> ValidateModel(object model)
        {
            var validationContext = new ValidationContext(model, serviceProvider: null, items: null);
            var validationResults = new System.Collections.Generic.List<ValidationResult>();

            Validator.TryValidateObject(model, validationContext, validationResults, validateAllProperties: true);

            return validationResults;
        }
    }
}
