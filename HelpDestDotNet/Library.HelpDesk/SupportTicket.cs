using System;

namespace Library.HelpDesk
{
    public class SupportTicket
    {
        private static int currentId = 1;
        public SupportTicket()
        {
            Id = currentId++;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }
        public DateTime Deadline { get; set; }
        public DateTime DateAdded { get; set; }

        public SupportTicket Parent { get; set; }

        public override string ToString()
        {
            return $"{Id}. [{Priority}] {Name} - {Description} Due: {Deadline}";
        }
    }
}
