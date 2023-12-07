namespace WebProgrammingTerm.Core.DTO;

public class ProductUpdateDto
{
    public string TargetProductId { get; set; }
    public float Price { get; set; } = 0f;
    public string Name { get; set; } = string.Empty;
    public int Stock { get; set; } = 0;
    public float DiscountRate { get; set; } = 0f;

}