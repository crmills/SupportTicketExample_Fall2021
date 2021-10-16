using Library.HelpDesk.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace HelpDesk.UWP.ViewModels
{
    public class ItemDialogViewModel : INotifyPropertyChanged
    {
        public Visibility ShowBug
        {
            get
            {
                if(BackingItem is Bug)
                {
                    return Visibility.Visible;
                } else
                {
                    return Visibility.Collapsed;
                }
            }
        }

        public Visibility ShowTicket
        {
            get
            {
                if (BackingItem is SupportTicket)
                {
                    return Visibility.Visible;
                }
                else
                {
                    return Visibility.Collapsed;
                }
            }
        }

        public ItemBase BackingItem { get; set; }

        private string itemType;
        public string ItemType { 
            get {
                return itemType;
            } 
            set {
                if(value != null && !value.Equals(itemType, StringComparison.InvariantCultureIgnoreCase))
                {
                    itemType = value;
                    if (value.Equals("Support Ticket", StringComparison.InvariantCultureIgnoreCase))
                    {
                        BackingItem = new SupportTicket();
                    }
                    else if (value.Equals("Bug", StringComparison.InvariantCultureIgnoreCase))
                    {
                        BackingItem = new Bug();
                    }
                    else
                    {
                        BackingItem = null; //bad things are afoot
                    }
                    NotifyPropertyChanged();
                    NotifyPropertyChanged("ShowBug");
                    NotifyPropertyChanged("ShowTicket");
                }
            } 
        }

        public ItemDialogViewModel()
        {
            ItemType = null;
        }

        public ItemDialogViewModel(ItemBase item)
        {
            BackingItem = item;
            if(BackingItem is SupportTicket)
            {
                ItemType = "Support Ticket";
            } else if(BackingItem is Bug) {
                ItemType = "Bug";
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
