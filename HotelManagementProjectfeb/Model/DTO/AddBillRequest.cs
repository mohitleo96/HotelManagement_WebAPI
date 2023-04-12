namespace HotelManagementProjectfeb.Model.DTO
{
    public class AddBillRequest
    {
        public int stay_dates { get; set; }

        public decimal total_bill { get; set; }

        //room price = adult*1000+child*500 *stay_dates
        public Guid Room_id { get; set; }

        //[ForeignKey("Reservation")]
        public Guid Reservation_id { get; set; }
    }
}
