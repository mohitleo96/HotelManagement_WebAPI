using System.Data;

namespace HotelManagementProjectfeb.Model.Domain
{
    public class User_Roles
    {

        public Guid Id { get; set; }

        //from staff table
        public Guid UserId { get; set; }
        public Staff Staffs { get; set; }

        public Guid RoleId { get; set; }
        public Role Role { get; set; }

    }
}
