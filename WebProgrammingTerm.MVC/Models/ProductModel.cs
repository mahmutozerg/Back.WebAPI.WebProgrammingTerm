using System.Collections.Generic;
using SharedLibrary.DTO;

namespace WebProgrammingTerm.MVC.Models;

public class ProductModel
{
    public ProductWCommentDto ProductWGetDto { get; set; }
    public UserFavoritesListDto UserFavoritesDto { get; set; }
}