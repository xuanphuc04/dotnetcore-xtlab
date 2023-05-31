using L74Razor_PageModel.Models;

namespace L74Razor_PageModel.Services
{
	public class ProductsService
	{
		private List<Product> products= new List<Product>();
		public ProductsService() { 
			LoadProducts();
		}

		public void LoadProducts()
		{
			products.Clear();
			products.Add(new Product()
			{
				Id= 1,
				Name= "iPhone",
				Description= "Điện thoại iPhone",
				Price= 1000
			});
			products.Add(new Product()
			{
				Id = 2,
				Name = "Xiaomi",
				Description = "Điện thoại Xiaomi",
				Price = 600
			});
			products.Add(new Product()
			{
				Id = 3,
				Name = "Samsung",
				Description = "Điện thoại Samsung",
				Price = 800
			});
			
		}

		public List<Product> GetAllProducts()
		{
			return products;
		}

		public Product FindProduct(int id)
		{
			var result = (from product in products
					where product.Id == id
					select product).FirstOrDefault();
			return result;
		}
	}
}
