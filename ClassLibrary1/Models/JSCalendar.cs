using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Lib.Models
{
    //JSON objects are only allow a single root element
    //This shouldnt be necessary at all. The spec defines the output of each object. So better to allow them to be built individually.
    [Obsolete("Don't need this at all unless you want to be able to build multiple into one file")]
    public class JSCalendar
    {
        public IParentNode parentNode;

        public void SetParent(IParentNode parent)
        {
            parentNode = parent;
        }

        public string GetSerializedResult()
        {
            return JsonSerializer.Serialize(parentNode);//JSON.Text does a polymorphic downcast if you supply the type as object, https://khalidabuhakmeh.com/serialize-interface-instances-system-text-json
        }
    }
}
