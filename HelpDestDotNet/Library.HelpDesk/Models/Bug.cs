using Newtonsoft.Json;
using System.Collections.Generic;
using Utilities;

namespace Library.HelpDesk.Models
{
    [JsonConverter(typeof(ItemJsonConverter))]
    public class Bug : ItemBase
    {
        public List<string> RepoSteps = new List<string>();
    }
}
