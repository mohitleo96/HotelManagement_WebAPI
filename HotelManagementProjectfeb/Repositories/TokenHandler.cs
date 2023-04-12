using HotelManagementProjectfeb.Model.Domain;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HotelManagementProjectfeb.Repositories
{
    
    
        public class TokenHandler : ITokenHandler
        {
            private readonly IConfiguration _configuration;
            //we need to inject configuration here
            public TokenHandler(IConfiguration configuration)
            {
                this._configuration = configuration;
            }
        public Task<string> CreateTokenAsync(Staff user)
        {
            // var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            //create calims
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.GivenName, user.FirstName));
            claims.Add(new Claim(ClaimTypes.Surname, user.LastName));
            claims.Add(new Claim(ClaimTypes.Email, user.UserName));
 

                // Loop into roles of users
                //here we are doing this for each role claims will create 
                //and ForEach perform the specified action on each element.
                user.Roles.ForEach((role) =>
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                });
                //here Jwt:Key must be match with the the jwt key we have useed inside appsettingjson
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            //var

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
               // expires: DateTime.Now.AddMinutes(15),
               expires: DateTime.Now.AddMinutes(15),

                signingCredentials: credential) ;

                return Task.FromResult(new JwtSecurityTokenHandler().WriteToken(token));
            }
        }
    }

