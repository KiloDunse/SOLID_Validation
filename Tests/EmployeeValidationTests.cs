using Core;
using Core.Entities;
using Core.Validators;

namespace Tests
{
    public class EmployeeValidationTests
    {
        [Theory]
        [MemberData(nameof(GetSuccessData))]
        public void Employee_Should_Pass(Employee e, ValidationAggregator<Employee> validator)
        {
            // Arrange

            // Act
            var res = validator.Validate(e);

            //Assert
            Assert.True(res.IsValid);
        }

        [Theory]
        [MemberData(nameof(GetFailureData))]
        public void Employee_Should_Fail(Employee e, ValidationAggregator<Employee> validator, List<string> expectedErrorMessages)
        {
            // Arrange

            // Act
            var res = validator.Validate(e);

            //Assert
            Assert.False(res.IsValid);
            Assert.Equal(expectedErrorMessages.Count, res.ErrorMessages.Count());

            foreach (var error in expectedErrorMessages)
                Assert.Contains(error, res.ErrorMessages);
        }

        public static IEnumerable<object[]> GetSuccessData()
        {
            yield return new object[] 
            { 
                new Employee { Id = 10 }, 
                new ValidationAggregator<Employee>(
                    new List<IValidator<Employee>> { new EmployeeIdValidator() })
            };
            yield return new object[]
            {
                new Employee { Id = 120 },
                new ValidationAggregator<Employee>(
                    new List<IValidator<Employee>> { new EmployeeIdValidator() })
            };
        }

        public static IEnumerable<object[]> GetFailureData()
        {
            yield return new object[]
            {
                new Employee { Id = -1 },
                new ValidationAggregator<Employee>(
                    new List<IValidator<Employee>> { new EmployeeIdValidator() }),
                new List<string> { "Id cannot be a Negative number" }
            };
            yield return new object[]
            {
                new Employee { Id = 121 },
                new ValidationAggregator<Employee>(
                    new List<IValidator<Employee>> { new EmployeeIdValidator() }),
                new List<string> { "Id cannot be higher than 120" }
            };
        }       
    }
}