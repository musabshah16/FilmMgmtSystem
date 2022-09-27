using Microsoft.AspNetCore.Identity;

namespace FilmMgmtSystem.WebApi.WebApi.Authentication
{
    public class ApplicationRole : IdentityRole
    {
        public int Id { get; set; }
        public string RoleName { get; set; }

        
    }
}
