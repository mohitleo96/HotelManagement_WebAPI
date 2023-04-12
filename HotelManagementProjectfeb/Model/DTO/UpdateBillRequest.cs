using System.ComponentModel.DataAnnotations.Schema;

namespace HotelManagementProjectfeb.Model.DTO
{
    public class UpdateBillRequest
    {
        public int stay_dates { get; set; }

        //room price = adult*1000+child*500 *stay_dates
        public decimal total_bill { get; set; }
        public Guid Room_id { get; set; }


        //[ForeignKey("Reservation")]
        public Guid Reservation_id { get; set; }
      
    }
}
