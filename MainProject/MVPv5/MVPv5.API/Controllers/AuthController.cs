//using Microsoft.AspNetCore.Mvc;

//namespace MVPv5.API.Controllers;

//public class AuthController : ControllerBase
//{
//    //        private readonly IConfiguration _configuration;

//    //        public AuthController(IConfiguration configuration)
//    //        {
//    //            _configuration = configuration;
//    //        }

//    //        [HttpPost("login")]
//    //        public IActionResult Login([FromBody] LoginRequest request)
//    //        {
//    //            if (IsValidUser(request))
//    //            {
//    //                var claims = new[]
//    //                {
//    //                new Claim(ClaimTypes.Name, request.Username),
//    //                new Claim(ClaimTypes.Role, "Admin")
//    //            };

//    //                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Flat_earth_hitler_caput_super_secret_key_1234512345!"));
//    //                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

//    //                var token = new JwtSecurityToken(
//    //                    issuer: "1",
//    //                    audience: "2",
//    //                    claims: claims,
//    //                    expires: DateTime.Now.AddMinutes(30),
//    //                    signingCredentials: creds
//    //                );

//    //                return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
//    //            }

//    //            return Unauthorized();
//    //        }

//    //        private bool IsValidUser(LoginRequest request)
//    //        {
//    //            // Логика проверки пользователя
//    //            return request.Username == "admin" && request.Password == "password";
//    //        }
//    //    }

//}