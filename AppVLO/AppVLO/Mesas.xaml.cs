using AppVLO.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppVLO
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Mesas : ContentPage
	{
		public Mesas ()
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
                string url = string.Format("api/Mesas");
                var response = await client.GetAsync(url);
                result = response.Content.ReadAsStringAsync().Result;

            }
            catch (Exception)
            {
                await DisplayAlert("Error", $"Problemas de conexión", "Ok");
                return;
            }

            List<MesasD> mesas = JsonConvert.DeserializeObject<List<MesasD>>(result);
            
            listMesas.ItemsSource = mesas;

        }

        private async void ListMesas_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            listMesas.SelectedItem = null;

            if (e.Item is MesasD _Data)
            {
                await Navigation.PushAsync(new Menus { BindingContext = _Data });
            }
        }
    }
}