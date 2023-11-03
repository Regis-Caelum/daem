using YabieFrontend.Models;

namespace YabieFrontend.IServices;

public interface IProductService
{
    Task<IEnumerable<Product>> ListAllProducts();
    Task<Product> GetProduct(Guid productId);
}