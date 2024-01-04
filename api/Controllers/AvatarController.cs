using api.Filters;
using api.TransferModels;
using infrastructure.DataModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using service.Services;

namespace api.Controllers;

[ApiController]
public class AvatarController : Controller
{
    private readonly AvatarService _avatarService;
    
    public AvatarController(AvatarService avatarService)
    {
        _avatarService = avatarService;
    }
    
    /*
     * Gets all avatars from the database.  
     */
    [HttpGet]
    [Route("/avatar/all")]
    [Authorize]
    public ResponseDto GetAllAvatars()
    {
        return new ResponseDto()
        {
            MessageToClient = "Succesfully got all Avatars",
            ResponseData =  _avatarService.GetAllAvatars()
        };
    }
    
    [HttpGet]
    [Route("/avatar/allDeleted")]
    [Authorize]
    public ResponseDto GetAllDeletedAvatars()
    {
        return new ResponseDto()
        {
            MessageToClient = "Successfully got all deleted avatars",
            ResponseData =  _avatarService.GetAllDeletedAvatars()
        };
    }
    
    /*
     * Creates a new avatar in the database. 
     */
    [HttpPost]
    [ValidateModel]
    [Route("/avatar")]
    [Authorize]
    public ResponseDto postAvatar([FromBody] AvatarModel avatar)
    {
        return new ResponseDto()
        {
            MessageToClient = "Successfully created an Avatar",
            ResponseData = _avatarService.CreateAvatar(avatar)
        };
    }
    
    /*
     * Updates an existing avatar in the database. 
     */
    [HttpPut]
    [Route("/avatar/{avatar_id}")]
    [Authorize]
    public ResponseDto putAvatar([FromRoute] int avatar_id, [FromBody] AvatarModel avatar)
    {
        return new ResponseDto()
        {
            MessageToClient = "Succesfully updated an Avatar",
            ResponseData =  _avatarService.UpdateAvatar(avatar_id ,avatar)
        };
    }
    
    /*
     * Deletes an existing avatar from the database. 
     */
    [HttpDelete]
    
    [Route("/avatar/{avatar_id}")]
    [Authorize]
    public object deleteAvatar([FromRoute] int avatar_id)
    {
        _avatarService.deleteAvatar(avatar_id);
        return  new ResponseDto()
        {
            MessageToClient = "Succesfully deleted avatar"
        };
    }

    [HttpDelete]
    [Route("/avatar/enable/{avatar_id}")]
    [Authorize(Roles = "Admin")]
    public object enableAvatar([FromRoute] int avatar_id)
    {
        
        _avatarService.enableAvatar(avatar_id);
        return new ResponseDto()
        {
            MessageToClient = "Successfully enabled an avatar",
        };
    }
    
    /*
     * Gets the information of an existing avatar from the database.
     */
    [HttpGet]
    [Route("/avatar/{avatar_id}")]
    [Authorize]
    public ResponseDto getAvatarInformation([FromRoute]int avatar_id)
    {
        
        return new ResponseDto()
        {
            MessageToClient = "Succesfully got all AvatarInformation",
            ResponseData =  _avatarService.GetAvatarInformation(avatar_id)
        };
    }
}