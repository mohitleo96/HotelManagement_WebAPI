using HotelManagementProjectfeb.Model.Domain;

namespace HotelManagementProjectfeb.Repositories
{
    public interface ITokenHandler
    {
        Task<string> CreateTokenAsync(Staff user);
    }
}
