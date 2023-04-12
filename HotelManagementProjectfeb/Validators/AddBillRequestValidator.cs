using FluentValidation;

namespace HotelManagementProjectfeb.Validators
{
    public class AddBillRequestValidator:AbstractValidator<Model.DTO.AddBillRequest>
    {
        public AddBillRequestValidator()
        {
            RuleFor(x =>x.stay_dates).GreaterThanOrEqualTo(0);

            RuleFor(x=>x.total_bill).GreaterThanOrEqualTo(0);

            RuleFor(x => x.Room_id).NotEmpty();

            RuleFor(x => x.Reservation_id).NotEmpty();
        }
    }
}
