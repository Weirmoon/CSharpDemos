using MauiDemo.Models;
using MauiDemo.PageModels;

namespace MauiDemo.Pages
{
    public partial class MainPage : ContentPage
    {
        public MainPage(MainPageModel model)
        {
            InitializeComponent();
            BindingContext = model;
        }
    }
}