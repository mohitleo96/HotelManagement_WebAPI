using HotelManagementProjectfeb.Model.Domain;

namespace HotelManagementProjectfeb.Repositories
{
    public interface IInventoryRepositorycs
    {
        Task<IEnumerable<Inventory>> GetAllAsync();
        //After this goes to RegionRepository class

        Task<Inventory> GetAsync(Guid id);

        Task<Inventory> AddAsync(Inventory inventory);

        Task<Inventory> DeleteAsync(Guid id);

        Task<Inventory> UpdateAsync(Guid id, Inventory inventory);
    }
}
