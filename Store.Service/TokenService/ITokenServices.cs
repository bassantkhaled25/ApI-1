using Store.Data.Entities.IdentityEntities;

namespace Services.TokenServices
{
    public interface ITokenServices
    {
        public string CreateToken(AppUser appUser);
    }
}
