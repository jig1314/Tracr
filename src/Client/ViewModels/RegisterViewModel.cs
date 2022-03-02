using FluentValidation;
using FluentValidation.Validators;
using System.Text.RegularExpressions;

namespace Tracr.Client.ViewModels
{
    public class RegisterViewModel
    {
        public string FirstName { get; set; } = "";

        public string LastName { get; set; } = "";

        public string Email { get; set; } = "";

        public string UserName { get; set; } = "";

        public string Password { get; set; } = "";

        public string ConfirmPassword { get; set; } = "";
    }

    public class RegisterValidator : AbstractValidator<RegisterViewModel>
    {
        public RegisterValidator()
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

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Please enter valid email address")
                .EmailAddress(EmailValidationMode.AspNetCoreCompatible)
                .WithMessage("Please enter valid email address");

            RuleFor(x => x.UserName)
                .NotEmpty()
                .WithMessage("Please enter a valid username.")
                .Matches("^(?=[a-zA-Z0-9._]{6,20}$)(?!.*[_.]{2})[^_.].*[^_.]$")
                .WithMessage("Please enter a valid username.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Please enter password.")
                .Custom((password, context) =>
                {
                    var hasNumber = new Regex(@"[0-9]+");
                    var hasLowerChar = new Regex(@"[a-z]+");
                    var hasUpperChar = new Regex(@"[A-Z]+");
                    var hasMinimum6Chars = new Regex(@".{6,}");
                    var hasSymbols = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");

                    var isValid = hasNumber.IsMatch(password) && hasLowerChar.IsMatch(password) && hasUpperChar.IsMatch(password) && hasMinimum6Chars.IsMatch(password) && hasSymbols.IsMatch(password);
                    if (!isValid)
                    {
                        context.AddFailure("Please enter a password that is between 6 and 100 characters with at least one number, uppercase and lowercase letter and one special character.");
                    }
                });

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().WithMessage("Please re-enter password.")
                .Equal(x => x.Password).WithMessage("The password and confirmation password do not match.");
        }
    }
}
