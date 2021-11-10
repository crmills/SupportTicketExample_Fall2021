using Library.ToDo.Persistence;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.ToDoApplication.Models
{
    [JsonConverter(typeof(ItemJsonConverter))]
    public class Appointment : Item, INotifyPropertyChanged
    {
        public Appointment()
        {
            BoundStart = DateTime.Today;
            BoundEnd = DateTime.Today.AddDays(1);

            Priority = 10;
        }
        public ObservableCollection<string> Attendees { get; set; }
        public DateTime StartTime { get; set; }
        private DateTimeOffset boundStart;
        public DateTimeOffset BoundStart
        {
            get
            {
                return boundStart;
            }
            set
            {
                boundStart = value;
                StartTime = boundStart.Date;
                NotifyPropertyChanged("StartTime");
                NotifyPropertyChanged("SecondaryText");
            }
        }
        public DateTime EndTime { get; set; }
        private DateTimeOffset boundEnd;
        public DateTimeOffset BoundEnd
        {
            get
            {
                return boundEnd;
            }
            set
            {
                boundEnd = value;
                EndTime = boundEnd.Date;
                NotifyPropertyChanged("EndTime");
            }
        }

        public override string PrimaryText => $"Appointment: {Name} - {Description}";
        public override string SecondaryText => $"{Priority} {StartTime} - {EndTime}";
    }
}
