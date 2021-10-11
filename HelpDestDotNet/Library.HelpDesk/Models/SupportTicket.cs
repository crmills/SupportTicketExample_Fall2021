using System;

namespace Library.HelpDesk.Models
{
    public class SupportTicket : ItemBase
    {
        public SupportTicket() : base() {

        }

        public DateTime Deadline { get; set; }

        public override string ToString()
        {
            return $"{Id}. [{Priority}] {Name} - {Description} Due: {Deadline}";
        }
    }
}
