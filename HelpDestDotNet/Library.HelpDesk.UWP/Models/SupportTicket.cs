using Newtonsoft.Json;
using System;
using Utilities;

namespace Library.HelpDesk.Models
{
    [JsonConverter(typeof(ItemJsonConverter))]
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
