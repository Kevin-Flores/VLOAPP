using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppVLO.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppVLO
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Inicio : TabbedPage
	{
        public Inicio()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            NavigationPage.SetHasBackButton(this, false);
            
        }

        protected override bool OnBackButtonPressed()
        {
            base.OnBackButtonPressed();
            return true;
        }
    }
}