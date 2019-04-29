using AppVLO.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
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
            string orden = string.Format("Orden {0}",((MesasD)BindingContext).NumMesa);
            lblOrdenes.Text = orden;
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
                await Navigation.PushAsync(new OpcionMenu { BindingContext = _Data });
            }
        }
    }
}