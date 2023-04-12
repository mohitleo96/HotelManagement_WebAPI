using HotelManagementProjectfeb.Model.Domain;
using Microsoft.EntityFrameworkCore;
using HotelManagementProjectfeb.Data;

namespace HotelManagementProjectfeb.Repositories
{
    public class RoomRepository:IRoomRepository
    {
        //using private readonly data types can
        //help promote good programming practices such as encapsulation, immutability,
        //and thread safety, making the resulting code more robust and easier to maintain.
        //here created object HotelManagementDataContext to perform all crud operation.
        private readonly ZzzDbContext _db;
        public RoomRepository(ZzzDbContext db)
        {
            _db = db;
        }

        //here it is add more guest so it is work on post method
        public async Task<Room> AddAsync(Room room)
        {
            room.room_id = Guid.NewGuid();

            await _db.AddAsync(room);

            await _db.SaveChangesAsync();

            return room;

        }


        // here we are deleting basis  on id if id will got then delete other than do not delete
        public async Task<Room> DeleteAsync(Guid id)

        {
            var room = await _db.Rooms.FirstOrDefaultAsync(x => x.room_id == id);


            if (room == null)
            {
                return null;
            }
            //Delete 
            _db.Rooms.Remove(room);

            await _db.SaveChangesAsync();

            return room;

        }

        //here it will show all things
        public async Task<IEnumerable<Room>> GetAllAsync()
        {
            return await _db.Rooms.ToListAsync();

        }
        public async Task<Room> GetAsync(Guid id)
        {
            return await _db.Rooms.FirstOrDefaultAsync(x => x.room_id == id);


        }
        public async Task<Room> UpdateAsync(Guid id, Room room)
        {
            //here we are making searching is there any id which is existing

            var existingroom = await _db.Rooms.FirstOrDefaultAsync(x => x.room_id == id);

            if (existingroom == null)
            {
                return null;
            }

            // here we are updating the all value except id becauses it will remain same

            existingroom.room_status = room.room_status;

            existingroom.room_rate = room.room_rate;

            await _db.SaveChangesAsync();

            return existingroom;

        }

    }
}


