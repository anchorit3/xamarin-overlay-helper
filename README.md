# Xamarin.Forms Overlay Helper
For open custom Popups over content in your app without custom renderer. 

You can customize popups however you like, you only need one thing.

Create new ContentView and then use as content of Popup.


<img src="https://raw.githubusercontent.com/anchorit3/xamarin-overlay-helper/master/Images/s1.jpg" width="140"><img src="https://raw.githubusercontent.com/anchorit3/xamarin-overlay-helper/master/Images/s2.jpg" width="140"><img src="https://raw.githubusercontent.com/anchorit3/xamarin-overlay-helper/master/Images/s3.jpg" width="140"><img src="https://raw.githubusercontent.com/anchorit3/xamarin-overlay-helper/master/Images/s4.jpg" width="140"><img src="https://raw.githubusercontent.com/anchorit3/xamarin-overlay-helper/master/Images/s5.jpg" width="140"><img src="https://raw.githubusercontent.com/anchorit3/xamarin-overlay-helper/master/Images/s6.jpg" width="140">

Maybe this is not perfect solution, but now working with `Stacklayout`, `Grid` and `ScrollView` and with `NavigationPage` and without.

#### Requirements:
* Xamarin.Forms
* Xamarin.Esseltials

### To start using:

#### 1. Import `OverlayHelper.cs` into your project

#### 2. Add `AdjustView` method in `OnAppearing` in your `ContentPage`
```cs
OverlayHelper.AdjustView();
```

*Full example of OnAppearing:*
```cs
protected override void OnAppearing()
{
    base.OnAppearing();
    OverlayHelper.AdjustView();
}
```
*This part moves Stacklayout, Grid or ScrollView to AbsoluteLayout and adds an Overlay element to it*


#### 3. Now you need only open Popup 😉
```cs
OverlayHelper.Open(new ExamplePopup1(), AbsoluteLayoutFlags.All,
                    new Rectangle(0, 0.5, 1, 0.3), Color.FromHex("#4d000000"));
```
*In this case we use* `(ContentView) ExamplePopup1` with `(AbsoluteLayoutFlags) LayoutFlags`, `(Rectangle) LayoutBounds` and `(Color) Overlay Background Color`

##### For more information about `AbsoluteLayoutFlags` you can go to [Microsoft documentation](https://docs.microsoft.com/en-us/xamarin/xamarin-forms/user-interface/layouts/absolute-layout)

#### How to close Popup programmatically?

```cs
OverlayHelper.Close();
```

### Dropdown example
<img src="https://raw.githubusercontent.com/anchorit3/xamarin-overlay-helper/master/Images/s7.jpg" width="140"><img src="https://raw.githubusercontent.com/anchorit3/xamarin-overlay-helper/master/Images/s8.jpg" width="140">

*An example of a drop-down menu can be found in* `ScrollView Example Page` *(Green button)*


#### License
Licensed under MIT, see license file.


*I think it may be useful to many people. Best regards and have a nice use* 😁
