using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Models
{
    public class JSCalendar
    {
        [JsonInclude]
        public JSEvent jsEvent;


        public void AddEvent(JSEvent jsEventToSet)
        {
            jsEvent = jsEventToSet;
        }

        public  string GetSerializedResult()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
