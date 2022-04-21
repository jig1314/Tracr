using FluentValidation;

namespace Tracr.Client.ViewModels
{
    public class SuggestionsFilterViewModel
    {
        public string City { get; set; } = "Kennesaw";

        public int? State { get; set; } = 11;

        public decimal? MaxListPrice { get; set; }

        public int? SortBy { get; set; } = 1;
    }

    public class SuggestionsFilterValidator : AbstractValidator<SuggestionsFilterViewModel>
    {
        public SuggestionsFilterValidator()
        {
            RuleFor(x => x.City)
                .NotEmpty()
                .WithMessage("Please enter a city.");

            RuleFor(x => x.State)
                .NotNull()
                .WithMessage("Please select a state.")
                .GreaterThan(0)
                .WithMessage("Please select a state.");

            RuleFor(x => x.MaxListPrice)
                .NotNull()
                .WithMessage("Please select a maximum list price.")
                .GreaterThan(0)
                .WithMessage("Maximum list price must be greater than $0.00"); 

            RuleFor(x => x.SortBy)
                .NotNull()
                .WithMessage("Please select a sort by value.");
        }
    }
}
