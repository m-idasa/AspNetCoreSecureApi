using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
namespace Auth.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private UserManager<IdentityUser> _userManager;

    public AuthController(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }
    [HttpGet]
    [Route("assignAdminRole")]
    public async Task<IActionResult> AssignAdminRole(IdentityUser user)
    {
        if (user != null) {
            List<string>  roles = (List<string>)await _userManager.GetRolesAsync(user);
            return new ObjectResult(roles.ToString()) { StatusCode = 200 };

        }
        return BadRequest();
        

        //IdentityUser appUser = new IdentityUser { UserName = user.UserName, Email = user.Email };
        //IdentityResult result = await _userManager.AddToRoleAsync(appUser, "Admin");

        //if (result.Succeeded)
        //{
        //    return Redirect("index");
        //    return StatusCode(200, "role added"); ;
        //    //Whichever Page you want, you can redirect

        //}

        //foreach (IdentityError error in result.Errors)
        //{
        //    ModelState.AddModelError(error.Code, error.Description);
        //}
        //return BadRequest(ModelState);

    }
}