using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Models
{
    public class JSCalendar
    {
        [JsonInclude]
        public IList<JSEvent> _jsEvents = new List<JSEvent>();


        public void AddEvent(JSEvent jsEvent)
        {
            _jsEvents.Add(jsEvent);
        }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
