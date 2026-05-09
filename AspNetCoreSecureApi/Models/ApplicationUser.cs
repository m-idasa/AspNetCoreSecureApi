using AspNetCoreSecureApi.Enums;
using Microsoft.AspNetCore.Identity;

namespace AspNetCoreSecureApi.Models;

public class ApplicationUser : IdentityUser
{
    public Role Role { get; set; }
}