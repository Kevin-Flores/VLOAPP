using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AppVLO.Model;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppVLO
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class OpcionMenu : ContentPage
	{
        Pedido PedidoUsar;
        Model.Detalle DetalleEnviar { get; set; }
		public OpcionMenu ()
		{
			InitializeComponent ();
            DetalleEnviar = new Model.Detalle();
            
        }

        public OpcionMenu(Pedido PedidoRecibido) : this()
        {
            PedidoUsar = PedidoRecibido;
            
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            string result;
            try
            {
                HttpClient client = new HttpClient
                {
                    BaseAddress = new Uri(VarGlobal.Link)
                };
                string url = string.Format("api/Menus/{0}", ((TipoMenu)BindingContext).IdTipoMenu);
                var response = await client.GetAsync(url);
                result = response.Content.ReadAsStringAsync().Result;

            }
            catch (Exception)
            {
                await DisplayAlert("Error", $"Problemas de conexión", "Ok");
                return;
            }

            List<Model.Menu> menu = JsonConvert.DeserializeObject<List<Model.Menu>>(result);

            listOMenus.ItemsSource = menu;
        }

        private void ListOMenus_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item is Model.Menu _Data)
            {

                DetalleEnviar.IdDetalle = 0;
                DetalleEnviar.IdMenu = 0;
                DetalleEnviar.IdPedido = PedidoUsar.IdPedido;
                DetalleEnviar.IdMesa = PedidoUsar.IdMesa;
                DetalleEnviar.Termino = "";
                DetalleEnviar.cantidad = 0;
                DetalleEnviar.Comentarios = "";
                PopupPage PP = new PopPup(DetalleEnviar)
                {
                    BindingContext = _Data,
                    CloseWhenBackgroundIsClicked = true
                };

                PopupNavigation.Instance.PushAsync(PP);
                listOMenus.SelectedItem = null;
            }
        }
    }
}