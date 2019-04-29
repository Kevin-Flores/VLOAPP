using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AppVLO.Model;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppVLO
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class OpcionMenu : ContentPage
	{
		public OpcionMenu ()
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
    }
}