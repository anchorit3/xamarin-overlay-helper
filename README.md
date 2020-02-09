# Xamarin.Forms Overlay Helper
For open custom Popups over content in your app without custom renderer. 
You can customize popups how you wont, what you need is only one thing. 
Make new ContentView and then use as content of popup.


<img src="https://raw.githubusercontent.com/anchorit3/xamarin-overlay-helper/master/Images/s1.jpg" width="140"><img src="https://raw.githubusercontent.com/anchorit3/xamarin-overlay-helper/master/Images/s2.jpg" width="140"><img src="https://raw.githubusercontent.com/anchorit3/xamarin-overlay-helper/master/Images/s3.jpg" width="140"><img src="https://raw.githubusercontent.com/anchorit3/xamarin-overlay-helper/master/Images/s4.jpg" width="140"><img src="https://raw.githubusercontent.com/anchorit3/xamarin-overlay-helper/master/Images/s5.jpg" width="140"><img src="https://raw.githubusercontent.com/anchorit3/xamarin-overlay-helper/master/Images/s6.jpg" width="140">

This is not perfect solution, but now working with `Stacklayout`, `Grid` and `ScrollView` and with `NavigationPage` and without.
*(Propobly dont work with AppShell)*

My helper need only `Xamarin.forms` + `Xamarin.Esseltials`

#### To start using, import `OverlayHelper.cs` into your project and add `OnAppearing` in `ContentPage`:
```cs
protected override void OnAppearing()
{
    base.OnAppearing();
    OverlayHelper.AdjustView();
}
```
*This part move Stacklayout, Grid or ScrollView to AbsoluteLayout and adding overlay element to view*


#### Now you need only open Popup 😉
```cs
OverlayHelper.Open(new ExamplePopup1(), AbsoluteLayoutFlags.All,
                    new Rectangle(0, 0.5, 1, 0.4), Color.FromHex("#4d000000"));
```
*In this case we use* `(ContentView) ExamplePopup1` with `(AbsoluteLayoutFlags) LayoutFlags`, `(Rectangle) LayoutBounds` and `(Color)Overlay Background Color`

##### For more information about `AbsoluteLayoutFlags` you can go to [Microsoft documentation](https://docs.microsoft.com/en-us/xamarin/xamarin-forms/user-interface/layouts/absolute-layout)

#### How to close Popup programmatically?

```cs
OverlayHelper.Close();
```

#### License
Licensed under MIT, see license file.


*I think it may be available to many people. Best regards and have a nice use* 😁
