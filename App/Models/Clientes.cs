using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Models
{
    public class Clientes
    {
        public string ID { get; set; }
        public required string NombreCompleto{ get; set; } 
        public DateTime FechaDeIngreso { get; set; }
        public required TipoCliente TipoCliente { get; set; }
    }
}
