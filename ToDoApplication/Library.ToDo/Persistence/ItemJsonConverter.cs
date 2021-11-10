using Library.ToDoApplication.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.ToDo.Persistence
{
    public class ItemJsonConverter : JsonCreationConverter<Item>
    {
        protected override Item Create(Type objectType, JObject jObject)
        {
            if (jObject == null) throw new ArgumentNullException("jObject");

            if (jObject["Attendees"] != null || jObject["attendees"] != null)
            {
                return new Appointment();
            }
            else if (jObject["IsCompleted"] != null || jObject["isCompleted"] != null)
            {
                return new ToDoApplication.Models.ToDo();
            }
            else
            {
                return new Item();
            }
        }
    }
}
