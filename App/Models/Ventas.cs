using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Models
{
    public class Ventas
    {
        public required string Producto { get; set; }
        public DateTime FechaVenta { get; set; }
        public Double Precio { get; set; }
    }
}
