using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Exceptions;
using WebAPI.Interfaces;
using WebAPI.Models;

namespace WebAPI.Repository
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly ContextDb _context;

        public ProductoRepository(ContextDb contextDb)
        {
            _context = contextDb;
        }

        public async Task<IEnumerable<Producto>> GetProductosAsync()
        {
            try
            {
                return await _context.Productos.Where(x => x.Estado == 1).ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Producto> GetProductoByIdAsync(int id)
        {
            try
            {
                var product = await _context.Productos.Where(x => x.Id == id).FirstOrDefaultAsync();

                if (product == null)
                {
                    throw new NotFoundException("Producto no encontrado");
                }

                return product;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Producto> CreateProductoAsync(Producto producto)
        {
            try
            {
                _context.Productos.Add(producto);
                await _context.SaveChangesAsync();

                return producto;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UpdateProductoAsync(Producto producto)
        {
            try
            {
                var oldProduct = await _context.Productos.FindAsync(producto.Id);

                if (oldProduct == null)
                {
                    throw new NotFoundException("Producto no encontrado");
                }

                oldProduct.Nombre = producto.Nombre;
                oldProduct.Descripcion = producto.Descripcion;
                oldProduct.Precio = producto.Precio;
                oldProduct.Stock = producto.Stock;

                _context.Entry(oldProduct).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteProductoByIdAsync(int id)
        {
            try
            {
                var producto = await _context.Productos.FindAsync(id);

                if (producto == null)
                {
                    throw new NotFoundException("Producto no encontrado");
                }

                _context.Productos.Remove(producto);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
