using FluentValidation;
using Users.Api.Models;

namespace Users.Api.Validators
{
    public class UserModelValidator : AbstractValidator<UserModel>
    {
        public UserModelValidator() 
        {
            RuleFor(user => user.FullName)
                .NotEmpty().WithMessage("FullName is required.")
                .MinimumLength(2).WithMessage("FullName must be at least 2 characters long.");
            RuleFor(user => user.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("A valid Email is required.");
            RuleFor(user => user.DateOfBirth)
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("DateOfBirth cannot be in the future.");
        }
    }
}
