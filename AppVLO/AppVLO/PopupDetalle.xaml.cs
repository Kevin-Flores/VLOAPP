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

namespace AppVLO
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PopupDetalle : PopupPage
    {

        
        public PopupDetalle ()
		{
			InitializeComponent ();
        }
    

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Cantidad.Text = Convert.ToString(((Detalle)BindingContext).cantidad);
            Termino.SelectedItem = ((Detalle)BindingContext).Termino;
            Comentario.Text = ((Detalle)BindingContext).Comentarios;
        }

        private void Guardar_Clicked(object sender, EventArgs e)
        {
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