using HotelManagementProjectfeb.Model.Domain;

namespace HotelManagementProjectfeb.Repositories
{
    public interface IRoomRepository
    {
        Task<IEnumerable<Room>> GetAllAsync();
        //After this goes to RegionRepository class

        Task<Room> GetAsync(Guid id);

        Task<Room> AddAsync(Room room);

        Task<Room> DeleteAsync(Guid id);

        Task<Room> UpdateAsync(Guid id, Room room);
    }
}
