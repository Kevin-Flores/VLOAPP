using AppVLO.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Linq;

namespace AppVLO
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Menus : ContentPage
	{

		public Menus ()
		{
			InitializeComponent ();
        }

        Pedido PedidoDescargado { get; set; }
        string IDPedido;

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            PedidoDescargado = new Pedido();
            string Orden = string.Format("Orden {0}",((MesasD)BindingContext).NumMesa);
            lblOrdenes.Text = Orden;
            string result;
            try
            {
                HttpClient client = new HttpClient
                {
                    BaseAddress = new Uri(VarGlobal.Link)
                };
                string url = string.Format("api/TipoMenus");
                var response = await client.GetAsync(url);
                result = response.Content.ReadAsStringAsync().Result;

            }
            catch (Exception ea)
            {
                var oi = ea.Message;
                await DisplayAlert("Error", $"Problemas de conexión \n {oi}", "Ok");
                return;
            }

            List<Model.TipoMenu> TipoMenu = JsonConvert.DeserializeObject<List<Model.TipoMenu>>(result);

            listMesas.ItemsSource = TipoMenu;

            if (TipoMenu.Count > 0)
            {
                string Ocupada;
                try
                {
                    HttpClient client = new HttpClient
                    {
                        BaseAddress = new Uri(VarGlobal.Link)
                    };
                    string url = string.Format("api/Pedidoes");
                    var response = await client.GetAsync(url);
                    Ocupada = response.Content.ReadAsStringAsync().Result;

                }
                catch (Exception)
                {
                    await DisplayAlert("Error", $"Problemas de conexión", "Ok");
                    return;
                }

                IEnumerable<Model.Pedido> VarPedido = new List<Model.Pedido>();
                VarPedido = JsonConvert.DeserializeObject<List<Model.Pedido>>(Ocupada);


                if (VarPedido.Count<Pedido>() > 0)
                {
                    VarPedido = from p in VarPedido
                                         where p.IdMesa == ((MesasD)BindingContext).IdMesa && p.Estado == 1
                                         select p;

                    PedidoDescargado = VarPedido.FirstOrDefault();
                    if (PedidoDescargado != null)
                    {
                        txtCliente.Text = PedidoDescargado.Cliente.ToString();
                        txtPersonas.Text = PedidoDescargado.Cantidad.ToString();
                        IDPedido = PedidoDescargado.IdPedido.ToString();
                    }
                    if (PedidoDescargado != null)
                    {
                        OcuparMesa.IsEnabled = false;
                    }
                }

            }
            //foreach (var M in Pedido)
            //{
            //    if (M.Estado == 1 && ((MesasD)BindingContext).Estado == "Red")
            //    {

            //    }
            //}
            string obtener;
            try
            {
                HttpClient client = new HttpClient
                {
                    BaseAddress = new Uri(VarGlobal.Link)
                };
                string url = string.Format("api/DetallePedidoes");
                var response = await client.GetAsync(url);
                obtener = response.Content.ReadAsStringAsync().Result;

            }
            catch (Exception ea)
            {
                var oi = ea.Message;
                await DisplayAlert("Error", $"Problemas de conexión \n {oi}", "Ok");
                return;
            }

            Model.ListaBebida ListaBebida = JsonConvert.DeserializeObject<Model.ListaBebida>(obtener);
            var resultado = ListaBebida;
            var pedido = resultado.pedidos;
            var menus = resultado.menus;
            var detalle = resultado.detalle;
            var tipomenu = resultado.tipomenu;

            var query = (from deta in detalle where deta.Estado == 1 || deta.Estado == 2
                         join pedi in pedido
                              on deta.IdPedido equals pedi.IdPedido
                         where (pedi.IdUser == Convert.ToInt32(VarGlobal.Global) && pedi.IdMesa == ((MesasD)BindingContext).IdMesa)
                         join men in menus
                              on deta.IdMenu equals men.IdMenu
                         select new
                         {
                             deta.cantidad,
                             men.Precio
                         }).ToList();
            double total = 0;

            foreach (var sum in query)
            {
                total = total + (sum.cantidad * sum.Precio);
            }

            TotalPagar.Text =string.Format("$ {0}", Convert.ToString(total * 1.06));
        }

        private async void OcuparMesa_Clicked(object sender, EventArgs e)
        {
            
                if (string.IsNullOrEmpty(txtCliente.Text) || string.IsNullOrEmpty(txtPersonas.Text))
                {
                    await DisplayAlert("Mensaje", "Debe proporcionar nombre de cliente y Numero de comensales", "ok");
                    return;
                }
            
            var pedido = new Pedido
            {
                Cantidad = Convert.ToInt32(txtPersonas.Text),
                Cliente = txtCliente.Text,
                Estado = 1,
                IdMesa = ((MesasD)BindingContext).IdMesa,
                IdUser = Convert.ToInt32(VarGlobal.Global)
            };

            var Vmesa = new MesasD
            {
                IdMesa = ((MesasD)BindingContext).IdMesa,
                NumMesa_BF = ((MesasD)BindingContext).NumMesa_BF,
                Estado = false
            };


            var jsonRequest = JsonConvert.SerializeObject(pedido);
            var content = new StringContent(jsonRequest, Encoding.UTF8, "text/json");

            var jsonRequestMesa = JsonConvert.SerializeObject(Vmesa);
            var contentMesa = new StringContent(jsonRequestMesa, Encoding.UTF8, "text/json");

            string result;
            //string resultMesas;
            try
            {
                /*

                HttpClient clientMesas = new HttpClient
                {
                    BaseAddress = new Uri(VarGlobal.Link)
                };
                
                string urlMesas = string.Format("api/Mesas/");
                var responseMesas = await clientMesas.PutAsync(urlMesas, contentMesa);
                resultMesas = responseMesas.Content.ReadAsStringAsync().Result;
                */

                HttpClient client = new HttpClient
                {
                    BaseAddress = new Uri(VarGlobal.Link)
                };
                string url = string.Format("api/Pedidoes");
                var response = await client.PostAsync(url, content);
                result = response.Content.ReadAsStringAsync().Result;
                OcuparMesa.IsEnabled = false;



            }
            catch (Exception)
            {
                await DisplayAlert("Error", $"Problemas de conexión", "Ok");
                return;
            }
        }

        private async void Cancelar_Clicked(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txtCliente.Text) || string.IsNullOrEmpty(txtPersonas.Text))
            {
                await Navigation.PopAsync();
                return;
            }

            var Vmesa = new MesasD
            {
                IdMesa = ((MesasD)BindingContext).IdMesa,
                NumMesa_BF = ((MesasD)BindingContext).NumMesa_BF,
                Estado = true
            };

            var jsonRequestMesa = JsonConvert.SerializeObject(Vmesa);
            var contentMesa = new StringContent(jsonRequestMesa, Encoding.UTF8, "text/json");
            
            //string resultMesas;
            //string resultPedido;
            try
            {
                /*

                HttpClient clientMesas = new HttpClient
                {
                    BaseAddress = new Uri(VarGlobal.Link)
                };

                string urlMesas = string.Format("api/Mesas/");
                var responseMesas = await clientMesas.PutAsync(urlMesas, contentMesa);
                resultMesas = responseMesas.Content.ReadAsStringAsync().Result;
                */

                HttpClient clientPedido = new HttpClient
                {
                    BaseAddress = new Uri(VarGlobal.Link)
                };

                string urlPedido = string.Format($"api/Pedidoes/{IDPedido}");
                var responsePedido = await clientPedido.DeleteAsync(urlPedido);
                //resultPedido = responseMesas.Content.ReadAsStringAsync().Result;
                if(!responsePedido.IsSuccessStatusCode)
                {
                    await DisplayAlert("Aviso", "No puede cancelar", "OK");
                    Cancelar.IsEnabled = false;
                    return;
                }

            }
            catch (Exception)
            {
                await DisplayAlert("Error", $"Problemas de conexión", "Ok");
                return;
            }
            await Navigation.PopAsync();
        }

        private void VerDetalle_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CargarPedido((MesasD)BindingContext));
        }

        private async void ListMesas_ItemTapped(object sender, ItemTappedEventArgs e)
        {

            listMesas.SelectedItem = null;
            string Ocupada;
            try
            {
                HttpClient client = new HttpClient
                {
                    BaseAddress = new Uri(VarGlobal.Link)
                };
                string url = string.Format("api/Pedidoes");
                var response = await client.GetAsync(url);
                Ocupada = response.Content.ReadAsStringAsync().Result;

            }
            catch (Exception)
            {
                await DisplayAlert("Error", $"Problemas de conexión", "Ok");
                return;
            }

            IEnumerable<Model.Pedido> VarPedido = new List<Model.Pedido>();
            VarPedido = JsonConvert.DeserializeObject<List<Model.Pedido>>(Ocupada);

            Pedido PedidoDes = new Pedido();
            if (VarPedido.Count<Pedido>() > 0)
            {
                VarPedido = from p in VarPedido
                            where p.IdMesa == ((MesasD)BindingContext).IdMesa && p.Estado == 1
                            select p;

                PedidoDes = VarPedido.FirstOrDefault();
            }

            if (e.Item is TipoMenu _Data)
            {
                
                OpcionMenu Pagina = new OpcionMenu(PedidoDes)
                {
                    BindingContext = _Data
                };
                await Navigation.PushAsync(Pagina);
                // await Navigation.PushAsync(new OpcionMenu { BindingContext = _Data });
            }
        }

        //private void ListMesas_ItemTapped(object sender, ItemTappedEventArgs e)
        //{
        //    //TipoMenu MenuSelecionado = new TipoMenu();


        //    var item = (TipoMenu)e.Item;
        //    ((NavigationPage)this.Parent).PushAsync(new Views.ProductosView(item));

        //}

    }
}