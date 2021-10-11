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
        private static int currentId = 1;
        private int _id = -1;

        object _lock = new object();

        public ItemBase()
        {
        }

        public int Id { 
            get
            {
                lock(_lock)
                {
                    if (_id < 0)
                    {
                        _id = currentId++;
                    }
                    return _id;
                }

            }
        }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }

        public DateTime DateAdded { get; set; }

    }
}
