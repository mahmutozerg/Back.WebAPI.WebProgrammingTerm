namespace SharedLibrary.DTO;

public class OrderAddDto
{
 
    public string Shipment { get; set; } = string.Empty;
    public string LocationId { get; set; } = string.Empty;
    public List<string> ProductIdList { get; set; } = new List<string>();
    public string PaymentCard { get; set; } = string.Empty;
}