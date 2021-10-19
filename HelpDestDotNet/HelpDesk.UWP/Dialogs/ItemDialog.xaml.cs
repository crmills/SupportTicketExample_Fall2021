using Library.HelpDesk.Models;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using Utilities;
using Newtonsoft.Json;
using HelpDesk.UWP.ViewModels;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace HelpDesk.UWP.Dialogs
{
    public sealed partial class ItemDialog : ContentDialog
    {
        private IList<ItemBase> supportTickets;

        //constructor for a NEW item
        public ItemDialog(IList<ItemBase> supportTickets)
        {
            InitializeComponent();
            DataContext = new ItemDialogViewModel();
            this.supportTickets = supportTickets;
        }

        public ItemDialog(IList<ItemBase> supportTickets, ItemBase item)
        {
            InitializeComponent();
            DataContext = new ItemDialogViewModel(item);
            this.supportTickets = supportTickets;
        }

        private async void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            var ticketToEdit = (DataContext as ItemDialogViewModel)?.BackingItem;
            var i = supportTickets.IndexOf(ticketToEdit);
            if (i >= 0)
            {
                supportTickets.Remove(ticketToEdit);
                supportTickets.Insert(i, ticketToEdit);
            }
            else
            {
                supportTickets.Add(ticketToEdit);
            }

            var itemFromServer = JsonConvert.DeserializeObject<ItemBase>(await new WebRequestHandler().Post("http://localhost:35259/Item/AddOrUpdate", ticketToEdit));

        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {

        }
    }
}
