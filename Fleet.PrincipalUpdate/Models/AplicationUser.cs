using Microsoft.AspNetCore.Identity;

namespace Fleet.PrincipalUpdate.Models
{
    public class AplicationUser:IdentityUser
    {
        public string? Name  { get; set; }
    }
}
