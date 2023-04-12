using AutoMapper;

namespace HotelManagementProjectfeb.Profiles
{
    public class RoomProfil : Profile
    {
        public RoomProfil()
        {
           CreateMap<Model.Domain.Room,Model.DTO.Room>().ReverseMap();
        }
    }
}
