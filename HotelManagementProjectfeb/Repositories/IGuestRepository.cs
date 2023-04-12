using HotelManagementProjectfeb.Model.Domain;

namespace HotelManagementProjectfeb.Repositories
{
    public interface IGuestRepository
    {
        Task<IEnumerable<Guest>> GetAllAsync();
        //After this goes to RegionRepository class

        Task<Guest> GetAsync(Guid id);

        Task<Guest> AddAsync(Guest guest);

        Task<Guest> DeleteAsync(Guid id);

        Task<Guest> UpdateAsync(Guid id, Guest guest);

    }
}
