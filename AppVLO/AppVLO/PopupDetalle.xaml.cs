using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AppVLO.Model;
using System.Net.Http;
using Newtonsoft.Json;

namespace AppVLO
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopupDetalle : PopupPage
    {


        public PopupDetalle()
        {
            InitializeComponent();
        }


        protected async override void OnAppearing()
        {
            base.OnAppearing();
            Tema.Text = ((Detalle)BindingContext).Menu;
            Cantidad.Text = Convert.ToString(((Detalle)BindingContext).cantidad);
            string result;
            try
            {
                HttpClient client = new HttpClient
                {
                    BaseAddress = new Uri(VarGlobal.Link)
                };
                string url = string.Format("api/TipoMenus/{0}", ((Detalle)BindingContext).IdMenu);
                var response = await client.GetAsync(url);
                result = response.Content.ReadAsStringAsync().Result;

            }
            catch (Exception)
            {
                await DisplayAlert("Error", $"Problemas de conexión", "Ok");
                return;
            }

            TipoMenu bebida = JsonConvert.DeserializeObject<TipoMenu>(result);

            if (bebida.Nombre == "Bebidas" || bebida.Nombre == "Postres" || bebida.Nombre == "Sopas")
            {
                Termino.IsEnabled = false;
                Termino.IsVisible = false;
                lblCanti.IsVisible = false;
            }
            else
            {
                Termino.SelectedItem = ((Detalle)BindingContext).Termino;
            }
            Comentario.Text = ((Detalle)BindingContext).Comentarios;
        }

        private void Guardar_Clicked(object sender, EventArgs e)
        {
            if (Cantidad.Text == "" || Cantidad.Text == "0" || Cantidad.Text == null)
            {
                DisplayAlert("Aviso","Debe ingresar una cantidad valida!","OK");
                return;
            }
            Detalle det = new Detalle();
            det = ((Detalle)BindingContext);
            det.cantidad = Convert.ToInt32(Cantidad.Text);
            det.Termino = Convert.ToString(Termino.SelectedItem);
            det.Comentarios = Comentario.Text;

            App.Database.SaveItemAsync(det);
            PopupNavigation.Instance.PopAsync();
        }

        private void Borrar_Clicked(object sender, EventArgs e)
        {
            Detalle det = new Detalle();
            det = ((Detalle)BindingContext);
            App.Database.DeleteItemAsync(det);
            PopupNavigation.Instance.PopAsync();
        }

        private void Cancel_Clicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();
        }
    }
}