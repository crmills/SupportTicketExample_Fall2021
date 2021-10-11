using Library.HelpDesk.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Utilities
{
    public class ItemJsonConverter : JsonCreationConverter<ItemBase>
    {
        protected override ItemBase Create(Type objectType, JObject jObject)
        {
            if (jObject == null) throw new ArgumentNullException("jObject");

            if (jObject["RepoSteps"] != null || jObject["repoSteps"] != null)
            {
                return new Bug();
            }
            else if (jObject["Deadline"] != null || jObject["deadline"] != null)
            {
                return new SupportTicket();
            }
            else
            {
                return new ItemBase();
            }
        }
    }
}
