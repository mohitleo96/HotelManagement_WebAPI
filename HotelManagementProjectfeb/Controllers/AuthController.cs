using HotelManagementProjectfeb.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace HotelManagementProjectfeb.Controllers


{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly IUserRepository userRepository;
        private readonly ITokenHandler tokenHandler;

        public AuthController(IUserRepository userRepository, ITokenHandler tokenHandler )
        {
            this.userRepository = userRepository;
            this.tokenHandler = tokenHandler;
        }

        [HttpPost]
        [Route("login")]
        //we cannot keep here authorize because any one wants to authorize 
       
        public async Task<IActionResult> LoginAsync(Model.DTO.LoginRequest loginRequest)
        {
            //validate the Incoming Request we have used fluent validation
            // Check if user is authenticated
            // Check username and password


            var user = await userRepository.AuthenticateAsync(
                loginRequest.UserName
                ,loginRequest.Password);


            if (user != null)
            {
                 //Generate a JWT Token
                var token = await tokenHandler.CreateTokenAsync(user);
                return Ok(token);
            }

            return BadRequest("Username or Password is Incorrect.");
        }
    }
}
