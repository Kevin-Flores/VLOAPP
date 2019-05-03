﻿using AppVLO.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppVLO
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Menus : ContentPage
	{
		public Menus ()
		{
			InitializeComponent ();
		}

        protected async override void OnAppearing()
        {
            base.OnAppearing();
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
            catch (Exception)
            {
                await DisplayAlert("Error", $"Problemas de conexión", "Ok");
                return;
            }

            List<Model.TipoMenu> TipoMenu = JsonConvert.DeserializeObject<List<Model.TipoMenu>>(result);

            listMesas.ItemsSource = TipoMenu;
        }

        private async void ListMesas_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem is TipoMenu _Data)
            {
                var pedido = new Pedido
                {
                    Cantidad = Convert.ToInt32(txtPersonas.Text),
                    Cliente = txtCliente.Text,
                    Estado = 1,
                    IdMesa = ((MesasD)BindingContext).IdMesa,
                    IdUser = Convert.ToInt32(VarGlobal.Global)
                };

                var jsonRequest = JsonConvert.SerializeObject(pedido);
                var content = new StringContent(jsonRequest, Encoding.UTF8, "text/json");

                string result;
                string resultMesas;
                try
                {
                    /**/

                    HttpClient clientMesas = new HttpClient
                    {
                        BaseAddress = new Uri(VarGlobal.Link)
                    };
                    string urlMesas = string.Format("api/Mesas");
                    var responseMesas = await clientMesas.GetAsync(urlMesas);
                    resultMesas = responseMesas.Content.ReadAsStringAsync().Result;
                    /**/

                    HttpClient client = new HttpClient
                    {
                        BaseAddress = new Uri(VarGlobal.Link)
                    };
                    string url = string.Format("api/Pedidoes");
                    var response = await client.PostAsync(url,content);
                    result = response.Content.ReadAsStringAsync().Result;

                }
                catch (Exception)
                {
                    await DisplayAlert("Error", $"Problemas de conexión", "Ok");
                    return;
                }
                await Navigation.PushAsync(new OpcionMenu { BindingContext = _Data });
            }
        }

        private void OcuparMesa_Clicked(object sender, EventArgs e)
        {

        }

        private void Cancelar_Clicked(object sender, EventArgs e)
        {

        }

        private void VerDetalle_Clicked(object sender, EventArgs e)
        {

        }
    }
}