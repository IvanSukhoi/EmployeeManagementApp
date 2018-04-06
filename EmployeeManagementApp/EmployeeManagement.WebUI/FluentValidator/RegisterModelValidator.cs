using EmployeeManagement.WebUI.Models;
using FluentValidation;

namespace Fluent_Validtion.Models.Validators
{
    public class RegisterModelValidator : AbstractValidator<EmployeeModel>
    {
        public RegisterModelValidator()
        {
            RuleFor(x => x.FirstName)
                   .NotEmpty().WithMessage("FirstName is Required").Length(0, 50); 
            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("LastName is Required").Length(0, 50);
            RuleFor(x => x.ID).Null();

        }
    }
}