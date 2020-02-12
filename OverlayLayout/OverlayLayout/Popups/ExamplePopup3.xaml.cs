using OverlayLayout.Helper;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OverlayLayout.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExamplePopup3 : ContentView
    {
        public ExamplePopup3()
        {
            InitializeComponent();
        }

        private void OK_Clicked(object sender, EventArgs e)
        {
            OverlayHelper.Open(new ExamplePopup2(), AbsoluteLayoutFlags.All,
                new Rectangle(0, 1, 1, 0.3), Color.FromHex("#88000000"));
        }
    }
}