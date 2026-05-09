using AspNetCoreSecureApi.Models;
using AspNetCoreSecureApi.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using AspNetCoreSecureApi.Enums;
using System.Security;
using Microsoft.Identity.Web;
using Microsoft.AspNetCore.Authorization;
using AspNetCoreSecureApi;


namespace MvcMovie.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : Controller
{
    private UserManager<IdentityUser> _userManager;
    private readonly AppDbContext _context;
    private readonly TokenService _tokenService;

    public UserController(UserManager<IdentityUser> userManager, AppDbContext context,
        TokenService tokenService, ILogger<UserController> logger)
    {
        _userManager = userManager;
        _context = context;
        _tokenService = tokenService;
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register(RegistrationRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _userManager.CreateAsync(
            new ApplicationUser { UserName = request.Username, Email = request.Email, Role = request.Role },
            request.Password!
        );

        if (result.Succeeded)
        {
            request.Password = "";
            return CreatedAtAction(nameof(Register), new { email = request.Email, role = Role.User }, request);
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(error.Code, error.Description);
        }

        return BadRequest(ModelState);
    }


    [HttpPost]
    [Route("login")]
    public async Task<ActionResult<AuthResponse>> Authenticate([FromBody] AuthRequest user)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var managedUser = await _userManager.FindByEmailAsync(user.Email!);

        if (managedUser == null)
        {
            return BadRequest("Bad credentials");
        }

        var isPasswordValid = await _userManager.CheckPasswordAsync(managedUser, user.Password!);

        if (!isPasswordValid)
        {
            return BadRequest("Bad credentials");
        }

        var userInDb = _context.Users.FirstOrDefault(u => u.Email == user.Email);

        if (userInDb is null)
        {
            return Unauthorized();
        }

        var accessToken = _tokenService.CreateToken(userInDb);
        await _context.SaveChangesAsync();

        return Ok(new AuthResponse
        {
            Username = userInDb.UserName,
            Email = userInDb.Email,
            Token = accessToken,
        });
    }
    // 
    // GET: /HelloWorld/
    [HttpPost]
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "ADMIN")]
    public string Index()
    {
        return "This is my default action...";
    }
    // 
    // GET: /HelloWorld/Welcome/ 
    //[HttpGet]
    //[Route("Welcome")]
    //public async Task<string> WelcomeAsync(IdentityUser user, IServiceProvider serviceProvider)
    //{
    //    if (user != null)
    //    {
            
    //        //adding custom roles
    //        var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    //        string[] roleNames = { "Admin", "Manager", "Member" };
    //        IdentityResult roleResult;

    //        foreach (var roleName in roleNames)
    //        {
    //            //creating the roles and seeding them to the database
    //            var roleExist = await RoleManager.RoleExistsAsync(roleName);
    //            if (!roleExist)
    //            {
    //                roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
    //            }
    //        }
    //        String r = "";
    //        IdentityResult result = await _userManager.AddToRoleAsync(user, "Admin");
    //        Task<IList<String>> roles = _userManager.GetRolesAsync(user);
    //        foreach (var role in roles.Result) { 
    //            r += role;
    //        }
    //        return r;
            
    //    }
    //    return "This is the Welcome action method...";
    //}
}