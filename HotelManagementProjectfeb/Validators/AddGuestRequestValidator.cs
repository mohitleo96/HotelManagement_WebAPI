using FluentValidation;

namespace HotelManagementProjectfeb.Validators
{
    public class AddGuestRequestValidator :AbstractValidator<Model.DTO.AddGuestRequest>
    {
        public AddGuestRequestValidator()
        {
            RuleFor(x=>x.E_mail).NotEmpty();

            RuleFor(x => x.Guest_Name).NotEmpty();

            RuleFor(x=>x.Gender).NotEmpty();

            RuleFor(x=>x.Address).NotEmpty();

            RuleFor(x => x.Phone_number).GreaterThanOrEqualTo(0);
        }
    }
}
