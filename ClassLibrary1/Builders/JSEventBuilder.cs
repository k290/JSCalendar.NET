using Lib.Models;
using System;
using System.Text.Json;

namespace Lib.Builders
{
    public class JSEventBuilder
    {
        private JSEvent _jsEvent;

        public JSEventBuilder()
        {
            _jsEvent = new JSEvent();
        }


        public JSEventBuilder WithUid(string uid) //might want to autogenerate in the future and provide a GetUID method
        {
            _jsEvent.uid = uid;
            return this;
        }

        internal JSEvent Build()
        {
            return _jsEvent;
        }
    }
}
