using System;
using System.Collections.Generic;
using System.Text;

namespace AppVLO.Model
{
    public class DetallePedido
    {
        public int IdDetalle { get; set; }

        //Relaciones
        //Tabla Menu
        public int IdMenu { get; set; }
        //Tabla Pedido
        public int IdPedido { get; set; }

        public int Cantidad { get; set; }
        public int Sesion { get; set; }
        public int Estado { get; set; }
        public string Termino { get; set; }
    }
}
