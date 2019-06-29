using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AppVLO.Model;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppVLO
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CargarPedido : ContentPage
	{
        //public List<Detalle> detalle { get; set; }

        public MesasD pedido;
        public CargarPedido ()
		{
			InitializeComponent ();
            Device.StartTimer(TimeSpan.FromMilliseconds(500), () =>
            {
                Cargar();
                return true;
            });

        }

        private async void Cargar()
        {
            var obtener = await App.Database.ObtenerPersonalizado(pedido.IdMesa);
            listaDetalle.ItemsSource = obtener;
        }

        public CargarPedido(MesasD pe) : this()
        {
            pedido = pe;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            var obtener = await App.Database.ObtenerPersonalizado(pedido.IdMesa);
            listaDetalle.ItemsSource = obtener;
        }

        private void ListaDetalle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item is Model.Detalle _Data)
            {
                PopupNavigation.Instance.PushAsync(new PopupDetalle {BindingContext = _Data });
                listaDetalle.SelectedItem = null;
            }
        }

        private async void Enviar_Clicked(object sender, EventArgs e)
        {
            var obtener = await App.Database.ObtenerPersonalizado(pedido.IdMesa);
            listaDetalle.ItemsSource = obtener;
            foreach (var p in obtener)
            {
                var pedido = new Model.DetallePedido
                {
                    IdMenu = p.IdMenu,
                    IdPedido = p.IdPedido,
                    cantidad = p.cantidad,
                    Termino = p.Termino,
                    Comentarios = p.Comentarios,
                    Estado = p.Estado
                };
                var jsonRequest = JsonConvert.SerializeObject(pedido);
                var content = new StringContent(jsonRequest, Encoding.UTF8, "text/json");

                string result;
                //string resultMesas;
                try
                {

                    HttpClient client = new HttpClient
                    {
                        BaseAddress = new Uri(VarGlobal.Link)
                    };
                    string url = string.Format("api/DetallePedidoes");
                    var response = await client.PostAsync(url, content);
                    result = response.Content.ReadAsStringAsync().Result;


                    
                }
                catch (Exception)
                {
                    await DisplayAlert("Error", $"Problemas de conexión", "Ok");
                    return;
                }

                await App.Database.DeleteItemAsync(p);
            }
            await Navigation.PopAsync();
        }

        private async void ListaDetalle_Refreshing(object sender, EventArgs e)
        {
            var obtener = await App.Database.ObtenerPersonalizado(pedido.IdMesa);
            listaDetalle.ItemsSource = obtener;
            Device.StartTimer(TimeSpan.FromSeconds(2), () =>
            {
                listaDetalle.IsRefreshing = false;
                return false;
            });
        }
    }
}