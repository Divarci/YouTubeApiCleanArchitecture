using Microsoft.AspNetCore.Mvc;
using YouTubeApiCleanArchitecture.Domain.Abstraction;
using Abstraction = YouTubeApiCleanArchitecture.Domain.Abstraction;

namespace YouTubeApiCleanArchitecture.API.Controllers;

[ApiController]
public class BaseController : ControllerBase
{
    public IActionResult CreateResult<TDto>(
        Result<TDto> result) 
        where TDto : Abstraction.IResult
        => result.StatusCode == 204
            ? new ObjectResult(null) { StatusCode = 204}
            : new ObjectResult(result) { StatusCode = result.StatusCode };    
}
