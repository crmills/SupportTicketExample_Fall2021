using Library.HelpDesk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelpDesk.API
{
    public static class DataRepository
    {
        public static List<ItemBase> Items = new List<ItemBase> {
            new SupportTicket { Name = "First", Description = "First Ticket", Priority = 3, Deadline = DateTime.Today, Id = 1 },
            new SupportTicket { Name = "Second", Description = "Second Ticket", Priority = 3, Deadline = DateTime.Today, Id = 2 },
            new SupportTicket { Name = "Third", Description = "Third Ticket", Priority = 3, Deadline = DateTime.Today, Id = 3 }
        };

        private static int LastInt
        {
            get
            {
                return Items.Max(i => i.Id);
            }
        }

        public static void AddItem(ItemBase item)
        {
            item.Id = LastInt + 1;
            Items.Add(item);
        }
    }
}
