using FluentValidation.TestHelper;
using NUnit.Framework;
using System;
using Tracr.Client.ViewModels;

namespace Tracr.Tests.UnitTests
{
    [TestFixture]
    [Category("UnitTest")]
    public class RenterValidatorTests
    {
        private RenterValidator validator;

        [SetUp]
        public void RenterValidatorTestSetUp()
        {
            validator = new RenterValidator();
        }

        [Test]
        public void FirstNameShould_HaveValidationError_WhenLeftEmpty()
        {
            var model = new RenterViewModel() { FirstName = null };
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.FirstName);
        }

        [Test]
        public void FirstNameShould_HaveValidationError_WhenOnlyOneCharacter()
        {
            var model = new RenterViewModel() { FirstName = "J" };
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.FirstName);
        }

        [Test]
        public void FirstNameShould_HaveValidationError_WhenDoesNotStartWithUppercaseLetter()
        {
            var model = new RenterViewModel() { FirstName = "jOnathan" };
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.FirstName);
        }

        [Test]
        public void FirstNameShould_HaveNoValidationErrors_WhenStartsWithUppercaseLetterAndLongerThanOneCharacter()
        {
            var model = new RenterViewModel() { FirstName = "Joe" };
            var result = validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(x => x.FirstName);
        }

        [Test]
        public void LastNameShould_HaveValidationError_WhenLeftEmpty()
        {
            var model = new RenterViewModel() { LastName = null };
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.LastName);
        }

        [Test]
        public void LastNameShould_HaveValidationError_WhenOnlyOneCharacter()
        {
            var model = new RenterViewModel() { LastName = "D" };
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.LastName);
        }

        [Test]
        public void LastNameShould_HaveValidationError_WhenDoesNotStartWithUppercaseLetter()
        {
            var model = new RenterViewModel() { LastName = "gAmble" };
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.LastName);
        }

        [Test]
        public void LastNameShould_HaveNoValidationErrors_WhenStartsWithUppercaseLetterAndLongerThanOneCharacter()
        {
            var model = new RenterViewModel() { LastName = "Do" };
            var result = validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(x => x.LastName);
        }

        [Test]
        public void StartingMonthShould_HaveValidationErrors_WhenItIsNull()
        {
            var model = new RenterViewModel() { StartingMonth = null };
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.StartingMonth);
        }

        [Test]
        public void StartingMonthShould_HaveNoValidationErrors_WhenItIsNotNull()
        {
            var model = new RenterViewModel() { StartingMonth = DateOnly.FromDateTime(DateTime.Today) };
            var result = validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(x => x.StartingMonth);
        }

        [Test]
        public void EndingMonthShould_HaveValidationErrors_WhenItIsNull()
        {
            var model = new RenterViewModel() { EndingMonth = null };
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.EndingMonth);
        }

        [Test]
        public void EndingMonthShould_HaveNoValidationErrors_WhenItIsNotNull()
        {
            var model = new RenterViewModel() { EndingMonth = DateOnly.FromDateTime(DateTime.Today) };
            var result = validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(x => x.EndingMonth);
        }
    }
}
