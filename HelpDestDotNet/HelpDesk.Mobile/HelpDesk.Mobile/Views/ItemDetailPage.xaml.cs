using HelpDesk.Mobile.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace HelpDesk.Mobile.Views
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