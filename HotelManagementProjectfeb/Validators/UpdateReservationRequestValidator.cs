using FluentValidation;

namespace HotelManagementProjectfeb.Validators
{
    public class UpdateReservationRequestValidator : AbstractValidator<Model.DTO.UpdateReservationRequest>
    {
        public UpdateReservationRequestValidator()
        {

            RuleFor(x => x.no_of_adults).GreaterThan(0);

            RuleFor(x => x.no_of_children).GreaterThan(0);

            RuleFor(x => x.Check_out).GreaterThanOrEqualTo(DateTime.Now);

            RuleFor(x => x.Check_in).GreaterThanOrEqualTo(DateTime.Now);

            RuleFor(x => x.no_of_nights).GreaterThan(0);

            RuleFor(x => x.Guest_Id).NotEmpty();

            RuleFor(x => x.Room_id).NotEmpty();



        }
    }
}
