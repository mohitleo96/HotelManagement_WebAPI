using HotelManagementProjectfeb.Model.Domain;

namespace HotelManagementProjectfeb.Repositories
{
    public interface IBillRepository
    {
        Task<IEnumerable<Bill>> GetAllAsync();
        //After this goes to RegionRepository class

        Task<Bill> GetAsync(Guid id);

        Task<Bill> AddAsync(Bill bill);

        Task<Bill> DeleteAsync(Guid id);

        Task<Bill> UpdateAsync(Guid id, Bill bill);
    }
}

