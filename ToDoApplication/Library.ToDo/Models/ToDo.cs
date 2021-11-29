using Library.ToDo.Persistence;
using Library.ToDoApplication.Persistence;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Library.ToDoApplication.Models
{
    [JsonConverter(typeof(ItemJsonConverter))]
    public class ToDo : Item, INotifyPropertyChanged
    {
        [BsonElement("isCompleted"), BsonRequired]
        private bool isCompleted;

        [BsonIgnore]
        public bool IsCompleted {
            get
            {
                return isCompleted;
            }
            set
            {
                isCompleted = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged("Completed");
            }
        }

        [BsonIgnore]
        public override bool Completed { get => IsCompleted; set => IsCompleted = value; }
        [BsonElement("deadline")]
        public DateTime Deadline { get; set; }

        [BsonIgnore]
        public override string PrimaryText => $"ToDo: {Name} - {Description}";
        [BsonIgnore]
        public override string SecondaryText => $"{Priority} {IsCompleted}";
        public override string ToString()
        {
            return $"{IsCompleted} [{Priority}] {Name} - {Description}";
        }

        public ToDo()
        {
            Priority = 1;
        }
    }
}
