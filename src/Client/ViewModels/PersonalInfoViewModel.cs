using FluentValidation;

namespace Tracr.Client.ViewModels
{
    public class PersonalInfoViewModel
    {
        public string FirstName { get; set; } = "";

        public string LastName { get; set; } = "";
    }

    public class PersonalInfoValidator : AbstractValidator<PersonalInfoViewModel>
    {
        public PersonalInfoValidator()
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
        }
    }
}
