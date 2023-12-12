using System.Security.Claims;
using SharedLibrary.DTO;
using SharedLibrary.Models;


namespace WebProgrammingTerm.Core.Services;

public interface IUserCommentService:IGenericService<UserComments>
{
    Task<CustomResponseDto<UserComments>> UpdateAsync(UserCommentUpdateDto userCommentUpdateDto,ClaimsIdentity claimsIdentity);
    Task<CustomResponseDto<UserComments>> AddAsync(UserCommentAddDto userCommentAddDto, ClaimsIdentity claimsIdentity);
}