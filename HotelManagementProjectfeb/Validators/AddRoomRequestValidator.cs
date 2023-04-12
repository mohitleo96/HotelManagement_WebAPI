using FluentValidation;

namespace HotelManagementProjectfeb.Validators
{
    public class AddRoomRequestValidator :AbstractValidator<Model.DTO.AddRoomRequest>
    {
        public AddRoomRequestValidator()
        {
            RuleFor(x => x.room_rate).GreaterThan(0);

            RuleFor(x=>x.room_status).NotEmpty();

        }
    }
}
