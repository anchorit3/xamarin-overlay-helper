using OverlayLayout.Helper;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OverlayLayout.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExamplePopup1 : ContentView
    {
        public ExamplePopup1()
        {
            InitializeComponent();
        }

        private void newLayerClicked(object sender, System.EventArgs e)
        {
            OverlayHelper.Open(new ExamplePopup3(), AbsoluteLayoutFlags.All,
                new Rectangle(0, 0.5, 1, 0.3), Color.FromHex("#88000000"));
        }
    }
}