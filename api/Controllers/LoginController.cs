using api.TransferModels;
using infrastructure.DataModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using service.Services;

namespace api.Controllers;

[ApiController]
public class LoginController : ControllerBase
{
    private LoginService _service;

    public LoginController(LoginService service)
    {
        _service = service;
    }

    /*
     * Handles the login functionality for users.
     * It takes in a LoginModel object containing the user's email and password,
     * authenticates the user, generates a token, and returns a
     * ResponseDto object with a welcome message and the generated token. 
     */
    [AllowAnonymous]
    [HttpPost]
    [Route("/api/account/login")]
    public ResponseDto Login([FromBody] LoginModel model)
    {
        UserModel user = _service.Authenticate(model);
            var token = _service.GenerateToken(user);
            
            return new ResponseDto()
            {
                MessageToClient = "Welcome "+ user.full_name,
                ResponseData = new { token }
            };
    }
    
    /*
     * Handles the user registration functionality.
     * It takes in a RegisterModel object containing the user's registration details,
     * registers the user, and returns a ResponseDto object with a message
     * indicating the successful registration.
     */
    [HttpPost]
    [Route("/api/account/register")]
    public ResponseDto Register([FromBody] RegisterModel model)
    {
        return new ResponseDto()
        {
            MessageToClient = "Register new user",
            ResponseData =_service.Register(model)
        };
    }

    [HttpPut]
    [Route("/api/account/update/{user_id}")]
    public ResponseDto Update([FromBody] UserModel userModel, [FromRoute] int user_id)
    {
        UserModel updatedUser = _service.Update(user_id, userModel);
        var token = _service.GenerateToken(updatedUser);
        return new ResponseDto()
        {
            MessageToClient = "Successfully updated a user",
            ResponseData = new { token }
        };
    }
}