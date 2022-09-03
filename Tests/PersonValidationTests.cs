using Core;
using Core.Entities;
using Core.Validators;

namespace Tests
{
    public class PersonValidationTests
    {
        [Theory]
        [MemberData(nameof(GetSuccessData))]
        public void Person_Should_Pass(Person p, ValidationAggregator<Person> validator)
        {
            // Arrange

            // Act
            var res = validator.Validate(p);

            //Assert
            Assert.True(res.IsValid);
        }

        [Theory]
        [MemberData(nameof(GetFailureData))]
        public void Person_Should_Fail(Person p, ValidationAggregator<Person> validator, List<string> expectedErrorMessages)
        {
            // Arrange

            // Act
            var res = validator.Validate(p);

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
                new Person { Age = 10 }, 
                new ValidationAggregator<Person>(
                    new List<IValidator<Person>> { new PersonAgeValidator() })
            };
            yield return new object[]
            {
                new Person { Age = 120, FirstName = "Tester" },
                new ValidationAggregator<Person>(
                    new List<IValidator<Person>> { new PersonAgeValidator(), new PersonFirstNameValidator() })
            };
        }

        public static IEnumerable<object[]> GetFailureData()
        {
            yield return new object[]
            {
                new Person { Age = -1 },
                new ValidationAggregator<Person>(
                    new List<IValidator<Person>> { new PersonAgeValidator() }),
                new List<string> { "Age cannot be a Negative number" }

            };
            yield return new object[]
            {
                new Person { Age = 122, FirstName = "Test" },
                new ValidationAggregator<Person>(
                    new List<IValidator<Person>> { new PersonAgeValidator(), new PersonFirstNameValidator() }),
                new List<string> { "Age cannot be higher than 120", "FirstName must have more than 5 chars", "FirstName may not be 'Test'" }
            };
        }       
    }
}