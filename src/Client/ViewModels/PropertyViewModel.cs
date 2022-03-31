using FluentValidation;

namespace Tracr.Client.ViewModels
{
    public class PropertyViewModel
    {
        public string Name { get; set; }

        public string NumBedrooms { get; set; }

        public string NumBathrooms { get; set; }

        public string StreetAddress { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string ZipCode { get; set; }

        public string Principal { get; set; }

        public string MonthlyPayment { get; set; }

        public string APR { get; set; }
    }

    public class PropertyValidator : AbstractValidator<PropertyViewModel>
    {
        public PropertyValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Please enter a property name.");

            RuleFor(x => x.NumBedrooms)
                .NotEmpty()
                .WithMessage("Please enter a number of bedrooms. Must be a number value! (Example: 5)")
                .Custom((numBedrooms, context) =>
                {
                    if ((!(int.TryParse(numBedrooms, out int value)) || value < 0))
                    {
                        context.AddFailure($"{numBedrooms} is not a valid number of bedrooms! Must be a number value! (Example: 5)");
                    }
                });

            RuleFor(x => x.NumBathrooms)
                .NotEmpty()
                .WithMessage("Please enter a number of bathrooms. Must be a positive number or decimal value less than 100! (Example: 3.5)")
                .Custom((numBedrooms, context) =>
                {
                    if ((!(decimal.TryParse(numBedrooms, out decimal value)) || value < 0 || value >= 100))
                    {
                        context.AddFailure($"{numBedrooms} is not a valid number of bathrooms! Must be a positive number or decimal value less than 100! (Example: 3.5)");
                    }
                });

            RuleFor(x => x.StreetAddress)
                .NotEmpty()
                .WithMessage("Please enter a street address.");

            RuleFor(x => x.City)
                .NotEmpty()
                .WithMessage("Please enter a city.");

            RuleFor(x => x.State)
                .NotEmpty()
                .WithMessage("Please enter a state.");

            RuleFor(x => x.ZipCode)
                .NotEmpty()
                .WithMessage("Please enter a zip code.");

            RuleFor(x => x.Principal)
                .NotEmpty()
                .WithMessage("Please enter a principal. (Example: 100000.00)")
                .Custom((principal, context) =>
                {
                    if ((!(decimal.TryParse(principal, out decimal value)) || value < 0))
                    {
                        context.AddFailure($"{principal} is not a valid principal! (Example: 100000.00)");
                    }
                });

            RuleFor(x => x.MonthlyPayment)
                .NotEmpty()
                .WithMessage("Please enter a monthly payment. (Example: 2000.00)")
                .Custom((monthlyPayment, context) =>
                {
                    if ((!(decimal.TryParse(monthlyPayment, out decimal value)) || value < 0))
                    {
                        context.AddFailure($"{monthlyPayment} is not a valid monthly payment! (Example: 2000.00)");
                    }
                });

            RuleFor(x => x.APR)
                .NotEmpty()
                .WithMessage("Please enter an APR. (Example: 2.75)")
                .Custom((apr, context) =>
                {
                    if ((!(decimal.TryParse(apr, out decimal value)) || value < 0))
                    {
                        context.AddFailure($"{apr} is not a valid APR! (Example: 2.75)");
                    }
                });
        }
    }
}
