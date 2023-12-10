using WebProgrammingTerm.Core.DTO;
using WebProgrammingTerm.Core.Models;

namespace WebProgrammingTerm.Core.Mappers;

public static class UserCommentMapper
{
    public static UserComments ToUserComment(UserCommentAddDto userCommentUpdateDto)
    {
        var userComment = new UserComments()
        {
            Id = Guid.NewGuid().ToString(),
            Title = userCommentUpdateDto.Title,
            Content = userCommentUpdateDto.Content,
            Rate = userCommentUpdateDto.Rate,
        };


        return userComment;
    }
}