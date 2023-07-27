namespace Web.Model;

public class Product
{
    public int ProductId { get; set; }
    public string CategoryName { get; set; }
    public string ProductName { get; set; }
    public string Color { get; set; }
    public decimal StandardCost { get; set; }
    public DateTime SellStartDate { get; set; }
}