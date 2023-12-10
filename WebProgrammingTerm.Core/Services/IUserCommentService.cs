using System.Security.Claims;
using WebProgrammingTerm.Core.DTO;
using WebProgrammingTerm.Core.Models;

namespace WebProgrammingTerm.Core.Services;

public interface IUserCommentService:IGenericService<UserComments>
{
    Task<CustomResponseDto<UserComments>> UpdateAsync(UserCommentUpdateDto userCommentUpdateDto,ClaimsIdentity claimsIdentity);
    Task<CustomResponseDto<UserComments>> AddAsync(UserCommentAddDto userCommentAddDto, ClaimsIdentity claimsIdentity);
}