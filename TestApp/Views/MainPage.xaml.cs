using System;
using SkiaSharp.Views.Forms;
using TestApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MainPage : ContentPage
    {
		



        public MainPage()
        {
			InitializeComponent();
	        BindingContext = new MainPageViewModel();
			Title = "Xamarin Testapp";

			btnGoToDraw.Clicked += BtnGoToDrawOnClicked;
            
        }

	    private void BtnGoToDrawOnClicked(object sender, EventArgs e) {
		    Navigation.PushAsync(new DrawPage());
	    }
    }
}