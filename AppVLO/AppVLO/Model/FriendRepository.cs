using AppVLO.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace AppVLO.Model
{
    public class FriendRepository
    {
        public IList<Detalle> Detalle { get; set; }

        public FriendRepository()
        {
            //Hydrator<Friend> _friendHydrator
            //    = new Hydrator<Friend>();
            //Friends = _friendHydrator.GetList(50);
            Task.Run(async () =>
                Detalle = await App.Database.GetItemsAsync()).Wait();
        }

        public IList<Detalle> GetAll()
        {
            return Detalle;
        }

        public IList<Detalle> ObtenerPersonalizado(int idMesa)
        {
            var query = from q in Detalle
                        where q.IdMesa == idMesa && q.Estado == 1
                        select q;

            return query.ToList();
        }

        public IList<Detalle> BorrarPersonalizado(int idMesa)
        {
            var query = from q in Detalle
                        where q.IdMesa == idMesa && q.Estado == 1
                        select q;

            return query.ToList();
        }
    }
}
