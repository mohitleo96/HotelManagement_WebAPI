using FluentValidation;

namespace HotelManagementProjectfeb.Validators
{
    public class UpdateInventoryRequestValidator : AbstractValidator<Model.DTO.Inventory>
    {
        public UpdateInventoryRequestValidator()
        {
            RuleFor(x => x.Inventory_Name).NotEmpty();

            RuleFor(x => x.quantity).GreaterThanOrEqualTo(0);
        }
    }
}
