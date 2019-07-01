using System;
using System.Collections.Generic;
using System.Text;

namespace AppVLO.Model
{
    public class Menu
    {

        public int IdMenu { get; set; }

        public string Nombre { get; set; }
        
        public double Precio { get; set; }

        public string PrecioUnitario
        {
            get { return $"Precio: ${Precio}"; }
        }

        public string Descripcion { get; set; }

        //Relación con Tipo de Menu
        public int IdTipoMenu { get; set; }
        
    }
}
