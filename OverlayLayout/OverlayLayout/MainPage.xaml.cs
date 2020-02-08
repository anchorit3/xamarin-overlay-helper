using OverlayLayout.Pages;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace OverlayLayout
{
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        
        public MainPage()
        {
            InitializeComponent();
        }

        private void StackLayoutExample_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new StackPage());
        }

        private void ScrollViewExample_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ScrollPage());
        }

        private void GridExample_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new GridPage());
        }
    }
}
