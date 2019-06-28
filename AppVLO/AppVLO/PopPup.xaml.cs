using AppVLO.Model;
using Foundation;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppVLO
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopPup : PopupPage
    {
        NSTimer alertDelay;
        UIAlertController alert;
        public Model.Detalle De;
        public PopPup()
        {
            InitializeComponent();
        }
        public PopPup(Model.Detalle DetalleRecibido):this()
        {
            De = DetalleRecibido;
        }

        private void Cancel_Clicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();
        }

        private async void Guardar_Clicked(object sender, EventArgs e)
        {
            string coment;
            if(Comentario.Text == null)
            {
                coment = "";
            }
            else { coment = Comentario.Text; }

            var pedido = new Model.Detalle
            {
                IdMenu = ((Model.Menu)BindingContext).IdMenu,
                IdMesa = De.IdMesa,
                Menu = ((Model.Menu)BindingContext).Nombre,
                PrecioUnitario = Convert.ToString(((Model.Menu)BindingContext).Precio),
                IdPedido = De.IdPedido,
                cantidad = Convert.ToInt32(Cantidad.Text),
                Termino = Convert.ToString(Termino.SelectedItem),
                Comentarios = coment,
                Estado = 1
            };

            await App.Database.SaveItemAsync(pedido);

            //var jsonRequest = JsonConvert.SerializeObject(pedido);
            //var content = new StringContent(jsonRequest, Encoding.UTF8, "text/json");

            //string result;
            ////string resultMesas;
            //try
            //{

            //    HttpClient client = new HttpClient
            //    {
            //        BaseAddress = new Uri(VarGlobal.Link)
            //    };
            //    string url = string.Format("api/DetallePedidoes");
            //    var response = await client.PostAsync(url, content);
            //    result = response.Content.ReadAsStringAsync().Result;



            //}
            //catch (Exception)
            //{
            //    await DisplayAlert("Error", $"Problemas de conexión", "Ok");
            //    return;
            //}
            string message = "Datos Guardados";
            if (Device.RuntimePlatform == Device.Android)
            {
                
                Android.Widget.Toast.MakeText(Android.App.Application.Context,message, Android.Widget.ToastLength.Short).Show();
            }
            else if (Device.RuntimePlatform == Device.iOS)
            {
                ShowAlert(message, 2.0);
            }
            await PopupNavigation.Instance.PopAsync();
        }
        void ShowAlert(string message, double seconds)
        {
            alertDelay = NSTimer.CreateScheduledTimer(seconds, (obj) =>
            {
                dismissMessage();
            });
            alert = UIAlertController.Create(null, message, UIAlertControllerStyle.Alert);
            UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController(alert, true, null);
        }

        void dismissMessage()
        {
            if (alert != null)
            {
                alert.DismissViewController(true, null);
            }
            if (alertDelay != null)
            {
                alertDelay.Dispose();
            }
        }
    }
}