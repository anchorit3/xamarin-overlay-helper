using System;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace OverlayLayout
{
    public static class OverlayHelper
    {
        public static View CurrentContent;
        public static AbsoluteLayout Absolute;
        public static StackLayout OverlayContainer;
        public static ContentView PopUpContent;
        public static Rectangle LayoutBounds;

        public static void AdjustView()
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                Absolute = new AbsoluteLayout()
                {
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                };
                if (Application.Current.MainPage.Navigation.NavigationStack.Count > 0)
                {
                    int index = Application.Current.MainPage.Navigation.NavigationStack.Count - 1;
                    CurrentContent = ((ContentPage)Application.Current.MainPage.Navigation.NavigationStack[index]).Content;
                }
                else
                {
                    CurrentContent = ((ContentPage)Application.Current.MainPage).Content;
                }

                if (CurrentContent is StackLayout || CurrentContent is Grid || CurrentContent is ScrollView)
                {
                    var gestureRecognizer = new TapGestureRecognizer();
                    gestureRecognizer.Tapped += OverlayTapped;

                    AbsoluteLayout.SetLayoutFlags(CurrentContent, AbsoluteLayoutFlags.All);
                    AbsoluteLayout.SetLayoutBounds(CurrentContent, new Rectangle(0, 0, 1, 1));

                    OverlayContainer = new StackLayout()
                    {
                        BackgroundColor = Color.FromHex("#55000000"),
                        IsVisible = false,
                    };
                    AbsoluteLayout.SetLayoutFlags(OverlayContainer, AbsoluteLayoutFlags.All);
                    AbsoluteLayout.SetLayoutBounds(OverlayContainer, new Rectangle(0, 0, 1, 1));

                    OverlayContainer.GestureRecognizers.Add(gestureRecognizer);
                    Absolute.Children.Add(CurrentContent);
                    Absolute.Children.Add(OverlayContainer);

                    if (Application.Current.MainPage.Navigation.NavigationStack.Count > 0)
                    {
                        int index = Application.Current.MainPage.Navigation.NavigationStack.Count - 1;
                        ((ContentPage)Application.Current.MainPage.Navigation.NavigationStack[index]).Content = Absolute;
                    }
                    else
                    {
                        ((ContentPage)Application.Current.MainPage).Content = Absolute;
                    }
                }
            });
        }

        public static void Open(ContentView popUpContent, AbsoluteLayoutFlags absoluteLayoutFlags, Rectangle absoluteLayoutBounds, Color OverlayBgColor)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                LayoutBounds = absoluteLayoutBounds;
                PopUpContent = popUpContent;

                if (Application.Current.MainPage.Navigation.NavigationStack.Count > 0)
                {
                    int index = Application.Current.MainPage.Navigation.NavigationStack.Count - 1;
                    CurrentContent = ((ContentPage)Application.Current.MainPage.Navigation.NavigationStack[index]).Content;
                }
                else
                {
                    CurrentContent = ((ContentPage)Application.Current.MainPage).Content;
                }

                if (!(CurrentContent is AbsoluteLayout)) AdjustView();
            
                OverlayContainer.BackgroundColor = OverlayBgColor; 
                OverlayContainer.IsVisible = true;

                AddPopupToView(absoluteLayoutFlags);
            });
        }

        public static void AddPopupToView(AbsoluteLayoutFlags layoutFlags)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                AbsoluteLayout.SetLayoutFlags(PopUpContent, layoutFlags);
                AbsoluteLayout.SetLayoutBounds(PopUpContent, LayoutBounds);
                Absolute.Children.Add(PopUpContent);
            });
        }

        public static void Close()
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                OverlayContainer.IsVisible = false;
                Absolute.Children.Remove(PopUpContent);
            });
        }

        private static void OverlayTapped(object sender, EventArgs e) => Close();
    }
}
