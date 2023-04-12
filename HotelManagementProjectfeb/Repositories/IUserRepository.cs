using HotelManagementProjectfeb.Model.Domain;

namespace HotelManagementProjectfeb.Repositories
{
    public interface IUserRepository
    {
        Task<Staff> AuthenticateAsync(string username, string password);
    }
}
