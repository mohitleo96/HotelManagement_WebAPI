using FluentValidation;

namespace HotelManagementProjectfeb.Validators
{
    public class UpdateGuestRequestValidator:AbstractValidator<Model.DTO.UpdateGuestRequest>
    {
            public UpdateGuestRequestValidator()
            {
                RuleFor(x => x.E_mail).NotEmpty();

                RuleFor(x => x.Guest_Name).NotEmpty();

                RuleFor(x => x.Gender).NotEmpty();

                RuleFor(x => x.Address).NotEmpty();

                RuleFor(x => x.Phone_number).GreaterThanOrEqualTo(0);
            }
        }
    }

