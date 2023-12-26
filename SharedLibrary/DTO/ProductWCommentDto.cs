using SharedLibrary.Models;

namespace SharedLibrary.DTO;

public class ProductWCommentDto:ProductGetDto
{
    public List<UserComments> UserComments { get; set; }
    public ProductDetail ProductDetail { get; set; }
    public bool IsDeleted { get; set; } = false;
}