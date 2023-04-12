using HotelManagementProjectfeb.Model.Domain;

namespace HotelManagementProjectfeb.Repositories
{
    public class StaticUserRepository : IUserRepository
    {

        private List<Staff> Staffs = new List<Staff>()
        {
      
        };

        

        public async Task<Staff> AuthenticateAsync(string username, string password)
        {
            //here we are using InvariantCultureIgnoreCase for ignoring upper case or lower case
            //using method syntax
            var user = Staffs.Find(x => x.UserName.Equals(username, StringComparison.InvariantCultureIgnoreCase) &&
           x.Password == password);

         
            //return user
            
            return user;
        }
    }
}
