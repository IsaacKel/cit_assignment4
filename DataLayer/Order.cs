namespace DataLayer;
public class Order
{
  // Properties
  public int Id { get; set; } = 0;
  public string? CustomerId { get; set; }
  public int EmployeeId { get; set; } = 0;
  public DateTime Date { get; set; } = new DateTime();
  public DateTime Required { get; set; } = new DateTime();
  public DateTime ShippedDate { get; set; } = DateTime.Now;
  public decimal Freight { get; set; } = 0m;
  public string? ShipName { get; set; }
  public string? ShipAddress { get; set; }
  public string? ShipCity { get; set; }
  public string? ShipPostalCode { get; set; }
  public string? ShipCountry { get; set; }
  public virtual ICollection<OrderDetail> OrderDetails { get; set; }
}