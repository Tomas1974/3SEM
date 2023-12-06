using api.Filters;
using api.TransferModels;
using Microsoft.AspNetCore.Mvc;
using service.Services;


namespace api.Controllers;

public class SearchController : ControllerBase
{
    private readonly SearchService _searchService;

    public SearchController(SearchService searchService)
    {
        _searchService = searchService;
    }

    [HttpGet]
    [ValidateModel]
    [Route("/avatar")]
    public ResponseDto SearchAvatar([FromQuery] string searchQuery)
    {
        HttpContext.Response.StatusCode = StatusCodes.Status201Created;
        return new ResponseDto()
        {
            MessageToClient = "Succesfully searched for an Avatar",
            ResponseData = _searchService.SearchAvatar(searchQuery)
        };
    }
}
