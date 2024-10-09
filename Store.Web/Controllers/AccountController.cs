using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.UserServices.Dto;
using store.Services;
using store.Services.Dto;
using Store.Data.Entities.IdentityEntities;
using Store.Web.Controllers;

namespace store.Web.Controllers
{

    public class AccountController : BaseController
    {
        private readonly IUserServices _userServices;
        private readonly UserManager<AppUser> _userManager;

        public AccountController(IUserServices userServices, UserManager<AppUser> userManager)
        {
            _userServices = userServices;
            _userManager = userManager;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)

        {
            var user = await _userServices.Login(loginDto);

            if (user == null)
                return BadRequest(new CustomException(400, "Email Does Not Exist"));

            return Ok(user);
        }


        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            var user = await _userServices.Register(registerDto);

            if (user == null)
                return BadRequest(new CustomException(400, "Email Already Exist"));

            return Ok(user);
        }


        [HttpGet("GetCurrentUser")]
        [Authorize]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        { 
            var UserId = User?.FindFirst("UserId");                               //claims ممكن اعملها بالايميل - بشتغل ع ال 

            var user = await _userManager.FindByIdAsync(UserId.Value);

            return new UserDto

            {
                Id=Guid.Parse(user.Id),    
                DisplayName = user.DisplayName,
                Email = user.Email,
            };
        }
    }
}