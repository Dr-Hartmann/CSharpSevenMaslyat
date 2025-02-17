using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using DTO;

namespace Registr.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;

        public AccController(UserManager<IdentityUser> userManager) 
        {
            this.userManager = userManager;
        }

        [HttpGet]
        public IActionResult Hi()
        {
            if (User.Identity == null || !User.Identity.IsAuthenticated)
            {
                return Ok("No Auth");
            }

            return Ok("You Auth");
        }

        [Authorize]
        [HttpGet("Profile")]
        public async Task<IActionResult> Profile()
        {
            var currentUser = await userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return BadRequest();
            }

            var UserProf = new UserProfile
            {
                Id = currentUser.Id,
                Name = currentUser.UserName ?? "",
                Email = currentUser.Email ?? "",
                PhoneNumber = currentUser.PhoneNumber ?? ""
            };

            return Ok(UserProf);
        }
    }
}
