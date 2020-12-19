﻿using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Models
{
    public class JSCalendar
    {
        public IParentNode parentNode;

        public void SetParent(IParentNode parent)
        {
            parentNode = parent;
        }

        public string GetSerializedResult()
        {
            return JsonSerializer.Serialize((object)parentNode);//JSON.Text does a polymorphic downcast if you supply the type as object, https://khalidabuhakmeh.com/serialize-interface-instances-system-text-json
        }
    }
}
