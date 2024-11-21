using WebAPI.Models;

namespace WebAPI.Interfaces
{
    public interface IProductoRepository
    {
        Task<Producto> CreateProductoAsync(Producto producto);

        Task DeleteProductoByIdAsync(int id);

        Task<Producto> GetProductoByIdAsync(int id);

        Task<IEnumerable<Producto>> GetProductosAsync();

        Task UpdateProductoAsync(Producto producto);

    }
}