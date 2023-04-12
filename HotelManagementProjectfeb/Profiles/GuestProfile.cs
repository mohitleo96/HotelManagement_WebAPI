using AutoMapper;

namespace HotelManagementProjectfeb.Profiles
{
    public class GuestProfile: Profile
    {
        //Autompper convert domain to dTO and Reverse map convert DTO to Domain

        //How it know that automapper has used so for that
        //we go to program.cs and inject autommaper after repository

        public GuestProfile()
        {

            CreateMap<Model.Domain.Guest, Model.DTO.Guest>().ReverseMap();
        }
    }
}
