using Lib.Models;
using System;
using System.Text.Json;

namespace Lib.Builders
{
    public class JSTaskBuilder: JSCommonBuilder<JSTask, JSTaskBuilder>
    {
        protected override JSTask _jsCalendarObject { get; set;  }


        public JSTaskBuilder()
        {
            _jsCalendarObject = new JSTask();
        }


        public JSTask Build()
        {
            return _jsCalendarObject;
        }
    }
}
