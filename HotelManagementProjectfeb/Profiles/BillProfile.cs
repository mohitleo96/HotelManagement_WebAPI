using AutoMapper;

namespace HotelManagementProjectfeb.Profiles
{
    public class BillProfile: Profile
    {
        public BillProfile()
        {
          
            CreateMap<Model.Domain.Bill, Model.DTO.Bill>().ReverseMap();
            
        }
    }
}
