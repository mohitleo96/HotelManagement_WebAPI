using HotelManagementProjectfeb.Model.Domain;
using Microsoft.EntityFrameworkCore;
using HotelManagementProjectfeb.Data;

namespace HotelManagementProjectfeb.Repositories
{
    public class GuestRepository : IGuestRepository

    {
        //using private readonly data types can
        //help promote good programming practices such as encapsulation, immutability,
        //and thread safety, making the resulting code more robust and easier to maintain.
        //here created object HotelManagementDataContext to perform all crud operation.
        private readonly ZzzDbContext _db;
        public GuestRepository(ZzzDbContext db)
        {
            _db = db;
        }

        //here it is add more guest so it is work on post method
        public async Task<Guest> AddAsync(Guest guest)
        {
            guest.Guest_id = Guid.NewGuid();

            await _db.AddAsync(guest);

            await _db.SaveChangesAsync();

            return guest;

        }


        // here we are deleting basis  on id if id will got then delete other than do not delete
        public async Task<Guest> DeleteAsync(Guid id)

        {
            var guest = await _db.Guests.FirstOrDefaultAsync(x => x.Guest_id == id);


            if (guest == null)
            {
                return null;
            }
            //Delete the guest
            _db.Guests.Remove(guest);

            await _db.SaveChangesAsync();

            return guest;

        }

        //here it will show all things
        public async Task<IEnumerable<Guest>> GetAllAsync()
        {
            return await _db.Guests.ToListAsync();

        }
        public async Task<Guest> GetAsync(Guid id)
        {
            return await _db.Guests.FirstOrDefaultAsync(x => x.Guest_id == id);


        }
        public async Task<Guest> UpdateAsync(Guid id, Guest guest)
        {
            //here we are making searching is there any id which is existing

            var existingguest = await _db.Guests.FirstOrDefaultAsync(x => x.Guest_id == id);

            if (existingguest == null)
            {
                return null;
            }

            // here we are updating the all value except id becauses it will remain same

            existingguest.E_mail = guest.E_mail;

            existingguest.Guest_Name = guest.Guest_Name;

            existingguest.Address = guest.Address;

            existingguest.Gender = guest.Gender;

            existingguest.Phone_number = guest.Phone_number;

            await _db.SaveChangesAsync();

            return existingguest;

        }

    }
}

