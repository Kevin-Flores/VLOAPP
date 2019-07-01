using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AppVLO.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppVLO
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Bebidas : ContentPage
	{
		public Bebidas ()
		{
			InitializeComponent ();
            Device.StartTimer(TimeSpan.FromMilliseconds(500), () =>
            {
                Cargar();
                return true;
            });
        }
        async void Cargar()
        {
            string result;
            try
            {
                HttpClient client = new HttpClient
                {
                    BaseAddress = new Uri(VarGlobal.Link)
                };
                string url = string.Format("api/DetallePedidoes");
                var response = await client.GetAsync(url);
                result = response.Content.ReadAsStringAsync().Result;

            }
            catch (Exception ea)
            {
                var oi = ea.Message;
                await DisplayAlert("Error", $"Problemas de conexión \n {oi}", "Ok");
                return;
            }

            Model.ListaBebida ListaBebida = JsonConvert.DeserializeObject<Model.ListaBebida>(result);
            var resultado = ListaBebida;
            var pedido = resultado.pedidos;
            var menus = resultado.menus;
            var detalle = resultado.detalle;
            var tipomenu = resultado.tipomenu;

            var query = (from deta in detalle
                         where deta.Estado == 1
                         join pedi in pedido
                              on deta.IdPedido equals pedi.IdPedido
                         where pedi.IdUser == Convert.ToInt32(VarGlobal.Global)
                         join men in menus
                              on deta.IdMenu equals men.IdMenu
                         join tmenu in tipomenu
                                 on men.IdTipoMenu equals tmenu.IdTipoMenu
                         where tmenu.Nombre == "Bebidas"
                         select new
                         {
                             men.Nombre,
                             men.Precio,
                             men.PrecioUnitario,
                             men.Descripcion,
                             deta.Canti,
                             deta.IdDetalle,
                             deta.IdMenu,
                             deta.IdPedido,
                             deta.cantidad,
                             deta.Estado,
                             deta.Termino,
                             deta.Comentarios
                         }).ToList();

            listMesas.ItemsSource = query;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            Cargar();
        }

        private async void ListMesas_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            listMesas.SelectedItem = null;

            var opcion = await DisplayAlert("Aviso", "¿Terminar Pedido?","SI","NO");

            if (opcion == true)
            {
               

                var detalle1 = JsonConvert.SerializeObject(e.Item);
                var detalle = JsonConvert.DeserializeObject<mandar>(detalle1);

                string result;
                try
                {

                    HttpClient client = new HttpClient
                    {
                        BaseAddress = new Uri(VarGlobal.Link)
                    };
                    string url = string.Format("api/DetallePedidoes/{0}", detalle.IdDetalle);
                    var response = await client.DeleteAsync(url);
                    result = response.Content.ReadAsStringAsync().Result;
                }
                catch (Exception)
                {
                    await DisplayAlert("Error", $"Problemas de conexión", "Ok");
                    return;
                }
            }
        }
    }
    public class mandar
    {
        public string Nombre { get; set; }
        public double Precio { get; set; }
        public int IdDetalle { get; set; }
        public int IdMenu { get; set; }
        public int IdPedido { get; set; }
        public int cantidad { get; set; }
        public int Estado { get; set; }
        public string Termino { get; set; }
        public string Comentarios { get; set; }
    }
}