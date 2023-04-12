using FluentValidation;

namespace HotelManagementProjectfeb.Validators
{
    public class LoginRequestValidators: AbstractValidator<Model.DTO.LoginRequest>
    {
        public LoginRequestValidators()
        {
            RuleFor(x=>x.UserName).NotEmpty();  

            RuleFor(x=>x.Password).NotEmpty();  
        }
    }
}
