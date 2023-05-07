using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inmobiliaria.Entities
{
    public class Respuesta<T>
    {
        public bool Completa { get; set; }
        public string Mensaje { get; set; }
        public T? Datos { get; set; }
    }
}
