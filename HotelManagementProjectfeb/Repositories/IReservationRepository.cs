using HotelManagementProjectfeb.Model.Domain;

namespace HotelManagementProjectfeb.Repositories
{
    public interface IReservationRepository
    {
        Task<IEnumerable<Reservation>> GetAllAsync();
        //After this goes to RegionRepository class

        Task<Reservation> GetAsync(Guid id);

        Task<Reservation> AddAsync(Reservation reservation);

        Task<Reservation> DeleteAsync(Guid id);

        Task<Reservation> UpdateAsync(Guid id, Reservation reservation);
    }
}
