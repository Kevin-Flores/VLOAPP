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

        public int cantidad { get; set; }
        public int Estado { get; set; }
        public string Termino { get; set; }
        public string Comentarios { get; set; }

        public string Canti
        {
            get { return $"Cantidad: {cantidad}"; }
        }
        public string Orden
        {
            get
            {
                if (Estado == 1)
                {
                    return "En Cocina";
                }
                else if(Estado == 2)
                {
                    return "Terminado";
                }
                else
                {
                    return "nada";
                }
            }
        }
    }
}
