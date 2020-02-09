using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using OverlayLayout.Popups;

namespace OverlayLayout.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScrollPage : ContentPage
    {
        public ScrollPage()
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

        private async void dropdown_Clicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var dropdown = new ContentView();

            var dropdownHeight = 200;
            var dropdownContent = new Frame()
            {
                Margin = new Thickness(20, 0),
                CornerRadius = 20,
                HeightRequest = dropdownHeight,
                BackgroundColor = Color.FromHex("#F5F5F5"),
            };

            var list = new ListView()
            {
                ItemsSource = new List<string> {
                    "test option 1",
                    "test option 2",
                    "test option 3",
                    "test option 4",
                    "test option 5",
                }
            };
            list.ItemSelected += ((object s, SelectedItemChangedEventArgs ev) =>
            {
                button.Text = ((string)((ListView)s).SelectedItem);
                OverlayHelper.Close();
            });

            dropdownContent.Content = list;

            dropdown.Content = dropdownContent;

            var dropdownContentY = button.Y + button.Height + 5;
            var parent = (VisualElement)button.Parent;

            while (parent != null)
            {
                if (parent.Parent.GetType() == typeof(VisualElement) || parent.Parent.GetType() == typeof(ScrollView))
                {
                    if (parent.Parent.GetType() == typeof(VisualElement))
                    {
                        dropdownContentY += parent.Y;
                        parent = (VisualElement)parent.Parent;
                    }
                    else if (parent.Parent.GetType() == typeof(ScrollView))
                    {
                        dropdownContentY -= ((ScrollView)parent.Parent).ScrollY;
                        parent = null;
                    }
                }
            }
            if((dropdownContentY + dropdownHeight) > Application.Current.MainPage.Height)
            {
                dropdownContentY -= (dropdownHeight + button.Height + 10);
            }

            await Task.Run(() =>
            {
                OverlayHelper.Open(dropdown, AbsoluteLayoutFlags.WidthProportional,
                    new Rectangle(0, dropdownContentY, 1, dropdownHeight), Color.FromHex("#00000000"));
            });
        }
    }
}