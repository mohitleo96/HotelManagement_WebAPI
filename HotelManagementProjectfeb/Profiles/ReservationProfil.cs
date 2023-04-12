using AutoMapper;

namespace HotelManagementProjectfeb.Profiles
{
    public class ReservationProfil : Profile
    {
        public ReservationProfil()
        {
            CreateMap<Model.Domain.Reservation,Model.DTO.Reservation>().ReverseMap();
        }
    }
}
