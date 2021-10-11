using HelpDesk.UWP.Dialogs;
using Library.HelpDesk.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace HelpDesk.UWP.ViewModels
{
    public class MainViewModel: INotifyPropertyChanged
    {
        public ItemBase SelectedTicket { get; set; }
        public ObservableCollection<ItemBase> SupportTickets { get; set; }
        private ObservableCollection<ItemBase> filteredTickets;
        public ObservableCollection<ItemBase> FilteredTickets
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Query))
                {
                    return SupportTickets;
                }
                else
                {
                    filteredTickets = new ObservableCollection<ItemBase>(SupportTickets
                        .Where(s => s.Description.ToUpper().Contains(Query.ToUpper())
                        || s.Name.ToUpper().Contains(Query.ToUpper())).ToList());
                    return filteredTickets;
                }
            }
        }
        public string Query { get; set; }

        private string persistencePath;
        private JsonSerializerSettings serializationSettings;
        public MainViewModel()
        {
            //SupportTickets = new ObservableCollection<ItemBase>();
            persistencePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

            //SupportTickets.Add(new SupportTicket { Name = "First", Description = "First Ticket", Priority = 3 });
            //SupportTickets.Add(new SupportTicket { Name = "Second", Description = "Third Ticket", Priority = 3 });
            //SupportTickets.Add(new SupportTicket { Name = "Third", Description = "Third Ticket", Priority = 3 });
            SupportTickets = new ObservableCollection<ItemBase>();
            var items = JsonConvert.DeserializeObject<List<ItemBase>>(new WebRequestHandler().Get("http://localhost:35259/Item").Result);
            foreach(var item in items)
            {
                if(item is SupportTicket)
                {
                    SupportTickets.Add(item as SupportTicket);
                } else
                {
                    SupportTickets.Add(item as Bug);
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Remove()
        {
            if (SelectedTicket == null)
            {
                return;
            }
            SupportTickets.Remove(SelectedTicket);
        }

        public async Task EditTicket()
        {
            var diag = new ItemDialog(SupportTickets, SelectedTicket);
            NotifyPropertyChanged("SelectedTicket");
            await diag.ShowAsync();
        }

        public void RefreshList()
        {
            NotifyPropertyChanged("FilteredTickets");
        }
    }
}
