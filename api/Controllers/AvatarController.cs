using api.Filters;
using api.TransferModels;
using infrastructure.DataModels;
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
    
    
    [HttpGet]
    [Route("/avatar/all")]
    public ResponseDto GetAllAvatars()
    {
        return new ResponseDto()
        {
            MessageToClient = "Succesfully got all Avatars",
            ResponseData =  _avatarService.GetAllAvatars()
        };
    }
    
    [HttpPost]
    //[ValidateModel]
    [Route("/avatar")]
    public ResponseDto postAvatar([FromBody] AvatarModel avatar)
    {
        return new ResponseDto()
        {
            MessageToClient = "Succesfully created an Avatar",
            ResponseData = _avatarService.CreateAvatar(avatar)
        };
    }
        

    [HttpPut]
    [Route("/avatar/{avatar_id}")]
    public ResponseDto putAvatar([FromRoute] int avatar_id, [FromBody] AvatarModel avatar)
    {
        return new ResponseDto()
        {
            MessageToClient = "Succesfully updated an Avatar",
            ResponseData =  _avatarService.UpdateAvatar(avatar_id ,avatar)
        };
    }
    
    [HttpDelete]
    //[ValidateModel]
    [Route("/avatar/{avatar_id}")]
    public object deleteAvatar([FromRoute] int avatar_id)
    {
        _avatarService.deleteAvatar(avatar_id);
        return  new ResponseDto()
        {
            MessageToClient = "Succesfully deleted avatar"
        };
    }
    [HttpGet]
    //[ValidateModel]
    [Route("/avatar/{avatar_id}")]
    public ResponseDto getAvatarInformation([FromRoute]int avatar_id)
    {
        
        return new ResponseDto()
        {
            MessageToClient = "Succesfully got all AvatarInformation",
            ResponseData =  _avatarService.GetAvatarInformation(avatar_id)
        };
    }
}