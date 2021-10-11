using Library.HelpDesk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.HelpDesk.ViewModels
{ 
    public class MainViewModel
    {
        public List<ItemBase> Tickets { get; set; }
        public ItemBase SelectedTicket { get; set; }
        public MainViewModel ()
        {
            Tickets = new List<ItemBase>();
        }

        public void AddTicket()
        {
            if(SelectedTicket == null)
            {
                Tickets.Add(new ItemBase());
            }
        }

        public void DeleteTicket()
        {
            if(SelectedTicket != null)
            {
                Tickets.Remove(SelectedTicket);
            }
        }
    }
}
