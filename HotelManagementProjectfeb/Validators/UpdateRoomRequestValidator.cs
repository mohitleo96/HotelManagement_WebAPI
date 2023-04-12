using FluentValidation;

namespace HotelManagementProjectfeb.Validators
{
    public class UpdateRoomRequestValidator : AbstractValidator<Model.DTO.UpdateRoomRequest>
    {
        public UpdateRoomRequestValidator()
        {
            RuleFor(x => x.room_rate).GreaterThan(0);

        }
    }
}
