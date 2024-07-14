using FINSHARK.Models;

namespace FINSHARK.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}
