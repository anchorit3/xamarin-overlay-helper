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
                FindContent(true);

                if (CurrentContent is StackLayout || CurrentContent is Grid || CurrentContent is ScrollView)
                {
                    Absolute = new AbsoluteLayout()
                    {
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                    };

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

                    OverrideContent(Absolute);
                }
            });
        }

        public static void Open(ContentView popUpContent, AbsoluteLayoutFlags absoluteLayoutFlags, Rectangle absoluteLayoutBounds, Color OverlayBgColor)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                LayoutBounds = absoluteLayoutBounds;
                PopUpContent = popUpContent;

                FindContent();
                if (!(CurrentContent is AbsoluteLayout)) AdjustView();
                else Absolute = (AbsoluteLayout)CurrentContent; 

                OverlayContainer.BackgroundColor = OverlayBgColor; 
                OverlayContainer.IsVisible = true;

                AddPopupToView(absoluteLayoutFlags);
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


        private static void AddPopupToView(AbsoluteLayoutFlags layoutFlags)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                AbsoluteLayout.SetLayoutFlags(PopUpContent, layoutFlags);
                AbsoluteLayout.SetLayoutBounds(PopUpContent, LayoutBounds);
                Absolute.Children.Add(PopUpContent);
            });
        }

        private static void FindContent(bool replace = false)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                if (Application.Current.MainPage.Navigation.NavigationStack.Count > 0)
                {
                    int index = Application.Current.MainPage.Navigation.NavigationStack.Count - 1;
                    if (((ContentPage)Application.Current.MainPage.Navigation.NavigationStack[index]) != null)
                    {
                        CurrentContent = ((ContentPage)Application.Current.MainPage.Navigation.NavigationStack[index]).Content;
                        if (replace) ((ContentPage)Application.Current.MainPage.Navigation.NavigationStack[index]).Content = null;
                    }
                    else
                    {
                        CurrentContent = ((ContentPage)((IShellSectionController)((Shell)Application.Current.MainPage).CurrentItem.CurrentItem).PresentedPage).Content;
                        if (replace) ((ContentPage)((IShellSectionController)((Shell)Application.Current.MainPage).CurrentItem.CurrentItem).PresentedPage).Content = null;
                    }
                }
                else
                {
                    CurrentContent = ((ContentPage)Application.Current.MainPage).Content;
                    if (replace) ((ContentPage)Application.Current.MainPage).Content = null;
                }
                CurrentContent.Parent = null;
            });
        }

        private static void OverrideContent(AbsoluteLayout absolute)
        {
            if (Application.Current.MainPage.Navigation.NavigationStack.Count > 0)
            {
                int index = Application.Current.MainPage.Navigation.NavigationStack.Count - 1;
                if (((ContentPage)Application.Current.MainPage.Navigation.NavigationStack[index]) != null)
                {
                    ((ContentPage)Application.Current.MainPage.Navigation.NavigationStack[index]).Content = absolute;
                }
                else
                {
                    ((ContentPage)((IShellSectionController)((Shell)Application.Current.MainPage).CurrentItem.CurrentItem).PresentedPage).Content = absolute;
                }
            }
            else
            {
                ((ContentPage)Application.Current.MainPage).Content = absolute;
            }
        }
    }
}
