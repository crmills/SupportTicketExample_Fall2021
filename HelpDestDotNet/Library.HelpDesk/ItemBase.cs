using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.HelpDesk
{
    public class ItemBase
    {
        private static int currentId = 1;
        private int _id = -1;
        public ItemBase()
        {
        }

        public int Id { 
            get
            {
                if(_id < 0)
                {
                    _id = currentId++;
                }
                return _id;
            }
        }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }

        public DateTime DateAdded { get; set; }

    }
}
