using HotelManagementProjectfeb.Model.Domain;
using Microsoft.EntityFrameworkCore;
using HotelManagementProjectfeb.Data;

namespace HotelManagementProjectfeb.Repositories
{
    public class ReservationRepository:IReservationRepository
    {

        //using private readonly data types can

        //help promote good programming practices such as encapsulation, immutability,

        //and thread safety, making the resulting code more robust and easier to maintain.

        //here created object HotelManagementDataContext to perform all crud operation.

        private readonly ZzzDbContext _db;

        public ReservationRepository(ZzzDbContext db)

        {
            _db = db;
        }


        //here it is add more guest so it is work on post method

        public async Task<Reservation> AddAsync(Reservation reservation)

        {
            reservation.reservation_id = Guid.NewGuid();

            await _db.AddAsync(reservation);

            await _db.SaveChangesAsync();

            return reservation;

        }


        // here we are deleting basis  on id if id will got then delete other than do not delete

        public async Task<Reservation> DeleteAsync(Guid id)

        {
            var reservation = await _db.Reservations.FirstOrDefaultAsync(x => x.reservation_id == id);

            if (reservation == null)

            {
                return null;
            }

            //Delete 

            _db.Reservations.Remove(reservation);

            await _db.SaveChangesAsync();

            return reservation;

        }

        //here it will show all things

        public async Task<IEnumerable<Reservation>> GetAllAsync()

        {
            return await _db.Reservations.ToListAsync();

        }
        public async Task<Reservation> GetAsync(Guid id)

        {
            return await _db.Reservations.FirstOrDefaultAsync(x => x.reservation_id == id);

        }
        public async Task<Reservation> UpdateAsync(Guid id, Reservation reservation)

        {

            //here we are making searching is there any id which is existing

            var existingreservation = await _db.Reservations.FirstOrDefaultAsync(x => x.reservation_id == id);

            if (existingreservation == null)
            {

                return null;

            }

            // here we are updating the all value except id becauses it will remain same


            existingreservation.no_of_adults = reservation.no_of_adults;    

            existingreservation.no_of_children=reservation.no_of_children;

            existingreservation.Check_out = reservation.Check_out;

            existingreservation.Check_in=reservation.Check_in;

            existingreservation.status=reservation.status;

            existingreservation.no_of_nights = reservation.no_of_nights;

            existingreservation.Guest_Id= reservation.Guest_Id;

            existingreservation.Room_id = reservation.Room_id;

            await _db.SaveChangesAsync();

            return existingreservation;

        }

    }

}
    


