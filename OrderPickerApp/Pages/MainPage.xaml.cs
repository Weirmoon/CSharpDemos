using OrderPickerApp.Models;
using OrderPickerApp.PageModels;

namespace OrderPickerApp.Pages;

public partial class MainPage : ContentPage
{
    public MainPage(MainPageModel model)
    {
        InitializeComponent();
        BindingContext = model;
    }
}