using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_4_Estructura_de_datos_avanzada
{
    public class Contacto
    {
        public string Nombre { get; set; }
        public string InformacionContacto { get; set; }

        public Contacto(string nombre, string informacionContacto)
        {
            Nombre = nombre;
            InformacionContacto = informacionContacto;
        }
    }
}
