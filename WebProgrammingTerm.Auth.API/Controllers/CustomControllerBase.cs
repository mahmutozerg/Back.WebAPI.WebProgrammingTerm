using Microsoft.AspNetCore.Mvc;
using WebProgrammingTerm.Auth.Core.DTOs;

namespace WebProgrammingTerm.Auth.API.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
public class CustomControllerBase:ControllerBase 
{
    [NonAction]
    public IActionResult CreateActionResult<T>(Response<T> res) where T : class
    {

        return new ObjectResult(res)
        {
            StatusCode = res.StatusCode
        };

    }


}