using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Models
{
    public class Empleado
    {
        public string ID { get; set; } // Este será el Key de Firebase
        public required string NombreCompleto { get; set; }
        public DateTime FechaInicio { get; set; }
        public double Salario { get; set; }
        public required Cargo Cargo { get; set; }
    }

}
