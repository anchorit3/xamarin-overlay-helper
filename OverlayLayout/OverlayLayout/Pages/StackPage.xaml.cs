using System;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using OverlayLayout.Popups;

namespace OverlayLayout.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StackPage : ContentPage
    {
        public StackPage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            OverlayHelper.AdjustView();
        }

        private async void popupTop_Clicked(object sender, EventArgs e)
        {
            await Task.Run(() =>
            {
                OverlayHelper.Open(new ExamplePopup1(), AbsoluteLayoutFlags.All,
                new Rectangle(0, 0, 1, 0.3), Color.FromHex("#4d000000"));
            });
        }

        private async void popupCenter_Clicked(object sender, EventArgs e)
        {
            await Task.Run(() =>
            {
                OverlayHelper.Open(new ExamplePopup1(), AbsoluteLayoutFlags.All,
                    new Rectangle(0, 0.5, 1, 0.3), Color.FromHex("#4d000000"));
            });
        }

        private async void popupBottom_Clicked(object sender, EventArgs e)
        {
            await Task.Run(() =>
            {
                OverlayHelper.Open(new ExamplePopup1(), AbsoluteLayoutFlags.All,
                    new Rectangle(0, 1, 1, 0.3), Color.FromHex("#4d000000"));
            });
        }

        private async void popupError_Clicked(object sender, EventArgs e)
        {
            await Task.Run(() =>
            {
                OverlayHelper.Open(new ExamplePopup2(), AbsoluteLayoutFlags.All,
                    new Rectangle(0, 0.5, 1, 0.3), Color.FromHex("#88000000"));
            });
        }
    }
}