using Library.ToDoApplication.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Api.ToDoApplication.Persistence
{
    public class Database
    {
        private IMongoDatabase _database;

        private static Database instance;
        public static Database Current
        {
            get
            {
                if(instance == null)
                {
                    var settings = MongoClientSettings.FromConnectionString("mongodb+srv://xdbuser:OIJoGwRpMQ0Rw0uU@xidentifydev.lds8g.mongodb.net/xIdentifyDev?retryWrites=true&w=majority");
                    var client = new MongoClient(settings);
                    var _db = client.GetDatabase("test");
                    instance = new Database(_db);
                }

                return instance;
            }
        }

        public void AddOrUpdate(Item item)
        {
            if (string.IsNullOrEmpty(item._id))
            {
                item._id = ObjectId.GenerateNewId().ToString();
            }

            //mapping for collections
            IMongoCollection<BsonDocument> collection;
            Item itemToPersist;
            if (item is ToDo)
            {
                collection = _database.GetCollection<BsonDocument>("todos");
                itemToPersist = item as ToDo;
            } else if (item is Appointment)
            {
                collection = _database.GetCollection<BsonDocument>("appointments");
                itemToPersist = item as Appointment;
            } else
            {
                throw new TypeNotSupportedException(item.GetType().ToString());
            }


            collection.DeleteOne(Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(itemToPersist._id)));
            collection.InsertOne(itemToPersist.ToBsonDocument());
            return;
        }

        //TODO: make a Delete method for removing ToDo and Appointment instances from MongoDB.
        //public bool Delete(string type, string id)
        //{
        //    if()
        //}

        public List<ToDo> ToDos {
            get
            {
                var todoBson = _database.GetCollection<BsonDocument>("todos");
                var data = todoBson.Find(_ => true).ToList();
                var _todos = new List<ToDo>();
                foreach(var item in data)
                {
                    var json = item.ToJson();
                    var obj = BsonSerializer.Deserialize<ToDo>(item);
                    _todos.Add(obj);
                }
                return _todos;
            }

        }
        
        public List<Appointment> Appointments 
        {
            get
            {
                var appBson = _database.GetCollection<BsonDocument>("appointments");
                var data = appBson.Find(_ => true).ToList();
                var _apps = new List<Appointment>();
                foreach (var item in data)
                {
                    var json = item.ToJson();
                    var obj = BsonSerializer.Deserialize<Appointment>(item);
                    _apps.Add(obj);
                }
                return _apps;
            }

        }


        private Database(IMongoDatabase db)
        {
            _database = db;
        }

    }

    public class TypeNotSupportedException : Exception
    {
        private string _type;
        public TypeNotSupportedException(string type)
        {
            _type = type;
        }
        public override string Message => $"Attempt was made to persist an unsupported type: {_type}";
    }
}
