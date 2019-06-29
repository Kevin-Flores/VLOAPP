﻿using Newtonsoft.Json;
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
                        join pedi in pedido
                             on deta.IdPedido equals pedi.IdPedido where pedi.IdUser == Convert.ToInt32(VarGlobal.Global)
                        join men in menus
                             on deta.IdMenu equals men.IdMenu
                        join tmenu in tipomenu
                                on men.IdTipoMenu equals tmenu.IdTipoMenu where tmenu.Nombre == "Bebidas"
                        select new
                        {
                            men.Nombre,
                            men.Precio,
                            deta.IdDetalle,
                            deta.IdMenu,
                            deta.IdPedido,
                            deta.cantidad,
                            deta.Estado,
                            deta.Termino,
                            deta.Comentarios
                        }).ToList();

            listMesas.ItemsSource = query;
            //detalle.Join(menus.Join(tipomenu.))


            //listMesas.ItemsSource 
        }

        private void ListMesas_ItemTapped(object sender, ItemTappedEventArgs e)
        {

        }
    }
}