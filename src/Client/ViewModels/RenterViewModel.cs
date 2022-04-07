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
                .WithMessage("Please enter a valid first name. Must start with a capital letter and be at leasr two letters long!")
                .Matches("^([A-Z][a-zA-Z]{1,})$")
                .WithMessage("Please enter a valid first name. Must start with a capital letter and be at leasr two letters long!");

            RuleFor(x => x.LastName)
                .NotEmpty()
                .WithMessage("Please enter a valid last name. Must start with a capital letter and be at leasr two letters long!")
                .Matches("^([A-Z][a-zA-Z]{1,})$")
                .WithMessage("Please enter a valid last name. Must start with a capital letter and be at leasr two letters long!");

            RuleFor(x => x.MonthlyRent)
                .NotEmpty()
                .WithMessage("Please enter the montly rent. (Example: 2000.00)")
                .Custom((monthlyRent, context) =>
                {
                    if ((!(decimal.TryParse(monthlyRent, out decimal value)) || value < 0))
                    {
                        context.AddFailure($"{monthlyRent} is not a valid montly rent! (Example: 2000.00)");
                    }
                });

            RuleFor(x => x.StartingMonth).Must(date => date.HasValue)
                .WithMessage("Please enter a starting month.");

            RuleFor(x => x.EndingMonth)
                .Must(date => date.HasValue)
                .WithMessage("Please enter an ending month greater than starting month.")
                .GreaterThan(x => x.StartingMonth)
                .WithMessage("Ending month must be greater than starting month.");
        }
    }
}
