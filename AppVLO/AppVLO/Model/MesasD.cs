using System;
using System.Collections.Generic;
using System.Text;

namespace AppVLO.Model
{
    public class MesasD
    {
        public int IdMesa { get; set; }

        public string NumMesa_BF { get; set; }

        public string NumMesa
        {
            get { return $"Mesa {NumMesa_BF}"; }
            set { NumMesa_BF = value; }
        }

        public bool Estado { get; set; }

        public string Estado_
        {
            get
            {
                if (Estado == true)
                {
                    return "Green";
                }
                else
                {
                    return "Red";
                }
            }
            set
            {
                Estado = Convert.ToBoolean(value);
            }
        }

        public string VEstado
        {
            get
            {
                if(Estado_ == "Green")
                {
                    return "Disponible";
                }
                else
                {
                    return "Ocupada";
                }
            }
        }

        //public override string ToString()
        //{
        //    return string.Format("Mesa {0}",NumMesa);
        //}
    }
}
