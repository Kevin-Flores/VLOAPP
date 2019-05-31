using System;
using System.Collections.Generic;
using System.Text;

namespace AppVLO.Model
{
    public class MesasD
    {
        public int IdMesa { get; set; }

        private string NumMesa_BF;

        public string NumMesa
        {
            get { return $"Mesa {NumMesa_BF}"; }
            set { NumMesa_BF = value; }
        }

        public bool Estado_ { get; set; }

        public string Estado
        {
            get
            {
                if (Estado_ == true)
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
                Estado_ = Convert.ToBoolean(value);
            }
        }

        public string VEstado
        {
            get
            {
                if(Estado == "Green")
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
