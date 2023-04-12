using AutoMapper;

namespace HotelManagementProjectfeb.Profiles
{
    public class InventoryProfil : Profile
    {
        public InventoryProfil()
        {
         
                CreateMap<Model.Domain.Inventory, Model.DTO.Inventory>().ReverseMap();
            
        }
    }
}
