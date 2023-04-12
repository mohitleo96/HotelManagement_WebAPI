using System.ComponentModel.DataAnnotations.Schema;

namespace HotelManagementProjectfeb.Model.Domain
{
    public class Staff
    {
        public Guid Id { get; set; }

        public string UserName { get; set; }
        public string Password { get; set; }
        [NotMapped]
        public List<string> Roles { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        //Navigation property
        public List<User_Roles> UserRoles { get; set; }

    }
}
