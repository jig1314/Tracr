using FluentValidation;

namespace Tracr.Client.ViewModels
{
    public class DeleteAccountViewModel
    {
        public string Password { get; set; } = "";
    }

    public class DeleteAccountValidator : AbstractValidator<DeleteAccountViewModel>
    {
        public DeleteAccountValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Please enter your current password.");
        }
    }
}
