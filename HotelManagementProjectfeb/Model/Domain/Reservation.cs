using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelManagementProjectfeb.Model.Domain
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

        //here reservation id will be foreign key inside bill table bill table can fetch any 
        //reservation records
        
        public virtual ICollection<Bill> Bills { get; set; }


    }
}
