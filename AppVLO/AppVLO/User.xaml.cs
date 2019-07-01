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

            string result3;
            try
            {
                HttpClient client = new HttpClient
                {
                    BaseAddress = new Uri(VarGlobal.Link)
                };
                string url = string.Format("api/Turnos/{0}",VarGlobal.Global);
                var response = await client.GetAsync(url);
                result3 = response.Content.ReadAsStringAsync().Result;

            }
            catch (Exception)
            {
                await DisplayAlert("Error", $"Problemas de conexión", "Ok");
                return;
            }

            List<Turnos> turnos = JsonConvert.DeserializeObject<List<Turnos>>(result3);

            int cont = 0;
            foreach (var turn in turnos)
            {
                cont++;
                if(cont == 1)
                {
                    DateTime dia = Convert.ToDateTime(turn.Fecha);
                    DateTime turno1 = Convert.ToDateTime(turn.HoraInicial);
                    DateTime turno2 = Convert.ToDateTime(turn.HoraFinal);
                    Turno1.Text = $"Dia: {dia.ToString("dddd")} {dia.ToString("dd")} de {dia.ToString("MMMM")} de {dia.ToString("yyyy")}\n" +
                              $"Turno: {turn.Nombre}\n" +
                              $"De  {turno1.ToString("hh:mm tt")}  A  {turno2.ToString("hh:mm tt")}";
                }
                if (cont == 2)
                {
                    DateTime dia = Convert.ToDateTime(turn.Fecha);
                    DateTime turno1 = Convert.ToDateTime(turn.HoraInicial);
                    DateTime turno2 = Convert.ToDateTime(turn.HoraFinal);
                    Turno2.Text = $"Dia: {dia.ToString("dddd")} {dia.ToString("dd")} de {dia.ToString("MMMM")} de {dia.ToString("yyyy")}\n" +
                              $"Turno: {turn.Nombre}\n" +
                              $"De  {turno1.ToString("hh:mm tt")}  A  {turno2.ToString("hh:mm tt")}";
                }
                if (cont == 3)
                {
                    DateTime dia = Convert.ToDateTime(turn.Fecha);
                    DateTime turno1 = Convert.ToDateTime(turn.HoraInicial);
                    DateTime turno2 = Convert.ToDateTime(turn.HoraFinal);
                    Turno3.Text = $"Dia: {dia.ToString("dddd")} {dia.ToString("dd")} de {dia.ToString("MMMM")} de {dia.ToString("yyyy")}\n" +
                              $"Turno: {turn.Nombre}\n" +
                              $"De  {turno1.ToString("hh:mm tt")}  A  {turno2.ToString("hh:mm tt")}";
                }
                if (cont == 4)
                {
                    DateTime dia = Convert.ToDateTime(turn.Fecha);
                    DateTime turno1 = Convert.ToDateTime(turn.HoraInicial);
                    DateTime turno2 = Convert.ToDateTime(turn.HoraFinal);
                    Turno4.Text = $"Dia: {dia.ToString("dddd")} {dia.ToString("dd")} de {dia.ToString("MMMM")} de {dia.ToString("yyyy")}\n" +
                              $"Turno: {turn.Nombre}\n" +
                              $"De  {turno1.ToString("hh:mm tt")}  A  {turno2.ToString("hh:mm tt")}";
                }
                if (cont == 5)
                {
                    DateTime dia = Convert.ToDateTime(turn.Fecha);
                    DateTime turno1 = Convert.ToDateTime(turn.HoraInicial);
                    DateTime turno2 = Convert.ToDateTime(turn.HoraFinal);
                    Turno5.Text = $"Dia: {dia.ToString("dddd")} {dia.ToString("dd")} de {dia.ToString("MMMM")} de {dia.ToString("yyyy")}\n" +
                              $"Turno: {turn.Nombre}\n" +
                              $"De  {turno1.ToString("hh:mm tt")}  A  {turno2.ToString("hh:mm tt")}";
                }

            }
        }

        private void Salir_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new MainPage());
        }
    }
}