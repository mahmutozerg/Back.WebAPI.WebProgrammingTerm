using Microsoft.AspNetCore.Mvc;
using SharedLibrary.DTO;
using WebProgrammingTerm.Core.DTO;

namespace WebProgrammingTerm.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CustomControllerBase:ControllerBase
{
    [NonAction]
    public IActionResult CreateActionResult<T>(CustomResponseDto<T> res)
    {

        return new ObjectResult(res)
        {
            StatusCode = res.StatusCode
        };

    }
    [NonAction]

    public IActionResult CreateActionResult<T>(CustomResponseListDataDto<T> res)
    {


        return new ObjectResult(res)
        {
            StatusCode = res.StatusCode
        };

    }    
    [NonAction]

    public IActionResult CreateActionResult(CustomResponseNoDataDto res)
    {
        return new ObjectResult(res)
        {
            StatusCode = res.StatusCode
        };

    }    
}