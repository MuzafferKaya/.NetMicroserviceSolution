using Microsoft.AspNetCore.Identity;

namespace Mongo.Services.AuthAPI.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
