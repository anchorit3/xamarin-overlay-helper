using System;
using System.Collections.Generic;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace OverlayLayout.Helper
{
    public static class OverlayHelper
    {
        public static bool IsOpen;
        public static View CurrentContent;
        public static AbsoluteLayout Absolute;
        public static List<StackLayout> OverlayContainer;
        public static List<ContentView> PopUpContent;
        public static Rectangle LayoutBounds;
        public static TapGestureRecognizer OverlayTapGesture;


        public static event EventHandler<OverlayStateEventArgs> OverlayStateChanged = delegate { };

        public static void AdjustView()
        {
            OverlayTapGesture = new TapGestureRecognizer();
            OverlayTapGesture.Tapped += OverlayTapped;

            MainThread.BeginInvokeOnMainThread(() =>
            {
                FindContent(true);

                if (CurrentContent is StackLayout || CurrentContent is Grid || CurrentContent is ScrollView)
                {
                    PopUpContent = new List<ContentView>();
                    OverlayContainer = new List<StackLayout>();

                    Absolute = new AbsoluteLayout()
                    {
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                    };


                    AbsoluteLayout.SetLayoutFlags(CurrentContent, AbsoluteLayoutFlags.All);
                    AbsoluteLayout.SetLayoutBounds(CurrentContent, new Rectangle(0, 0, 1, 1));

                    Absolute.Children.Add(CurrentContent);
                }
                OverrideContent(Absolute);
            });
        }

        public static void Open(ContentView popUpContent, AbsoluteLayoutFlags absoluteLayoutFlags, Rectangle absoluteLayoutBounds, Color overlayBgColor)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                LayoutBounds = absoluteLayoutBounds;
                PopUpContent.Add(popUpContent);

                FindContent();
                if (!(CurrentContent is AbsoluteLayout)) AdjustView();
                else Absolute = (AbsoluteLayout)CurrentContent;

                AddOverlayToView(overlayBgColor);
                AddPopupToView(absoluteLayoutFlags);

                ChangeStateOverlay(true);
            });
        }

        public static void Close(bool triggerEvent = true)
        {
            if (triggerEvent)
            {
                ChangeStateOverlay(false);
            }

            MainThread.BeginInvokeOnMainThread(() =>
            {
                if (PopUpContent != null && OverlayContainer != null)
                {
                    if (PopUpContent.Count != 0 && OverlayContainer.Count != 0)
                    {
                        Absolute.Children.Remove(PopUpContent[PopUpContent.Count - 1]);
                        PopUpContent.Remove(PopUpContent[PopUpContent.Count - 1]);
                        Absolute.Children.Remove(OverlayContainer[OverlayContainer.Count - 1]);
                        OverlayContainer.Remove(OverlayContainer[OverlayContainer.Count - 1]);
                    }
                }
            });
        }

        private static void OverlayTapped(object sender, EventArgs e) => Close();


        private static void AddOverlayToView(Color overlayBgColor)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                OverlayContainer.Add(new StackLayout()
                {
                    BackgroundColor = Color.FromHex("#55000000"),
                });
                AbsoluteLayout.SetLayoutFlags(OverlayContainer[OverlayContainer.Count - 1], AbsoluteLayoutFlags.All);
                AbsoluteLayout.SetLayoutBounds(OverlayContainer[OverlayContainer.Count - 1], new Rectangle(0, 0, 1, 1));

                OverlayContainer[OverlayContainer.Count - 1].BackgroundColor = overlayBgColor;
                OverlayContainer[OverlayContainer.Count - 1].GestureRecognizers.Add(OverlayTapGesture);
                Absolute.Children.Add(OverlayContainer[OverlayContainer.Count - 1]);
            });
        }
        private static void AddPopupToView(AbsoluteLayoutFlags layoutFlags)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                AbsoluteLayout.SetLayoutFlags(PopUpContent[PopUpContent.Count - 1], layoutFlags);
                AbsoluteLayout.SetLayoutBounds(PopUpContent[PopUpContent.Count - 1], LayoutBounds);
                Absolute.Children.Add(PopUpContent[PopUpContent.Count - 1]);
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

        public static void ChangeStateOverlay(bool state)
        {
            IsOpen = state;
            OverlayStateChanged?.Invoke(null, new OverlayStateEventArgs(state));
        }
    }
}
