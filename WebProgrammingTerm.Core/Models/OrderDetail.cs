using System.Security.Cryptography.X509Certificates;

namespace WebProgrammingTerm.Core.Models;

public class OrderDetail
{    

    public Order Order { get; set; } = new Order();
    public string OrderId { get; set; } = string.Empty;

    public float Tax { get; set; } = 0f;
    public string PaymentMethod { get; set; } = string.Empty;
}