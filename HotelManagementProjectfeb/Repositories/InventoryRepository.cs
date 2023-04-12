using HotelManagementProjectfeb.Model.Domain;
using Microsoft.EntityFrameworkCore;
using HotelManagementProjectfeb.Data;

namespace HotelManagementProjectfeb.Repositories
{
    public class InventoryRepository : IInventoryRepositorycs
    {
        //using private readonly data types can
        //help promote good programming practices such as encapsulation, immutability,
        //and thread safety, making the resulting code more robust and easier to maintain.
        //here created object HotelManagementDataContext to perform all crud operation.
        private readonly ZzzDbContext _db;
        public InventoryRepository(ZzzDbContext db)
        {
            _db = db;
        }

        //here it is add more guest so it is work on post method
        public async Task<Inventory> AddAsync(Inventory inventory)
        {
            inventory.Inventory_Id = Guid.NewGuid();

            await _db.AddAsync(inventory);

            await _db.SaveChangesAsync();

            return inventory;

        }


        // here we are deleting basis  on id if id will got then delete other than do not delete
        public async Task<Inventory> DeleteAsync(Guid id)

        {
            var inventory = await _db.Inventories.FirstOrDefaultAsync(x => x.Inventory_Id == id);


            if (inventory == null)
            {
                return null;
            }
            //Delete 
            _db.Inventories.Remove(inventory);

            await _db.SaveChangesAsync();

            return inventory;

        }

        //here it will show all things
        public async Task<IEnumerable<Inventory>> GetAllAsync()
        {
            return await _db.Inventories.ToListAsync();

        }
        public async Task<Inventory> GetAsync(Guid id)
        {
            return await _db.Inventories.FirstOrDefaultAsync(x => x.Inventory_Id == id);


        }
        public async Task<Inventory> UpdateAsync(Guid id, Inventory inventory)
        {
            //here we are making searching is there any id which is existing

            var existinginventory = await _db.Inventories.FirstOrDefaultAsync(x => x.Inventory_Id == id);

            if (existinginventory == null)
            {
                return null;
            }

            // here we are updating the all value except id becauses it will remain same

            existinginventory.Inventory_Name = inventory.Inventory_Name;

            existinginventory.quantity = inventory.quantity;

            await _db.SaveChangesAsync();

            return existinginventory;

        }

    }
}




