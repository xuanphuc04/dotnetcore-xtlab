using Models;
using System.Xml.Linq;

namespace L70L71Razor_Partial_ViewComponent.Services
{
	public class ProductService
	{
		public List<Product> Products { set; get; } = new List<Product>()
        {
                new Product() { Name = "SP1", Description = "Sản phẩm 1", Price = 1000 },
                new Product() { Name = "SP2", Description = "Sản phẩm 2", Price = 600 },
                new Product() { Name = "SP3", Description = "Sản phẩm 3", Price = 800 },
                new Product() { Name = "SP4", Description = "Sản phẩm 4", Price = 900 },
		};
}
}
