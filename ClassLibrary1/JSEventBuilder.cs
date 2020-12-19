using Models;
using System;
using System.Text.Json;

namespace Builder
{
    public class JSEventBuilder
    {
        private JSEvent _jsEvent;

        public JSEventBuilder()
        {
            _jsEvent = new JSEvent();
        }

 
        public JSEventBuilder WithStart()
        {
            return this;
        }
  
        internal JSEvent Build()
        {
            return _jsEvent;
        }
    }
}
