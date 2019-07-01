using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using AppVLO.Model;
using Xamarin.Forms.PlatformConfiguration;
using System.Threading;
//using Android.Views;

namespace AppVLO
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            NavigationPage.SetHasBackButton(this, false);
            txtUser.Focus();
        }
        
        protected override void OnAppearing()
        {
            base.OnAppearing();
            txtUser.Focus();
        }

        protected override bool OnBackButtonPressed()
        {
            base.OnBackButtonPressed();

            if (Device.RuntimePlatform == Device.Android)
            {
                Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
            }
            else if (Device.RuntimePlatform == Device.iOS)
            {
                Application.Current.Quit();
            }

            //Thread.CurrentThread.Abort();
            return false;
        }

        private void TxtUser_Completed(object sender, EventArgs e)
        {
            if ((sender as Entry).ReturnType == ReturnType.Go)
            {
                txtPassword.Focus();
            }
        }

        private void Ver_Clicked(object sender, EventArgs e)
        {
            if(txtPassword.IsPassword == true)
            {
                txtPassword.IsPassword = false;
                FileImageSource ruta = "nover.png";
                ver.Image = ruta;
            }else
            {
                txtPassword.IsPassword = true;
                FileImageSource ruta = "ver.png";
                ver.Image = ruta;
            }
        }

        private async void BtnIngresar_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUser.Text))
            {
                await DisplayAlert("Aviso", "Debe ingresar un usuario", "Ok");
                txtUser.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                await DisplayAlert("Aviso", "Debe ingresar una contraseña", "Ok");
                txtPassword.Focus();
                return;
            }
            waitActivityIndicator.IsRunning = true;
            string result;
            try
            {
                btnIngresar.IsEnabled = false;
                HttpClient client = new HttpClient
                {
                    BaseAddress = new Uri(VarGlobal.Link)
                };
                string url = string.Format("api/Usuarios/{0}/{1}", txtUser.Text, txtPassword.Text);
                var response = await client.GetAsync(url);
                result = response.Content.ReadAsStringAsync().Result;
                btnIngresar.IsEnabled = true;

            }
            catch (Exception)
            {
                await DisplayAlert("Error", $"Problemas de conexión", "Ok");
                btnIngresar.IsEnabled = true;
                waitActivityIndicator.IsRunning = false;
                return;
            }
            waitActivityIndicator.IsRunning = false;

            if (string.IsNullOrEmpty(result) || result == "null")
            {
                await DisplayAlert("Aviso", "Usuario o Contraseña Incorrectos", "Ok");
                txtUser.Text = string.Empty;
                txtPassword.Text = string.Empty;
                txtUser.Focus();
                return;
            }
            var Usuarios = JsonConvert.DeserializeObject<Usuarios>(result);
            VarGlobal.Global = Usuarios.IdUser.ToString();
            if(Usuarios.IdRol == 3)
            {
                var NavPag = new NavigationPage(new Inicio()) { };
                NavPag.BarBackgroundColor = Color.FromHex("#0B5894");
                await Navigation.PushModalAsync(NavPag);
            }
            else
            {
                var NavPag = new NavigationPage(new User()) { };
                NavPag.BarBackgroundColor = Color.FromHex("#0B5894");
                await Navigation.PushModalAsync(NavPag);
            }
        }
    }
}
