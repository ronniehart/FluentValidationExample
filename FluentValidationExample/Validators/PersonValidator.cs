using FluentValidation;
using FluentValidationExample.Models;

namespace FluentValidationExample.Validators
{
    public class PersonValidator : AbstractValidator<Person>
    {
        public PersonValidator()
        {
            RuleFor(x => x.Id).NotNull();
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Enter a name")
                .Length(3, 20).WithMessage("Enter 3 to 20 characters");

            RuleFor(x => x.Email)
                .EmailAddress().WithMessage("Enter an Email Address");

            RuleFor(x => x.Age)
                .GreaterThan(17).WithMessage("You have to be at least 18 years old");
        }
    }
}