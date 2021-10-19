using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace Library.HelpDesk.Models
{
    [JsonConverter(typeof(ItemJsonConverter))]
    public class ItemBase
    {

        object _lock = new object();

        public ItemBase()
        {
        }

        public int Id { get; set; }

        private string name;
        public string Name {
            get
            {
                return name;
            }
            set
            {
                name = value;
            } 
        }
        public string Description { get; set; }
        public int Priority { get; set; }

        public DateTime DateAdded { get; set; }

    }
}
