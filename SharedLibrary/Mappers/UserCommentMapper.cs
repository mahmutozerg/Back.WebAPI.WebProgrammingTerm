using SharedLibrary.DTO;
using SharedLibrary.Models;

namespace SharedLibrary.Mappers;

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