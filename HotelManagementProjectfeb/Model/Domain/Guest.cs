using System.ComponentModel.DataAnnotations;

namespace HotelManagementProjectfeb.Model.Domain
{
    public class Guest
    {
        [Key]

        public Guid Guest_id { get; set; }

        public string E_mail { get; set; }

        public string Guest_Name { get; set; }

        public string Gender { get; set; }

        public string Address { get; set; }

        public long Phone_number { get; set; }
        
        public virtual ICollection<Reservation> Reservations { get; set; }


    }
}
