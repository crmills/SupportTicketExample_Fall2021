using Library.ToDo.Persistence;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

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
        [BsonElement("Attendees")]
        public ObservableCollection<string> Attendees { get; set; }

        [BsonElement("StartTime")]
        public DateTime StartTime { get; set; }

        [BsonIgnore]
        private DateTimeOffset boundStart;
        [BsonIgnore]
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
        [BsonElement("EndTime")]
        public DateTime EndTime { get; set; }
        [BsonIgnore]
        private DateTimeOffset boundEnd;
        [BsonIgnore]
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
        [BsonIgnore]
        public override string PrimaryText => $"Appointment: {Name} - {Description}";
        [BsonIgnore]
        public override string SecondaryText => $"{Priority} {StartTime} - {EndTime}";
    }
}
