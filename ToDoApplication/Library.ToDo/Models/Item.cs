using Library.ToDo.Persistence;
using MongoDB.Bson;
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
    public class Item : INotifyPropertyChanged
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id
        {
            get; set;
        }

        [BsonElement("name")]
        private string name;

        public event PropertyChangedEventHandler PropertyChanged;

        [BsonIgnore]
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged("PrimaryText");
                NotifyPropertyChanged("SecondaryText");
            }
        }

        [BsonElement("description")]
        private string description;
        [BsonIgnore]
        public string Description { 
            get
            {
                return description;
            }
            set
            {
                description = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged("PrimaryText");
                NotifyPropertyChanged("SecondaryText");
            }
        }

        [BsonElement("priority")]
        public int Priority { get; set; }

        public virtual string PrimaryText { get; }
        public virtual string SecondaryText { get; }

        public virtual bool Completed { get; set; }

        internal void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
