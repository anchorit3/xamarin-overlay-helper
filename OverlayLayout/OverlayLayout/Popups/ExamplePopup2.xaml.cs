using OverlayLayout.Helper;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OverlayLayout.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExamplePopup2 : ContentView
    {
        public ExamplePopup2()
        {
            InitializeComponent();
        }

        private void OK_Clicked(object sender, System.EventArgs e)
        {
            OverlayHelper.Close();
        }
    }
}