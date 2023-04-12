using FluentValidation;

namespace HotelManagementProjectfeb.Validators
{
    public class AddInventoryRequestValidator : AbstractValidator<Model.DTO.Inventory>
    {
        public AddInventoryRequestValidator()
        {
            RuleFor(x=>x.Inventory_Name).NotEmpty();

            RuleFor(x=>x.quantity).GreaterThanOrEqualTo(0);
        }
    }
}
