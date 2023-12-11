using Fleet.PrincipalUpdate.Models;

namespace Fleet.PrincipalUpdate.Services.IServices
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(AplicationUser aplicationUser);
    }
}
