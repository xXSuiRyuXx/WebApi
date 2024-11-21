using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;
using WebAPI.Data;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly ContextDb _context;

        public ProductosController(ContextDb context)
        {
            _context = context;
        }

        // GET: api/Productos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Producto>>> GetProductos()
        {
            try
            {
                return await _context.Productos.Where(x => x.Estado == 1).ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\n" + ex.StackTrace);
                return BadRequest("Ha ocurrido un error inesperado");
            }
        }

        // GET: api/Productos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Producto>> GetProducto([FromRoute] int id)
        {
            try
            {
                var producto = await _context.Productos.FindAsync(id);

                if (producto == null)
                {
                    return NotFound("Producto no encontrado");
                }

                return Ok(producto);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\n" + ex.StackTrace);
                return BadRequest("Ha ocurrido un error inesperado");
            }
        }

        // PUT: api/Productos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProducto([FromRoute] int id, [FromBody] Producto producto)
        {
            try
            {
                if (id != producto.Id)
                {
                    return BadRequest();
                }

                var oldProduct = await _context.Productos.FindAsync(producto.Id);

                if (oldProduct == null)
                {
                    return NotFound("Producto no encontrado");
                }

                oldProduct.Nombre = producto.Nombre;
                oldProduct.Descripcion = producto.Descripcion;
                oldProduct.Precio = producto.Precio;
                oldProduct.Stock = producto.Stock;

                _context.Entry(oldProduct).State = EntityState.Modified;

                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\n" + ex.StackTrace);
                return BadRequest("Ha ocurrido un error inesperado");
            }
        }

        // POST: api/Productos
        [HttpPost]
        public async Task<ActionResult<Producto>> PostProducto([FromBody] Producto producto)
        {
            try
            {
                Console.WriteLine(producto);
                _context.Productos.Add(producto);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetProducto", new { id = producto.Id }, producto);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\n" + ex.StackTrace);
                return BadRequest("Ha ocurrido un error inesperado");
            }
        }

        // DELETE: api/Productos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProducto([FromRoute] int id)
        {
            try
            {
                var producto = await _context.Productos.FindAsync(id);
                if (producto == null)
                {
                    return NotFound("Producto no encontrado");
                }

                _context.Productos.Remove(producto);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\n" + ex.StackTrace);
                return BadRequest("Ha ocurrido un error inesperado");
            }
        }

    }
}
