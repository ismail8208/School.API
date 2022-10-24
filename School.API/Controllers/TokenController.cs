using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using School.API.Authentication;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace School.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        [HttpPost]
        public ActionResult<String> auth(AuthenticationRequst authenticationRequst)
        {
            var user = ValidateUserCredatials(authenticationRequst.username,authenticationRequst.password);
            if (user == null)
            {
                return Unauthorized();
            }
            var mySecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("MyAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAASecret"));

            var mySigningCredentials = new SigningCredentials(mySecurityKey,SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>()
            {
                new Claim("sub",user.UserId.ToString()),
                new Claim(ClaimTypes.GivenName,user.FirstName.ToString()),
                new Claim(ClaimTypes.Surname,user.UserId.ToString()),
                new Claim(ClaimTypes.Role,"User")
            };

            var token = new JwtSecurityToken(
                "issuer",
                "Audience",
                claims,
                DateTime.UtcNow,
                DateTime.UtcNow.AddHours(1),
                mySigningCredentials
                );
            var serializedToken = new JwtSecurityTokenHandler().WriteToken(token);
            return Ok(serializedToken);

        }
        private SchoolUser ValidateUserCredatials(string username, string password)
        {
            return new SchoolUser(1, "ismailalahmad90", "ismail", "alahmad");
        }
    }
}
