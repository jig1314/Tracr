using FluentValidation;

namespace Tracr.Client.ViewModels
{
    public class RenterViewModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MonthlyRent { get; set; }

        public DateOnly? StartingMonth { get; set; }

        public DateOnly? EndingMonth { get; set; }
    }

    public class RenterValidator : AbstractValidator<RenterViewModel>
    {
        public RenterValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage("Please enter a valid first name.")
                .Matches("^([A-Z][a-zA-Z]{1,})$")
                .WithMessage("Please enter a valid first name.");

            RuleFor(x => x.LastName)
                .NotEmpty()
                .WithMessage("Please enter a valid last name.")
                .Matches("^([A-Z][a-zA-Z]{1,})$")
                .WithMessage("Please enter a valid last name.");

            RuleFor(x => x.MonthlyRent)
                .NotEmpty()
                .WithMessage("Please enter the montly rent.")
                .Custom((monthlyRent, context) =>
                {
                    if ((!(decimal.TryParse(monthlyRent, out decimal value)) || value < 0))
                    {
                        context.AddFailure($"{monthlyRent} is not a valid montly rent!");
                    }
                })
                .ScalePrecision(2, 18)
                .WithMessage($"Please enter a valid montly rent!");

            RuleFor(x => x.StartingMonth).Must(date => date.HasValue)
                .WithMessage("Please enter a starting month.");

            RuleFor(x => x.EndingMonth).Must(date => date.HasValue)
                .WithMessage("Please enter an ending month.");
        }
    }
}
