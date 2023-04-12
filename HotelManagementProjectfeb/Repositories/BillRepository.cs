using HotelManagementProjectfeb.Model.Domain;
using Microsoft.EntityFrameworkCore;
using HotelManagementProjectfeb.Data;
using HotelManagementProjectfeb.Repositories;

namespace HotelManagementProjectfeb.Repositories
{
    public class BillRepository : IBillRepository
    {
        private readonly ZzzDbContext _db;
        public BillRepository(ZzzDbContext db)
        {
            _db = db;
        }

        //here it is add more guest so it is work on post method
        public async Task<Bill> AddAsync(Bill bill)
        {
            bill.Bill_id = Guid.NewGuid();

            await _db.AddAsync(bill);

            await _db.SaveChangesAsync();

            return bill;

        }


        // here we are deleting basis  on id if id will got then delete other than do not delete
        public async Task<Bill> DeleteAsync(Guid id)

        {
            var bill = await _db.Bills.FirstOrDefaultAsync(x => x.Bill_id == id);


            if (bill == null)
            {
                return null;
            }
            //Delete the guest
            _db.Bills.Remove(bill);

            await _db.SaveChangesAsync();

            return bill;

        }

        //here it will show all things
        public async Task<IEnumerable<Bill>> GetAllAsync()
        {
            return await _db.Bills.ToListAsync();

        }
        public async Task<Bill> GetAsync(Guid id)
        {
            return await _db.Bills.FirstOrDefaultAsync(x => x.Bill_id == id);


        }
        public async Task<Bill> UpdateAsync(Guid id, Bill bill)
        {
            //here we are making searching is there any id which is existing

            var existingbill = await _db.Bills.FirstOrDefaultAsync(x => x.Bill_id == id);

            if (existingbill == null)
            {
                return null;
            }

            // here we are updating the all value except id becauses it will remain same

            existingbill.stay_dates = bill.stay_dates;

            existingbill.Room_id = bill.Room_id;

            existingbill.Reservation_id = bill.Reservation_id;

            await _db.SaveChangesAsync();

            return existingbill;

        }

    }
}

