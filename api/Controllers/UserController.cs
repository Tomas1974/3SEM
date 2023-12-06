using System.Security.Claims;
using infrastructure.DataModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

public class UserController : ControllerBase
{
    [Route("api/admin")]
    [Authorize(Roles = "Admin")]
    [HttpGet]
    public IActionResult AdminsEndpoint()
    {
        var currentUser = GetCurrentUser();
        return Ok($"Hi {currentUser.Name} you are an {currentUser.Admin}");
    }
    
    
    [Route("api/get")]
    [HttpGet]
    private UserModel GetCurrentUser()
    {
        var identity = HttpContext.User.Identity as ClaimsIdentity;

        if (identity != null)
        {
            var userClaims = identity.Claims;

            return new UserModel
            {
                Name = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value,
                Mail = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Email)?.Value,
                Admin = userClaims.FirstOrDefault(o=>  o.Type == ClaimTypes.Role)?.Value
                
            };
        }
        return null;
    }

    
}