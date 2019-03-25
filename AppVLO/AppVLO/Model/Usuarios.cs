using System;
using System.Collections.Generic;
using System.Text;

namespace AppVLO.Model
{
    public class Usuarios
    {
        public int IdUser { get; set; }

        public string Nombre { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }
        
        public int IdRol { get; set; }

        public virtual Roles Roles { get; set; }
    }
}
