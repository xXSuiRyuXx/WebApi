using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    public class Producto
    {
        public int Id { get; set; }

        [Column(TypeName = "varchar(100)")]
        public required string Nombre { get; set; }

        public string? Descripcion { get; set; }

        public double Precio { get; set; }

        public double Stock { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime Creacion { get; set; } = DateTime.Now;

        [DefaultValue(1)]
        public int Estado { get; set; } = 1;

    }
}
