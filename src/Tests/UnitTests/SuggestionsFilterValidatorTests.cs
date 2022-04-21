using FluentValidation.TestHelper;
using NUnit.Framework;
using Tracr.Client.ViewModels;

namespace Tracr.Tests.UnitTests
{
    [TestFixture]
    [Category("UnitTest")]
    public class SuggestionsFilterValidatorTests
    {
        private SuggestionsFilterValidator validator;

        [SetUp]
        public void SuggestionsFilterValidatorTestSetUp()
        {
            validator = new SuggestionsFilterValidator();
        }

        [Test]
        public void CityShould_HaveValidationError_WhenLeftEmpty()
        {
            var model = new SuggestionsFilterViewModel() { City = null };
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.City);
        }

        [Test]
        public void CityShould_HaveNoValidationError_WhenNotEmpty()
        {
            var model = new SuggestionsFilterViewModel() { City = "Orlando" };
            var result = validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(x => x.City);
        }

        [Test]
        public void StateShould_HaveValidationError_WhenLeftEmpty()
        {
            var model = new SuggestionsFilterViewModel() { State = null };
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.State);
        }

        [Test]
        public void StateShould_HaveValidationError_WhenLessThanZero()
        {
            var model = new SuggestionsFilterViewModel() { State = -1 };
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.State);
        }

        [Test]
        public void StateShould_HaveValidationError_WhenEqualsToZero()
        {
            var model = new SuggestionsFilterViewModel() { State = 0 };
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.State);
        }

        [Test]
        public void StateShould_HaveNoValidationError_WhenGreaterThanZero()
        {
            var model = new SuggestionsFilterViewModel() { State = 1 };
            var result = validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(x => x.State);
        }

        [Test]
        public void MaxListPriceShould_HaveValidationError_WhenLeftEmpty()
        {
            var model = new SuggestionsFilterViewModel() { MaxListPrice = null };
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.MaxListPrice);
        }

        [Test]
        public void MaxListPriceShould_HaveValidationError_WhenLessThanZero()
        {
            var model = new SuggestionsFilterViewModel() { MaxListPrice = -1 };
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.MaxListPrice);
        }

        [Test]
        public void MaxListPriceShould_HaveValidationError_WhenEqualsToZero()
        {
            var model = new SuggestionsFilterViewModel() { MaxListPrice = 0 };
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.MaxListPrice);
        }

        [Test]
        public void MaxListPriceShould_HaveNoValidationError_WhenGreaterThanZero()
        {
            var model = new SuggestionsFilterViewModel() { MaxListPrice = 1 };
            var result = validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(x => x.MaxListPrice);
        }

        [Test]
        public void SortByShould_HaveValidationError_WhenLeftEmpty()
        {
            var model = new SuggestionsFilterViewModel() { SortBy = null };
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.SortBy);
        }

        [Test]
        public void SortByShould_HaveNoValidationError_WhenNotEmpty()
        {
            var model = new SuggestionsFilterViewModel() { SortBy = 1 };
            var result = validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(x => x.SortBy);
        }
    }
}
