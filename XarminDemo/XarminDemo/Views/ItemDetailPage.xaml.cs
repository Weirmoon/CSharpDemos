using System.ComponentModel;
using Xamarin.Forms;
using XarminDemo.ViewModels;

namespace XarminDemo.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}