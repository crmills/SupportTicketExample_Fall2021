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
                    var settings = MongoClientSettings.FromConnectionString("mongodb+srv://xdbuser:2jxmVsJs6Fz4LP2f@xidentifydev.lds8g.mongodb.net/xIdentifyDev?retryWrites=true&w=majority");
                    var client = new MongoClient(settings);
                    var _db = client.GetDatabase("test");
                    instance = new Database(_db);
                }

                return instance;
            }
        }

        public ToDo AddOrUpdate(ToDo todo)
        {
            if(string.IsNullOrEmpty(todo._id))
            {
                todo._id = ObjectId.GenerateNewId().ToString();
            }

            //var todoBson = BsonSerializer.Serialize(todo, );

            var todoCollection = _database.GetCollection<BsonDocument>("todos");

            todoCollection.DeleteOne(Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(todo._id)));
            todoCollection.InsertOne(todo.ToBsonDocument());
            return todo;
        }

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
                return new List<Appointment>
                {
                    new Appointment { Name = "1st",
                               Description = "First Appointment"},
                    new Appointment { Name = "2nd",
                               Description = "Second Appointment"}
                };
            }

        }


        private Database(IMongoDatabase db)
        {
            _database = db;
        }

    }
}
