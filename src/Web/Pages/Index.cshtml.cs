using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Web.Model;

namespace Web.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly IConfiguration _configuration;

    public IndexModel(ILogger<IndexModel> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    public void OnGet()
    {
        var connectionString = _configuration.GetValue<string>("SqlConnectionString");
        var query = "select " +
                        "p.ProductID, " +
                        "pc.Name as CategoryName, " + 
                        "p.Name as ProductName, " +
                        "p.Color, " +
                        "p.StandardCost, " +
                        "p.SellStartDate " +
                        "from [SalesLT].[Product] p " +
                        "inner join [SalesLT].[ProductCategory] pc on p.ProductCategoryID = pc.ProductCategoryID " +
                        "where pc.Name = 'Mountain Bikes'";

        var products = new List<Product>();

        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();

            using (var command = new SqlCommand(query, connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        products.Add(
                            new Product
                            {
                                ProductId = reader.GetInt32(0),
                                CategoryName = reader.GetString(1),
                                ProductName = reader.GetString(2),
                                Color = reader.GetString(3),
                                StandardCost = reader.GetDecimal(4),
                                SellStartDate = reader.GetDateTime(5)
                            }
                        );
                    }
                }
            }
        }

        ViewData["products"] = products;
    }
}
