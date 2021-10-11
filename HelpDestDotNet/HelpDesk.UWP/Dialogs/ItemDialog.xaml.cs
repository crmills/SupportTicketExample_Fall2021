using Library.HelpDesk.Models;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace HelpDesk.UWP.Dialogs
{
    public sealed partial class ItemDialog : ContentDialog
    {
        private IList<ItemBase> supportTickets;
        public ItemDialog(IList<ItemBase> supportTickets)
        {
            InitializeComponent();
            DataContext = new SupportTicket();
            this.supportTickets = supportTickets;
        }

        public ItemDialog(IList<ItemBase> supportTickets, ItemBase ticket)
        {
            InitializeComponent();
            DataContext = ticket;
            this.supportTickets = supportTickets;
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            var ticketToEdit = DataContext as SupportTicket;
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


        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {

        }
    }
}
