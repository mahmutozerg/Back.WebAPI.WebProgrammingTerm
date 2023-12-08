using WebProgrammingTerm.Core.DTO;
using WebProgrammingTerm.Core.Models;

namespace WebProgrammingTerm.Core.Services;

public interface IUserCommentService:IGenericService<UserComments>
{
    Task<CustomResponseDto<UserComments>> UpdateAsync(UserCommentUpdateDto userCommentUpdateDto,string updatedBy);
    Task<CustomResponseDto<UserComments>> AddAsync(UserCommentAddDto userCommentAddDto, string createdBy);
}