using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedLibrary.DTO;
using WebProgrammingTerm.Core.Services;

namespace WebProgrammingTerm.API.Controllers;

[Authorize]
public class UserCommentsController:CustomControllerBase
{
    private readonly IUserCommentService _userCommentService;

    public UserCommentsController(IUserCommentService  userCommentService)
    {
        _userCommentService = userCommentService;
    }
    
    [HttpPost("[action]")]
    public async Task<IActionResult> Add( UserCommentAddDto userCommentAddDto)
    {
        return CreateActionResult(await _userCommentService.AddAsync(userCommentAddDto,(ClaimsIdentity)User.Identity));
    }
    [HttpPost("[action]")]
    public async Task<IActionResult> Update(UserCommentUpdateDto userCommentUpdateDto)
    {
        var claimsIdentity = (ClaimsIdentity)User.Identity;
        return CreateActionResult(await _userCommentService.UpdateAsync(userCommentUpdateDto,(ClaimsIdentity)User.Identity));
    }}