using System.ComponentModel.DataAnnotations;

namespace HotelManagementProjectfeb.Model.DTO
{
    public class Reservation
    {
        [Key]
        public Guid reservation_id { get; set; }

        public int no_of_adults { get; set; }

        public int no_of_children { get; set; }

        public DateTime Check_out { get; set; }

        public DateTime Check_in { get; set; }

        public bool status { get; set; }

        public int no_of_nights { get; set; }


        public Guid Guest_Id { get; set; }

        public  Guest Guests { get; set; }

        public Guid Room_id { get; set; }

        public  Room Rooms { get; set; }

        public virtual ICollection<Bill> Bills { get; set; }


       
       


    }
}
