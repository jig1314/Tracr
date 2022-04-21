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
                .WithMessage("Please enter a valid first name. Must start with a capital letter and be at leasr two letters long!")
                .Matches("^([A-Z][a-zA-Z]{1,})$")
                .WithMessage("Please enter a valid first name. Must start with a capital letter and be at leasr two letters long!");

            RuleFor(x => x.LastName)
                .NotEmpty()
                .WithMessage("Please enter a valid last name. Must start with a capital letter and be at leasr two letters long!")
                .Matches("^([A-Z][a-zA-Z]{1,})$")
                .WithMessage("Please enter a valid last name. Must start with a capital letter and be at leasr two letters long!");
        }
    }
}
