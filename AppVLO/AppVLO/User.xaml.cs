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
	public partial class User : ContentPage
	{
        public User()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            NavigationPage.SetHasBackButton(this, false);

        }

        //protected override bool OnBackButtonPressed()
        //{
        //    base.OnBackButtonPressed();
        //    return true;
        //}

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if(VarGlobal.Global == string.Empty)
            {
               await Navigation.PopAsync();
            }
            string result;
            try
            {
                HttpClient client = new HttpClient
                {
                    BaseAddress = new Uri(VarGlobal.Link)
                };
                string url = string.Format("api/Usuarios/{0}", VarGlobal.Global);
                var response = await client.GetAsync(url);
                result = response.Content.ReadAsStringAsync().Result;

            }
            catch (Exception)
            {
                await DisplayAlert("Error", $"Problemas de conexión", "Ok");
                return;
            }
            
            var Usuarios = JsonConvert.DeserializeObject<Usuarios>(result);
            lblNombre.Text = Usuarios.Nombre;
            lblUser.Text = Usuarios.Username;
            
            string result2;
            try
            {
                HttpClient client = new HttpClient
                {
                    BaseAddress = new Uri(VarGlobal.Link)
                };
                string url = string.Format("api/Roles/{0}", Usuarios.IdRol.ToString());
                var response = await client.GetAsync(url);
                result2 = response.Content.ReadAsStringAsync().Result;

            }
            catch (Exception)
            {
                await DisplayAlert("Error", $"Problemas de conexión", "Ok");
                return;
            }

            var Roles = JsonConvert.DeserializeObject<Roles>(result2);
            lblRol.Text = Roles.Tipo;
        }

        private void Salir_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new MainPage());
        }
    }
}