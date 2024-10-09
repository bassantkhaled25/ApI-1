using Services.UserServices.Dto;
namespace store.Services.Dto
{
    public interface IUserServices

    {
        Task<UserDto> Register(RegisterDto registerDto);
        Task<UserDto> Login(LoginDto loginDto); 
    }
}
